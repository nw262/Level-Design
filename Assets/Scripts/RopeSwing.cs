using UnityEngine;

public class RopeSwing : MonoBehaviour
{
    public GameObject ropeBottom;
    public float swingSpeed = 2f;
    public Transform player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = - Input.GetAxis("Vertical");

        Rigidbody ropeBottomRB = ropeBottom.GetComponent<Rigidbody>();
        ropeBottomRB.AddForce(transform.forward * horizontal * swingSpeed, ForceMode.Acceleration);
        ropeBottomRB.AddForce(transform.right * vertical * swingSpeed, ForceMode.Acceleration);

        player.position = new Vector3(ropeBottom.transform.position.x, ropeBottom.transform.position.y, ropeBottom.transform.position.z);
        //Debug.Log(player.GetComponent<Rigidbody>().linearVelocity);
        //Debug.Log(ropeBottomRB.linearVelocity);
    }
}
