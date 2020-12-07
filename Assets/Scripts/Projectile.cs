using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private int popCountLeft = 1;
    [SerializeField]
    private int power = 1;
    [SerializeField]
    private float movementSpeed = 5;
    [SerializeField]
    private float range = 10;
    [SerializeField]
    private float rangeLeft;
    [SerializeField]
    private float rotationAngle;
    private float spriteRotationAngle;
    private Rigidbody2D myRigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        rangeLeft = range;

        //Przerabianie kątów na wektory by wysłać tam darta
        myRigidbody2D.velocity = new Vector2(Mathf.Cos(rotationAngle * Mathf.Deg2Rad) * movementSpeed, Mathf.Sin(rotationAngle * Mathf.Deg2Rad) * movementSpeed);

        //Obracanie sprite darta w kierunku lotu
        spriteRotationAngle = Mathf.Atan2(myRigidbody2D.velocity.y, myRigidbody2D.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(spriteRotationAngle, Vector3.forward);
    }

    private bool busyPoping = false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        //Sprawdzenie czy napotkany objekt jest balonem oraz czy wtym momencie dart nie jest zajęty PoP-owaniem
        if (collision.TryGetComponent(out Bloon bloonComponent) && busyPoping == false)
        {
            busyPoping = true;

            //PoP-owanie
            popCountLeft--;
            bloonComponent.LayerPop(power);
            if (popCountLeft <= 0)
            {
                gameObject.SetActive(false);
                return;
            }
            busyPoping = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        rangeLeft -= Time.deltaTime * movementSpeed;
        if (rangeLeft < 0)
        { gameObject.SetActive(false); }
    }

}
