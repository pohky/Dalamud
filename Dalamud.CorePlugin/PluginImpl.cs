using System;

using Dalamud.Game.Command;
using Dalamud.Interface.Windowing;
using Dalamud.Logging;
using Dalamud.Plugin;

namespace Dalamud.CorePlugin
{
    /// <summary>
    /// This class is a a plugin testbed for developing new Dalamud features with easy access to Dalamud itself.
    /// Be careful to not commit anything extra.
    /// </summary>
    public sealed class PluginImpl : IDalamudPlugin
    {
        private readonly WindowSystem windowSystem = new("Dalamud.CorePlugin");

        // private Localization localizationManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginImpl"/> class.
        /// </summary>
        /// <param name="pluginInterface">Dalamud plugin interface.</param>
        public PluginImpl(DalamudPluginInterface pluginInterface)
        {
            try
            {
                // this.InitLoc();
                this.Interface = pluginInterface;

                this.windowSystem.AddWindow(new PluginWindow());

                this.Interface.UiBuilder.Draw += this.OnDraw;
                this.Interface.UiBuilder.OpenConfigUi += this.OnOpenConfigUi;

                Service<CommandManager>.Get().AddHandler("/di", new(this.OnCommand) { HelpMessage = $"Access the {this.Name} plugin." });
            }
            catch (Exception ex)
            {
                PluginLog.Error(ex, "kaboom");
            }
        }

        /// <inheritdoc/>
        public string Name => "Dalamud.CorePlugin";

        /// <summary>
        /// Gets the plugin interface.
        /// </summary>
        internal DalamudPluginInterface Interface { get; private set; }

        /// <inheritdoc/>
        public void Dispose()
        {
            Service<CommandManager>.Get().RemoveHandler("/di");

            this.Interface.UiBuilder.Draw -= this.OnDraw;

            this.windowSystem.RemoveAllWindows();

            this.Interface.Dispose();
        }

        // private void InitLoc()
        // {
        //     // CheapLoc needs to be reinitialized here because it tracks the setup by assembly name. New assembly, new setup.
        //     this.localizationManager = new Localization(Path.Combine(Dalamud.Instance.AssetDirectory.FullName, "UIRes", "loc", "dalamud"), "dalamud_");
        //     if (!string.IsNullOrEmpty(Dalamud.Instance.Configuration.LanguageOverride))
        //     {
        //         this.localizationManager.SetupWithLangCode(Dalamud.Instance.Configuration.LanguageOverride);
        //     }
        //     else
        //     {
        //         this.localizationManager.SetupWithUiCulture();
        //     }
        // }

        /// <summary>
        /// Draw the window system.
        /// </summary>
        private void OnDraw()
        {
            try
            {
                this.windowSystem.Draw();
            }
            catch (Exception ex)
            {
                PluginLog.Error(ex, "Boom");
            }
        }

        private void OnCommand(string command, string args)
        {
            // this.window.IsOpen = true;
        }

        private void OnOpenConfigUi(object sender, EventArgs e)
        {
            // this.window.IsOpen = true;
        }
    }
}