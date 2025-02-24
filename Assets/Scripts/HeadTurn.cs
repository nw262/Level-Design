using UnityEngine;

public class HeadTurn : MonoBehaviour
{
    public Transform target;
    public static bool ShouldLook { get; set; }

    private Quaternion origRot;
    private Quaternion lastLooking;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        origRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (ShouldLook)
        {
            transform.LookAt(target);
            lastLooking = transform.rotation;
        }
        else
        {
            //Debug.Log(transform.rotation);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, origRot, 3 * Time.deltaTime);
        }
    }
}
