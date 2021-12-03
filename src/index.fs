module ManaShield

open System.Collections.Generic
open System.Diagnostics
open Fable.Core


open SkyrimPlatform
open PatternNoWarn
open BindingWrapper
open Core

open FSharp.UMX

let targets = HashSet<ShieldedActor>()

let mutable lastHealth        : ValueOfActorValue   = %0.
let mutable lastHealthPercent : PercentOfActorValue = %1.

let pluginName        = "ManaShield.esp"
let effectFormID      = 2050.
let perkFormID        = 2048.

let manaShieldEffect() =
  match sp.Game.GetFormFromFile effectFormID pluginName with
  | MagicEffectFromForm effect -> Some effect
  | _ -> None
let manaShieldPerk()   =
  match sp.Game.GetFormFromFile perkFormID pluginName with
  | PerkFromForm perk -> Some perk
  | _ -> None

let mutable damageReduceMult  = 0.0
let mutable multMagickaDamage = 1.0

let inline DebugTrace (message: string) =
  if false then
    sp.printConsole(message)

let inline addToManaShieldTargets      (ctx: MagicEffectApplyEvent) (shieldEffect: MagicEffect) =
  
  DebugTrace $"Add to targets... sh effect - {shieldEffect.getFormID()} | effect - {ctx.effect.getFormID()}"
  
  match (shieldEffect.getFormID() = ctx.effect.getFormID(), ctx.target) with
  | IsManaShieldEffect actor ->
    DebugTrace $"Add: Is Shield effect for actor id: {actor.getFormID()}"
    actor.TryAddPerk <| manaShieldPerk()
    let result = targets.Add(ShieldedActor.CreateFromActor actor)
    DebugTrace $"Result of add - {result}"
  | _ -> ()
  
let inline removeFromManaShieldTargets shieldedActor (self: Actor) =
  
  DebugTrace $"Remove actor: actorId - {shieldedActor.FormID}"
  self.TryRemovePerk <| manaShieldPerk()
  let result = targets.Remove(shieldedActor)
  DebugTrace $"Result of remove - {result}"
  
sp.on_magicEffectApply <| fun ctx ->
  DebugTrace "Effect start debug"
  match manaShieldEffect() with
  | Some effect ->
    DebugTrace $"Debug: shield effect id - {effect.getFormID()}"
    addToManaShieldTargets      ctx effect
  | None -> ()

sp.once_update <| fun _ ->
  
  let inline setPerkValue (perk: Perk) id value =
    match perk.SetNthEntryValue id 0. value with
    | true  -> DebugTrace $"Success set new value to perk: ID: {id} | Value: {value}"
    | false -> DebugTrace $"Failure set new value to perk: ID: {id} | Value: {value}"
  
  DebugTrace $"Rising once update: plugin name - {pluginName} | perkID - {perkFormID} | effectID - {effectFormID}"
  
  match manaShieldEffect() with
  | Some _ ->
    DebugTrace "Find some effect"
  | _ ->
    DebugTrace "No find effect"
    
  match manaShieldPerk() with
  | Some p ->
    DebugTrace "Find some perk"
    let entriesCounter = int(p.getNumEntries()) - 1
    
    if entriesCounter > 0 then
      
      let value =
        let v = p.GetNthEntryValue 0. 0.
        DebugTrace $"vALUE {v}"
        if v <= 0.1 then
          0.1
        else v
        
      for id in 0..entriesCounter do
        setPerkValue p id value
        
      damageReduceMult  <- value
      multMagickaDamage <- 1.0 - damageReduceMult
      
      DebugTrace $"Set new value to damage reduce: reduceMult - {damageReduceMult} | magickaMult - {multMagickaDamage}"
    else
      DebugTrace $"EmptyEntryes of perk - {p.getFormID()}"
  | _ ->
    DebugTrace "No find perk"

sp.on_update <| fun _ ->
  
  for sa in targets do
    
    let self = sa.Self()
    if not <| self.hasMagicEffect(manaShieldEffect()) then
      removeFromManaShieldTargets sa self
    else
      let healthCtx = sa.HealthContext self
      
      match healthCtx with
      | PercentMoreHealth
      | PercentEqualHealth -> sa.UpdateHealth healthCtx.CurrentHealth healthCtx.CurrentHealthPercent
      | PercentLessHealth  ->
        match healthCtx with
        | MoreHealth _ 
        | EqualHealth      -> sa.UpdateHealth healthCtx.CurrentHealth healthCtx.CurrentHealthPercent
        | LessHealth delta ->
          
          let fullDamage    = delta / damageReduceMult
          let magickaDamage = fullDamage * multMagickaDamage
          let magicka       = self.GetActorValue ActorValue.Magicka
          
          DebugTrace $"DEBUG TRACE: ReduceMult: {damageReduceMult} | MagickaMult {multMagickaDamage} | FullDamage {fullDamage} | Delta {delta}"
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