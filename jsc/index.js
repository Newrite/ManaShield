import { HashSet } from "./fable_modules/fable-library.3.6.2/MutableSet.js";
import { getEnumerator, createAtom, safeHash, equals } from "./fable_modules/fable-library.3.6.2/Util.js";
import * as skyrimPlatform from "../src/skyrimPlatform.declare";
import { $007CPercentLessHealth$007CPercentMoreHealth$007CPercentEqualHealth$007C, $007CLessHealth$007CMoreHealth$007CEqualHealth$007C, $007CIsManaShieldEffect$007C_$007C, $007CPerkFromForm$007C_$007C, $007CMagicEffectFromForm$007C_$007C } from "./PatternNoWarn.js";
import { toText } from "./fable_modules/fable-library.3.6.2/String.js";
import { addToSet } from "./fable_modules/fable-library.3.6.2/MapUtil.js";
import { ShieldedActor__HealthContext_Z2EA54798, ShieldedActor__UpdateHealthInternal_Z2EA54798, ShieldedActor_CreateFromActor_Z2EA54798 } from "./Core.js";
import { iterate } from "./fable_modules/fable-library.3.6.2/Seq.js";
import { toArray } from "./fable_modules/fable-library.3.6.2/Option.js";
import { toString } from "./fable_modules/fable-library.3.6.2/Types.js";
import { ActorValue } from "./BindingWrapper.js";

export const Plugin_targets = new HashSet([], {
    Equals: (x, y) => equals(x, y),
    GetHashCode: (x) => safeHash(x),
});

export const Plugin_pluginName = "ManaShield.esp";

export const Plugin_effectFormID = 2050;

export const Plugin_perkFormID = 2048;

export function Plugin_manaShieldEffect() {
    const matchValue = skyrimPlatform.Game.getFormFromFile(2050, "ManaShield.esp");
    const activePatternResult440 = $007CMagicEffectFromForm$007C_$007C(matchValue);
    if (activePatternResult440 != null) {
        const effect = activePatternResult440;
        skyrimPlatform.printConsole("Find some effect");
        return effect;
    }
    else {
        skyrimPlatform.printConsole("No find effect");
        return void 0;
    }
}

export function Plugin_manaShieldPerk() {
    const matchValue = skyrimPlatform.Game.getFormFromFile(2048, "ManaShield.esp");
    const activePatternResult444 = $007CPerkFromForm$007C_$007C(matchValue);
    if (activePatternResult444 != null) {
        const perk = activePatternResult444;
        skyrimPlatform.printConsole("Find some perk");
        return perk;
    }
    else {
        skyrimPlatform.printConsole("No find perk");
        return void 0;
    }
}

export let Plugin_damageReduceMult = createAtom(0);

export let Plugin_multMagickaDamage = createAtom(1);

function OnMagicEffectApply_addToManaShieldTargets(ctx, shieldEffect) {
    skyrimPlatform.printConsole(toText(`Add to targets... sh effect - ${shieldEffect.getFormID()} | effect - ${ctx.effect.getFormID()}`));
    const matchValue_0 = shieldEffect.getFormID() === ctx.effect.getFormID();
    const matchValue_1 = ctx.target;
    const activePatternResult447 = $007CIsManaShieldEffect$007C_$007C(matchValue_0, matchValue_1);
    if (activePatternResult447 != null) {
        const actor = activePatternResult447;
        skyrimPlatform.printConsole(toText(`Add: Is Shield effect for actor id: ${actor.getFormID()}`));
        actor.addPerk(Plugin_manaShieldPerk());
        const result = addToSet(ShieldedActor_CreateFromActor_Z2EA54798(actor), Plugin_targets);
        skyrimPlatform.printConsole(toText(`Result of add - ${result}`));
    }
}

skyrimPlatform.on('magicEffectApply',((ctx) => {
    skyrimPlatform.printConsole("Effect start debug");
    const matchValue = Plugin_manaShieldEffect();
    if (matchValue == null) {
    }
    else {
        const effect = matchValue;
        skyrimPlatform.printConsole(toText(`Debug: shield effect id - ${effect.getFormID()}`));
        OnMagicEffectApply_addToManaShieldTargets(ctx, effect);
    }
}));

function OnceUpdate_setMultiplays(newValue) {
    Plugin_damageReduceMult(newValue, true);
    Plugin_multMagickaDamage(1 - Plugin_damageReduceMult(), true);
    skyrimPlatform.printConsole("Set new value to damage reduce:");
    skyrimPlatform.printConsole(toText(`__ reduceMult - ${Plugin_damageReduceMult()} | magickaMult - ${Plugin_multMagickaDamage()}`));
}

function OnceUpdate_setPerkValue(perk, id, value) {
    if (perk.setNthEntryValue(id, 0, value)) {
        skyrimPlatform.printConsole(toText(`Success set new value to perk: ID: ${id} | Value: ${value}`));
    }
    else {
        skyrimPlatform.printConsole(toText(`Failure set new value to perk: ID: ${id} | Value: ${value}`));
    }
}

function OnceUpdate_firstValueOfPerk(perk) {
    const v = perk.getNthEntryValue(0, 0);
    skyrimPlatform.printConsole(toText(`Value from perk ${v}`));
    if ((v > 0.1) === false) {
        return 0.1;
    }
    else {
        return v;
    }
}

