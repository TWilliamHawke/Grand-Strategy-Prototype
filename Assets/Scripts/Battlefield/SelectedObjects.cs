using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battlefield
{
    [CreateAssetMenu(fileName = "SelectedObjects", menuName = "Battlefield/Selected Objects")]
    public class SelectedObjects : ScriptableObject
    {
        public Square hoveredSquare { get; set; }
        public Troop troop { get; set; }
        public ISelectable selectable { get; set; }

        private void OnEnable()
        {
            hoveredSquare = null;
            troop = null;
        }

        public void SetHoveredSquare(Square square)
        {
            if (hoveredSquare == square) return;
            if(troop?.square != hoveredSquare)
            {
                hoveredSquare?.SetDefaultFrameColor();
            }
            hoveredSquare = square;

            if(troop?.square == square) {
                square.UpdateFrameColors(troop.direction);
            } else {
                square.SetHoverColor();
            }
        }

        //troop has special selection function
        //where we select parent of hit target
        public void SetSelectedTroop(Troop troop)
        {
            if (troop == this.troop) return;

            this.troop?.Deselect();

            troop.Select();
            this.troop = troop;
        }

        public void SelectObject(ISelectable obj)
        {
            if (obj == selectable) return;

            troop?.Deselect();

            obj.Select();
            selectable = obj;
        }

        public bool GetSelectedObject<T>(out T selectable) where T: class, ISelectable
        {
            selectable = default(T);

            if(this.selectable is T)
            {
                selectable = this.selectable as T;
                return true;
            }

            return false;
        }
    }

}
