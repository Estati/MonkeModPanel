using MelonLoader;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

[assembly: MelonInfo(typeof(Monke_Mod_Panel.Core), "Monke Mod Panel", "1.0.0", "Estatic & biotest05", null)]
[assembly: MelonGame("Another Axiom", "Gorilla Tag")]

namespace Monke_Mod_Panel
{
    public class Core : MelonMod
    {
        GameObject menu;
        
        bool menuOpen = false;
        bool animating = false;
        private bool openRequested = false;

        Vector3 targetScale;
        Vector3 closedScale = Vector3.zero;
        Vector3 openScale = new Vector3(0.2f, 0.3f, 0.02f);

        float animSpeed = 8f;

        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("Initialized.");
            GorillaTagger.OnPlayerSpawned(CreateMenu);
            
            if (AudioUtil.GetClip("WristMenu.Resources.close.wav") == null)
                LoggerInstance.Error("Could not find WritstMenu.Resources.close.wav");
            
            if (AudioUtil.GetClip("WristMenu.Resources.open.wav") == null)
                LoggerInstance.Error("Could not find WritstMenu.Resources.open.wav");
        }

        public override void OnUpdate()
        {
            if (!menu) return;
            
            if (ControllerInputPoller.instance.leftGrab)
            {
                if (!openRequested)
                {
                    openRequested = true;
                    targetScale = openScale;
                    animating = true;
                    menu.SetActive(true);

                    AudioUtil.PlayClip("WristMenu.Resources.open.wav", menu.transform.position);
                }
            }
            else
            {
                if (openRequested)
                {
                    openRequested = false;
                    targetScale = closedScale;
                    animating = true;

                    AudioUtil.PlayClip("WristMenu.Resources.close.wav", menu.transform.position);
                }
            }
            
            if (animating)
            {
                animSpeed = targetScale == openScale ? 8f : 16f;
                
                menu.transform.localScale = Vector3.Lerp(
                    menu.transform.localScale,
                    targetScale,
                    1f - Mathf.Exp(-animSpeed * Time.deltaTime)
                );

                if (Vector3.Distance(menu.transform.localScale, targetScale) < 0.01f)
                {
                    menu.transform.localScale = targetScale;
                    animating = false;

                    if (targetScale == closedScale)
                        menu.SetActive(false);
                }
            }
        }

        void CreateMenu()
        {
            menu = GameObject.CreatePrimitive(PrimitiveType.Cube);
            
            menu.transform.localScale = closedScale;
            menu.transform.SetParent(GorillaTagger.Instance.leftHandTransform, false);
            menu.transform.localRotation = Quaternion.Euler(0, 90, 90);
            menu.transform.localPosition = new Vector3(0.05f, 0f, 0f);
            
            menu.GetComponent<Renderer>().material = new Material(Shader.Find("GorillaTag/UberShader"));
            menu.GetComponent<Renderer>().material.color = Color.black;
            
            GameObject.Destroy(menu.GetComponent<BoxCollider>());

            GameObject textObject = new GameObject("Title");
            textObject.transform.SetParent(menu.transform, false);

            TextMeshPro tmp = textObject.AddComponent<TextMeshPro>();
            tmp.text = "Monke Mod Panel";
            tmp.font = Resources.Load<TMP_FontAsset>("Fonts & Materials/LiberationSans SDF");
            tmp.color = Color.white;
            tmp.fontSize = 0.7f;
            tmp.fontStyle = FontStyles.Normal;
            tmp.alignment = TextAlignmentOptions.Center;

            textObject.transform.localScale = new Vector3(5f / 3f, 3f / 3f, 50f / 3f);
            textObject.transform.localRotation = Quaternion.Euler(0, 180, 0);
            textObject.transform.localPosition += new Vector3(0f, 0.45f, 1.01f);
            
            menu.SetActive(false);
        }
    }
}