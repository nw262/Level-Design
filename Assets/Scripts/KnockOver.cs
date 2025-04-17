using UnityEngine;

public class KnockOver : MonoBehaviour
{   
    public float force = 100f;

    public GameObject[] items;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var item in items)
        {
            Rigidbody[] rigidbodies = item.GetComponentsInChildren<Rigidbody>();

            foreach (var crb in rigidbodies)
            {
                crb.isKinematic = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fall()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = false;

        // rb.AddTorque(transform.right * Random.Range(200, 400)); // or transform.forward
        rb.AddTorque(transform.forward * 75f, ForceMode.Impulse);

        foreach (var item in items)
        {
            Rigidbody[] rigidbodies = item.GetComponentsInChildren<Rigidbody>();

            foreach (var crb in rigidbodies)
            {
                crb.isKinematic = false;
            }
        }
    }
}
