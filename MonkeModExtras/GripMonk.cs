using Monke_Mod_Panel.Attributes;
using UnityEngine;
using HarmonyLib;
using System.Reflection;
using MelonLoader;

namespace ModTemplate;

[Toggleable]
public class GripMonk : Monke_Mod_Panel.Mod
{
    public override string Name => "Grip Monk";
    
    private static HarmonyLib.Harmony harmony = new HarmonyLib.Harmony("com.est.gtag.gripmonk");
    public static bool inModdedRoom = false;

    public override void OnInitializeMelon()
    {
        inModdedRoom = false;
        var original = AccessTools.Method(typeof(GorillaLocomotion.GTPlayer), "GetSlidePercentage");
        harmony.Unpatch(original, HarmonyPatchType.All);
    }
    public override void OnEnable()
    {
        inModdedRoom = true;
        harmony.PatchAll(Assembly.GetExecutingAssembly());
    }
    
    public override void OnDisable()
    {
        inModdedRoom = false;
        var original = AccessTools.Method(typeof(GorillaLocomotion.GTPlayer), "GetSlidePercentage");
        harmony.Unpatch(original, HarmonyPatchType.All);
    }

    public override void OnUpdate() { }
    
    public override void OnModdedJoin()
    {
        inModdedRoom = true;
    }

    public override void OnModdedLeave()
    {
        inModdedRoom = false;
        var original = AccessTools.Method(typeof(GorillaLocomotion.GTPlayer), "GetSlidePercentage");
        harmony.Unpatch(original, HarmonyPatchType.All);
    }
}

[HarmonyPatch(typeof(GorillaLocomotion.GTPlayer), "GetSlidePercentage")]
public class SlidePatch
{
    static void Postfix(ref float __result)
    {
        if (GripMonk.inModdedRoom)
        {
            __result = 0.03f;
        }
    }
}