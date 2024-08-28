using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoPlaybackControl : MonoBehaviour
{
    public GameObject[] contentGameObjects; // Array of content GameObjects
    public Button playButton;
    public Button pauseButton;
    public Button restartButton;
    public Button fullscreenButton;

    private VideoPlayer[] currentVideoPlayers;
    private int activeContentIndex = -1; // Track the currently active content index
    private bool isFullscreen = false;



    void Start()
    {
        // Add listeners to the buttons
        playButton.onClick.AddListener(PlayVideos);
        pauseButton.onClick.AddListener(PauseVideos);
        restartButton.onClick.AddListener(RestartVideos);

        if (fullscreenButton != null)
        {
            fullscreenButton.onClick.AddListener(ToggleFullscreen);
        }


        // Initialize with the first content (if any)
        if (contentGameObjects.Length > 0)
        {
            SetActiveContent(0); // Start with the first content
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
    void ToggleFullscreen()
    {
        isFullscreen = !isFullscreen;
        Screen.fullScreen = isFullscreen;
        UpdateButtonStates();
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

        if (fullscreenButton != null)
        {
            Text fullscreenButtonText = fullscreenButton.GetComponentInChildren<Text>();
            if (fullscreenButtonText != null)
            {
                fullscreenButtonText.text = isFullscreen ? "Exit Fullscreen" : "Fullscreen";
            }
        }
    }



    void OnVideoEnd(VideoPlayer vp)
    {
        UpdateButtonStates();
    }
}
