using Dalamud.Game.ClientState.JobGauge.Enums;
using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVComboExpandedestPlugin.Combos
{
    internal static class SMN
    {
        public const byte ClassID = 26;
        public const byte JobID = 27;

        public const uint
            Deathflare = 3582,
            EnkindlePhoenix = 16516,
            EnkindleBahamut = 7429,
            DreadwyrmTrance = 3581,
            SummonBahamut = 7427,
            SummonPhoenix = 25831,
            Aethercharge = 25800,
            Ruin1 = 163,
            Ruin3 = 3579,
            BrandOfPurgatory = 16515,
            FountainOfFire = 16514,
            AstralImpulse = 25820,
            Fester = 181,
            EnergyDrain = 16508,
            Painflare = 3578,
            EnergySyphon = 16510,
            SummonCarbuncle = 25798,
            RadiantAegis = 25799;

        public static class Buffs
        {
            public const ushort
                HellishConduit = 1867,
                Aetherflow = 304;
        }

        public static class Debuffs
        {
            public const ushort Placeholder = 0;
        }

        public static class Levels
        {
            public const byte
                Painflare = 52,
                Ruin3 = 54,
                SummonBahamut = 70,
                EnhancedFirebirdTrance = 80;
        }
    }

    internal class SummonerDemiCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SummonerDemiCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            // Replace demi summons with enkindle
            if (actionID == SMN.SummonBahamut || actionID == SMN.SummonPhoenix || actionID == SMN.DreadwyrmTrance || actionID == SMN.Aethercharge)
            {
                if (OriginalHook(SMN.Ruin1) == SMN.AstralImpulse && level >= SMN.Levels.SummonBahamut)
                    return SMN.EnkindleBahamut;
                if (OriginalHook(SMN.Ruin1) == SMN.FountainOfFire)
                    return SMN.EnkindlePhoenix;
            }

            return actionID;
        }
    }


    internal class SummonerEDFesterCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SummonerEDFesterCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SMN.Fester)
            {
                var gauge = GetJobGauge<SMNGauge>();
                if (!gauge.HasAetherflowStacks)
                    return SMN.EnergyDrain;
            }

            return actionID;
        }
    }

    internal class SummonerESPainflareCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SummonerESPainflareCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SMN.Painflare)
            {
                var gauge = GetJobGauge<SMNGauge>();
                if (!gauge.HasAetherflowStacks)
                    return SMN.EnergySyphon;

                if (level >= SMN.Levels.Painflare)
                    return SMN.Painflare;

                return SMN.EnergySyphon;
            }

            return actionID;
        }
    }

    /*internal class SummonerCarbyFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SummonerCarbyFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SMN.SummonCarbuncle)
            {
                var gauge = GetJobGauge<SMNGauge>();
                if (gauge.ReturnSummon != SummonPet.NONE)
                    return SMN.RadiantAegis;
            }

            return actionID;
        }
    }*/
}
