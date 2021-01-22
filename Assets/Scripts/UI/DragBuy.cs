using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class DragBuy : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Vector3 positionToSpawn;
    private RectTransform myRectTransform;
    private UIMenager uIMenager;
    private int myCost;

    [SerializeField]
    private TowerTypes towerName;
    [SerializeField]
    private GameObject towerPrefab;
    [SerializeField]
    private GameObject towerBlueprint;
    [SerializeField]
    private GameObject towerAnchor;
    [SerializeField]
    private TMP_Text towerCostText;

    private void Start()
    {
        uIMenager = GameBox.instance.uIMenager;
        myRectTransform = GetComponent<RectTransform>();
        myCost = uIMenager.GiveTowerCost(towerName);
        towerCostText.text = myCost.ToString();
        towerBlueprint.SetActive(false);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        towerBlueprint.SetActive(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        myRectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Input.mousePosition.x < 836 && Input.mousePosition.y > 100)
        {
            if (uIMenager.TryBuy(myCost))
            {
                positionToSpawn = transform.position;
                positionToSpawn.z = 0;
                Instantiate(towerPrefab, positionToSpawn, Quaternion.identity, towerAnchor.transform);

            }
            else
            {
                //NotEnoughMoney Popup
            }
        }
        myRectTransform.anchoredPosition = Vector2.zero;
        towerBlueprint.SetActive(false);
    }

}
