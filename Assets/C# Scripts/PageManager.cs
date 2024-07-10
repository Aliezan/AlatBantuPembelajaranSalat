using System.Collections;
using System.Collections.Generic;
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
        NextNav.interactable = currentPageIndex < pages.Length - 1;

        // Ensure the buttons are always active (not set inactive)
        PrevNav.gameObject.SetActive(true);
        NextNav.gameObject.SetActive(true);

        // Call SetActiveContent with the current page index
        if (videoPlaybackControl != null)
        {
            videoPlaybackControl.SetActiveContent(currentPageIndex);
        }
    }

    void NextPage()
    {
        if (currentPageIndex < pages.Length - 1)
        {
            currentPageIndex++;
            UpdatePage();
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
