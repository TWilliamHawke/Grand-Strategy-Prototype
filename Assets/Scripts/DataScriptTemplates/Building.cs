using System.Collections;
using System.Collections.Generic;
using Effects;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingNAme", menuName = "Core Game/Building")]
public class Building : EffectsContainer
{
    public string localizedName;
    [SpritePreview()]
    public Sprite icon;
    public int goldCost;
    public BuildingSlots BuildingSlot;

    [Space(5)]
    [SerializeField] List<Effect> _effects; //for properly order in inspector

    public override List<Effect> effects => _effects;

    public string GetFullDescription()
    {
        var effect = GetEffectsDescription();

        return $"<b>{localizedName}</b>\n{effect}";

    }
}

public enum BuildingSlots
{
    castleMain,
    castlePort,
    castleAny,
}