using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaisicBloon : Bloon
{
    /*private CircleCollider2D myCircleCollider2D;
    private SpriteRenderer mySpriteRenderer;

    protected override void Start()
    {
        myCircleCollider2D = GetComponent<CircleCollider2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        base.Start();
    }

    public override void LayerPop(int power)
    {
        base.LayerPop(power);

        //zmiana właśniwości po pop-nięciu
        switch(layersLeft)
        {
            case 4:
                mySpriteRenderer.sprite = GameBox.instance.bloonsData.yellowBloonSprite;
                movementSpeed = GameBox.instance.bloonsData.yellowBloonSpeed;
                myCircleCollider2D.radius = GameBox.instance.bloonsData.yellowBloonSize;
                break;
            case 3:
                mySpriteRenderer.sprite = GameBox.instance.bloonsData.greenBloonSprite;
                movementSpeed = GameBox.instance.bloonsData.greenBloonSpeed;
                myCircleCollider2D.radius = GameBox.instance.bloonsData.greenBloonSize;
                break;
            case 2:
                mySpriteRenderer.sprite = GameBox.instance.bloonsData.blueBloonSprite;
                movementSpeed = GameBox.instance.bloonsData.blueBloonSpeed;
                myCircleCollider2D.radius = GameBox.instance.bloonsData.blueBloonSize;
                break;
            case 1:
                mySpriteRenderer.sprite = GameBox.instance.bloonsData.redBloonSprite;
                movementSpeed = GameBox.instance.bloonsData.redBloonSpeed;
                myCircleCollider2D.radius = GameBox.instance.bloonsData.redBloonSize;
                break;
        }
    }
    */
}
