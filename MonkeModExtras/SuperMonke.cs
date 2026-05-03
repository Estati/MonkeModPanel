using Monke_Mod_Panel.Attributes;
using UnityEngine;

namespace ModTemplate;

[Toggleable]
public class SuperMonke : Monke_Mod_Panel.Mod
{
    public override string Name => "Super Monke";
    public bool nograv = false;
    public bool debounce = false;
    public override void OnUpdate()
    {
        if (ControllerInputPoller.instance.rightControllerSecondaryButton)
        {
            GorillaLocomotion.GTPlayer.Instance.transform.position += GorillaTagger.Instance.headCollider.transform.forward * 12f * Time.deltaTime * GorillaLocomotion.GTPlayer.Instance.scale;
            GorillaLocomotion.GTPlayer.Instance.bodyCollider.attachedRigidbody.velocity = Vector3.zero;
        }

        bool pressed = ControllerInputPoller.instance.rightControllerPrimaryButton;
        if (pressed && !debounce)
        {
            
            nograv = !nograv;
            
        }
        debounce = pressed;
        
        if (nograv)
        {
            GorillaLocomotion.GTPlayer.Instance.bodyCollider.attachedRigidbody.velocity += GorillaLocomotion.GTPlayer.Instance.bodyCollider.transform.up * 9.9f * Time.deltaTime * GorillaLocomotion.GTPlayer.Instance.scale;
        }
    }
}