function OnceUpdate_handlePerk(perk) {
    const entriesCounter = ((~(~perk.getNumEntries())) - 1) | 0;
    if (entriesCounter > 0) {
        const value = OnceUpdate_firstValueOfPerk(perk);
        for (let id = 0; id <= entriesCounter; id++) {
            OnceUpdate_setPerkValue(perk, id, value);
        }
        OnceUpdate_setMultiplays(value);
    }
    else {
        skyrimPlatform.printConsole(toText(`EmptyEntryes of perk - ${perk.getFormID()}`));
    }
}

skyrimPlatform.once('update',(() => {
    skyrimPlatform.printConsole("Rising once update");
    skyrimPlatform.printConsole(toText(`__ plugin name - ${"ManaShield.esp"} | perkID - ${2048} | effectID - ${2050}`));
    iterate((perk) => {
        OnceUpdate_handlePerk(perk);
    }, toArray(Plugin_manaShieldPerk()));
}));

function OnUpdate_removeFromManaShieldTargets(shieldedActor, self) {
    skyrimPlatform.printConsole(toText(`Remove actor: actorId - ${shieldedActor.FormID}`));
    self.removePerk(Plugin_manaShieldPerk());
    const result = Plugin_targets.delete(shieldedActor);
    skyrimPlatform.printConsole(toText(`Result of remove - ${result}`));
}

function OnUpdate_evaluteHealthMagickaDamage(sa, self, healthCtx, delta) {
    const fullDamage = delta / Plugin_damageReduceMult();
    const magickaDamage = fullDamage * Plugin_multMagickaDamage();
    const magicka = self.getActorValue(toString(new ActorValue(1)));
    skyrimPlatform.printConsole(toText(`DEBUG TRACE: ReduceMult ${Plugin_damageReduceMult()} | MagickaMult ${Plugin_multMagickaDamage()}`));
    skyrimPlatform.printConsole(toText(`DEBUG TRACE: FullDamage ${fullDamage} | Delta ${delta}`));
    skyrimPlatform.printConsole(toText(`Damage magicka: PlayerMagicka - ${magicka} | MagickaDamage - ${magickaDamage}`));
    if (magickaDamage > magicka) {
        const damageHealth = magickaDamage - magicka;
        skyrimPlatform.printConsole(toText(`Damage magicka delta: DamageHealth - ${damageHealth}`));
        self.damageActorValue(toString(new ActorValue(0)), damageHealth);
        self.damageActorValue(toString(new ActorValue(1)), magicka);
        ShieldedActor__UpdateHealthInternal_Z2EA54798(sa, self);
    }
    else {
        sa.LastHealth = healthCtx.CurrentHealth;
        sa.LastHealthPercent = healthCtx.CurrentHealthPercent;
        self.damageActorValue(toString(new ActorValue(1)), magickaDamage);
    }
}

function OnUpdate_handleHealthValue(sa, self, healthCtx) {
    let pattern_matching_result, delta;
    const activePatternResult485 = $007CLessHealth$007CMoreHealth$007CEqualHealth$007C(healthCtx);
    if (activePatternResult485.tag === 2) {
        pattern_matching_result = 0;
    }
    else if (activePatternResult485.tag === 0) {
        pattern_matching_result = 1;
        delta = activePatternResult485.fields[0];
    }
    else {
        pattern_matching_result = 0;
    }
    switch (pattern_matching_result) {
        case 0: {
            sa.LastHealth = healthCtx.CurrentHealth;
            sa.LastHealthPercent = healthCtx.CurrentHealthPercent;
            break;
        }
        case 1: {
            OnUpdate_evaluteHealthMagickaDamage(sa, self, healthCtx, delta);
            break;
        }
    }
}

function OnUpdate_handleHealthPercent(sa, self) {
    const healthCtx = ShieldedActor__HealthContext_Z2EA54798(sa, self);
    let pattern_matching_result;
    const activePatternResult491 = $007CPercentLessHealth$007CPercentMoreHealth$007CPercentEqualHealth$007C(healthCtx);
    if (activePatternResult491.tag === 2) {
        pattern_matching_result = 0;
    }
    else if (activePatternResult491.tag === 0) {
        pattern_matching_result = 1;
    }
    else {
        pattern_matching_result = 0;
    }
    switch (pattern_matching_result) {
        case 0: {
            sa.LastHealth = healthCtx.CurrentHealth;
            sa.LastHealthPercent = healthCtx.CurrentHealthPercent;
            break;
        }
        case 1: {
            OnUpdate_handleHealthValue(sa, self, healthCtx);
            break;
        }
    }
}

function OnUpdate_handleTarget(sa) {
    const self = sa.Self();
    if (self.hasMagicEffect(Plugin_manaShieldEffect())) {
        OnUpdate_handleHealthPercent(sa, self);
    }
    else {
        OnUpdate_removeFromManaShieldTargets(sa, self);
    }
}

skyrimPlatform.on('update',(() => {
    let enumerator = getEnumerator(Plugin_targets);
    try {
        while (enumerator["System.Collections.IEnumerator.MoveNext"]()) {
            OnUpdate_handleTarget(enumerator["System.Collections.Generic.IEnumerator`1.get_Current"]());
        }
    }
    finally {
        enumerator.Dispose();
    }
}));

