// ts2fable 0.8.0
module rec SkyrimPlatform

#nowarn "953"

open System
open Fable.Core
open Fable.Core.JS


//let [<Import("storage","module")>] storage: Record<string, obj> = jsNative
//let [<Import("settings","module")>] settings: Record<string, Record<string, obj>> = jsNative
let [<Import("mpClientPlugin","module")>] mpClientPlugin: MpClientPlugin = jsNative
let [<Import("browser","module")>] browser: Browser = jsNative
let [<Import("getExtraContainerChanges","module")>] getExtraContainerChanges: (float -> ResizeArray<InventoryChangesEntry>) = jsNative
let [<Import("getContainer","module")>] getContainer: (float -> ResizeArray<InventoryEntry>) = jsNative
let [<Import("hooks","module")>] hooks: Hooks = jsNative

type [<AllowNullLiteral>] IExports =
    abstract PapyrusObject: PapyrusObjectStatic
    abstract printConsole: [<ParamArray>] arguments: obj[] -> unit
    abstract writeLogs: pluginName: string * [<ParamArray>] arguments: obj[] -> unit
    abstract setPrintConsolePrefixesEnabled: enabled: bool -> unit
    abstract writeScript: scriptName: string * src: string -> unit
    abstract callNative: className: string * functionName: string * ?self: PapyrusObject * [<ParamArray>] args: PapyrusValue[] -> PapyrusValue
    abstract getJsMemoryUsage: unit -> float
    abstract getPluginSourceCode: pluginName: string -> string
    abstract writePlugin: pluginName: string * newSources: string -> string
    abstract getPlatformVersion: unit -> string
    abstract disableCtrlPrtScnHotkey: unit -> unit
    abstract sendIpcMessage: targetSystemName: string * message: ArrayBuffer -> unit
    abstract encodeUtf8: text: string -> ArrayBuffer
    abstract decodeUtf8: buffer: ArrayBuffer -> string
    [<Emit "$0.on('update',$1)">] abstract on_update: callback: (unit -> unit) -> unit
    [<Emit "$0.once('update',$1)">] abstract once_update: callback: (unit -> unit) -> unit
    [<Emit "$0.on('tick',$1)">] abstract on_tick: callback: (unit -> unit) -> unit
    [<Emit "$0.once('tick',$1)">] abstract once_tick: callback: (unit -> unit) -> unit
    [<Emit "$0.on('ipcMessage',$1)">] abstract on_ipcMessage: callback: (IpcMessageEvent -> unit) -> unit
    [<Emit "$0.once('ipcMessage',$1)">] abstract once_ipcMessage: callback: (IpcMessageEvent -> unit) -> unit
    abstract loadGame: pos: ResizeArray<float> * angle: ResizeArray<float> * worldOrCell: float * ?changeFormNpc: ChangeFormNpc -> unit
    abstract worldPointToScreenPoint: [<ParamArray>] args: ResizeArray<float>[] -> ResizeArray<ResizeArray<float>>
    [<Emit "$0.on('activate',$1)">] abstract on_activate: callback: (ActivateEvent -> unit) -> unit
    [<Emit "$0.once('activate',$1)">] abstract once_activate: callback: (ActivateEvent -> unit) -> unit
    [<Emit "$0.on('waitStop',$1)">] abstract on_waitStop: callback: (WaitStopEvent -> unit) -> unit
    [<Emit "$0.once('waitStop',$1)">] abstract once_waitStop: callback: (WaitStopEvent -> unit) -> unit
    [<Emit "$0.on('objectLoaded',$1)">] abstract on_objectLoaded: callback: (ObjectLoadedEvent -> unit) -> unit
    [<Emit "$0.once('objectLoaded',$1)">] abstract once_objectLoaded: callback: (ObjectLoadedEvent -> unit) -> unit
    [<Emit "$0.on('moveAttachDetach',$1)">] abstract on_moveAttachDetach: callback: (MoveAttachDetachEvent -> unit) -> unit
    [<Emit "$0.once('moveAttachDetach',$1)">] abstract once_moveAttachDetach: callback: (MoveAttachDetachEvent -> unit) -> unit
    [<Emit "$0.on('lockChanged',$1)">] abstract on_lockChanged: callback: (LockChangedEvent -> unit) -> unit
    [<Emit "$0.once('lockChanged',$1)">] abstract once_lockChanged: callback: (LockChangedEvent -> unit) -> unit
    [<Emit "$0.on('grabRelease',$1)">] abstract on_grabRelease: callback: (GrabReleaseEvent -> unit) -> unit
    [<Emit "$0.once('grabRelease',$1)">] abstract once_grabRelease: callback: (GrabReleaseEvent -> unit) -> unit
    [<Emit "$0.on('cellFullyLoaded',$1)">] abstract on_cellFullyLoaded: callback: (CellFullyLoadedEvent -> unit) -> unit
    [<Emit "$0.once('cellFullyLoaded',$1)">] abstract once_cellFullyLoaded: callback: (CellFullyLoadedEvent -> unit) -> unit
    [<Emit "$0.on('switchRaceComplete',$1)">] abstract on_switchRaceComplete: callback: (SwitchRaceCompleteEvent -> unit) -> unit
    [<Emit "$0.once('switchRaceComplete',$1)">] abstract once_switchRaceComplete: callback: (SwitchRaceCompleteEvent -> unit) -> unit
    [<Emit "$0.on('uniqueIdChange',$1)">] abstract on_uniqueIdChange: callback: (UniqueIDChangeEvent -> unit) -> unit
    [<Emit "$0.once('uniqueIdChange',$1)">] abstract once_uniqueIdChange: callback: (UniqueIDChangeEvent -> unit) -> unit
    [<Emit "$0.on('trackedStats',$1)">] abstract on_trackedStats: callback: (TrackedStatsEvent -> unit) -> unit
    [<Emit "$0.once('trackedStats',$1)">] abstract once_trackedStats: callback: (TrackedStatsEvent -> unit) -> unit
    [<Emit "$0.on('scriptInit',$1)">] abstract on_scriptInit: callback: (InitScriptEvent -> unit) -> unit
    [<Emit "$0.once('scriptInit',$1)">] abstract once_scriptInit: callback: (InitScriptEvent -> unit) -> unit
    [<Emit "$0.on('reset',$1)">] abstract on_reset: callback: (ResetEvent -> unit) -> unit
    [<Emit "$0.once('reset',$1)">] abstract once_reset: callback: (ResetEvent -> unit) -> unit
    [<Emit "$0.on('combatState',$1)">] abstract on_combatState: callback: (CombatEvent -> unit) -> unit
    [<Emit "$0.once('combatState',$1)">] abstract once_combatState: callback: (CombatEvent -> unit) -> unit
    [<Emit "$0.on('loadGame',$1)">] abstract on_loadGame: callback: (unit -> unit) -> unit
    [<Emit "$0.once('loadGame',$1)">] abstract once_loadGame: callback: (unit -> unit) -> unit
    [<Emit "$0.on('deathEnd',$1)">] abstract on_deathEnd: callback: (DeathEvent -> unit) -> unit
    [<Emit "$0.once('deathEnd',$1)">] abstract once_deathEnd: callback: (DeathEvent -> unit) -> unit
    [<Emit "$0.on('deathStart',$1)">] abstract on_deathStart: callback: (DeathEvent -> unit) -> unit
    [<Emit "$0.once('deathStart',$1)">] abstract once_deathStart: callback: (DeathEvent -> unit) -> unit
    [<Emit "$0.on('containerChanged',$1)">] abstract on_containerChanged: callback: (ContainerChangedEvent -> unit) -> unit
    [<Emit "$0.once('containerChanged',$1)">] abstract once_containerChanged: callback: (ContainerChangedEvent -> unit) -> unit
    [<Emit "$0.on('hit',$1)">] abstract on_hit: callback: (HitEvent -> unit) -> unit
    [<Emit "$0.once('hit',$1)">] abstract once_hit: callback: (HitEvent -> unit) -> unit
    [<Emit "$0.on('unequip',$1)">] abstract on_unequip: callback: (EquipEvent -> unit) -> unit
    [<Emit "$0.once('unequip',$1)">] abstract once_unequip: callback: (EquipEvent -> unit) -> unit
    [<Emit "$0.on('equip',$1)">] abstract on_equip: callback: (EquipEvent -> unit) -> unit
    [<Emit "$0.once('equip',$1)">] abstract once_equip: callback: (EquipEvent -> unit) -> unit
    [<Emit "$0.on('magicEffectApply',$1)">] abstract on_magicEffectApply: callback: (MagicEffectApplyEvent -> unit) -> unit
    [<Emit "$0.once('magicEffectApply',$1)">] abstract once_magicEffectApply: callback: (MagicEffectApplyEvent -> unit) -> unit
    [<Emit "$0.on('effectFinish',$1)">] abstract on_effectFinish: callback: (ActiveEffectApplyRemoveEvent -> unit) -> unit
    [<Emit "$0.once('effectFinish',$1)">] abstract once_effectFinish: callback: (ActiveEffectApplyRemoveEvent -> unit) -> unit
    [<Emit "$0.on('effectStart',$1)">] abstract on_effectStart: callback: (ActiveEffectApplyRemoveEvent -> unit) -> unit
    [<Emit "$0.once('effectStart',$1)">] abstract once_effectStart: callback: (ActiveEffectApplyRemoveEvent -> unit) -> unit
    [<Emit "$0.on('menuOpen',$1)">] abstract on_menuOpen: callback: (MenuOpenEvent -> unit) -> unit
    [<Emit "$0.once('menuOpen',$1)">] abstract once_menuOpen: callback: (MenuOpenEvent -> unit) -> unit
    [<Emit "$0.on('menuClose',$1)">] abstract on_menuClose: callback: (MenuCloseEvent -> unit) -> unit
    [<Emit "$0.once('menuClose',$1)">] abstract once_menuClose: callback: (MenuCloseEvent -> unit) -> unit
    [<Emit "$0.on('browserMessage',$1)">] abstract on_browserMessage: callback: (BrowserMessageEvent -> unit) -> unit
    [<Emit "$0.once('browserMessage',$1)">] abstract once_browserMessage: callback: (BrowserMessageEvent -> unit) -> unit
    [<Emit "$0.on('consoleMessage',$1)">] abstract on_consoleMessage: callback: (ConsoleMessageEvent -> unit) -> unit
    [<Emit "$0.once('consoleMessage',$1)">] abstract once_consoleMessage: callback: (ConsoleMessageEvent -> unit) -> unit
    abstract ConsoleComand: ConsoleComandStatic
    abstract findConsoleCommand: cmdName: string -> ConsoleComand
    abstract Hooks: HooksStatic
    abstract HttpResponse: HttpResponseStatic
    //abstract HttpClient: HttpClientStatic
    abstract Form: FormStatic
    abstract Action: ActionStatic
    abstract Activator: ActivatorStatic
    abstract ActiveMagicEffect: ActiveMagicEffectStatic
    abstract ObjectReference: ObjectReferenceStatic
    abstract Actor: ActorStatic
    abstract ActorBase: ActorBaseStatic
    abstract ActorValueInfo: ActorValueInfoStatic
    abstract Alias: AliasStatic
    abstract Ammo: AmmoStatic
    abstract MiscObject: MiscObjectStatic
    abstract Apparatus: ApparatusStatic
    abstract Armor: ArmorStatic
    abstract ArmorAddon: ArmorAddonStatic
    abstract Art: ArtStatic
    abstract AssociationType: AssociationTypeStatic
    abstract Book: BookStatic
    abstract Cell: CellStatic
    abstract Class: ClassStatic
    abstract ColorForm: ColorFormStatic
    abstract CombatStyle: CombatStyleStatic
    abstract ConstructibleObject: ConstructibleObjectStatic
    abstract Container: ContainerStatic
    abstract Debug: DebugStatic
    abstract DefaultObjectManager: DefaultObjectManagerStatic
    abstract Door: DoorStatic
    abstract EffectShader: EffectShaderStatic
    abstract Enchantment: EnchantmentStatic
    abstract EncounterZone: EncounterZoneStatic
    abstract EquipSlot: EquipSlotStatic
    abstract Explosion: ExplosionStatic
    abstract Faction: FactionStatic
    abstract Flora: FloraStatic
    abstract FormList: FormListStatic
    abstract Furniture: FurnitureStatic
    abstract Game: GameStatic
    abstract GlobalVariable: GlobalVariableStatic
    abstract Hazard: HazardStatic
    abstract HeadPart: HeadPartStatic
    abstract Idle: IdleStatic
    abstract ImageSpaceModifier: ImageSpaceModifierStatic
    abstract ImpactDataSet: ImpactDataSetStatic
    abstract Ingredient: IngredientStatic
    abstract Input: InputStatic
    abstract Key: KeyStatic
    abstract Keyword: KeywordStatic
    abstract LeveledActor: LeveledActorStatic
    abstract LeveledItem: LeveledItemStatic
    abstract LeveledSpell: LeveledSpellStatic
    abstract Light: LightStatic
    abstract Location: LocationStatic
    abstract LocationAlias: LocationAliasStatic
    abstract LocationRefType: LocationRefTypeStatic
    abstract MagicEffect: MagicEffectStatic
    abstract Message: MessageStatic
    abstract MusicType: MusicTypeStatic
    abstract NetImmerse: NetImmerseStatic
    abstract Outfit: OutfitStatic
    abstract Projectile: ProjectileStatic
    abstract Package: PackageStatic
    abstract Perk: PerkStatic
    abstract Potion: PotionStatic
    abstract Quest: QuestStatic
    abstract Race: RaceStatic
    abstract ReferenceAlias: ReferenceAliasStatic
    abstract Spell: SpellStatic
    abstract Scene: SceneStatic
    abstract Scroll: ScrollStatic
    abstract ShaderParticleGeometry: ShaderParticleGeometryStatic
    abstract Shout: ShoutStatic
    abstract SoulGem: SoulGemStatic
    abstract Sound: SoundStatic
    abstract SoundCategory: SoundCategoryStatic
    abstract SoundDescriptor: SoundDescriptorStatic
    abstract TESModPlatform: TESModPlatformStatic
    abstract TalkingActivator: TalkingActivatorStatic
    abstract TextureSet: TextureSetStatic
    abstract Topic: TopicStatic
    abstract TopicInfo: TopicInfoStatic
    abstract TreeObject: TreeObjectStatic
    abstract Ui: UiStatic
    abstract VisualEffect: VisualEffectStatic
    abstract VoiceType: VoiceTypeStatic
    abstract Weapon: WeaponStatic
    abstract Weather: WeatherStatic
    abstract WordOfPower: WordOfPowerStatic
    abstract WorldSpace: WorldSpaceStatic
    abstract Utility: UtilityStatic

type [<AllowNullLiteral>] PapyrusObject =
    interface end

type [<AllowNullLiteral>] PapyrusObjectStatic =
    [<EmitConstructor>] abstract Create: unit -> PapyrusObject
    abstract from: papyrusObject: PapyrusObject option -> PapyrusObject option

[<NoComparison>]
type PapyrusValue =
    | PapyrusObject of PapyrusObject
    | Float of float
    | String of string
    | Bool of bool
    | PapyrusValue of ResizeArray<PapyrusValue>

type [<AllowNullLiteral>] IpcMessageEvent =
    abstract sourceSystemName: string with get, set
    abstract message: ArrayBuffer with get, set

type [<AllowNullLiteral>] Face =
    abstract hairColor: float with get, set
    abstract bodySkinColor: float with get, set
    abstract headTextureSetId: float with get, set
    abstract headPartIds: ResizeArray<float> with get, set
    abstract presets: ResizeArray<float> with get, set

type [<AllowNullLiteral>] ChangeFormNpc =
    abstract raceId: float option with get, set
    abstract name: string option with get, set
    abstract face: Face option with get, set

type [<StringEnum>] [<RequireQualifiedAccess>] PacketType =
    | Message
    | Disconnect
    | ConnectionAccepted
    | ConnectionFailed
    | ConnectionDenied

type [<AllowNullLiteral>] MpClientPlugin =
    abstract getVersion: unit -> string
    abstract createClient: host: string * port: float -> unit
    abstract destroyClient: unit -> unit
    abstract isConnected: unit -> bool
    abstract tick: tickHandler: (PacketType -> string -> string -> unit) -> unit
    abstract send: jsonContent: string * reliable: bool -> unit

type [<AllowNullLiteral>] Browser =
    abstract setVisible: visible: bool -> unit
    abstract setFocused: focused: bool -> unit
    abstract loadUrl: url: string -> unit
    abstract getToken: unit -> string
    abstract executeJavaScript: src: string -> unit

type [<AllowNullLiteral>] ExtraData =
    abstract ``type``: ExtraDataType with get, set

type [<AllowNullLiteral>] ExtraHealth =
    inherit ExtraData
    abstract ``type``: string with get, set
    abstract health: float with get, set

type [<AllowNullLiteral>] ExtraCount =
    inherit ExtraData
    abstract ``type``: string with get, set
    abstract count: float with get, set

type [<AllowNullLiteral>] ExtraEnchantment =
    inherit ExtraData
    abstract ``type``: string with get, set
    abstract enchantmentId: float with get, set
    abstract maxCharge: float with get, set
    abstract removeOnUnequip: bool with get, set

type [<AllowNullLiteral>] ExtraCharge =
    inherit ExtraData
    abstract ``type``: string with get, set
    abstract charge: float with get, set

type [<AllowNullLiteral>] ExtraTextDisplayData =
    inherit ExtraData
    abstract ``type``: string with get, set
    abstract name: string with get, set

type [<AllowNullLiteral>] ExtraSoul =
    inherit ExtraData
    abstract ``type``: string with get, set
    abstract soul: ExtraSoulSoul with get, set

type [<AllowNullLiteral>] ExtraPoison =
    inherit ExtraData
    abstract ``type``: string with get, set
    abstract poisonId: float with get, set
    abstract count: float with get, set

type [<AllowNullLiteral>] ExtraWorn =
    inherit ExtraData
    abstract ``type``: string with get, set

type [<AllowNullLiteral>] ExtraWornLeft =
    inherit ExtraData
    abstract ``type``: string with get, set

type BaseExtraList =
    ResizeArray<ExtraData>

