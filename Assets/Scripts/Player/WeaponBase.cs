using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.InputSystem;
namespace Believe.Games.Studios
{
    public class WeaponBase : MonoBehaviour
    {
        public PlayerMovement movement;
        public InputSystem_Actions inputHandler;
        [Header("Animation")]
        public Animator gunController;
        Transform shootPoint;
        
        [Header("Weapon Stats")]
        public float fireRate = 0.3f;
        [SerializeField] float gunRange = 80;
        [SerializeField] float gunForce = 30;
        [SerializeField] int maxAmmo = 5;
        [SerializeField] int minDamage = 20;
        [SerializeField] int maxDamage = 50;
        public int currentAmmo;
        public int MagCount=60;
        int damage;
        public float nextTimeToFire;

        public bool canShoot = false;                  //Use Animation to make this true
        public bool isReloading = false;                  //Use Animation to make this true

        [Header("SFX")]
        [SerializeField] AudioSource gunSource;
        [SerializeField] AudioSource emptySource;
        [SerializeField] ParticleSystem muzzleFlash;
        [SerializeField] ParticleSystem caseShell;
        [Header("Particle Effect Setup")]
        [SerializeField] SurfaceTypes[] surfaceTypes;
        private  void OnEnable()
        {
            inputHandler = new InputSystem_Actions();
            inputHandler.Player.Enable();
            inputHandler.Player.Reload.performed += ctx => Reload();
            inputHandler.Player.Attack.canceled += ctx => EnableShooting();
            currentAmmo = maxAmmo;
            shootPoint = Camera.main.transform;
            movement = FindFirstObjectByType<PlayerMovement>();
            gunController = GetComponent<Animator>();
            gunController.SetTrigger("Draw");
        }
        private void Update()
        {
            PlayAnimation();
            
            if(inputHandler.Player.Attack.IsPressed() && !isReloading)
            {
                Shoot();
                return;
            }
        }
        public virtual void PlayAnimation()
        {
            
        }
        public virtual void Shoot()
        {
            if (isReloading) return;
            if (currentAmmo <= 0)
            {
                emptySource.Play();
                return;
            }
            currentAmmo--;
            FindFirstObjectByType<RecoilSystem1>().ApplyRecoil();
            damage = Random.Range(minDamage, maxDamage);
            gunSource.Play();
            muzzleFlash.Play();
            gunController.SetTrigger("Shoot");
            RaycastHit hit;
            if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, gunRange))
            {
                ITakeDamage zombieHealth = hit.transform.GetComponent<ITakeDamage>();
                Rigidbody hitRb = hit.transform.GetComponent<Rigidbody>();
                if(zombieHealth!=null)
                {
                    zombieHealth.TakeDamage(damage);
                }
                if(hitRb!=null)
                {
                    hitRb.AddRelativeForce(-hit.normal * gunForce, ForceMode.Impulse);
                }
                string surfaceTag = hit.transform.tag;
                for(int i=0;i<surfaceTypes.Length;i++)
                {
                    if (surfaceTag == surfaceTypes[i].SurfaceType)
                    {
                        Instantiate(surfaceTypes[i].surfaceEffect, hit.point, Quaternion.Euler(hit.normal));
                    }
                }
            }
            
        }
        public virtual void OnCaseOut()
        {
            if (caseShell == null) return;
            caseShell.Play();
        }
        public void Reload()
        {
            if (currentAmmo >= maxAmmo || MagCount<=0) return;
            canShoot = false;
            isReloading = true;
            gunController.SetTrigger("Reload");
            
        }
        public void FillAmmo()
        {
            if (MagCount > 0 && currentAmmo < maxAmmo)
            {
                int ammoToFill = maxAmmo - currentAmmo;
                if (currentAmmo <= 0 && MagCount >= maxAmmo)
                {
                    currentAmmo = maxAmmo;
                    MagCount -= maxAmmo;
                }
                else if (currentAmmo > 0 && MagCount > ammoToFill)
                {
                    currentAmmo = maxAmmo;
                    MagCount -= ammoToFill;
                }
                else if (currentAmmo > 0 && MagCount < ammoToFill)
                {
                    currentAmmo += MagCount;
                    MagCount = 0;
                }
                else if (MagCount < maxAmmo)
                {
                    currentAmmo += MagCount;
                    MagCount = 0;
                }
            }
        }
        public void EnableShooting()
        {
            if (currentAmmo <= 0) return;
            canShoot = true;
            gunController.ResetTrigger("Shoot");
            isReloading = false;
        }

    }
    [System.Serializable]
    public class SurfaceTypes
    {
        public string SurfaceType;
        public GameObject surfaceEffect;
    }
}
