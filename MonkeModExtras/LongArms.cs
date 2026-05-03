using Monke_Mod_Panel.Attributes;
using UnityEngine;
using GorillaLibrary.Attributes;


namespace ModTemplate;

[Toggleable]

public class LongArms : Monke_Mod_Panel.Mod
{
    
    public override string Name => "Long Arms";
    
    
    public override void OnUpdate()
    {
        if (ControllerInputPoller.instance.rightControllerSecondaryButton)
        {
            GorillaLocomotion.GTPlayer.Instance.transform.localScale = new  Vector3(1f, 1f, 1f);
        }

        
        if (ControllerInputPoller.instance.rightControllerPrimaryButton)
        {
            
            GorillaLocomotion.GTPlayer.Instance.transform.localScale = new  Vector3(2f, 2f, 2f);
            
        }
        
    }

    public override void OnDisable()
    {
        GorillaLocomotion.GTPlayer.Instance.transform.localScale = new  Vector3(1f, 1f, 1f);
    }
    
    
    public override void OnModdedLeave()
    {
        GorillaLocomotion.GTPlayer.Instance.transform.localScale = new  Vector3(1f, 1f, 1f);
    }
}