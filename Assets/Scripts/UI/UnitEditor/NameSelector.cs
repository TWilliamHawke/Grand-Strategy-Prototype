using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace UnitEditor
{
    //TODO make complex system with localisation support
    //and different word's form (-ой, -ая, -ое etc)
    public class NameSelector : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] NameSelectorPart _prefixSelector;
        [SerializeField] NameSelectorPart _mainSelector;
        [SerializeField] NameSelectorPart _sufffixSelector;
        [SerializeField] Image _warningMessage;

        [Header("Scriptable Objects")]
        [SerializeField] TemplateController _templateController;
        [SerializeField] UIScreensManager _uiScreenManager;

        List<UnitNamePart> _typeNames;
        List<UnitNamePart> _equipmentNames;

        private void OnEnable()
        {
            GetNames();
        }

        public void SaveTemplate()
        {
            var prefix = _prefixSelector.GetText();
            var main = _mainSelector.GetText();
            var suffix = _sufffixSelector.GetText();

            if(main.Length < 3)
            {
                ShowWarningMessage();
                return;
            }

            _templateController.currentTemplate.namePrefix = prefix;
            _templateController.currentTemplate.nameCore = main;
            _templateController.currentTemplate.nameSuffix = suffix;

            _templateController.SaveTemplate();

            gameObject.SetActive(false);

            var templateViewer = FindObjectOfType<UnitTemplateViewer>(true);
            _uiScreenManager.OpenScreen(templateViewer);
        }

        public void ShowWarningMessage()
        {
            _warningMessage.gameObject.SetActive(true);
        }

        public void HideWarningMessage()
        {
            _warningMessage.gameObject.SetActive(false);
        }


        void GetNames()
        {
            //TODO replace this shit
            //look at NameSelectorPart.cs
            _typeNames = _templateController.currentTemplate.GetPossibleNamesByType();
            _equipmentNames = _templateController.currentTemplate.GetPossibleNamesByEquipment();

            _prefixSelector.SetNames(GetPrefixes(_typeNames), GetPrefixes(_equipmentNames));
            _mainSelector.SetNames(GetMain(_typeNames), GetMain(_equipmentNames));
            _sufffixSelector.SetNames(GetSuffixes(_typeNames), GetSuffixes(_equipmentNames));
            _prefixSelector.SetDefaultName(_templateController.currentTemplate.namePrefix);
            _mainSelector.SetDefaultName(_templateController.currentTemplate.nameCore);
            _sufffixSelector.SetDefaultName(_templateController.currentTemplate.nameSuffix);
        }

        List<string> GetPrefixes(List<UnitNamePart> namePartlist)
        {
            var nameList = new List<string>();

            foreach (var namePart in namePartlist)
            {
                nameList.AddRange(namePart.prefix);
            }

            return nameList;
        }

        List<string> GetMain(List<UnitNamePart> namePartlist)
        {
            var nameList = new List<string>();

            foreach (var namePart in namePartlist)
            {
                nameList.AddRange(namePart.main);
            }

            return nameList;
        }

        List<string> GetSuffixes(List<UnitNamePart> namePartlist)
        {
            var nameList = new List<string>();

            foreach (var namePart in namePartlist)
            {
                nameList.AddRange(namePart.suffix);
            }

            return nameList;
        }
    }

}
