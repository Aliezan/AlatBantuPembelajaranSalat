using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
            Debug.Log("Listener added to button: " + button.name);
        }
    }

    public void CheckAnswer(Button selectedButton)
    {
        Debug.Log("Button clicked: " + selectedButton.name);

        if (correctAnswerButton == null)
        {
            Debug.LogError("Correct answer button is not assigned for the current page.");
            return;
        }

        Text selectedText = selectedButton.GetComponentInChildren<Text>();
        if (selectedText == null)
        {
            Debug.LogError("Selected button does not have a Text component.");
            return;
        }

        Text correctText = correctAnswerButton.GetComponentInChildren<Text>();
        if (correctText == null)
        {
            Debug.LogError("Correct answer button does not have a Text component.");
            return;
        }

        if (selectedButton == correctAnswerButton)
        {
            selectedText.color = Color.green;
        }
        else
        {
            selectedText.color = Color.red;
            correctText.color = Color.green;
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
