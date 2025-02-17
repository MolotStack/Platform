using UnityEngine;

public class Character : MonoBehaviour
{
    public bool IsDie
    {
        get { return _isDie; }
        set { _isDie = value; }
    }
    public bool IsJump
    {
        get { return _isJump; }
        set { _isJump = value; }
    }
    public int CountJumps
    {
        get { return _countJumps; }
        set { _countJumps = value; }
    }
    public float JumpTimeJumps
    {
        get { return _jumpTime; }
        set { _jumpTime = value; }
    }
    public float CurrentInputDirection
    {
        get { return _currentInputDirection.x; }
        set { _currentInputDirection.x = value; }
    }

    [SerializeField, Range(0, 20)]
    private float _speedMovement;
    [SerializeField, Range(0, 25)]
    private float _forceJump;
    [SerializeField, Range(0, 10)]
    private int _numberOfJumps;
    [SerializeField, Range(0, 1)]
    private float _pressingButtonJumpTime;
    [SerializeField]
    private PhysicsMaterial2D _heroNoFriction;
    [SerializeField]
    private PhysicsMaterial2D _heroFriction;

    [Header("Animation"), Space(15)]
    [SerializeField]
    private Animator _currentAnimator;

    [Header("Health")]
    [SerializeField]
    private bool _isDie;
    [SerializeField]
    private HealthComponent _healthComponent;
    [SerializeField]
    private ParticleSystem _hitParticleSystem;

    [Header("Interact Settigs"), Space(15)]
    [SerializeField]
    private Transform _pointCenterRayZone;
    [SerializeField]
    private float _radiusInteractionZone;
    [SerializeField]
    private Collider2D[] _interactingObjectsCollider = new Collider2D[1];
    [SerializeField]
    private LayerMask _interactingLayer;
    private IInteracting _currentSelectObject;

    [Header("Wallet"), Space(15)]
    [SerializeField]
    private Wallet _currentWallet = new Wallet(100);

    private Vector2 _currentInputDirection;

    private Rigidbody2D _rigidbody;
    private GroundCheck _groundCheck;

    private bool _isJump;
    private int _countJumps;
    private float _jumpTime;

    #region static parametrs

    private static int _animIsGround = Animator.StringToHash("IsGrounded");
    private static int _animIsRun = Animator.StringToHash("IsRuning");
    private static int _animVerticalVelocity = Animator.StringToHash("VerticalVelocity");
    private static int _animAirJump = Animator.StringToHash("AirJumping");
    private static int _animIsDie = Animator.StringToHash("IsDie");
    private static int _animHit = Animator.StringToHash("Hit");
    #endregion

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _groundCheck = GetComponentInChildren<GroundCheck>();
        _currentAnimator = GetComponentInChildren<Animator>();
        //_hitParticleSystem = GetComponentInChildren<ParticleSystem>();

        _healthComponent = GetComponent<HealthComponent>();

        _healthComponent.OnDied += SetStateDie;
        _healthComponent.OnDamage += TakeDamage;
    }

    private void Update()
    {
        if (!_isDie)
        {
            Jump();

            CheackInteractingObjects();
        }
    }

    private void FixedUpdate()
    {
        if (!_isDie)
        {
            Movement();
        }
    }

    private void OnDisable()
    {
        _healthComponent.OnDied -= SetStateDie;
        _healthComponent.OnDamage -= TakeDamage;
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
        SetAnimation();
    }
    private void Jump()
    {

        if (_pressingButtonJumpTime < _jumpTime)
        {
            _jumpTime = 0;
            _isJump = false;
        }

        if (_isJump)
        {
            _jumpTime += Time.deltaTime;

            if (_numberOfJumps > _countJumps)
            {
                _rigidbody.velocity = Vector2.up * _forceJump;
            }
        }

        if (_groundCheck.IsGrounded)
        {
            _countJumps = 0;
        }
    }
    private float CalculateDirectionHorizontal()
    {
        return _currentInputDirection.x * _speedMovement;
    }
    private void SetAnimation()
    {
        if (_currentInputDirection.x != 0)
        {
            _currentAnimator.SetBool(_animIsRun, true);

            if (_currentInputDirection.x > 0)
            {
                transform.localScale = Vector3.one;
            }
            else if (_currentInputDirection.x < 0)
            {
                transform.localScale = new Vector3(-1,1,1);
            }
        }
        else
        {
            _currentAnimator.SetBool(_animIsRun, false);
        }


        _currentAnimator.SetBool(_animIsGround, _groundCheck.IsGrounded);
        _currentAnimator.SetFloat(_animVerticalVelocity, _rigidbody.velocity.y);
    }

    private void CheackInteractingObjects()
    {
        var size = Physics2D.OverlapCircleNonAlloc(_pointCenterRayZone.position, _radiusInteractionZone, _interactingObjectsCollider, _interactingLayer);

        if (size == 0 && _currentSelectObject != null)
        {
            _currentSelectObject.OnDisableVisualHint();
            _currentSelectObject = null;
            return;
        }

        for (int i = 0; i < size; i++)
        {
            IInteracting interactingObjects = _interactingObjectsCollider[i].GetComponent<IInteracting>();
            if (interactingObjects != null)
            {
                if (_currentSelectObject != null && _currentSelectObject != interactingObjects)
                {
                    _currentSelectObject.OnDisableVisualHint();
                    _currentSelectObject = interactingObjects;
                    _currentSelectObject.OnEnableVisualHint();
                }
                else if (_currentSelectObject == null)
                {
                    _currentSelectObject = interactingObjects;
                    _currentSelectObject.OnEnableVisualHint();
                }
            }
        }
    }

    public void SetAnimationAirJump()
    {
        if (_numberOfJumps > _countJumps && _countJumps >= 1)
        {
            _currentAnimator.SetTrigger(_animAirJump);
        }
    }

    public void SetCoint(int coinCost)
    {
        _currentWallet.AddCoin(coinCost);
    }

    public void Interactions()
    {
        _currentSelectObject?.Interaction();
    }

    public void Teleport(Vector3 position)
    {
        transform.position = position;
    }

    private void SetStateDie()
    {
        _isDie = true;
        _rigidbody.sharedMaterial = _heroFriction;
        _currentAnimator.SetBool(_animIsDie, true);
    }

    private void TakeDamage(float damage)
    {
        if (_currentWallet.Coin > 0)
        {
            int intDamage = Mathf.Abs((int)damage);

            if (_currentWallet.Coin < intDamage)
            {
                intDamage = _currentWallet.Coin;
            }

            var burst = _hitParticleSystem.emission.GetBurst(0);
            burst.count = intDamage;
            _hitParticleSystem.emission.SetBurst(0, burst);

            _hitParticleSystem.gameObject.SetActive(true);
            Debug.Log(intDamage);
            _hitParticleSystem.Play();

            _currentWallet.RemoveCoin(intDamage);
        }

        _currentAnimator.SetTrigger(_animHit);
    }
}
