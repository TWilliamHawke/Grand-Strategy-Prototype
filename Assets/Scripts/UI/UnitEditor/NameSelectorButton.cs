using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace UnitEditor
{
[RequireComponent(typeof(Button))]
public class NameSelectorButton : MonoBehaviour
{
    [SerializeField] Text _namesCountText;

    public UnityEvent<string> setInputText;

    public int nameIndex { get; set; }
    public int maxIndex { get; set; }

    List<string> _names;

    Button _button;

    void UpdateText()
    {
        string count = $"{nameIndex}/{maxIndex}";
        _namesCountText.text = count;
    }

    public void SetNameIndex(int index)
    {
        nameIndex = index;
        UpdateText();
    }

    public void SetMaxIndex(int index)
    {
        _button = GetComponent<Button>();
        maxIndex = index;
        nameIndex = 0;

        if(maxIndex == 0)
        {
            _button.interactable = false;
        }
        else
        {
            _button.interactable = true;
        }
        UpdateText();
    }

    public void SetNames(List<string> names)
    {
        _names = names;
        SetMaxIndex(names.Count);
    }

    //button listener
    public void SetTextFromList()
    {
        if(nameIndex == maxIndex)
        {
            nameIndex = 0;
        }

        //this is unity event from inspector
        setInputText.Invoke(_names[nameIndex]);
        nameIndex++;
        UpdateText();
    }
}

}
