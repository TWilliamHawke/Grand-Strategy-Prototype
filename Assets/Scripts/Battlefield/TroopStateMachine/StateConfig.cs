using UnityEngine;

[CreateAssetMenu(fileName = "StateConfig", menuName = "Battlefield/State Config")]
public class StateConfig : ScriptableObject
{
    [Header("State Icons")]
    public Sprite defaultStateIcon;
    public Sprite movementStateIcon;
    public Sprite rotationStateIcon;
    public Sprite readyStateIcon;
    public Sprite chargeStateIcon;
    public Sprite fightStateIcon;

    [Header("params")]
    public int movementTime = 10;
}
