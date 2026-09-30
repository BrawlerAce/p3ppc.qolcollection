using p3ppc.qolcollection.Configuration;
using p3ppc.qolcollection.Template;
using Reloaded.Hooks.ReloadedII.Interfaces;
using Reloaded.Mod.Interfaces;
using CriFs.V2.Hook;
using CriFs.V2.Hook.Interfaces;
using PAK.Stream.Emulator;
using PAK.Stream.Emulator.Interfaces;
using System.Diagnostics;

namespace p3ppc.qolcollection
{
    /// <summary>
    /// Your mod logic goes here.
    /// </summary>
    public class Mod : ModBase // <= Do not Remove.
    {
        /// <summary>
        /// Provides access to the mod loader API.
        /// </summary>
        private readonly IModLoader _modLoader;

        /// <summary>
        /// Provides access to the Reloaded.Hooks API.
        /// </summary>
        /// <remarks>This is null if you remove dependency on Reloaded.SharedLib.Hooks in your mod.</remarks>
        private readonly IReloadedHooks? _hooks;

        /// <summary>
        /// Provides access to the Reloaded logger.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Entry point into the mod, instance that created this class.
        /// </summary>
        private readonly IMod _owner;

        /// <summary>
        /// Provides access to this mod's configuration.
        /// </summary>
        private Config _configuration;

        /// <summary>
        /// The configuration of the currently executing mod.
        /// </summary>
        private readonly IModConfig _modConfig;

        public Mod(ModContext context)
        {
            _modLoader = context.ModLoader;
            _hooks = context.Hooks;
            _logger = context.Logger;
            _owner = context.Owner;
            _configuration = context.Configuration;
            _modConfig = context.ModConfig;

#if DEBUG
            Debugger.Launch();
#endif

            // For more information about this template, please see
            // https://reloaded-project.github.io/Reloaded-II/ModTemplate/

            // If you want to implement e.g. unload support in your mod,
            // and some other neat features, override the methods in ModBase.

            // TODO: Implement some mod logic

            var criFsController = _modLoader.GetController<ICriFsRedirectorApi>();
            if (criFsController == null || !criFsController.TryGetTarget(out var criFsApi))
            {
                _logger.WriteLine($"criFSController returned as null! p3ppc.qolcollection won't work properly :(", System.Drawing.Color.Red);
                return;
            }

            var PakEmulatorController = _modLoader.GetController<IPakEmulator>();
            if (PakEmulatorController == null || !PakEmulatorController.TryGetTarget(out var _PakEmulator))
            {
                _logger.WriteLine($"PakEmulatorController returned as null! p3ppc.qolcollection won't work properly :(", System.Drawing.Color.Red);
                return;
            }

            var modDir = _modLoader.GetDirectoryForModId(_modConfig.ModId);

            this._modLoader.OnModLoaderInitialized += () =>
            {
            };

            if (_configuration.Description == Config.DescriptionEnum.UsefulDescriptions)
            {
                //criFsApi.AddProbingPath(Path.Combine(modDir, "Descriptions/UsefulDescriptions/CriV2"));
                _PakEmulator.AddDirectory(Path.Combine(modDir, "Descriptions/UsefulDescriptions/PAK"));
            }

            else if (_configuration.Description == Config.DescriptionEnum.Maragion)
            {
                //criFsApi.AddProbingPath(Path.Combine(modDir, "Descriptions/Maragion/CriV2"));
                _PakEmulator.AddDirectory(Path.Combine(modDir, "Descriptions/Maragion/PAK"));
            }

            if (_configuration.BattleModels == Config.BattleModelEnum.P3F)
            {
                criFsApi.AddProbingPath(Path.Combine(modDir, "BattleModels/P3F/CriV2"));
                _PakEmulator.AddDirectory(Path.Combine(modDir, "BattleModels/P3F/PAK"));
            }

            else
            {
                criFsApi.AddProbingPath(Path.Combine(modDir, "BattleModels/P3P/CriV2"));
                _PakEmulator.AddDirectory(Path.Combine(modDir, "BattleModels/P3P/PAK"));
            }
        }

        #region Standard Overrides
        public override void ConfigurationUpdated(Config configuration)
        {
            // Apply settings from configuration.
            // ... your code here.
            _configuration = configuration;
            _logger.WriteLine($"[{_modConfig.ModId}] Config Updated: Applying");
        }
        #endregion

        #region For Exports, Serialization etc.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Mod() { }
#pragma warning restore CS8618
        #endregion
    }
}