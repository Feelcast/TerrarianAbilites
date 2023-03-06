using ExampleMod.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace TerrarianAbilites.UI
{
	internal class SkillBar : UIState
	{
		public MinorSkillSlot minorSkillSlot;
		public static bool Visible;

		public override void OnInitialize()
		{
			minorSkillSlot = new MinorSkillSlot();
			minorSkillSlot.Left.Set(600f, 0f);
			minorSkillSlot.Top.Set(20f, 0f);
			minorSkillSlot.Width.Set(20f, 0f);
			minorSkillSlot.Height.Set(20f, 1f);

			Append(minorSkillSlot);
		}

	}
}
