using Monke_Mod_Panel.Attributes;
using UnityEngine;

namespace ModTemplate;

[Toggleable]
public class NoClip : Monke_Mod_Panel.Mod
{
    public override string Name => "No Clip (Y)";
    public static bool NoClipOn;
    public override void OnUpdate()
    {
        if (ControllerInputPoller.instance.leftControllerSecondaryButton && !NoClipOn)
        {
            MeshCollider[] array = Resources.FindObjectsOfTypeAll<MeshCollider>();
            for (int i = 0; i < array.Length; i++)
                array[i].enabled = false;

            NoClipOn = true;
        }

        if (!ControllerInputPoller.instance.leftControllerSecondaryButton && NoClipOn)
        {
            MeshCollider[] array = Resources.FindObjectsOfTypeAll<MeshCollider>();
            for (int i = 0; i < array.Length; i++)
                array[i].enabled = true;

            NoClipOn = false;
        }
    }
}