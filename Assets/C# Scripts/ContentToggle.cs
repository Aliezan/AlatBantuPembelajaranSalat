using UnityEngine;
using UnityEngine.UI;

public class ContentToggle : MonoBehaviour
{
    public GameObject content1;
    public GameObject content2;
    public GameObject content3;
    public Button buttonContent1;
    public Button buttonContent2;
    public Button buttonContent3;

    void Start()
    {
        buttonContent1.onClick.AddListener(ShowContent1);
        buttonContent2.onClick.AddListener(ShowContent2);

        if (buttonContent3 != null)
        {
            buttonContent3.onClick.AddListener(ShowContent3);
        }

        ShowContent1(); // Default to showing Content1
    }

    void ShowContent1()
    {
        content1.SetActive(true);
        content2.SetActive(false);
        if (content3 != null)
        {
            content3.SetActive(false);
        }
    }

    void ShowContent2()
    {
        content1.SetActive(false);
        content2.SetActive(true);
        if (content3 != null)
        {
            content3.SetActive(false);
        }
    }

    void ShowContent3()
    {
        content1.SetActive(false);
        content2.SetActive(false);
        if (content3 != null)
        {
            content3.SetActive(true);
        }
    }
}