type [<AllowNullLiteral>] InventoryChangesEntry =
    abstract countDelta: float with get, set
    abstract baseId: float with get, set
    abstract extendDataList: ResizeArray<BaseExtraList> with get, set

type [<AllowNullLiteral>] InventoryEntry =
    abstract count: float with get, set
    abstract baseId: float with get, set

type [<AllowNullLiteral>] ActivateEvent =
    abstract target: ObjectReference with get, set
    abstract caster: ObjectReference with get, set
    abstract isCrimeToActivate: bool with get, set

type [<AllowNullLiteral>] MoveAttachDetachEvent =
    abstract movedRef: ObjectReference with get, set
    abstract isCellAttached: bool with get, set

type [<AllowNullLiteral>] WaitStopEvent =
    abstract isInterrupted: bool with get, set

type [<AllowNullLiteral>] ObjectLoadedEvent =
    abstract object: Form with get, set
    abstract isLoaded: bool with get, set

type [<AllowNullLiteral>] LockChangedEvent =
    abstract lockedObject: ObjectReference with get, set

type [<AllowNullLiteral>] CellFullyLoadedEvent =
    abstract cell: Cell with get, set

type [<AllowNullLiteral>] GrabReleaseEvent =
    abstract refr: ObjectReference with get, set
    abstract isGrabbed: bool with get, set

type [<AllowNullLiteral>] SwitchRaceCompleteEvent =
    abstract subject: ObjectReference with get, set

type [<AllowNullLiteral>] UniqueIDChangeEvent =
    abstract oldBaseID: float with get, set
    abstract newBaseID: float with get, set
    abstract oldUniqueID: float with get, set
    abstract newUniqueID: float with get, set

type [<AllowNullLiteral>] TrackedStatsEvent =
    abstract statName: string with get, set
    abstract newValue: float with get, set

type [<AllowNullLiteral>] InitScriptEvent =
    abstract initializedObject: ObjectReference with get, set

type [<AllowNullLiteral>] ResetEvent =
    abstract object: ObjectReference with get, set

type [<AllowNullLiteral>] CombatEvent =
    abstract target: ObjectReference with get, set
    abstract actor: ObjectReference with get, set
    abstract isCombat: bool with get, set
    abstract isSearching: bool with get, set

type [<AllowNullLiteral>] DeathEvent =
    abstract actorDying: ObjectReference with get, set
    abstract actorKiller: ObjectReference with get, set

type [<AllowNullLiteral>] ContainerChangedEvent =
    abstract oldContainer: ObjectReference with get, set
    abstract newContainer: ObjectReference with get, set
    abstract baseObj: Form with get, set
    abstract numItems: float with get, set
    abstract uniqueID: float with get, set
    abstract reference: ObjectReference with get, set

type [<AllowNullLiteral>] HitEvent =
    abstract target: ObjectReference with get, set
    abstract aggressor: ObjectReference with get, set
    abstract source: Form with get, set
    abstract projectile: Projectile with get, set
    abstract isPowerAttack: bool with get, set
    abstract isSneakAttack: bool with get, set
    abstract isBashAttack: bool with get, set
    abstract isHitBlocked: bool with get, set

type [<AllowNullLiteral>] EquipEvent =
    abstract actor: ObjectReference with get, set
    abstract baseObj: Form with get, set
    abstract uniqueId: float with get, set
    abstract originalRefr: ObjectReference with get, set

type [<AllowNullLiteral>] ActiveEffectApplyRemoveEvent =
    abstract activeEffect: ActiveMagicEffect with get, set
    abstract effect: MagicEffect with get, set
    abstract caster: ObjectReference with get, set
    abstract target: ObjectReference with get, set

type [<AllowNullLiteral>] MenuOpenEvent =
    abstract name: string with get, set

type [<AllowNullLiteral>] MenuCloseEvent =
    abstract name: string with get, set

type [<AllowNullLiteral>] MagicEffectApplyEvent =
    abstract activeEffect: ActiveMagicEffect with get, set
    abstract effect: MagicEffect with get, set
    abstract caster: ObjectReference with get, set
    abstract target: ObjectReference with get, set

type [<AllowNullLiteral>] BrowserMessageEvent =
    abstract arguments: ResizeArray<obj> with get, set

type [<AllowNullLiteral>] ConsoleMessageEvent =
    abstract message: string with get, set

type [<AllowNullLiteral>] ConsoleComand =
    abstract longName: string with get, set
    abstract shortName: string with get, set
    abstract numArgs: float with get, set
    abstract execute: (ResizeArray<obj> -> bool) with get, set

type [<AllowNullLiteral>] ConsoleComandStatic =
    [<EmitConstructor>] abstract Create: unit -> ConsoleComand

type [<RequireQualifiedAccess>] MotionType =
    | Dynamic = 1
    | SphereInertia = 2
    | BoxInertia = 3
    | Keyframed = 4
    | Fixed = 5
    | ThinBoxInertia = 6
    | Character = 7

type [<StringEnum>] [<RequireQualifiedAccess>] Menu =
    | [<CompiledName "BarterMenu">] Barter
    | [<CompiledName "Book Menu">] Book
    | [<CompiledName "Console">] Console
    | [<CompiledName "Console Native UI Menu">] ConsoleNativeUI
    | [<CompiledName "ContainerMenu">] Container
    | [<CompiledName "Crafting Menu">] Crafting
    | [<CompiledName "Credits Menu">] Credits
    | [<CompiledName "Cursor Menu">] Cursor
    | [<CompiledName "Debug Text Menu">] Debug
    | [<CompiledName "Dialogue Menu">] Dialogue
    | [<CompiledName "Fader Menu">] Fader
    | [<CompiledName "FavoritesMenu">] Favorites
    | [<CompiledName "GiftMenu">] Gift
    | [<CompiledName "HUD Menu">] HUD
    | [<CompiledName "InventoryMenu">] Inventory
    | [<CompiledName "Journal Menu">] Journal
    | [<CompiledName "Kinect Menu">] Kinect
    | [<CompiledName "LevelUp Menu">] LevelUp
    | [<CompiledName "Loading Menu">] Loading
    | [<CompiledName "Main Menu">] Main
    | [<CompiledName "Lockpicking Menu">] Lockpicking
    | [<CompiledName "MagicMenu">] Magic
    | [<CompiledName "MapMenu">] Map
    | [<CompiledName "MessageBoxMenu">] MessageBox
    | [<CompiledName "Mist Menu">] Mist
    | [<CompiledName "Overlay Interaction Menu">] OverlayInteraction
    | [<CompiledName "Overlay Menu">] Overlay
    | [<CompiledName "Quantity Menu">] Quantity
    | [<CompiledName "RaceSex Menu">] RaceSex
    | [<CompiledName "Sleep/Wait Menu">] Sleep
    | [<CompiledName "StatsMenu">] Stats
    | [<CompiledName "TitleSequence Menu">] TitleSequence
    | [<CompiledName "Top Menu">] Top
    | [<CompiledName "Training Menu">] Training
    | [<CompiledName "Tutorial Menu">] Tutorial
    | [<CompiledName "TweenMenu">] Tween

type DxScanCode =
    obj

type [<RequireQualifiedAccess>] FormType =
    | ANIO = 83
    | ARMA = 102
    | AcousticSpace = 16
    | Action = 6
    | Activator = 24
    | ActorValueInfo = 95
    | AddonNode = 94
    | Ammo = 42
    | Apparatus = 33
    | Armor = 26
    | ArrowProjectile = 64
    | Art = 125
    | AssociationType = 123
    | BarrierProjectile = 69
    | BeamProjectile = 66
    | BodyPartData = 93
    | Book = 27
    | CameraPath = 97
    | CameraShot = 96
    | Cell = 60
    | Character = 62
    | Class = 10
    | Climate = 55
    | CollisionLayer = 132
    | ColorForm = 133
    | CombatStyle = 80
    | ConeProjectile = 68
    | ConstructibleObject = 49
    | Container = 28
    | DLVW = 117
    | Debris = 88
    | DefaultObject = 107
    | DialogueBranch = 115
    | Door = 29
    | DualCastData = 129
    | EffectSetting = 18
    | EffectShader = 85
    | Enchantment = 21
    | EncounterZone = 103
    | EquipSlot = 120
    | Explosion = 87
    | Eyes = 13
    | Faction = 11
    | FlameProjectile = 67
    | Flora = 39
    | Footstep = 110
    | FootstepSet = 111
    | Furniture = 40
    | GMST = 3
    | Global = 9
    | Grass = 37
    | GrenadeProjectile = 65
    | Group = 2
    | Hazard = 51
    | HeadPart = 12
    | Idle = 78
    | IdleMarker = 47
    | ImageSpace = 89
    | ImageSpaceModifier = 90
    | ImpactData = 100
    | ImpactDataSet = 101
    | Ingredient = 30
    | Key = 45
    | Keyword = 4
    | Land = 72
    | LandTexture = 20
    | LeveledCharacter = 44
    | LeveledItem = 53
    | LeveledSpell = 82
    | Light = 31
    | LightingTemplate = 108
    | List = 91
    | LoadScreen = 81
    | Location = 104
    | LocationRef = 5
    | Material = 126
    | MaterialType = 99
    | MenuIcon = 8
    | Message = 105
    | Misc = 32
    | MissileProjectile = 63
    | MovableStatic = 36
    | MovementType = 127
    | MusicTrack = 116
    | MusicType = 109
    | NAVI = 59
    | NPC = 43
    | NavMesh = 73
    | None = 0
    | Note = 48
    | Outfit = 124
    | PHZD = 70
    | Package = 79
    | Perk = 92
    | Potion = 46
    | Projectile = 50
    | Quest = 77
    | Race = 14
    | Ragdoll = 106
    | Reference = 61
    | ReferenceEffect = 57
    | Region = 58
    | Relationship = 121
    | ReverbParam = 134
    | Scene = 122
    | Script = 19
    | ScrollItem = 23
    | ShaderParticleGeometryData = 56
    | Shout = 119
    | Skill = 17
    | SoulGem = 52
    | Sound = 15
    | SoundCategory = 130
    | SoundDescriptor = 128
    | SoundOutput = 131
    | Spell = 22
    | Static = 34
    | StaticCollection = 35
    | StoryBranchNode = 112
    | StoryEventNode = 114
    | StoryQuestNode = 113
    | TES4 = 1
    | TLOD = 74
    | TOFT = 86
    | TalkingActivator = 25
    | TextureSet = 7
    | Topic = 75
    | TopicInfo = 76
    | Tree = 38
    | VoiceType = 98
    | Water = 84
    | Weapon = 41
    | Weather = 54
    | WordOfPower = 118
    | WorldSpace = 71

type WeaponType =
    obj

type EquippedItemType =
    obj

type [<RequireQualifiedAccess>] SlotMask =
    | Head = 1L
    | Hair = 2L
    | Body = 4L
    | Hands = 8L
    | Forearms = 16L
    | Amulet = 32L
    | Ring = 64L
    | Feet = 128L
    | Calves = 256L
    | Shield = 512L
    | Tail = 1024L
    | LongHair = 2048L
    | Circlet = 4096L
    | Ears = 8192L
    | Face = 16384L
    | Mouth = 16384L
    | Neck = 32768L
    | ChestOuter = 65536L
    | ChestPrimary = 65536L
    | Back = 131072L
    | Misc01 = 262144L
    | PelvisOuter = 524288L
    | PelvisPrimary = 524288L
    | DecapitateHead = 1048576L
    | Decapitate = 2097152L
    | PelvisUnder = 4194304L
    | PelvisSecondary = 4194304L
    | LegOuter = 8388608L
    | LegPrimary = 8388608L
    | LegUnder = 16777216L
    | LegSecondary = 16777216L
    | FaceAlt = 33554432L
    | Jewelry = 33554432L
    | ChestUnder = 67108864L
    | ChestSecondary = 67108864L
    | Shoulder = 134217728L
    | ArmUnder = 268435456L
    | ArmSecondary = 268435456L
    | ArmLeft = 268435456L
    | ArmOuter = 536870912L
    | ArmPrimary = 536870912L
    | ArmRight = 536870912L
    | Misc02 = 1073741824L
    | FX01 = 2147483648L

module SendAnimationEventHook =

    type [<AllowNullLiteral>] IExports =
        abstract Context: ContextStatic
        abstract LeaveContext: LeaveContextStatic
        abstract Handler: HandlerStatic
        abstract Target: TargetStatic

    type [<AllowNullLiteral>] Context =
        abstract selfId: float
        abstract animEventName: string with get, set
        abstract storage: Map<string, obj>

    type [<AllowNullLiteral>] ContextStatic =
        [<EmitConstructor>] abstract Create: unit -> Context

    type [<AllowNullLiteral>] LeaveContext =
        inherit Context
        abstract animationSucceeded: bool

    type [<AllowNullLiteral>] LeaveContextStatic =
        [<EmitConstructor>] abstract Create: unit -> LeaveContext

    type [<AllowNullLiteral>] Handler =
        abstract enter: ctx: Context -> unit
        abstract leave: ctx: LeaveContext -> unit

    type [<AllowNullLiteral>] HandlerStatic =
        [<EmitConstructor>] abstract Create: unit -> Handler

    type [<AllowNullLiteral>] Target =
        abstract add: handler: Handler * ?minSelfId: float * ?maxSelfId: float * ?eventPattern: string -> float
        abstract remove: id: float -> unit

    type [<AllowNullLiteral>] TargetStatic =
        [<EmitConstructor>] abstract Create: unit -> Target

module SendPapyrusEventHook =

    type [<AllowNullLiteral>] IExports =
        abstract Context: ContextStatic
        abstract Handler: HandlerStatic
        abstract Target: TargetStatic

    type [<AllowNullLiteral>] Context =
        abstract selfId: float
        abstract papyrusEventName: string
        abstract storage: Map<string, obj>

    type [<AllowNullLiteral>] ContextStatic =
        [<EmitConstructor>] abstract Create: unit -> Context

    type [<AllowNullLiteral>] Handler =
        abstract enter: ctx: Context -> unit

    type [<AllowNullLiteral>] HandlerStatic =
        [<EmitConstructor>] abstract Create: unit -> Handler

    type [<AllowNullLiteral>] Target =
        abstract add: handler: Handler * ?minSelfId: float * ?maxSelfId: float * ?eventPattern: string -> float
        abstract remove: id: float -> unit

    type [<AllowNullLiteral>] TargetStatic =
        [<EmitConstructor>] abstract Create: unit -> Target

type [<AllowNullLiteral>] Hooks =
    abstract sendAnimationEvent: SendAnimationEventHook.Target with get, set
    abstract sendPapyrusEvent: SendPapyrusEventHook.Target with get, set

type [<AllowNullLiteral>] HooksStatic =
    [<EmitConstructor>] abstract Create: unit -> Hooks

type [<AllowNullLiteral>] HttpResponse =
    abstract body: string with get, set
    abstract status: float with get, set

type [<AllowNullLiteral>] HttpResponseStatic =
    [<EmitConstructor>] abstract Create: unit -> HttpResponse

//type HttpHeaders =
//    Record<string, string>
//
//type [<AllowNullLiteral>] HttpClient =
//    abstract get: path: string * ?options: {| headers: HttpHeaders option |} -> Promise<HttpResponse>
//    abstract post: path: string * options: {| body: string; contentType: string; headers: HttpHeaders option |} -> Promise<HttpResponse>

//type [<AllowNullLiteral>] HttpClientStatic =
//    [<EmitConstructor>] abstract Create: url: string -> HttpClient

type [<AllowNullLiteral>] Form =
    inherit PapyrusObject
    abstract getFormID: unit -> float
    abstract getGoldValue: unit -> float
    abstract getKeywords: unit -> ResizeArray<PapyrusObject> option
    abstract getName: unit -> string
    abstract getNthKeyword: index: float -> Keyword option
    abstract getNumKeywords: unit -> float
    abstract getType: unit -> float
    abstract getWeight: unit -> float
    abstract getWorldModelNthTextureSet: n: float -> TextureSet option
    abstract getWorldModelNumTextureSets: unit -> float
    abstract getWorldModelPath: unit -> string
    abstract hasKeyword: akKeyword: Keyword option -> bool
    abstract hasWorldModel: unit -> bool
    abstract isPlayable: unit -> bool
    abstract playerKnows: unit -> bool
    abstract registerForActorAction: actionType: float -> unit
    abstract registerForAnimationEvent: akSender: ObjectReference option * asEventName: string -> bool
    abstract registerForCameraState: unit -> unit
    abstract registerForControl: control: string -> unit
    abstract registerForCrosshairRef: unit -> unit
    abstract registerForKey: keyCode: float -> unit
    abstract registerForLOS: akViewer: Actor option * akTarget: ObjectReference option -> unit
    abstract registerForMenu: menuName: string -> unit
    abstract registerForModEvent: eventName: string * callbackName: string -> unit
    abstract registerForNiNodeUpdate: unit -> unit
    abstract registerForSingleLOSGain: akViewer: Actor option * akTarget: ObjectReference option -> unit
    abstract registerForSingleLOSLost: akViewer: Actor option * akTarget: ObjectReference option -> unit
    abstract registerForSingleUpdate: afInterval: float -> unit
    abstract registerForSingleUpdateGameTime: afInterval: float -> unit
    abstract registerForSleep: unit -> unit
    abstract registerForTrackedStatsEvent: unit -> unit
    abstract registerForUpdate: afInterval: float -> unit
    abstract registerForUpdateGameTime: afInterval: float -> unit
    abstract sendModEvent: eventName: string * strArg: string * numArg: float -> unit
    abstract setGoldValue: value: float -> unit
    abstract setName: name: string -> unit
    abstract setPlayerKnows: knows: bool -> unit
    abstract setWeight: weight: float -> unit
    abstract setWorldModelNthTextureSet: nSet: TextureSet option * n: float -> unit
    abstract setWorldModelPath: path: string -> unit
    abstract startObjectProfiling: unit -> unit
    abstract stopObjectProfiling: unit -> unit
    abstract tempClone: unit -> Form option
    abstract unregisterForActorAction: actionType: float -> unit
    abstract unregisterForAllControls: unit -> unit
    abstract unregisterForAllKeys: unit -> unit
    abstract unregisterForAllMenus: unit -> unit
    abstract unregisterForAllModEvents: unit -> unit
    abstract unregisterForAnimationEvent: akSender: ObjectReference option * asEventName: string -> unit
    abstract unregisterForCameraState: unit -> unit
    abstract unregisterForControl: control: string -> unit
    abstract unregisterForCrosshairRef: unit -> unit
    abstract unregisterForKey: keyCode: float -> unit
    abstract unregisterForLOS: akViewer: Actor option * akTarget: ObjectReference option -> unit
    abstract unregisterForMenu: menuName: string -> unit
    abstract unregisterForModEvent: eventName: string -> unit
    abstract unregisterForNiNodeUpdate: unit -> unit
    abstract unregisterForSleep: unit -> unit
    abstract unregisterForTrackedStatsEvent: unit -> unit
    abstract unregisterForUpdate: unit -> unit
    abstract unregisterForUpdateGameTime: unit -> unit

