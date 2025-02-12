using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private bool _isOpened;
    [SerializeField]
    private Animator _currentAnimator;

    private static int _animOpened = Animator.StringToHash("IsOpened");

    private void Start()
    {
        _currentAnimator.SetBool(_animOpened, _isOpened);
    }

    public void ActionDoor()
    {
        _isOpened = !_isOpened;
        _currentAnimator.SetBool(_animOpened, _isOpened);
    }
}
