module PatternNoWarn

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