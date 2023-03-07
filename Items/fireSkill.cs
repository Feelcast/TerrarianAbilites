using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrarianAbilites.Items
{
	public class fireSkill : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("sword"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("This is a basic modded sword.");
		}

		public override void SetDefaults()
		{
			Item.damage = 20;
			Item.DamageType = DamageClass.Magic;
			Item.width = 30;
			Item.height = 30;
			Item.useTime = 20;
			Item.knockBack = 6;
			Item.value = 10000;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item8;
			Item.shoot = ProjectileID.Spark;
			Item.shootSpeed = 16f;
			Item.SetNameOverride("Fire spark");
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.WandofSparking, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}