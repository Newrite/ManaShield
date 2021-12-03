import { createAtom } from "./fable_modules/fable-library.3.6.2/Util.js";
import * as skyrimPlatform from "../src/skyrimPlatform.declare";
import { toString } from "./fable_modules/fable-library.3.6.2/Types.js";
import { ActorValue } from "./BindingWrapper.js";
import { HealthContext } from "./Core.js";
import { FSharpChoice$3 } from "./fable_modules/fable-library.3.6.2/Choice.js";

export let lastHealth = createAtom(0);

export let lastHealthPercent = createAtom(1);

export const damageReduceMult = 0.7;

export const multMagickaDamage = 1 - damageReduceMult;

skyrimPlatform.on('update',(() => {
    const player = skyrimPlatform.Game.getPlayer();
    const healthPercent = player.getActorValuePercentage(toString(new ActorValue(0)));
    const health = player.getActorValue(toString(new ActorValue(0)));
    const magicka = player.getActorValue(toString(new ActorValue(1)));
    const healthCtx = new HealthContext(lastHealth(), health, lastHealthPercent(), healthPercent);
    const updateHealth = (health_1, percentHealth) => {
        lastHealth(health_1, true);
        lastHealthPercent(percentHealth, true);
    };
    let pattern_matching_result;
    let activePatternResult396;
    const ctxHealth = healthCtx;
    const percentEquality = !(Math.abs(ctxHealth.CurrentHealthPercent - ctxHealth.LastHealthPercent) > 0.01);
    if (percentEquality) {
        activePatternResult396 = (new FSharpChoice$3(2, void 0));
    }
    else if (ctxHealth.CurrentHealthPercent > ctxHealth.LastHealthPercent) {
        activePatternResult396 = (new FSharpChoice$3(1, void 0));
    }
    else if (ctxHealth.CurrentHealthPercent < ctxHealth.LastHealthPercent) {
        activePatternResult396 = (new FSharpChoice$3(0, void 0));
    }
    else {
        throw (new Error("Match failure"));
    }
    if (activePatternResult396.tag === 2) {
        pattern_matching_result = 0;
    }
    else if (activePatternResult396.tag === 0) {
        pattern_matching_result = 1;
    }
    else {
        pattern_matching_result = 0;
    }
    switch (pattern_matching_result) {
        case 0: {
            updateHealth(healthCtx.CurrentHealth, healthCtx.CurrentHealthPercent);
            break;
        }
        case 1: {
            let pattern_matching_result_1, delta_1;
            let activePatternResult395;
            const ctxHealth_1 = healthCtx;
            const delta = Math.abs(ctxHealth_1.CurrentHealth - ctxHealth_1.LastHealth);
            if (ctxHealth_1.CurrentHealth > ctxHealth_1.LastHealth) {
                activePatternResult395 = (new FSharpChoice$3(1, delta));
            }
            else if (ctxHealth_1.CurrentHealth === ctxHealth_1.LastHealth) {
                activePatternResult395 = (new FSharpChoice$3(2, void 0));
            }
            else if (ctxHealth_1.CurrentHealth < ctxHealth_1.LastHealth) {
                activePatternResult395 = (new FSharpChoice$3(0, delta));
            }
            else {
                throw (new Error("Match failure"));
            }
            if (activePatternResult395.tag === 2) {
                pattern_matching_result_1 = 0;
            }
            else if (activePatternResult395.tag === 0) {
                pattern_matching_result_1 = 1;
                delta_1 = activePatternResult395.fields[0];
            }
            else {
                pattern_matching_result_1 = 0;
            }
            switch (pattern_matching_result_1) {
                case 0: {
                    updateHealth(healthCtx.CurrentHealth, healthCtx.CurrentHealthPercent);
                    break;
                }
                case 1: {
                    const fullDamage = delta_1 / damageReduceMult;
                    const magickaDamage = fullDamage * multMagickaDamage;
                    const self_3 = skyrimPlatform;
                    self_3.printConsole(`DEBUG TRACE: ReduceMult: ${damageReduceMult} | MagickaMult ${multMagickaDamage} | FullDamage ${fullDamage} | Delta ${delta_1}`);
                    const self_4 = skyrimPlatform;
                    self_4.printConsole(`Damage magicka: PlayerMagicka - ${magicka} | MagickaDamage - ${magickaDamage}`);
                    if (magickaDamage > magicka) {
                        const damageHealth = magickaDamage - magicka;
                        const self_5 = skyrimPlatform;
                        self_5.printConsole(`Damage magicka delta: DamageHealth - ${damageHealth}`);
                        player.damageActorValue(toString(new ActorValue(0)), damageHealth);
                        player.damageActorValue(toString(new ActorValue(1)), magicka);
                        updateHealth(player.getActorValue(toString(new ActorValue(0))), player.getActorValuePercentage(toString(new ActorValue(0))));
                    }
                    else {
                        updateHealth(healthCtx.CurrentHealth, healthCtx.CurrentHealthPercent);
                        player.damageActorValue(toString(new ActorValue(1)), magickaDamage);
                    }
                    break;
                }
            }
            break;
        }
    }
}));

//# sourceMappingURL=index.js.map
