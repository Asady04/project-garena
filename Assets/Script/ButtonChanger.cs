using UnityEngine;
using UnityEngine.UI;

public class ButtonChanger : MonoBehaviour
{
    Button button;           // Reference to the Button
    public Sprite newSprite;        // The new Sprite you want to assign
    Sprite currentSprite;        // The new Sprite you want to assign
    Image buttonImage;

    void Start()
    {
        button = GetComponent<Button>();
        buttonImage = button.GetComponent<Image>();
        currentSprite = buttonImage.sprite;
        if (PlayerPrefs.GetInt("MusicMuted", 0) == 1)
        {
            buttonImage.sprite = newSprite;
            buttonImage.SetNativeSize();
        }
        else
        {
            buttonImage.sprite = currentSprite;
        }
    }
    // Update is called once per frame
    public void ChangeSprite()
    {
        if (PlayerPrefs.GetInt("MusicMuted", 0) == 1)
        {
            buttonImage.sprite = newSprite;
            buttonImage.SetNativeSize();
        }
        else
        {
            buttonImage.sprite = currentSprite;
        }
    }
}
