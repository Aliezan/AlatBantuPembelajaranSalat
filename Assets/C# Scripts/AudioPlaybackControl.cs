using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AudioPlaybackControl : MonoBehaviour
{
    public AudioSource[] audioSources; // Array of audio sources corresponding to contentGameObjects
    public Button[] playButtons; // Array of play buttons corresponding to audioSources

    private AudioSource currentAudioSource;
    private Button currentPlayButton;

    void Start()
    {
        // Add listeners to the play buttons
        if (playButtons.Length == audioSources.Length)
        {
            for (int i = 0; i < playButtons.Length; i++)
            {
                int index = i; // Capture the index
                playButtons[i].onClick.AddListener(() => PlayAudio(index));
            }

            // Initialize with the first audio source (if any)
            if (audioSources.Length > 0)
            {
                SetActiveContent(0); // Start with the first audio source
            }
            else
            {
                Debug.LogWarning($"No audioSources set for {gameObject.name}");
            }
        }
        else
        {
            Debug.LogError("The number of play buttons must match the number of audio sources.");
        }
    }

    public void SetActiveContent(int index)
    {
        if (index >= 0 && index < audioSources.Length)
        {
            // Deactivate the previously active audio source
            if (currentAudioSource != null)
            {
                currentAudioSource.Stop();
            }

            currentAudioSource = audioSources[index];
            currentPlayButton = playButtons[index];

            UpdateButtonStates();
        }
    }

    void PlayAudio(int index)
    {
        if (index >= 0 && index < audioSources.Length)
        {
            SetActiveContent(index);

            if (currentAudioSource != null)
            {
                currentAudioSource.Play();
                StartCoroutine(CheckAudioCompletion(currentAudioSource));
                UpdateButtonStates();
            }
        }
    }

    void UpdateButtonStates()
    {
        if (currentPlayButton != null)
        {
            currentPlayButton.interactable = currentAudioSource != null && !currentAudioSource.isPlaying;
        }
    }

    private IEnumerator CheckAudioCompletion(AudioSource audioSource)
    {
        yield return new WaitWhile(() => audioSource.isPlaying);
        UpdateButtonStates();
    }
}
