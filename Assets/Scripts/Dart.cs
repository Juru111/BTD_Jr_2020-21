using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : MonoBehaviour
{
    private int popCountLeft = 1;
    private int power = 1;
    [SerializeField]
    private float mSpeed = 5;
    [SerializeField]
    private float rotationValue = 0;
    private float spriteRotationValue = 0;
    private Rigidbody2D rbody2D;

    // Start is called before the first frame update
    void Start()
    {
        rbody2D = GetComponent<Rigidbody2D>();
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(this+ " spotkał " + collision.gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        //Przerabianie kątów na wektory by wysłać tam darta
        //rbody2D.velocity = new Vector2(mSpeed, 0);
        rbody2D.velocity = new Vector2(Mathf.Cos(rotationValue * Mathf.Deg2Rad) * mSpeed, Mathf.Sin(rotationValue * Mathf.Deg2Rad) * mSpeed);

        //Obracanie sprite darta w kierunku lotu
        spriteRotationValue = Mathf.Atan2(rbody2D.velocity.y, rbody2D.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(spriteRotationValue, Vector3.forward);
    }
}
