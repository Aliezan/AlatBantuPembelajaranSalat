using UnityEngine;
using UnityEngine.UI;

public class VideoNavigation : MonoBehaviour
{
    public GameObject[] contentGameObjects; // Array of video GameObjects
    public Button nextNav; // Button to navigate to the next video
    public Button prevNav; // Button to navigate to the previous video
    public GameObject textOne;
    public GameObject textTwo;

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
