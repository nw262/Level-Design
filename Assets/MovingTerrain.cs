using UnityEngine;

public class MovingTerrain : MonoBehaviour
{
    public float speed = 50f; // Movement speed
    public float terrainLength = 50f; // Length of the terrain piece

    void Update()
    {
        // Move terrain backward
        transform.position += Vector3.back * speed * Time.deltaTime;

        // Check if the terrain has moved past its reset point
        if (transform.position.z <= -terrainLength)
        {
            // Move it to the back of the last terrain piece
            transform.position += Vector3.forward * terrainLength * 2f;
        }
    }
}
