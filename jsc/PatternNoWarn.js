import { equals, compare } from "./fable_modules/fable-library.3.6.2/Util.js";
import { FSharpChoice$3 } from "./fable_modules/fable-library.3.6.2/Choice.js";
import * as skyrimPlatform from "../src/skyrimPlatform.declare";

export function $007CLessHealth$007CMoreHealth$007CEqualHealth$007C(ctxHealth) {
    const delta = Math.abs(ctxHealth.CurrentHealth - ctxHealth.LastHealth);
    if (compare(ctxHealth.CurrentHealth, ctxHealth.LastHealth) > 0) {
        return new FSharpChoice$3(1, delta);
    }
    else if (equals(ctxHealth.CurrentHealth, ctxHealth.LastHealth)) {
        return new FSharpChoice$3(2, void 0);
    }
    else if (compare(ctxHealth.CurrentHealth, ctxHealth.LastHealth) < 0) {
        return new FSharpChoice$3(0, delta);
    }
    else {
        throw (new Error("Match failure"));
    }
}

export function $007CPercentLessHealth$007CPercentMoreHealth$007CPercentEqualHealth$007C(ctxHealth) {
    if (Math.abs(ctxHealth.CurrentHealthPercent - ctxHealth.LastHealthPercent) > 0.01) {
        if (compare(ctxHealth.CurrentHealthPercent, ctxHealth.LastHealthPercent) > 0) {
            return new FSharpChoice$3(1, void 0);
        }
        else if (compare(ctxHealth.CurrentHealthPercent, ctxHealth.LastHealthPercent) < 0) {
            return new FSharpChoice$3(0, void 0);
        }
        else {
            throw (new Error("Match failure"));
        }
    }
    else {
        return new FSharpChoice$3(2, void 0);
    }
}

export function $007CIsManaShieldEffect$007C_$007C(equal, objRef) {
    if (equal) {
        let matchValue;
        const self = skyrimPlatform.Actor;
        matchValue = skyrimPlatform.Actor.from(objRef);
        if (matchValue != null) {
            return matchValue;
        }
        else {
            return void 0;
        }
    }
    else {
        return void 0;
    }
}

export function $007CMagicEffectFromForm$007C_$007C(someForm) {
    if (someForm != null) {
        let matchValue;
        const self = skyrimPlatform.MagicEffect;
        matchValue = skyrimPlatform.MagicEffect.from(someForm);
        if (matchValue != null) {
            return matchValue;
        }
        else {
            return void 0;
        }
    }
    else {
        return void 0;
    }
}

export function $007CPerkFromForm$007C_$007C(someForm) {
    if (someForm != null) {
        let matchValue;
        const self = skyrimPlatform.Perk;
        matchValue = skyrimPlatform.Perk.from(someForm);
        if (matchValue != null) {
            return matchValue;
        }
        else {
            return void 0;
        }
    }
    else {
        return void 0;
    }
}

