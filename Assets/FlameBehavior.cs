using UnityEngine;

public class FlameBehavior : MonoBehaviour
{

    public Color color1 = Color.yellow;
    public Color color2 = Color.red;
    public float changeSpeed = 1;

    private Renderer renderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float step = Mathf.PingPong(Time.time * changeSpeed, 1);
        renderer.material.color = Color.Lerp(color1, color2, step);
    }
}
