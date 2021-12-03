module PatternNoWarn

open SkyrimPlatform
open BindingWrapper

#nowarn "25"

open Core
open FSharp.UMX
let inline (|LessHealth|MoreHealth|EqualHealth|) (ctxHealth: HealthContext) =
  
  let delta = System.Math.Abs(%ctxHealth.CurrentHealth - %ctxHealth.LastHealth)
      
  match ctxHealth.CurrentHealth with
  | _ when %ctxHealth.CurrentHealth > %ctxHealth.LastHealth -> MoreHealth delta
  | _ when %ctxHealth.CurrentHealth = %ctxHealth.LastHealth -> EqualHealth
  | _ when %ctxHealth.CurrentHealth < %ctxHealth.LastHealth -> LessHealth delta
    
let inline (|PercentLessHealth|PercentMoreHealth|PercentEqualHealth|) (ctxHealth: HealthContext) =

  let percentEquality =
    if System.Math.Abs(%ctxHealth.CurrentHealthPercent - %ctxHealth.LastHealthPercent) > 0.01 then
      false
    else
      true
        
  match percentEquality with
  | true -> PercentEqualHealth
  | _ when %ctxHealth.CurrentHealthPercent > %ctxHealth.LastHealthPercent -> PercentMoreHealth
  | _ when %ctxHealth.CurrentHealthPercent < %ctxHealth.LastHealthPercent -> PercentLessHealth
  
let inline (|IsManaShieldEffect|_|) (equal, objRef: ObjectReference) =
  if equal then
    match sp.Actor.From (objRef :> Form) with
    | Some actor -> Some actor
    | _ -> None
  else None
  
let inline (|MagicEffectFromForm|_|) (someForm: Form option) =
  match someForm with
  | Some form ->
    match sp.MagicEffect.From(form) with
    | Some me -> Some me
    | _ -> None
  | _ -> None
  
let inline (|PerkFromForm|_|) (someForm: Form option) =
  match someForm with
  | Some form ->
    match sp.Perk.From(form) with
    | Some p -> Some p
    | _ -> None
  | _ -> None