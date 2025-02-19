using UnityEngine;

public class PlayerMovement3 : MonoBehaviour
{
    public float speed = 10;
    public float jumpForce = 2;
    public Transform orientation;
    public Rigidbody rigidbody;
    public AudioSource audioSource;
    public AudioClip bounceSFX;

    public static bool OnRope { get; set; }

    private float xInput;
    private float yInput;
    private Vector3 moveDirection;
    [SerializeField] private bool isGrounded;

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
        if (OnRope)
        {
            //Debug.Log("OnRope");
            return;
        }

        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        moveDirection = orientation.forward * yInput + orientation.right * xInput;

        if (isGrounded)
        {
            rigidbody.linearVelocity = new Vector3(moveDirection.x * speed, rigidbody.linearVelocity.y, moveDirection.z * speed);
        }
        else
        {
            rigidbody.linearVelocity = new Vector3(moveDirection.x * speed / 2, rigidbody.linearVelocity.y, moveDirection.z * speed / 2);
        }

        Jump();
    }

    void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else if (collision.gameObject.CompareTag("Trampoline"))
        {
            PlayAudioClip(bounceSFX);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("FoamPit"))
        {
            speed = speed / 2;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("FoamPit"))
        {
            speed = speed * 2;
        }
    }

    void PlayAudioClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
