using UnityEngine;

namespace Believe.Games.Studios
{
    public class Pistol : WeaponBase
    {
        [SerializeField] AudioSource cockSource;
        [SerializeField] AudioSource magOutSource;
        [SerializeField] AudioSource magInSource;
        public override void Shoot()
        {
            if (!canShoot) return;
            base.Shoot();
            canShoot = false;
        }
        public override void PlayAnimation()
        {
           if(isReloading)
            {
                gunController.SetBool("Empty", false);
                return;
            }
            if(currentAmmo<=0)
            {
                gunController.SetBool("Empty", true);
            }
            else
            {
                gunController.SetBool("Empty", false);
            }
        }
        public void OnDraw()
        {
            cockSource.Play();
        }
        public void OnMagOut()
        {
            magOutSource.Play();
        }
        public void OnMagIn()
        {
            magInSource.Play();
        }
    }
}
