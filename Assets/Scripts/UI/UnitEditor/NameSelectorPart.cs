using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Linq;

namespace UnitEditor
{
    public class NameSelectorPart : MonoBehaviour
    {

        [SerializeField] NameSelectorButton _typeButton;
        [SerializeField] NameSelectorButton _equipmentButton;
        [SerializeField] InputField _input;

        string _defaultName;

        public void SetDefaultName()
        {
            SetInputText(_defaultName);
            _typeButton.SetNameIndex(1);
        }

        public void ClearInput()
        {
            _input.text = "";
        }


        public void SetNames(List<string> typeNames, List<string> equipmentNames)
        {
            _defaultName = typeNames.FirstOrDefault();
            _typeButton.SetNames(typeNames);
            _equipmentButton.SetNames(equipmentNames);
        }

        public void SetInputText(string text)
        {
            var regex = new Regex(@"\$");
            var replaced = regex.Replace(text, "-");
            _input.text = replaced;
        }

        public string GetText()
        {
            //TODO: make toLower depends from language;
            return _input.text.Trim().ToLower();
        }
    }

}
