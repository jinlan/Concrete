using System;
using Verse;

namespace Concrete {
    public class CompPropertiesCountdown : CompProperties {
        public CompPropertiesCountdown() {
            compClass = typeof(CompCountdown);
        }
        public float daysCountdown = 5;
        public string inspectString = "";
    }
}
