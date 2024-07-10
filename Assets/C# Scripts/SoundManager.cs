using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance; // Singleton instance

    private bool soundOn = true; // Initial sound state, assuming sound is initially on

    void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    public void ToggleSound()
    {
        soundOn = !soundOn; // Toggle sound state

        // Update global volume based on sound state
        float volume = soundOn ? 1.0f : 0.0f;
        AudioListener.volume = volume;

        // You can add more logic here if needed (e.g., save sound state to PlayerPrefs)
    }

    public bool GetSoundState()
    {
        return soundOn;
    }
}
