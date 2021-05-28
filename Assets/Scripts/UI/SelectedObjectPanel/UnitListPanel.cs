using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnitListPanel : MonoBehaviour
{
    [SerializeField] List<UnitCard> _unitCards;
    
    static public event UnityAction OnUnitCardSelection;

    protected List<UnitCard> selectedCards = new List<UnitCard>();

    protected IHaveUnits _unitsOwner;

    public void UpdateUnitsCards(IHaveUnits owner)
    {
        _unitsOwner = owner;
        UpdateUnitsCards();
    }

    protected virtual void UpdateUnitsCards()
    {
        selectedCards.Clear();

        for (int i = 0; i < _unitCards.Count; i++)
        {
            var card = _unitCards[i];
            card.Deselect();
            if (i < _unitsOwner.unitList.Count)
            {
                card.gameObject.SetActive(true);
                card.UpdateData(_unitsOwner.unitList[i]);
            }
            else
            {
                card.gameObject.SetActive(false);
            }
        }
    }

    void Awake()
    {
        UnitCard.OnSelect += SelectCard;
        UnitCard.OnDeSelect += DeselectCard;
    }

    void OnDisable()
    {
        ClearSelection();
    }

    void OnDestroy()
    {
        UnitCard.OnSelect -= SelectCard;
        UnitCard.OnDeSelect -= DeselectCard;
    }

    void SelectCard(UnitCard card)
    {
        if (!_unitCards.Contains(card)) return;

        if (Input.GetKey(KeyCode.LeftControl))
        {
        }
        else
        {
            ClearSelection();
        }
        selectedCards.Add(card);
        OnUnitCardSelection?.Invoke();
    }

    void DeselectCard(UnitCard card)
    {
        if (!_unitCards.Contains(card)) return;

        if (Input.GetKey(KeyCode.LeftControl))
        {
            selectedCards.Remove(card);
        }
        else
        {
            int sevectedCount = selectedCards.Count;
            ClearSelection();
            if (sevectedCount > 1)
            {
                selectedCards.Add(card);
                OnUnitCardSelection?.Invoke();
                card.Select();
            }
        }
    }

    void ClearSelection()
    {
        foreach (var UnitCard in selectedCards)
        {
            UnitCard.Deselect();
        }
        selectedCards.Clear();

    }

    public void RemoveSelectedUnitsFromOwner()
    {
        foreach(var card in selectedCards)
        {
            _unitsOwner.RemoveUnit(card.unit);
        }
        ClearSelection();
    }


}
