using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public UnityEvent trigg;
    void OnTriggerEnter2D(Collider2D col2d)
    {
        if (col2d.tag == "Player")
        {
            trigg?.Invoke();
            Destroy(gameObject);
        }
    }
}