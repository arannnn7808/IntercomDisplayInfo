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
            
            var classD = Player.List.Count(p => p.Role == RoleTypeId.ClassD);
            var fforces = Player.List.Count(p => p.Team == Team.FoundationForces);
            var scientist = Player.List.Count(p => p.Role == RoleTypeId.Scientist);
            var chaos = Player.List.Count(p => p.Team == Team.ChaosInsurgency);
            var scps = Player.List.Count(p => p.Team == Team.SCPs);
            
            static string GetTranslatedState(IntercomState state)
            {
                var translation = IntercomDisplayInfo.Instance.Translation;
                
                return state switch
                {
                    IntercomState.Ready => translation.StateReady,
                    IntercomState.Starting => translation.StateStarting,
                    IntercomState.InUse => translation.StateInUse,
                    IntercomState.Cooldown => translation.StateCooldown,
                    IntercomState.NotFound => translation.StateNotFound,
                    _ => state.ToString()
                };
            }

            var intercomState = Intercom.State;
            string translatedState = GetTranslatedState(intercomState);
            var remainingTime = Mathf.Round(icomInstance.RemainingTime);
            string intercomRemainingTime = remainingTime > 1 ? remainingTime.ToString() : "";
            string roundTime = $"{Round.Duration.TotalSeconds / 60:00}:{Round.Duration.TotalSeconds % 60:00}";
            
            StringBuilder stringBuilder = new StringBuilder();
            
            if (IntercomDisplayInfo.Instance.Config.RoundTimer)
                stringBuilder.AppendLine($"{IntercomDisplayInfo.Instance.Translation.RoundTimer} - {roundTime}");

            stringBuilder.AppendLine($"<color=white>{IntercomDisplayInfo.Instance.Translation.Information}</color>");
            
            if (IntercomDisplayInfo.Instance.Config.ClassD)
                stringBuilder.AppendLine($"<color=#f7a71b>{IntercomDisplayInfo.Instance.Translation.ClassDText} - {classD}</color>");
            
            if (IntercomDisplayInfo.Instance.Config.FForces)
                stringBuilder.AppendLine($"<color=#3550db>{IntercomDisplayInfo.Instance.Translation.FoundationForces} - {fforces}</color>");
            
            if (IntercomDisplayInfo.Instance.Config.Scientist)
                stringBuilder.AppendLine($"<color=#d4eb7a>{IntercomDisplayInfo.Instance.Translation.Scientists} - {scientist}</color>");
            
            if (IntercomDisplayInfo.Instance.Config.Chaos)
                stringBuilder.AppendLine($"<color=#227a0f>{IntercomDisplayInfo.Instance.Translation.ChaosInsurgency} - {chaos}</color>");
            
            if (IntercomDisplayInfo.Instance.Config.Scps)
                stringBuilder.AppendLine($"<color=#c21010>{IntercomDisplayInfo.Instance.Translation.SCPs} - {scps}</color>");

            stringBuilder.AppendLine("<color=white>-------------------------------------</color>");
            
            stringBuilder.AppendLine($"{IntercomDisplayInfo.Instance.Translation.State} - {translatedState}");
            if (Intercom.State == IntercomState.InUse || Intercom.State == IntercomState.Cooldown)
            {
                stringBuilder.AppendLine($"{IntercomDisplayInfo.Instance.Translation.Timer} {intercomRemainingTime}");
            }
            
            
            singletonInstance.Network_overrideText = stringBuilder.ToString();
            stringBuilder.Clear();
        }
    }
}