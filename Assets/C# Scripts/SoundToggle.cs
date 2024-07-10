using UnityEngine;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour
{
    public Sprite soundOnIcon;   // Assign these sprites in the Inspector
    public Sprite soundOffIcon;

    public Image buttonImage;    // Reference to the Image component of the button

    void Start()
    {
        UpdateIcon(); // Update icon based on initial sound state
    }

    public void ToggleSound()
    {
        SoundManager.instance.ToggleSound(); // Toggle sound using SoundManager

        UpdateIcon(); // Update icon based on new sound state
    }

    public void UpdateIcon()
    {
        // Set the button's icon based on the current sound state from SoundManager
        bool soundOn = SoundManager.instance.GetSoundState();
        buttonImage.sprite = soundOn ? soundOnIcon : soundOffIcon;
    }
}
