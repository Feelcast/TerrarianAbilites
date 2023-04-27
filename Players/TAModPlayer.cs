using IL.Terraria.Audio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Steamworks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using TerrarianAbilites.Buffs;

namespace TerrarianAbilites
{
	// ModPlayer classes provide a way to attach data to Players and act on that data. ExamplePlayer has a lot of functionality related to 
	// several effects and items in ExampleMod. See SimpleModPlayer for a very simple example of how ModPlayer classes work.
	public class TAModPlayer : ModPlayer
	{

		public Item MinorSkill;
		public Item SkillTwo;
		public Item SkillThree;
		public Item MajorSkill;
		public int MinorSkillCD;
		public int SkillTwoCD;
		public int SkillThreeCD;
		public int MajorSkillCD;
		public int corruptedAreaTimer;
        public int genCounterOne;
		public static ModKeybind MinorSkillKey;
		public static ModKeybind SkillTwoKey;
		public static ModKeybind SkillThreeKey;
		public static ModKeybind MajorSkillKey;
		public bool canLock;
		public bool canDemonMark;
		public bool corruptedAuraOne;
        public bool usingNecromancy;
		public  static List<String> projectileBasedMinor = new List<String> { "Fire spark", "Geode thrower" };
        public static List<String> projectileBasedTwo = new List<String> { "Cold spark", "Electric zap" };
        public static List<String> projectileBasedThree = new List<String> { "Seed blast", "Plasma bomb", "Light anomaly" };
        public static Texture2D AuraArea;

        public override void ResetEffects() {
			
		}

        public override void Initialize()
        {
			MinorSkillKey = KeybindLoader.RegisterKeybind(Mod, "Minor skill", "F");
			SkillTwoKey = KeybindLoader.RegisterKeybind(Mod, "Skill two", "G");
			SkillThreeKey = KeybindLoader.RegisterKeybind(Mod, "Skill three", "V");
            MajorSkillKey = KeybindLoader.RegisterKeybind(Mod, "Major skill", "B");
			MinorSkillCD = 0;
			SkillTwoCD = 0;
			SkillThreeCD = 0;
			MajorSkillCD = 0;
            corruptedAreaTimer = 0;
            genCounterOne = 0;
            AuraArea = ModContent.Request<Texture2D>("TerrarianAbilites/Sprites/corruptedAuraArea").Value;

			
            base.Initialize();
        }
        public override void OnEnterWorld(Player player) {
			// We can refresh UI using OnEnterWorld. OnEnterWorld happens after Load, so nonStopParty is the correct value.
		}

		// In MP, other clients need accurate information about your player or else bugs happen.
		// clientClone, SyncPlayer, and SendClientChanges, ensure that information is correct.
		// We only need to do this for data that is changed by code not executed by all clients, 
		// or data that needs to be shared while joining a world.
		// For example, examplePet doesn't need to be synced because all clients know that the player is wearing the ExamplePet item in an equipment slot. 
		// The examplePet bool is set for that player on every clients computer independently (via the Buff.Update), keeping that data in sync.
		// ExampleLifeFruits, however might be out of sync. For example, when joining a server, we need to share the exampleLifeFruits variable with all other clients.
		// In addition, in ExampleUI we have a button that toggles "Non-Stop Party". We need to sync this whenever it changes.
		public override void clientClone(ModPlayer clientClone) {
		}

		public override void SyncPlayer(int toWho, int fromWho, bool newPlayer) {
		}

		public override void SendClientChanges(ModPlayer clientPlayer) {
		}

		public override void UpdateDead() {
		}

		public override void UpdateBadLifeRegen() {
		}

		public override void ProcessTriggers(TriggersSet triggersSet) {
		}

		public override void PreUpdateBuffs() {
		}


		public override void PostUpdateBuffs() {
		}


		public override void PostUpdateEquips() {
		}

		public override void FrameEffects() {
           
        }

        public override void OnConsumeMana(Item item, int manaConsumed) {
		}

