using UnityEngine;
using Unity.Cinemachine;

public class ThirdPersonController : MonoBehaviour
{
    public float speed = 10f;
    public float gravity = 9.81f;
    public CharacterController controller;
    public CinemachineThirdPersonFollow followCM;
    public float cameraFollowDistance;

    Vector3 input;
    Vector3 moveDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        followCM.ShoulderOffset = new Vector3(0.8f, 0.3f, cameraFollowDistance);
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        input = transform.right * moveHorizontal + transform.forward * moveVertical;
        input.Normalize();

        if (controller.isGrounded)
        {
            moveDirection = input;
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * speed * Time.deltaTime);
    }

    /*
    public float speed = 10f;
    public float gravity = 9.81f;
    public float rotationSpeed = 5;
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
        
        input = new Vector3(moveHorizontal, 0, moveVertical);
        input.Normalize();

        moveDirection = input;

        if (input.magnitude >= 0.1f)
        {
            float rotationAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, rotationAngle, 0);

            Vector3 moveDir = Quaternion.Euler(0, rotationAngle, 0) * Vector3.forward;
            moveDirection = moveDir.normalized * rotationSpeed;
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * speed * Time.deltaTime);
    }
    */
}