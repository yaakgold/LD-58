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
        Instantiate(abilities[Random.Range(0, abilities.Count)], transform.position, Quaternion.identity);
    }
}
