using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GlobalMap
{

[CreateAssetMenu(fileName = "Selector", menuName = "Core Game/Selector")]
    public class GlobalMapSelectable : ScriptableObject
    {
        public event UnityAction<ISelectable> OnSelect;
        public event UnityAction OnSelectionCancel;

        ISelectable _selectedObject;

        public ISelectable selectedObject => _selectedObject;

        public void Select(ISelectable nextTarget)
        {
            _selectedObject?.Deselect();
            _selectedObject = nextTarget;
            nextTarget.Select();
            OnSelect?.Invoke(nextTarget);
        }

        public void CancelSelection()
        {
            if (_selectedObject == null) return;

            _selectedObject.Deselect();
            _selectedObject = null;
            OnSelectionCancel?.Invoke();
        }
    }
}