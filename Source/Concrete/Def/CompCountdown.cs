using System;
using Verse;
using RimWorld;

namespace Concrete {
    public class CompCountdown : ThingComp {

        private int ticks = 0;
        private int maxTick = -1;

        private int getMaxTick() {
            if(maxTick < 0) {
                CompPropertiesCountdown pcd = props as CompPropertiesCountdown;
                maxTick = (int)(pcd.daysCountdown * 60000f - ticks);
            }
            return maxTick;
        }

        private int getRemainingTick() {
            return getMaxTick() - ticks;
        }

        public override void CompTick() {
            ticks++;
            if(ticks > getMaxTick()) {
                this.parent.Destroy(DestroyMode.Vanish);
            }
        }

        public override string CompInspectStringExtra() {
            CompPropertiesCountdown pcd = props as CompPropertiesCountdown;
            return pcd.inspectString + getRemainingTick().ToStringTicksToPeriodVagueMax();
        }
    }
}
