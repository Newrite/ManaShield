import { HashSet } from "./fable_modules/fable-library.3.6.2/MutableSet.js";
import { getEnumerator, createAtom, safeHash, equals } from "./fable_modules/fable-library.3.6.2/Util.js";
import * as skyrimPlatform from "../src/skyrimPlatform.declare";
import { addToSet } from "./fable_modules/fable-library.3.6.2/MapUtil.js";
import { ShieldedActor__UpdateHealthInternal_Z2EA54798, ShieldedActor__UpdateHealth, ShieldedActor__HealthContext_Z2EA54798, ShieldedActor_CreateFromActor_Z2EA54798 } from "./Core.js";
import { toString } from "./fable_modules/fable-library.3.6.2/Types.js";
import { ActorValue } from "./BindingWrapper.js";
import { FSharpChoice$3 } from "./fable_modules/fable-library.3.6.2/Choice.js";

export const targets = new HashSet([], {
    Equals: (x, y) => equals(x, y),
    GetHashCode: (x) => safeHash(x),
});

export let lastHealth = createAtom(0);

export let lastHealthPercent = createAtom(1);

export const pluginName = "ManaShield.esp";

export const effectFormID = 2050;

export const perkFormID = 2048;

export function manaShieldEffect() {
    const matchValue = skyrimPlatform.Game.getFormFromFile(effectFormID, pluginName);
    let activePatternResult432;
    const someForm = matchValue;
    if (someForm != null) {
        let matchValue_1;
        const self_1 = skyrimPlatform.MagicEffect;
        matchValue_1 = skyrimPlatform.MagicEffect.from(someForm);
        activePatternResult432 = ((matchValue_1 != null) ? matchValue_1 : (void 0));
    }
    else {
        activePatternResult432 = (void 0);
    }
    if (activePatternResult432 != null) {
        const effect = activePatternResult432;
        return effect;
    }
    else {
        return void 0;
    }
}

export function manaShieldPerk() {
    const matchValue = skyrimPlatform.Game.getFormFromFile(perkFormID, pluginName);
    let activePatternResult436;
    const someForm = matchValue;
    if (someForm != null) {
        let matchValue_1;
        const self_1 = skyrimPlatform.Perk;
        matchValue_1 = skyrimPlatform.Perk.from(someForm);
        activePatternResult436 = ((matchValue_1 != null) ? matchValue_1 : (void 0));
    }
    else {
        activePatternResult436 = (void 0);
    }
    if (activePatternResult436 != null) {
        const perk = activePatternResult436;
        return perk;
    }
    else {
        return void 0;
    }
}

export let damageReduceMult = createAtom(0);

export let multMagickaDamage = createAtom(1);

skyrimPlatform.on('magicEffectApply',((ctx) => {
    skyrimPlatform.printConsole("Effect start debug");
    const matchValue = manaShieldEffect();
    if (matchValue == null) {
    }
    else {
        const effect = matchValue;
        skyrimPlatform.printConsole(`Debug: shield effect id - ${effect.getFormID()}`);
        const ctx_1 = ctx;
        const shieldEffect = effect;
        skyrimPlatform.printConsole(`Add to targets... sh effect - ${shieldEffect.getFormID()} | effect - ${ctx_1.effect.getFormID()}`);
        const matchValue_1 = [shieldEffect.getFormID() === ctx_1.effect.getFormID(), ctx_1.target];
        let activePatternResult440;
        if (matchValue_1[0]) {
            let matchValue_2;
            const self = skyrimPlatform.Actor;
            matchValue_2 = skyrimPlatform.Actor.from(matchValue_1[1]);
            activePatternResult440 = ((matchValue_2 != null) ? matchValue_2 : (void 0));
        }
        else {
            activePatternResult440 = (void 0);
        }
        if (activePatternResult440 != null) {
            const actor_1 = activePatternResult440;
            skyrimPlatform.printConsole(`Add: Is Shield effect for actor id: ${actor_1.getFormID()}`);
            actor_1.addPerk(manaShieldPerk());
            const result = addToSet(ShieldedActor_CreateFromActor_Z2EA54798(actor_1), targets);
            skyrimPlatform.printConsole(`Result of add - ${result}`);
        }
    }
}));

skyrimPlatform.once('update',(() => {
    skyrimPlatform.printConsole(`Rising once update: plugin name - ${pluginName} | perkID - ${perkFormID} | effectID - ${effectFormID}`);
    if (manaShieldEffect() != null) {
        skyrimPlatform.printConsole("Find some effect");
    }
    else {
        skyrimPlatform.printConsole("No find effect");
    }
    const matchValue_1 = manaShieldPerk();
    if (matchValue_1 != null) {
        const p = matchValue_1;
        skyrimPlatform.printConsole("Find some perk");
        const entriesCounter = ((~(~p.getNumEntries())) - 1) | 0;
        if (entriesCounter > 0) {
            let value;
            const v = p.getNthEntryValue(0, 0);
            skyrimPlatform.printConsole(`vALUE ${v}`);
            value = ((v <= 0.1) ? 0.1 : v);
            for (let id = 0; id <= entriesCounter; id++) {
                const value_1 = value;
                const id_1 = id;
                if (p.setNthEntryValue(id_1, 0, value_1)) {
                    skyrimPlatform.printConsole(`Success set new value to perk: ID: ${id_1} | Value: ${value_1}`);
                }
                else {
                    skyrimPlatform.printConsole(`Failure set new value to perk: ID: ${id_1} | Value: ${value_1}`);
                }
            }
            damageReduceMult(value, true);
            multMagickaDamage(1 - damageReduceMult(), true);
            skyrimPlatform.printConsole(`Set new value to damage reduce: reduceMult - ${damageReduceMult()} | magickaMult - ${multMagickaDamage()}`);
        }
        else {
            skyrimPlatform.printConsole(`EmptyEntryes of perk - ${p.getFormID()}`);
        }
    }
    else {
        skyrimPlatform.printConsole("No find perk");
    }
}));

