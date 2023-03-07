using Terraria;
using Terraria.ModLoader;

namespace TerrarianAbilites.Buffs
{
    public class depletedOne : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Depleted I");
            Description.SetDefault("Minor skill recharging");
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
            Terraria.ID.BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
        }
    }
}