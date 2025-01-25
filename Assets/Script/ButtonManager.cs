using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public RectTransform play, exit, title;
    public Button[] buttons;
    public Button back;
    public GameObject firstClick, backButton;
    private bool left = true;

    void Start()
    {
        foreach (Button button in buttons) { button.enabled = false; }
        back.enabled = false;
    }

    public void MovenDisappear()
    {
        StartCoroutine(MoveObject(play, 160f, 0.5f));
        StartCoroutine(MoveObject(exit, 160f, 0.5f));
        StartCoroutine(MoveObject(title, 150f, 0.5f));
        Destroy(firstClick);
        StartCoroutine(DelayBeforeEnabled(0.5f));
    }

    public void MoveSide()
    {
        foreach (Button button in buttons) { button.enabled = false; }
        StartCoroutine(MoveSide(play, -500f, -180f, 0.6f));
        StartCoroutine(MoveSide(exit, 500f, -180f, 0.6f));
        StartCoroutine(MoveObject(title, 400f, 0.6f));
        BackDelay();
    }

    public void MoveBack()
    {
        StartCoroutine(MoveSide(play, 500f, 180f, 0.6f));
        StartCoroutine(MoveSide(exit, -500f, 180f, 0.6f));
        StartCoroutine(MoveObject(title, -400f, 0.6f));
        StartCoroutine(DelayBeforeEnabled(0.6f));
        BackDelay();
    }

    private void BackDelay()
    {
        if (back.enabled)
        {
            backButton.SetActive(false);
            back.enabled = false;
        }
        else
        {
            StartCoroutine(DelayBackON(0.6f));
        }
    }

    private IEnumerator DelayBackON(float time)
    {
        yield return new WaitForSeconds(time);
        backButton.SetActive(true);
        back.enabled = true;
    }

    private IEnumerator DelayBeforeEnabled(float time)
    {
        yield return new WaitForSeconds(time);
        foreach (Button button in buttons) { button.enabled = true; }
    }

    private IEnumerator MoveObject(RectTransform item, float distance, float time)
    {
        Vector3 startPosition = item.localPosition;
        Vector3 targetPosition = startPosition + new Vector3(0, distance, 0);

        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            float speed = elapsedTime / time;
            item.localPosition = Vector3.Lerp(startPosition, targetPosition, speed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        item.localPosition = targetPosition;
    }

    private IEnumerator MoveSide(RectTransform item, float x, float y, float time)
    {
        Vector3 startPosition = item.localPosition;
        Vector3 targetPosition;
        if (left)
        {
            left = false;
            targetPosition = startPosition + new Vector3(x, y, 0);
        }
        else
        {
            left = true;
            targetPosition = startPosition + new Vector3(x, y, 0);
        }
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            float speed = elapsedTime / time;
            item.localPosition = Vector3.Lerp(startPosition, targetPosition, speed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        item.localPosition = targetPosition;
    }
}
