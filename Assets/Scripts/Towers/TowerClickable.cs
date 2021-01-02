using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerClickable : MonoBehaviour, IClickable
{
    [SerializeField]
    private GameObject rangeIndicator;
    [SerializeField]
    private GameObject towerMechanics;

    private void Start()
    {
        rangeIndicator.SetActive(false);
    }

    public void OnSelected()
    {
        rangeIndicator.SetActive(true);
    }

    public void OnDeselect()
    {
        rangeIndicator.SetActive(false);
    }
}
