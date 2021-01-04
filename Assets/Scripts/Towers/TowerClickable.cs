using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerClickable : MonoBehaviour, IClickable
{
    [SerializeField]
    private GameObject rangeIndicator;
    [SerializeField]
    private Tower towerMechanics;

    private void Start()
    {
        rangeIndicator.SetActive(false);
    }

    public void OnSelected()
    {
        rangeIndicator.SetActive(true);
        GameBox.instance.uIMenager.SelectedOnUI(towerMechanics);
    }

    public void OnDeselect()
    {
        rangeIndicator.SetActive(false);
        GameBox.instance.uIMenager.DeselectedOnUI();
    }
}
