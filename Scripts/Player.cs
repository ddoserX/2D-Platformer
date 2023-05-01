using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Vector2 _groundBoxSize;
    [SerializeField] private Transform _startPosition;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private Animator _animator;
    private SpriteRenderer _sprite;
    private Rigidbody2D _rigidBody2d;
    private Vector2 _velocity;
    private Vector2 _direction;

    public bool IsGrounded {
        get {
            return GetStateOnGrounded();
        }
    }

    private void Start() {
        _rigidBody2d = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Update() {
        _direction = GetDirectionMove();

        AnimationState();
        FaceFlip();
    }

    private void FixedUpdate() {
        Jump();
        Move();
    }

    private void Move() {
        _velocity.x = _direction.x * _speed * Time.fixedDeltaTime;
        _velocity.y = _rigidBody2d.velocity.y;

        _rigidBody2d.velocity = _velocity;
    }

    private void Jump() {
        if (_direction.y > 0 && IsGrounded) {
            _rigidBody2d.velocity = transform.up * _jumpForce;
        }
    }

    private Vector2 GetDirectionMove() {
        float directionX = Input.GetAxis("Horizontal");
        float directionY = 0f;
        
        if (Input.GetKey(KeyCode.Space)) {
            directionY = 1f;
        }

        return new Vector2(directionX, directionY);
    }

    private bool GetStateOnGrounded() {
        Vector2 checkDirection = _groundCheck.transform.up * -1;

        RaycastHit2D hit = Physics2D.BoxCast(_groundCheck.position, _groundBoxSize, 
        0f, checkDirection, 0f, _groundLayer);

        if (hit.collider == null) {
            return false;
        }

        return true;
    }

    private void FaceFlip() {
        if (_direction.x < 0) {
            _sprite.flipX = true;
        }
        else if (_direction.x > 0) {
            _sprite.flipX = false;
        }
    }

    private void AnimationState() {
        string move = "IsMoved";
        string jump = "IsGrounded";
        string fall = "VelocityY";

        if (_direction.x != 0) {
            _animator.SetBool(move, true);
        }
        else {
            _animator.SetBool(move, false);
        }

        _animator.SetBool(jump, IsGrounded);
        _animator.SetFloat(fall, _rigidBody2d.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.TryGetComponent<Traps>(out Traps trap)) {
            transform.position = _startPosition.position;
        }
    }
}
