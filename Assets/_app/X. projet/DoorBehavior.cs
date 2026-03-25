using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    [SerializeField] Animator doorAnimator;
    
    public void OpenDoor()
    {
        Debug.Log("Door opened!");
        doorAnimator.SetBool("isOpen", true);
    }
    
    public void CloseDoor()
    {
        Debug.Log("Door closed!");
        doorAnimator.SetBool("isOpen", false);
    } 
}
