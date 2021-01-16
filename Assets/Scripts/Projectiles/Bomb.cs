using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Projectile
{
    private bool neverInteracted = true;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Bloon bloonComponent) &&
            (bloonComponent.isCammo == false || canHitCamo) &&
            neverInteracted)
        {
            neverInteracted = false;
            GameBox.instance.poolingMenager.SummonProjectile(ProjectileTypes.Explosion, transform.position, popCountLeft, power, movementSpeed, rotationAngle, range, canHitCamo);
            GameBox.instance.poolingMenager.ReturnProjectile(gameObject, projectileName);
        }
    }
}
