using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabelController : MonoBehaviour
{
    [SerializeField] Label _labelPrefab;

    private void Awake() {
        Castle.OnSettlementInit += CreateLabel;
    }

    private void OnDestroy() {
        Castle.OnSettlementInit -= CreateLabel;
    }

    void CreateLabel(Settlement castle)
    {
        var label = Instantiate(_labelPrefab, transform);
        label.AddParent(castle);
    }
}
