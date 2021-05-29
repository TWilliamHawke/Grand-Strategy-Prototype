using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Castle : Settlement
{
    [SerializeField] SkirmishController _skirmishController;

    void OnTriggerEnter(Collider other)
    {
        if (isPlayerSettlement) return;

        if (other.TryGetComponent<Army>(out var army))
        {
            _skirmishController.ShowPrebattleScreen(army, settlementData);
        }
    }
}

public interface ISelectable
{
    Transform transform { get; }
    void Select();
    void Deselect();
}