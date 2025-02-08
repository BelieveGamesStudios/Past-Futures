using UnityEngine;

namespace Believe.Games.Studios
{
    public class Weapon : MonoBehaviour, IInteract
    {
        [SerializeField] WeaponTypes weaponType;
        public void Interact()
        {
            FindFirstObjectByType<WeaponManager>().EquipNewWeapon(weaponType);
            Destroy(gameObject);
        }
    }
}
