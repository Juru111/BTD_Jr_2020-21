﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private int popCountLeft = 1;
    private int power = 1;
    [SerializeField]
    private float movementSpeed = 5;
    [SerializeField]
    private float rotationAngle;
    private float spriteRotationAngle;
    private Rigidbody2D myRigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();

        //Przerabianie kątów na wektory by wysłać tam darta
        myRigidbody2D.velocity = new Vector2(Mathf.Cos(rotationAngle * Mathf.Deg2Rad) * movementSpeed, Mathf.Sin(rotationAngle * Mathf.Deg2Rad) * movementSpeed);

        //Obracanie sprite darta w kierunku lotu
        spriteRotationAngle = Mathf.Atan2(myRigidbody2D.velocity.y, myRigidbody2D.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(spriteRotationAngle, Vector3.forward);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(this+ " spotkał " + collision.gameObject.name);

        //zamienić sprawdzanie layera na sprawdzenie czy ma component (klasę) "Bloon"
        if(collision.gameObject.layer == 8)
        {
            collision.GetComponent<Bloon>().LayerPop(power);
            popCountLeft--;
            if (popCountLeft <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}