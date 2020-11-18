using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataBase", menuName = "MojeScriptables/DataBase")]
public class DataBase : ScriptableObject
{
    #region Dane Balonów
    [field: SerializeField] public float redBloonSpeed { get; private set; } = 1;
    [field: SerializeField] public Sprite redBloonSprite { get; private set; }
    [field: SerializeField] public float redBloonSize { get; private set; } = 0.19f; 

    [field: SerializeField] public float blueBloonSpeed { get; private set; } = 1.4f;
    [field: SerializeField] public Sprite blueBloonSprite { get; private set; }
    [field: SerializeField] public float blueBloonSize { get; private set; } = 0.2f;

    [field: SerializeField] public float greenBloonSpeed { get; private set; } = 1.8f;
    [field: SerializeField] public Sprite greenBloonSprite { get; private set; }
    [field: SerializeField] public float greenBloonSize { get; private set; } = 0.22f;

    [field: SerializeField] public float yellowBloonSpeed { get; private set; } = 3.2f;
    [field: SerializeField] public Sprite yellowBloonSprite { get; private set; }
    [field: SerializeField] public float yellowBloonSize { get; private set; } = 0.24f;

    [field: SerializeField] public float pinkBloonSpeed { get; private set; } = 3.5f;
    [field: SerializeField]  public Sprite pinkBloonSprite { get; private set; }
    [field: SerializeField] public float pinkBloonSize { get; private set; } = 0.28f;

    [field: SerializeField] public float blackBloonSpeed { get; private set; } = 1.8f;
    [field: SerializeField] public Sprite blackBloonSprite { get; private set; }

    [field: SerializeField] public float whiteBloonSpeed { get; private set; } = 2;
    [field: SerializeField] public Sprite whiteBloonSprite { get; private set; }

    [field: SerializeField] public float purpleBloonSpeed { get; private set; } = 3;
    [field: SerializeField] public Sprite purpleBloonSprite { get; private set; }

    [field: SerializeField] public float leadBloonSpeed { get; private set; } = 1;
    [field: SerializeField] public Sprite leadBloonSprite { get; private set; }

    [field: SerializeField] public float zebraBloonSpeed { get; private set; } = 1.8f;
    [field: SerializeField]  public Sprite zebraBloonSprite { get; private set; }

    [field: SerializeField] public float ranibowBloonSpeed { get; private set; } = 2.2f;
    [field: SerializeField] public Sprite rainbowBloonSprite { get; private set; }

    [field: SerializeField] public float ceramicBloonSpeed { get; private set; } = 2.5f;
    [field: SerializeField] public Sprite ceramicBloonSprite { get; private set; }

    [field: SerializeField] public float MOABSpeed { get; private set; } = 1;
    [field: SerializeField] public Sprite MOABSprite { get; private set; }
    #endregion

    //#region Spis waypointów
    //[field: SerializeField] public Transform[] waypoints { get; private set; } 
    //#endregion
}