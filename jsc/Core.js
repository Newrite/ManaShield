import { Record } from "./fable_modules/fable-library.3.6.2/Types.js";
import { record_type, float64_type } from "./fable_modules/fable-library.3.6.2/Reflection.js";

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

//# sourceMappingURL=Core.js.map
