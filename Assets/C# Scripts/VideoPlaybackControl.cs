using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoPlaybackControl : MonoBehaviour
{
    public GameObject[] contentGameObjects; // Array of content GameObjects
    public Button playButton;
    public Button pauseButton;
    public Button restartButton;

    private VideoPlayer[] currentVideoPlayers;
    private int activeContentIndex = -1; // Track the currently active content index

    void Start()
    {
        // Add listeners to the buttons
        playButton.onClick.AddListener(PlayVideos);
        pauseButton.onClick.AddListener(PauseVideos);
        restartButton.onClick.AddListener(RestartVideos);

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
                foreach (VideoPlayer videoPlayer in contentGameObjects[activeContentIndex].GetComponentsInChildren<VideoPlayer>())
                {
                    videoPlayer.Stop();
                }
            }

            // Activate the new content
            activeContentIndex = index;
            currentVideoPlayers = contentGameObjects[activeContentIndex].GetComponentsInChildren<VideoPlayer>();

            if (currentVideoPlayers.Length > 0)
            {
                // Ensure videos play on awake if the GameObject is active
                foreach (VideoPlayer videoPlayer in currentVideoPlayers)
                {
                    videoPlayer.loopPointReached += OnVideoEnd; // Handle video end event
                    if (videoPlayer.playOnAwake && videoPlayer.gameObject.activeInHierarchy)
                    {
                        videoPlayer.Play();
                    }
                }
            }
            UpdateButtonStates();
        }
    }

    void PlayVideos()
    {
        if (currentVideoPlayers != null)
        {
            foreach (VideoPlayer videoPlayer in currentVideoPlayers)
            {
                if (videoPlayer.gameObject.activeInHierarchy) // Check if the GameObject is active
                {
                    videoPlayer.Play();
                }
            }
            UpdateButtonStates();
        }
    }

    void PauseVideos()
    {
        if (currentVideoPlayers != null)
        {
            foreach (VideoPlayer videoPlayer in currentVideoPlayers)
            {
                if (videoPlayer.gameObject.activeInHierarchy) // Check if the GameObject is active
                {
                    videoPlayer.Pause();
                }
            }
            UpdateButtonStates();
        }
    }

    void RestartVideos()
    {
        if (currentVideoPlayers != null)
        {
            foreach (VideoPlayer videoPlayer in currentVideoPlayers)
            {
                if (videoPlayer.gameObject.activeInHierarchy) // Check if the GameObject is active
                {
                    videoPlayer.Stop();
                    videoPlayer.Play();
                }
            }
            UpdateButtonStates();
        }
    }

    void UpdateButtonStates()
    {
        bool isPlaying = false;
        bool isPaused = false;

        if (currentVideoPlayers != null)
        {
            foreach (VideoPlayer videoPlayer in currentVideoPlayers)
            {
                if (videoPlayer.gameObject.activeInHierarchy) // Check if the GameObject is active
                {
                    if (videoPlayer.isPlaying)
                    {
                        isPlaying = true;
                    }
                    else if (videoPlayer.time > 0 && videoPlayer.time < videoPlayer.length)
                    {
                        isPaused = true;
                    }
                }
            }
        }

        playButton.interactable = !isPlaying;
        pauseButton.interactable = isPlaying || isPaused;
        restartButton.interactable = isPlaying || isPaused;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Called when the video ends
        UpdateButtonStates();
    }
}
