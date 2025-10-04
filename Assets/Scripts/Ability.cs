using System;
using UnityEngine;
using Random = System.Random;

public class Ability : MonoBehaviour
{
    public bool isOneTime;
    public bool isUsed;
    public PlayerController player;

    [SerializeField] private Collider2D col;

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
    }
    
    public virtual void UseAbility()
    {
        
    }

    public virtual void RemoveAbility()
    {
        
    }
}
