module ManaShield

open System.Collections.Generic

open SkyrimPlatform
open PatternNoWarn
open BindingWrapper
open Core

open FSharp.UMX

module Plugin =
  let targets = HashSet<ShieldedActor>()
  
  let pluginName        = "ManaShield.esp"
  let effectFormID      = 2050.
  let perkFormID        = 2048.
  
  let manaShieldEffect() =
    match sp.Game.GetFormFromFile effectFormID pluginName with
    | MagicEffectFromForm effect ->
      DebugTrace "Find some effect"
      Some effect
    | _ ->
      DebugTrace "No find effect"
      None
  let manaShieldPerk()   =
    match sp.Game.GetFormFromFile perkFormID pluginName with
    | PerkFromForm perk ->
      DebugTrace "Find some perk"
      Some perk
    | _ ->
      DebugTrace "No find perk"
      None
  
  let mutable damageReduceMult  = 0.0
  let mutable multMagickaDamage = 1.0

module private OnMagicEffectApply =

  let private addToManaShieldTargets (ctx: MagicEffectApplyEvent) (shieldEffect: MagicEffect) =
    
    DebugTrace $"Add to targets... sh effect - {shieldEffect.getFormID()} | effect - {ctx.effect.getFormID()}"
    
    match (shieldEffect.getFormID() = ctx.effect.getFormID(), ctx.target) with
    | IsManaShieldEffect actor ->
      DebugTrace $"Add: Is Shield effect for actor id: {actor.getFormID()}"
      actor.TryAddPerk <| Plugin.manaShieldPerk()
      let result = Plugin.targets.Add(ShieldedActor.CreateFromActor actor)
      DebugTrace $"Result of add - {result}"
    | _ -> ()
    
  sp.on_magicEffectApply <| fun ctx ->
    DebugTrace "Effect start debug"
    match Plugin.manaShieldEffect() with
    | Some effect ->
      DebugTrace $"Debug: shield effect id - {effect.getFormID()}"
      addToManaShieldTargets      ctx effect
    | None -> ()

module private OnceUpdate =
  
  let setMultiplays newValue =
    Plugin.damageReduceMult  <- newValue
    Plugin.multMagickaDamage <- 1.0 - Plugin.damageReduceMult
    
    DebugTrace "Set new value to damage reduce:"
    DebugTrace $"__ reduceMult - {Plugin.damageReduceMult} | magickaMult - {Plugin.multMagickaDamage}"
  
  let private setPerkValue (perk: Perk) id value =
    match perk.SetNthEntryValue id 0. value with
    | true  -> DebugTrace $"Success set new value to perk: ID: {id} | Value: {value}"
    | false -> DebugTrace $"Failure set new value to perk: ID: {id} | Value: {value}"
    
  let private firstValueOfPerk (perk: Perk) =
    let v = perk.GetNthEntryValue 0. 0.
    DebugTrace $"Value from perk {v}"
    if v <= 0.1 then
      0.1
    else v
    
  let private handlePerk (perk: Perk) =

    let entriesCounter = int(perk.getNumEntries()) - 1
    
    if entriesCounter > 0 then
      
      let value = firstValueOfPerk perk
        
      for id in 0..entriesCounter do
        setPerkValue perk id value
        
      setMultiplays value
      
    else
      DebugTrace $"EmptyEntryes of perk - {perk.getFormID()}"
      
  sp.once_update <| fun _ ->
    
    DebugTrace "Rising once update"
    DebugTrace $"__ plugin name - {Plugin.pluginName} | perkID - {Plugin.perkFormID} | effectID - {Plugin.effectFormID}"
      
    Option.iter handlePerk ^ Plugin.manaShieldPerk()

module private OnUpdate =
  
  let private removeFromManaShieldTargets shieldedActor (self: Actor) =
  
    DebugTrace $"Remove actor: actorId - {shieldedActor.FormID}"
    self.TryRemovePerk <| Plugin.manaShieldPerk()
    let result = Plugin.targets.Remove(shieldedActor)
    DebugTrace $"Result of remove - {result}"

  let private evaluteHealthMagickaDamage (sa: ShieldedActor) (self: Actor) healthCtx delta =
    let fullDamage    = delta / Plugin.damageReduceMult
    let magickaDamage = fullDamage * Plugin.multMagickaDamage
    let magicka       = self.GetActorValue ActorValue.Magicka
    
    DebugTrace $"DEBUG TRACE: ReduceMult {Plugin.damageReduceMult} | MagickaMult {Plugin.multMagickaDamage}"
    DebugTrace $"DEBUG TRACE: FullDamage {fullDamage} | Delta {delta}"
    DebugTrace $"Damage magicka: PlayerMagicka - {magicka} | MagickaDamage - {magickaDamage}"
    
    match magickaDamage > %magicka with
    | true ->
      let damageHealth = magickaDamage - %magicka
      
      DebugTrace $"Damage magicka delta: DamageHealth - {damageHealth}"
      
      self.DamageActorValue ActorValue.Health  damageHealth
      self.DamageActorValue ActorValue.Magicka %magicka
      
      sa.UpdateHealthInternal self
    | false ->
      sa.UpdateHealth healthCtx.CurrentHealth healthCtx.CurrentHealthPercent
      self.DamageActorValue ActorValue.Magicka magickaDamage
  
  let private handleHealthValue (sa: ShieldedActor) self (healthCtx: HealthContext) =
    match healthCtx with
      | MoreHealth _ 
      | EqualHealth      -> sa.UpdateHealth healthCtx.CurrentHealth healthCtx.CurrentHealthPercent
      | LessHealth delta -> evaluteHealthMagickaDamage sa self healthCtx delta
  
  let private handleHealthPercent (sa: ShieldedActor) self =
    let healthCtx = sa.HealthContext self
    match healthCtx with
      | PercentMoreHealth
      | PercentEqualHealth -> sa.UpdateHealth healthCtx.CurrentHealth healthCtx.CurrentHealthPercent
      | PercentLessHealth  -> handleHealthValue sa self healthCtx
  
  let private handleTarget (sa: ShieldedActor) =
    let self = sa.Self()
    if not ^ self.TryHasMagicEffect (Plugin.manaShieldEffect()) then
        removeFromManaShieldTargets sa self
    else
      handleHealthPercent sa self
  
  sp.on_update <| fun _ ->
    
    for sa in Plugin.targets do
      
      handleTarget sa