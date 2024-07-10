using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public Button ShubuhButton;
    public Button DzuhurButton;
    public Button AsharButton;
    public Button MaghribButton;
    public Button IsyaButton;
    public Button ExitButton;
    public Button SoundToggleBtn;

    private SoundToggle soundToggle;

    void Start()
    {
        // Add listeners for prayer buttons
        ShubuhButton.onClick.AddListener(() => GoToMateriShalat(0));
        DzuhurButton.onClick.AddListener(() => GoToMateriShalat(1));
        AsharButton.onClick.AddListener(() => GoToMateriShalat(2));
        MaghribButton.onClick.AddListener(() => GoToMateriShalat(3));
        IsyaButton.onClick.AddListener(() => GoToMateriShalat(4));

        // Add listener for exit button
        ExitButton.onClick.AddListener(() => Application.Quit());

        // Get reference to SoundToggle script
        soundToggle = SoundToggleBtn.GetComponent<SoundToggle>();

        // Add listener for sound toggle button
        SoundToggleBtn.onClick.AddListener(ToggleSound);

        // Update sound toggle button icon based on current sound state
        UpdateSoundToggleIcon();
    }

    void GoToMateriShalat(int pageIndex)
    {
        PageManager.PageIndex = pageIndex;
        SceneManager.LoadScene("MateriShalat");
    }

    void ToggleSound()
    {
        soundToggle.ToggleSound(); // Toggle sound using SoundToggle script
        UpdateSoundToggleIcon(); // Update sound toggle button icon
    }

    void UpdateSoundToggleIcon()
    {
        // Update sound toggle button icon based on current sound state
        soundToggle.UpdateIcon();
    }
}
