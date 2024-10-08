using Assets._RecruitmentTask.Scripts.Wave.Data;
using UnityEngine;

namespace Assets._RecruitmentTask.Scripts.Architecture.Events.ConcreteEvents
{
    [CreateAssetMenu(fileName = "[NAME]_WaveDataScriptableEvent", menuName = "ScriptableObject/Architecture/Events/Wave Data SO")]
    public class ScriptableEventWaveData : ScriptableEvent<WaveDataSO>
    {
    
    }
}
