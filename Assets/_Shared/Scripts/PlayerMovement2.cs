using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    public float speed = 10f;
    public float jumpHeight = 2f;
    public float gravity = 9.81f;
    public CharacterController controller;

    Vector3 input;
    Vector3 moveDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        input = transform.right * moveHorizontal + transform.forward * moveVertical;
        input.Normalize();

        if(controller.isGrounded)
        {
            moveDirection = input;

            if(Input.GetButton("Jump"))
            {
                moveDirection.y = Mathf.Sqrt(2 * jumpHeight * gravity);
            }
            else
            {
                moveDirection.y = 0.0f;
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * speed * Time.deltaTime);
    }
}
