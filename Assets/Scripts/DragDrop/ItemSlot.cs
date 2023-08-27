using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot: MonoBehaviour, IDropHandler {
    // ıı
    [SerializeField] RectTransform referenceTransform;
    public DragDrop currentItem;
    private void OnEnable()
    {
        EventsManager.OnCharSelectionSuccess += OnCharSelect;
    }
    private void OnDisable()
    {
        EventsManager.OnCharSelectionSuccess -= OnCharSelect; 
    }

    private void OnCharSelect(bool stat)
    { 
        EventsManager.OnCharSelectionCheck?.Invoke();
    }

    public void OnDrop(PointerEventData eventData)
    {  
        if(currentItem != null)
        {
            currentItem.OnDragCancel();
        }
        if(eventData.pointerDrag != null)
        {
            if(eventData.pointerDrag.TryGetComponent(out DragDrop item))
            {

                item.isDrop = true;
                currentItem = item;
            }
            eventData.pointerDrag.transform.SetParent(transform.GetChild(0));
            RectTransform rectTransform = eventData.pointerDrag.GetComponent<RectTransform>();
         
            rectTransform.pivot = referenceTransform.pivot;
            rectTransform.sizeDelta = referenceTransform.sizeDelta;
            rectTransform.anchorMin = referenceTransform.anchorMin;
            rectTransform.anchorMax = referenceTransform.anchorMax;
            rectTransform.anchoredPosition = referenceTransform.anchoredPosition; 
        }
    } 
    public void ClearItem()
    {
        currentItem = null;

    }
}
