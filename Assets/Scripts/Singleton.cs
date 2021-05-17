using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T: MonoBehaviour
{
    static T _instance;
    public static T instance
    {
        get
        {
            if(_instance == null)
            _instance = FindObjectOfType<T>();
            if(_instance == null)
            {
                throw new System.Exception("Object doesnt exist in scene");
            }
            return _instance;
        }
    }
}
