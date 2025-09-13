using System.ComponentModel;

namespace IntercomDisplayInfo
{
    public class Translation
    {
        [Description("The label displayed next to the round timer. Example: 'Round Time:'")]
        public string RoundTimer { get; set; } = "Round Timer";

        [Description("The text used as a header for the team information section.")]
        public string Information { get; set; } = "------Information------";

        [Description("The label for the Intercom's remaining time (either for usage or cooldown). Example: 'Time Remaining:'")]
        public string Timer { get; set; } = "Time Remaining";
        
        [Description("The display name for the Class-D team.")]
        public string ClassDText { get; set; } = "Class-D";

        [Description("The display name for the Foundation Forces (groups MTF and Facility Guards).")]
        public string FoundationForces { get; set; } = "Foundation Forces";
        
        [Description("The display name for the Scientist team.")]
        public string Scientists { get; set; } = "Scientists";

        [Description("The display name for the Chaos Insurgency team.")]
        public string ChaosInsurgency { get; set; } = "Chaos Insurgency";

        [Description("The display name for the SCP team.")]
        public string SCPs { get; set; } = "SCPs";
        
        [Description("The label that precedes the current Intercom status. Example: 'Status:'")]
        public string State { get; set; } = "Intercom Status";
        
        [Description("The text shown when the Intercom is ready to be used.")]
        public string StateReady { get; set; } = "Ready";

        [Description("The text shown during the Intercom's brief startup sequence before a player can speak.")]
        public string StateStarting { get; set; } = "Starting";

        [Description("The text shown when a player is actively speaking on the Intercom.")]
        public string StateInUse { get; set; } = "In Use";

        [Description("The text shown when the Intercom is on cooldown after being used.")]
        public string StateCooldown { get; set; } = "Cooldown";

        [Description("The text shown if the Intercom's state cannot be determined (usually an error or notfound state).")]
        public string StateNotFound { get; set; } = "Not Found";
    }
}