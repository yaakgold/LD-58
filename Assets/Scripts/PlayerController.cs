using System;
using System.Collections.Generic;
using DG.Tweening;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private static readonly int AnimIsWalking = Animator.StringToHash("IsWalking");
    private static readonly int AnimAttack = Animator.StringToHash("Attack");
    
    [SerializeField] private float speed, jumpForce, rotSpeed, abilityDisplayDist;
    [SerializeField] private LayerMask jumpLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private GameObject gfx;
    [SerializeField] private Quaternion positiveRotation, negativeRotation;
    [SerializeField] private Animator anim;
    [SerializeField] private float maxXSpeed;
    [SerializeField] private GameObject abilityRing;
    [SerializeField] private Staff staff;
    
    private int _jumpsRemaining;
    private int _jumpsReset;
    private List<GameObject> _abilities;
    private Vector2 _movement;
    private Rigidbody2D _rb;
    private float _defaultGravityScale;
    
    private void Start()
    {
        _abilities = new List<GameObject>();
        _rb = GetComponent<Rigidbody2D>();
        _defaultGravityScale = _rb.gravityScale;
    }

    private void Update()
    {
        abilityRing.transform.Rotate(Vector3.forward, Time.deltaTime * rotSpeed);
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Jumpable"))
        {
            _jumpsRemaining = _jumpsReset;
        }
    }

    public void CollectAbility(GameObject ability)
    {
        _abilities.Add(ability);
        
        ability.transform.SetParent(abilityRing.transform);
        ability.transform.DOScale(Vector3.one * .25f, 1f);
        ability.transform.DOLocalMove(Utils.GetRandomInUnitWithDist(abilityDisplayDist), 1f);

        if (ability.TryGetComponent(out Ability abilityComponent))
        {
            AbilityDisplayController.Instance.AddAbility(abilityComponent.ability);
            abilityComponent.UseAbility();
        }
    }

    public GameObject LoseAbility(GameObject enemy)
    {
        var chosenAbility = _abilities.GetRandom();
        _abilities.Remove(chosenAbility);

        if (!chosenAbility)
        {
            // This means I died
            Destroy(gameObject);
        
            return null;
        }
        
        if (chosenAbility.TryGetComponent(out Ability abilityComponent))
        {
            AbilityDisplayController.Instance.RemoveAbility(abilityComponent.ability);
            abilityComponent.RemoveAbility();
        }
        
        chosenAbility.transform.SetParent(enemy.transform);
        chosenAbility.transform.DOMove(enemy.transform.position, 1f);
        
        return chosenAbility;
    }
    
    // Ability methods
    public void AddSpeed(float spd)
    {
        speed += spd;
    }

    public void AddJumpForce(float jmp)
    {
        jumpForce += jmp;
    }

    public void AddJump()
    {
        _jumpsRemaining++;
        _jumpsReset++;
    }

    public void RemoveJump()
    {
        _jumpsRemaining--;
        _jumpsReset++;
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
        else if (_jumpsRemaining > 0)
        {
            _jumpsRemaining = Mathf.Max(0, _jumpsRemaining - 1);
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public void OnAttack(InputValue value)
    {
        if (staff.isSwinging) return;
        staff.isSwinging = true;
        anim.SetTrigger(AnimAttack);
    }
}
