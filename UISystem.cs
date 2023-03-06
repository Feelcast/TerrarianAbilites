using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria;
using TerrarianAbilites.UI;

namespace TerrarianAbilites
{
    public class UISystem : ModSystem
    {
        internal SkillBar skillBar;
        private UserInterface _skillBar;
        public override void Load()
        {
            skillBar = new SkillBar();
            skillBar.Activate();
            _skillBar = new UserInterface();
            _skillBar.SetState(skillBar);
        }

        public override void Unload()
        {
            skillBar = null;
            base.Unload();
        }

        public override void UpdateUI(GameTime gameTime)
        {
            _skillBar?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "YourMod: A Description",
                    delegate
                    {
                        _skillBar.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
