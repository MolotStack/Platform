using UnityEngine;
using UnityEngine.Events;
using System;

public class TeleportController : MonoBehaviour, IInteracting
{
    [SerializeField]
    private GameObject _visualHint;

    [SerializeField]
    private Transform _teleportPoint;

    [SerializeField]
    private TeleportEvent _teleportEvent;

    public void Awake()
    {
        OnDisableVisualHint();
    }

    public void Interaction()
    {
        _teleportEvent?.Invoke(_teleportPoint.position);
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

[Serializable]
public class TeleportEvent : UnityEvent<Vector3>
{

}
