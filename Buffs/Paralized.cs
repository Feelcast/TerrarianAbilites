using Terraria;
using Terraria.ModLoader;
using TerrarianAbilites.NPCs;

namespace TerrarianAbilites.Buffs
{
    public class Paralized : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Slowed (NPC)");
            Description.SetDefault("Movement speed reduced");
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
            Terraria.ID.BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            if (!npc.boss)
            {
                npc.GetGlobalNPC<TAGlobalNPC>().slowed = true;
            }
            base.Update(npc, ref buffIndex);
        }
    }
}