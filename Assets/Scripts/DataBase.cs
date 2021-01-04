using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataBase", menuName = "MojeScriptables/DataBase")]
public class DataBase : ScriptableObject
{
    #region Sprite-y uleprzeń

    public DartMonkey dartmonkey;
    public TackShooter tackShooter;
    public BombShooter bombShooter;
    public IceTower iceTower;
    public GlueMonkey glueMonkey;
    public MonkeyBuccaneer monkeyBuccaneer;

    [System.Serializable]
    public class DartMonkey
    {
        [field: SerializeField] public Sprite Path1LvL1 { get; private set; }
        [field: SerializeField] public Sprite Path1LvL2 { get; private set; }
        [field: SerializeField] public Sprite Path1LvL3 { get; private set; }
        [field: SerializeField] public Sprite Path1LvL4 { get; private set; }

        [field: SerializeField] public Sprite Path2LvL1 { get; private set; }
        [field: SerializeField] public Sprite Path2LvL2 { get; private set; }
        [field: SerializeField] public Sprite Path2LvL3 { get; private set; }
        [field: SerializeField] public Sprite Path2LvL4 { get; private set; }
    }
    [System.Serializable]
    public class TackShooter
    {
        [field: SerializeField] public Sprite Path1LvL1 { get; private set; }
        [field: SerializeField] public Sprite Path1LvL2 { get; private set; }
        [field: SerializeField] public Sprite Path1LvL3 { get; private set; }
        [field: SerializeField] public Sprite Path1LvL4 { get; private set; }

        [field: SerializeField] public Sprite Path2LvL1 { get; private set; }
        [field: SerializeField] public Sprite Path2LvL2 { get; private set; }
        [field: SerializeField] public Sprite Path2LvL3 { get; private set; }
        [field: SerializeField] public Sprite Path2LvL4 { get; private set; }
    }
    [System.Serializable]
    public class BombShooter
    {
        [field: SerializeField] public Sprite Path1LvL1 { get; private set; }
        [field: SerializeField] public Sprite Path1LvL2 { get; private set; }
        [field: SerializeField] public Sprite Path1LvL3 { get; private set; }
        [field: SerializeField] public Sprite Path1LvL4 { get; private set; }

        [field: SerializeField] public Sprite Path2LvL1 { get; private set; }
        [field: SerializeField] public Sprite Path2LvL2 { get; private set; }
        [field: SerializeField] public Sprite Path2LvL3 { get; private set; }
        [field: SerializeField] public Sprite Path2LvL4 { get; private set; }
    }
    [System.Serializable]
    public class IceTower
    {
        [field: SerializeField] public Sprite Path1LvL1 { get; private set; }
        [field: SerializeField] public Sprite Path1LvL2 { get; private set; }
        [field: SerializeField] public Sprite Path1LvL3 { get; private set; }
        [field: SerializeField] public Sprite Path1LvL4 { get; private set; }

        [field: SerializeField] public Sprite Path2LvL1 { get; private set; }
        [field: SerializeField] public Sprite Path2LvL2 { get; private set; }
        [field: SerializeField] public Sprite Path2LvL3 { get; private set; }
        [field: SerializeField] public Sprite Path2LvL4 { get; private set; }
    }
    [System.Serializable]
    public class GlueMonkey
    {
        [field: SerializeField] public Sprite Path1LvL1 { get; private set; }
        [field: SerializeField] public Sprite Path1LvL2 { get; private set; }
        [field: SerializeField] public Sprite Path1LvL3 { get; private set; }
        [field: SerializeField] public Sprite Path1LvL4 { get; private set; }

        [field: SerializeField] public Sprite Path2LvL1 { get; private set; }
        [field: SerializeField] public Sprite Path2LvL2 { get; private set; }
        [field: SerializeField] public Sprite Path2LvL3 { get; private set; }
        [field: SerializeField] public Sprite Path2LvL4 { get; private set; }
    }
    [System.Serializable]
    public class MonkeyBuccaneer
    {
        [field: SerializeField] public Sprite Path1LvL1 { get; private set; }
        [field: SerializeField] public Sprite Path1LvL2 { get; private set; }
        [field: SerializeField] public Sprite Path1LvL3 { get; private set; }
        [field: SerializeField] public Sprite Path1LvL4 { get; private set; }

        [field: SerializeField] public Sprite Path2LvL1 { get; private set; }
        [field: SerializeField] public Sprite Path2LvL2 { get; private set; }
        [field: SerializeField] public Sprite Path2LvL3 { get; private set; }
        [field: SerializeField] public Sprite Path2LvL4 { get; private set; }
    }

    #endregion
}