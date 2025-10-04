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
    
    
    private Vector2 _movement;
    private Rigidbody2D _rb;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rb.AddForce(_movement * speed);
        
        _movement = Vector2.Lerp(_movement, Vector2.zero, Time.fixedDeltaTime);
        anim.SetBool(AnimIsWalking, _rb.linearVelocityX != 0);
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
