using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImputMenager : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private LayerMask clickableLayerMask;
    private IClickable clicableObject;

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            HandleLeftMouseButtonClicked();
        }
    }

    private void HandleLeftMouseButtonClicked()
    {
        Debug.Log("Coś kliknięto");
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, distance: 100, layerMask: clickableLayerMask);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.name); //tu konntynuuj
        }
    }
}
