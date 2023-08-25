using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableUnit : MonoBehaviour
{
    // ıı
    [SerializeField] GameObject selectionVisual;
     UnitMovement unitMovement;
    private void Start()
    {
        UnitSelections.Instance.unitList.Add(this);
        unitMovement = GetComponent<UnitMovement>();
    }
    private void OnDestroy()
    {
        UnitSelections.Instance.unitList.Remove(this);

    }
    public void ActivateSelectionVisual(bool stat)
    {
        selectionVisual.SetActive(stat);
        unitMovement.enabled = stat;
    }
}
