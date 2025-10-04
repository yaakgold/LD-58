using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private GameObject enemyObj;
    
    private MeleeEnemyController _mec;
    
    private void Start()
    {
        if (enemyObj.TryGetComponent(out MeleeEnemyController mec))
        {
            _mec = mec;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") 
            && other.TryGetComponent(out PlayerController pc)
            && !_mec.isRunning)
        {
            _mec.chosenAbility = pc.LoseAbility(enemyObj);
            _mec.isRunning = true;
        }
    }
}
