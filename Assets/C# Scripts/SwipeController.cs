using UnityEngine;

public class SwipeController : MonoBehaviour
{
    [SerializeField] int maxPage;
    int currentPage;
    Vector3 targetPos;
    [SerializeField] Vector3 pageStep;
    [SerializeField] RectTransform contentRect;

    [SerializeField] float tweenTime;
    [SerializeField] LeanTweenType tweenType;


    private void Awake()
    {
        currentPage = 1;
        targetPos = contentRect.localPosition;
    }


    public void Next()
    {
        if (currentPage < maxPage)
        {
            currentPage++;
            targetPos += pageStep;
            MovePage();
        }

    }

    public void Previous()
    {
        if (currentPage > 1)
        {
            currentPage--;
            targetPos -= pageStep;
            MovePage();
        }
    }

    void MovePage()
    {
        contentRect.LeanMoveLocal(targetPos, tweenTime).setEase(tweenType);
    }

}
