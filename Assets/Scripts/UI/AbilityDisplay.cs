using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class AbilityDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public SOAbility ability;
        public Tooltip tooltip;
        public Image image;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            tooltip.SetValues(ability);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            tooltip.gameObject.SetActive(false);
        }
    }
}
