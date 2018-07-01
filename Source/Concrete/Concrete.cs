using System;
using System.Collections.Generic;
using System.Reflection;
using HugsLib;
using HugsLib.Settings;
using Harmony;

namespace Concrete {
    public class Concrete : ModBase {

        private const int DaysToHydrate = 5;

        public static Dictionary<string, SettingHandle<int>> SettingInt;

        public Concrete() {
            SettingInt = new Dictionary<string, SettingHandle<int>>();
        }

        public override string ModIdentifier {
            get {
                return "Concrete";
            }
        }

        public override void DefsLoaded() {
            HarmonyInstance harmony = HarmonyInstance.Create("Concrete");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            SettingInt["daysToHydrate"] = Settings.GetHandle<int>("daysToHydrate", "days to hydrate", "How many days required for concrete to get its full health.", DaysToHydrate, Validators.IntRangeValidator(1, 99));
        }
    }
}
