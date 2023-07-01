using HarmonyLib;
using IPA.Loader;
using System.Reflection;
using System;
using UnityEngine;
using IPA.Utilities;
using TMPro;

namespace RemoveExtraVersionDigits.Patches
{
    internal class VersionText
    {

        [HarmonyPatch(typeof(SetApplicationVersionText))]
        [HarmonyPatch("Start")]
        internal class VersionTextPatch
        {
            //static bool Prepare() => Application.version.Contains("_");

            // Prefix: Before the regular method runs - bool disables method.
            private static bool Prefix(SetApplicationVersionText __instance) {
                Plugin.Log.Info($"Game version {Application.version}");
                TextMeshPro versionText = __instance.GetField<TextMeshPro, SetApplicationVersionText>("_versionText");
                string currVersion = Application.version.Split('_')[0];
                versionText.gameObject.transform.transform.position = new Vector3(0.0969f, 0.005f, -0.106f);
                if (DateTime.Now.Day == 1 && DateTime.Now.Month == 4)
                {
                    versionText.text = "7.2.7";
                } else if (DateTime.Now.Day == 27 && DateTime.Now.Month == 7)
                {
                    versionText.text = "7.2.7";
                } else
                {
                    versionText.text = Application.version.Split('_')[0];
                }
                Plugin.Log.Info($"Moved foot text and set to {versionText.text}");

                return false;
            }
        }
    }
}