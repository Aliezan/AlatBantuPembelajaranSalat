using UnityEngine;
using UnityEngine.UI;

public class ContentToggle : MonoBehaviour
{
    public GameObject[] contentGameObjects; // Array of content GameObjects
    public Button[] contentButtons; // Array of buttons to toggle contents
    public AudioPlaybackControl audioPlaybackControl;

    void Start()
    {
        // Add listeners to the content buttons
        for (int i = 0; i < contentButtons.Length; i++)
        {
            int index = i; // Capture the index
            contentButtons[i].onClick.AddListener(() => ShowContent(index));
        }

        // Default to showing the first content (if any)
        if (contentGameObjects.Length > 0)
        {
            ShowContent(0);
        }
    }

    void ShowContent(int index)
    {
        for (int i = 0; i < contentGameObjects.Length; i++)
        {
            contentGameObjects[i].SetActive(i == index);
        }

        if (audioPlaybackControl != null && index < audioPlaybackControl.audioSources.Length)
        {
            audioPlaybackControl.SetActiveContent(index);
        }
    }
}
