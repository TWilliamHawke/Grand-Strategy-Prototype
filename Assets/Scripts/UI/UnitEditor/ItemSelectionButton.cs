using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UnitEditor
{
    public class ItemSelectionButton : UIDataElement<Equipment>, IPointerClickHandler
    {
        [SerializeField] ItemSlotController _itemSlotController;
        [SerializeField] TemplateController _templateController;

        [Header("UI Elements")]
        [SerializeField] Image _itemIcon;
        [SerializeField] Text _itemName;
        [SerializeField] Text _itemCost;
        [SerializeField] Text _weaponSkillText;
        [SerializeField] RectTransform _weaponSkill;

        Equipment _itemData;
        string _tooltipText;
        bool _isInactive = false;

        public Equipment itemData => _itemData;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_itemData == null || _isInactive) return;

            _itemSlotController?.ChangeItemInSelectedSlot(_itemData);

        }

        public override void UpdateData(Equipment data)
        {
            _itemData = data;
            _itemIcon.sprite = data.sprite;
            _itemName.text = data.Name;
            _tooltipText = _itemData.GetToolTipText();

            SetItemCost();
            SetWeaponSkill();

        }

        private void SetItemCost()
        {
            var realCost = _itemData.CalculateCurrentCost(_templateController);
            _itemCost.text = realCost.ToString();
            var freeGold = _itemSlotController.CalculateFreeGold();

            if (freeGold < realCost)
            {
                SetInactiveDueGold();
            }
        }

        public override string GetTooltipText()
        {
            return _tooltipText;
        }

        public void SetInactiveDueGold()
        {
            SetInactive();
            _tooltipText = "<color=red>Not enough gold</color>";
            _itemCost.color = Color.red;
        }

        public void SetInactiveDueSkill()
        {
            SetInactive();
            _tooltipText = "<color=red>Weapon skill is too low</color>";
            _weaponSkillText.color = Color.red;
        }

        void SetWeaponSkill()
        {
            if (_itemData is RequireSkillEquipment)
            {
                CheckWeaponSkill();
            }
            else
            {
                _weaponSkill.gameObject.SetActive(false);
            }
        }

        void CheckWeaponSkill()
        {
            int realSkill = (_itemData as RequireSkillEquipment)?.CalculateRealSkill(_templateController) ?? 0;

            _weaponSkillText.text = realSkill.ToString();

            if (realSkill > _itemSlotController.classSkill)
            {
                SetInactiveDueSkill();
            }
        }

        void SetInactive()
        {
            _isInactive = true;
            _itemName.color = Color.red;
        }

    }

}
