using System;
using System.Collections;
using DG.Tweening;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossController : MonoBehaviour
{
    private static readonly int AttackSpawnEnemies = Animator.StringToHash("SpawnEnemies");
    private static readonly int AttackFly = Animator.StringToHash("Fly");
    private static readonly int AttackReturnToIdle = Animator.StringToHash("ReturnToIdle");
    private static readonly int Dead = Animator.StringToHash("Dead");

    [SerializeField] private Transform leftPlatform, rightPlatform, leftEdge, rightEdge;
    [SerializeField] private float moveSpeed, spawnForce;
    [SerializeField] private int enemyCount;
    [SerializeField] private GameObject enemyPref;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Animator anim;
    
    public Coroutine BetweenTimerVar;
    public bool isAttacking;
    
    private bool _isFlying;
    private bool _useLeftPlatform;

    private void Start()
    {
        GetComponent<Health>().OnDeath.AddListener(OnDeath);
        _useLeftPlatform = true;

        BetweenTimerVar = StartCoroutine(BetweenTimer(4));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isAttacking) return;

        if (!other.CompareTag("Player")) return;

        if (other.TryGetComponent(out PlayerController player))
        {
            player.LoseAbility(gameObject);
        }
    }

    private void OnDeath()
    {
        UIManager.Instance.OnPlayerWin();
        anim.SetTrigger(Dead);
    }

    private IEnumerator BetweenTimer(float time)
    {
        yield return new WaitForSeconds(time);
        
        ChooseNextAttack();
    }

    private void ChooseNextAttack()
    {
        var randomChance = Random.Range(0.0f, 1.0f);

        if (randomChance < 0.5f) // Do Spawn Enemies
        {
            anim.SetTrigger(AttackSpawnEnemies);
        }
        else // Do fly attack
        {
            DoFlyAttack();
        }
    }
    
    #region Spawn Enemy Attack
    public void DoSpawnEnemyAttack()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        var enemies = enemyCount;

        while (enemies > 0)
        {
            enemies--;
            
            var go = Instantiate(enemyPref,  spawnPoint.position, spawnPoint.rotation);

            if (go.TryGetComponent(out EnemyController ec))
            {
                ec.GetWaypoint(0).transform.position = leftEdge.position;
                ec.GetWaypoint(0).transform.rotation = leftEdge.rotation;
                ec.GetWaypoint(1).transform.position = rightEdge.position;
                ec.GetWaypoint(1).transform.rotation = rightEdge.rotation;
            }
            
            yield return new WaitForSeconds(.2f);
        }
        
        BetweenTimerVar = StartCoroutine(BetweenTimer(4));
    }
    #endregion
    
    #region Fly Attack

    private void DoFlyAttack()
    {
        StartCoroutine(HandleFlyAttack());
    }

    private IEnumerator HandleFlyAttack()
    {
        anim.SetTrigger(AttackFly);
        
        yield return new WaitForSeconds(.6f);
        
        transform.DOMoveX(transform.position.x + (_useLeftPlatform ? -100 : 100), .75f)
            .OnComplete(() =>
            {
                isAttacking = true;
                transform.localPosition = new Vector3(transform.localPosition.x, 1.25f, transform.localPosition.z);
                transform.DOMoveX(transform.position.x + (_useLeftPlatform ? 300 : -300), 4)
                    .OnComplete(() =>
                    {
                        anim.SetTrigger(AttackReturnToIdle);
                        isAttacking = false;
                        var randomSide = Random.Range(0, 2);
                        transform.position = new Vector3(
                            randomSide == 0 ? leftPlatform.position.x : rightPlatform.position.x, 
                            (randomSide == 0 ? leftPlatform.position.y : rightPlatform.position.y) + 100,
                            transform.position.z);

                        if (randomSide == 0)
                        {
                            _useLeftPlatform = false;
                            var rot = transform.rotation;
                            rot.y = 180;
                            transform.rotation = rot;
                        }
                        else
                        {
                            _useLeftPlatform = true;
                            var rot = transform.rotation;
                            rot.y = 0;
                            transform.rotation = rot;
                        }
                        transform.DOMoveY(randomSide == 0 ? leftPlatform.position.y : rightPlatform.position.y, 4)
                            .OnComplete(() => BetweenTimerVar = StartCoroutine(BetweenTimer(4)));
                    });
            });
    }
    #endregion
}
