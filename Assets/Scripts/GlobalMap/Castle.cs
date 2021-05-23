using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Castle : Settlement
{
    
}

public interface ISelectable {
    Transform transform { get; }
    void Select();
    void Deselect();
}