﻿namespace XIVComboExpandedestPlugin.Combos
{
    internal static class SGE
    {
        public const byte JobID = 40;

        public const uint
            Diagnosis = 24284,
            Holos = 24310,
            Ixochole = 24299,
            Egeiro = 24287,
            Kardia = 24285,
            Soteria = 24294;

        public static class Buffs
        {
            public const ushort
                Kardia = 2604;
        }

        public static class Debuffs
        {
            public const ushort
                Placeholder = 0;
        }

        public static class Levels
        {
            public const ushort
                Dosis = 1,
                Prognosis = 10,
                Soteria = 35,
                Druochole = 45,
                Kerachole = 50,
                Taurochole = 62,
                Ixochole = 52,
                Dosis2 = 72,
                Holos = 76,
                Rizomata = 74,
                Dosis3 = 82;
        }
    }

    internal class SageKardiaFeature : CustomCombo
    {
        protected override CustomComboPreset Preset => CustomComboPreset.SageKardiaFeature;

        protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
        {
            if (actionID == SGE.Soteria)
            {
                if (HasEffect(SGE.Buffs.Kardia) && IsActionOffCooldown(SGE.Soteria))
                    return SGE.Soteria;
                return SGE.Kardia;
            }

            return actionID;
        }
    }
}