type [<AllowNullLiteral>] FormStatic =
    [<EmitConstructor>] abstract Create: unit -> Form
    abstract from: papyrusObject: PapyrusObject option -> Form option

type [<AllowNullLiteral>] Action =
    inherit Form

type [<AllowNullLiteral>] ActionStatic =
    [<EmitConstructor>] abstract Create: unit -> Action
    abstract from: papyrusObject: PapyrusObject option -> Action option

type [<AllowNullLiteral>] Activator =
    inherit Form

type [<AllowNullLiteral>] ActivatorStatic =
    [<EmitConstructor>] abstract Create: unit -> Activator
    abstract from: papyrusObject: PapyrusObject option -> Activator option

type [<AllowNullLiteral>] ActiveMagicEffect =
    inherit PapyrusObject
    abstract addInventoryEventFilter: akFilter: Form option -> unit
    abstract dispel: unit -> unit
    abstract getBaseObject: unit -> MagicEffect option
    abstract getCasterActor: unit -> Actor option
    abstract getDuration: unit -> float
    abstract getMagnitude: unit -> float
    abstract getTargetActor: unit -> Actor option
    abstract getTimeElapsed: unit -> float
    abstract registerForActorAction: actionType: float -> unit
    abstract registerForAnimationEvent: akSender: ObjectReference option * asEventName: string -> bool
    abstract registerForCameraState: unit -> unit
    abstract registerForControl: control: string -> unit
    abstract registerForCrosshairRef: unit -> unit
    abstract registerForKey: keyCode: float -> unit
    abstract registerForLOS: akViewer: Actor option * akTarget: ObjectReference option -> unit
    abstract registerForMenu: menuName: string -> unit
    abstract registerForModEvent: eventName: string * callbackName: string -> unit
    abstract registerForNiNodeUpdate: unit -> unit
    abstract registerForSingleLOSGain: akViewer: Actor option * akTarget: ObjectReference option -> unit
    abstract registerForSingleLOSLost: akViewer: Actor option * akTarget: ObjectReference option -> unit
    abstract registerForSingleUpdate: afInterval: float -> unit
    abstract registerForSingleUpdateGameTime: afInterval: float -> unit
    abstract registerForSleep: unit -> unit
    abstract registerForTrackedStatsEvent: unit -> unit
    abstract registerForUpdate: afInterval: float -> unit
    abstract registerForUpdateGameTime: afInterval: float -> unit
    abstract removeAllInventoryEventFilters: unit -> unit
    abstract removeInventoryEventFilter: akFilter: Form option -> unit
    abstract sendModEvent: eventName: string * strArg: string * numArg: float -> unit
    abstract startObjectProfiling: unit -> unit
    abstract stopObjectProfiling: unit -> unit
    abstract unregisterForActorAction: actionType: float -> unit
    abstract unregisterForAllControls: unit -> unit
    abstract unregisterForAllKeys: unit -> unit
    abstract unregisterForAllMenus: unit -> unit
    abstract unregisterForAllModEvents: unit -> unit
    abstract unregisterForAnimationEvent: akSender: ObjectReference option * asEventName: string -> unit
    abstract unregisterForCameraState: unit -> unit
    abstract unregisterForControl: control: string -> unit
    abstract unregisterForCrosshairRef: unit -> unit
    abstract unregisterForKey: keyCode: float -> unit
    abstract unregisterForLOS: akViewer: Actor option * akTarget: ObjectReference option -> unit
    abstract unregisterForMenu: menuName: string -> unit
    abstract unregisterForModEvent: eventName: string -> unit
    abstract unregisterForNiNodeUpdate: unit -> unit
    abstract unregisterForSleep: unit -> unit
    abstract unregisterForTrackedStatsEvent: unit -> unit
    abstract unregisterForUpdate: unit -> unit
    abstract unregisterForUpdateGameTime: unit -> unit

type [<AllowNullLiteral>] ActiveMagicEffectStatic =
    [<EmitConstructor>] abstract Create: unit -> ActiveMagicEffect
    abstract from: papyrusObject: PapyrusObject option -> ActiveMagicEffect option

type [<AllowNullLiteral>] ObjectReference =
    inherit Form
    abstract activate: akActivator: ObjectReference option * abDefaultProcessingOnly: bool -> bool
    abstract addDependentAnimatedObjectReference: akDependent: ObjectReference option -> bool
    abstract addInventoryEventFilter: akFilter: Form option -> unit
    abstract addItem: akItemToAdd: Form option * aiCount: float * abSilent: bool -> unit
    abstract addToMap: abAllowFastTravel: bool -> unit
    abstract applyHavokImpulse: afX: float * afY: float * afZ: float * afMagnitude: float -> Promise<unit>
    abstract blockActivation: abBlocked: bool -> unit
    abstract calculateEncounterLevel: aiDifficulty: float -> float
    abstract canFastTravelToMarker: unit -> bool
    abstract clearDestruction: unit -> unit
    abstract createDetectionEvent: akOwner: Actor option * aiSoundLevel: float -> unit
    abstract createEnchantment: maxCharge: float * effects: ResizeArray<PapyrusObject> option * magnitudes: ResizeArray<float> option * areas: ResizeArray<float> option * durations: ResizeArray<float> option -> unit
    abstract damageObject: afDamage: float -> Promise<unit>
    abstract delete: unit -> Promise<unit>
    abstract disable: abFadeOut: bool -> Promise<unit>
    abstract disableNoWait: abFadeOut: bool -> unit
    abstract dropObject: akObject: Form option * aiCount: float -> Promise<ObjectReference option>
    abstract enable: abFadeIn: bool -> Promise<unit>
    abstract enableFastTravel: abEnable: bool -> unit
    abstract enableNoWait: abFadeIn: bool -> unit
    abstract forceAddRagdollToWorld: unit -> Promise<unit>
    abstract forceRemoveRagdollFromWorld: unit -> Promise<unit>
    abstract getActorOwner: unit -> ActorBase option
    abstract getAllForms: toFill: FormList option -> unit
    abstract getAngleX: unit -> float
    abstract getAngleY: unit -> float
    abstract getAngleZ: unit -> float
    abstract getAnimationVariableBool: arVariableName: string -> bool
    abstract getAnimationVariableFloat: arVariableName: string -> float
    abstract getAnimationVariableInt: arVariableName: string -> float
    abstract getBaseObject: unit -> Form option
    abstract getContainerForms: unit -> ResizeArray<PapyrusObject> option
    abstract getCurrentDestructionStage: unit -> float
    abstract getCurrentLocation: unit -> Location option
    abstract getCurrentScene: unit -> Scene option
    abstract getDisplayName: unit -> string
    abstract getEditorLocation: unit -> Location option
    abstract getEnableParent: unit -> ObjectReference option
    abstract getEnchantment: unit -> Enchantment option
    abstract getFactionOwner: unit -> Faction option
    abstract getHeadingAngle: akOther: ObjectReference option -> float
    abstract getHeight: unit -> float
    abstract getItemCharge: unit -> float
    abstract getItemCount: akItem: Form option -> float
    abstract getItemHealthPercent: unit -> float
    abstract getItemMaxCharge: unit -> float
    abstract getKey: unit -> Key option
    abstract getLength: unit -> float
    abstract getLinkedRef: apKeyword: Keyword option -> ObjectReference option
    abstract getLockLevel: unit -> float
    abstract getMass: unit -> float
    abstract getNthForm: index: float -> Form option
    abstract getNthLinkedRef: aiLinkedRef: float -> ObjectReference option
    abstract getNthReferenceAlias: n: float -> ReferenceAlias option
    abstract getNumItems: unit -> float
    abstract getNumReferenceAliases: unit -> float
    abstract getOpenState: unit -> float
    abstract getParentCell: unit -> Cell option
    abstract getPoison: unit -> Potion option
    abstract getPositionX: unit -> float
    abstract getPositionY: unit -> float
    abstract getPositionZ: unit -> float
    abstract getReferenceAliases: unit -> ResizeArray<PapyrusObject> option
    abstract getScale: unit -> float
    abstract getTotalArmorWeight: unit -> float
    abstract getTotalItemWeight: unit -> float
    abstract getTriggerObjectCount: unit -> float
    abstract getVoiceType: unit -> VoiceType option
    abstract getWidth: unit -> float
    abstract getWorldSpace: unit -> WorldSpace option
    abstract hasEffectKeyword: akKeyword: Keyword option -> bool
    abstract hasNode: asNodeName: string -> bool
    abstract hasRefType: akRefType: LocationRefType option -> bool
    abstract ignoreFriendlyHits: abIgnore: bool -> unit
    abstract interruptCast: unit -> unit
    abstract is3DLoaded: unit -> bool
    abstract isActivateChild: akChild: ObjectReference option -> bool
    abstract isActivationBlocked: unit -> bool
    abstract isDeleted: unit -> bool
    abstract isDisabled: unit -> bool
    abstract isFurnitureInUse: abIgnoreReserved: bool -> bool
    abstract isFurnitureMarkerInUse: aiMarker: float * abIgnoreReserved: bool -> bool
    abstract isHarvested: unit -> bool
    abstract isIgnoringFriendlyHits: unit -> bool
    abstract isInDialogueWithPlayer: unit -> bool
    abstract isLockBroken: unit -> bool
    abstract isLocked: unit -> bool
    abstract isMapMarkerVisible: unit -> bool
    abstract isOffLimits: unit -> bool
    abstract knockAreaEffect: afMagnitude: float * afRadius: float -> unit
    abstract lock: abLock: bool * abAsOwner: bool -> unit
    abstract moveTo: akTarget: ObjectReference option * afXOffset: float * afYOffset: float * afZOffset: float * abMatchRotation: bool -> Promise<unit>
    abstract moveToInteractionLocation: akTarget: ObjectReference option -> Promise<unit>
    abstract moveToMyEditorLocation: unit -> Promise<unit>
    abstract moveToNode: akTarget: ObjectReference option * asNodeName: string -> Promise<unit>
    abstract placeActorAtMe: akActorToPlace: ActorBase option * aiLevelMod: float * akZone: EncounterZone option -> Actor option
    abstract placeAtMe: akFormToPlace: Form option * aiCount: float * abForcePersist: bool * abInitiallyDisabled: bool -> ObjectReference option
    abstract playAnimation: asAnimation: string -> bool
    abstract playAnimationAndWait: asAnimation: string * asEventName: string -> Promise<bool>
    abstract playGamebryoAnimation: asAnimation: string * abStartOver: bool * afEaseInTime: float -> bool
    abstract playImpactEffect: akImpactEffect: ImpactDataSet option * asNodeName: string * afPickDirX: float * afPickDirY: float * afPickDirZ: float * afPickLength: float * abApplyNodeRotation: bool * abUseNodeLocalRotation: bool -> bool
    abstract playSyncedAnimationAndWaitSS: asAnimation1: string * asEvent1: string * akObj2: ObjectReference option * asAnimation2: string * asEvent2: string -> Promise<bool>
    abstract playSyncedAnimationSS: asAnimation1: string * akObj2: ObjectReference option * asAnimation2: string -> bool
    abstract playTerrainEffect: asEffectModelName: string * asAttachBoneName: string -> unit
    abstract processTrapHit: akTrap: ObjectReference option * afDamage: float * afPushback: float * afXVel: float * afYVel: float * afZVel: float * afXPos: float * afYPos: float * afZPos: float * aeMaterial: float * afStagger: float -> unit
    abstract pushActorAway: akActorToPush: Actor option * aiKnockbackForce: float -> unit
    abstract removeAllInventoryEventFilters: unit -> unit
    abstract removeAllItems: akTransferTo: ObjectReference option * abKeepOwnership: bool * abRemoveQuestItems: bool -> unit
    abstract removeDependentAnimatedObjectReference: akDependent: ObjectReference option -> bool
    abstract removeInventoryEventFilter: akFilter: Form option -> unit
    abstract removeItem: akItemToRemove: Form option * aiCount: float * abSilent: bool * akOtherContainer: ObjectReference option -> unit
    abstract reset: akTarget: ObjectReference option -> Promise<unit>
    abstract resetInventory: unit -> unit
    abstract say: akTopicToSay: Topic option * akActorToSpeakAs: Actor option * abSpeakInPlayersHead: bool -> unit
    abstract sendStealAlarm: akThief: Actor option -> unit
    abstract setActorCause: akActor: Actor option -> unit
    abstract setActorOwner: akActorBase: ActorBase option -> unit
    abstract setAngle: afXAngle: float * afYAngle: float * afZAngle: float -> Promise<unit>
    abstract setAnimationVariableBool: arVariableName: string * abNewValue: bool -> unit
    abstract setAnimationVariableFloat: arVariableName: string * afNewValue: float -> unit
    abstract setAnimationVariableInt: arVariableName: string * aiNewValue: float -> unit
    abstract setDestroyed: abDestroyed: bool -> unit
    abstract setDisplayName: name: string * force: bool -> bool
    abstract setEnchantment: source: Enchantment option * maxCharge: float -> unit
    abstract setFactionOwner: akFaction: Faction option -> unit
    abstract setHarvested: harvested: bool -> unit
    abstract setItemCharge: charge: float -> unit
    abstract setItemHealthPercent: health: float -> unit
    abstract setItemMaxCharge: maxCharge: float -> unit
    abstract setLockLevel: aiLockLevel: float -> unit
    abstract setMotionType: aeMotionType: MotionType * abAllowActivate: bool -> Promise<unit>
    abstract setNoFavorAllowed: abNoFavor: bool -> unit
    abstract setOpen: abOpen: bool -> unit
    abstract setPosition: afX: float * afY: float * afZ: float -> Promise<unit>
    abstract setScale: afScale: float -> Promise<unit>
    abstract splineTranslateTo: afX: float * afY: float * afZ: float * afXAngle: float * afYAngle: float * afZAngle: float * afTangentMagnitude: float * afSpeed: float * afMaxRotationSpeed: float -> unit
    abstract splineTranslateToRefNode: arTarget: ObjectReference option * arNodeName: string * afTangentMagnitude: float * afSpeed: float * afMaxRotationSpeed: float -> unit
    abstract stopTranslation: unit -> unit
    abstract tetherToHorse: akHorse: ObjectReference option -> unit
    abstract translateTo: afX: float * afY: float * afZ: float * afXAngle: float * afYAngle: float * afZAngle: float * afSpeed: float * afMaxRotationSpeed: float -> unit
    abstract waitForAnimationEvent: asEventName: string -> Promise<bool>
    abstract getDistance: akOther: ObjectReference option -> float

type [<AllowNullLiteral>] ObjectReferenceStatic =
    [<EmitConstructor>] abstract Create: unit -> ObjectReference
    abstract from: papyrusObject: PapyrusObject option -> ObjectReference option

