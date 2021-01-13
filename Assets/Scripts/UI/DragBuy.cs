using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragBuy : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    private Canvas canvas;

    private RectTransform myRectTransform;
    [SerializeField]
    private GameObject towerBlueprint;

    private void Awake()
    {
        myRectTransform = GetComponent<RectTransform>();
        towerBlueprint.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Click");
        towerBlueprint.SetActive(true);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Draging");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag");
        //Vector2 localMousePosition = imgRectTransform.InverseTransformPoint(Input.mousePosition);

        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        myRectTransform.anchoredPosition += eventData.delta;
        //Camera.main.ScreenToWorldPoint(screenPosition) / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("DropDraged");
        myRectTransform.anchoredPosition = Vector2.zero;
        towerBlueprint.SetActive(false);
    }

}
