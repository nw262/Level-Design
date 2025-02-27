using UnityEngine;

public class MovingTerrain : MonoBehaviour
{
    public float speed = 50f; // Adjust speed to match train movement
    public float resetPosition = -100f; // Position at which the terrain resets
    public float startPosition = 100f;  // Starting position of the terrain

    void Update()
    {
        // Move the terrain backward
        transform.position += Vector3.back * speed * Time.deltaTime;

        // Reset terrain position when it moves past the reset point
        if (transform.position.z <= resetPosition)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, startPosition);
        }
    }
}
