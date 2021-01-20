using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Projectile
{
    protected bool neverInteracted = true;
    [SerializeField]
    protected ProjectileTypes ExploisonType = ProjectileTypes.Explosion;

    protected virtual void OnEnable()
    {
        neverInteracted = true;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Bloon bloonComponent) &&
            (bloonComponent.isCammo == false || canHitCamo) &&
            neverInteracted)
        {
            neverInteracted = false;
            BombAction();
        }
    }

    protected virtual void BombAction()
    {
        GameBox.instance.poolingMenager.SummonProjectile(ExploisonType, transform.position, popCountLeft, power, movementSpeed, rotationAngle, range, canHitCamo);
        GameBox.instance.poolingMenager.ReturnProjectile(gameObject, projectileName);
    }
}
