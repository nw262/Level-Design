using UnityEngine;

public class RopeInteraction : Interactable
{
    public RopeSwing ropeSwing;
    public GameObject playerBody;
    public GameObject player;
    public GameObject ropeBottom;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Interaction

    // Called when the player looks at the object
    public override void OnLook()
    {
        //Debug.Log("looked at");
    }

    // Called when the player interacts with the object
    public override void OnInteract()
    {
        Debug.Log("interacted");
        //Debug.Log("ropeSwing: " + ropeSwing);
        if (ropeSwing)
        {
            playerBody.GetComponent<CapsuleCollider>().enabled = false;
            PlayerMovement3.OnRope = true;
            Debug.Log(PlayerMovement3.OnRope);
            ropeSwing.enabled = true;
        }
        else
        {
            Debug.Log("No ropeswing script found");
        }
    }

    // Called when the player stops looking at the object
    public override void OnLookAway()
    {
        //Debug.Log("looked away");
    }

    public override void OnStop()
    {
        if (ropeSwing)
        {
            playerBody.GetComponent<CapsuleCollider>().enabled = true;
            PlayerMovement3.OnRope = false;
            Debug.Log(PlayerMovement3.OnRope);
            ropeSwing.enabled = false;
            player.GetComponent<Rigidbody>().linearVelocity = new Vector3(0f, 0f, ropeBottom.GetComponent<Rigidbody>().linearVelocity.y);
                //-= new Vector3(0f, player.GetComponent<Rigidbody>().linearVelocity.y, 0f);
        }
        else
        {
            Debug.Log("No ropeswing script found");
        }
    }
}
