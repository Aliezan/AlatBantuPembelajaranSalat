using UnityEngine;
using UnityEngine.UI;

public class AudioPlaybackControl : MonoBehaviour
{
    public GameObject[] contentGameObjects; // Array of content GameObjects
    public Button playButton;

    private AudioSource[] currentAudioSources;
    private int activeContentIndex = -1; // Track the currently active content index

    void Start()
    {
        // Add listener to the play button
        playButton.onClick.AddListener(PlayAudios);

        // Initialize with the first content (if any)
        if (contentGameObjects.Length > 0)
        {
            SetActiveContent(0); // Start with the first content
        }
        else
        {
            Debug.LogWarning($"No contentGameObjects set for {gameObject.name}");
        }
    }

    // Call this method when you change the active content
    public void SetActiveContent(int index)
    {
        if (index >= 0 && index < contentGameObjects.Length)
        {
            // Deactivate the previously active content
            if (activeContentIndex >= 0 && activeContentIndex < contentGameObjects.Length)
            {
                foreach (AudioSource audioSource in contentGameObjects[activeContentIndex].GetComponentsInChildren<AudioSource>())
                {
                    audioSource.Stop();
                }
            }

            // Activate the new content
            activeContentIndex = index;
            currentAudioSources = contentGameObjects[activeContentIndex].GetComponentsInChildren<AudioSource>();

            if (currentAudioSources.Length > 0)
            {
                // Ensure audios play on awake if the GameObject is active
                foreach (AudioSource audioSource in currentAudioSources)
                {
                    if (audioSource.playOnAwake && audioSource.gameObject.activeInHierarchy)
                    {
                        audioSource.Play();
                        StartCoroutine(CheckAudioCompletion(audioSource));
                    }
                }
            }
            UpdateButtonStates();
        }
    }

    void PlayAudios()
    {
        if (currentAudioSources != null)
        {
            foreach (AudioSource audioSource in currentAudioSources)
            {
                if (audioSource.gameObject.activeInHierarchy) // Check if the GameObject is active
                {
                    audioSource.Play();
                    StartCoroutine(CheckAudioCompletion(audioSource));
                }
            }
            UpdateButtonStates();
        }
    }

    void UpdateButtonStates()
    {
        bool isPlaying = false;

        if (currentAudioSources != null)
        {
            foreach (AudioSource audioSource in currentAudioSources)
            {
                if (audioSource.gameObject.activeInHierarchy && audioSource.isPlaying) // Check if the GameObject is active
                {
                    isPlaying = true;
                }
            }
        }

        playButton.interactable = !isPlaying;
    }

    private System.Collections.IEnumerator CheckAudioCompletion(AudioSource audioSource)
    {
        yield return new WaitWhile(() => audioSource.isPlaying);
        UpdateButtonStates();
    }
}
