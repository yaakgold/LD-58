using UnityEngine;

public class MeleeAttackController : MonoBehaviour
{
    public MeleeEnemyController mec;

    public void StartAttack()
    {
        mec.isAttacking = true;
    }
    
    public void EndAttack()
    {
        mec.isAttacking = false;
    }
}
