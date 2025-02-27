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
        if (!target)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        origRot = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (ShouldLook)
        {
            Vector3 lookPosition = target.position - transform.position;
            //lookPosition.x = 0;
            lookPosition.y= 0;
            lookPosition.z = -Mathf.Abs(lookPosition.z);
            Debug.Log(lookPosition);
            transform.rotation = Quaternion.LookRotation(lookPosition, Vector3.left);
            //transform.LookAt(target, Vector3.left);
            //transform.localRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z);
            lastLooking = transform.rotation;
        }
        else
        {
            //Debug.Log(transform.rotation);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, origRot, 3 * Time.deltaTime);
        }
    }
}
