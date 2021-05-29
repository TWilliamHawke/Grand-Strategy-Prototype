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

    public void UpdateUnitsCards(IHaveUnits owner)
    {
        _unitsOwner = owner;
        UpdateUnitsCards();
    }

    public void RemoveSelectedUnitsFromOwner()
    {
        foreach (var card in selectedCards)
        {
            _unitsOwner.RemoveUnit(card.unit);
        }
        ClearSelection();
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

    void SelectCard(UnitCard card)
    {
        if (!_unitCards.Contains(card)) return;

        if (!Input.GetKey(KeyCode.LeftControl))
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
            //save count before clear list
            int selectedCount = selectedCards.Count;
            ClearSelection();

            //if multiple card was selected - select clicked card again
            if (selectedCount > 1)
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


}
