using MelonLoader;
using Monke_Mod_Panel.Attributes;
using UnityEngine;
using GorillaLibrary.Content.Utilities;
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

    public override void OnModdedJoin()
    {
        Bundle = AssetBundleUtility.LoadBundle(System.Reflection.Assembly.GetExecutingAssembly(),
            "ModTemplate.Resources.talkingslidethatdog");
        slides = Object.Instantiate(Bundle.LoadAsset<GameObject>("messy"));
        slides.transform.position = new Vector3(-67.0938f, 11.7505f, -82.6406f);
        MelonLogger.Msg("this WORKED on load bundle");
        if (!enabled)
        {
            slides.SetActive(false);
        }
        else
        {
            slides.SetActive(true);
        }
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
    
