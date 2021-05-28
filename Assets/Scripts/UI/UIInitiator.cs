using UnityEngine;
using System.Linq;

//used for subscribing inactive ui elenements on events
public class UIInitiator : MonoBehaviour
{
    private void Awake() {
        var uiList = FindObjectsOfType<MonoBehaviour>(true).OfType<INeedInit>();

        foreach(var uiElement in uiList)
        {
            uiElement.Init();
        }
    }
}
