using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GlobalMap
{
    public class SelectionController : MonoBehaviour
    {
        [SerializeField] LayerMask _selectableObjects;
        [SerializeField] GlobalMapSelectable _selector;

        void Update()
        {
            SelectObjectByClick();
        }

        private void SelectObjectByClick()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            if (EventSystem.current.IsPointerOverGameObject()) return;

            if (Raycasts.HitTarget<ISelectable>(out var nextTarget, _selectableObjects))
            {
                _selector.Select(nextTarget);
            }
            else
            {
                _selector.CancelSelection();
            }

        }
    }
}