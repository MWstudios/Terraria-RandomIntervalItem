using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;

namespace RandomIntervalItem
{
    public class WorldEmptyGen : ModWorld
    {
        public int[] blocks = { //priotizing blocks for higher spawn chances
            2, 3, 5, 9, 11, 12, 13, 14, 56, 61, 116, 129, 131, 133, 134, 137, 139, 141, 143, 145, 154, 169, 170, 172, 173, 174, 176, 183, 214, 276, //1.0
            539, 577, 586, 591, 593, 594, //1.1
            604, 607, 609, 611, 612, 613, 614, 619, 620, 621, 662, 664, 699, 700, 701, 702, 717, 718, 719, 751, 765, 775, 824, 833, 834, 835, 836, 883, 911, 947, 1101, 1103, 1104, 1105, 1106, 1246, 1344, 1589, 1591, 1593, //1.2.0
            1725, 1727, 1729, 1872, 2119, 2120, 2260, 2503, 2504, 2692, 2693, 2694, 2695, 2697, //1.2.1
            2792, 2793, 2794, 2860, 3066, 3081, 3086, 3087, 3100, 3234, 3271, 3272, 3274, 3275, 3276, 3277, 3338, 3339, 3347, 3461, 3573, 3574, 3575, 3576, //1.3.0
            3521, 3633, 3634, 3635, 3636, 3637, 3754, 3755, 3756 }; //1.3.1 and newer
        public int itemsel = 0;
        public int ItemCountdown = 300;
        public Random rng = new Random();
        public override void PreWorldGen()
        {
            WorldGen.worldSurface = 100;
        }
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            while (tasks.Count>1)
            {
                tasks.RemoveAt(1); //throws out all passes except for the first one (Resetting)
            }
            tasks.Add(new PassLegacy("Finalize", delegate (GenerationProgress progress)
            {
                progress.Message = "Finalizing";
                Main.spawnTileX = (int)(Main.maxTilesX / 2);
                Main.spawnTileY = (int)(Main.maxTilesY / 8);
                WorldGen.PlaceTile(Main.spawnTileX, Main.spawnTileY+1, TileID.LihzahrdBrick);
            }));
        }
        public override void PostUpdate()
        {
            if (ItemCountdown == 0)
            {
                ItemCountdown = 300;
                for (int playerindex = 0; playerindex < 255; playerindex++)
                {
                    Player player = Main.player[playerindex];
                    if (rng.Next(0, 5) == 0)
                    {
                        itemsel = blocks[rng.Next(blocks.Length)];
                    }
                    else itemsel = rng.Next(1, 3230);
                    player.QuickSpawnItem(itemsel, 1);
                }
            }
            else ItemCountdown--;

        }
    }
}