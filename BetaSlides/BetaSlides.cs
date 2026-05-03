using MelonLoader;
using Monke_Mod_Panel.Attributes;
using UnityEngine;
using GorillaLibrary.Utilities;
using GorillaLibrary.Models;

namespace ModTemplate;

/// <summary>
/// Creates an example mod usable by MonkeModPanel
/// If you want this mod to be toggleable, add [Toggleable] above the class.
/// </summary>
[Toggleable]
public class BetaSlides : Monke_Mod_Panel.Mod
{
    public AssetBundle Bundle;
    public override string Name => "Beta Slides";
    public bool enabled = false;
    public GameObject slides;
    public GameObject slide1 = null;
    public GameObject slide2 = null;
    public GameObject slide3 = null;
    public GameObject slidet1 = null;
    public GameObject slidet2 = null;
    public GameObject slidet3 = null;
    public override void OnModdedJoin()
    {
        Bundle = AssetBundleUtility.LoadBundle(System.Reflection.Assembly.GetExecutingAssembly(),
            "ModTemplate.Resources.talkingslidethatdog");
        slides = Object.Instantiate(Bundle.LoadAsset<GameObject>("messy"));
        slides.transform.position = new Vector3(-67.0938f, 11.7505f, -82.6406f);
        
        slide1 = GameObject.Find("bigslidespiralslide");
        slide2 = GameObject.Find("bigslideslide");
        slide3 = GameObject.Find("bigslideslide2");
        slide1.AddComponent<GorillaSurfaceOverride>().overrideIndex = 59;
        slide2.AddComponent<GorillaSurfaceOverride>().overrideIndex = 59;
        slide3.AddComponent<GorillaSurfaceOverride>().overrideIndex = 59;
        slidet1 = GameObject.Find("bigslidestructurespiral");
        slidet2 = GameObject.Find("bigslidestructure");
        slidet3 = GameObject.Find("bigslidestructure2");
        slidet1.AddComponent<GorillaSurfaceOverride>().overrideIndex = 26;
        slidet2.AddComponent<GorillaSurfaceOverride>().overrideIndex = 26;
        slidet3.AddComponent<GorillaSurfaceOverride>().overrideIndex = 26;
            slides.SetActive(false);
        MelonLogger.Msg("this WORKED on load bundle");


    }

    public override void OnEnable()
    {
        enabled = true;
        slides.SetActive(true);
    }

    public override void OnDisable()
    {
        enabled = false;
        slides.SetActive(false);
    }

    public override void OnModdedLeave()
    {
        slides.SetActive(false);
    }
}
    
