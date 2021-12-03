module BindingWrapper

open SkyrimPlatform
open Core
open FSharp.UMX

[<RequireQualifiedAccess>]
type ActorValue =
  | Health
  | Magicka
  with
  
  override self.ToString() =
    match self with
    | Health  -> "health"
    | Magicka -> "magicka"

type Actor with

  member inline self.GetActorValue             (actorValue: ActorValue): ValueOfActorValue   =
    %self.getActorValue(actorValue.ToString())
  member inline self.GetActorValuePercentage   (actorValue: ActorValue): PercentOfActorValue =
    %self.getActorValuePercentage(actorValue.ToString())
  member inline self.DamageActorValue          (actorValue: ActorValue) afDamage =
    self.damageActorValue(actorValue.ToString(), afDamage)
  
type DebugStatic with

  member inline self.Notification message = self.notification(message)
  member inline self.MessageBox   message = self.messageBox(message)
  
type IExports with

 member inline self.PrintConsole ([<System.ParamArray>] message) = self.printConsole(message)