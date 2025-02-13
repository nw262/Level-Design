using UnityEngine;
using UnityEngine.UI;

public class JournalPageTurner : MonoBehaviour
{   

    public GameObject[] pages; // store all pages of the journal
    private int currentPageIndex = 0; // start at first page

    public Button nextPageButton;
    public Button previousPageButton;
    public Button coverButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        UpdatePages();
    }

    public void NextPage()
    {   
        if (currentPageIndex < pages.Length - 1)
        {
            currentPageIndex++;
            UpdatePages();
        }
    }

    public void PreviousPage()
    {
        if (currentPageIndex > 0)
        {
            currentPageIndex--;
            UpdatePages();
        }
    }

    private void UpdatePages()
    {
        // Enable only the active page
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == currentPageIndex);
        }

        // Enable/disable buttons based on available pages
        coverButton.interactable = currentPageIndex == 0;
        previousPageButton.interactable = currentPageIndex > 0;
        nextPageButton.interactable = currentPageIndex < pages.Length - 1 && currentPageIndex != 0;
    }
}
