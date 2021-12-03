module ManaShield

open Fable.Core

open SkyrimPlatform
open PatternNoWarn
open BindingWrapper
open Core

open FSharp.UMX
let mutable lastHealth        : ValueOfActorValue   = %0.
let mutable lastHealthPercent : PercentOfActorValue = %1.

let damageReduceMult  = 0.7
let multMagickaDamage = 1.0 - damageReduceMult

[<ImportAll("./skyrimPlatform.declare")>]
let sp: IExports = jsNative

sp.on_update <| fun _ ->
  
  let player        = sp.Game.getPlayer()
  let healthPercent = player.GetActorValuePercentage ActorValue.Health
  let health        = player.GetActorValue           ActorValue.Health
  let magicka       = player.GetActorValue           ActorValue.Magicka
  
  let healthCtx =
    {
      LastHealth           = lastHealth
      CurrentHealth        = health
      LastHealthPercent    = lastHealthPercent
      CurrentHealthPercent = healthPercent
    }
  
  let updateHealth health percentHealth =
    lastHealth        <- health
    lastHealthPercent <- percentHealth
  
  match healthCtx with
  | PercentMoreHealth
  | PercentEqualHealth -> updateHealth healthCtx.CurrentHealth healthCtx.CurrentHealthPercent
  | PercentLessHealth  ->
    match healthCtx with
    | MoreHealth _ 
    | EqualHealth      -> updateHealth healthCtx.CurrentHealth healthCtx.CurrentHealthPercent
    | LessHealth delta ->
      let fullDamage    = delta / damageReduceMult
      let magickaDamage = fullDamage * multMagickaDamage
      sp.PrintConsole $"DEBUG TRACE: ReduceMult: {damageReduceMult} | MagickaMult {multMagickaDamage} | FullDamage {fullDamage} | Delta {delta}"
      sp.PrintConsole $"Damage magicka: PlayerMagicka - {magicka} | MagickaDamage - {magickaDamage}"
      match magickaDamage > %magicka with
      | true ->
        let damageHealth = magickaDamage - %magicka
        
        sp.PrintConsole $"Damage magicka delta: DamageHealth - {damageHealth}"
        
        player.DamageActorValue ActorValue.Health  damageHealth
        player.DamageActorValue ActorValue.Magicka %magicka
        
        (player.GetActorValue ActorValue.Health, player.GetActorValuePercentage ActorValue.Health)
        ||> updateHealth
      | false ->
        updateHealth healthCtx.CurrentHealth healthCtx.CurrentHealthPercent
        player.DamageActorValue ActorValue.Magicka magickaDamage