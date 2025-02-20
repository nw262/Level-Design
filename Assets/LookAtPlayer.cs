using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{   

    public GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LookAtTarget();
    }

    void LookAtTarget() {
        if (player) {
            transform.LookAt(player.transform);
        }
    }
}
