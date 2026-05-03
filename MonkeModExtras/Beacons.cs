using Monke_Mod_Panel.Attributes;
using UnityEngine;
using System.Collections.Generic;

namespace ModTemplate;

[Toggleable]
public class Beacons : Monke_Mod_Panel.Mod
{
    public override string Name => "Beacons";
    public bool beaconson = false;
    public List<GameObject> myBeacons = new List<GameObject>();
    public VRRig[] allRigs;
    public float rigUpdateTimer = 0f;

    public override void OnEnable()
    {
        beaconson = true;
        rigUpdateTimer = 2f;
    }

    public override void OnDisable()
    {
        beaconson = false;
        for (int i = 0; i < myBeacons.Count; i++)
        {
            GameObject.Destroy(myBeacons[i]);
        }
        myBeacons.Clear();
    }

    public override void OnModdedJoin() {}

    public override void OnModdedLeave()
    {
        beaconson = false;
        for (int i = 0; i < myBeacons.Count; i++)
        {
            GameObject.Destroy(myBeacons[i]);
        }
        myBeacons.Clear();
    }

    public override void OnUpdate()
    {
        if (beaconson)
        {
            rigUpdateTimer += Time.deltaTime;
            if (rigUpdateTimer >= 2f || allRigs == null)
            {
                rigUpdateTimer = 0f;
                allRigs = (VRRig[])GameObject.FindObjectsOfType(typeof(VRRig));

                for (int i = 0; i < myBeacons.Count; i++)
                {
                    GameObject.Destroy(myBeacons[i]);
                }
                myBeacons.Clear();

                for (int i = 0; i < allRigs.Length; i++)
                {
                    if (allRigs[i].isOfflineVRRig || allRigs[i].isMyPlayer)
                        continue;

                    GameObject beacon = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                    GameObject.Destroy(beacon.GetComponent<Collider>());
                    beacon.transform.localScale = new Vector3(0.04f, 200f, 0.04f);
                    beacon.GetComponent<MeshRenderer>().material = allRigs[i].mainSkin.material;
                    myBeacons.Add(beacon);
                }
            }

            int beaconIndex = 0;
            for (int i = 0; i < allRigs.Length; i++)
            {
                if (allRigs[i].isOfflineVRRig || allRigs[i].isMyPlayer)
                    continue;

                if (beaconIndex < myBeacons.Count)
                {
                    myBeacons[beaconIndex].transform.position = allRigs[i].transform.position;
                    beaconIndex++;
                }
            }
        }
    }
}