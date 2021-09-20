using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "SkirmishController", menuName = "Core Game/Skirmish Controller")]
public class SkirmishController : ScriptableObject
{
    Army _attackerForce;
    IHaveUnits _defenderForce;

    //temp
    [SerializeField] Faction _playerFaction;

    public event UnityAction<Army, IHaveUnits> OnConfrontationStart;
    public event UnityAction OnConfrontationEnd;

    public void ShowPrebattleScreen(Army attacker, IHaveUnits defender)
    {
        _attackerForce = attacker;
        _defenderForce = defender;
        OnConfrontationStart?.Invoke(attacker, defender);
    }

    public void Retreat()
    {
        _attackerForce.Defeat(_playerFaction);
        OnConfrontationEnd?.Invoke();
    }

    public void WinBattle()
    {
        _defenderForce.Defeat(_playerFaction);
        OnConfrontationEnd?.Invoke();
    }

}