type [<AllowNullLiteral>] Actor =
    inherit ObjectReference
    abstract addPerk: akPerk: Perk option -> unit
    abstract addShout: akShout: Shout option -> bool
    abstract addSpell: akSpell: Spell option * abVerbose: bool -> bool
    abstract allowBleedoutDialogue: abCanTalk: bool -> unit
    abstract allowPCDialogue: abTalk: bool -> unit
    abstract attachAshPile: akAshPileBase: Form option -> unit
    abstract canFlyHere: unit -> bool
    abstract changeHeadPart: hPart: HeadPart option -> unit
    abstract clearArrested: unit -> unit
    abstract clearExpressionOverride: unit -> unit
    abstract clearExtraArrows: unit -> unit
    abstract clearForcedMovement: unit -> unit
    abstract clearKeepOffsetFromActor: unit -> unit
    abstract clearLookAt: unit -> unit
    abstract damageActorValue: asValueName: string * afDamage: float -> unit
    abstract dismount: unit -> bool
    abstract dispelAllSpells: unit -> unit
    abstract dispelSpell: akSpell: Spell option -> bool
    abstract doCombatSpellApply: akSpell: Spell option * akTarget: ObjectReference option -> unit
    abstract drawWeapon: unit -> unit
    abstract enableAI: abEnable: bool -> unit
    abstract endDeferredKill: unit -> unit
    abstract equipItem: akItem: Form option * abPreventRemoval: bool * abSilent: bool -> unit
    abstract equipItemById: item: Form option * itemId: float * equipSlot: float * preventUnequip: bool * equipSound: bool -> unit
    abstract equipItemEx: item: Form option * equipSlot: float * preventUnequip: bool * equipSound: bool -> unit
    abstract equipShout: akShout: Shout option -> unit
    abstract equipSpell: akSpell: Spell option * aiSource: float -> unit
    abstract evaluatePackage: unit -> unit
    abstract forceActorValue: asValueName: string * afNewValue: float -> unit
    abstract forceMovementDirection: afXAngle: float * afYAngle: float * afZAngle: float -> unit
    abstract forceMovementDirectionRamp: afXAngle: float * afYAngle: float * afZAngle: float * afRampTime: float -> unit
    abstract forceMovementRotationSpeed: afXMult: float * afYMult: float * afZMult: float -> unit
    abstract forceMovementRotationSpeedRamp: afXMult: float * afYMult: float * afZMult: float * afRampTime: float -> unit
    abstract forceMovementSpeed: afSpeedMult: float -> unit
    abstract forceMovementSpeedRamp: afSpeedMult: float * afRampTime: float -> unit
    abstract forceTargetAngle: afXAngle: float * afYAngle: float * afZAngle: float -> unit
    abstract forceTargetDirection: afXAngle: float * afYAngle: float * afZAngle: float -> unit
    abstract forceTargetSpeed: afSpeed: float -> unit
    abstract getActorValue: asValueName: string -> float
    abstract getActorValueMax: asValueName: string -> float
    abstract getActorValuePercentage: asValueName: string -> float
    abstract getBaseActorValue: asValueName: string -> float
    abstract getBribeAmount: unit -> float
    abstract getCombatState: unit -> float
    abstract getCombatTarget: unit -> Actor option
    abstract getCrimeFaction: unit -> Faction option
    abstract getCurrentPackage: unit -> Package option
    abstract getDialogueTarget: unit -> Actor option
    abstract getEquippedArmorInSlot: aiSlot: float -> Armor option
    abstract getEquippedItemId: Location: float -> float
    abstract getEquippedItemType: aiHand: float -> float
    abstract getEquippedObject: Location: float -> Form option
    abstract getEquippedShield: unit -> Armor option
    abstract getEquippedShout: unit -> Shout option
    abstract getEquippedSpell: aiSource: float -> Spell option
    abstract getEquippedWeapon: abLeftHand: bool -> Weapon option
    abstract getFactionRank: akFaction: Faction option -> float
    abstract getFactionReaction: akOther: Actor option -> float
    abstract getFactions: minRank: float * maxRank: float -> ResizeArray<PapyrusObject> option
    abstract getFlyingState: unit -> float
    abstract getForcedLandingMarker: unit -> ObjectReference option
    abstract getFurnitureReference: unit -> ObjectReference option
    abstract getGoldAmount: unit -> float
    abstract getHighestRelationshipRank: unit -> float
    abstract getKiller: unit -> Actor option
    abstract getLevel: unit -> float
    abstract getLeveledActorBase: unit -> ActorBase option
    abstract getLightLevel: unit -> float
    abstract getLowestRelationshipRank: unit -> float
    abstract getNoBleedoutRecovery: unit -> bool
    abstract getNthSpell: n: float -> Spell option
    abstract getPlayerControls: unit -> bool
    abstract getRace: unit -> Race option
    abstract getRelationshipRank: akOther: Actor option -> float
    abstract getSitState: unit -> float
    abstract getSleepState: unit -> float
    abstract getSpellCount: unit -> float
    abstract getVoiceRecoveryTime: unit -> float
    abstract getWarmthRating: unit -> float
    abstract getWornForm: slotMask: float -> Form option
    abstract getWornItemId: slotMask: float -> float
    abstract hasAssociation: akAssociation: AssociationType option * akOther: Actor option -> bool
    abstract hasFamilyRelationship: akOther: Actor option -> bool
    abstract hasLOS: akOther: ObjectReference option -> bool
    abstract hasMagicEffect: akEffect: MagicEffect option -> bool
    abstract hasMagicEffectWithKeyword: akKeyword: Keyword option -> bool
    abstract hasParentRelationship: akOther: Actor option -> bool
    abstract hasPerk: akPerk: Perk option -> bool
    abstract hasSpell: akForm: Form option -> bool
    abstract isAIEnabled: unit -> bool
    abstract isAlarmed: unit -> bool
    abstract isAlerted: unit -> bool
    abstract isAllowedToFly: unit -> bool
    abstract isArrested: unit -> bool
    abstract isArrestingTarget: unit -> bool
    abstract isBeingRidden: unit -> bool
    abstract isBleedingOut: unit -> bool
    abstract isBribed: unit -> bool
    abstract isChild: unit -> bool
    abstract isCommandedActor: unit -> bool
    abstract isDead: unit -> bool
    abstract isDetectedBy: akOther: Actor option -> bool
    abstract isDoingFavor: unit -> bool
    abstract isEquipped: akItem: Form option -> bool
    abstract isEssential: unit -> bool
    abstract isFlying: unit -> bool
    abstract isGhost: unit -> bool
    abstract isGuard: unit -> bool
    abstract isHostileToActor: akActor: Actor option -> bool
    abstract isInCombat: unit -> bool
    abstract isInFaction: akFaction: Faction option -> bool
    abstract isInKillMove: unit -> bool
    abstract isIntimidated: unit -> bool
    abstract isOnMount: unit -> bool
    abstract isOverEncumbered: unit -> bool
    abstract isPlayerTeammate: unit -> bool
    abstract isPlayersLastRiddenHorse: unit -> bool
    abstract isRunning: unit -> bool
    abstract isSneaking: unit -> bool
    abstract isSprinting: unit -> bool
    abstract isSwimming: unit -> bool
    abstract isTrespassing: unit -> bool
    abstract isUnconscious: unit -> bool
    abstract isWeaponDrawn: unit -> bool
    abstract keepOffsetFromActor: arTarget: Actor option * afOffsetX: float * afOffsetY: float * afOffsetZ: float * afOffsetAngleX: float * afOffsetAngleY: float * afOffsetAngleZ: float * afCatchUpRadius: float * afFollowRadius: float -> unit
    abstract kill: akKiller: Actor option -> unit
    abstract killSilent: akKiller: Actor option -> unit
    abstract modActorValue: asValueName: string * afAmount: float -> unit
    abstract modFactionRank: akFaction: Faction option * aiMod: float -> unit
    abstract moveToPackageLocation: unit -> Promise<unit>
    abstract openInventory: abForceOpen: bool -> unit
    abstract pathToReference: aTarget: ObjectReference option * afWalkRunPercent: float -> Promise<bool>
    abstract playIdle: akIdle: Idle option -> bool
    abstract playIdleWithTarget: akIdle: Idle option * akTarget: ObjectReference option -> bool
    abstract playSubGraphAnimation: asEventName: string -> unit
    abstract queueNiNodeUpdate: unit -> unit
    abstract regenerateHead: unit -> unit
    abstract removeFromAllFactions: unit -> unit
    abstract removeFromFaction: akFaction: Faction option -> unit
    abstract removePerk: akPerk: Perk option -> unit
    abstract removeShout: akShout: Shout option -> bool
    abstract removeSpell: akSpell: Spell option -> bool
    abstract replaceHeadPart: oPart: HeadPart option * newPart: HeadPart option -> unit
    abstract resetAI: unit -> unit
    abstract resetExpressionOverrides: unit -> unit
    abstract resetHealthAndLimbs: unit -> unit
    abstract restoreActorValue: asValueName: string * afAmount: float -> unit
    abstract resurrect: unit -> Promise<unit>
    abstract sendAssaultAlarm: unit -> unit
    abstract sendLycanthropyStateChanged: abIsWerewolf: bool -> unit
    abstract sendTrespassAlarm: akCriminal: Actor option -> unit
    abstract sendVampirismStateChanged: abIsVampire: bool -> unit
    abstract setActorValue: asValueName: string * afValue: float -> unit
    abstract setAlert: abAlerted: bool -> unit
    abstract setAllowFlying: abAllowed: bool -> unit
    abstract setAllowFlyingEx: abAllowed: bool * abAllowCrash: bool * abAllowSearch: bool -> unit
    abstract setAlpha: afTargetAlpha: float * abFade: bool -> unit
    abstract setAttackActorOnSight: abAttackOnSight: bool -> unit
    abstract setBribed: abBribe: bool -> unit
    abstract setCrimeFaction: akFaction: Faction option -> unit
    abstract setCriticalStage: aiStage: float -> unit
    abstract setDoingFavor: abDoingFavor: bool -> unit
    abstract setDontMove: abDontMove: bool -> unit
    abstract setExpressionModifier: index: float * value: float -> unit
    abstract setExpressionOverride: aiMood: float * aiStrength: float -> unit
    abstract setExpressionPhoneme: index: float * value: float -> unit
    abstract setEyeTexture: akNewTexture: TextureSet option -> unit
    abstract setFactionRank: akFaction: Faction option * aiRank: float -> unit
    abstract setForcedLandingMarker: aMarker: ObjectReference option -> unit
    abstract setGhost: abIsGhost: bool -> unit
    abstract setHeadTracking: abEnable: bool -> unit
    abstract setIntimidated: abIntimidate: bool -> unit
    abstract setLookAt: akTarget: ObjectReference option * abPathingLookAt: bool -> unit
    abstract setNoBleedoutRecovery: abAllowed: bool -> unit
    abstract setNotShowOnStealthMeter: abNotShow: bool -> unit
    abstract setOutfit: akOutfit: Outfit option * abSleepOutfit: bool -> unit
    abstract setPlayerControls: abControls: bool -> unit
    abstract setPlayerResistingArrest: unit -> unit
    abstract setPlayerTeammate: abTeammate: bool * abCanDoFavor: bool -> unit
    abstract setRace: akRace: Race option -> unit
    abstract setRelationshipRank: akOther: Actor option * aiRank: float -> unit
    abstract setRestrained: abRestrained: bool -> unit
    abstract setSubGraphFloatVariable: asVariableName: string * afValue: float -> unit
    abstract setUnconscious: abUnconscious: bool -> unit
    abstract setVehicle: akVehicle: ObjectReference option -> unit
    abstract setVoiceRecoveryTime: afTime: float -> unit
    abstract sheatheWeapon: unit -> unit
    abstract showBarterMenu: unit -> unit
    abstract showGiftMenu: abGivingGift: bool * apFilterList: FormList option * abShowStolenItems: bool * abUseFavorPoints: bool -> Promise<float>
    abstract startCannibal: akTarget: Actor option -> unit
    abstract startCombat: akTarget: Actor option -> unit
    abstract startDeferredKill: unit -> unit
    abstract startSneaking: unit -> unit
    abstract startVampireFeed: akTarget: Actor option -> unit
    abstract stopCombat: unit -> unit
    abstract stopCombatAlarm: unit -> unit
    abstract trapSoul: akTarget: Actor option -> bool
    abstract unLockOwnedDoorsInCell: unit -> unit
    abstract unequipAll: unit -> unit
    abstract unequipItem: akItem: Form option * abPreventEquip: bool * abSilent: bool -> unit
    abstract unequipItemEx: item: Form option * equipSlot: float * preventEquip: bool -> unit
    abstract unequipItemSlot: aiSlot: float -> unit
    abstract unequipShout: akShout: Shout option -> unit
    abstract unequipSpell: akSpell: Spell option * aiSource: float -> unit
    abstract updateWeight: neckDelta: float -> unit
    abstract willIntimidateSucceed: unit -> bool
    abstract wornHasKeyword: akKeyword: Keyword option -> bool

type [<AllowNullLiteral>] ActorStatic =
    [<EmitConstructor>] abstract Create: unit -> Actor
    abstract from: papyrusObject: PapyrusObject option -> Actor option

type [<AllowNullLiteral>] ActorBase =
    inherit Form
    abstract getClass: unit -> Class option
    abstract getCombatStyle: unit -> CombatStyle option
    abstract getDeadCount: unit -> float
    abstract getFaceMorph: index: float -> float
    abstract getFacePreset: index: float -> float
    abstract getFaceTextureSet: unit -> TextureSet option
    abstract getGiftFilter: unit -> FormList option
    abstract getHairColor: unit -> ColorForm option
    abstract getHeight: unit -> float
    abstract getIndexOfHeadPartByType: ``type``: float -> float
    abstract getIndexOfOverlayHeadPartByType: ``type``: float -> float
    abstract getNthHeadPart: slotPart: float -> HeadPart option
    abstract getNthOverlayHeadPart: slotPart: float -> HeadPart option
    abstract getNthSpell: n: float -> Spell option
    abstract getNumHeadParts: unit -> float
    abstract getNumOverlayHeadParts: unit -> float
    abstract getOutfit: bSleepOutfit: bool -> Outfit option
    abstract getRace: unit -> Race option
    abstract getSex: unit -> float
    abstract getSkin: unit -> Armor option
    abstract getSkinFar: unit -> Armor option
    abstract getSpellCount: unit -> float
    abstract getTemplate: unit -> ActorBase option
    abstract getVoiceType: unit -> VoiceType option
    abstract getWeight: unit -> float
    abstract isEssential: unit -> bool
    abstract isInvulnerable: unit -> bool
    abstract isProtected: unit -> bool
    abstract isUnique: unit -> bool
    abstract setClass: c: Class option -> unit
    abstract setCombatStyle: cs: CombatStyle option -> unit
    abstract setEssential: abEssential: bool -> unit
    abstract setFaceMorph: value: float * index: float -> unit
    abstract setFacePreset: value: float * index: float -> unit
    abstract setFaceTextureSet: textures: TextureSet option -> unit
    abstract setHairColor: color: ColorForm option -> unit
    abstract setHeight: height: float -> unit
    abstract setInvulnerable: abInvulnerable: bool -> unit
    abstract setNthHeadPart: HeadPart: HeadPart option * slotPart: float -> unit
    abstract setOutfit: akOutfit: Outfit option * abSleepOutfit: bool -> unit
    abstract setProtected: abProtected: bool -> unit
    abstract setSkin: skin: Armor option -> unit
    abstract setSkinFar: skin: Armor option -> unit
    abstract setVoiceType: nVoice: VoiceType option -> unit
    abstract setWeight: weight: float -> unit

type [<AllowNullLiteral>] ActorBaseStatic =
    [<EmitConstructor>] abstract Create: unit -> ActorBase
    abstract from: papyrusObject: PapyrusObject option -> ActorBase option

type [<AllowNullLiteral>] ActorValueInfo =
    inherit Form
    abstract addSkillExperience: exp: float -> unit
    abstract getBaseValue: akActor: Actor option -> float
    abstract getCurrentValue: akActor: Actor option -> float
    abstract getExperienceForLevel: currentLevel: float -> float
    abstract getMaximumValue: akActor: Actor option -> float
    abstract getPerkTree: list: FormList option * akActor: Actor option * unowned: bool * allRanks: bool -> unit
    abstract getPerks: akActor: Actor option * unowned: bool * allRanks: bool -> ResizeArray<PapyrusObject> option
    abstract getSkillExperience: unit -> float
    abstract getSkillImproveMult: unit -> float
    abstract getSkillImproveOffset: unit -> float
    abstract getSkillLegendaryLevel: unit -> float
    abstract getSkillOffsetMult: unit -> float
    abstract getSkillUseMult: unit -> float
    abstract isSkill: unit -> bool
    abstract setSkillExperience: exp: float -> unit
    abstract setSkillImproveMult: value: float -> unit
    abstract setSkillImproveOffset: value: float -> unit
    abstract setSkillLegendaryLevel: level: float -> unit
    abstract setSkillOffsetMult: value: float -> unit
    abstract setSkillUseMult: value: float -> unit

type [<AllowNullLiteral>] ActorValueInfoStatic =
    [<EmitConstructor>] abstract Create: unit -> ActorValueInfo
    abstract from: papyrusObject: PapyrusObject option -> ActorValueInfo option
    abstract getActorValueInfoByID: id: float -> ActorValueInfo option
    abstract getActorValueInfoByName: avName: string -> ActorValueInfo option

type [<AllowNullLiteral>] Alias =
    inherit PapyrusObject
    abstract getID: unit -> float
    abstract getName: unit -> string
    abstract getOwningQuest: unit -> Quest option
    abstract registerForActorAction: actionType: float -> unit
    abstract registerForAnimationEvent: akSender: ObjectReference option * asEventName: string -> bool
    abstract registerForCameraState: unit -> unit
    abstract registerForControl: control: string -> unit
    abstract registerForCrosshairRef: unit -> unit
    abstract registerForKey: keyCode: float -> unit
    abstract registerForLOS: akViewer: Actor option * akTarget: ObjectReference option -> unit
    abstract registerForMenu: menuName: string -> unit
    abstract registerForModEvent: eventName: string * callbackName: string -> unit
    abstract registerForNiNodeUpdate: unit -> unit
    abstract registerForSingleLOSGain: akViewer: Actor option * akTarget: ObjectReference option -> unit
    abstract registerForSingleLOSLost: akViewer: Actor option * akTarget: ObjectReference option -> unit
    abstract registerForSingleUpdate: afInterval: float -> unit
    abstract registerForSingleUpdateGameTime: afInterval: float -> unit
    abstract registerForSleep: unit -> unit
    abstract registerForTrackedStatsEvent: unit -> unit
    abstract registerForUpdate: afInterval: float -> unit
    abstract registerForUpdateGameTime: afInterval: float -> unit
    abstract sendModEvent: eventName: string * strArg: string * numArg: float -> unit
    abstract startObjectProfiling: unit -> unit
    abstract stopObjectProfiling: unit -> unit
    abstract unregisterForActorAction: actionType: float -> unit
    abstract unregisterForAllControls: unit -> unit
    abstract unregisterForAllKeys: unit -> unit
    abstract unregisterForAllMenus: unit -> unit
    abstract unregisterForAllModEvents: unit -> unit
    abstract unregisterForAnimationEvent: akSender: ObjectReference option * asEventName: string -> unit
    abstract unregisterForCameraState: unit -> unit
    abstract unregisterForControl: control: string -> unit
    abstract unregisterForCrosshairRef: unit -> unit
    abstract unregisterForKey: keyCode: float -> unit
    abstract unregisterForLOS: akViewer: Actor option * akTarget: ObjectReference option -> unit
    abstract unregisterForMenu: menuName: string -> unit
    abstract unregisterForModEvent: eventName: string -> unit
    abstract unregisterForNiNodeUpdate: unit -> unit
    abstract unregisterForSleep: unit -> unit
    abstract unregisterForTrackedStatsEvent: unit -> unit
    abstract unregisterForUpdate: unit -> unit
    abstract unregisterForUpdateGameTime: unit -> unit

type [<AllowNullLiteral>] AliasStatic =
    [<EmitConstructor>] abstract Create: unit -> Alias
    abstract from: papyrusObject: PapyrusObject option -> Alias option

type [<AllowNullLiteral>] Ammo =
    inherit Form
    abstract getDamage: unit -> float
    abstract getProjectile: unit -> Projectile option
    abstract isBolt: unit -> bool

type [<AllowNullLiteral>] AmmoStatic =
    [<EmitConstructor>] abstract Create: unit -> Ammo
    abstract from: papyrusObject: PapyrusObject option -> Ammo option

type [<AllowNullLiteral>] MiscObject =
    inherit Form

type [<AllowNullLiteral>] MiscObjectStatic =
    [<EmitConstructor>] abstract Create: unit -> MiscObject
    abstract from: papyrusObject: PapyrusObject option -> MiscObject option

type [<AllowNullLiteral>] Apparatus =
    inherit MiscObject
    abstract getQuality: unit -> float
    abstract setQuality: quality: float -> unit

