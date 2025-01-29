using UnityEngine;

public class Character : MonoBehaviour
{
    public bool IsJump
    {
        get { return _isJump; }
        set { _isJump = value; }
    }
    public float CurrentInputDirection
    {
        get { return _currentInputDirection.x; }
        set { _currentInputDirection.x = value; }
    }

    [SerializeField, Range(0, 10)]
    private float _speedMovement;
    [SerializeField, Range(0, 25)]
    private float _forceJump;
    [SerializeField]
    private PhysicsMaterial2D _heroNoFriction;
    [SerializeField]
    private PhysicsMaterial2D _heroFriction;


    private Vector2 _currentInputDirection;

    private Rigidbody2D _rigidbody;
    private GroundCheck _groundCheck;

    private bool _isJump;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _groundCheck = GetComponentInChildren<GroundCheck>();
    }

    private void Update()
    {
        Jump();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        if (_groundCheck.IsGrounded)
        {
            _rigidbody.sharedMaterial = _heroFriction;
        }
        else
        {
            _rigidbody.sharedMaterial = _heroNoFriction;
        }

        _rigidbody.velocity = new Vector2(CalculateDirectionHorizontal(), _rigidbody.velocity.y);
    }

    private void Jump()
    {
        if (_isJump)
        {
            if (_groundCheck.IsGrounded)
            {
                _rigidbody.velocity = Vector2.up * _forceJump;
            }
        }
        else if (_rigidbody.velocity.y > 0)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * 0.8f);
        }
    }

    private float CalculateDirectionHorizontal()
    {
        return _currentInputDirection.x * _speedMovement;
    }

    public void SetCoint(int coinCost)
    {
        Debug.Log(coinCost);
    }

}
