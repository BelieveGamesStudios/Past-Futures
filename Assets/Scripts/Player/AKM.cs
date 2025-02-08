using UnityEngine;

namespace Believe.Games.Studios
{
    public class AKM : WeaponBase
    {
        [SerializeField] AudioSource cockSource;
        [SerializeField] AudioSource magOutSource;
        [SerializeField] AudioSource magInSource;
        public override void Shoot()
        {
            if(isReloading)return;
            if(Time.time>=nextTimeToFire)
            {
                nextTimeToFire = Time.time + fireRate;
                base.Shoot();
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
