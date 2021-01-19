using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    protected ProjectileTypes projectileName;
    protected int popCountLeft = 1;
    protected int power = 1;
    protected float movementSpeed = 5;
    protected float rotationAngle;
    protected float range = 10;
    protected float rangeLeft;
    protected bool canHitCamo;

    protected float spriteRotationAngle;
    protected Rigidbody2D myRigidbody2D;

    protected virtual void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        CalculateInitialProjectileData();
    }

    protected virtual void CalculateInitialProjectileData()
    {
        rangeLeft = range;

        //Przerabianie kątów na wektory by wysłać tam darta
        myRigidbody2D.velocity = new Vector2(Mathf.Cos(rotationAngle * Mathf.Deg2Rad) * movementSpeed, Mathf.Sin(rotationAngle * Mathf.Deg2Rad) * movementSpeed);

        //Obracanie sprite darta w kierunku lotu
        spriteRotationAngle = Mathf.Atan2(myRigidbody2D.velocity.y, myRigidbody2D.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(spriteRotationAngle, Vector3.forward);
    }

    public virtual void SetMe(ProjectileTypes _projectileName, Vector3 _position, int _popCountLeft, int _power,
                                        float _movementSpeed, float _rotationAngle, float _range, bool _canHitCamo)
    {
        projectileName = _projectileName;
        transform.position = _position;
        popCountLeft = _popCountLeft;
        power = _power;
        movementSpeed = _movementSpeed;
        rotationAngle = _rotationAngle;
        range = _range;
        canHitCamo = _canHitCamo;

        CalculateInitialProjectileData();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Bloon bloonComponent) &&
            !(bloonComponent.parentPopProjectles.Contains( gameObject)) &&
            popCountLeft > 0 &&
            (bloonComponent.isCammo == false || canHitCamo) &&
            bloonComponent.neverLayerPoped)
        {
            ProjectileAction(bloonComponent);
        }
    }

    //domyślne działąnie pocisku
    protected virtual void ProjectileAction(Bloon bloonComponent)
    {
        popCountLeft--;
        bloonComponent.LayerPop(power, gameObject);
        if (popCountLeft <= 0)
        {
            GameBox.instance.poolingMenager.ReturnProjectile(gameObject, projectileName);
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        rangeLeft -= Time.deltaTime * movementSpeed;
        if (rangeLeft <= 0)
        {
            GameBox.instance.poolingMenager.ReturnProjectile(gameObject, projectileName);
        }
    }

}
