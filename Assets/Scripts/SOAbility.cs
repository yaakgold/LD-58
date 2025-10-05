using UnityEngine;

[CreateAssetMenu(fileName = "SOAbility", menuName = "Scriptable Objects/SOAbility")]
public class SOAbility : ScriptableObject
{
    public Sprite icon;
    public string description;
    public bool isOneTime;
}