type [<AllowNullLiteral>] ApparatusStatic =
    [<EmitConstructor>] abstract Create: unit -> Apparatus
    abstract from: papyrusObject: PapyrusObject option -> Apparatus option

type [<AllowNullLiteral>] Armor =
    inherit Form
    abstract addSlotToMask: slotMask: float -> float
    abstract getArmorRating: unit -> float
    abstract getEnchantment: unit -> Enchantment option
    abstract getIconPath: bFemalePath: bool -> string
    abstract getMessageIconPath: bFemalePath: bool -> string
    abstract getModelPath: bFemalePath: bool -> string
    abstract getNthArmorAddon: n: float -> ArmorAddon option
    abstract getNumArmorAddons: unit -> float
    abstract getSlotMask: unit -> float
    abstract getWarmthRating: unit -> float
    abstract getWeightClass: unit -> float
    abstract modArmorRating: modBy: float -> unit
    abstract removeSlotFromMask: slotMask: float -> float
    abstract setArmorRating: armorRating: float -> unit
    abstract setEnchantment: e: Enchantment option -> unit
    abstract setIconPath: path: string * bFemalePath: bool -> unit
    abstract setMessageIconPath: path: string * bFemalePath: bool -> unit
    abstract setModelPath: path: string * bFemalePath: bool -> unit
    abstract setSlotMask: slotMask: float -> unit
    abstract setWeightClass: weightClass: float -> unit

type [<AllowNullLiteral>] ArmorStatic =
    [<EmitConstructor>] abstract Create: unit -> Armor
    abstract from: papyrusObject: PapyrusObject option -> Armor option
    abstract getMaskForSlot: slot: float -> float

type [<AllowNullLiteral>] ArmorAddon =
    inherit Form
    abstract addSlotToMask: slotMask: float -> float
    abstract getModelNthTextureSet: n: float * first: bool * female: bool -> TextureSet option
    abstract getModelNumTextureSets: first: bool * female: bool -> float
    abstract getModelPath: firstPerson: bool * female: bool -> string
    abstract getNthAdditionalRace: n: float -> Race option
    abstract getNumAdditionalRaces: unit -> float
    abstract getSlotMask: unit -> float
    abstract removeSlotFromMask: slotMask: float -> float
    abstract setModelNthTextureSet: texture: TextureSet option * n: float * first: bool * female: bool -> unit
    abstract setModelPath: path: string * firstPerson: bool * female: bool -> unit
    abstract setSlotMask: slotMask: float -> unit

type [<AllowNullLiteral>] ArmorAddonStatic =
    [<EmitConstructor>] abstract Create: unit -> ArmorAddon
    abstract from: papyrusObject: PapyrusObject option -> ArmorAddon option

type [<AllowNullLiteral>] Art =
    inherit Form
    abstract getModelPath: unit -> string
    abstract setModelPath: path: string -> unit

type [<AllowNullLiteral>] ArtStatic =
    [<EmitConstructor>] abstract Create: unit -> Art
    abstract from: papyrusObject: PapyrusObject option -> Art option

type [<AllowNullLiteral>] AssociationType =
    inherit Form

type [<AllowNullLiteral>] AssociationTypeStatic =
    [<EmitConstructor>] abstract Create: unit -> AssociationType
    abstract from: papyrusObject: PapyrusObject option -> AssociationType option

type [<AllowNullLiteral>] Book =
    inherit Form
    abstract getSkill: unit -> float
    abstract getSpell: unit -> Spell option
    abstract isRead: unit -> bool
    abstract isTakeable: unit -> bool

type [<AllowNullLiteral>] BookStatic =
    [<EmitConstructor>] abstract Create: unit -> Book
    abstract from: papyrusObject: PapyrusObject option -> Book option

type [<AllowNullLiteral>] Cell =
    inherit Form
    abstract getActorOwner: unit -> ActorBase option
    abstract getFactionOwner: unit -> Faction option
    abstract getNthRef: n: float * formTypeFilter: float -> ObjectReference option
    abstract getNumRefs: formTypeFilter: float -> float
    abstract getWaterLevel: unit -> float
    abstract isAttached: unit -> bool
    abstract isInterior: unit -> bool
    abstract reset: unit -> unit
    abstract setActorOwner: akActor: ActorBase option -> unit
    abstract setFactionOwner: akFaction: Faction option -> unit
    abstract setFogColor: aiNearRed: float * aiNearGreen: float * aiNearBlue: float * aiFarRed: float * aiFarGreen: float * aiFarBlue: float -> unit
    abstract setFogPlanes: afNear: float * afFar: float -> unit
    abstract setFogPower: afPower: float -> unit
    abstract setPublic: abPublic: bool -> unit

type [<AllowNullLiteral>] CellStatic =
    [<EmitConstructor>] abstract Create: unit -> Cell
    abstract from: papyrusObject: PapyrusObject option -> Cell option

type [<AllowNullLiteral>] Class =
    inherit Form

type [<AllowNullLiteral>] ClassStatic =
    [<EmitConstructor>] abstract Create: unit -> Class
    abstract from: papyrusObject: PapyrusObject option -> Class option

type [<AllowNullLiteral>] ColorForm =
    inherit Form
    abstract getColor: unit -> float
    abstract setColor: color: float -> unit

type [<AllowNullLiteral>] ColorFormStatic =
    [<EmitConstructor>] abstract Create: unit -> ColorForm
    abstract from: papyrusObject: PapyrusObject option -> ColorForm option

type [<AllowNullLiteral>] CombatStyle =
    inherit Form
    abstract getAllowDualWielding: unit -> bool
    abstract getAvoidThreatChance: unit -> float
    abstract getCloseRangeDuelingCircleMult: unit -> float
    abstract getCloseRangeDuelingFallbackMult: unit -> float
    abstract getCloseRangeFlankingFlankDistance: unit -> float
    abstract getCloseRangeFlankingStalkTime: unit -> float
    abstract getDefensiveMult: unit -> float
    abstract getFlightDiveBombChance: unit -> float
    abstract getFlightFlyingAttackChance: unit -> float
    abstract getFlightHoverChance: unit -> float
    abstract getGroupOffensiveMult: unit -> float
    abstract getLongRangeStrafeMult: unit -> float
    abstract getMagicMult: unit -> float
    abstract getMeleeAttackStaggeredMult: unit -> float
    abstract getMeleeBashAttackMult: unit -> float
    abstract getMeleeBashMult: unit -> float
    abstract getMeleeBashPowerAttackMult: unit -> float
    abstract getMeleeBashRecoiledMult: unit -> float
    abstract getMeleeMult: unit -> float
    abstract getMeleePowerAttackBlockingMult: unit -> float
    abstract getMeleePowerAttackStaggeredMult: unit -> float
    abstract getMeleeSpecialAttackMult: unit -> float
    abstract getOffensiveMult: unit -> float
    abstract getRangedMult: unit -> float
    abstract getShoutMult: unit -> float
    abstract getStaffMult: unit -> float
    abstract getUnarmedMult: unit -> float
    abstract setAllowDualWielding: allow: bool -> unit
    abstract setAvoidThreatChance: chance: float -> unit
    abstract setCloseRangeDuelingCircleMult: mult: float -> unit
    abstract setCloseRangeDuelingFallbackMult: mult: float -> unit
    abstract setCloseRangeFlankingFlankDistance: mult: float -> unit
    abstract setCloseRangeFlankingStalkTime: mult: float -> unit
    abstract setDefensiveMult: mult: float -> unit
    abstract setFlightDiveBombChance: chance: float -> unit
    abstract setFlightFlyingAttackChance: mult: float -> unit
    abstract setFlightHoverChance: chance: float -> unit
    abstract setGroupOffensiveMult: mult: float -> unit
    abstract setLongRangeStrafeMult: mult: float -> unit
    abstract setMagicMult: mult: float -> unit
    abstract setMeleeAttackStaggeredMult: mult: float -> unit
    abstract setMeleeBashAttackMult: mult: float -> unit
    abstract setMeleeBashMult: mult: float -> unit
    abstract setMeleeBashPowerAttackMult: mult: float -> unit
    abstract setMeleeBashRecoiledMult: mult: float -> unit
    abstract setMeleeMult: mult: float -> unit
    abstract setMeleePowerAttackBlockingMult: mult: float -> unit
    abstract setMeleePowerAttackStaggeredMult: mult: float -> unit
    abstract setMeleeSpecialAttackMult: mult: float -> unit
    abstract setOffensiveMult: mult: float -> unit
    abstract setRangedMult: mult: float -> unit
    abstract setShoutMult: mult: float -> unit
    abstract setStaffMult: mult: float -> unit
    abstract setUnarmedMult: mult: float -> unit

type [<AllowNullLiteral>] CombatStyleStatic =
    [<EmitConstructor>] abstract Create: unit -> CombatStyle
    abstract from: papyrusObject: PapyrusObject option -> CombatStyle option

type [<AllowNullLiteral>] ConstructibleObject =
    inherit MiscObject
    abstract getNthIngredient: n: float -> Form option
    abstract getNthIngredientQuantity: n: float -> float
    abstract getNumIngredients: unit -> float
    abstract getResult: unit -> Form option
    abstract getResultQuantity: unit -> float
    abstract getWorkbenchKeyword: unit -> Keyword option
    abstract setNthIngredient: required: Form option * n: float -> unit
    abstract setNthIngredientQuantity: value: float * n: float -> unit
    abstract setResult: result: Form option -> unit
    abstract setResultQuantity: quantity: float -> unit
    abstract setWorkbenchKeyword: aKeyword: Keyword option -> unit

type [<AllowNullLiteral>] ConstructibleObjectStatic =
    [<EmitConstructor>] abstract Create: unit -> ConstructibleObject
    abstract from: papyrusObject: PapyrusObject option -> ConstructibleObject option

type [<AllowNullLiteral>] Container =
    inherit Form

type [<AllowNullLiteral>] ContainerStatic =
    [<EmitConstructor>] abstract Create: unit -> Container
    abstract from: papyrusObject: PapyrusObject option -> Container option

type [<AllowNullLiteral>] Debug =
    inherit PapyrusObject

type [<AllowNullLiteral>] DebugStatic =
    [<EmitConstructor>] abstract Create: unit -> Debug
    abstract from: papyrusObject: PapyrusObject option -> Debug option
    abstract centerOnCell: param1: string -> unit
    abstract centerOnCellAndWait: param1: string -> Promise<float>
    abstract closeUserLog: param1: string -> unit
    abstract dBSendPlayerPosition: unit -> unit
    abstract debugChannelNotify: param1: string * param2: string -> unit
    abstract dumpAliasData: param1: Quest option -> unit
    abstract getConfigName: unit -> Promise<string>
    abstract getPlatformName: unit -> Promise<string>
    abstract getVersionNumber: unit -> Promise<string>
    abstract messageBox: param1: string -> unit
    abstract notification: param1: string -> unit
    abstract openUserLog: param1: string -> bool
    abstract playerMoveToAndWait: param1: string -> Promise<float>
    abstract quitGame: unit -> unit
    abstract sendAnimationEvent: param1: ObjectReference option * param2: string -> unit
    abstract setFootIK: param1: bool -> unit
    abstract setGodMode: param1: bool -> unit
    abstract showRefPosition: arRef: ObjectReference option -> unit
    abstract startScriptProfiling: param1: string -> unit
    abstract startStackProfiling: unit -> unit
    abstract stopScriptProfiling: param1: string -> unit
    abstract stopStackProfiling: unit -> unit
    abstract takeScreenshot: param1: string -> unit
    abstract toggleAI: unit -> unit
    abstract toggleCollisions: unit -> unit
    abstract toggleMenus: unit -> unit
    abstract trace: param1: string * param2: float -> unit
    abstract traceStack: param1: string * param2: float -> unit
    abstract traceUser: param1: string * param2: string * param3: float -> bool

type [<AllowNullLiteral>] DefaultObjectManager =
    inherit Form
    abstract getForm: key: string -> Form option
    abstract setForm: key: string * newForm: Form option -> unit

type [<AllowNullLiteral>] DefaultObjectManagerStatic =
    [<EmitConstructor>] abstract Create: unit -> DefaultObjectManager
    abstract from: papyrusObject: PapyrusObject option -> DefaultObjectManager option

type [<AllowNullLiteral>] Door =
    inherit Form

type [<AllowNullLiteral>] DoorStatic =
    [<EmitConstructor>] abstract Create: unit -> Door
    abstract from: papyrusObject: PapyrusObject option -> Door option

type [<AllowNullLiteral>] EffectShader =
    inherit Form
    abstract play: param1: ObjectReference option * param2: float -> unit
    abstract stop: param1: ObjectReference option -> unit

type [<AllowNullLiteral>] EffectShaderStatic =
    [<EmitConstructor>] abstract Create: unit -> EffectShader
    abstract from: papyrusObject: PapyrusObject option -> EffectShader option

type [<AllowNullLiteral>] Enchantment =
    inherit Form
    abstract getBaseEnchantment: unit -> Enchantment option
    abstract getCostliestEffectIndex: unit -> float
    abstract getKeywordRestrictions: unit -> FormList option
    abstract getNthEffectArea: index: float -> float
    abstract getNthEffectDuration: index: float -> float
    abstract getNthEffectMagicEffect: index: float -> MagicEffect option
    abstract getNthEffectMagnitude: index: float -> float
    abstract getNumEffects: unit -> float
    abstract isHostile: unit -> bool
    abstract setKeywordRestrictions: newKeywordList: FormList option -> unit
    abstract setNthEffectArea: index: float * value: float -> unit
    abstract setNthEffectDuration: index: float * value: float -> unit
    abstract setNthEffectMagnitude: index: float * value: float -> unit

type [<AllowNullLiteral>] EnchantmentStatic =
    [<EmitConstructor>] abstract Create: unit -> Enchantment
    abstract from: papyrusObject: PapyrusObject option -> Enchantment option

type [<AllowNullLiteral>] EncounterZone =
    inherit Form

type [<AllowNullLiteral>] EncounterZoneStatic =
    [<EmitConstructor>] abstract Create: unit -> EncounterZone
    abstract from: papyrusObject: PapyrusObject option -> EncounterZone option

type [<AllowNullLiteral>] EquipSlot =
    inherit Form
    abstract getNthParent: n: float -> EquipSlot option
    abstract getNumParents: unit -> float

type [<AllowNullLiteral>] EquipSlotStatic =
    [<EmitConstructor>] abstract Create: unit -> EquipSlot
    abstract from: papyrusObject: PapyrusObject option -> EquipSlot option

type [<AllowNullLiteral>] Explosion =
    inherit Form

type [<AllowNullLiteral>] ExplosionStatic =
    [<EmitConstructor>] abstract Create: unit -> Explosion
    abstract from: papyrusObject: PapyrusObject option -> Explosion option

type [<AllowNullLiteral>] Faction =
    inherit Form
    abstract canPayCrimeGold: unit -> bool
    abstract clearFactionFlag: flag: float -> unit
    abstract getBuySellList: unit -> FormList option
    abstract getCrimeGold: unit -> float
    abstract getCrimeGoldNonViolent: unit -> float
    abstract getCrimeGoldViolent: unit -> float
    abstract getInfamy: unit -> float
    abstract getInfamyNonViolent: unit -> float
    abstract getInfamyViolent: unit -> float
    abstract getMerchantContainer: unit -> ObjectReference option
    abstract getReaction: akOther: Faction option -> float
    abstract getStolenItemValueCrime: unit -> float
    abstract getStolenItemValueNoCrime: unit -> float
    abstract getVendorEndHour: unit -> float
    abstract getVendorRadius: unit -> float
    abstract getVendorStartHour: unit -> float
    abstract isFactionFlagSet: flag: float -> bool
    abstract isFactionInCrimeGroup: akOther: Faction option -> bool
    abstract isNotSellBuy: unit -> bool
    abstract isPlayerExpelled: unit -> bool
    abstract modCrimeGold: aiAmount: float * abViolent: bool -> unit
    abstract modReaction: akOther: Faction option * aiAmount: float -> unit
    abstract onlyBuysStolenItems: unit -> bool
    abstract playerPayCrimeGold: abRemoveStolenItems: bool * abGoToJail: bool -> unit
    abstract sendAssaultAlarm: unit -> unit
    abstract sendPlayerToJail: abRemoveInventory: bool * abRealJail: bool -> Promise<unit>
    abstract setAlly: akOther: Faction option * abSelfIsFriendToOther: bool * abOtherIsFriendToSelf: bool -> unit
    abstract setBuySellList: akList: FormList option -> unit
    abstract setCrimeGold: aiGold: float -> unit
    abstract setCrimeGoldViolent: aiGold: float -> unit
    abstract setEnemy: akOther: Faction option * abSelfIsNeutralToOther: bool * abOtherIsNeutralToSelf: bool -> unit
    abstract setFactionFlag: flag: float -> unit
    abstract setMerchantContainer: akContainer: ObjectReference option -> unit
    abstract setNotSellBuy: notSellBuy: bool -> unit
    abstract setOnlyBuysStolenItems: onlyStolen: bool -> unit
    abstract setPlayerEnemy: abIsEnemy: bool -> unit
    abstract setPlayerExpelled: abIsExpelled: bool -> unit
    abstract setReaction: akOther: Faction option * aiNewValue: float -> unit
    abstract setVendorEndHour: hour: float -> unit
    abstract setVendorRadius: radius: float -> unit
    abstract setVendorStartHour: hour: float -> unit

type [<AllowNullLiteral>] FactionStatic =
    [<EmitConstructor>] abstract Create: unit -> Faction
    abstract from: papyrusObject: PapyrusObject option -> Faction option

type [<AllowNullLiteral>] Flora =
    inherit Activator
    abstract getHarvestSound: unit -> SoundDescriptor option
    abstract getIngredient: unit -> Form option
    abstract setHarvestSound: akSoundDescriptor: SoundDescriptor option -> unit
    abstract setIngredient: akIngredient: Form option -> unit

type [<AllowNullLiteral>] FloraStatic =
    [<EmitConstructor>] abstract Create: unit -> Flora
    abstract from: papyrusObject: PapyrusObject option -> Flora option

