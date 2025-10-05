using System;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject text;
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            text.SetActive(true);
            LevelManager.Instance.isPortalActive = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            text.SetActive(false);
            LevelManager.Instance.isPortalActive = false;
        }
    }
}
