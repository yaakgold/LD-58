using System;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : Singleton<PortalManager>
{
    public List<GameObject> portals = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            portals.Add(transform.GetChild(i).gameObject);
        }
    }
}
