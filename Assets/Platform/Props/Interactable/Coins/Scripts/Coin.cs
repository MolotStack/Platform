using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private int _cost;
    [SerializeField]
    private string _nameFinishAnimation;

    private SpriteAnimation _currentSpriteAnimation;

    private bool _isTaken = false;

    private void Awake()
    {
        _currentSpriteAnimation = GetComponent<SpriteAnimation>();

        if ( _currentSpriteAnimation != null)
        {
            _currentSpriteAnimation.OnCompletion += DestroyCoin;
        }

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!_isTaken)
        {
            if (collider?.tag == "Player")
            {
                _isTaken = true;
                collider.GetComponent<Character>().SetCoint(_cost);

                if (_nameFinishAnimation == "")
                {
                    Destroy(gameObject);
                    return;
                }

                _currentSpriteAnimation.SetAnimation(_nameFinishAnimation);
            }
        }
    }

    private void DestroyCoin(string nameAnimation)
    {
        if(nameAnimation == _nameFinishAnimation)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        _currentSpriteAnimation.OnCompletion -= DestroyCoin;
    }
}
