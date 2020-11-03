using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bloon : MonoBehaviour
{
    [SerializeField]
    protected int layersLeft = 1;
    [SerializeField]
    protected float movementSpeed = 3.5f;
    [SerializeField]
    protected float rotationAngle;
    [SerializeField]
    protected bool isCammo;
    [SerializeField]
    protected bool isRegrow;
    protected Rigidbody2D myRigidbody2D;



    // Start is called before the first frame update
    protected virtual void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    public virtual void LayerPop(int power)
    {
        layersLeft -= power;
        if (layersLeft <= 0)
        {
            Debug.Log("Bloon died");
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {   
        //Przerabianie kątów na wektory by wysłać tam bloona
        myRigidbody2D.velocity = new Vector2(Mathf.Cos(rotationAngle * Mathf.Deg2Rad) * movementSpeed, Mathf.Sin(rotationAngle * Mathf.Deg2Rad) * movementSpeed);
    }
}
