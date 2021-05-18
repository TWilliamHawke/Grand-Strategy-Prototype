using UnityEngine;

namespace Effects
{
    public abstract class Effect : ScriptableObject
    {
        public abstract string GetText();
    }
}