using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; // Make sure to include the TMPro namespace

public class QuizManager : MonoBehaviour
{
    public static int PageIndex = 0;
    public GameObject[] pages;
    public Button NextNav;
    public Button PrevNav;
    public Button BackToMenuButton;
    private int currentPageIndex = 0;

    // Correct answer button for each page
    public Button correctAnswerButton;

    void Start()
    {
        currentPageIndex = PageIndex;
        UpdatePage();
        NextNav.onClick.AddListener(NextPage);
        PrevNav.onClick.AddListener(PreviousPage);
        BackToMenuButton.onClick.AddListener(BackToMenu);

        // Add listeners to the buttons on the current page
        AddListenersToOptionButtons();
    }

    void UpdatePage()
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == currentPageIndex);
        }

        PrevNav.interactable = currentPageIndex > 0;

        PrevNav.gameObject.SetActive(true);
        NextNav.gameObject.SetActive(true);

        NextNav.interactable = currentPageIndex < pages.Length - 1;

        // Add listeners to the buttons on the current page
        AddListenersToOptionButtons();
    }

    void AddListenersToOptionButtons()
    {
        Button[] optionButtons = pages[currentPageIndex].GetComponentsInChildren<Button>();
        foreach (var button in optionButtons)
        {
            // Remove any existing listener to avoid multiple additions
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => CheckAnswer(button));
        }
    }

    public void CheckAnswer(Button selectedButton)
    {
        if (correctAnswerButton == null)
        {
            return;
        }

        // Get all TextMeshProUGUI components in the selected button
        TextMeshProUGUI[] selectedTexts = selectedButton.GetComponentsInChildren<TextMeshProUGUI>();

        // Get all TextMeshProUGUI components in the correct answer button
        TextMeshProUGUI[] correctTexts = correctAnswerButton.GetComponentsInChildren<TextMeshProUGUI>();

        if (selectedButton == correctAnswerButton)
        {
            // Set all texts in the selected button to green
            foreach (var text in selectedTexts)
            {
                text.color = Color.green;
            }
        }
        else
        {
            // Set all texts in the selected button to red
            foreach (var text in selectedTexts)
            {
                text.color = Color.red;
            }

            // Set all texts in the correct button to green
            foreach (var text in correctTexts)
            {
                text.color = Color.green;
            }
        }

        DisableAllOptionButtons();

        // Automatically move to the next page after a delay
        StartCoroutine(WaitAndLoadNextPage());
    }

    void DisableAllOptionButtons()
    {
        Button[] optionButtons = pages[currentPageIndex].GetComponentsInChildren<Button>();
        foreach (var button in optionButtons)
        {
            button.interactable = false;
        }
    }

    IEnumerator WaitAndLoadNextPage()
    {
        yield return new WaitForSeconds(2); // Wait for 2 seconds before loading the next page
        NextPage();
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
