using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimation : MonoBehaviour
{

    [SerializeField]
    private int _frameRate;
    [SerializeField]
    private bool _isLooping;
    [SerializeField]
    private Sprite[] _sprites;
    [SerializeField]
    private UnityEvent _onComplet;

    private SpriteRenderer _currentSpriteRenderer;

    private float _secondsPerFrame;
    private float _nextFrameTime;

    private int _currentFrameSprite = 0;

    private bool _isPlaying = true;

    private void Awake()
    {
        _currentSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _secondsPerFrame = 1f / _frameRate;
        _nextFrameTime = Time.time + _secondsPerFrame;

        _currentSpriteRenderer.sprite = _sprites[_currentFrameSprite];
    }

    private void Update()
    {
        if (_isPlaying)
        {
            if (_nextFrameTime > Time.time)
            {
                return;
            }

            if (_sprites.Length - 1 < _currentFrameSprite)
            {
                if (_isLooping)
                {
                    _currentFrameSprite = 0;
                }
                else
                {
                    _isPlaying = false;
                    _onComplet?.Invoke();
                    return;
                }
            }

            _currentSpriteRenderer.sprite = _sprites[_currentFrameSprite];
            _nextFrameTime += _secondsPerFrame;

            _currentFrameSprite++;
        }
    }

}
