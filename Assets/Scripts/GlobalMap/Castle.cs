using System.Collections.Generic;
using UnityEngine;


public class Castle : Settlement
{
    [SerializeField] SkirmishController _skirmishController;

    void OnTriggerEnter(Collider other)
    {
        if (isPlayerSettlement) return;

        if (other.TryGetComponent<Army>(out var army))
        {
            _skirmishController.ShowPrebattleScreen(army, this);
        }
    }
}

public interface ISelectable
{
    Transform transform { get; }
    void Select();
    void Deselect();
}