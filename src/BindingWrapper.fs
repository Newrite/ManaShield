module BindingWrapper

open System
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
  member inline self.DamageActorValue          (actorValue: ActorValue) afDamage             =
    self.damageActorValue(actorValue.ToString(), afDamage)
  member inline self.TryAddPerk    perk = self.addPerk(perk)
  member inline self.AddPerk       perk = self.addPerk(Some(perk))
  member inline self.TryRemovePerk perk = self.removePerk(perk)
  member inline self.RemovePerk    perk = self.removePerk(Some(perk))
  
  member inline self.HasMagicEffect    me = self.hasMagicEffect(Some(me))
  member inline self.TryHasMagicEffect me = self.hasMagicEffect(me)
  
type DebugStatic with

  member inline self.Notification message = self.notification(message)
  member inline self.MessageBox   message = self.messageBox(message)
  
type IExports with

 member inline self.PrintConsole ([<System.ParamArray>] message) = self.printConsole(message)
 
type GameStatic with
  
  member inline self.GetFormFromFile formId fileName = self.getFormFromFile(formId, fileName)
  
type Perk with

  member inline self.GetNthEntryValue entryIndex numberOfValue          =
    self.getNthEntryValue(entryIndex, numberOfValue)
  
  member inline self.SetNthEntryValue entryIndex numberOfValue newValue =
    self.setNthEntryValue(entryIndex, numberOfValue, newValue)
  
type MagicEffectStatic with

  member inline self.From (form: Form) = sp.MagicEffect.from(?+form)
  
type PerkStatic with

  member inline self.From (form: Form) = sp.Perk.from(?+form)
  
type ActorStatic with

  member inline self.From (form: Form) = sp.Actor.from(?+form)
  