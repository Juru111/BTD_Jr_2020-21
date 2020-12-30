using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerClickable : MonoBehaviour, IClickable
{
    public void OnClicked()
    {
        Debug.Log("Clicked!");
    }
}
