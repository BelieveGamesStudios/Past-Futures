using UnityEngine;

namespace Believe.Games.Studios
{
    public class WeaponManager : MonoBehaviour
    {
        [Header("Gun Slots")]
        [SerializeField] GameObject glockArm;
        [SerializeField] GameObject pythonArm;
        [SerializeField] GameObject UMP45Arm;
        [SerializeField] GameObject mp5kArm;
        [SerializeField] GameObject akmArm;
        [SerializeField] GameObject m870Arm;
        public void EquipNewWeapon(WeaponTypes weaponType)
        {
            DisableAllWeapons();
            switch (weaponType)
            {
                case WeaponTypes.Glock:
                    glockArm.SetActive(true);
                    break;
                case WeaponTypes.Python:
                    pythonArm.SetActive(true);
                    break;
                case WeaponTypes.MP5K:
                    mp5kArm.SetActive(true);
                    break;
                case WeaponTypes.AKM:
                    akmArm.SetActive(true);
                    break;
                case WeaponTypes.UMP45:
                    UMP45Arm.SetActive(true);
                    break;
                case WeaponTypes.m870:
                    m870Arm.SetActive(true);
                    break;

            }
        }
        void DisableAllWeapons()
        {
            glockArm.SetActive(false);
            pythonArm.SetActive(false);
            UMP45Arm.SetActive(false);
            mp5kArm.SetActive(false);
            akmArm.SetActive(false);
            m870Arm.SetActive(false);
        }
       
    }
    public enum WeaponTypes
    {
        Glock,
        AKM,
        MP5K,
        UMP45,
        Python,
        m870
    }
}
