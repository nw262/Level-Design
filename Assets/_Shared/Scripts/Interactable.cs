using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    // Called when the player looks at the object
    public abstract void OnLook();

    // Called when the player interacts with the object
    public abstract void OnInteract();

    // Called when the player stops looking at the object
    public abstract void OnLookAway();
}
