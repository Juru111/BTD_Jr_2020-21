using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataBase", menuName = "MojeScriptables/DataBase")]
public class DataBase : ScriptableObject
{
    //koszty oraz ilony uleprzeń małp
    [System.Serializable]
    public class UpgradesData
    {
        //public Sprite upgradeIcon[]
        [field: SerializeField] public Sprite Path1LvL1 { get; private set; }
        [field: SerializeField] public Sprite Path1LvL2 { get; private set; }
        [field: SerializeField] public Sprite Path1LvL3 { get; private set; }
        [field: SerializeField] public Sprite Path1LvL4 { get; private set; }
        [field: SerializeField] public int Path1LvL1Cost { get; private set; }
        [field: SerializeField] public int Path1LvL2Cost { get; private set; }
        [field: SerializeField] public int Path1LvL3Cost { get; private set; }
        [field: SerializeField] public int Path1LvL4Cost { get; private set; }


        [field: SerializeField] public Sprite Path2LvL1 { get; private set; }
        [field: SerializeField] public Sprite Path2LvL2 { get; private set; }
        [field: SerializeField] public Sprite Path2LvL3 { get; private set; }
        [field: SerializeField] public Sprite Path2LvL4 { get; private set; }
        [field: SerializeField] public int Path2LvL1Cost { get; private set; }
        [field: SerializeField] public int Path2LvL2Cost { get; private set; }
        [field: SerializeField] public int Path2LvL3Cost { get; private set; }
        [field: SerializeField] public int Path2LvL4Cost { get; private set; }
    }
    public UpgradesData dartMonkey;
    public UpgradesData tackShooter;
    public UpgradesData bombShooter;
    public UpgradesData iceTower;
    public UpgradesData glueMonkey;
    public UpgradesData monkeyBuccaneer;

    //koszty małp
    [System.Serializable]
    public class TowerBuyCost
    {
        [field: SerializeField] public int dartMonkey { get; private set; }
        [field: SerializeField] public int tackShooter { get; private set; }
        [field: SerializeField] public int bombShooter { get; private set; }
        [field: SerializeField] public int iceTower { get; private set; }
        [field: SerializeField] public int glueMonkey { get; private set; }
        [field: SerializeField] public int monkeyBuccaneer { get; private set; }
    }
    public TowerBuyCost towerBuyCost;

    //rundy
    [System.Serializable]
    public class RoundPiece
    {
        public BloonTypes bloonName;
        public bool isCammo;
        public int count;
        public float bloonSpaceing;
        public float pieceSpaceing;
    }
    [System.Serializable]
    public class Round
    {
        public List<RoundPiece> piece;
        public string rbeInfo;
    }
    [SerializeField]
    public List<Round> round;

}