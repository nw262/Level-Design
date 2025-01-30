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

    private float xInput;
    private float yInput;
    private Vector3 moveDirection;
    private bool crouch;
    private float origScaleY;
    private float origPosY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // hold original scale of the player when the game first starts
        origScaleY = player.transform.localScale.y;
        // hold the correct y pos for the player by getting half of its y scale
        origPosY = origScaleY / 2;

        // these are used to ensure that crouching doesn't bump the character up or down
        // and returns them to the same height
    }

    // Update is called each frame
    void Update()
    {
        // crouch toggle
        if (Input.GetKeyDown("c"))
        {
            // scale y by half
            player.transform.localScale += new Vector3(0, -(origScaleY / 2), 0);
            // lower pos to be at ground level
            player.transform.position += new Vector3(0, -origPosY, 0);
            // lower speed
            speed = speed / 2;
        }
        if (Input.GetKeyUp("c"))
        {
            // scale y back to original
            player.transform.localScale += new Vector3(0, (origScaleY / 2), 0);
            // raise y pos to be at ground level
            player.transform.position += new Vector3(0, origPosY / 2, 0);
            // put speed back
            speed = speed * 2;
        }
    }

    // FixedUpdate is called by each unit of time(?)
    void FixedUpdate()
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
