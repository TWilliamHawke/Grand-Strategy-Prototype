using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EmptyEffect", menuName = "Effects/Empty", order = 0)]
public class EmptyEffect : Effect
{
    [TextArea]
    [SerializeField] string _effectDescription;

    public override string GetText()
    {
        return _effectDescription;
    }
}
