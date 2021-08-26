using System.Collections;
using System.Collections.Generic;
using Battlefield.Chunks;
using UnityEngine;

namespace Battlefield
{
    [CreateAssetMenu(fileName = "SelectedObjects", menuName = "Battlefield/Selected Objects")]
    public class SelectedObjects : ScriptableObject
    {
        public Chunk hoveredChunk { get; set; }
        public Troop troop { get; set; }
        public ISelectable selectable { get; set; }

        private void OnEnable()
        {
            hoveredChunk = null;
            troop = null;
        }

        public void SetHoveredChunk(Chunk chunk)
        {
            if (hoveredChunk == chunk) return;
            if (troop?.chunk != hoveredChunk)
            {
                hoveredChunk?.SetDefaultFrameColor();
            }
            hoveredChunk = chunk;

            if (troop?.chunk == chunk)
            {
                chunk.UpdateFrameColors((int)troop.direction);
            }
            else
            {
                chunk.SetHoverColor();
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

        public bool GetSelectedObject<T>(out T selectable) where T : class, ISelectable
        {
            selectable = default(T);

            if (this.selectable is T)
            {
                selectable = this.selectable as T;
                return true;
            }

            return false;
        }
    }

}
