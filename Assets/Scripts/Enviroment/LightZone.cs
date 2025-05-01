using UnityEngine;

public class LightZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EventBus.RaisePlayerEnteredLight();
        }
    }
}
