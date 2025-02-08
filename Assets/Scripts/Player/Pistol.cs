using UnityEngine;

namespace Believe.Games.Studios
{
    public class Pistol : WeaponBase
    {
        public override void Shoot()
        {
            base.Shoot();
            canShoot = false;
        }
        public override void PlayAnimation()
        {
            
            if(currentAmmo<=0)
            {
                gunController.SetBool("Empty", true);
            }
            else
            {
                gunController.SetBool("Empty", false);
            }
        }
    }
}
