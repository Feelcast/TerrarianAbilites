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
using Microsoft.Xna.Framework.Graphics;

namespace TerrarianAbilites
{
    public class UISystem : ModSystem
    {
        internal SkillBar skillBar;
        private UserInterface _skillBar;
        public static Player currentPlayer;
        TAModPlayer skillPlayer;
        //public static Texture2D AuraArea;
        public override void Load()
        {
            skillBar = new SkillBar();
            skillBar.Activate();
            _skillBar = new UserInterface();
            _skillBar.SetState(skillBar);
        }
        public override void OnModLoad()
        {
            //AuraArea = ModContent.Request<Texture2D>("TerrarianAbilites/Sprites/corruptedAuraArea").Value;
            base.OnModLoad();
        }
        public override void PostWorldGen()
        {
            currentPlayer = Main.player[Main.myPlayer];
            skillPlayer = currentPlayer.GetModPlayer<TAModPlayer>();
            skillBar.minorSkillSlot.Item = skillPlayer.MinorSkill;
            skillBar.skillTwoSlot.Item = skillPlayer.SkillTwo;
            skillBar.skillThreeSlot.Item = skillPlayer.SkillThree;
            skillBar.majorSkillSlot.Item = skillPlayer.MajorSkill;
            base.PostWorldGen();
        }
        public override void Unload()
        {
            skillBar = null;
            base.Unload();
        }
        public override void PostSetupContent()
        {            
            base.PostSetupContent();
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
            /*
            TAModPlayer skillPlayer = currentPlayer.GetModPlayer<TAModPlayer>();
            if (skillPlayer.corruptedAuraOne)
            {
                Rectangle frame = new Rectangle(0, 0, AuraArea.Width, AuraArea.Height);
                Vector2 origin = new Vector2(AuraArea.Width * 0.5f, AuraArea.Height * 0.5f);
                Main.EntitySpriteDraw(AuraArea, currentPlayer.Center, frame, default(Color), 0, origin, 1f, SpriteEffects.None, 0);
            }
            */


        }

    }
}
