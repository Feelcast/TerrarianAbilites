using IL.Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrarianAbilites.NPCs
{
	public class TAGlobalNPC : GlobalNPC
	{
        public override bool InstancePerEntity => true;
        public bool locked;
		public int markCount;
		public bool stunned;
		public bool slowed;
		public static Texture2D crosshair;
        public override void ResetEffects(NPC npc) {
			stunned = false;
			slowed = false;
		}

		public override void SetDefaults(NPC npc) {
			markCount = 0;
			locked = false;
			crosshair = ModContent.Request<Texture2D>("TerrarianAbilites/Sprites/Lock").Value;
		}

		public override void UpdateLifeRegen(NPC npc, ref int damage) {
			
		}


		public override void DrawEffects(NPC npc, ref Color drawColor) {
			if (markCount == 3) {
				if (Main.rand.Next(4) < 3) {
					int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, DustID.Smoke, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3.5f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
					if (Main.rand.NextBool(4)) {
						Main.dust[dust].noGravity = false;
						Main.dust[dust].scale *= 0.5f;
					}
				}
				Lighting.AddLight(npc.position, 0.1f, 0.2f, 0.7f);
			}
		}
        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
			if (locked)
			{
				Rectangle frame = new Rectangle(0,0,crosshair.Width,crosshair.Height);
				Vector2 origin = new Vector2(crosshair.Width*0.5f,crosshair.Height * 0.5f);
				Main.EntitySpriteDraw(crosshair, npc.Center - screenPos, frame, drawColor, 0, origin, 1f, SpriteEffects.None, 0);
			}
            base.PostDraw(npc, spriteBatch, screenPos, drawColor);
        }

        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns) {

		}

		public override void SetupShop(int type, Chest shop, ref int nextSlot) {
			
		}

		public override void GetChat(NPC npc, ref string chat) {
			
		}

        public override void OnHitByItem(NPC npc, Player player, Item item, int damage, float knockback, bool crit)
        {
			// put demon mark code here
            base.OnHitByItem(npc, player, item, damage, knockback, crit);
        }
        public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit)
        {
			//put bullseye lock code here
            base.OnHitByProjectile(npc, projectile, damage, knockback, crit);
        }
        public override bool StrikeNPC(NPC npc, ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
			// put bullseye lock code here
            return base.StrikeNPC(npc, ref damage, defense, ref knockback, hitDirection, ref crit);
        }
        public override void PostAI(NPC npc)
        {
			//demon mark and bullseye lock
            base.PostAI(npc);
        }
    }
}
