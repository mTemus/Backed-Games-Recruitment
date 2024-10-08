using Assets._RecruitmentTask.Scripts.Architecture.Events.Listeners;
using UnityEngine;

namespace Assets._RecruitmentTask.Scripts.Architecture.Events.ConcreteEvents
{
    [CreateAssetMenu(fileName = "[NAME]_EmptyScriptableEvent", menuName = "ScriptableObject/Architecture/Events/Empty")]
    public class ScriptableEventEmpty : ScriptableEvent<Empty>
    {
    }
}
