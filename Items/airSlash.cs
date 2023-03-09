using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrarianAbilites.Projectiles;

namespace TerrarianAbilites.Items
{
	public class airSlash : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("sword"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("Shoots air");
		}

		public override void SetDefaults()
		{
			Item.damage = 20;
			Item.DamageType = DamageClass.Magic;
			Item.width = 30;
			Item.height = 30;
			Item.useTime = 20;
			Item.knockBack = 36;
			Item.value = 10000;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item1;
			Item.shoot = ModContent.ProjectileType<airSlashP>();
			Item.shootSpeed = 8f;
			Item.SetNameOverride("Air slash");
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Cloud, 5);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}