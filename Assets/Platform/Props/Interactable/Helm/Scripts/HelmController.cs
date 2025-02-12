using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HelmController : MonoBehaviour, IInteracting
{
    [SerializeField]
    private GameObject _visualHint;
    [SerializeField]
    private Animator _currentAnimator;

    [SerializeField]
    private UnityEvent objectAction;

    private static int _isUsedAnim = Animator.StringToHash("IsUsed");

    private void Awake()
    {
        OnDisableVisualHint();
    }

    public void Interaction()
    {
        _currentAnimator.SetTrigger(_isUsedAnim);
        objectAction?.Invoke();
    }

    public void OnDisableVisualHint()
    {
        _visualHint.SetActive(false);
    }

    public void OnEnableVisualHint()
    {
        _visualHint.SetActive(true);
    }

}
