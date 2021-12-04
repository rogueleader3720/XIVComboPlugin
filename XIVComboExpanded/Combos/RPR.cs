namespace XIVComboExpandedestPlugin.Combos
{
    internal static class RPR
    {
        public const byte JobID = 39;

        public const uint
            // Single Target
            Slice = 24373,
            WaxingSlice = 24374,
            InfernalSlice = 24375,
            Gibbet = 24382,
            Gallows = 24383,
            ShadowOfDeath = 24378,
            // AoE
            SpinningScythe = 24376,
            NightmareScythe = 24377,
            Guillotine = 24384,
            // Shroud
            Enshroud = 24394,
            Communio = 24398,
            ArcaneCircle = 24405,
            PlentifulHarvest = 24385;

        public static class Buffs
        {
            public const ushort
                Enshrouded = 2593,
                SoulReaver = 2587,
                ImmortalSacrifice = 2592;
        }

        public static class Debuffs
        {
            public const ushort
                Placeholder = 0;
        }

        public static class Levels
        {
            public const byte
                Slice = 1,
                WaxingSlice = 5,
                SpinningScythe = 25,
                InfernalSlice = 30,
                NightmareScythe = 45,
                Enshroud = 80,
                Communio = 90;
        }
    }

    internal class ReaperSliceCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.ReaperSliceCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == RPR.Slice)
            {
                if (IsEnabled(CustomComboPreset.ReaperGibbetGallowsGuillotineFeature) && HasEffect(RPR.Buffs.SoulReaver))
                    return RPR.Gibbet;

                if (comboTime > 0)
                {
                    if (lastComboMove == RPR.Slice && level >= RPR.Levels.WaxingSlice)
                        return RPR.WaxingSlice;

                    if (lastComboMove == RPR.WaxingSlice && level >= RPR.Levels.InfernalSlice)
                        return RPR.InfernalSlice;
                }

                return RPR.Slice;
            }

            return actionID;
        }
    }

    internal class ReaperScytheCombo : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.ReaperScytheCombo;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == RPR.SpinningScythe)
            {
                if (IsEnabled(CustomComboPreset.ReaperGibbetGallowsGuillotineFeature) && HasEffect(RPR.Buffs.SoulReaver))
                    return RPR.Guillotine;

                if (comboTime > 0)
                {
                    if (lastComboMove == RPR.SpinningScythe && level >= RPR.Levels.NightmareScythe)
                        return RPR.NightmareScythe;
                }

                return RPR.SpinningScythe;
            }

            return actionID;
        }
    }

    internal class EnshroudCommunioFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.ReaperEnshroudCommunioFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == RPR.Enshroud)
            {
                if (level >= RPR.Levels.Communio && HasEffect(RPR.Buffs.Enshrouded))
                    return RPR.Communio;

                return RPR.Enshroud;
            }

            return actionID;
        }
    }

    internal class GibbetGallowsGuillotineFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.ReaperGibbetGallowsGuillotineFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == RPR.ShadowOfDeath)
            {
                if (HasEffect(RPR.Buffs.SoulReaver))
                {
                    return RPR.Gallows;
                }
            }

            return actionID;
        }
    }

    internal class ReaperHarvestFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.ReaperHarvestFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == RPR.ArcaneCircle)
            {
                if (HasEffect(RPR.Buffs.ImmortalSacrifice) && level >= RPR.Levels.PlentifulHarvest)
                    return RPR.PlentifulHarvest;
            }

            return actionID;
        }
    }
}