using System.Collections;
using UnityEngine;

public class CashierBehavior : MonoBehaviour
{   
    public Hallucination hallucinationManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnAround()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Trigger");

        StartCoroutine(DelayedHallucination());
    }

    private IEnumerator DelayedHallucination()
    {
        hallucinationManager.PauseMusic();
        yield return new WaitForSeconds(3f);
        hallucinationManager.TriggerHallucination(HallucinationType.Fifth);
    }
}
