using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class FreeCameraBehaviour : MonoBehaviour
{
    [SerializeField] private InputActionReference buttonToEnableRotationAction;
    [SerializeField] private InputActionReference rotateAction;
    [SerializeField] private InputActionReference moveAction;

    [SerializeField] private float rotationSpeed = 75;
    [SerializeField] private Vector2 verticalAngleBoundaries = new(-75, 75);

    private float m_Yaw;
    private float m_Pitch;

    private void Start()
    {
        m_Yaw = transform.eulerAngles.y;
        m_Pitch = transform.eulerAngles.x;
    }

    private void LateUpdate()
    {
        var move  = moveAction.action.ReadValue<Vector2>() * (rotationSpeed * Time.deltaTime);
        transform.position += transform.right * move.x + transform.forward * move.y;
        
        if (buttonToEnableRotationAction != null && buttonToEnableRotationAction.action.phase != InputActionPhase.Performed) return;

        var rotate = rotateAction.action.ReadValue<Vector2>() * (rotationSpeed * Time.deltaTime);

        m_Yaw += rotate.x;
        m_Pitch -= rotate.y; // Inversé pour correspondre aux mouvements naturels
        m_Pitch = Mathf.Clamp(m_Pitch, verticalAngleBoundaries.x, verticalAngleBoundaries.y);

        transform.rotation = Quaternion.Euler(m_Pitch, m_Yaw, 0f);
    }
}