using System.ComponentModel;

namespace IntercomDisplayInfo
{
    public class Config
    {
        [Description("Controls whether the current round timer is displayed on the Intercom screen.")]
        public bool RoundTimer { get; set; } = true;

        [Description("Toggles the display for the number of alive Class-D personnel.")]
        public bool ClassD { get; set; } = true;

        [Description("Toggles the display for the number of alive Foundation Forces (MTF & Guards).")]
        public bool FForces { get; set; } = true;

        [Description("Toggles the display for the number of alive Scientists.")]
        public bool Scientist { get; set; } = true;

        [Description("Toggles the display for the number of alive Chaos Insurgency.")]
        public bool Chaos { get; set; } = true;

        [Description("Toggles the display for the number of alive SCPs.")]
        public bool Scps { get; set; } = true;
    }
}