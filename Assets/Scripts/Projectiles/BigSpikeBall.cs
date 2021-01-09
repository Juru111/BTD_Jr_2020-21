using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSpikeBall : Projectile
{
    protected override void ProjectileAction(Bloon bloonComponent)
    {
        if (bloonComponent.bloonName == BloonTypes.Ceramic)
        {
            popCountLeft--;
            bloonComponent.LayerPop(power*5, gameObject);
            if (popCountLeft <= 0)
            {
                GameBox.instance.poolingMenager.ReturnProjectile(gameObject, projectileName);
            }
        }
        else
        {
            base.ProjectileAction(bloonComponent);
        }
    }
}
