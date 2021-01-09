using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : Projectile
{
    protected override void ProjectileAction(Bloon bloonComponent)
    {
        if (bloonComponent.bloonName == BloonTypes.Lead)
        {
            GameBox.instance.poolingMenager.ReturnProjectile(gameObject, projectileName);
        }
        else
        {
            base.ProjectileAction(bloonComponent);
        }
    }
}
