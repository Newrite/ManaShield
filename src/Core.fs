module Core

open FSharp.UMX

type [<Measure>] private value_of_actorvalue
type [<Measure>] private percent_of_actorvalue
type ValueOfActorValue   = float<value_of_actorvalue>
type PercentOfActorValue = float<percent_of_actorvalue>

type HealthContext =
  {
    LastHealth           : ValueOfActorValue
    CurrentHealth        : ValueOfActorValue
    LastHealthPercent    : PercentOfActorValue
    CurrentHealthPercent : PercentOfActorValue
  }