using System.Collections;
using TMPro;
using UnityEngine;

public class Hallucination : MonoBehaviour
{   
    [Header("First Hallucination Settings")]
    public TMP_Text[] aisleTexts;
    public Camera mainCamera; // assign your 3rd person cam here
    public float duration = 20f;
    
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
            default:
                Debug.Log("Invalid Number");
                break;
        }

    }

    void FirstHallucination()
    {
        ShuffleSigns();
        StartCoroutine(RestoreSignsAfterDelay(duration));
    }


    void ShuffleSigns()
    {
        for (int i = aisleTexts.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);

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

    private IEnumerator RestoreSignsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the given duration
        RestoreSigns(); // Restore the original texts after the wait time
    }
}
