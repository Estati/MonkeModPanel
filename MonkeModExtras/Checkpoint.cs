using Monke_Mod_Panel.Attributes;
using UnityEngine;
using GorillaLibrary.Utilities;

namespace ModTemplate;

[Toggleable]
public class Checkpoint : Monke_Mod_Panel.Mod
{
    public override string Name => "Checkpoint";
    private GameObject checkpointer;
    public float maxDistance = 100f;
    public bool enablayd;

    public override void OnEnable()
    {
        enablayd = true;
        checkpointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        checkpointer.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        checkpointer.GetComponent<Renderer>().material = new Material(Shader.Find("GorillaTag/UberShader"));
        checkpointer.GetComponent<Renderer>().material.color = Color.purple;
        checkpointer.GetComponent<Collider>().Destroy();
        
    }

    public override void OnDisable()
    {
        enablayd = false;
        checkpointer.Destroy();
    }
    public override void OnModdedJoin() {}
    public override void OnModdedLeave() { OnDisable(); }


    public override void OnUpdate()
    {
        if (enablayd)
        {
            if (InputUtility.RightSecondary.pressed)
            {
                GorillaLocomotion.GTPlayer.Instance.TeleportTo(checkpointer.transform.position, Quaternion.identity);
                checkpointer.GetComponent<Renderer>().material.color = Color.green;
            }

            if (InputUtility.RightGrip.GetValue() > 0.7f && !InputUtility.RightSecondary.pressed)
            {

                checkpointer.transform.position = GorillaTagger.Instance.rightHandTransform.position;
                checkpointer.GetComponent<Renderer>().material.color = Color.purple;
            }
            else if (!InputUtility.RightSecondary.pressed)

            {
                checkpointer.GetComponent<Renderer>().material.color = Color.blue;
            }
        }
    }
}