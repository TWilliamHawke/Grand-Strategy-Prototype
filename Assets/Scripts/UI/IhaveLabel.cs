using UnityEngine;

public interface IhaveLabel : ISelectable
{
    string GetName();
    Color baseLabelColor { get; }
}
