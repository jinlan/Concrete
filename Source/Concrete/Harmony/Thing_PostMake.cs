using System;
using Harmony;
using Verse;
using RimWorld;


namespace Concrete {
    [HarmonyPatch(typeof(Thing), "PostMake")]
    public class Thing_PostMake {
        private static bool startCheck = false;
        static void Postfix(Thing __instance) {
            if(__instance != null && __instance.Stuff != null && (__instance.Stuff.defName == "Concrete") && !(__instance is Frame) && startCheckConcrete()) {
                ConcreteManager cm = Find.World.GetComponent<ConcreteManager>();
                if(!cm.contains(__instance)) {
                    __instance.HitPoints = 1;
                    cm.Add(__instance);
                }
            }
        }
        private static bool startCheckConcrete() {
            Map findMap = Find.VisibleMap;
            if(findMap != null && findMap.mapPawns.AnyColonistSpawned) {
                startCheck = true;
            }
            return startCheck;
        }
    }
}
