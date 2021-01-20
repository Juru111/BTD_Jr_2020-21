using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombFullRange : Bomb
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        //nie ma się nie dziać
    }

    protected override void HandleMaxRange()
    {
        BombAction();
    }
}
