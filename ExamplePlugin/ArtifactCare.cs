using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using RoR2;
using R2API;

namespace ArtifactOfCare
{
    internal class ArtifactCare : ArtifactBase
    {
        public static ConfigEntry<int> TimesToPrintMessageOnStart;
        public override string ArtifactName => "Artifact of Care";
        public override string ArtifactLangTokenName => "KERMIR_ARTIFACT_OF_CARE";
        public override string ArtifactDescription => "When enabled, most items and money will become shared. Some Multiplayer bonuses are reduced. (Toggles ShareSuite)";
        public override Sprite ArtifactEnabledIcon => Main.MainAssets.LoadAsset<Sprite>("ArtifactOfCare_On.png");
        public override Sprite ArtifactDisabledIcon => Main.MainAssets.LoadAsset<Sprite>("ArtifactOfCare_Off.png");
        public override void Init(ConfigFile config)
        {
            Log.Debug("Initializing Artifact Of Care Artifact");
            CreateLang();
            CreateArtifact();
            Hooks();
        }

        public override void Hooks()
        {
            Run.onRunStartGlobal += ToggleShareSuite;
        }
        private void ToggleShareSuite(Run run)
        {
            if (NetworkServer.active)
            {
                if (ArtifactEnabled)
                {
                    Log.Debug("Enabling ShareSuite");
                    RoR2.Console.instance.SubmitCmd(null, "ss_Enabled True");
                    //run command to enable
                }
                else
                {
                    Log.Debug("Disabling ShareSuite");
                    RoR2.Console.instance.SubmitCmd(null, "ss_Enabled False");
                    //run command to disable sharesuite
                }                
            }
        }
    }
}
