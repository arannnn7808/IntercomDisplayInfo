using System.Linq;
using System.Reflection;
using System.Text;
using HarmonyLib;
using LabApi.Features.Wrappers;
using PlayerRoles;
using PlayerRoles.Voice;
using UnityEngine;

namespace IntercomDisplayInfo
{
    [HarmonyPatch(typeof(Intercom), nameof(Intercom.Update))]
    public class IntercomPatch
    {
        private static readonly FieldInfo SingletonField = AccessTools.Field(typeof(IntercomDisplay), "_singleton");
        private static readonly FieldInfo IcomField = AccessTools.Field(typeof(IntercomDisplay), "_icom");
        
        [HarmonyPrefix]
        public static void OnUpdate()
        {
            var singletonInstance = (IntercomDisplay)SingletonField.GetValue(null);
            
            if (singletonInstance == null)
                return;
            
            var icomInstance = (Intercom)IcomField.GetValue(singletonInstance);
            if (icomInstance == null)
                return;
            
            var translation = IntercomDisplayInfo.Instance.Translation;
            var intercomState = Intercom.State;
            string translatedState = intercomState switch
            {
                IntercomState.Ready => translation.StateReady,
                IntercomState.Starting => translation.StateStarting,
                IntercomState.InUse => translation.StateInUse,
                IntercomState.Cooldown => translation.StateCooldown,
                IntercomState.NotFound => translation.StateNotFound,
                _ => intercomState.ToString()
            };
            
            var remainingTime = Mathf.CeilToInt(icomInstance.RemainingTime).ToString();
            string roundTime = $"{Round.Duration.TotalSeconds / 60:00}:{Round.Duration.TotalSeconds % 60:00}";
            
            StringBuilder stringBuilder = new StringBuilder();
            
            if (IntercomDisplayInfo.Instance.Config.RoundTimer)
                stringBuilder.AppendLine($"{IntercomDisplayInfo.Instance.Translation.RoundTimer} - {roundTime}");

            stringBuilder.AppendLine($"<color=white>{IntercomDisplayInfo.Instance.Translation.Information}</color>");
            
            
            var teamCounts = Player.ReadyList.GroupBy(p => p.Team).ToDictionary(g => g.Key, g => (byte)g.Count());
            
            if (IntercomDisplayInfo.Instance.Config.ClassD)
                stringBuilder.AppendLine($"<color=#f7a71b>{IntercomDisplayInfo.Instance.Translation.ClassDText} - {teamCounts.GetValueOrDefault(Team.ClassD)}</color>");
            
            if (IntercomDisplayInfo.Instance.Config.FForces)
                stringBuilder.AppendLine($"<color=#3550db>{IntercomDisplayInfo.Instance.Translation.FoundationForces} - {teamCounts.GetValueOrDefault(Team.FoundationForces)}</color>");
            
            if (IntercomDisplayInfo.Instance.Config.Scientist)
                stringBuilder.AppendLine($"<color=#d4eb7a>{IntercomDisplayInfo.Instance.Translation.Scientists} - {teamCounts.GetValueOrDefault(Team.Scientists)}</color>");
            
            if (IntercomDisplayInfo.Instance.Config.Chaos)
                stringBuilder.AppendLine($"<color=#227a0f>{IntercomDisplayInfo.Instance.Translation.ChaosInsurgency} - {teamCounts.GetValueOrDefault(Team.ChaosInsurgency)}</color>");
            
            if (IntercomDisplayInfo.Instance.Config.Scps)
                stringBuilder.AppendLine($"<color=#c21010>{IntercomDisplayInfo.Instance.Translation.SCPs} - {teamCounts.GetValueOrDefault(Team.SCPs)}</color>");

            stringBuilder.AppendLine("<color=white>-------------------------------------</color>");
            
            stringBuilder.AppendLine($"{IntercomDisplayInfo.Instance.Translation.State} - {translatedState}");
            if (intercomState is IntercomState.InUse or IntercomState.Cooldown)
                stringBuilder.AppendLine($"{IntercomDisplayInfo.Instance.Translation.Timer} {remainingTime}");
            
            
            singletonInstance.Network_overrideText = stringBuilder.ToString();
            stringBuilder.Clear();
        }
    }
}
