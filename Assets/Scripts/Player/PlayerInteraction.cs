using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

namespace Believe.Games.Studios
{
    public class PlayerInteraction : MonoBehaviour
    {

        [Header("Input System")]
        InputSystem_Actions inputHandler;

        [Header("Interaction Details")]
        [SerializeField] float interactionRange = 10;
        Transform interactionPoint;
        IInteract nearbyItem;


        [Header("UI")]
        [SerializeField] TMP_Text interactText;
        void OnEnable()
        {
            interactionPoint = Camera.main.transform;
            inputHandler=new InputSystem_Actions();
            inputHandler.Player.Enable();
            inputHandler.Player.Interact.performed += ctx => OnInteract();
        }

        void Update()
        {
            SearchForItems();
        }
        void SearchForItems()
        {
            RaycastHit hit;
            if(Physics.Raycast(interactionPoint.position,interactionPoint.forward,out hit, interactionRange))
            {
                IInteract item=hit.transform.GetComponent<IInteract>();
                if(item!=null)
                {
                    nearbyItem= item;
                    interactText.text = "PRESS E TO INTERACT";
                    return;
                }
                interactText.text = "";
                nearbyItem = null;
            }
        }
        void OnInteract()
        {
            if (nearbyItem == null) return;
            nearbyItem.Interact();
            nearbyItem = null;
        }
    }
    public interface IInteract
    {
        public void Interact();
    }
}
