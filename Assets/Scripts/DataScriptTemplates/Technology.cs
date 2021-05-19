using System.Collections.Generic;
using Effects;
using UnityEngine;

[CreateAssetMenu(fileName = "TechName", menuName = "Core Game/Technology")]
public class Technology : EffectsContainer
{
    public string localizedName;
    [Space(5)]
    [SerializeField] List<Effect> _effects;  //for properly order in inspector

    public override List<Effect> effects => _effects;

}
