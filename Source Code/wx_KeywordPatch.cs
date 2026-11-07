using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppInterop.Runtime.Runtime;
using Il2CppSystem;
using MelonLoader;
using System;
using System.ComponentModel.Design;
using UnityEngine;
using Il2CppTMPro;
using HarmonyLib;

public class wx_KeywordPatch // Code by Skill Cycler, absolute legend!
{

    public string PatchTooltip(string value)
    {
        if (value != null)
        {

        }
        return value;
    }
    [HarmonyPatch(typeof(TextTooltipRecognizer), "GetTooltipInfo")]
    public static class TooltipPatch
    {
        static void Postfix(string linkID, ref TooltipInfo __result)
        {
            wx_KeywordPatch patcher = new();
            if (linkID == "LycanthropeWerewolfLink")
            {
                __result = new TooltipInfo(
                    patcher.PatchTooltip("Evil Minion that Lies and Disguises. At Night, the Werewolf deals 1 damage to you."),
                    "Werewolf",
                    new Color32(217, 116, 0, 255)
                );
            }
            if (linkID == "PuppeteerPuppetLink")
            {
                __result = new TooltipInfo(
                    patcher.PatchTooltip("Evil Minion who Disguises as the role it used to be, but does not Lie."),
                    "Puppet",
                    new Color32(217, 116, 0, 255)
                );
            }
        }
    }
}
