using UnityEngine;

namespace Assets._RecruitmentTask.Scripts.Architecture.Player.Abilities.ConcreteAbilities
{
    public class PlayerInteractionAbility : MonoBehaviour, IPlayerAbility
    {
        [Header("Properties")] [SerializeField]
        private LayerMask m_interactableLayer;

        private Ray m_cameraToMouseRay;
        private RaycastHit m_hit;

        public bool IsEnabled { get; set; }

        public void Enable()
        {
            IsEnabled = true;
        }

        public void Disable()
        {
            IsEnabled = false;

            m_cameraToMouseRay = default;
            m_hit = default;
        }

        public void UseAbility()
        {
            if (!IsEnabled)
            {
                Debug.LogError($"{GetType().Name} ({name}) --- Trying to use ability that is not enabled!");
                return;
            }

            m_cameraToMouseRay = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);

            if (!Physics.Raycast(m_cameraToMouseRay, out m_hit, Mathf.Infinity, m_interactableLayer))
                return;

            m_hit.collider.GetComponent<IInteractable>().Interact(transform.parent.gameObject, m_hit.point);
        }
    }
}