type [<AllowNullLiteral>] FormList =
    inherit Form
    abstract addForm: apForm: Form option -> unit
    abstract addForms: forms: ResizeArray<PapyrusObject> option -> unit
    abstract find: apForm: Form option -> float
    abstract getAt: aiIndex: float -> Form option
    abstract getSize: unit -> float
    abstract hasForm: akForm: Form option -> bool
    abstract removeAddedForm: apForm: Form option -> unit
    abstract revert: unit -> unit
    abstract toArray: unit -> ResizeArray<PapyrusObject> option

type [<AllowNullLiteral>] FormListStatic =
    [<EmitConstructor>] abstract Create: unit -> FormList
    abstract from: papyrusObject: PapyrusObject option -> FormList option

type [<AllowNullLiteral>] Furniture =
    inherit Activator

type [<AllowNullLiteral>] FurnitureStatic =
    [<EmitConstructor>] abstract Create: unit -> Furniture
    abstract from: papyrusObject: PapyrusObject option -> Furniture option

type [<AllowNullLiteral>] Game =
    inherit PapyrusObject

type [<AllowNullLiteral>] GameStatic =
    [<EmitConstructor>] abstract Create: unit -> Game
    abstract from: papyrusObject: PapyrusObject option -> Game option
    abstract addAchievement: aiAchievementID: float -> unit
    abstract addHavokBallAndSocketConstraint: arRefA: ObjectReference option * arRefANode: string * arRefB: ObjectReference option * arRefBNode: string * afRefALocalOffsetX: float * afRefALocalOffsetY: float * afRefALocalOffsetZ: float * afRefBLocalOffsetX: float * afRefBLocalOffsetY: float * afRefBLocalOffsetZ: float -> Promise<bool>
    abstract addPerkPoints: aiPerkPoints: float -> unit
    abstract advanceSkill: asSkillName: string * afMagnitude: float -> unit
    abstract calculateFavorCost: aiFavorPrice: float -> float
    abstract clearPrison: unit -> unit
    abstract clearTempEffects: unit -> unit
    abstract disablePlayerControls: abMovement: bool * abFighting: bool * abCamSwitch: bool * abLooking: bool * abSneaking: bool * abMenu: bool * abActivate: bool * abJournalTabs: bool * aiDisablePOVType: float -> unit
    abstract enableFastTravel: abEnable: bool -> unit
    abstract enablePlayerControls: abMovement: bool * abFighting: bool * abCamSwitch: bool * abLooking: bool * abSneaking: bool * abMenu: bool * abActivate: bool * abJournalTabs: bool * aiDisablePOVType: float -> unit
    abstract fadeOutGame: abFadingOut: bool * abBlackFade: bool * afSecsBeforeFade: float * afFadeDuration: float -> unit
    abstract fastTravel: akDestination: ObjectReference option -> unit
    abstract findClosestActor: afX: float * afY: float * afZ: float * afRadius: float -> Actor option
    abstract findClosestReferenceOfAnyTypeInList: arBaseObjects: FormList option * afX: float * afY: float * afZ: float * afRadius: float -> ObjectReference option
    abstract findClosestReferenceOfType: arBaseObject: Form option * afX: float * afY: float * afZ: float * afRadius: float -> ObjectReference option
    abstract findRandomActor: afX: float * afY: float * afZ: float * afRadius: float -> Actor option
    abstract findRandomReferenceOfAnyTypeInList: arBaseObjects: FormList option * afX: float * afY: float * afZ: float * afRadius: float -> ObjectReference option
    abstract findRandomReferenceOfType: arBaseObject: Form option * afX: float * afY: float * afZ: float * afRadius: float -> ObjectReference option
    abstract forceFirstPerson: unit -> unit
    abstract forceThirdPerson: unit -> unit
    abstract getCameraState: unit -> float
    abstract getCurrentConsoleRef: unit -> ObjectReference option
    abstract getCurrentCrosshairRef: unit -> ObjectReference option
    abstract getDialogueTarget: unit -> ObjectReference option
    abstract getExperienceForLevel: currentLevel: float -> float
    abstract getForm: aiFormID: float -> Form option
    abstract getFormEx: formId: float -> Form option
    abstract getFormFromFile: aiFormID: float * asFilename: string -> Form option
    abstract getGameSettingFloat: asGameSetting: string -> float
    abstract getGameSettingInt: asGameSetting: string -> float
    abstract getGameSettingString: asGameSetting: string -> Promise<string>
    abstract getHotkeyBoundObject: hotkey: float -> Form option
    abstract getLightModAuthor: idx: float -> string
    abstract getLightModByName: name: string -> float
    abstract getLightModCount: unit -> float
    abstract getLightModDependencyCount: idx: float -> float
    abstract getLightModDescription: idx: float -> string
    abstract getLightModName: idx: float -> string
    abstract getModAuthor: modIndex: float -> string
    abstract getModByName: name: string -> float
    abstract getModCount: unit -> float
    abstract getModDependencyCount: modIndex: float -> float
    abstract getModDescription: modIndex: float -> string
    abstract getModName: modIndex: float -> string
    abstract getNthLightModDependency: modIdx: float * idx: float -> float
    abstract getNthTintMaskColor: n: float -> float
    abstract getNthTintMaskTexturePath: n: float -> string
    abstract getNthTintMaskType: n: float -> float
    abstract getNumTintMasks: unit -> float
    abstract getNumTintsByType: ``type``: float -> float
    abstract getPerkPoints: unit -> float
    abstract getPlayerExperience: unit -> float
    abstract getPlayerGrabbedRef: unit -> ObjectReference option
    abstract getPlayerMovementMode: unit -> bool
    abstract getPlayersLastRiddenHorse: unit -> Actor option
    abstract getRealHoursPassed: unit -> float
    abstract getSunPositionX: unit -> float
    abstract getSunPositionY: unit -> float
    abstract getSunPositionZ: unit -> float
    abstract getTintMaskColor: ``type``: float * index: float -> float
    abstract getTintMaskTexturePath: ``type``: float * index: float -> string
    abstract hideTitleSequenceMenu: unit -> unit
    abstract incrementSkill: asSkillName: string -> unit
    abstract incrementSkillBy: asSkillName: string * aiCount: float -> unit
    abstract incrementStat: asStatName: string * aiModAmount: float -> unit
    abstract isActivateControlsEnabled: unit -> bool
    abstract isCamSwitchControlsEnabled: unit -> bool
    abstract isFastTravelControlsEnabled: unit -> bool
    abstract isFastTravelEnabled: unit -> bool
    abstract isFightingControlsEnabled: unit -> bool
    abstract isJournalControlsEnabled: unit -> bool
    abstract isLookingControlsEnabled: unit -> bool
    abstract isMenuControlsEnabled: unit -> bool
    abstract isMovementControlsEnabled: unit -> bool
    abstract isObjectFavorited: Form: Form option -> bool
    abstract isPlayerSungazing: unit -> bool
    abstract isPluginInstalled: name: string -> bool
    abstract isSneakingControlsEnabled: unit -> bool
    abstract isWordUnlocked: akWord: WordOfPower option -> bool
    abstract loadGame: name: string -> unit
    abstract modPerkPoints: perkPoints: float -> unit
    abstract playBink: asFilename: string * abInterruptible: bool * abMuteAudio: bool * abMuteMusic: bool * abLetterbox: bool -> unit
    abstract precacheCharGen: unit -> unit
    abstract precacheCharGenClear: unit -> unit
    abstract queryStat: asStat: string -> float
    abstract quitToMainMenu: unit -> unit
    abstract removeHavokConstraints: arFirstRef: ObjectReference option * arFirstRefNodeName: string * arSecondRef: ObjectReference option * arSecondRefNodeName: string -> Promise<bool>
    abstract requestAutosave: unit -> unit
    abstract requestModel: asModelName: string -> unit
    abstract requestSave: unit -> unit
    abstract saveGame: name: string -> unit
    abstract sendWereWolfTransformation: unit -> unit
    abstract serveTime: unit -> unit
    abstract setAllowFlyingMountLandingRequests: abAllow: bool -> unit
    abstract setBeastForm: abEntering: bool -> unit
    abstract setCameraTarget: arTarget: Actor option -> unit
    abstract setGameSettingBool: setting: string * value: bool -> unit
    abstract setGameSettingFloat: setting: string * value: float -> unit
    abstract setGameSettingInt: setting: string * value: float -> unit
    abstract setGameSettingString: setting: string * value: string -> unit
    abstract setHudCartMode: abSetCartMode: bool -> unit
    abstract setInChargen: abDisableSaving: bool * abDisableWaiting: bool * abShowControlsDisabledMessage: bool -> unit
    abstract setMiscStat: name: string * value: float -> unit
    abstract setNthTintMaskColor: n: float * color: float -> unit
    abstract setNthTintMaskTexturePath: path: string * n: float -> unit
    abstract setPerkPoints: perkPoints: float -> unit
    abstract setPlayerAIDriven: abAIDriven: bool -> unit
    abstract setPlayerExperience: exp: float -> unit
    abstract setPlayerLevel: level: float -> unit
    abstract setPlayerReportCrime: abReportCrime: bool -> unit
    abstract setPlayersLastRiddenHorse: horse: Actor option -> unit
    abstract setSittingRotation: afValue: float -> unit
    abstract setSunGazeImageSpaceModifier: apImod: ImageSpaceModifier option -> unit
    abstract setTintMaskColor: color: float * ``type``: float * index: float -> unit
    abstract setTintMaskTexturePath: path: string * ``type``: float * index: float -> unit
    abstract showFirstPersonGeometry: abShow: bool -> unit
    abstract showLimitedRaceMenu: unit -> unit
    abstract showRaceMenu: unit -> unit
    abstract showTitleSequenceMenu: unit -> unit
    abstract showTrainingMenu: aTrainer: Actor option -> unit
    abstract startTitleSequence: asSequenceName: string -> unit
    abstract teachWord: akWord: WordOfPower option -> unit
    abstract triggerScreenBlood: aiValue: float -> unit
    abstract unbindObjectHotkey: hotkey: float -> unit
    abstract unlockWord: akWord: WordOfPower option -> unit
    abstract updateHairColor: unit -> unit
    abstract updateThirdPerson: unit -> unit
    abstract updateTintMaskColors: unit -> unit
    abstract usingGamepad: unit -> bool
    abstract getPlayer: unit -> Actor
    abstract shakeCamera: akSource: ObjectReference option * afStrength: float * afDuration: float -> unit
    abstract shakeController: afSmallMotorStrength: float * afBigMotorStreangth: float * afDuration: float -> unit

type [<AllowNullLiteral>] GlobalVariable =
    inherit Form
    abstract getValue: unit -> float
    abstract setValue: param1: float -> unit

type [<AllowNullLiteral>] GlobalVariableStatic =
    [<EmitConstructor>] abstract Create: unit -> GlobalVariable
    abstract from: papyrusObject: PapyrusObject option -> GlobalVariable option

type [<AllowNullLiteral>] Hazard =
    inherit Form

type [<AllowNullLiteral>] HazardStatic =
    [<EmitConstructor>] abstract Create: unit -> Hazard
    abstract from: papyrusObject: PapyrusObject option -> Hazard option

type [<AllowNullLiteral>] HeadPart =
    inherit Form
    abstract getIndexOfExtraPart: p: HeadPart option -> float
    abstract getNthExtraPart: n: float -> HeadPart option
    abstract getNumExtraParts: unit -> float
    abstract getPartName: unit -> string
    abstract getType: unit -> float
    abstract getValidRaces: unit -> FormList option
    abstract hasExtraPart: p: HeadPart option -> bool
    abstract isExtraPart: unit -> bool
    abstract setValidRaces: vRaces: FormList option -> unit

type [<AllowNullLiteral>] HeadPartStatic =
    [<EmitConstructor>] abstract Create: unit -> HeadPart
    abstract from: papyrusObject: PapyrusObject option -> HeadPart option
    abstract getHeadPart: name: string -> HeadPart option

type [<AllowNullLiteral>] Idle =
    inherit Form

type [<AllowNullLiteral>] IdleStatic =
    [<EmitConstructor>] abstract Create: unit -> Idle
    abstract from: papyrusObject: PapyrusObject option -> Idle option

type [<AllowNullLiteral>] ImageSpaceModifier =
    inherit Form
    abstract apply: param1: float -> unit
    abstract applyCrossFade: param1: float -> unit
    abstract popTo: param1: ImageSpaceModifier option * param2: float -> unit
    abstract remove: unit -> unit

type [<AllowNullLiteral>] ImageSpaceModifierStatic =
    [<EmitConstructor>] abstract Create: unit -> ImageSpaceModifier
    abstract from: papyrusObject: PapyrusObject option -> ImageSpaceModifier option
    abstract removeCrossFade: param1: float -> unit

type [<AllowNullLiteral>] ImpactDataSet =
    inherit Form

type [<AllowNullLiteral>] ImpactDataSetStatic =
    [<EmitConstructor>] abstract Create: unit -> ImpactDataSet
    abstract from: papyrusObject: PapyrusObject option -> ImpactDataSet option

type [<AllowNullLiteral>] Ingredient =
    inherit Form
    abstract getCostliestEffectIndex: unit -> float
    abstract getEffectAreas: unit -> ResizeArray<float> option
    abstract getEffectDurations: unit -> ResizeArray<float> option
    abstract getEffectMagnitudes: unit -> ResizeArray<float> option
    abstract getIsNthEffectKnown: index: float -> bool
    abstract getMagicEffects: unit -> ResizeArray<PapyrusObject> option
    abstract getNthEffectArea: index: float -> float
    abstract getNthEffectDuration: index: float -> float
    abstract getNthEffectMagicEffect: index: float -> MagicEffect option
    abstract getNthEffectMagnitude: index: float -> float
    abstract getNumEffects: unit -> float
    abstract isHostile: unit -> bool
    abstract learnAllEffects: unit -> unit
    abstract learnEffect: aiIndex: float -> unit
    abstract learnNextEffect: unit -> float
    abstract setNthEffectArea: index: float * value: float -> unit
    abstract setNthEffectDuration: index: float * value: float -> unit
    abstract setNthEffectMagnitude: index: float * value: float -> unit

type [<AllowNullLiteral>] IngredientStatic =
    [<EmitConstructor>] abstract Create: unit -> Ingredient
    abstract from: papyrusObject: PapyrusObject option -> Ingredient option

type [<AllowNullLiteral>] Input =
    inherit PapyrusObject

type [<AllowNullLiteral>] InputStatic =
    [<EmitConstructor>] abstract Create: unit -> Input
    abstract from: papyrusObject: PapyrusObject option -> Input option
    abstract getMappedControl: keycode: float -> string
    abstract getMappedKey: control: string * deviceType: float -> float
    abstract getNthKeyPressed: n: float -> float
    abstract getNumKeysPressed: unit -> float
    abstract holdKey: dxKeycode: float -> unit
    abstract isKeyPressed: dxKeycode: float -> bool
    abstract releaseKey: dxKeycode: float -> unit
    abstract tapKey: dxKeycode: float -> unit

type [<AllowNullLiteral>] Key =
    inherit MiscObject

type [<AllowNullLiteral>] KeyStatic =
    [<EmitConstructor>] abstract Create: unit -> Key
    abstract from: papyrusObject: PapyrusObject option -> Key option

type [<AllowNullLiteral>] Keyword =
    inherit Form
    abstract getString: unit -> string
    abstract sendStoryEvent: akLoc: Location option * akRef1: ObjectReference option * akRef2: ObjectReference option * aiValue1: float * aiValue2: float -> unit
    abstract sendStoryEventAndWait: akLoc: Location option * akRef1: ObjectReference option * akRef2: ObjectReference option * aiValue1: float * aiValue2: float -> Promise<bool>

type [<AllowNullLiteral>] KeywordStatic =
    [<EmitConstructor>] abstract Create: unit -> Keyword
    abstract from: papyrusObject: PapyrusObject option -> Keyword option
    abstract getKeyword: key: string -> Keyword option

type [<AllowNullLiteral>] LeveledActor =
    inherit Form
    abstract addForm: apForm: Form option * aiLevel: float -> unit
    abstract getNthCount: n: float -> float
    abstract getNthForm: n: float -> Form option
    abstract getNthLevel: n: float -> float
    abstract getNumForms: unit -> float
    abstract revert: unit -> unit
    abstract setNthCount: n: float * count: float -> unit
    abstract setNthLevel: n: float * level: float -> unit

type [<AllowNullLiteral>] LeveledActorStatic =
    [<EmitConstructor>] abstract Create: unit -> LeveledActor
    abstract from: papyrusObject: PapyrusObject option -> LeveledActor option

type [<AllowNullLiteral>] LeveledItem =
    inherit Form
    abstract addForm: apForm: Form option * aiLevel: float * aiCount: float -> unit
    abstract getChanceGlobal: unit -> GlobalVariable option
    abstract getChanceNone: unit -> float
    abstract getNthCount: n: float -> float
    abstract getNthForm: n: float -> Form option
    abstract getNthLevel: n: float -> float
    abstract getNumForms: unit -> float
    abstract revert: unit -> unit
    abstract setChanceGlobal: glob: GlobalVariable option -> unit
    abstract setChanceNone: chance: float -> unit
    abstract setNthCount: n: float * count: float -> unit
    abstract setNthLevel: n: float * level: float -> unit

type [<AllowNullLiteral>] LeveledItemStatic =
    [<EmitConstructor>] abstract Create: unit -> LeveledItem
    abstract from: papyrusObject: PapyrusObject option -> LeveledItem option

type [<AllowNullLiteral>] LeveledSpell =
    inherit Form
    abstract addForm: apForm: Form option * aiLevel: float -> unit
    abstract getChanceNone: unit -> float
    abstract getNthForm: n: float -> Form option
    abstract getNthLevel: n: float -> float
    abstract getNumForms: unit -> float
    abstract revert: unit -> unit
    abstract setChanceNone: chance: float -> unit
    abstract setNthLevel: n: float * level: float -> unit

type [<AllowNullLiteral>] LeveledSpellStatic =
    [<EmitConstructor>] abstract Create: unit -> LeveledSpell
    abstract from: papyrusObject: PapyrusObject option -> LeveledSpell option

type [<AllowNullLiteral>] Light =
    inherit Form
    abstract getWarmthRating: unit -> float

type [<AllowNullLiteral>] LightStatic =
    [<EmitConstructor>] abstract Create: unit -> Light
    abstract from: papyrusObject: PapyrusObject option -> Light option

