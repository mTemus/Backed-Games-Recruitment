using Assets._RecruitmentTask.Scripts.Enemy;
using UnityEngine;

namespace Assets._RecruitmentTask.Scripts.Architecture.Events.ConcreteEvents
{
    [CreateAssetMenu(fileName = "[NAME]_EnemyDeathDataScriptableEvent", menuName = "ScriptableObject/Architecture/Events/Enemy Death Data")]

    public class ScriptableEventEnemyDeathData : ScriptableEvent<EnemiesManager.EnemyDeathData>
    {
    }
}
