using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 8f;
    public float drag;
    public Rigidbody rb;
    public Transform orientation;
    public GameObject player;

    float xInput;
    float yInput;
    Vector3 moveDirection;
    bool crouch;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Get movement input
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        // make sure speed doesn't keep going up with acceleration
        SpeedControl();

        // find direction the player is facing based on the sphere object attached
        moveDirection = orientation.forward * yInput + orientation.right * xInput;

        // add force to move player in correct direction
        rb.AddForce(moveDirection * speed, ForceMode.Force);

        // add drag to keep the player from sliding when they stop
        rb.linearDamping = drag;

        // crouch toggle
        if (Input.GetKeyDown("c"))
        {
            player.transform.localScale += new Vector3(0, -0.5f, 0);
            player.transform.position += new Vector3(0, -0.54f, 0);
            speed = speed / 2;
        }
        if (Input.GetKeyUp("c"))
        {
            player.transform.localScale += new Vector3(0, 0.5f, 0);
            player.transform.position += new Vector3(0, 0.54f, 0);
            speed = speed * 2;
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > speed)
        {
            Vector3 limitedVel = flatVel.normalized * speed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }
}
