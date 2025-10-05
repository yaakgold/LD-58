using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public GameObject level, bossLevel, playerSpawnPosition;
    public bool isPortalActive;
    public Health bossHealth;

    public void OnPortalEnter(GameObject player)
    {
        if (!isPortalActive) return;
        
        level.SetActive(false);
        bossLevel.SetActive(true);
        
        player.transform.position = playerSpawnPosition.transform.position;

        bossHealth.SetHealth(35 + (ItemHolder.Instance.abilityCount * 2));
    }
}
