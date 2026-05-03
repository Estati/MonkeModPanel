using Monke_Mod_Panel.Attributes;
using UnityEngine;

namespace ModTemplate;

[Toggleable]
public class SpiderMonke : Monke_Mod_Panel.Mod
{
    public override string Name => "Spider Monke";

    private SpringJoint rightJoint, leftJoint;
    private LineRenderer rightLR, leftLR;
    private Vector3 rightPoint, leftPoint;
    private GameObject rightBall, leftBall;
    private bool inModded = false;

    void Setup()
    {
        rightLR = GorillaLocomotion.GTPlayer.Instance.gameObject.AddComponent<LineRenderer>();
        leftLR = new GameObject().AddComponent<LineRenderer>();
        foreach (var lr in new[] { rightLR, leftLR })
        {
            lr.material = new Material(Shader.Find("Sprites/Default"));
            lr.startWidth = lr.endWidth = 0.02f;
            lr.positionCount = 0;
        }

        rightBall = CreateBall();
        leftBall = CreateBall();
    }

    void Teardown()
    {
        Object.Destroy(rightJoint); Object.Destroy(leftJoint);
        Object.Destroy(rightLR); Object.Destroy(leftLR);
        Object.Destroy(rightBall); Object.Destroy(leftBall);
    }

    GameObject CreateBall()
    {
        var ball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        ball.transform.localScale = Vector3.one * 0.1f;
        ball.GetComponent<Collider>().enabled = false;
        ball.GetComponent<Renderer>().material = new Material(Shader.Find("GorillaTag/UberShader"));
        ball.SetActive(false);
        return ball;
    }

    public override void OnEnable() { if (inModded) Setup(); }
    public override void OnDisable() { if (inModded) Teardown(); }
    public override void OnModdedJoin() { inModded = true; if (Enabled) Setup(); }
    public override void OnModdedLeave() { inModded = false; Teardown(); }

    public override void OnUpdate()
    {
        if (!inModded) return;
        Grapple(ControllerInputPoller.instance.rightControllerIndexFloat, GorillaTagger.Instance.rightHandTransform, ref rightJoint, rightLR, ref rightPoint, rightBall);
        Grapple(ControllerInputPoller.instance.leftControllerIndexFloat, GorillaTagger.Instance.leftHandTransform, ref leftJoint, leftLR, ref leftPoint, leftBall);
    }

    void Grapple(float trigger, Transform hand, ref SpringJoint joint, LineRenderer lr, ref Vector3 point, GameObject ball)
    {
        if (trigger > 0.1f)
        {
            if (!joint && Physics.Raycast(hand.position, hand.forward, out RaycastHit hit, 100f))
            {
                point = hit.point;
                joint = GorillaLocomotion.GTPlayer.Instance.gameObject.AddComponent<SpringJoint>();
                joint.autoConfigureConnectedAnchor = false;
                joint.connectedAnchor = point;
                float dist = Vector3.Distance(GorillaLocomotion.GTPlayer.Instance.bodyCollider.attachedRigidbody.position, point);
                joint.maxDistance = dist * 0.8f;
                joint.minDistance = dist * 0.25f;
                joint.spring = 20f; joint.damper = 21f; joint.massScale = 12f;
                lr.positionCount = 2;
            }
            ball.SetActive(false);
            if (joint) { lr.SetPosition(0, hand.position); lr.SetPosition(1, point); }
        }
        else
        {
            Object.Destroy(joint); joint = null;
            lr.positionCount = 0;
            if (Physics.Raycast(hand.position, hand.forward, out RaycastHit aimHit, 100f))
            {
                ball.transform.position = aimHit.point;
                ball.SetActive(true);
            }
            else
            {
                ball.SetActive(false);
            }
        }
    }
}