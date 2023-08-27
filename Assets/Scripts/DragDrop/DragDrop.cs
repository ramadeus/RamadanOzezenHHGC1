using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop: MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {
    RectTransform rectTransform;
    [SerializeField] RectTransform referenceTransform;
    [SerializeField] Canvas canvas;
    CanvasGroup canvasGroup;
    Vector2 firstPos;
    Transform firstParent;
    Mask parentMask;
    public bool isDrop;
    public CharType charType;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        InitializeFirstSettings();

    }

    private void InitializeFirstSettings()
    {
        firstParent = transform.parent;
        parentMask = transform.parent.GetComponent<Mask>();
        firstPos = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDrop = false;
        ItemSlot parentSlot = GetComponentInParent<ItemSlot>();
        if(parentSlot != null)
        {
            parentSlot.ClearItem();
        }
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
        parentMask.enabled = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        if(!isDrop)
        {
            OnDragCancel();
            EventsManager.OnCharSelectionSuccess?.Invoke(false);
        } else
        {
            EventsManager.OnCharSelectionSuccess?.Invoke(true);
        }
    }
    public void OnDragCancel()
    {
        transform.SetParent(firstParent);
        parentMask.enabled = true;
        rectTransform.anchoredPosition = firstPos;
        rectTransform.pivot = referenceTransform.pivot;
        rectTransform.sizeDelta = referenceTransform.sizeDelta;
        rectTransform.anchorMin = referenceTransform.anchorMin;
        rectTransform.anchorMax = referenceTransform.anchorMax;
        rectTransform.anchoredPosition = referenceTransform.anchoredPosition;

    }
    // ıı



}
