import { Record } from "./fable_modules/fable-library.3.6.2/Types.js";
import { lambda_type, class_type, unit_type, record_type, float64_type } from "./fable_modules/fable-library.3.6.2/Reflection.js";
import { numberHash } from "./fable_modules/fable-library.3.6.2/Util.js";
import { value } from "./fable_modules/fable-library.3.6.2/Option.js";
import * as skyrimPlatform from "../src/skyrimPlatform.declare";

export class HealthContext extends Record {
    constructor(LastHealth, CurrentHealth, LastHealthPercent, CurrentHealthPercent) {
        super();
        this.LastHealth = LastHealth;
        this.CurrentHealth = CurrentHealth;
        this.LastHealthPercent = LastHealthPercent;
        this.CurrentHealthPercent = CurrentHealthPercent;
    }
}

export function HealthContext$reflection() {
    return record_type("Core.HealthContext", [], HealthContext, () => [["LastHealth", float64_type], ["CurrentHealth", float64_type], ["LastHealthPercent", float64_type], ["CurrentHealthPercent", float64_type]]);
}

export class ShieldedActor extends Record {
    constructor(Self, FormID, LastHealth, LastHealthPercent) {
        super();
        this.Self = Self;
        this.FormID = FormID;
        this.LastHealth = LastHealth;
        this.LastHealthPercent = LastHealthPercent;
    }
    Equals(other) {
        let sa;
        const self = this;
        return (other instanceof ShieldedActor) && ((sa = other, self.FormID === sa.FormID));
    }
    GetHashCode() {
        const self = this;
        const cp = self.FormID;
        return numberHash(cp) | 0;
    }
    CompareTo(other) {
        let sa;
        const self = this;
        return ((other instanceof ShieldedActor) ? ((sa = other, (self.FormID > sa.FormID) ? 1 : ((self.FormID === sa.FormID) ? 0 : -1))) : -1) | 0;
    }
}

export function ShieldedActor$reflection() {
    return record_type("Core.ShieldedActor", [], ShieldedActor, () => [["Self", lambda_type(unit_type, class_type("SkyrimPlatform.Actor"))], ["FormID", float64_type], ["LastHealth", float64_type], ["LastHealthPercent", float64_type]]);
}

export function ShieldedActor__HealthContext_Z2EA54798(self, selfActor) {
    return new HealthContext(self.LastHealth, selfActor.getActorValue("health"), self.LastHealthPercent, selfActor.getActorValuePercentage("health"));
}

export function ShieldedActor__UpdateHealth(self, lastHealth, lastHealthPercent) {
    self.LastHealth = lastHealth;
    self.LastHealthPercent = lastHealthPercent;
}

export function ShieldedActor__UpdateHealthInternal_Z2EA54798(self, selfActor) {
    self.LastHealth = selfActor.getActorValue("health");
    self.LastHealthPercent = selfActor.getActorValuePercentage("health");
}

export function ShieldedActor_CreateFromActor_Z2EA54798(actor) {
    const formID = actor.getFormID();
    return new ShieldedActor(() => {
        let a, a_1;
        return value(skyrimPlatform.Actor.from((a = skyrimPlatform.Game.getFormEx(formID), (a == null) ? (void 0) : ((a_1 = a, a_1)))));
    }, actor.getFormID(), actor.getActorValue("health"), actor.getActorValuePercentage("health)"));
}

