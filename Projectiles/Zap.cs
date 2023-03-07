using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrarianAbilites.Buffs;

namespace TerrarianAbilites.Projectiles
{
	public class Zap : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Zap");
		}

		public override void SetDefaults() {
			Projectile.CloneDefaults(ProjectileID.MartianTurretBolt);
			Projectile.friendly = true;
			Projectile.hostile = false;
			AIType = ProjectileID.MartianTurretBolt;
		}

		public override bool PreKill(int timeLeft) {
            Projectile.type = ProjectileID.MartianTurretBolt;
			return true;
		}
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			target.AddBuff(ModContent.BuffType<Paralized>(),30);
            base.OnHitNPC(target, damage, knockback, crit);
        }

    }
}