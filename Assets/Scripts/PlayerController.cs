using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed, jumpForce;
    [SerializeField] private LayerMask jumpLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private GameObject gfx;
    [SerializeField] private Quaternion positiveRotation, negativeRotation;
    [SerializeField] private Staff staff;
    
    private Vector2 _movement;
    private Rigidbody2D _rb;
    private Camera _cam;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _cam = Camera.main;
    }

    private void Update()
    {
        gfx.transform.rotation = Vector3.Cross(transform.position, Utils.MousePos(_cam)).z > 0 ? positiveRotation : negativeRotation;
    }

    private void FixedUpdate()
    {
        _rb.AddForce(_movement * speed);
        
        _movement = Vector2.Lerp(_movement, Vector2.zero, Time.fixedDeltaTime);
    }
    
    public void OnMove(InputValue value)
    {
        _movement = value.Get<Vector2>();
        _movement.y = 0;
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
        staff.SwingWeapon();
    }
}
