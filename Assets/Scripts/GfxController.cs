using UnityEngine;

public class GfxController : MonoBehaviour
{
    [SerializeField] private Staff staff;

    public void SetStaffSwingTrue()
    {
        staff.isSwinging = true;
    }
    
    public void SetStaffSwingFalse()
    {
        staff.isSwinging = false;
    }
}
