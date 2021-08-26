using UnityEngine;

namespace Battlefield.Chunks
{
    public interface IPounterController
    {
        void RotatePointer(Vector3 position);
        void HidePointer();
        void ShowPointer();
        Directions currentDirection { get; }
    }
}
