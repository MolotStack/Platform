using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPartical : MonoBehaviour
{
    [SerializeField]
    private SpriteAnimation _currentAnimation;

    private void OnEnable()
    {
        _currentAnimation.OnCompletion += CompletionAnimation;
    }

    private void OnDestroy()
    {
        _currentAnimation.OnCompletion -= CompletionAnimation;
    }

    private void CompletionAnimation(string nameAnimation)
    {
        Destroy(gameObject);
    }
}
