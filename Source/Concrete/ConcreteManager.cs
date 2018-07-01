using System;
using System.Collections.Generic;
using RimWorld.Planet;
using Verse;

namespace Concrete {
    public class ConcreteManager : WorldComponent {

        private SimpleCurve hydrateCurve = new SimpleCurve();
        private int daysToHydrate;

        private Dictionary<Thing, int> concreteThingList;
        public ConcreteManager(World world) : base(world) {
            concreteThingList = new Dictionary<Thing, int>();
            daysToHydrate = Concrete.SettingInt["daysToHydrate"].Value;
            hydrateCurve.Add(new CurvePoint(0, 0));
            hydrateCurve.Add(new CurvePoint(daysToHydrate / 0.25f, 0.5f));
            hydrateCurve.Add(new CurvePoint(daysToHydrate, 1f));
        }
        public void Add(Thing thing) {
            concreteThingList[thing] = Find.TickManager.TicksAbs;
        }
        public bool contains(Thing thing) {
            return concreteThingList.ContainsKey(thing);
        }
        public override void WorldComponentTick() {
            TickManager tm = Find.TickManager;
            if(tm.TicksGame % 250 != 0) {
                return;
            }
            int currentAbs = tm.TicksAbs;
            float oneDay = 60000f;
            List<Thing> toRemove = new List<Thing>();
            foreach(KeyValuePair<Thing, int> kvp in concreteThingList) {
                float days = (currentAbs - kvp.Value) / oneDay;
                Thing thing = kvp.Key;
                int hitPoints;
                if(days >= daysToHydrate) {
                    hitPoints = thing.MaxHitPoints;
                } else {
                    hitPoints = (int)(thing.MaxHitPoints * hydrateCurve.Evaluate(days));
                }

                if(hitPoints < 1) {
                    hitPoints = 1;
                }
                if(hitPoints > thing.MaxHitPoints) {
                    hitPoints = thing.MaxHitPoints;
                }
                thing.HitPoints = hitPoints;
                if(thing.HitPoints == thing.MaxHitPoints) {
                    toRemove.Add(thing);
                }
            }
            foreach(Thing removeThing in toRemove) {
                concreteThingList.Remove(removeThing);
            }
        }
        public override void ExposeData() {
            Scribe_Collections.Look<Thing, int>(ref concreteThingList, "cTL", LookMode.Def, LookMode.Value);
        }
    }
}
