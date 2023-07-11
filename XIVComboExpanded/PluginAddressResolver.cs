using System;

using Dalamud.Game;
using Dalamud.Logging;

namespace XIVComboExpandedestPlugin
{
    /// <summary>
    /// Plugin address resolver.
    /// </summary>
    internal class PluginAddressResolver : BaseAddressResolver
    {
        /// <summary>
        /// Gets the address of the member ComboTimer.
        /// </summary>
        public IntPtr ComboTimer { get; private set; }

        /// <summary>
        /// Gets the address of the member LastComboMove.
        /// </summary>
        public IntPtr LastComboMove => this.ComboTimer + 0x4;

        /// <summary>
        /// Gets the address of fpGetAdjustedActionId.
        /// </summary>
        public IntPtr GetAdjustedActionId { get; private set; }

        /// <summary>
        /// Gets the address of fpIsIconReplacable.
        /// </summary>
        public IntPtr IsActionIdReplaceable { get; private set; }

        /// <summary>
        /// Gets the address of fpGetActionCooldown.
        /// </summary>
        public IntPtr GetActionCooldown { get; private set; }

        /// <inheritdoc/>
        protected override void Setup64Bit(SigScanner scanner)
        {
            this.ComboTimer = scanner.GetStaticAddressFromSig("F3 0F 11 05 ?? ?? ?? ?? 48 83 C7 08");

            this.GetAdjustedActionId = scanner.ScanText("E8 ?? ?? ?? ?? 89 03 8B 03");  // Client::Game::ActionManager.GetAdjustedActionId

            this.IsActionIdReplaceable = scanner.ScanText("E8 ?? ?? ?? ?? 84 C0 74 4C 8B D3");

            PluginLog.Verbose("===== X I V C O M B O =====");
            PluginLog.Verbose($"GetAdjustedActionId   0x{this.GetAdjustedActionId:X}");
            PluginLog.Verbose($"IsActionIdReplaceable 0x{this.IsActionIdReplaceable:X}");
            PluginLog.Verbose($"ComboTimer            0x{this.ComboTimer:X}");
            PluginLog.Verbose($"LastComboMove         0x{this.LastComboMove:X}");
        }
    }
}
