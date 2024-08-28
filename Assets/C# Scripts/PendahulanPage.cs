using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PendahuluanPage : MonoBehaviour
{
    public Button NextNavBtn;
    public Button BackToMenuBtn;

    void Start()
    {
        NextNavBtn.onClick.AddListener(() => GoToMateriShalat("Shubuh"));
        BackToMenuBtn.onClick.AddListener(() => GoToMateriShalat("MainMenu"));
    }

    void GoToMateriShalat(string scene)
    {
        SceneManager.LoadScene(scene);
    }

}
