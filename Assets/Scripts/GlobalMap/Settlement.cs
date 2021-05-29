using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settlement : MonoBehaviour, IhaveLabel, ISelectable
{
    [SerializeField] SettlementData _settlementData;
    [SerializeField] MeshRenderer _selector;

    public bool isPlayerSettlement => _settlementData.isPlayerSettlement;
    protected SettlementData settlementData => _settlementData;

    public static event System.Action<Settlement> OnSettlementInit;
    public static event System.Action<SettlementData> OnSettlementSelect;

    void Start()
    {
        OnSettlementInit?.Invoke(this);
        _settlementData.SetSettlementPostion(transform.position);
    }


    public void Deselect()
    {
        _selector.gameObject.SetActive(false);
    }

    public string GetName() => _settlementData.localizedName;

    public void Select()
    {
        _selector.gameObject.SetActive(true);
        OnSettlementSelect?.Invoke(_settlementData);
        SettlementData.selectedSettlement = _settlementData;
    }


}