type [<AllowNullLiteral>] Location =
    inherit Form
    abstract getKeywordData: param1: Keyword option -> float
    abstract getRefTypeAliveCount: param1: LocationRefType option -> float
    abstract getRefTypeDeadCount: param1: LocationRefType option -> float
    abstract hasCommonParent: param1: Location option * param2: Keyword option -> bool
    abstract hasRefType: param1: LocationRefType option -> bool
    abstract isChild: param1: Location option -> bool
    abstract isCleared: unit -> bool
    abstract isLoaded: unit -> bool
    abstract setCleared: param1: bool -> unit
    abstract setKeywordData: param1: Keyword option * param2: float -> unit

type [<AllowNullLiteral>] LocationStatic =
    [<EmitConstructor>] abstract Create: unit -> Location
    abstract from: papyrusObject: PapyrusObject option -> Location option

type [<AllowNullLiteral>] LocationAlias =
    inherit Alias
    abstract clear: unit -> unit
    abstract forceLocationTo: param1: Location option -> unit
    abstract getLocation: unit -> Location option

type [<AllowNullLiteral>] LocationAliasStatic =
    [<EmitConstructor>] abstract Create: unit -> LocationAlias
    abstract from: papyrusObject: PapyrusObject option -> LocationAlias option

type [<AllowNullLiteral>] LocationRefType =
    inherit Keyword

type [<AllowNullLiteral>] LocationRefTypeStatic =
    [<EmitConstructor>] abstract Create: unit -> LocationRefType
    abstract from: papyrusObject: PapyrusObject option -> LocationRefType option

type [<AllowNullLiteral>] MagicEffect =
    inherit Form
    abstract clearEffectFlag: flag: float -> unit
    abstract getArea: unit -> float
    abstract getAssociatedSkill: unit -> Promise<string>
    abstract getBaseCost: unit -> float
    abstract getCastTime: unit -> float
    abstract getCastingArt: unit -> Art option
    abstract getCastingType: unit -> float
    abstract getDeliveryType: unit -> float
    abstract getEnchantArt: unit -> Art option
    abstract getEnchantShader: unit -> EffectShader option
    abstract getEquipAbility: unit -> Spell option
    abstract getExplosion: unit -> Explosion option
    abstract getHitEffectArt: unit -> Art option
    abstract getHitShader: unit -> EffectShader option
    abstract getImageSpaceMod: unit -> ImageSpaceModifier option
    abstract getImpactDataSet: unit -> ImpactDataSet option
    abstract getLight: unit -> Light option
    abstract getPerk: unit -> Perk option
    abstract getProjectile: unit -> Projectile option
    abstract getResistance: unit -> string
    abstract getSkillLevel: unit -> float
    abstract getSkillUsageMult: unit -> float
    abstract getSounds: unit -> ResizeArray<PapyrusObject> option
    abstract isEffectFlagSet: flag: float -> bool
    abstract setArea: area: float -> unit
    abstract setAssociatedSkill: skill: string -> unit
    abstract setBaseCost: cost: float -> unit
    abstract setCastTime: castTime: float -> unit
    abstract setCastingArt: obj: Art option -> unit
    abstract setEffectFlag: flag: float -> unit
    abstract setEnchantArt: obj: Art option -> unit
    abstract setEnchantShader: obj: EffectShader option -> unit
    abstract setEquipAbility: obj: Spell option -> unit
    abstract setExplosion: obj: Explosion option -> unit
    abstract setHitEffectArt: obj: Art option -> unit
    abstract setHitShader: obj: EffectShader option -> unit
    abstract setImageSpaceMod: obj: ImageSpaceModifier option -> unit
    abstract setImpactDataSet: obj: ImpactDataSet option -> unit
    abstract setLight: obj: Light option -> unit
    abstract setPerk: obj: Perk option -> unit
    abstract setProjectile: obj: Projectile option -> unit
    abstract setResistance: skill: string -> unit
    abstract setSkillLevel: level: float -> unit
    abstract setSkillUsageMult: usageMult: float -> unit

type [<AllowNullLiteral>] MagicEffectStatic =
    [<EmitConstructor>] abstract Create: unit -> MagicEffect
    abstract from: papyrusObject: PapyrusObject option -> MagicEffect option

type [<AllowNullLiteral>] Message =
    inherit Form
    abstract show: param1: float * param2: float * param3: float * param4: float * param5: float * param6: float * param7: float * param8: float * param9: float -> Promise<float>
    abstract showAsHelpMessage: param1: string * param2: float * param3: float * param4: float -> unit

type [<AllowNullLiteral>] MessageStatic =
    [<EmitConstructor>] abstract Create: unit -> Message
    abstract from: papyrusObject: PapyrusObject option -> Message option
    abstract resetHelpMessage: param1: string -> unit

type [<AllowNullLiteral>] MusicType =
    inherit Form
    abstract add: unit -> unit
    abstract remove: unit -> unit

type [<AllowNullLiteral>] MusicTypeStatic =
    [<EmitConstructor>] abstract Create: unit -> MusicType
    abstract from: papyrusObject: PapyrusObject option -> MusicType option

type [<AllowNullLiteral>] NetImmerse =
    inherit PapyrusObject

type [<AllowNullLiteral>] NetImmerseStatic =
    [<EmitConstructor>] abstract Create: unit -> NetImmerse
    abstract from: papyrusObject: PapyrusObject option -> NetImmerse option
    abstract getNodeLocalPosition: ref: ObjectReference option * node: string * _in: ResizeArray<float> option * firstPerson: bool -> bool
    abstract getNodeLocalPositionX: ref: ObjectReference option * node: string * firstPerson: bool -> float
    abstract getNodeLocalPositionY: ref: ObjectReference option * node: string * firstPerson: bool -> float
    abstract getNodeLocalPositionZ: ref: ObjectReference option * node: string * firstPerson: bool -> float
    abstract getNodeLocalRotationEuler: ref: ObjectReference option * node: string * _in: ResizeArray<float> option * firstPerson: bool -> bool
    abstract getNodeLocalRotationMatrix: ref: ObjectReference option * node: string * _in: ResizeArray<float> option * firstPerson: bool -> bool
    abstract getNodeScale: ref: ObjectReference option * node: string * firstPerson: bool -> float
    abstract getNodeWorldPosition: ref: ObjectReference option * node: string * _in: ResizeArray<float> option * firstPerson: bool -> bool
    abstract getNodeWorldPositionX: ref: ObjectReference option * node: string * firstPerson: bool -> float
    abstract getNodeWorldPositionY: ref: ObjectReference option * node: string * firstPerson: bool -> float
    abstract getNodeWorldPositionZ: ref: ObjectReference option * node: string * firstPerson: bool -> float
    abstract getNodeWorldRotationEuler: ref: ObjectReference option * node: string * _in: ResizeArray<float> option * firstPerson: bool -> bool
    abstract getNodeWorldRotationMatrix: ref: ObjectReference option * node: string * _in: ResizeArray<float> option * firstPerson: bool -> bool
    abstract getRelativeNodePosition: ref: ObjectReference option * nodeA: string * nodeB: string * _in: ResizeArray<float> option * firstPerson: bool -> bool
    abstract getRelativeNodePositionX: ref: ObjectReference option * nodeA: string * nodeB: string * firstPerson: bool -> float
    abstract getRelativeNodePositionY: ref: ObjectReference option * nodeA: string * nodeB: string * firstPerson: bool -> float
    abstract getRelativeNodePositionZ: ref: ObjectReference option * nodeA: string * nodeB: string * firstPerson: bool -> float
    abstract hasNode: ref: ObjectReference option * node: string * firstPerson: bool -> bool
    abstract setNodeLocalPosition: ref: ObjectReference option * node: string * _in: ResizeArray<float> option * firstPerson: bool -> bool
    abstract setNodeLocalPositionX: ref: ObjectReference option * node: string * x: float * firstPerson: bool -> unit
    abstract setNodeLocalPositionY: ref: ObjectReference option * node: string * y: float * firstPerson: bool -> unit
    abstract setNodeLocalPositionZ: ref: ObjectReference option * node: string * z: float * firstPerson: bool -> unit
    abstract setNodeLocalRotationEuler: ref: ObjectReference option * node: string * _in: ResizeArray<float> option * firstPerson: bool -> bool
    abstract setNodeLocalRotationMatrix: ref: ObjectReference option * node: string * _in: ResizeArray<float> option * firstPerson: bool -> bool
    abstract setNodeScale: ref: ObjectReference option * node: string * scale: float * firstPerson: bool -> unit
    abstract setNodeTextureSet: ref: ObjectReference option * node: string * tSet: TextureSet option * firstPerson: bool -> unit

type [<AllowNullLiteral>] Outfit =
    inherit Form
    abstract getNthPart: n: float -> Form option
    abstract getNumParts: unit -> float

type [<AllowNullLiteral>] OutfitStatic =
    [<EmitConstructor>] abstract Create: unit -> Outfit
    abstract from: papyrusObject: PapyrusObject option -> Outfit option

type [<AllowNullLiteral>] Projectile =
    inherit Form

type [<AllowNullLiteral>] ProjectileStatic =
    [<EmitConstructor>] abstract Create: unit -> Projectile
    abstract from: papyrusObject: PapyrusObject option -> Projectile option

type [<AllowNullLiteral>] Package =
    inherit Form
    abstract getOwningQuest: unit -> Quest option
    abstract getTemplate: unit -> Package option

type [<AllowNullLiteral>] PackageStatic =
    [<EmitConstructor>] abstract Create: unit -> Package
    abstract from: papyrusObject: PapyrusObject option -> Package option

type [<AllowNullLiteral>] Perk =
    inherit Form
    abstract getNextPerk: unit -> Perk option
    abstract getNthEntryLeveledList: n: float -> LeveledItem option
    abstract getNthEntryPriority: n: float -> float
    abstract getNthEntryQuest: n: float -> Quest option
    abstract getNthEntryRank: n: float -> float
    abstract getNthEntrySpell: n: float -> Spell option
    abstract getNthEntryStage: n: float -> float
    abstract getNthEntryText: n: float -> string
    abstract getNthEntryValue: n: float * i: float -> float
    abstract getNumEntries: unit -> float
    abstract setNthEntryLeveledList: n: float * lList: LeveledItem option -> bool
    abstract setNthEntryPriority: n: float * priority: float -> bool
    abstract setNthEntryQuest: n: float * newQuest: Quest option -> bool
    abstract setNthEntryRank: n: float * rank: float -> bool
    abstract setNthEntrySpell: n: float * newSpell: Spell option -> bool
    abstract setNthEntryStage: n: float * stage: float -> bool
    abstract setNthEntryText: n: float * newText: string -> bool
    abstract setNthEntryValue: n: float * i: float * value: float -> bool

type [<AllowNullLiteral>] PerkStatic =
    [<EmitConstructor>] abstract Create: unit -> Perk
    abstract from: papyrusObject: PapyrusObject option -> Perk option

type [<AllowNullLiteral>] Potion =
    inherit Form
    abstract getCostliestEffectIndex: unit -> float
    abstract getEffectAreas: unit -> ResizeArray<float> option
    abstract getEffectDurations: unit -> ResizeArray<float> option
    abstract getEffectMagnitudes: unit -> ResizeArray<float> option
    abstract getMagicEffects: unit -> ResizeArray<PapyrusObject> option
    abstract getNthEffectArea: index: float -> float
    abstract getNthEffectDuration: index: float -> float
    abstract getNthEffectMagicEffect: index: float -> MagicEffect option
    abstract getNthEffectMagnitude: index: float -> float
    abstract getNumEffects: unit -> float
    abstract getUseSound: unit -> SoundDescriptor option
    abstract isFood: unit -> bool
    abstract isHostile: unit -> bool
    abstract isPoison: unit -> bool
    abstract setNthEffectArea: index: float * value: float -> unit
    abstract setNthEffectDuration: index: float * value: float -> unit
    abstract setNthEffectMagnitude: index: float * value: float -> unit

type [<AllowNullLiteral>] PotionStatic =
    [<EmitConstructor>] abstract Create: unit -> Potion
    abstract from: papyrusObject: PapyrusObject option -> Potion option

type [<AllowNullLiteral>] Quest =
    inherit Form
    abstract completeAllObjectives: unit -> unit
    abstract completeQuest: unit -> unit
    abstract failAllObjectives: unit -> unit
    abstract getAlias: aiAliasID: float -> Alias option
    abstract getAliasById: aliasId: float -> Alias option
    abstract getAliasByName: name: string -> Alias option
    abstract getAliases: unit -> ResizeArray<PapyrusObject> option
    abstract getCurrentStageID: unit -> float
    abstract getID: unit -> string
    abstract getNthAlias: index: float -> Alias option
    abstract getNumAliases: unit -> float
    abstract getPriority: unit -> float
    abstract isActive: unit -> bool
    abstract isCompleted: unit -> bool
    abstract isObjectiveCompleted: aiObjective: float -> bool
    abstract isObjectiveDisplayed: aiObjective: float -> bool
    abstract isObjectiveFailed: aiObjective: float -> bool
    abstract isRunning: unit -> bool
    abstract isStageDone: aiStage: float -> bool
    abstract isStarting: unit -> bool
    abstract isStopped: unit -> bool
    abstract isStopping: unit -> bool
    abstract reset: unit -> unit
    abstract setActive: abActive: bool -> unit
    abstract setCurrentStageID: aiStageID: float -> Promise<bool>
    abstract setObjectiveCompleted: aiObjective: float * abCompleted: bool -> unit
    abstract setObjectiveDisplayed: aiObjective: float * abDisplayed: bool * abForce: bool -> unit
    abstract setObjectiveFailed: aiObjective: float * abFailed: bool -> unit
    abstract start: unit -> Promise<bool>
    abstract stop: unit -> unit
    abstract updateCurrentInstanceGlobal: aUpdateGlobal: GlobalVariable option -> bool

type [<AllowNullLiteral>] QuestStatic =
    [<EmitConstructor>] abstract Create: unit -> Quest
    abstract from: papyrusObject: PapyrusObject option -> Quest option
    abstract getQuest: editorId: string -> Quest option

type [<AllowNullLiteral>] Race =
    inherit Form
    abstract clearRaceFlag: n: float -> unit
    abstract getDefaultVoiceType: female: bool -> VoiceType option
    abstract getNthSpell: n: float -> Spell option
    abstract getSkin: unit -> Armor option
    abstract getSpellCount: unit -> float
    abstract isRaceFlagSet: n: float -> bool
    abstract setDefaultVoiceType: female: bool * voice: VoiceType option -> unit
    abstract setRaceFlag: n: float -> unit
    abstract setSkin: skin: Armor option -> unit

type [<AllowNullLiteral>] RaceStatic =
    [<EmitConstructor>] abstract Create: unit -> Race
    abstract from: papyrusObject: PapyrusObject option -> Race option
    abstract getNthPlayableRace: n: float -> Race option
    abstract getNumPlayableRaces: unit -> float
    abstract getRace: editorId: string -> Race option

type [<AllowNullLiteral>] ReferenceAlias =
    inherit Alias
    abstract addInventoryEventFilter: param1: Form option -> unit
    abstract clear: unit -> unit
    abstract forceRefTo: param1: ObjectReference option -> unit
    abstract getReference: unit -> ObjectReference option
    abstract removeAllInventoryEventFilters: unit -> unit
    abstract removeInventoryEventFilter: param1: Form option -> unit

type [<AllowNullLiteral>] ReferenceAliasStatic =
    [<EmitConstructor>] abstract Create: unit -> ReferenceAlias
    abstract from: papyrusObject: PapyrusObject option -> ReferenceAlias option

type [<AllowNullLiteral>] Spell =
    inherit Form
    abstract cast: akSource: ObjectReference option * akTarget: ObjectReference option -> Promise<unit>
    abstract getCastTime: unit -> float
    abstract getCostliestEffectIndex: unit -> float
    abstract getEffectAreas: unit -> ResizeArray<float> option
    abstract getEffectDurations: unit -> ResizeArray<float> option
    abstract getEffectMagnitudes: unit -> ResizeArray<float> option
    abstract getEffectiveMagickaCost: caster: Actor option -> float
    abstract getEquipType: unit -> EquipSlot option
    abstract getMagicEffects: unit -> ResizeArray<PapyrusObject> option
    abstract getMagickaCost: unit -> float
    abstract getNthEffectArea: index: float -> float
    abstract getNthEffectDuration: index: float -> float
    abstract getNthEffectMagicEffect: index: float -> MagicEffect option
    abstract getNthEffectMagnitude: index: float -> float
    abstract getNumEffects: unit -> float
    abstract getPerk: unit -> Perk option
    abstract isHostile: unit -> bool
    abstract preload: unit -> unit
    abstract remoteCast: akSource: ObjectReference option * akBlameActor: Actor option * akTarget: ObjectReference option -> Promise<unit>
    abstract setEquipType: ``type``: EquipSlot option -> unit
    abstract setNthEffectArea: index: float * value: float -> unit
    abstract setNthEffectDuration: index: float * value: float -> unit
    abstract setNthEffectMagnitude: index: float * value: float -> unit
    abstract unload: unit -> unit

type [<AllowNullLiteral>] SpellStatic =
    [<EmitConstructor>] abstract Create: unit -> Spell
    abstract from: papyrusObject: PapyrusObject option -> Spell option

type [<AllowNullLiteral>] Static =
    inherit Form

type [<AllowNullLiteral>] StaticStatic =
    [<EmitConstructor>] abstract Create: unit -> Static
    abstract from: papyrusObject: PapyrusObject option -> Static option

type [<AllowNullLiteral>] Scene =
    inherit Form
    abstract forceStart: unit -> unit
    abstract getOwningQuest: unit -> Quest option
    abstract isActionComplete: param1: float -> bool
    abstract isPlaying: unit -> bool
    abstract start: unit -> unit
    abstract stop: unit -> unit

type [<AllowNullLiteral>] SceneStatic =
    [<EmitConstructor>] abstract Create: unit -> Scene
    abstract from: papyrusObject: PapyrusObject option -> Scene option

