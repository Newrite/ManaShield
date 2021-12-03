import { Record } from "./fable_modules/fable-library.3.6.2/Types.js";
import { record_type, string_type } from "./fable_modules/fable-library.3.6.2/Reflection.js";

export class Hello extends Record {
    constructor(Hello) {
        super();
        this.Hello = Hello;
    }
}

export function Hello$reflection() {
    return record_type("PluginSource.Check.Hello", [], Hello, () => [["Hello", string_type]]);
}

//# sourceMappingURL=Check.js.map
