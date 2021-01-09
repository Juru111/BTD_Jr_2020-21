using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataBase", menuName = "MojeScriptables/DataBase")]
public class DataBase : ScriptableObject
{
    #region Uleprzenia

    public UpgradesData dartMonkey;
    public UpgradesData tackShooter;
    public UpgradesData bombShooter;
    public UpgradesData iceTower;
    public UpgradesData glueMonkey;
    public UpgradesData monkeyBuccaneer;

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

    #endregion
}