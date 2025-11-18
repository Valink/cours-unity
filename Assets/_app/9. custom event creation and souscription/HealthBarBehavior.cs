using UnityEngine;

public class HealthBarBehavior : MonoBehaviour
{
    [SerializeField] private RectTransform greenBar;
    [SerializeField] private EnnemyBehavior ennemy;

    private void OnEnable()
    {
        ennemy.onHealthChangeEvent.AddListener(UpdateHealth);
    }

    private void OnDisable()
    {
        ennemy.onHealthChangeEvent.RemoveListener(UpdateHealth);
    }
    
    private void UpdateHealth(float health)
    {
        greenBar.localScale = new Vector3(health, 1, 1);
    }
}
