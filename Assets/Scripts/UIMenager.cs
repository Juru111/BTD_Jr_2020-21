using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
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

    [Space]
    [SerializeField]
    private GameObject upgradePanel;
    [SerializeField]
    private TMP_Text towerNameText;

    [SerializeField]
    private TMP_Text path1Info;
    [SerializeField]
    private Image path1Active;
    [SerializeField]
    private TMP_Text path1BuyInfo;
    [SerializeField]
    private Image path1ToBuy;
    [SerializeField]
    private TMP_Text path2Info;
    [SerializeField]
    private Image path2Active;
    [SerializeField]
    private TMP_Text path2BuyInfo;
    [SerializeField]
    private Image path2ToBuy;

    private Tower selectedTower;


    void Awake()
    {
        upgradePanel.SetActive(false);
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

    public void ChangeMoneyBalance(int moneyChange)
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

    public void SelectedOnUI(Tower selected)
    {
        
        selectedTower = selected;
        StringBuilder sb = new StringBuilder();
        upgradePanel.SetActive(true);

        sb.AppendFormat("{0} upgrades", GiveTowerName(selectedTower.towerName));
        towerNameText.text = sb.ToString();
        sb.Clear();

        #region Uleprzenia
        //Current Upgrade on Path 1
        sb.AppendFormat("Lvl {0}\nPath 1", selected.path1Lv);
        path1Info.text = sb.ToString();
        sb.Clear();
        if(GiveLvIcon(selectedTower.towerName, 1, selectedTower.path1Lv) != null)
        {
            path1Active.enabled = true;
            path1Active.sprite = GiveLvIcon(selectedTower.towerName, 1, selectedTower.path1Lv);
        }
        else
        {
            path1Active.enabled = false;
        }
        
        //Next upgrade on Path 1
        sb.AppendFormat("Buy\n\n{0}", "WIP");
        path1BuyInfo.text = sb.ToString();
        sb.Clear();
        if (GiveLvIcon(selectedTower.towerName, 1, selectedTower.path1Lv + 1) != null)
        {
            path1ToBuy.enabled = true;
            path1ToBuy.sprite = GiveLvIcon(selectedTower.towerName, 1, selectedTower.path1Lv + 1);
        }
        else
        {
            path1ToBuy.enabled = false;
        }

        //Current Upgrade on Path 2
        sb.AppendFormat("Lvl {0}\nPath 2", selected.path2Lv);
        path2Info.text = sb.ToString();
        sb.Clear();
        if (GiveLvIcon(selectedTower.towerName, 2, selectedTower.path2Lv) != null)
        {
            path2Active.enabled = true;
            path2Active.sprite = GiveLvIcon(selectedTower.towerName, 2, selectedTower.path2Lv);
        }
        else
        {
            path2Active.enabled = false;
        }

        //Next upgrade on Path 2
        sb.AppendFormat("Buy\n\n{0}", "WIP");
        path2BuyInfo.text = sb.ToString();
        sb.Clear();
        if (GiveLvIcon(selectedTower.towerName, 2, selectedTower.path2Lv + 1))
        {
            path2ToBuy.enabled = true;
            path2ToBuy.sprite = GiveLvIcon(selectedTower.towerName, 2, selectedTower.path2Lv + 1);
        }
        else
        {
            path2ToBuy.enabled = false;
        }
        #endregion
    }

    private Sprite GiveLvIcon(TowerTypes towerType, int path, int Lv)
    {
        switch (towerType)
        {
            case TowerTypes.DartMonkey:
                if (path == 1)
                {
                    switch (Lv)
                    {
                        case 1: return GameBox.instance.dataBase.dartmonkey.Path1LvL1;
                        case 2: return GameBox.instance.dataBase.dartmonkey.Path1LvL2;
                        case 3: return GameBox.instance.dataBase.dartmonkey.Path1LvL3;
                        case 4: return GameBox.instance.dataBase.dartmonkey.Path1LvL4;
                        default: return null;
                    }
                }
                if (path == 2)
                {
                    switch (Lv)
                    {
                        case 1: return GameBox.instance.dataBase.dartmonkey.Path2LvL1;
                        case 2: return GameBox.instance.dataBase.dartmonkey.Path2LvL2;
                        case 3: return GameBox.instance.dataBase.dartmonkey.Path2LvL3;
                        case 4: return GameBox.instance.dataBase.dartmonkey.Path2LvL4;
                        default: return null;
                    }
                }
                else
                {
                    return null;
                }
            case TowerTypes.TackShooter:
                if (path == 1)
                {
                    switch (Lv)
                    {
                        case 1: return GameBox.instance.dataBase.tackShooter.Path1LvL1;
                        case 2: return GameBox.instance.dataBase.tackShooter.Path1LvL2;
                        case 3: return GameBox.instance.dataBase.tackShooter.Path1LvL3;
                        case 4: return GameBox.instance.dataBase.tackShooter.Path1LvL4;
                        default: return null;
                    }
                }
                if (path == 2)
                {
                    switch (Lv)
                    {
                        case 1: return GameBox.instance.dataBase.tackShooter.Path2LvL1;
                        case 2: return GameBox.instance.dataBase.tackShooter.Path2LvL2;
                        case 3: return GameBox.instance.dataBase.tackShooter.Path2LvL3;
                        case 4: return GameBox.instance.dataBase.tackShooter.Path2LvL4;
                        default: return null;
                    }
                }
                else
                {
                    return null;
                }
            case TowerTypes.BombSooter:
                if (path == 1)
                {
                    switch (Lv)
                    {
                        case 1: return GameBox.instance.dataBase.bombShooter.Path1LvL1;
                        case 2: return GameBox.instance.dataBase.bombShooter.Path1LvL2;
                        case 3: return GameBox.instance.dataBase.bombShooter.Path1LvL3;
                        case 4: return GameBox.instance.dataBase.bombShooter.Path1LvL4;
                        default: return null;
                    }
                }
                if (path == 2)
                {
                    switch (Lv)
                    {
                        case 1: return GameBox.instance.dataBase.bombShooter.Path2LvL1;
                        case 2: return GameBox.instance.dataBase.bombShooter.Path2LvL2;
                        case 3: return GameBox.instance.dataBase.bombShooter.Path2LvL3;
                        case 4: return GameBox.instance.dataBase.bombShooter.Path2LvL4;
                        default: return null;
                    }
                }
                else
                {
                    return null;
                }
            case TowerTypes.IceTower:
                if (path == 1)
                {
                    switch (Lv)
                    {
                        case 1: return GameBox.instance.dataBase.iceTower.Path1LvL1;
                        case 2: return GameBox.instance.dataBase.iceTower.Path1LvL2;
                        case 3: return GameBox.instance.dataBase.iceTower.Path1LvL3;
                        case 4: return GameBox.instance.dataBase.iceTower.Path1LvL4;
                        default: return null;
                    }
                }
                if (path == 2)
                {
                    switch (Lv)
                    {
                        case 1: return GameBox.instance.dataBase.iceTower.Path2LvL1;
                        case 2: return GameBox.instance.dataBase.iceTower.Path2LvL2;
                        case 3: return GameBox.instance.dataBase.iceTower.Path2LvL3;
                        case 4: return GameBox.instance.dataBase.iceTower.Path2LvL4;
                        default: return null;
                    }
                }
                else
                {
                    return null;
                }
            case TowerTypes.GlueMonkey:
                if (path == 1)
                {
                    switch (Lv)
                    {
                        case 1: return GameBox.instance.dataBase.glueMonkey.Path1LvL1;
                        case 2: return GameBox.instance.dataBase.glueMonkey.Path1LvL2;
                        case 3: return GameBox.instance.dataBase.glueMonkey.Path1LvL3;
                        case 4: return GameBox.instance.dataBase.glueMonkey.Path1LvL4;
                        default: return null;
                    }
                }
                if (path == 2)
                {
                    switch (Lv)
                    {
                        case 1: return GameBox.instance.dataBase.glueMonkey.Path2LvL1;
                        case 2: return GameBox.instance.dataBase.glueMonkey.Path2LvL2;
                        case 3: return GameBox.instance.dataBase.glueMonkey.Path2LvL3;
                        case 4: return GameBox.instance.dataBase.glueMonkey.Path2LvL4;
                        default: return null;
                    }
                }
                else
                {
                    return null;
                }
            case TowerTypes.MonkeyBuccaneer:
                if (path == 1)
                {
                    switch (Lv)
                    {
                        case 1: return GameBox.instance.dataBase.monkeyBuccaneer.Path1LvL1;
                        case 2: return GameBox.instance.dataBase.monkeyBuccaneer.Path1LvL2;
                        case 3: return GameBox.instance.dataBase.monkeyBuccaneer.Path1LvL3;
                        case 4: return GameBox.instance.dataBase.monkeyBuccaneer.Path1LvL4;
                        default: return null;
                    }
                }
                if (path == 2)
                {
                    switch (Lv)
                    {
                        case 1: return GameBox.instance.dataBase.monkeyBuccaneer.Path2LvL1;
                        case 2: return GameBox.instance.dataBase.monkeyBuccaneer.Path2LvL2;
                        case 3: return GameBox.instance.dataBase.monkeyBuccaneer.Path2LvL3;
                        case 4: return GameBox.instance.dataBase.monkeyBuccaneer.Path2LvL4;
                        default: return null;
                    }
                }
                else
                {
                    return null;
                }
            default:
                return null;
        }
    }

    private string GiveTowerName(TowerTypes towerType)
    {
        switch(towerType)
        {
            case TowerTypes.DartMonkey: return "Dart Monkey";
            case TowerTypes.TackShooter: return "Tack Shooter";
            case TowerTypes.BombSooter: return "Bomb Sooter";
            case TowerTypes.IceTower: return "Ice Tower";
            case TowerTypes.GlueMonkey: return "Glue Monkey";
            case TowerTypes.MonkeyBuccaneer: return "Monkey Buccaneer";
            default: return null;
        }
    }

    public void DeselectedOnUI()
    {
        upgradePanel.SetActive(false);
        selectedTower = null;
    }

    public void UpgradeSelectedPath1()
    {

    }
    public void UpgradeSelectedPath2()
    {

    }
}
