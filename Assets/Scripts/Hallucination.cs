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

    [Header("Final Hallucination Settings")]
    public GameObject universalLight;

    string[] originalTexts;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalTexts = new string[aisleTexts.Length];
        for (int i = 0; i < aisleTexts.Length; i++)
        {
            originalTexts[i] = aisleTexts[i].text;
        }

    }

    // number is the number of the hallucination you want to trigger
    public void TriggerHallucination(HallucinationType type)
    {   
        switch(type)
        {
            case HallucinationType.First:
                FirstHallucination();
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
        StartCoroutine(RestoreSignsAfterDelay(duration, RestoreSigns));
    }

    void FinalHallucination()
    {
        BlinkingLights.ActivateHallucination();
        universalLight.SetActive(false);
        StartCoroutine(RestoreSignsAfterDelay(duration, ResetLights));
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

    void RestoreSigns()
    {
        for (int i = 0; i < aisleTexts.Length; i++)
        {
            aisleTexts[i].text = originalTexts[i];
        }
    }

    void ResetLights()
    {
        BlinkingLights.ActivateHallucination();
        universalLight.SetActive(true);
    }

    private IEnumerator RestoreSignsAfterDelay(float delay, Action action)
    {
        yield return new WaitForSeconds(delay); // Wait for the given duration
        action?.Invoke(); // invoke specified "reset" function
    }
}
