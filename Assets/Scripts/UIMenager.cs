using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIMenager : MonoBehaviour
{

    [SerializeField]
    private TMP_Text moneyDisplay;
    [SerializeField]
    private int moneyCount;
    [SerializeField]
    private TMP_Text hpDisplay;
    [SerializeField]
    private int hpCount;

    void Awake()
    {
        RefreshMoneyDispaly();
        RefreshHpDispaly();
    }


    public void LoseHp(int hpLost)
    {
        hpCount -= hpLost;
        if (hpCount <= 0)
        {
            //lose Popup
            Debug.Log("END!");
            hpCount = 0;
        }
        RefreshHpDispaly();
    }

    public void changeMoneyBalance(int moneyChange)
    {
        moneyCount += moneyChange;

        //może robić to rzadziej gdy będzie się to działo często
        RefreshMoneyDispaly();
    }

    private void RefreshMoneyDispaly()
    {
        moneyDisplay.text = moneyCount.ToString();
    }

    private void RefreshHpDispaly()
    {
        hpDisplay.text = hpCount.ToString();
    }

}
