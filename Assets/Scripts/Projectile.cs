using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    protected ProjectileTypes projectileName;
    [SerializeField]
    protected int popCountLeft = 1;
    [SerializeField]
    protected int power = 1;
    [SerializeField]
    protected float movementSpeed = 5;
    [SerializeField]
    protected float rotationAngle;
    [SerializeField]
    protected float range = 10;
    [SerializeField]
    protected float rangeLeft;

    protected float spriteRotationAngle;
    protected Rigidbody2D myRigidbody2D;
    protected PoolingMenager poolingMenager;

    // Start is called before the first frame update
    protected void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        rangeLeft = range;
        poolingMenager = FindObjectOfType<PoolingMenager>();

        //Przerabianie kątów na wektory by wysłać tam darta
        myRigidbody2D.velocity = new Vector2(Mathf.Cos(rotationAngle * Mathf.Deg2Rad) * movementSpeed, Mathf.Sin(rotationAngle * Mathf.Deg2Rad) * movementSpeed);

        //Obracanie sprite darta w kierunku lotu
        spriteRotationAngle = Mathf.Atan2(myRigidbody2D.velocity.y, myRigidbody2D.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(spriteRotationAngle, Vector3.forward);
    }

    public virtual void SetMe(ProjectileTypes _projectileName, Vector3 _position, int _popCountLeft, int _power,
                                        float _movementSpeed, float _rotationAngle, float _range)
    {
        projectileName = _projectileName;
        transform.position = _position;
        popCountLeft = _popCountLeft;
        power = _power;
        movementSpeed = _movementSpeed;
        rotationAngle = _rotationAngle;
        range = _range;
    }

    protected bool busyPoping = false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        //Sprawdzenie czy napotkany objekt jest balonem oraz czy wtym momencie dart nie jest zajęty PoP-owaniem
        if (collision.TryGetComponent(out Bloon bloonComponent) && busyPoping == false && gameObject != bloonComponent.parentPopProjectle)
        {
            busyPoping = true;
            ProjectileAction(bloonComponent);
            busyPoping = false;
        }

    }

    //domyślne działąnie pocisku
    protected void ProjectileAction(Bloon bloonComponent)
    {
        popCountLeft--;
        bloonComponent.LayerPop(power, gameObject);
        if (popCountLeft <= 0)
        {
            poolingMenager.ReturnProjectile(gameObject, projectileName);
            return;
        }
    }

    // Update is called once per frame
    protected void Update()
    {
        rangeLeft -= Time.deltaTime * movementSpeed;
        if (rangeLeft < 0)
        { gameObject.SetActive(false); }
    }

}
