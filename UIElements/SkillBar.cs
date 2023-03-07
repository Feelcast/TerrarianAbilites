using TerrarianAbilites.UI;
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
        public SkillTwoSlot skillTwoSlot;
        public SkillThreeSlot skillThreeSlot;
		public static bool Visible;

		public override void OnInitialize()
		{
			minorSkillSlot = new MinorSkillSlot();
			minorSkillSlot.Left.Set(600f, 0f);
			minorSkillSlot.Top.Set(20f, 0f);
			minorSkillSlot.Width.Set(20f, 0f);
			minorSkillSlot.Height.Set(20f, 0f);

            skillTwoSlot = new SkillTwoSlot();
            skillTwoSlot.Left.Set(655f, 0f);
            skillTwoSlot.Top.Set(20f, 0f);
            skillTwoSlot.Width.Set(20f, 0f);
            skillTwoSlot.Height.Set(20f, 0f);

            skillThreeSlot = new SkillThreeSlot();
			skillThreeSlot.Left.Set(710f, 0f);
            skillThreeSlot.Top.Set(20f, 0f);
            skillThreeSlot.Width.Set(20f, 0f);
            skillThreeSlot.Height.Set(20f, 0f);


            Append(minorSkillSlot);
            Append(skillTwoSlot);
            Append(skillThreeSlot);
		}

	}
}
