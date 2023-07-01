using HarmonyLib;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;
using IPALogger = IPA.Logging.Logger;

namespace RemoveExtraVersionDigits
{
    [Plugin(RuntimeOptions.DynamicInit)]
    public class Plugin
    {
        internal static Harmony HarmonyInstance { get; private set; }
        public readonly string HarmonyID = "Saeraphinx.RemoveExtraVersionDigits";
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }
        internal static Assembly Assembly { get; } = Assembly.GetExecutingAssembly();

        [Init]
        public Plugin(IPALogger logger)
        {
            Instance = this;
            Plugin.Log = logger;
            Plugin.Log?.Debug("Logger initialized.");
        }

        [OnEnable]
        public void OnEnable()
        {
            HarmonyInstance = new Harmony(HarmonyID);
            ApplyHarmonyPatches();
        }

        [OnDisable]
        public void OnDisable()
        {
            RemoveHarmonyPatches();
        }

        internal static void ApplyHarmonyPatches()
        {
            try
            {
                Plugin.Log?.Debug("Applying Harmony patches.");
                HarmonyInstance.PatchAll(Assembly);
            }
            catch (Exception ex)
            {
                Plugin.Log?.Error("Error applying Harmony patches: " + ex.Message);
                Plugin.Log?.Debug(ex);
            }
        }

        internal static void RemoveHarmonyPatches()
        {
            try
            {
                HarmonyInstance.UnpatchSelf();
            }
            catch (Exception ex)
            {
                Plugin.Log?.Error("Error removing Harmony patches: " + ex.Message);
                Plugin.Log?.Debug(ex);
            }
        }
    }
}
