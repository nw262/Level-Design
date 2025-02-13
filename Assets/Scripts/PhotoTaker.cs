using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

// main script for taking photos
// left click to take a photo which takes a screenshot of the screen and displays it as a textured 2D image
public class PhotoTaker : MonoBehaviour
{
    [SerializeField] private Image photoArea;
    [SerializeField] private GameObject photoFrame;
    [SerializeField] private List<GameObject> uiElements;
    [SerializeField] private PhotoController controller;

    private Texture2D screenCapture;
    private bool viewingPhoto;

    [Header("Flash Effect")]
    [SerializeField] private GameObject cameraFlash;
    [SerializeField] private float flashTime;

    [Header("Fade-in Effect")]
    [SerializeField] private Animator fadingAnimation;

    [Header("Photo Saving")]
    public GameObject[] pages; // store all pages of the journal
    private int currentIdx = 0;
    private int currPage = 0;
    private Image[] photos;
    private Image[] masks;

    private void Start()
    {
        screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        photos = pages[0].GetComponentsInChildren<Image>(true);
        masks = photos.Where(c => c.gameObject.tag == "OldImage").ToArray();
        photos = photos.Where(c => c.gameObject.tag == "NewImage").ToArray();

    }

    private void Update()
    {   

        if (!controller.photoMode) return; // don't run this script if we are not in camera mode

        if (Input.GetMouseButtonDown(0))
        { 
            if (!viewingPhoto) 
            {
                StartCoroutine(CapturePhoto());
            }
            else
            {
                RemovePhoto();
            }

        }
    }

    // captures the current view
    IEnumerator CapturePhoto()
    {   
        // Set all UI elements to false
        foreach (GameObject element in uiElements) element.SetActive(false);
    
        viewingPhoto = true;

        yield return new WaitForEndOfFrame();

        Rect regionToRead = new Rect(0, 0, Screen.width, Screen.height);

        screenCapture.ReadPixels(regionToRead, 0, 0, false);  
        screenCapture.Apply();
        ShowPhoto();
    }

    void ShowPhoto()
    {
        Sprite photoSprite = Sprite.Create(screenCapture, new Rect(0.0f, 0.0f, screenCapture.width, screenCapture.height), new Vector2(0.5f, 0.5f), 100.0f);
        photoArea.sprite = photoSprite;

        photoFrame.SetActive(true);

        // if no more room on current page, reset everything and go to next page
        if (currentIdx > photos.Length - 1)
        {
            currPage++;
            photos = pages[currPage].GetComponentsInChildren<Image>(true);
            masks = photos.Where(c => c.gameObject.tag == "OldImage").ToArray();
            photos = photos.Where(c => c.gameObject.tag == "NewImage").ToArray();
            currentIdx = 0;
        }
        
        photos[currentIdx].sprite = photoSprite;
        masks[currentIdx].gameObject.SetActive(true);

        currentIdx++;

        StartCoroutine(FlashEffect());
        fadingAnimation.Play("PhotoFade");
    }

    IEnumerator FlashEffect()
    {
        cameraFlash.SetActive(true);
        // wait for flash to end
        yield return new WaitForSeconds(flashTime);
        cameraFlash.SetActive(false);
    }

    void RemovePhoto()
    {
        viewingPhoto = false;
        photoFrame.SetActive(false);

        // all UI elements to true
        foreach (GameObject element in uiElements) element.SetActive(true);

    }
}
