using Terraria;
using Terraria.ModLoader;

namespace TerrarianAbilites.Buffs
{
    public class depletedThree : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Depleted III");
            Description.SetDefault("Skill three recharging");
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
            Terraria.ID.BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
        }
    }
}