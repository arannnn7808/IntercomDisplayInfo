using System;
using HarmonyLib;
using LabApi.Features;
using LabApi.Loader;
using LabApi.Loader.Features.Plugins;

namespace IntercomDisplayInfo
{
    public class IntercomDisplayInfo : Plugin
    {
        public static IntercomDisplayInfo Instance;
        public Translation Translation { get; private set; }
        public Config Config { get; private set; }
        public override string Name => "IntercomDisplayInfo";
        public override string Description => "Turns the Intercom into a live stats HUD. Displays the round timer, team counts, and Intercom status.";
        public override string Author => "araangarciiia";
        public override Version Version => new(1,0,1);
        public override Version RequiredApiVersion => new(LabApiProperties.CompiledVersion);
        
        private Harmony harmony = new("dev.araangarciiia.intercomdisplayinfo");
        
        public override void Enable()
        {
            Instance = this;
            harmony.PatchAll();
            LoadConfigs();
        }

        public override void Disable()
        {
            Instance = null;
            harmony.UnpatchAll();
        }
        
        public override void LoadConfigs()
        {
            this.TryLoadConfig("config.yml", out Config config);
            Config = config ?? new Config();
            this.TryLoadConfig("translation.yml", out Translation translation);
            Translation = translation ?? new Translation();
        }
    }
}