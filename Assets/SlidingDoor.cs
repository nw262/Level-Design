using UnityEngine;
using UnityEngine.UI;

public class SlidingDoor : Interactable
{

    public Text prompt; // UI element that shows up when player is capable of interacting
    public Transform endPosition;

    public float slideSpeed = 5f;
    public AudioClip slideSFX;
    public float timeBeforeClose = 3f;


    private Vector3 startPosition;
    private bool isOpening = false;
    private bool isClosing = false;
    private float closeTimer = 0f;
    private bool timeExpired = false;

    public override void OnInteract()
    {
        OpenDoor();
    }

    public override void OnLook()
    {   
        prompt.enabled = true;
    }

    public override void OnLookAway()
    {
        prompt.enabled = false;
    }

    public override void OnStop()
    {
        // do nothing
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        prompt.text = "F to open.";
        prompt.enabled = false;
        prompt.color = Color.red;
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isOpening)
        {
            transform.position = Vector3.Lerp(transform.position, endPosition.position, Time.deltaTime * slideSpeed);
            if (Vector3.Distance(transform.position, endPosition.position) < 0.1f)
            {
                transform.position = endPosition.position;  // Ensure it reaches the exact position
                isOpening = false;
                closeTimer = timeBeforeClose;  // Start the timer once the door is open
            }
        }

        // If the door is closing, move it back to the closed position
        if (isClosing)
        {
            transform.position = Vector3.Lerp(transform.position, startPosition, Time.deltaTime * slideSpeed);
            if (Vector3.Distance(transform.position, startPosition) < 0.1f)
            {
                transform.position = startPosition;
                isClosing = false;
            }
        }
        // If the door is open and the timer reaches zero, close it
        if (closeTimer > 0)
        {
            closeTimer -= Time.deltaTime;  // Decrease the timer every second
            timeExpired = true;
        }

        else if (!isClosing && !isOpening && timeExpired)
        {   
            CloseDoor();  // Automatically close the door when timer expires
            timeExpired = false;
        }

    }

    void OpenDoor() 
    {   
        if (isOpening) return;

        isOpening = true;
        isClosing = false;
        closeTimer = 0f;
    }

    void CloseDoor()
    {
        isClosing = true;
        isOpening = false;
    }
}
