using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalMap.Espionage
{
    [CreateAssetMenu(fileName = "SpyAction", menuName = "Global map/SpyAction")]
    public class SpyAction : ScriptableObject
    {
        [SerializeField] SpyNetworkController _controller;
        [SerializeField] string _title;
        [Multiline(4)]
        [SerializeField] string _description;

        [SerializeField] float _baseCost;
        [SerializeField] float _baseChance;
        [SerializeField] float _baseVisibility;

        public string title => _title;
        public float successChance => _baseChance + _controller.currentGuardState.actionsSuccessChance;
        public float visibility => _baseVisibility * TransformIntoMult(_controller.currentGuardState.visibilityPerActionPct);
        public float cost => _baseCost * TransformIntoMult(_controller.currentGuardState.actionCostPct);

        float TransformIntoMult(float pct)
        {
            return 1 + pct / 100;
        }
    }
}