        public override void PreUpdate()
        {
			if(MinorSkillKey.JustPressed && MinorSkill != null)
			{
				if (MinorSkillCD <= 0)
				{
                    MinorSkillPerform();				
                }
                else
				{
                    Terraria.Audio.SoundEngine.PlaySound(SoundID.Duck, Player.Center);
                }
            }
			if(SkillTwoKey.JustReleased && SkillTwo != null)
			{
                if (SkillTwoCD <= 0)
                {
                    SkillTwoPerform();
                }
                else
                {
                    Terraria.Audio.SoundEngine.PlaySound(SoundID.Duck, Player.Center);
                }
			}
			if(SkillThreeKey.JustReleased && SkillThree != null)
			{
                if (SkillThreeCD <= 0)
                {
                    SkillThreePerform();
                    SkillThreeCD = 900;
                    Player.AddBuff(ModContent.BuffType<depletedThree>(), 900);
                }
                else
                {
                    Terraria.Audio.SoundEngine.PlaySound(SoundID.Duck, Player.Center);
                }
			}
			if(MajorSkillKey.JustReleased && MajorSkill != null)
			{
                if (MajorSkillCD <= 0)
                {
                    MajorSkillPerform();
                    MajorSkillCD = 3000;
                    Player.AddBuff(ModContent.BuffType<depletedFour>(), 3000);
                }
                else
                {
                    Terraria.Audio.SoundEngine.PlaySound(SoundID.Duck, Player.Center);
                }
			}
			if (corruptedAreaTimer <= 0)
			{
				corruptedAuraOne = false;
			}
			MinorSkillCD --;
			SkillTwoCD --;
			SkillThreeCD --;
			MajorSkillCD --;
			corruptedAreaTimer --;
            base.PreUpdate();
        }
        public override void PostUpdate()
        {
            base.PostUpdate();
        }

        public void MinorSkillPerform()
		{
            Vector2 shootDirection = Player.DirectionTo(Main.MouseWorld);
            if (projectileBasedMinor.Contains(MinorSkill.Name))
			{
				Terraria.Audio.SoundEngine.PlaySound(MinorSkill.UseSound, Player.Center);
				Projectile.NewProjectile(MinorSkill.GetSource_Accessory(MinorSkill), Player.Center, shootDirection*MinorSkill.shootSpeed, MinorSkill.shoot, MinorSkill.damage, MinorSkill.knockBack, Player.whoAmI, 0, 0);
                MinorSkillCD = 120;
                Player.AddBuff(ModContent.BuffType<depletedOne>(), 120);
            }
		}
		public void SkillTwoPerform()
		{
            Vector2 shootDirection = Player.DirectionTo(Main.MouseWorld);
            if (projectileBasedTwo.Contains(SkillTwo.Name))
            {
                Terraria.Audio.SoundEngine.PlaySound(SkillTwo.UseSound, Player.Center);
                Projectile.NewProjectile(SkillTwo.GetSource_Accessory(SkillTwo), Player.Center, shootDirection * SkillTwo.shootSpeed, SkillTwo.shoot, SkillTwo.damage, Player.whoAmI, 0, 0);
                SkillTwoCD = 300;
                Player.AddBuff(ModContent.BuffType<depletedTwo>(), 300);
            }
            else
            {
                switch(SkillTwo.Name)
                {
                    case "Air slash":
                        Terraria.Audio.SoundEngine.PlaySound(SkillTwo.UseSound, Player.Center);
                        if (Math.Abs(shootDirection.AngleFrom(Vector2.UnitY)) <=0.5f || Math.Abs(shootDirection.AngleTo(Vector2.UnitY)) <= 0.5f)
                        {
                            Player.velocity -= shootDirection * 16f;
                        }
                        Projectile.NewProjectile(SkillTwo.GetSource_Accessory(SkillTwo), Player.Center, shootDirection * SkillTwo.shootSpeed, SkillTwo.shoot, SkillTwo.damage, Player.whoAmI, 0, 0);
                        SkillTwoCD = 300;
                        Player.AddBuff(ModContent.BuffType<depletedTwo>(), 300);
                        break;
                }
            }
        }

		public void SkillThreePerform()
		{
            Vector2 shootDirection = Player.DirectionTo(Main.MouseWorld);
            if (projectileBasedThree.Contains(SkillThree.Name))
            {
                Terraria.Audio.SoundEngine.PlaySound(SkillThree.UseSound, Player.Center);
                Projectile.NewProjectile(SkillThree.GetSource_Accessory(SkillThree), Player.Center, shootDirection * SkillThree.shootSpeed, SkillThree.shoot, SkillThree.damage, Player.whoAmI, 0, 0);            
            }
			else
			{
                switch (SkillThree.Name)
                {
                    case "Demon mark":
                        Terraria.Audio.SoundEngine.PlaySound(SkillThree.UseSound, Player.Center);
                        canDemonMark = true;
                        break;
                    case "Bullseye lock":
                        Terraria.Audio.SoundEngine.PlaySound(SkillThree.UseSound, Player.Center);
                        canLock = true;
                        break;
                }

            }
        }

