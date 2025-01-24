using UnityEngine;

public class BackgroundLoopLevel : MonoBehaviour
{
    public LevelManager lvlMng;
    public RectTransform[] image;
    public RectTransform[] button;
    public float scrollSpeed = 10000f;

    // private float imageHeight;

    void Start()
    {
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
            if (button[lvlMng.holdlvl - 1].position.y >= -10 && dragAmount > 0 || button[0].position.y <= 5 && dragAmount < 0)
            {
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    image[i].anchoredPosition += new Vector2(0, dragAmount);
                }
                for (int i = 0; i < 15; i++)
                {
                    button[i].anchoredPosition += new Vector2(0, dragAmount);
                }
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
}