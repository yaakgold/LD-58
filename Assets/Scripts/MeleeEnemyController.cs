using DG.Tweening;
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

    private RaycastHit2D _hit;
    public bool isAttacking;
    
    public override void Update()
    {
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

    private void DoAttack()
    {
        anim.SetTrigger(AnimAttack);
    }
}
