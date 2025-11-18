using UnityEngine;
using UnityEngine.Events;

public class TriggerWithCustomEventBehavior : MonoBehaviour
{
    public UnityEvent onTriggerEnterEvent;
    public UnityEvent onTriggerExitEvent;
    
    private void OnTriggerEnter(Collider otherCollider)
    {
        Debug.Log("[TriggerWithCustomEventBehavior] Entered by " + otherCollider.name);
        onTriggerEnterEvent.Invoke();
    }

    private void OnTriggerExit(Collider otherCollider)
    {
        Debug.Log("[TriggerWithCustomEventBehavior] Exited by " + otherCollider.name);
        onTriggerExitEvent.Invoke();
    }
}