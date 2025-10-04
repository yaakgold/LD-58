using System;
using System.Runtime.InteropServices.ComTypes;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeEnemyController : EnemyController
{
    private static readonly int AnimAttack = Animator.StringToHash("Attack");
    [SerializeField] private Quaternion positiveRotation, negativeRotation;
    [SerializeField] private float detectDistance;
    [SerializeField] private float attackRange;
    [SerializeField] private GameObject arm;
    [SerializeField] private float attackDist;
    [SerializeField] private Animator anim;

    public bool isAttacking, isRunning;

    public GameObject chosenAbility;
    
    private RaycastHit2D _hit;
    private GameObject _portal;
    
    public override void Update()
    {
        if (isRunning)
        {
            gameObject.layer = LayerMask.NameToLayer("EnemyRun");

            if (!_portal) _portal = PortalManager.Instance.portals.GetRandom();

            _col.isTrigger = true;
            _rb.gravityScale = 0;
            _rb.linearVelocity =  Vector2.zero;
            transform.position = Vector3.MoveTowards(transform.position, _portal.transform.position, speed * .5f * Time.deltaTime);

            if (!(Vector3.Distance(transform.position, _portal.transform.position) < .1f)) return;
            chosenAbility.TryGetComponent(out Ability ability);
            ItemHolder.Instance.abilityCount++;
            Destroy(gameObject);

            return;
        }
        
        if (!IsAttacking)
        {
            base.Update();
            
            if (transform.position.x < waypoints[WaypointIndex].position.x)
            {
                transform.rotation = positiveRotation;
            }
            else if (transform.position.x > waypoints[WaypointIndex].position.x)
            {
                transform.rotation = negativeRotation;
            }
            
            _hit = Physics2D.Raycast(transform.position, transform.right * detectDistance, detectDistance, LayerMask.GetMask("Player"));

            if (!_hit.collider) return;
            if (_hit.collider.CompareTag("Player"))
            {
                IsAttacking = true;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, _hit.transform.position, Time.deltaTime);

            if (Vector3.Distance(transform.position, _hit.transform.position) <= attackRange && !isAttacking)
            {
                DoAttack();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isRunning && other.CompareTag("Staff") && other.TryGetComponent(out Staff staff) && staff.isSwinging)
        {
            var player = GameObject.FindGameObjectWithTag("Player");

            if (player.TryGetComponent(out PlayerController pc))
            {
                pc.CollectAbility(chosenAbility);
            }
            
            GetComponent<Health>().TakeDamage(100);
        }
    }

    private void DoAttack()
    {
        anim.SetTrigger(AnimAttack);
    }
}
