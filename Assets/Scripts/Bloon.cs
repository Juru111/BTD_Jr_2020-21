using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bloon : MonoBehaviour
{
    public int layersLeft = 1;
    public bool isCammo;
    public bool isRegrow;
    public float movementSpeed;
    public float rotationAngle;
    public Rigidbody2D myRigidbody2D;
    
    

    // Start is called before the first frame update
    public virtual void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        
        
    }

    public virtual void LayerPop(int power)
    {
        layersLeft -= power;
        if (layersLeft <= 0)
        {
            Debug.Log("BaisicBloon died");
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    public virtual void Update()
    {   
        //Przerabianie kątów na wektory by wysłać tam darta
        myRigidbody2D.velocity = new Vector2(Mathf.Cos(rotationAngle * Mathf.Deg2Rad) * movementSpeed, Mathf.Sin(rotationAngle * Mathf.Deg2Rad) * movementSpeed);
    }
}
