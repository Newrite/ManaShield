import { Union } from "./fable_modules/fable-library.3.6.2/Types.js";
import { union_type } from "./fable_modules/fable-library.3.6.2/Reflection.js";

export class ActorValue extends Union {
    constructor(tag, ...fields) {
        super();
        this.tag = (tag | 0);
        this.fields = fields;
    }
    cases() {
        return ["Health", "Magicka"];
    }
    toString() {
        const self = this;
        return (self.tag === 1) ? "magicka" : "health";
    }
}

export function ActorValue$reflection() {
    return union_type("BindingWrapper.ActorValue", [], ActorValue, () => [[], []]);
}

//# sourceMappingURL=BindingWrapper.js.map
