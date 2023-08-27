using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelections : MonoBehaviour
{
    // ıı

    public List<ClickableUnit> unitList = new List<ClickableUnit>();
    public List<ClickableUnit> unitsSelected= new List<ClickableUnit>();
    static UnitSelections _instance;
    public static UnitSelections Instance { get { return _instance; } }
    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
        } else
        {
            _instance = this;
        }
    }

    public void ClickSelect(ClickableUnit unitToAdd)
    {
        DeselectAll();
        unitsSelected.Add(unitToAdd);
        unitToAdd.ActivateSelectionVisual(true);
        unitToAdd.GetComponent<UnitMovement>().enabled = true;
    }
    public void ShiftClickSelect(ClickableUnit unitToAdd)
    {
        if(!unitsSelected.Contains(unitToAdd))
        {
            unitsSelected.Add(unitToAdd);
        unitToAdd.ActivateSelectionVisual(true);
        unitToAdd.GetComponent<UnitMovement>().enabled = true;
        } else
        {
        unitToAdd.ActivateSelectionVisual(false);
        unitToAdd.GetComponent<UnitMovement>().enabled = false;
            unitsSelected.Remove(unitToAdd);
        }
    }
    public void DragSelect(ClickableUnit unitToAdd)
    {
        if(!unitsSelected.Contains(unitToAdd))
        {
            unitsSelected.Add(unitToAdd);
        unitToAdd.GetComponent<UnitMovement>().enabled = true;
            unitToAdd.ActivateSelectionVisual(true);
        }
    }
    public void DeselectAll()
    {
        for(int i = 0; i < unitsSelected.Count; i++)
        {
            unitsSelected[i].ActivateSelectionVisual(false);
            unitsSelected[i].GetComponent<UnitMovement>().enabled = false;

        }
        unitsSelected.Clear();
    }
    public void Deselect(ClickableUnit unitToDeselect)
    {

    }

}
