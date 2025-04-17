using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Hallucination : MonoBehaviour
{   
    [Header("First Hallucination Settings")]
    public TMP_Text[] aisleTexts;
    public Camera mainCamera; // assign your 3rd person cam here
    public float duration = 20f;

    [Header("Second Hallucination Settings")]
    public GameObject musicSource;

    [Header("Third Hallucination Settings")]
    public ParticleSystem drippingEffect;
    public GameObject water;

    [Header("Final Hallucination Settings")]
    public GameObject universalLight;

    string[] originalTexts;
    Material originalMaterial;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalTexts = new string[aisleTexts.Length];
        for (int i = 0; i < aisleTexts.Length; i++)
        {
            originalTexts[i] = aisleTexts[i].text;
        }

        originalMaterial = water.GetComponent<Renderer>().material;
    }

    // number is the number of the hallucination you want to trigger
    public void TriggerHallucination(HallucinationType type)
    {   
        switch(type)
        {
            case HallucinationType.First:
                FirstHallucination();
                break;
            case HallucinationType.Second:
                SecondHallucination();
                break;
            case HallucinationType.Third:
                ThirdHallucination();
                break;
            case HallucinationType.Fifth:
                FinalHallucination();
                break;
            default:
                Debug.Log("Invalid Number");
                break;
        }

    }

    void FirstHallucination()
    {
        ShuffleSigns();
        StartCoroutine(RestoreHallucination(duration, RestoreSigns));
    }

    void SecondHallucination()
    {
        PauseMusic();
        StartCoroutine(RestoreHallucination(5f, PlayMusic));
    }

    void ThirdHallucination()
    {
        ChangeWater();
        StartCoroutine(RestoreHallucination(duration, RestoreWater));
    }

    void FinalHallucination()
    {
        BlinkingLights.ActivateHallucination();
        universalLight.SetActive(false);
        StartCoroutine(RestoreHallucination(duration, ResetLights));
    }


    void ShuffleSigns()
    {
        for (int i = aisleTexts.Length - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);

            // Swap text values between signTexts[i] and signTexts[j]
            string temp = aisleTexts[i].text;
            aisleTexts[i].text = aisleTexts[j].text;
            aisleTexts[j].text = temp;
        }
    }

    void ChangeWater()
    {
        water.GetComponent<Renderer>().material.color = Color.red;
        var main = drippingEffect.main;
        main.startColor = Color.red;
    }

    void PauseMusic()
    {
        musicSource.GetComponent<AudioSource>().Pause();
    }

    void RestoreSigns()
    {
        for (int i = 0; i < aisleTexts.Length; i++)
        {
            aisleTexts[i].text = originalTexts[i];
        }
    }

    void RestoreWater()
    {
        water.GetComponent<Renderer>().material.color = originalMaterial.color;
        var main = drippingEffect.main;
        main.startColor = originalMaterial.color;
    }

    void PlayMusic()
    {
        musicSource.GetComponent<AudioSource>().Play();
    }

    void ResetLights()
    {
        BlinkingLights.ActivateHallucination();
        universalLight.SetActive(true);
    }

    private IEnumerator RestoreHallucination(float delay, Action action)
    {
        yield return new WaitForSeconds(delay); // Wait for the given duration
        action?.Invoke(); // invoke specified "reset" function
    }
}
