using TMPro;
using UnityEngine;

namespace UI
{
    public class Tooltip : MonoBehaviour
    {
        [SerializeField] private TMP_Text abName, description;

        public void SetValues(SOAbility ability)
        {
            gameObject.SetActive(true);
            abName.text = ability.name;
            description.text = ability.description;
        }
    }
}
