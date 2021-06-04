using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace Prebattle
{
    public class PrebattleUnitList : MonoBehaviour
    {
        [SerializeField] Text _totalCountText;
        [SerializeField] List<PrebattleUnitCard> _unitCards;

        public void UpdateForceData(IHaveUnits force)
        {
            UpdateCards(force);
            UpdataTotalCount(force);
        }

        void UpdateCards(IHaveUnits force)
        {
            for (int i = 0; i < _unitCards.Count; i++)
            {
                if (i < force.unitList.Count)
                {
                    _unitCards[i].UpdateData(force.unitList[i].unitTemplate);
                    _unitCards[i].gameObject.SetActive(true);
                }
                else
                {
                    _unitCards[i].gameObject.SetActive(false);
                }
            }
        }

        void UpdataTotalCount(IHaveUnits force)
        {
            var count = force.unitList
                .Sum(u => u.unitTemplate.unitClass.unitSize);

            _totalCountText.text = "Total soldiers: " + count;
        }
    }
}