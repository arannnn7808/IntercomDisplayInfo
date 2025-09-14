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

        [HarmonyPrefix]
        public static void OnUpdate()
        {

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

            var remainingTime = Mathf.CeilToInt(Intercom._singleton.RemainingTime).ToString();
            string roundTime = $"{Round.Duration.TotalSeconds / 60:00}:{Round.Duration.TotalSeconds % 60:00}";

            StringBuilder stringBuilder = new StringBuilder();

            if (IntercomDisplayInfo.Instance.Config.RoundTimer)
                stringBuilder.AppendLine($"{IntercomDisplayInfo.Instance.Translation.RoundTimer} - {roundTime}");

            stringBuilder.AppendLine($"<color=white>{IntercomDisplayInfo.Instance.Translation.Information}</color>");


            var teamCounts = Player.ReadyList.GroupBy(p => p.Team).ToDictionary(g => g.Key, g => (byte)g.Count());

            if (IntercomDisplayInfo.Instance.Config.ClassD)
            {
                teamCounts.TryGetValue(Team.ClassD, out byte classDCount);
                stringBuilder.AppendLine(
                    $"<color=#f7a71b>{IntercomDisplayInfo.Instance.Translation.ClassDText} - {classDCount}</color>");
            }

            if (IntercomDisplayInfo.Instance.Config.FForces)
            {
                teamCounts.TryGetValue(Team.ClassD, out byte fforcesCount);
                stringBuilder.AppendLine($"<color=#3550db>{IntercomDisplayInfo.Instance.Translation.FoundationForces} - {fforcesCount}</color>");
            }
            
            if (IntercomDisplayInfo.Instance.Config.Scientist)
            {
                teamCounts.TryGetValue(Team.Scientists, out byte scientistCount);
                stringBuilder.AppendLine($"<color=#d4eb7a>{IntercomDisplayInfo.Instance.Translation.Scientists} - {scientistCount}</color>");
            }
            
            if (IntercomDisplayInfo.Instance.Config.Chaos)
            {
                teamCounts.TryGetValue(Team.ChaosInsurgency, out byte chaosCount);
                stringBuilder.AppendLine($"<color=#227a0f>{IntercomDisplayInfo.Instance.Translation.ChaosInsurgency} - {chaosCount}</color>");
            }
            
            if (IntercomDisplayInfo.Instance.Config.Scps)
            {
                teamCounts.TryGetValue(Team.SCPs, out byte scpCount);
                stringBuilder.AppendLine($"<color=#c21010>{IntercomDisplayInfo.Instance.Translation.SCPs} - {scpCount}</color>");
            }
            stringBuilder.AppendLine("<color=white>-------------------------------------</color>");
            
            stringBuilder.AppendLine($"{IntercomDisplayInfo.Instance.Translation.State} - {translatedState}");
            if (intercomState is IntercomState.InUse or IntercomState.Cooldown)
                stringBuilder.AppendLine($"{IntercomDisplayInfo.Instance.Translation.Timer} {remainingTime}");
            
            
            IntercomDisplay._singleton.Network_overrideText = stringBuilder.ToString();
            stringBuilder.Clear();
        }
    }
}
