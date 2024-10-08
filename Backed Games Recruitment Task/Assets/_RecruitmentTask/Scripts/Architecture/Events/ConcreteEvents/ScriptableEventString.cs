using UnityEngine;

namespace Assets._RecruitmentTask.Scripts.Architecture.Events.ConcreteEvents
{
    [CreateAssetMenu(fileName = "[NAME]_StringScriptableEvent", menuName = "ScriptableObject/Architecture/Events/String")]
    public class ScriptableEventString : ScriptableEvent<string>
    {
    }
}
