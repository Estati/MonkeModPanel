using UnityEngine;

namespace Monke_Mod_Panel;

public class ModButtonTrigger : MonoBehaviour
{
    public Mod Mod;
    public Renderer Renderer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Core.Instance.ButtonPresser)
        {
            AudioUtil.PlayClip("WristMenu.Resources.click.wav", transform.position);
            Mod.OnClicked();
            
            if (Mod.Toggleable)
                Renderer.material.color = Mod.Enabled ? Color.green : Color.red;
        }
    }
}