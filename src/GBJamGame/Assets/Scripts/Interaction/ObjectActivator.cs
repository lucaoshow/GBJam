using UnityEngine;

namespace Root.Interactions
{
    public class ObjectActivator : MonoBehaviour
    {
        [SerializeField] private GameObject[] objectsToActivate;
        [SerializeField] private Interactable interactable;
      
        private void Update()
        {
            if (!this.interactable.CanInteract())
            {
                foreach (GameObject obj in objectsToActivate)
                {
                    obj.SetActive(true);
                }
                Destroy(this);
            }
        }

    }
}
