using System.Collections.Generic;
using UnityEngine;

namespace Assets._RecruitmentTask.Scripts.Architecture.Player.Abilities
{
    public class PlayerAbilitiesController : MonoBehaviour
    {
        // With odin, it would be able to be serialized in the inspector
        private readonly List<IPlayerAbility> m_playerAbilities = new List<IPlayerAbility>();

        private void Awake()
        {
            enabled = false;

            foreach (var ability in GetComponents<IPlayerAbility>())
                m_playerAbilities.Add(ability);
    
            DisableAllAbilities();
        }

        private void OnDestroy()
        {
            m_playerAbilities.Clear();
        }

        public void EnableAllAbilities()
        {
            SetAbilitiesState(true);
        }

        public void DisableAllAbilities()
        {
            SetAbilitiesState(false);
        }

        private void SetAbilitiesState(bool isActive)
        {
            for (var i = 0; i < m_playerAbilities.Count; i++)
            {
                if (isActive)
                    m_playerAbilities[i].Enable();
                else
                    m_playerAbilities[i].Disable();
            }
        }
    }
}
