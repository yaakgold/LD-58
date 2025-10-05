using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    private static readonly int AttackFly = Animator.StringToHash("Fly");
    [SerializeField] private GameObject enemyObj;
    
    private MeleeEnemyController _mec;
    
    private void Start()
    {
        if (enemyObj.TryGetComponent(out MeleeEnemyController mec))
        {
            _mec = mec;
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") 
            && other.TryGetComponent(out PlayerController pc)
            && !_mec.isRunning
            && _mec.isAttacking)
        {
            _mec.chosenAbility = pc.LoseAbility(enemyObj);
            _mec.isRunning = true;
            _mec.isAttacking = false;
            _mec.anim.SetTrigger(AttackFly);

            pc.Knockback(true);
        }
    }
}
