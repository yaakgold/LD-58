using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private static readonly int AnimIsWalking = Animator.StringToHash("IsWalking");
    private static readonly int AnimAttack = Animator.StringToHash("Attack");
    
    [SerializeField] private float speed, jumpForce;
    [SerializeField] private LayerMask jumpLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private GameObject gfx;
    [SerializeField] private Quaternion positiveRotation, negativeRotation;
    [SerializeField] private Animator anim;
    [SerializeField] private float maxXSpeed;
    
    private Vector2 _movement;
    private Rigidbody2D _rb;
    private float _defaultGravityScale;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _defaultGravityScale = _rb.gravityScale;
    }

    private void FixedUpdate()
    {
        _rb.AddForce(_movement * speed);

        _rb.linearVelocityX = Mathf.Clamp(_rb.linearVelocityX, -maxXSpeed, maxXSpeed);
        
        anim.SetBool(AnimIsWalking, _rb.linearVelocityX != 0);

        if (_rb.linearVelocityY < 0)
        {
            _rb.gravityScale = _defaultGravityScale * 3;
        }
        else
        {
            _rb.gravityScale = _defaultGravityScale;
        }
    }
    
    public void OnMove(InputValue value)
    {
        _movement = value.Get<Vector2>();
        _movement.y = 0;
        
        gfx.transform.rotation = _movement.x > 0 ? positiveRotation : _movement.x == 0 ? gfx.transform.rotation : negativeRotation;
    }

    public void OnJump(InputValue value)
    {
        var hit = Physics2D.CircleCast(groundCheck.position, .1f, Vector2.down, .5f,  jumpLayer);
        if (hit.collider != null)
        {
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public void OnAttack(InputValue value)
    {
        anim.SetTrigger(AnimAttack);
    }
}
