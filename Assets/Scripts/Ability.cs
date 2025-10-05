using System;
using UnityEngine;
using Random = System.Random;

public class Ability : MonoBehaviour
{
    public SOAbility ability;
    public bool isUsed;
    public PlayerController player;

    [HideInInspector] public RandomItemSpawner spawner;
    
    [SerializeField] private Collider2D col;
    [SerializeField] private SpriteRenderer gfx;
    
    private void Start()
    {
        gfx.sprite = ability.icon;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerController pc))
        {
            Collect(pc);
        }
    }

    private void Collect(PlayerController pc)
    {
        col.enabled = false;
        player = pc;
        
        pc.CollectAbility(gameObject);
        
        StartCoroutine(spawner.Respawn());
    }
    
    public virtual void UseAbility()
    {
        
    }

    public virtual void RemoveAbility()
    {
        
    }
}
