using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CheatController : MonoBehaviour
{
    [SerializeField]
    private CheatName[] _allCheats;
    [SerializeField]
    private float _inputTimeToLive;

    private string _currentInputString;
    private float _inputTime;

    private void Awake()
    {
        Keyboard.current.onTextInput += OnTextInput;
    }

    private void OnDestroy()
    {
        Keyboard.current.onTextInput -= OnTextInput;
    }

    private void OnTextInput(char inputChar)
    {
        _currentInputString += inputChar;
        _inputTime = _inputTimeToLive;
        FindAnyCheats();
    }

    private void FindAnyCheats()
    {
        foreach (var cheat in _allCheats)
        {
            if (_currentInputString.Contains(cheat.name))
            {
                cheat.unityEvent?.Invoke();
                _currentInputString =string.Empty;
            }
        }
    }

    private void Update()
    {
        if (_inputTime < 0)
        {
            _currentInputString = string.Empty;
        }
        else
        {
            _inputTime -= Time.deltaTime;
        }
    }
}

[Serializable]
public class CheatName
{
    public string name;
    public UnityEvent unityEvent;
}