		public void MajorSkillPerform()
		{
            Vector2 shootDirection = Player.DirectionTo(Main.MouseWorld);
            switch (MajorSkill.Name)
			{
				case "Monster dash":
                    Terraria.Audio.SoundEngine.PlaySound(MajorSkill.UseSound, Player.Center);
					Player.velocity = shootDirection * MajorSkill.shootSpeed;
                    Projectile.NewProjectile(MajorSkill.GetSource_Accessory(MajorSkill), new Vector2(Player.Center.X+4f,Player.Center.Y), shootDirection * MajorSkill.shootSpeed, MajorSkill.shoot, MajorSkill.damage, MajorSkill.knockBack, Player.whoAmI, 0, 0);
                    break;
				case "Corrupted aura":
                    Terraria.Audio.SoundEngine.PlaySound(MajorSkill.UseSound, Player.Center);
					Player.AddBuff(BuffID.Endurance, 900);
					Player.AddBuff(BuffID.Wrath, 900);
					corruptedAuraOne = true;
					corruptedAreaTimer = 900;
                    break;
				case "Vital sphere":
                    Terraria.Audio.SoundEngine.PlaySound(MajorSkill.UseSound, Player.Center);
                    Terraria.Audio.SoundEngine.PlaySound(SoundID.Item72, Player.Center);
                    Projectile.NewProjectile(MajorSkill.GetSource_Accessory(MajorSkill), new Vector2(Player.Center.X + 4f, Player.Center.Y), shootDirection * MajorSkill.shootSpeed, MajorSkill.shoot, MajorSkill.damage, MajorSkill.knockBack, Player.whoAmI, 0, 0);
                    break;
                case "Final spark":
                    Terraria.Audio.SoundEngine.PlaySound(MajorSkill.UseSound, Player.Center);
                    Terraria.Audio.SoundEngine.PlaySound(SoundID.Item72, Player.Center);
                    Projectile.NewProjectile(MajorSkill.GetSource_Accessory(MajorSkill), new Vector2(Player.Center.X + 4f, Player.Center.Y), shootDirection * MajorSkill.shootSpeed, MajorSkill.shoot, MajorSkill.damage, MajorSkill.knockBack, Player.whoAmI, 0, 0);
                    break;
                case "Necromancy I":
                    usingNecromancy = true;
                    break;
            }

		}

        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
			if (corruptedAuraOne)
			{
                Rectangle frame = new Rectangle(0, 0, AuraArea.Width, AuraArea.Height);
                Vector2 origin = new Vector2(AuraArea.Width * 0.5f, AuraArea.Height * 0.5f);
                DrawData value = new DrawData(AuraArea, Player.Center - Main.screenPosition, frame, Color.MediumPurple, 0, origin, 1f, drawInfo.playerEffect, 0);
				drawInfo.DrawDataCache.Add(value);
            }

            base.DrawEffects(drawInfo, ref r, ref g, ref b, ref a, ref fullBright);
        }

        public override void SaveData(TagCompound tag)
        {
            tag["MinorSkill"] = MinorSkill;
            tag["SkillTwo"] = SkillTwo;
            tag["SkillThree"] = SkillThree;
            tag["MajorSkill"] = MajorSkill;
            base.SaveData(tag);

        }
        public override void LoadData(TagCompound tag)
        {
            MinorSkill = tag.Get<Item>("MinorSkill");
            SkillTwo = tag.Get<Item>("SkillTwo");
            SkillThree = tag.Get<Item>("SkillThree");
            MajorSkill = tag.Get<Item>("MajorSkill");
        }

        public override void PostUpdateMiscEffects()
        {
            if (usingNecromancy)
            {
                if (genCounterOne < 60)
                {
                    if (genCounterOne % 6 == 0)
                    {
                        Random thread = new Random();
                        float rndX = thread.NextSingle();
                        float rndY = thread.NextSingle();
                        Vector2 randomDir = new Vector2(rndX - 0.5f, rndY - 0.5f);
                        Vector2 norDir = Vector2.Normalize(randomDir);
                        Vector2 shootDirection = Player.DirectionTo(Main.MouseWorld);
                        Terraria.Audio.SoundEngine.PlaySound(MajorSkill.UseSound, Player.Center);
                        Projectile.NewProjectile(MajorSkill.GetSource_Accessory(MajorSkill), new Vector2(Player.Center.X + 4f, Player.Center.Y), shootDirection * MajorSkill.shootSpeed, MajorSkill.shoot, MajorSkill.damage, MajorSkill.knockBack, Player.whoAmI, 0, 0);
                        Projectile.NewProjectile(MajorSkill.GetSource_Accessory(MajorSkill), new Vector2(Player.Center.X + 4f, Player.Center.Y), norDir * 16f, ProjectileID.InsanityShadowFriendly, MajorSkill.damage, MajorSkill.knockBack, Player.whoAmI, 0, 0);
                    }
                    genCounterOne++;
                }
                else
                {
                    genCounterOne = 0;
                    usingNecromancy = false;
                }

            }
            base.PostUpdateMiscEffects(); 
        }

    }
}
