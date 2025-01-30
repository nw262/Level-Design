using UnityEngine;
using UnityEngine.UI; 
using UnityEngineInternal;

public class Fireplace : Interactable
{
    public Text prompt; // UI element that shows up when player is capable of interacting
    private float nothing;

    private bool fireOn;
    private ParticleSystem fireParticles;
    private Light fireLight;
    private AudioSource sound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fireOn = false;

        // initialize text pop-up
        prompt.enabled = false;
        prompt.color = Color.red;
        prompt.text = "Press 'F' to light.";

        // make sure fire is turned off
        fireParticles = gameObject.GetComponentInChildren<ParticleSystem>();
        fireLight = gameObject.GetComponentInChildren<Light>();
        sound = gameObject.GetComponentInChildren<AudioSource>();

        fireParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        fireLight.enabled = false;
        sound.enabled = false;

    }

    public override void OnInteract()
    {
        // activate!!
        if (!fireOn) {
            fireParticles.Play();
            fireLight.enabled = true;
            prompt.enabled = false;
            fireOn = true;
            sound.enabled = true;

        }
    }

    public override void OnLook()
    {   
        // show prompt on look
        if (!fireOn) 
        {
            prompt.enabled = true;
        }
    }

    public override void OnLookAway()
    {   
        // get rid of prompt when looking away
        prompt.enabled = false;
    }
}
