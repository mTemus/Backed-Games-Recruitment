using Assets._RecruitmentTask.Scripts.Architecture.Events.Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets._RecruitmentTask.Scripts.Architecture.Input
{
    public class InputManager : MonoBehaviour
    {
        [Header("Input Events")]
        [SerializeField] private ScriptableEventEmpty m_onInteractionStarted;

        private PlayerInput m_playerInput;


        private void Awake()
        {
            m_playerInput = new PlayerInput();
            m_playerInput.PlayerAbilities.Interaction.started += OnInteractionStarted;
        }

        public void OnGameStart()
        {
            m_playerInput.PlayerAbilities.Enable();
        }

        public void OnGameEnd()
        {
            m_playerInput.PlayerAbilities.Disable();
        }

        private void OnInteractionStarted(InputAction.CallbackContext context)
        {
            m_onInteractionStarted.Invoke(default);
        }
    }
}
