using UnityEngine;
using UnityEngine.UI;

public class ButtonSpriteChanger : MonoBehaviour
{
    public Image img;
    public Sprite[] nums;
    private RectTransform rectTransform;
    private LevelManager lvlMng;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        lvlMng = FindFirstObjectByType<LevelManager>();
        for (int i = 0; i < lvlMng.holdlvl; i++)
        {
            float targetY = -i * 275;
            if (rectTransform.anchoredPosition.y == targetY)
            {
                img.sprite = nums[i];
                break;
            }
        }
    }
}