﻿using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIMenager : MonoBehaviour
{
    //HP i życia
    [SerializeField]
    private TMP_Text moneyDisplay;
    [SerializeField]
    private int moneyCount;
    [SerializeField]
    private TMP_Text hpDisplay;
    [SerializeField]
    private int hpCount;
    private bool isLostWindowShowed = false;

    //Start button
    [Space]
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private TMP_Text startButtonText;

    //Rounds info
    [Space]
    [SerializeField]
    private int roundReward;
    [SerializeField]
    private TMP_Text roundCount;
    [SerializeField]
    private TMP_Text roundRbeInfo;
    [SerializeField]
    private TMP_Text maxRoundCount;
    private int maxRound;
    private int lastFinishedRound = 0;

    //uleprzenia
    [Space]
    [SerializeField]
    private GameObject upgradePanel;
    [SerializeField]
    private TMP_Text towerNameText;
    private Tower selectedTower;

    [Space]
    [SerializeField]
    private TMP_Text path1Info;
    [SerializeField]
    private Image path1Active;
    [SerializeField]
    private TMP_Text path1BuyInfo;
    [SerializeField]
    private Image path1ToBuy;
    private Button path1Button;
    
    [Space]
    [SerializeField]
    private TMP_Text path2Info;
    [SerializeField]
    private Image path2Active;
    [SerializeField]
    private TMP_Text path2BuyInfo;
    [SerializeField]
    private Image path2ToBuy;
    private Button path2Button;


    //[System.Serializable]
    //public class Path1
    //{
    //[SerializeField]
    //private TMP_Text Info;
    //private Image Active; private TMP_Text BuyInfo;
    //private Image ToBuy;
    //}

    [Space]
    private DataBase dataBase;
    [SerializeField]
    private MessageWindow messageWindow;
    StringBuilder sb = new StringBuilder();


    //TODO: Nauczyć się segregować kod i go tu posegregować O.o


    void Awake()
    {
        upgradePanel.SetActive(false);
        RefreshMoneyDispaly();
        RefreshHpDispaly();

        path1Button = path1BuyInfo.transform.parent.gameObject.GetComponent<Button>();
        path2Button = path2BuyInfo.transform.parent.gameObject.GetComponent<Button>();

        dataBase = GameBox.instance.dataBase;
    }

    private void Start()
    {
        maxRound = GameBox.instance.dataBase.rounds.Count - 1;

        roundCount.text = "1";
        maxRoundCount.text = maxRound.ToString();
        roundRbeInfo.text = GameBox.instance.dataBase.rounds[1].rbeInfo;
    }

    public void LoseHp(int hpLost)
    {
        hpCount -= hpLost;
        if (hpCount <= 0)
        {
            hpCount = 0;
            if(isLostWindowShowed == false)
            {
                isLostWindowShowed = true;
                DoLose(lastFinishedRound);
            }
        }
        RefreshHpDispaly();

    }
    
    public bool TryBuy(int price)
    {
        if(moneyCount<price)
        {
            return false;
        }
        else
        {
            ChangeMoneyBalance(-price);
            return true;
        }
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

    public void SelectedOnUI(Tower _selectedTower)
    {
        selectedTower = _selectedTower;
        upgradePanel.SetActive(true);
        RefreshUpgradePanel(_selectedTower);
    }

    public void DeselectedOnUI()
    {
        upgradePanel.SetActive(false);
        selectedTower = null;
    }

    private void RefreshUpgradePanel(Tower _tower)
    {
        sb.AppendFormat("{0} upgrades", GiveTowerName(_tower.towerName));
        towerNameText.text = sb.ToString();
        sb.Clear();

        #region UpgradesPanel

        //Current Upgrade on Path 1
        sb.AppendFormat("Path 1\nLvl {0}", _tower.path1Lv);
        path1Info.text = sb.ToString();
        sb.Clear();
        if (GiveUpgradeIcon(_tower.towerName, 1, _tower.path1Lv) != null)
        {
            path1Active.enabled = true;
            path1Active.sprite = GiveUpgradeIcon(_tower.towerName, 1, _tower.path1Lv);
        }
        else
        {
            path1Active.enabled = false;
        }

        //Next upgrade on Path 1
        if (_tower.path2Lv > 2 && _tower.path1Lv > 1)
        {
            path1Button.interactable = false;

            sb.AppendFormat("Path\nClosed");
            path1BuyInfo.text = sb.ToString();
            sb.Clear();
        }
        else if (GiveUpgradeCost(_tower.towerName, 1, _tower.path1Lv + 1) == -1)
        {
            path1Button.interactable = false;
            sb.AppendFormat("Maxed");
            path1BuyInfo.text = sb.ToString();
            sb.Clear();
        }
        else
        {
            path1Button.interactable = true;
            sb.AppendFormat("Buy\n\n{0}", GiveUpgradeCost(_tower.towerName, 1, _tower.path1Lv + 1));
            path1BuyInfo.text = sb.ToString();
            sb.Clear();
        }

        if ((_tower.path2Lv > 2 && _tower.path1Lv > 1) || GiveUpgradeIcon(_tower.towerName, 1, _tower.path1Lv + 1) == null)
        {
            path1ToBuy.enabled = false;
        }
        else
        {
            path1ToBuy.enabled = true;
            path1ToBuy.sprite = GiveUpgradeIcon(_tower.towerName, 1, _tower.path1Lv + 1);
        }

        //Current Upgrade on Path 2
        sb.AppendFormat("Path 2\nLvl {0}", _tower.path2Lv);
        path2Info.text = sb.ToString();
        sb.Clear();
        if (GiveUpgradeIcon(_tower.towerName, 2, _tower.path2Lv) != null)
        {
            path2Active.enabled = true;
            path2Active.sprite = GiveUpgradeIcon(_tower.towerName, 2, _tower.path2Lv);
        }
        else
        {
            path2Active.enabled = false;
        }

        //Next upgrade on Path 2
        if (_tower.path1Lv > 2 && _tower.path2Lv > 1)
        {
            path2Button.interactable = false;
            sb.AppendFormat("Path\nClosed");
            path2BuyInfo.text = sb.ToString();
            sb.Clear();
        }
        else if(GiveUpgradeCost(_tower.towerName, 2, _tower.path2Lv + 1) == -1)
        {
            path2Button.interactable = false;
            sb.AppendFormat("Maxed");
            path2BuyInfo.text = sb.ToString();
            sb.Clear();
        }
        else
        {
            path2Button.interactable = true;
            sb.AppendFormat("Buy\n\n{0}", GiveUpgradeCost(_tower.towerName, 2, _tower.path2Lv + 1));
            path2BuyInfo.text = sb.ToString();
            sb.Clear();
        }
        
        //wstawanie odpowiednich ikon
        if ((_tower.path1Lv > 2 && _tower.path2Lv > 1) || GiveUpgradeIcon(_tower.towerName, 2, _tower.path2Lv + 1) == null)
        {
            path2ToBuy.enabled = false;
        }
        else
        {
            path2ToBuy.enabled = true;
            path2ToBuy.sprite = GiveUpgradeIcon(_tower.towerName, 2, _tower.path2Lv + 1);
        }


        //następujący kod jest ukrywaniem upeprzenia 4 poziomu na 2 path-u - czasowe (bądź nie) rozwiązanie
        if(_tower.path2Lv == 3)
        {
            path2Button.interactable = false;
            sb.AppendFormat("Maxed");
            path2BuyInfo.text = sb.ToString();
            sb.Clear();

            path2ToBuy.enabled = false;
        }
        #endregion
    }

    public void StartNextRoundOnUI()
    {
        startButton.interactable = false;
        startButtonText.text = "Round in\nprocess...";
    }

    public void FinishRoundOnUI(int nextRoundIndex, string nextRoundRbeInfo)
    {
        if (hpCount > 0)
        {
            RoundReward();

            startButton.interactable = true;
            startButtonText.text = "Start\nRound";
            lastFinishedRound = nextRoundIndex - 1;
            roundCount.text = nextRoundIndex.ToString();
            roundRbeInfo.text = nextRoundRbeInfo;
        }
    }

    public void DoWin()
    {
        RoundReward();
        startButtonText.text = "Goal\nAchieved";
        messageWindow.OpenWindow(WindowTypes.Win, 0);
    }

    private void DoLose(int lastFinishedRound)
    {
        startButton.interactable = false;
        sb.AppendFormat("Game ended");
        startButtonText.text = sb.ToString();
        sb.Clear();
        messageWindow.OpenWindow(WindowTypes.Lose, lastFinishedRound);
    }

    private void RoundReward()
    {
        ChangeMoneyBalance(roundReward);
        roundReward++;
    }

    public void HandleLeaveButtonPress()
    {
        messageWindow.OpenWindow(WindowTypes.Leave, 0);
    }

    public void HandleSettingsButtonPress()
    {
        messageWindow.OpenWindow(WindowTypes.Settings, 0);
    }

    private Sprite GiveUpgradeIcon(TowerTypes towerType, int path, int Lv)
    {
        switch (towerType)
        {
            case TowerTypes.DartMonkey:
                if (path == 1)
                {
                    switch (Lv)
                    {
                        case 1: return dataBase.dartMonkey.Path1LvL1;
                        case 2: return dataBase.dartMonkey.Path1LvL2;
                        case 3: return dataBase.dartMonkey.Path1LvL3;
                        case 4: return dataBase.dartMonkey.Path1LvL4;
                        default: return null;
                    }
                }
                if (path == 2)
                {
                    switch (Lv)
                    {
                        case 1: return dataBase.dartMonkey.Path2LvL1;
                        case 2: return dataBase.dartMonkey.Path2LvL2;
                        case 3: return dataBase.dartMonkey.Path2LvL3;
                        case 4: return dataBase.dartMonkey.Path2LvL4;
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
                        case 1: return dataBase.tackShooter.Path1LvL1;
                        case 2: return dataBase.tackShooter.Path1LvL2;
                        case 3: return dataBase.tackShooter.Path1LvL3;
                        case 4: return dataBase.tackShooter.Path1LvL4;
                        default: return null;
                    }
                }
                if (path == 2)
                {
                    switch (Lv)
                    {
                        case 1: return dataBase.tackShooter.Path2LvL1;
                        case 2: return dataBase.tackShooter.Path2LvL2;
                        case 3: return dataBase.tackShooter.Path2LvL3;
                        case 4: return dataBase.tackShooter.Path2LvL4;
                        default: return null;
                    }
                }
                else
                {
                    return null;
                }
            case TowerTypes.BombShooter:
                if (path == 1)
                {
                    switch (Lv)
                    {
                        case 1: return dataBase.bombShooter.Path1LvL1;
                        case 2: return dataBase.bombShooter.Path1LvL2;
                        case 3: return dataBase.bombShooter.Path1LvL3;
                        case 4: return dataBase.bombShooter.Path1LvL4;
                        default: return null;
                    }
                }
                if (path == 2)
                {
                    switch (Lv)
                    {
                        case 1: return dataBase.bombShooter.Path2LvL1;
                        case 2: return dataBase.bombShooter.Path2LvL2;
                        case 3: return dataBase.bombShooter.Path2LvL3;
                        case 4: return dataBase.bombShooter.Path2LvL4;
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
                        case 1: return dataBase.iceTower.Path1LvL1;
                        case 2: return dataBase.iceTower.Path1LvL2;
                        case 3: return dataBase.iceTower.Path1LvL3;
                        case 4: return dataBase.iceTower.Path1LvL4;
                        default: return null;
                    }
                }
                if (path == 2)
                {
                    switch (Lv)
                    {
                        case 1: return dataBase.iceTower.Path2LvL1;
                        case 2: return dataBase.iceTower.Path2LvL2;
                        case 3: return dataBase.iceTower.Path2LvL3;
                        case 4: return dataBase.iceTower.Path2LvL4;
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
                        case 1: return dataBase.glueMonkey.Path1LvL1;
                        case 2: return dataBase.glueMonkey.Path1LvL2;
                        case 3: return dataBase.glueMonkey.Path1LvL3;
                        case 4: return dataBase.glueMonkey.Path1LvL4;
                        default: return null;
                    }
                }
                if (path == 2)
                {
                    switch (Lv)
                    {
                        case 1: return dataBase.glueMonkey.Path2LvL1;
                        case 2: return dataBase.glueMonkey.Path2LvL2;
                        case 3: return dataBase.glueMonkey.Path2LvL3;
                        case 4: return dataBase.glueMonkey.Path2LvL4;
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
                        case 1: return dataBase.monkeyBuccaneer.Path1LvL1;
                        case 2: return dataBase.monkeyBuccaneer.Path1LvL2;
                        case 3: return dataBase.monkeyBuccaneer.Path1LvL3;
                        case 4: return dataBase.monkeyBuccaneer.Path1LvL4;
                        default: return null;
                    }
                }
                if (path == 2)
                {
                    switch (Lv)
                    {
                        case 1: return dataBase.monkeyBuccaneer.Path2LvL1;
                        case 2: return dataBase.monkeyBuccaneer.Path2LvL2;
                        case 3: return dataBase.monkeyBuccaneer.Path2LvL3;
                        case 4: return dataBase.monkeyBuccaneer.Path2LvL4;
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

    private int GiveUpgradeCost(TowerTypes towerType, int path, int Lv)
    {
        switch (towerType)
        {
            case TowerTypes.DartMonkey:
                if (path == 1)
                {
                    switch (Lv)
                    {
                        case 1: return dataBase.dartMonkey.Path1LvL1Cost;
                        case 2: return dataBase.dartMonkey.Path1LvL2Cost;
                        case 3: return dataBase.dartMonkey.Path1LvL3Cost;
                        case 4: return dataBase.dartMonkey.Path1LvL4Cost;
                        default: return -1;
                    }
                }
                if (path == 2)
                {
                    switch (Lv)
                    {
                        case 1: return dataBase.dartMonkey.Path2LvL1Cost;
                        case 2: return dataBase.dartMonkey.Path2LvL2Cost;
                        case 3: return dataBase.dartMonkey.Path2LvL3Cost;
                        case 4: return dataBase.dartMonkey.Path2LvL4Cost;
                        default: return -1;
                    }
                }
                else
                {
                    return -2;
                }
            case TowerTypes.TackShooter:
                if (path == 1)
                {
                    switch (Lv)
                    {
                        case 1: return dataBase.tackShooter.Path1LvL1Cost;
                        case 2: return dataBase.tackShooter.Path1LvL2Cost;
                        case 3: return dataBase.tackShooter.Path1LvL3Cost;
                        case 4: return dataBase.tackShooter.Path1LvL4Cost;
                        default: return -1;
                    }
                }
                if (path == 2)
                {
                    switch (Lv)
                    {
                        case 1: return dataBase.tackShooter.Path2LvL1Cost;
                        case 2: return dataBase.tackShooter.Path2LvL2Cost;
                        case 3: return dataBase.tackShooter.Path2LvL3Cost;
                        case 4: return dataBase.tackShooter.Path2LvL4Cost;
                        default: return -1;
                    }
                }
                else
                {
                    return -2;
                }
            case TowerTypes.BombShooter:
                if (path == 1)
                {
                    switch (Lv)
                    {
                        case 1: return dataBase.bombShooter.Path1LvL1Cost;
                        case 2: return dataBase.bombShooter.Path1LvL2Cost;
                        case 3: return dataBase.bombShooter.Path1LvL3Cost;
                        case 4: return dataBase.bombShooter.Path1LvL4Cost;
                        default: return -1;
                    }
                }
                if (path == 2)
                {
                    switch (Lv)
                    {
                        case 1: return dataBase.bombShooter.Path2LvL1Cost;
                        case 2: return dataBase.bombShooter.Path2LvL2Cost;
                        case 3: return dataBase.bombShooter.Path2LvL3Cost;
                        case 4: return dataBase.bombShooter.Path2LvL4Cost;
                        default: return -1;
                    }
                }
                else
                {
                    return -2;
                }
            case TowerTypes.IceTower:
                if (path == 1)
                {
                    switch (Lv)
                    {
                        case 1: return dataBase.iceTower.Path1LvL1Cost;
                        case 2: return dataBase.iceTower.Path1LvL2Cost;
                        case 3: return dataBase.iceTower.Path1LvL3Cost;
                        case 4: return dataBase.iceTower.Path1LvL4Cost;
                        default: return -1;
                    }
                }
                if (path == 2)
                {
                    switch (Lv)
                    {
                        case 1: return dataBase.iceTower.Path2LvL1Cost;
                        case 2: return dataBase.iceTower.Path2LvL2Cost;
                        case 3: return dataBase.iceTower.Path2LvL3Cost;
                        case 4: return dataBase.iceTower.Path2LvL4Cost;
                        default: return -1;
                    }
                }
                else
                {
                    return -2;
                }
            case TowerTypes.GlueMonkey:
                if (path == 1)
                {
                    switch (Lv)
                    {
                        case 1: return dataBase.glueMonkey.Path1LvL1Cost;
                        case 2: return dataBase.glueMonkey.Path1LvL2Cost;
                        case 3: return dataBase.glueMonkey.Path1LvL3Cost;
                        case 4: return dataBase.glueMonkey.Path1LvL4Cost;
                        default: return -1;
                    }
                }
                if (path == 2)
                {
                    switch (Lv)
                    {
                        case 1: return dataBase.glueMonkey.Path2LvL1Cost;
                        case 2: return dataBase.glueMonkey.Path2LvL2Cost;
                        case 3: return dataBase.glueMonkey.Path2LvL3Cost;
                        case 4: return dataBase.glueMonkey.Path2LvL4Cost;
                        default: return -1;
                    }
                }
                else
                {
                    return -2;
                }
            case TowerTypes.MonkeyBuccaneer:
                if (path == 1)
                {
                    switch (Lv)
                    {
                        case 1: return dataBase.monkeyBuccaneer.Path1LvL1Cost;
                        case 2: return dataBase.monkeyBuccaneer.Path1LvL2Cost;
                        case 3: return dataBase.monkeyBuccaneer.Path1LvL3Cost;
                        case 4: return dataBase.monkeyBuccaneer.Path1LvL4Cost;
                        default: return -1;
                    }
                }
                if (path == 2)
                {
                    switch (Lv)
                    {
                        case 1: return dataBase.monkeyBuccaneer.Path2LvL1Cost;
                        case 2: return dataBase.monkeyBuccaneer.Path2LvL2Cost;
                        case 3: return dataBase.monkeyBuccaneer.Path2LvL3Cost;
                        case 4: return dataBase.monkeyBuccaneer.Path2LvL4Cost;
                        default: return -1;
                    }
                }
                else
                {
                    return -2;
                }
            default:
                return -3;
        }
    }

    public int GiveTowerCost(TowerTypes towerType)
    {
        switch (towerType)
        {
            case TowerTypes.DartMonkey:
                return dataBase.towerBuyCost.dartMonkey;
            case TowerTypes.TackShooter:
                return dataBase.towerBuyCost.tackShooter;
            case TowerTypes.BombShooter:
                return dataBase.towerBuyCost.bombShooter;
            case TowerTypes.IceTower:
                return dataBase.towerBuyCost.iceTower;
            case TowerTypes.GlueMonkey:
                return dataBase.towerBuyCost.glueMonkey;
            case TowerTypes.MonkeyBuccaneer:
                return dataBase.towerBuyCost.monkeyBuccaneer;
            default:
                return -1;
        }
    }

    private string GiveTowerName(TowerTypes towerType)
    {
        switch(towerType)
        {
            case TowerTypes.DartMonkey: return "Dart Monkey";
            case TowerTypes.TackShooter: return "Tack Shooter";
            case TowerTypes.BombShooter: return "Bomb Shooter";
            case TowerTypes.IceTower: return "Ice Tower";
            case TowerTypes.GlueMonkey: return "Glue Monkey";
            case TowerTypes.MonkeyBuccaneer: return "Monkey Buccaneer";
            default: return null;
        }
    }

    public void UpgradeSelectedPath1()
    {
        if(moneyCount >= GiveUpgradeCost(selectedTower.towerName, 1, selectedTower.path1Lv+1))
        {
            ChangeMoneyBalance(-GiveUpgradeCost(selectedTower.towerName, 1, selectedTower.path1Lv+1));
            selectedTower.UpgradeMe(1);
            RefreshUpgradePanel(selectedTower);
        }
        else
        {
            //no
        }
        
    }
    public void UpgradeSelectedPath2()
    {
        if (moneyCount >= GiveUpgradeCost(selectedTower.towerName, 2, selectedTower.path2Lv+1))
        {
            ChangeMoneyBalance(-GiveUpgradeCost(selectedTower.towerName, 2, selectedTower.path2Lv+1));
            selectedTower.UpgradeMe(2);
            RefreshUpgradePanel(selectedTower);
        }
        else
        {
            //no
        }
    }
}
