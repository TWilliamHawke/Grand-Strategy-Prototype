using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UnitEditor
{
    public class ItemSelectionButton : UIDataElement<Equipment>, IPointerClickHandler
    {
        [SerializeField] ItemSlotController _itemSlotController;
        [SerializeField] TemplateController _templateController;
        [SerializeField] Sprite _meleeSkillSprite;
        [SerializeField] Sprite _rangeSkillSprite;

        [Header("UI Elements")]
        [SerializeField] Image _itemIcon;
        [SerializeField] Image _weaponSkillImage;
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

        public override string GetTooltipText()
        {
            return _tooltipText;
        }

        void SetWeaponSkill()
        {
            if (_itemData is RequireSkillEquipment)
            {
                SetSkillSprite();
                CheckWeaponSkill();
            }
            else
            {
                _weaponSkill.gameObject.SetActive(false);
            }
        }

        void SetItemCost()
        {
            var realCost = _templateController.FindRealItemCost(_itemData);
            _itemCost.text = realCost.ToString();
            var freeGold = _itemSlotController.CalculateFreeGold();

            if (freeGold < realCost)
            {
                SetInactiveDueGold();
            }
        }

        void SetSkillSprite()
        {
            if (_itemData is RangeWeapon)
            {
                _weaponSkillImage.sprite = _rangeSkillSprite;
            }
            else
            {
                _weaponSkillImage.sprite = _meleeSkillSprite;
            }
        }

        void CheckWeaponSkill()
        {
            int realSkill = _templateController.FindRealSkillForItem(_itemData);

            _weaponSkillText.text = realSkill.ToString();
            
            int classSkill = _itemData is RangeWeapon ?
                _templateController.currentTemplate.rangeSkill:
                _templateController.currentTemplate.meleeSkill;

            if (realSkill > classSkill)
            {
                SetInactiveDueSkill();
            }
        }

        void SetInactiveDueGold()
        {
            SetInactive();
            _tooltipText = "<color=red>Not enough gold</color>";
            _itemCost.color = Color.red;
        }

        void SetInactiveDueSkill()
        {
            SetInactive();
            _tooltipText = "<color=red>Weapon skill is too low</color>";
            _weaponSkillText.color = Color.red;
        }

        void SetInactive()
        {
            _isInactive = true;
            _itemName.color = Color.red;
        }

    }

}
