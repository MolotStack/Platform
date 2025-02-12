using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField]
    private string _nameFinishAnimation;
    [SerializeField]
    private float _changeHealth;
    private SpriteAnimation _currentSpriteAnimation;

    private void Awake()
    {
        _currentSpriteAnimation = GetComponent<SpriteAnimation>();

        if (_currentSpriteAnimation != null)
        {
            _currentSpriteAnimation.OnCompletion += DestroyPoint;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider )
    {
        if (collider.tag.Contains("Player"))
        {            
            if (collider.gameObject.TryGetComponent<HealthComponent>(out var healthComponent))
            {
                healthComponent.ChangeHealth(_changeHealth);
                _currentSpriteAnimation.SetAnimation(_nameFinishAnimation);
            }

        }
    }

    private void DestroyPoint(string nameAnimation)
    {
        if (nameAnimation == _nameFinishAnimation)
        {
            Destroy(gameObject);
        }
    }
}
