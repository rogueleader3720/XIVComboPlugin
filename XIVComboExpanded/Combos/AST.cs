using Dalamud.Game.ClientState.Conditions;
using Dalamud.Game.ClientState.JobGauge.Enums;
using Dalamud.Game.ClientState.JobGauge.Types;

namespace XIVComboExpandedestPlugin.Combos
{
    internal static class AST
    {
        public const byte JobID = 33;

        public const uint
            Benefic = 3594,
            Malefic = 3596,
            Benefic2 = 3610,
            Draw = 3590,
            Balance = 4401,
            Bole = 4404,
            Arrow = 4402,
            Spear = 4403,
            Ewer = 4405,
            Spire = 4406,
            MinorArcana = 7443,
            SleeveDraw = 7448,
            Play = 17055,
            CrownPlay = 25869,
            Astrodyne = 25870,
            Lightspeed = 3606,
            CelestialOpposition = 16553,
            CollectiveUnconscious = 3613,
            Divination = 16552,
            EarthlyStar = 7439,
            Exaltation = 25873,
            Macrocosmos = 25874,
            NeutralSect = 16559,
            Synastry = 3612,
            Horoscope = 16557;

        public static class Buffs
        {
            public const ushort
                LordOfCrownsDrawn = 2054,
                LadyOfCrownsDrawn = 2055,
                Balance = 913,
                Bole = 914,
                Arrow = 915,
                Spear = 916,
                Ewer = 917,
                Spire = 918,
                Divination = 1878;
        }

        public static class Debuffs
        {
            public const ushort Placeholder = 0;
        }

        public static class Levels
        {
            public const byte
                Benefic2 = 26,
                MinorArcana = 70;
        }
    }

    internal class AstrologianCardsOnDrawFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.AstrologianCardsOnDrawFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == AST.Play)
            {
                var gauge = GetJobGauge<ASTGauge>();
                if (!gauge.ContainsSeal(SealType.NONE) && IsEnabled(CustomComboPreset.AstrologianAstrodynePlayFeature) && (gauge.DrawnCard != CardType.NONE || GetCooldown(AST.Draw).CooldownRemaining > 30))
                    return AST.Astrodyne;

                if (HasEffect(AST.Buffs.Balance) || HasEffect(AST.Buffs.Bole) || HasEffect(AST.Buffs.Arrow) || HasEffect(AST.Buffs.Spear) || HasEffect(AST.Buffs.Ewer) || HasEffect(AST.Buffs.Spire))
                    return OriginalHook(AST.Play);

                return AST.Draw;
            }

            return actionID;
        }
    }

    internal class AstrologianAstrodynePlayFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.AstrologianAstrodynePlayFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == AST.Play && !IsEnabled(CustomComboPreset.AstrologianCardsOnDrawFeature))
            {
                var gauge = GetJobGauge<ASTGauge>();
                if (!gauge.ContainsSeal(SealType.NONE))
                    return AST.Astrodyne;
            }

            return actionID;
        }
    }

    internal class AstrologianDrawLockoutFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.AstrologianDrawLockoutFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            var gauge = GetJobGauge<ASTGauge>();

            return gauge.DrawnCard != CardType.NONE ? OriginalHook(AST.Malefic) : actionID;
        }
    }

    internal class AstrologianDivinationLockoutFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.AstrologianDivinationLockoutFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            return actionID == AST.Divination && IsActionOffCooldown(AST.Divination) && HasEffectAny(AST.Buffs.Divination) && FindEffectAny(AST.Buffs.Divination)?.RemainingTime > 3 ? SMN.Physick : actionID;
        }
    }

    internal class AstrologianBeneficFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.AstrologianBeneficFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == AST.Benefic2)
            {
                if (level < AST.Levels.Benefic2)
                    return AST.Benefic;
            }

            return actionID;
        }
    }

    internal class AstrologianLucidReminderFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.AstrologianLucidReminderFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            return OriginalHook(actionID) == actionID && HasCondition(ConditionFlag.InCombat) && IsActionOffCooldown(All.LucidDreaming) && !IsActionOffCooldown(actionID) && LocalPlayer?.CurrentMp <= 9000 && CanUseAction(All.LucidDreaming) ? All.LucidDreaming : actionID;
        }
    }
}
