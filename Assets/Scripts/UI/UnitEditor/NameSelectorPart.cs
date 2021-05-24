using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Linq;

namespace UnitEditor
{
    //TODO make this class abstract and create 3 new for prefix, suffix and main
    public class NameSelectorPart : MonoBehaviour
    {

        [SerializeField] NameSelectorButton _typeButton;
        [SerializeField] NameSelectorButton _equipmentButton;
        [SerializeField] InputField _input;

        string _defaultName;

        public void SetDefaultName(string name)
        {
            _defaultName = name;
            SetInputText(_defaultName);
            //_typeButton.SetNameIndex(1);
        }

        public void ClearInput()
        {
            _input.text = "";
        }

        public void SetNames(List<string> typeNames, List<string> equipmentNames)
        {
            _typeButton.SetNames(typeNames);
            _equipmentButton.SetNames(equipmentNames);
        }

        public string GetText()
        {
            //TODO: make toLower depends on language;
            return _input.text.Trim().ToLower();
        }

        public void SetInputText(string text)
        {

            var regex = new Regex(@"\$");
            var replaced = regex.Replace(text, "-");
            _input.text = replaced;
        }

    }

}
