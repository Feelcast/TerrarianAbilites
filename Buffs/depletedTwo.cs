using Terraria;
using Terraria.ModLoader;

namespace TerrarianAbilites.Buffs
{
    public class depletedTwo : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Depleted II");
            Description.SetDefault("Skill two recharging");
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
            Terraria.ID.BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
        }
    }
}