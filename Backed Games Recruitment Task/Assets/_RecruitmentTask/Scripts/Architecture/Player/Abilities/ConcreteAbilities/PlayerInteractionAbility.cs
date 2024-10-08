using UnityEngine;

namespace Assets._RecruitmentTask.Scripts.Architecture.Player.Abilities.ConcreteAbilities
{
    public class PlayerInteractionAbility : MonoBehaviour, IPlayerAbility
    {
        public void Enable()
        {
            enabled = true;
        }

        public void Disable()
        {
            enabled = false;
        }

        public void UseAbility()
        {

        }
    }
}
