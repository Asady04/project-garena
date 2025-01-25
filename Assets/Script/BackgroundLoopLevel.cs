using UnityEngine;

public class BackgroundLoopLevel : MonoBehaviour
{
    public LevelManager lvlMng;
    public RectTransform[] image;
    public RectTransform[] button;
    public GameObject buttonPrefab;
    public Transform buttonParent;
    public float scrollSpeed = 10000f;

    // private float imageHeight;

    void Start()
    {
        SpawnButtons();
        // imageHeight = image[0].rect.height;
    }

    void Update()
    {
        if (lvlMng.holdlvl > 1)
        {
            HandleDrag();
            // CheckAndLoop();
        }
    }

    void HandleDrag()
    {
        if (Input.GetMouseButton(0))
        {
            float dragAmount = Input.GetAxis("Mouse Y") * scrollSpeed * Time.deltaTime;
            if (button[lvlMng.holdlvl - 1].position.y >= -5 && dragAmount > 0 || button[0].position.y <= 5 && dragAmount < 0)
            {
                return;
            }
            else
            {
                for (int i = 0; i < image.Length; i++)
                {
                    image[i].anchoredPosition += new Vector2(0, dragAmount);
                }
                for (int i = 0; i < lvlMng.holdlvl; i++)
                {
                    button[i].anchoredPosition += new Vector2(0, dragAmount);
                }
            }
        }
    }

    void SpawnButtons()
    {
        // Adjust button array size
        button = new RectTransform[lvlMng.holdlvl]; // max 15 buttons

        for (int i = 0; i < button.Length; i++)
        {
            GameObject newButton = Instantiate(buttonPrefab, buttonParent);
            RectTransform rectTransform = newButton.GetComponent<RectTransform>();

            rectTransform.anchoredPosition = new Vector3(0, -i * 275, 0); // Spaced 250 units apart vertically
            button[i] = rectTransform;
        }
    }

    // void CheckAndLoop()
    // {
    //     foreach (var item in image)
    //     {
    //         if (item.anchoredPosition.y >= imageHeight * 1.5f)
    //         {
    //             item.anchoredPosition -= new Vector2(0, imageHeight * 3);
    //         }
    //     }

    //     foreach (var item in image)
    //     {
    //         if (item.anchoredPosition.y <= -imageHeight * 1.5f)
    //         {
    //             item.anchoredPosition += new Vector2(0, imageHeight * 3);
    //         }
    //     }
    // }
}