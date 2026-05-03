using Monke_Mod_Panel.Attributes;
using UnityEngine;
using GorillaLibrary.Utilities;

namespace ModTemplate;
// omg my first comment ever :P thank you buttplug15 (blue planet)
[Toggleable]
public class tpGun : Monke_Mod_Panel.Mod
{
    public override string Name => "Teleport Gun";
    private GameObject tpball;
    public float maxDistance = 100f;
    public bool enablayd;

    public override void OnEnable()
    {
        enablayd = true;
        tpball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        tpball.GetComponent<Renderer>().material = new Material(Shader.Find("GorillaTag/UberShader"));
        tpball.GetComponent<Renderer>().material.color = Color.blue;
        tpball.GetComponent<Collider>().Destroy();
        tpball.transform.localScale =  new Vector3(0.3f, 0.3f, 0.3f);
    }

    public override void OnDisable()
    {
        enablayd = false;
        tpball.Destroy();
    }
    public override void OnModdedJoin() {}
    public override void OnModdedLeave() { OnDisable(); }
    
    private RaycastHit hitData;
    public override void OnUpdate()
    {
        
        Vector3 origin = GorillaTagger.Instance.rightHandTransform.position;
        Vector3 direction = GorillaTagger.Instance.rightHandTransform.forward;
        float rightGripFloat = ControllerInputPoller.instance.rightControllerGripFloat;
        float rightTrigFloat = ControllerInputPoller.instance.rightControllerIndexFloat;
        Ray ray = new Ray(origin, direction);
        

        if (Physics.Raycast(ray, out hitData, maxDistance) && enablayd)
        {
            Vector3 hitPoint = hitData.point;
            
            if (rightGripFloat > 0.7f )
            {
                if (InputUtility.RightTrigger.GetValue() > 0.7f)
                {
                    GorillaLocomotion.GTPlayer.Instance.TeleportTo(tpball.transform.position, Quaternion.identity);
                    tpball.GetComponent<Renderer>().material.color = Color.green;
                }
                else
                {
                    tpball.transform.position = hitPoint;
                    tpball.GetComponent<Renderer>().material.color = Color.blue;
                }
                
            }
        }
    }
    
}