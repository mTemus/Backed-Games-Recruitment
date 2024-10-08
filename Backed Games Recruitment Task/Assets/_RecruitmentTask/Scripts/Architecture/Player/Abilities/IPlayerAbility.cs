using UnityEngine;
using UnityEngine.Events;

namespace Assets._RecruitmentTask.Scripts.Architecture.Player.Abilities
{
    public interface IPlayerAbility
    {
        public void Enable();
        public void Disable();
    }
}
