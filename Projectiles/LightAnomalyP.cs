using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrarianAbilites.Projectiles
{
	public class LightAnomalyP : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Light Anomaly");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
		}

		public override void SetDefaults() {
			Projectile.width = 54;               //The width of projectile hitbox
			Projectile.height = 54;              //The height of projectile hitbox
			Projectile.aiStyle = 1;             //The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = true;         //Can the projectile deal damage to enemies?
			Projectile.hostile = false;         //Can the projectile deal damage to the player?
												//Projectile.ranged = true;           //Is the projectile shoot by a ranged weapon?
			Projectile.penetrate = 10;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			Projectile.timeLeft = 180;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 255;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
												//Projectile.light = 0.5f;            //How much light emit around the projectile
			Projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false;          //Can the projectile collide with tiles?
			Projectile.extraUpdates = 1;            //Set to above 0 if you want the projectile to update multiple time in a frame
			AIType = ProjectileID.Bullet;           //Act exactly like default Bullet
		}

        public override void AI()
        {
			Vector2 direction = Vector2.Normalize(Projectile.velocity);
			if (Projectile.velocity.Length() > 0)
			{
                Projectile.velocity -= direction * 0.3f;
            }
			else
			{
				Projectile.velocity = Vector2.Zero;
			}
            Lighting.AddLight(Projectile.Center, 0.7f, 0.7f, 0.7f);
            base.AI();
        }
        /*
		public override bool OnTileCollide(Vector2 oldVelocity) {
            //If collide with tile, reduce the penetrate.
            //So the projectile can reflect at most 5 times
            Projectile.penetrate--;
			if (Projectile.penetrate <= 0) {
                Projectile.Kill();
			}
			else {
				Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
				//Main.PlaySound(SoundID.Item10, Projectile.position);
				if (Projectile.velocity.X != oldVelocity.X) {
                    Projectile.velocity.X = -oldVelocity.X;
				}
				if (Projectile.velocity.Y != oldVelocity.Y) {
                    Projectile.velocity.Y = -oldVelocity.Y;
				}
			}
			return false;
		}
		*/
        /*
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor) {
			//Redraw the projectile with the color not influenced by light
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++) {
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}
		*/
        public override void Kill(int timeLeft)
		{
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item4, Projectile.Center);
			Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center, Vector2.Zero, ProjectileID.Electrosphere, 40, 10, Main.myPlayer);
            // This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
            //Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
        }
    }
}
