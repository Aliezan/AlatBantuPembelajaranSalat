using UnityEngine;
using UnityEngine.UI;

public class ContentToggle : MonoBehaviour
{
    public GameObject content1; // Reference to Content1 GameObject
    public GameObject content2; // Reference to Content2 GameObject
    public Button buttonContent1; // Reference to ButtonContent1
    public Button buttonContent2; // Reference to ButtonContent2

    void Start()
    {
        buttonContent1.onClick.AddListener(ShowContent1);
        buttonContent2.onClick.AddListener(ShowContent2);

        ShowContent1();
    }

    void ShowContent1()
    {
        content1.SetActive(true);
        content2.SetActive(false);
    }

    void ShowContent2()
    {
        content1.SetActive(false);
        content2.SetActive(true);
    }
}
