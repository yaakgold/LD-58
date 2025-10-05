using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class AbilityDisplayController : Singleton<AbilityDisplayController>
    {
        public List<SOAbility> abilities = new List<SOAbility>();
        public GameObject prefab;
        public Transform content;
        public Tooltip tooltip;

        public void AddAbility(SOAbility ability)
        {
            abilities.Add(ability);
            var go = Instantiate(prefab, content);

            if (go.TryGetComponent(out AbilityDisplay display))
            {
                display.ability = ability;
                display.tooltip = tooltip;
            }
        }
        
        public void RemoveAbility(SOAbility ability)
        {
            abilities.Remove(ability);
            for (int i = 0; i < content.childCount; i++)
            {
                if (content.GetChild(i).TryGetComponent(out AbilityDisplay display))
                {
                    if (display.ability == ability)
                    {
                        Destroy(display.gameObject);
                        return;
                    }
                }
            }
        }
        
    }
}
