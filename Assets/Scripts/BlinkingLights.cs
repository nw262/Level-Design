using UnityEngine;

public class BlinkingLights : MonoBehaviour
{
    public GameObject[] lights;
    public GameObject emissionLight;

    private Material emissionMaterial;
    private float blinkSpeed = 0.1f; // How fast the lights blink
    private float timer;

    public static bool hallucinationActive { get; private set; } = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        emissionMaterial = emissionLight.GetComponent<Renderer>().material;

    }

    void Update()
    {   
        if (!hallucinationActive)
            return;

        timer += Time.deltaTime;

        if (timer >= blinkSpeed)
        {
            timer = 0f;

            // Toggle emission ON or OFF
            bool isOn = Random.value > 0.5f;
            Color baseColor = isOn ? Color.white * Mathf.LinearToGammaSpace(3f) : Color.black;
            emissionMaterial.SetColor("_EmissionColor", baseColor);

            // Toggle each light GameObject
            foreach (GameObject lightObj in lights)
            {
                lightObj.SetActive(isOn);
            }
        }
    }

    public static void ActivateHallucination()
    {
        hallucinationActive = !hallucinationActive;
    }
}
