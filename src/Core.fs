module Core

open System
open FSharp.UMX
open SkyrimPlatform
open Fable.Core

[<ImportAll("./skyrimPlatform.declare")>]
let sp: IExports = jsNative

let inline DebugTrace (message: string) =
  if true then
    sp.printConsole(message)

let inline (~+) (a: Form option) =
  match a with
  | Some a -> Some(a :> PapyrusObject)
  | None -> None
  
let inline (~?+) (a: Form) = Some(a :> PapyrusObject)

let inline (^) f x = f x


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
  
 [<CustomComparison; CustomEquality>]
type ShieldedActor = {
  Self                      : unit -> Actor
  FormID                    : float
  mutable LastHealth        : ValueOfActorValue
  mutable LastHealthPercent : PercentOfActorValue
}
with

  member self.HealthContext (selfActor: Actor) = {
    LastHealth           = self.LastHealth
    CurrentHealth        = %selfActor.getActorValue("health")
    LastHealthPercent    = self.LastHealthPercent
    CurrentHealthPercent = %selfActor.getActorValuePercentage("health")
  }
  
  member self.UpdateHealth lastHealth lastHealthPercent =
    self.LastHealth        <- lastHealth
    self.LastHealthPercent <- lastHealthPercent
    
  member self.UpdateHealthInternal (selfActor: Actor) =
    self.LastHealth        <- %selfActor.getActorValue("health")
    self.LastHealthPercent <- %selfActor.getActorValuePercentage("health")

  interface IComparable with
    member self.CompareTo(other) =
      match other with
      | :? ShieldedActor as sa ->
        if self.FormID > sa.FormID then 1
        elif self.FormID = sa.FormID then 0
        else -1
      | _ -> -1
      
  override self.Equals(other) =
    match other with
    | :? ShieldedActor as sa ->
      self.FormID = sa.FormID
    | _ -> false
    
  override self.GetHashCode() =
    let cp = self.FormID
    cp.GetHashCode()
    
  static member CreateFromActor (actor: Actor) =
    
    let formID = actor.getFormID()
    
    {
      Self              = fun () -> sp.Actor.from(+sp.Game.getFormEx(formID)).Value
      FormID            = actor.getFormID()
      LastHealth        = %actor.getActorValue("health")
      LastHealthPercent = %actor.getActorValuePercentage("health)")
    }
    
