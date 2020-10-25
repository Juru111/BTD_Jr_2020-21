using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BasicBloon : MonoBehaviour
{
    public int layersLeft = 5;
    [SerializeField]
    private float mSpeed = 0;
    private Rigidbody2D rbody2D;

    // Start is called before the first frame update
    void Start()
    {
        rbody2D = GetComponent<Rigidbody2D>();
    }

    public void LayerPop()
    {
        layersLeft--;
        if (layersLeft == 0)
        {
            this.gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.position = transform.position + new Vector3(mSpeed * Time.deltaTime, 0, 0);
        rbody2D.velocity = new Vector2(mSpeed, 0);
    }
    void Update()
    {
        //DataBase.
    }
}
