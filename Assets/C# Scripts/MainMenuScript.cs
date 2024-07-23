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
    public Button PlayAllBtn;
    public Button QuizBtn;
    public Button BacaanSetelahSalatBtn;

    private SoundToggle soundToggle;

    void Start()
    {
        ShubuhButton.onClick.AddListener(() => GoToMateriShalat("Shubuh"));
        DzuhurButton.onClick.AddListener(() => GoToMateriShalat("Dzuhur"));
        AsharButton.onClick.AddListener(() => GoToMateriShalat("Ashar"));
        MaghribButton.onClick.AddListener(() => GoToMateriShalat("Maghrib"));
        IsyaButton.onClick.AddListener(() => GoToMateriShalat("Isya"));

        PlayAllBtn.onClick.AddListener(() => GoToMateriShalat("Pendahuluan"));

        QuizBtn.onClick.AddListener(() => GoToMateriShalat("Quiz"));

        BacaanSetelahSalatBtn.onClick.AddListener(() => GoToMateriShalat("BacaanSetelahSalat"));

        ExitButton.onClick.AddListener(() => Application.Quit());
    }

    void GoToMateriShalat(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
