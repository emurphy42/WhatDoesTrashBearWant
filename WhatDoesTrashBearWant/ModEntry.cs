using StardewModdingAPI;
using HarmonyLib;

namespace WhatDoesTrashBearWant
{
    public class ModEntry : Mod
    {
        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            ObjectPatches.ModInstance = this;

            var harmony = new Harmony(this.ModManifest.UniqueID);
            // detect interaction with Trash Bear
            harmony.Patch(
                original: AccessTools.Method(typeof(StardewValley.Characters.TrashBear), nameof(StardewValley.Characters.TrashBear.checkAction)),
                prefix: new HarmonyMethod(typeof(ObjectPatches), nameof(ObjectPatches.TrashBear_checkAction_Prefix))
            );
            // if interaction with Trash Bear caused desired item to be refreshed, then react to it
            harmony.Patch(
                original: AccessTools.Method(typeof(StardewValley.Characters.TrashBear), nameof(StardewValley.Characters.TrashBear.updateItemWanted)),
                postfix: new HarmonyMethod(typeof(ObjectPatches), nameof(ObjectPatches.TrashBear_updateItemWanted_Postfix))
            );
        }
    }
}
