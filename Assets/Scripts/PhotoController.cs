using UnityEditor.Rendering;
using UnityEngine;

public class PhotoController : MonoBehaviour
{   

    public bool photoMode = false;

    [SerializeField] GameObject cameraUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            photoMode = !photoMode;
            cameraUI.SetActive(photoMode);
        }
    }
}
