using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PageManager : MonoBehaviour
{
    public static int PageIndex = 0;
    public GameObject[] pages;
    public Button NextNav;
    public Button PrevNav;
    public Button BackToMenuButton;
    public VideoPlaybackControl videoPlaybackControl;
    public string desiredScene;
    private int currentPageIndex = 0;

    void Start()
    {
        currentPageIndex = PageIndex;
        UpdatePage();
        NextNav.onClick.AddListener(NextPage);
        PrevNav.onClick.AddListener(PreviousPage);
        BackToMenuButton.onClick.AddListener(BackToMenu);
    }

    void UpdatePage()
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == currentPageIndex);
        }

        // Set interactability based on current page index
        PrevNav.interactable = currentPageIndex > 0;

        // Ensure the buttons are always active (not set inactive)
        PrevNav.gameObject.SetActive(true);
        NextNav.gameObject.SetActive(true);

        // Call SetActiveContent with the current page index
        if (videoPlaybackControl != null)
        {
            videoPlaybackControl.SetActiveContent(currentPageIndex);
        }

        // Update the interactability of the NextNav button separately
        if (currentPageIndex < pages.Length - 1)
        {
            NextNav.interactable = true;
        }
        else
        {
            NextNav.interactable = true;
        }
    }

    void NextPage()
    {
        if (currentPageIndex < pages.Length - 1)
        {
            currentPageIndex++;
            UpdatePage();
        }
        else
        {
            // Load the desired scene when the last page is reached
            SceneManager.LoadScene(desiredScene);
        }
    }

    void PreviousPage()
    {
        if (currentPageIndex > 0)
        {
            currentPageIndex--;
            UpdatePage();
        }
    }

    void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
