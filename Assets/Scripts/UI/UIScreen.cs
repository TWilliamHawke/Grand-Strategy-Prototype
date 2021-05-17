using UnityEngine;

abstract public class UIScreen : MonoBehaviour
{
    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

}