skyrimPlatform.on('update',(() => {
    let enumerator = getEnumerator(targets);
    try {
        while (enumerator["System.Collections.IEnumerator.MoveNext"]()) {
            const sa = enumerator["System.Collections.Generic.IEnumerator`1.get_Current"]();
            const self = sa.Self();
            if (!self.hasMagicEffect(manaShieldEffect())) {
                const shieldedActor = sa;
                skyrimPlatform.printConsole(`Remove actor: actorId - ${shieldedActor.FormID}`);
                self.removePerk(manaShieldPerk());
                const result = targets.delete(shieldedActor);
                skyrimPlatform.printConsole(`Result of remove - ${result}`);
            }
            else {
                const healthCtx = ShieldedActor__HealthContext_Z2EA54798(sa, self);
                let pattern_matching_result;
                let activePatternResult475;
                const ctxHealth = healthCtx;
                const percentEquality = !(Math.abs(ctxHealth.CurrentHealthPercent - ctxHealth.LastHealthPercent) > 0.01);
                if (percentEquality) {
                    activePatternResult475 = (new FSharpChoice$3(2, void 0));
                }
                else if (ctxHealth.CurrentHealthPercent > ctxHealth.LastHealthPercent) {
                    activePatternResult475 = (new FSharpChoice$3(1, void 0));
                }
                else if (ctxHealth.CurrentHealthPercent < ctxHealth.LastHealthPercent) {
                    activePatternResult475 = (new FSharpChoice$3(0, void 0));
                }
                else {
                    throw (new Error("Match failure"));
                }
                if (activePatternResult475.tag === 2) {
                    pattern_matching_result = 0;
                }
                else if (activePatternResult475.tag === 0) {
                    pattern_matching_result = 1;
                }
                else {
                    pattern_matching_result = 0;
                }
                switch (pattern_matching_result) {
                    case 0: {
                        ShieldedActor__UpdateHealth(sa, healthCtx.CurrentHealth, healthCtx.CurrentHealthPercent);
                        break;
                    }
                    case 1: {
                        let pattern_matching_result_1, delta_1;
                        let activePatternResult474;
                        const ctxHealth_1 = healthCtx;
                        const delta = Math.abs(ctxHealth_1.CurrentHealth - ctxHealth_1.LastHealth);
                        if (ctxHealth_1.CurrentHealth > ctxHealth_1.LastHealth) {
                            activePatternResult474 = (new FSharpChoice$3(1, delta));
                        }
                        else if (ctxHealth_1.CurrentHealth === ctxHealth_1.LastHealth) {
                            activePatternResult474 = (new FSharpChoice$3(2, void 0));
                        }
                        else if (ctxHealth_1.CurrentHealth < ctxHealth_1.LastHealth) {
                            activePatternResult474 = (new FSharpChoice$3(0, delta));
                        }
                        else {
                            throw (new Error("Match failure"));
                        }
                        if (activePatternResult474.tag === 2) {
                            pattern_matching_result_1 = 0;
                        }
                        else if (activePatternResult474.tag === 0) {
                            pattern_matching_result_1 = 1;
                            delta_1 = activePatternResult474.fields[0];
                        }
                        else {
                            pattern_matching_result_1 = 0;
                        }
                        switch (pattern_matching_result_1) {
                            case 0: {
                                ShieldedActor__UpdateHealth(sa, healthCtx.CurrentHealth, healthCtx.CurrentHealthPercent);
                                break;
                            }
                            case 1: {
                                const fullDamage = delta_1 / damageReduceMult();
                                const magickaDamage = fullDamage * multMagickaDamage();
                                const magicka = self.getActorValue(toString(new ActorValue(1)));
                                skyrimPlatform.printConsole(`DEBUG TRACE: ReduceMult: ${damageReduceMult()} | MagickaMult ${multMagickaDamage()} | FullDamage ${fullDamage} | Delta ${delta_1}`);
                                skyrimPlatform.printConsole(`Damage magicka: PlayerMagicka - ${magicka} | MagickaDamage - ${magickaDamage}`);
                                if (magickaDamage > magicka) {
                                    const damageHealth = magickaDamage - magicka;
                                    skyrimPlatform.printConsole(`Damage magicka delta: DamageHealth - ${damageHealth}`);
                                    self.damageActorValue(toString(new ActorValue(0)), damageHealth);
                                    self.damageActorValue(toString(new ActorValue(1)), magicka);
                                    ShieldedActor__UpdateHealthInternal_Z2EA54798(sa, self);
                                }
                                else {
                                    ShieldedActor__UpdateHealth(sa, healthCtx.CurrentHealth, healthCtx.CurrentHealthPercent);
                                    self.damageActorValue(toString(new ActorValue(1)), magickaDamage);
                                }
                                break;
                            }
                        }
                        break;
                    }
                }
            }
        }
    }
    finally {
        enumerator.Dispose();
    }
}));

