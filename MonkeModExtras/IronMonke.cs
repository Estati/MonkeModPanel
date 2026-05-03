using Monke_Mod_Panel.Attributes;
using UnityEngine;

namespace ModTemplate;

[Toggleable]
public class IronMonke : Monke_Mod_Panel.Mod
{
    public override string Name => "Iron Monke (LG & RG)";
    
    public override void OnUpdate()
    {
        if (ControllerInputPoller.instance.rightGrab)
        {
            GorillaLocomotion.GTPlayer.Instance.bodyCollider.attachedRigidbody.velocity += GorillaTagger.Instance.rightHandTransform.transform.right * 7 * Time.deltaTime * GorillaLocomotion.GTPlayer.Instance.scale;
        }
        if (ControllerInputPoller.instance.leftGrab)
        {
            GorillaLocomotion.GTPlayer.Instance.bodyCollider.attachedRigidbody.velocity += GorillaTagger.Instance.leftHandTransform.transform.right * -7 * Time.deltaTime * GorillaLocomotion.GTPlayer.Instance.scale;
        }
    }
}