type [<AllowNullLiteral>] Scroll =
    inherit Form
    abstract cast: akSource: ObjectReference option * akTarget: ObjectReference option -> Promise<unit>
    abstract getCastTime: unit -> float
    abstract getCostliestEffectIndex: unit -> float
    abstract getEffectAreas: unit -> ResizeArray<float> option
    abstract getEffectDurations: unit -> ResizeArray<float> option
    abstract getEffectMagnitudes: unit -> ResizeArray<float> option
    abstract getEquipType: unit -> EquipSlot option
    abstract getMagicEffects: unit -> ResizeArray<PapyrusObject> option
    abstract getNthEffectArea: index: float -> float
    abstract getNthEffectDuration: index: float -> float
    abstract getNthEffectMagicEffect: index: float -> MagicEffect option
    abstract getNthEffectMagnitude: index: float -> float
    abstract getNumEffects: unit -> float
    abstract getPerk: unit -> Perk option
    abstract setEquipType: ``type``: EquipSlot option -> unit
    abstract setNthEffectArea: index: float * value: float -> unit
    abstract setNthEffectDuration: index: float * value: float -> unit
    abstract setNthEffectMagnitude: index: float * value: float -> unit

type [<AllowNullLiteral>] ScrollStatic =
    [<EmitConstructor>] abstract Create: unit -> Scroll
    abstract from: papyrusObject: PapyrusObject option -> Scroll option

type [<AllowNullLiteral>] ShaderParticleGeometry =
    inherit Form
    abstract apply: param1: float -> unit
    abstract remove: param1: float -> unit

type [<AllowNullLiteral>] ShaderParticleGeometryStatic =
    [<EmitConstructor>] abstract Create: unit -> ShaderParticleGeometry
    abstract from: papyrusObject: PapyrusObject option -> ShaderParticleGeometry option

type [<AllowNullLiteral>] Shout =
    inherit Form
    abstract getNthRecoveryTime: n: float -> float
    abstract getNthSpell: n: float -> Spell option
    abstract getNthWordOfPower: n: float -> WordOfPower option
    abstract setNthRecoveryTime: n: float * time: float -> unit
    abstract setNthSpell: n: float * aSpell: Spell option -> unit
    abstract setNthWordOfPower: n: float * aWoop: WordOfPower option -> unit

type [<AllowNullLiteral>] ShoutStatic =
    [<EmitConstructor>] abstract Create: unit -> Shout
    abstract from: papyrusObject: PapyrusObject option -> Shout option

type [<AllowNullLiteral>] SoulGem =
    inherit MiscObject
    abstract getGemSize: unit -> float
    abstract getSoulSize: unit -> float

type [<AllowNullLiteral>] SoulGemStatic =
    [<EmitConstructor>] abstract Create: unit -> SoulGem
    abstract from: papyrusObject: PapyrusObject option -> SoulGem option

type [<AllowNullLiteral>] Sound =
    inherit Form
    abstract getDescriptor: unit -> SoundDescriptor option
    abstract play: akSource: ObjectReference option -> float
    abstract playAndWait: akSource: ObjectReference option -> Promise<bool>

type [<AllowNullLiteral>] SoundStatic =
    [<EmitConstructor>] abstract Create: unit -> Sound
    abstract from: papyrusObject: PapyrusObject option -> Sound option
    abstract setInstanceVolume: aiPlaybackInstance: float * afVolume: float -> unit
    abstract stopInstance: aiPlaybackInstance: float -> unit

type [<AllowNullLiteral>] SoundCategory =
    inherit Form
    abstract mute: unit -> unit
    abstract pause: unit -> unit
    abstract setFrequency: param1: float -> unit
    abstract setVolume: param1: float -> unit
    abstract unMute: unit -> unit
    abstract unPause: unit -> unit

type [<AllowNullLiteral>] SoundCategoryStatic =
    [<EmitConstructor>] abstract Create: unit -> SoundCategory
    abstract from: papyrusObject: PapyrusObject option -> SoundCategory option

type [<AllowNullLiteral>] SoundDescriptor =
    inherit Form
    abstract getDecibelAttenuation: unit -> float
    abstract getDecibelVariance: unit -> float
    abstract getFrequencyShift: unit -> float
    abstract getFrequencyVariance: unit -> float
    abstract setDecibelAttenuation: dbAttenuation: float -> unit
    abstract setDecibelVariance: dbVariance: float -> unit
    abstract setFrequencyShift: frequencyShift: float -> unit
    abstract setFrequencyVariance: frequencyVariance: float -> unit

type [<AllowNullLiteral>] SoundDescriptorStatic =
    [<EmitConstructor>] abstract Create: unit -> SoundDescriptor
    abstract from: papyrusObject: PapyrusObject option -> SoundDescriptor option

type [<AllowNullLiteral>] TESModPlatform =
    inherit PapyrusObject

type [<AllowNullLiteral>] TESModPlatformStatic =
    [<EmitConstructor>] abstract Create: unit -> TESModPlatform
    abstract from: papyrusObject: PapyrusObject option -> TESModPlatform option
    abstract addItemEx: containerRefr: ObjectReference option * item: Form option * countDelta: float * health: float * enchantment: Enchantment option * maxCharge: float * removeEnchantmentOnUnequip: bool * chargePercent: float * textDisplayData: string * soul: float * poison: Potion option * poisonCount: float -> unit
    abstract clearTintMasks: targetActor: Actor option -> unit
    abstract createNpc: unit -> ActorBase option
    abstract getNthVtableElement: pointer: Form option * pointerOffset: float * elementIndex: float -> float
    abstract getSkinColor: ``base``: ActorBase option -> ColorForm option
    abstract isPlayerRunningEnabled: unit -> bool
    abstract moveRefrToPosition: refr: ObjectReference option * cell: Cell option * world: WorldSpace option * posX: float * posY: float * posZ: float * rotX: float * rotY: float * rotZ: float -> unit
    abstract pushTintMask: targetActor: Actor option * ``type``: float * argb: float * texturePath: string -> unit
    abstract pushWornState: worn: bool * wornLeft: bool -> unit
    abstract resetContainer: container: Form option -> unit
    abstract resizeHeadpartsArray: npc: ActorBase option * newSize: float -> unit
    abstract resizeTintsArray: newSize: float -> unit
    abstract setFormIdUnsafe: Form: Form option * newId: float -> unit
    abstract setNpcHairColor: npc: ActorBase option * hairColor: float -> unit
    abstract setNpcRace: npc: ActorBase option * race: Race option -> unit
    abstract setNpcSex: npc: ActorBase option * sex: float -> unit
    abstract setNpcSkinColor: npc: ActorBase option * skinColor: float -> unit
    abstract setWeaponDrawnMode: actor: Actor option * mode: float -> unit
    abstract updateEquipment: actor: Actor option * item: Form option * leftHand: bool -> unit

type [<AllowNullLiteral>] TalkingActivator =
    inherit Activator

type [<AllowNullLiteral>] TalkingActivatorStatic =
    [<EmitConstructor>] abstract Create: unit -> TalkingActivator
    abstract from: papyrusObject: PapyrusObject option -> TalkingActivator option

type [<AllowNullLiteral>] TextureSet =
    inherit Form
    abstract getNthTexturePath: n: float -> string
    abstract getNumTexturePaths: unit -> float
    abstract setNthTexturePath: n: float * texturePath: string -> unit

type [<AllowNullLiteral>] TextureSetStatic =
    [<EmitConstructor>] abstract Create: unit -> TextureSet
    abstract from: papyrusObject: PapyrusObject option -> TextureSet option

type [<AllowNullLiteral>] Topic =
    inherit Form
    abstract add: unit -> unit

type [<AllowNullLiteral>] TopicStatic =
    [<EmitConstructor>] abstract Create: unit -> Topic
    abstract from: papyrusObject: PapyrusObject option -> Topic option

type [<AllowNullLiteral>] TopicInfo =
    inherit Form
    abstract getOwningQuest: unit -> Quest option

type [<AllowNullLiteral>] TopicInfoStatic =
    [<EmitConstructor>] abstract Create: unit -> TopicInfo
    abstract from: papyrusObject: PapyrusObject option -> TopicInfo option

type [<AllowNullLiteral>] TreeObject =
    inherit Form
    abstract getHarvestSound: unit -> SoundDescriptor option
    abstract getIngredient: unit -> Form option
    abstract setHarvestSound: akSoundDescriptor: SoundDescriptor option -> unit
    abstract setIngredient: akIngredient: Form option -> unit

type [<AllowNullLiteral>] TreeObjectStatic =
    [<EmitConstructor>] abstract Create: unit -> TreeObject
    abstract from: papyrusObject: PapyrusObject option -> TreeObject option

type [<AllowNullLiteral>] Ui =
    inherit PapyrusObject

type [<AllowNullLiteral>] UiStatic =
    [<EmitConstructor>] abstract Create: unit -> Ui
    abstract from: papyrusObject: PapyrusObject option -> Ui option
    abstract closeCustomMenu: unit -> unit
    abstract getBool: menuName: string * target: string -> bool
    abstract getFloat: menuName: string * target: string -> float
    abstract getInt: menuName: string * target: string -> float
    abstract getString: menuName: string * target: string -> string
    abstract invokeBool: menuName: string * target: string * arg: bool -> unit
    abstract invokeBoolA: menuName: string * target: string * args: ResizeArray<bool> option -> unit
    abstract invokeFloat: menuName: string * target: string * arg: float -> unit
    abstract invokeFloatA: menuName: string * target: string * args: ResizeArray<float> option -> unit
    abstract invokeForm: menuName: string * target: string * arg: Form option -> unit
    abstract invokeInt: menuName: string * target: string * arg: float -> unit
    abstract invokeIntA: menuName: string * target: string * args: ResizeArray<float> option -> unit
    abstract invokeString: menuName: string * target: string * arg: string -> unit
    abstract invokeStringA: menuName: string * target: string * args: ResizeArray<string> option -> unit
    abstract isMenuOpen: menuName: string -> bool
    abstract isTextInputEnabled: unit -> bool
    abstract openCustomMenu: swfPath: string * flags: float -> unit
    abstract setBool: menuName: string * target: string * value: bool -> unit
    abstract setFloat: menuName: string * target: string * value: float -> unit
    abstract setInt: menuName: string * target: string * value: float -> unit
    abstract setString: menuName: string * target: string * value: string -> unit

type [<AllowNullLiteral>] VisualEffect =
    inherit Form
    abstract play: param1: ObjectReference option * param2: float * param3: ObjectReference option -> unit
    abstract stop: param1: ObjectReference option -> unit

type [<AllowNullLiteral>] VisualEffectStatic =
    [<EmitConstructor>] abstract Create: unit -> VisualEffect
    abstract from: papyrusObject: PapyrusObject option -> VisualEffect option

type [<AllowNullLiteral>] VoiceType =
    inherit Form

type [<AllowNullLiteral>] VoiceTypeStatic =
    [<EmitConstructor>] abstract Create: unit -> VoiceType
    abstract from: papyrusObject: PapyrusObject option -> VoiceType option

type [<AllowNullLiteral>] Weapon =
    inherit Form
    abstract fire: akSource: ObjectReference option * akAmmo: Ammo option -> unit
    abstract getBaseDamage: unit -> float
    abstract getCritDamage: unit -> float
    abstract getCritEffect: unit -> Spell option
    abstract getCritEffectOnDeath: unit -> bool
    abstract getCritMultiplier: unit -> float
    abstract getEnchantment: unit -> Enchantment option
    abstract getEnchantmentValue: unit -> float
    abstract getEquipType: unit -> EquipSlot option
    abstract getEquippedModel: unit -> Static option
    abstract getIconPath: unit -> string
    abstract getMaxRange: unit -> float
    abstract getMessageIconPath: unit -> string
    abstract getMinRange: unit -> float
    abstract getModelPath: unit -> string
    abstract getReach: unit -> float
    abstract getResist: unit -> string
    abstract getSkill: unit -> string
    abstract getSpeed: unit -> float
    abstract getStagger: unit -> float
    abstract getTemplate: unit -> Weapon option
    abstract getWeaponType: unit -> float
    abstract setBaseDamage: damage: float -> unit
    abstract setCritDamage: damage: float -> unit
    abstract setCritEffect: ce: Spell option -> unit
    abstract setCritEffectOnDeath: ceod: bool -> unit
    abstract setCritMultiplier: crit: float -> unit
    abstract setEnchantment: e: Enchantment option -> unit
    abstract setEnchantmentValue: value: float -> unit
    abstract setEquipType: ``type``: EquipSlot option -> unit
    abstract setEquippedModel: model: Static option -> unit
    abstract setIconPath: path: string -> unit
    abstract setMaxRange: maxRange: float -> unit
    abstract setMessageIconPath: path: string -> unit
    abstract setMinRange: minRange: float -> unit
    abstract setModelPath: path: string -> unit
    abstract setReach: reach: float -> unit
    abstract setResist: resist: string -> unit
    abstract setSkill: skill: string -> unit
    abstract setSpeed: speed: float -> unit
    abstract setStagger: stagger: float -> unit
    abstract setWeaponType: ``type``: float -> unit

type [<AllowNullLiteral>] WeaponStatic =
    [<EmitConstructor>] abstract Create: unit -> Weapon
    abstract from: papyrusObject: PapyrusObject option -> Weapon option

type [<AllowNullLiteral>] Weather =
    inherit Form
    abstract forceActive: abOverride: bool -> unit
    abstract getClassification: unit -> float
    abstract getFogDistance: day: bool * ``type``: float -> float
    abstract getSunDamage: unit -> float
    abstract getSunGlare: unit -> float
    abstract getWindDirection: unit -> float
    abstract getWindDirectionRange: unit -> float
    abstract setActive: abOverride: bool * abAccelerate: bool -> unit

type [<AllowNullLiteral>] WeatherStatic =
    [<EmitConstructor>] abstract Create: unit -> Weather
    abstract from: papyrusObject: PapyrusObject option -> Weather option
    abstract findWeather: auiType: float -> Weather option
    abstract getCurrentWeather: unit -> Weather option
    abstract getCurrentWeatherTransition: unit -> float
    abstract getOutgoingWeather: unit -> Weather option
    abstract getSkyMode: unit -> float
    abstract releaseOverride: unit -> unit

type [<AllowNullLiteral>] WordOfPower =
    inherit Form

type [<AllowNullLiteral>] WordOfPowerStatic =
    [<EmitConstructor>] abstract Create: unit -> WordOfPower
    abstract from: papyrusObject: PapyrusObject option -> WordOfPower option

type [<AllowNullLiteral>] WorldSpace =
    inherit Form

type [<AllowNullLiteral>] WorldSpaceStatic =
    [<EmitConstructor>] abstract Create: unit -> WorldSpace
    abstract from: papyrusObject: PapyrusObject option -> WorldSpace option

type [<AllowNullLiteral>] Utility =
    inherit PapyrusObject

type [<AllowNullLiteral>] UtilityStatic =
    [<EmitConstructor>] abstract Create: unit -> Utility
    abstract from: papyrusObject: PapyrusObject option -> Utility option
    abstract captureFrameRate: numFrames: float -> string
    abstract createAliasArray: size: float * fill: Alias option -> ResizeArray<PapyrusObject> option
    abstract createBoolArray: size: float * fill: bool -> ResizeArray<bool> option
    abstract createFloatArray: size: float * fill: float -> ResizeArray<float> option
    abstract createFormArray: size: float * fill: Form option -> ResizeArray<PapyrusObject> option
    abstract createIntArray: size: float * fill: float -> ResizeArray<float> option
    abstract createStringArray: size: float * fill: string -> ResizeArray<string> option
    abstract endFrameRateCapture: unit -> unit
    abstract gameTimeToString: afGameTime: float -> Promise<string>
    abstract getAverageFrameRate: unit -> float
    abstract getBudgetCount: unit -> float
    abstract getBudgetName: aiBudgetNumber: float -> string
    abstract getCurrentBudget: aiBudgetNumber: float -> float
    abstract getCurrentGameTime: unit -> float
    abstract getCurrentMemory: unit -> float
    abstract getCurrentRealTime: unit -> float
    abstract getINIBool: ini: string -> bool
    abstract getINIFloat: ini: string -> float
    abstract getINIInt: ini: string -> float
    abstract getINIString: ini: string -> string
    abstract getMaxFrameRate: unit -> float
    abstract getMinFrameRate: unit -> float
    abstract isInMenuMode: unit -> bool
    abstract overBudget: aiBudgetNumber: float -> bool
    abstract randomFloat: afMin: float * afMax: float -> float
    abstract randomInt: aiMin: float * aiMax: float -> float
    abstract resizeAliasArray: source: ResizeArray<PapyrusObject> option * size: float * fill: Alias option -> ResizeArray<PapyrusObject> option
    abstract resizeBoolArray: source: ResizeArray<bool> option * size: float * fill: bool -> ResizeArray<bool> option
    abstract resizeFloatArray: source: ResizeArray<float> option * size: float * fill: float -> ResizeArray<float> option
    abstract resizeFormArray: source: ResizeArray<PapyrusObject> option * size: float * fill: Form option -> ResizeArray<PapyrusObject> option
    abstract resizeIntArray: source: ResizeArray<float> option * size: float * fill: float -> ResizeArray<float> option
    abstract resizeStringArray: source: ResizeArray<string> option * size: float * fill: string -> ResizeArray<string> option
    abstract setINIBool: ini: string * value: bool -> unit
    abstract setINIFloat: ini: string * value: float -> unit
    abstract setINIInt: ini: string * value: float -> unit
    abstract setINIString: ini: string * value: string -> unit
    abstract startFrameRateCapture: unit -> unit
    abstract wait: afSeconds: float -> Promise<unit>
    abstract waitGameTime: afHours: float -> Promise<unit>
    abstract waitMenuMode: afSeconds: float -> Promise<unit>

type [<StringEnum>] [<RequireQualifiedAccess>] ExtraDataType =
    | [<CompiledName "Health">] Health
    | [<CompiledName "Count">] Count
    | [<CompiledName "Enchantment">] Enchantment
    | [<CompiledName "Charge">] Charge
    | [<CompiledName "TextDisplayData">] TextDisplayData
    | [<CompiledName "Soul">] Soul
    | [<CompiledName "Poison">] Poison
    | [<CompiledName "Worn">] Worn
    | [<CompiledName "WornLeft">] WornLeft

type [<RequireQualifiedAccess>] ExtraSoulSoul =
    | N0 = 0
    | N1 = 1
    | N2 = 2
    | N3 = 3
    | N4 = 4
    | N5 = 5