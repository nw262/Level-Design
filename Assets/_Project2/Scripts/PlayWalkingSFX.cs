using UnityEngine;

public class PlayWalkingSFX : MonoBehaviour
{
    public AudioSource walking;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            walking.enabled = true;
        }
        else
        {
            walking.enabled = false;
        }
    }
}
