using UnityEngine;

namespace Believe.Games.Studios
{
    public class DestroyGameobject : MonoBehaviour
    {
        internal enum DestructionType
        {
            Time,
            Distance
        }
        [SerializeField] private DestructionType destroyType;
        [SerializeField] float destroyDistance = 50;
        [SerializeField] float destroyTime = 1;
        [SerializeField] Transform Player;
        private void OnEnable()
        {
            if (destroyType == DestructionType.Distance)
            {
                if (Player == null)
                {
                    Debug.LogWarning("Player cannot be found");
                    destroyType = DestructionType.Time;
                }
            }
        }

        private void Update()
        {
            switch (destroyType)
            {

                case DestructionType.Time:
                    Destroy(gameObject, destroyTime);
                    break;
                case DestructionType.Distance:
                    if (Player == null) return;
                    if (Vector3.Distance(Player.position, transform.position) >= destroyDistance)
                    {
                        Destroy(gameObject);
                    }
                    break;
            }

        }
    }
}
