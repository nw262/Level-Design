using UnityEngine;

public class WaterSounds : MonoBehaviour
{
    public int maxDistance = 10;

    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Access the player's position and the AudioSource of the sound
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        float distance = Vector3.Distance(transform.position, playerTransform.position);

        // Calculate the volume based on distance
        float volume = Mathf.Lerp(0.01f, 1f, 1 - (distance / maxDistance));


        Debug.Log(distance + " " + volume);
        audioSource.volume = volume;
    }
}
