using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimation : MonoBehaviour
{
    public UnityAction<string> OnCompletion;

    [SerializeField]
    private List<SpriteAnimationScriptableObject> _allAnimations;
    [SerializeField]
    private int _indexStartAnimation;

    private SpriteRenderer _currentSpriteRenderer;

    private SpriteAnimationScriptableObject _currentAmimation;

    private float _secondsPerFrame;
    private float _nextFrameTime;

    private int _currentFrameSprite = 0;
    private int _currentIndexAnimation;

    private bool _isPlaying = true;



    private void Awake()
    {
        _currentSpriteRenderer = GetComponent<SpriteRenderer>();


        if (_allAnimations.Count > 0)
        {
            _currentAmimation = _allAnimations[_indexStartAnimation];
            _currentIndexAnimation = _indexStartAnimation;
        }
        else
        {
            _isPlaying = false;
        }

    }

    private void Start()
    {
        if (_isPlaying)
        {
            _secondsPerFrame = 1f / _currentAmimation.FrameRate;
            _nextFrameTime = Time.time + _secondsPerFrame;

            _currentSpriteRenderer.sprite = _currentAmimation.Sprites[_currentFrameSprite];
        }

    }

    private void Update()
    {
        if (_isPlaying)
        {
            if (_nextFrameTime > Time.time)
            {
                return;
            }

            if (_currentAmimation.Sprites.Count - 1 < _currentFrameSprite)
            {
                if (_currentAmimation.IsLooping)
                {
                    _currentFrameSprite = 0;
                }
                else if (_currentAmimation.AllowNext && _allAnimations.Count -1 > _currentIndexAnimation)
                {

                    _currentFrameSprite = 0;
                    OnCompletion?.Invoke(_currentAmimation.name);
                    _currentIndexAnimation++;

                    _currentAmimation = _allAnimations[_currentIndexAnimation];
                }
                else
                {
                    _isPlaying = false;
                    OnCompletion?.Invoke(_currentAmimation.name);
                    return;
                }
            }

            _currentSpriteRenderer.sprite = _currentAmimation.Sprites[_currentFrameSprite];
            _nextFrameTime += _secondsPerFrame;

            _currentFrameSprite++;
        }
    }

    public void SetAnimation(string nameAnimation)
    {
        foreach (var anim in _allAnimations)
        {
            if (anim.name == nameAnimation)
            {
                _currentAmimation = anim;
                _currentFrameSprite = 0;
                return;
            }
        }
    }


}
