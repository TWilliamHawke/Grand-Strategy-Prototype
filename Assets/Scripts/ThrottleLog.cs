using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrottleLog : MonoBehaviour
{
    static float _baseTimeout = 0.5f;
    static float _timeout = _baseTimeout;

    static public void Log(object str)
    {
        _timeout += Time.deltaTime;
        if(_timeout >= _baseTimeout)
        {
            Debug.Log(str);
            _timeout -= _baseTimeout;
        }
    }
}
