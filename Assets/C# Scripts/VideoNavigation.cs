using UnityEngine;
using UnityEngine.UI;

public class VideoNavigation : MonoBehaviour
{
    public GameObject[] contentGameObjects;
    public Button nextNav; 
    public Button prevNav; 
    public GameObject textOne;
    public GameObject textTwo;
    public GameObject textThree;
    public GameObject textFour;

    private int currentVideoIndex = 0;

    void Start()
    {
        nextNav.onClick.AddListener(NextVideo);
        prevNav.onClick.AddListener(PreviousVideo);

        // Initialize with the first content (if any)
        if (contentGameObjects.Length > 0)
        {
            SetActiveContent(currentVideoIndex);
        }
    }

    public void SetActiveContent(int index)
    {
        if (index >= 0 && index < contentGameObjects.Length)
        {
            currentVideoIndex = index;

            // Deactivate all content
            foreach (GameObject content in contentGameObjects)
            {
                content.SetActive(false);
            }

            // Activate current content
            contentGameObjects[currentVideoIndex].SetActive(true);

            // Activate textOne if index is 0, otherwise deactivate it
            textOne.SetActive(currentVideoIndex == 0);

            // Activate textTwo if index is 1, otherwise deactivate it
            textTwo.SetActive(currentVideoIndex == 1);

            // Activate textThree if index is 2, otherwise deactivate it
            if (textThree != null)
            {
                textThree.SetActive(currentVideoIndex == 2);
            }

            if (textFour != null)
            {
                textFour.SetActive(currentVideoIndex == 3);
            }

            // Update button interactability based on current index
            UpdateButtonInteractability();
        }
    }

    void UpdateButtonInteractability()
    {
        // Set nextNav interactable if there's a next video, else disable it
        nextNav.interactable = currentVideoIndex < contentGameObjects.Length - 1;

        // Set prevNav interactable if there's a previous video, else disable it
        prevNav.interactable = currentVideoIndex > 0;
    }

    public void NextVideo()
    {
        if (currentVideoIndex < contentGameObjects.Length - 1)
        {
            currentVideoIndex++;
            SetActiveContent(currentVideoIndex);
        }
    }

    public void PreviousVideo()
    {
        if (currentVideoIndex > 0)
        {
            currentVideoIndex--;
            SetActiveContent(currentVideoIndex);
        }
    }
}
