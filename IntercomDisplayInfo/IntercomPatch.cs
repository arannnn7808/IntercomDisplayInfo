using System;
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
            var classD = 0;
            var fforces = 0;
            var scientist = 0;
            var chaos = 0;
            var scps = 0;

            foreach (var player in Player.List)
            {
                switch (player.Team)
                {
                    case Team.FoundationForces:
                        fforces++;
                        break;
                    case Team.ChaosInsurgency:
                        chaos++;
                        break;
                    case Team.SCPs:
                        scps++;
                        break;
                }

                switch (player.Role)
                {
                    case RoleTypeId.Scientist:
                        scientist++;
                        break;
                    case RoleTypeId.ClassD:
                        classD++;
                        break;
                }
            }

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
            var remainingTime = Mathf.Round(Intercom._singleton.RemainingTime);
            string intercomRemainingTime = remainingTime > 1 ? remainingTime.ToString() : "";
            string roundTime = Round.Duration.ToString("mm\\:ss"); 
            
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
            
            
            IntercomDisplay._singleton.Network_overrideText = stringBuilder.ToString();
            stringBuilder.Clear();
        }
    }
}