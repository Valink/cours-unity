using System;
using UnityEngine;
using UnityEngine.Events;

public class EnnemyBehavior : MonoBehaviour
{
    [SerializeField] private bool hasBeenHit = false;
    [SerializeField] private float health = 1;
    [SerializeField] public UnityEvent<float> onHealthChangeEvent;
    [SerializeField] public UnityEvent<float> onDieEvent;

    private void Update()
    {
        if (hasBeenHit)
        {
            health -= 0.1f;
            onHealthChangeEvent.Invoke(health);
            hasBeenHit = false;
            
            if (health <= 0)
            {
                onDieEvent.Invoke(health);
            }
        }
    }
}