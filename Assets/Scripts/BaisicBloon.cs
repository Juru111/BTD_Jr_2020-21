using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaisicBloon : Bloon
{
    public CircleCollider2D myCircleCollider2D;
    public SpriteRenderer mySpriteRenderer;

    public override void Start()
    {
        base.Start();
        myCircleCollider2D = GetComponent<CircleCollider2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        //layersLeft = 5;
        //Debug.Log(GameMenager.instance.bloonsData.yellowBloonSpeed);
    }

    public override void LayerPop(int power)
    {
        base.LayerPop(power);
        if(layersLeft == 4)
        {
            mySpriteRenderer.sprite = GameBox.instance.bloonsData.yellowBloonSprite;
            movementSpeed = GameBox.instance.bloonsData.yellowBloonSpeed;
            myCircleCollider2D.radius = GameBox.instance.bloonsData.yellowBloonSize;

        }
        if(layersLeft == 3)
        {
            mySpriteRenderer.sprite = GameBox.instance.bloonsData.greenBloonSprite;
            movementSpeed = GameBox.instance.bloonsData.greenBloonSpeed;
            myCircleCollider2D.radius = GameBox.instance.bloonsData.greenBloonSize;
        }
        if (layersLeft == 2)
        {
            mySpriteRenderer.sprite = GameBox.instance.bloonsData.blueBloonSprite;
            movementSpeed = GameBox.instance.bloonsData.blueBloonSpeed;
            myCircleCollider2D.radius = GameBox.instance.bloonsData.blueBloonSize;
        }
        if (layersLeft == 1)
        {
            mySpriteRenderer.sprite = GameBox.instance.bloonsData.redBloonSprite;
            movementSpeed = GameBox.instance.bloonsData.redBloonSpeed;
            myCircleCollider2D.radius = GameBox.instance.bloonsData.redBloonSize;
        }
    }

}
