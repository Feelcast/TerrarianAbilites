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
		public static ModKeybind MinorSkillKey;
		public static ModKeybind SkillTwoKey;
		public static ModKeybind SkillThreeKey;
		public static ModKeybind MajorSkillKey;
		public bool canLock;
		public bool canDemonMark;
		public  static List<String> projectileBasedMinor = new List<String> { "Fire spark", "Geode thrower" };
        public static List<String> projectileBasedTwo = new List<String> { "Cold spark", "Electric zap" };
        public static List<String> projectileBasedThree = new List<String> { "Seed blast", "Plasma bomb" };
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
					MinorSkillCD = 120;
					Player.AddBuff(ModContent.BuffType<depletedOne>(), 120);
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
                    SkillTwoCD = 300;
                    Player.AddBuff(ModContent.BuffType<depletedTwo>(), 300);
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
			MinorSkillCD --;
			SkillTwoCD --;
			SkillThreeCD --;
			MajorSkillCD --;
            base.PreUpdate();
        }

		public void MinorSkillPerform()
		{
            Vector2 shootDirection = Player.DirectionTo(Main.MouseWorld);
            if (projectileBasedMinor.Contains(MinorSkill.Name))
			{
				Terraria.Audio.SoundEngine.PlaySound(MinorSkill.UseSound, Player.Center);
				Projectile.NewProjectile(MinorSkill.GetSource_Accessory(MinorSkill), Player.Center, shootDirection*MinorSkill.shootSpeed, MinorSkill.shoot, MinorSkill.damage, MinorSkill.knockBack, Player.whoAmI, 0, 0);
			}
		}
		public void SkillTwoPerform()
		{
            Vector2 shootDirection = Player.DirectionTo(Main.MouseWorld);
            if (projectileBasedTwo.Contains(SkillTwo.Name))
            {
                Terraria.Audio.SoundEngine.PlaySound(SkillTwo.UseSound, Player.Center);
                Projectile.NewProjectile(SkillTwo.GetSource_Accessory(SkillTwo), Player.Center, shootDirection * SkillTwo.shootSpeed, SkillTwo.shoot, SkillTwo.damage, Player.whoAmI, 0, 0);
            }
        }

		public void SkillThreePerform()
		{
            Vector2 shootDirection = Player.DirectionTo(Main.MouseWorld);
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
			}

		}
    }
}
