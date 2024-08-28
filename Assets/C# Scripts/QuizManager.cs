using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public static QuizManager Instance;
    public static int PageIndex = 0;
    public GameObject[] pages;
    public Button NextNav;
    public Button PrevNav;
    public Button BackToMenuButton;
    public Button ResetAnswerButton;
    private int currentPageIndex = 0;

    // Array to store correct answer buttons for each page
    public Button[] correctAnswerButtons;

    // GameObjects to render based on the answer correctness
    public GameObject GameObjectA;
    public GameObject GameObjectB;

    // Audio clips for correct and incorrect answers
    public AudioClip correctAnswerAudio;
    public AudioClip incorrectAnswerAudio;
    private AudioSource audioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        // Add AudioSource component if it doesn't exist
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Start()
    {
        currentPageIndex = PageIndex;
        UpdatePage();

        // Set GameObjectA and GameObjectB to inactive initially
        if (GameObjectA != null)
        {
            GameObjectA.SetActive(false);
        }
        if (GameObjectB != null)
        {
            GameObjectB.SetActive(false);
        }

        NextNav.onClick.AddListener(NextPage);
        PrevNav.onClick.AddListener(PreviousPage);
        BackToMenuButton.onClick.AddListener(BackToMenu);
        ResetAnswerButton.onClick.AddListener(ResetAllAnswers);
    }

    void UpdatePage()
    {
        // Hide GameObjects when changing pages
        if (GameObjectA != null)
        {
            GameObjectA.SetActive(false);
        }
        if (GameObjectB != null)
        {
            GameObjectB.SetActive(false);
        }

        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == currentPageIndex);
        }

        PrevNav.interactable = currentPageIndex > 0;
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
        Button correctAnswerButton = correctAnswerButtons[currentPageIndex];

        if (correctAnswerButton == null)
        {
            return;
        }

        // Get all TextMeshProUGUI components in the selected button
        TextMeshProUGUI[] selectedTexts = selectedButton.GetComponentsInChildren<TextMeshProUGUI>();

        // Get all TextMeshProUGUI components in the correct answer button
        TextMeshProUGUI[] correctTexts = correctAnswerButton.GetComponentsInChildren<TextMeshProUGUI>();

        // Check if the selected button is the correct answer button
        if (selectedButton == correctAnswerButton)
        {
            // Set all texts in the selected button to green
            foreach (var text in selectedTexts)
            {
                text.color = Color.green;
            }

            // Render GameObjectA
            if (GameObjectA != null)
            {
                GameObjectA.SetActive(true);
            }
            if (GameObjectB != null)
            {
                GameObjectB.SetActive(false);
            }

            // Play correct answer audio
            PlayAudio(correctAnswerAudio);
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

            // Render GameObjectB
            if (GameObjectA != null)
            {
                GameObjectA.SetActive(false);
            }
            if (GameObjectB != null)
            {
                GameObjectB.SetActive(true);
            }

            // Play incorrect answer audio
            PlayAudio(incorrectAnswerAudio);
        }

        DisableAllOptionButtons();
    }

    void PlayAudio(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    void DisableAllOptionButtons()
    {
        Button[] optionButtons = pages[currentPageIndex].GetComponentsInChildren<Button>();
        foreach (var button in optionButtons)
        {
            button.interactable = false;
        }
    }

    public void NextPage()
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

    void ResetAllAnswers()
    {
        foreach (var page in pages)
        {
            Button[] optionButtons = page.GetComponentsInChildren<Button>();
            foreach (var button in optionButtons)
            {
                button.interactable = true;
                TextMeshProUGUI[] buttonTexts = button.GetComponentsInChildren<TextMeshProUGUI>();
                foreach (var text in buttonTexts)
                {
                    text.color = Color.black; // Assuming the original color is black
                }
            }
        }

        // Hide both GameObjects on reset
        if (GameObjectA != null)
        {
            GameObjectA.SetActive(false);
        }
        if (GameObjectB != null)
        {
            GameObjectB.SetActive(false);
        }
    }
}