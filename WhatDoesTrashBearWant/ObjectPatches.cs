using StardewValley;
using StardewModdingAPI;

namespace WhatDoesTrashBearWant
{
    internal class ObjectPatches
    {
        // initialized by ModEntry.cs
        public static ModEntry ModInstance;

        public static bool fromCheckAction = false;

        // detect interaction with Trash Bear
        public static bool TrashBear_checkAction_Prefix(Farmer who, GameLocation l)
        {
            ModInstance.Monitor.Log($"[What Does Trash Bear Want] {who.Name} interacted with Trash Bear", LogLevel.Trace);
            fromCheckAction = true;
            return true; // run base game function normally
        }

        // if interaction with Trash Bear caused desired item to be refreshed, then react to it
        public static void TrashBear_updateItemWanted_Postfix(StardewValley.Characters.TrashBear __instance)
        {
            ModInstance.Monitor.Log("[What Does Trash Bear Want] Trash Bear's desired item was refreshed", LogLevel.Trace);
            if (fromCheckAction)
            {
                var itemWanted = ItemRegistry.GetDataOrErrorItem(__instance.itemWantedIndex);
                ModInstance.Monitor.Log($"[What Does Trash Bear Want] Trash Bear wants {itemWanted.DisplayName}", LogLevel.Debug);
                Game1.showGlobalMessage(string.Format(
                    ModInstance.Helper.Translation.Get("Message"),
                    itemWanted.DisplayName
                ));
            }
            fromCheckAction = false;
        }
    }
}
