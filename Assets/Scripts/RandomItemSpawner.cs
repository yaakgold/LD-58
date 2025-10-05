using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItemSpawner : MonoBehaviour
{
    public List<GameObject> abilities; 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnItem();
    }

    private void SpawnItem()
    {
        var go = Instantiate(abilities[Random.Range(0, abilities.Count)], transform.position, Quaternion.identity);
        if (go.TryGetComponent(out Ability ability))
        {
            ability.spawner = this;
        }
    }

    public IEnumerator Respawn()
    {
        yield return new WaitForSeconds(10);
        
        SpawnItem();
    }
}
