using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x0200006F RID: 111
public static class GameConstant
{
	// Token: 0x06000369 RID: 873 RVA: 0x00010D74 File Offset: 0x0000EF74
	public static global::GameConstant.Tag GetTag(this GameObject gameObject)
	{
		return (global::GameConstant.Tag)gameObject;
	}

	// Token: 0x0600036A RID: 874 RVA: 0x00010D7C File Offset: 0x0000EF7C
	public static global::GameConstant.Tag GetTag(this Component component)
	{
		return (global::GameConstant.Tag)component;
	}

	// Token: 0x02000070 RID: 112
	public static class Layer
	{
		// Token: 0x0400023C RID: 572
		public const int kMask_BloodSplatter = 525313;

		// Token: 0x0400023D RID: 573
		public const int kMask_BulletImpactWorld = 1840145;

		// Token: 0x0400023E RID: 574
		public const int kMask_BulletImpactCharacter = 402784256;

		// Token: 0x0400023F RID: 575
		public const int kMask_BulletImpact = 406721553;

		// Token: 0x04000240 RID: 576
		public const int kMask_BlocksSprite = 525313;

		// Token: 0x04000241 RID: 577
		public const int kMask_InfoLabel = -67174405;

		// Token: 0x04000242 RID: 578
		public const int kMask_Use = -201523205;

		// Token: 0x04000243 RID: 579
		public const int kMask_SpawnLand = 525313;

		// Token: 0x04000244 RID: 580
		public const int kMask_ClientExplosion = 134217728;

		// Token: 0x04000245 RID: 581
		public const int kMask_ServerExplosion = 271975425;

		// Token: 0x04000246 RID: 582
		public const int kMask_Deployable = -472317957;

		// Token: 0x04000247 RID: 583
		public const int kMask_WildlifeMove = -472317957;

		// Token: 0x04000248 RID: 584
		public const int kMask_PlayerMovement = 538444803;

		// Token: 0x04000249 RID: 585
		public const int kMask_PlayerPusher = 1310720;

		// Token: 0x0400024A RID: 586
		public const int kMask_Melee = 406721553;

		// Token: 0x02000071 RID: 113
		public static class Default
		{
			// Token: 0x0400024B RID: 587
			public const string name = "Default";

			// Token: 0x0400024C RID: 588
			public const int index = 0;

			// Token: 0x0400024D RID: 589
			public const int mask = 1;
		}

		// Token: 0x02000072 RID: 114
		public static class TransparentFX
		{
			// Token: 0x0400024E RID: 590
			public const string name = "TransparentFX";

			// Token: 0x0400024F RID: 591
			public const int index = 1;

			// Token: 0x04000250 RID: 592
			public const int mask = 2;
		}

		// Token: 0x02000073 RID: 115
		public static class IgnoreRaycast
		{
			// Token: 0x04000251 RID: 593
			public const string name = "Ignore Raycast";

			// Token: 0x04000252 RID: 594
			public const int index = 2;

			// Token: 0x04000253 RID: 595
			public const int mask = 4;
		}

		// Token: 0x02000074 RID: 116
		public static class Water
		{
			// Token: 0x04000254 RID: 596
			public const string name = "Water";

			// Token: 0x04000255 RID: 597
			public const int index = 4;

			// Token: 0x04000256 RID: 598
			public const int mask = 16;
		}

		// Token: 0x02000075 RID: 117
		public static class NGUILayer
		{
			// Token: 0x04000257 RID: 599
			public const string name = "NGUILayer";

			// Token: 0x04000258 RID: 600
			public const int index = 8;

			// Token: 0x04000259 RID: 601
			public const int mask = 256;
		}

		// Token: 0x02000076 RID: 118
		public static class NGUILayer2D
		{
			// Token: 0x0400025A RID: 602
			public const string name = "NGUILayer2D";

			// Token: 0x0400025B RID: 603
			public const int index = 9;

			// Token: 0x0400025C RID: 604
			public const int mask = 512;
		}

		// Token: 0x02000077 RID: 119
		public static class Static
		{
			// Token: 0x0400025D RID: 605
			public const string name = "Static";

			// Token: 0x0400025E RID: 606
			public const int index = 10;

			// Token: 0x0400025F RID: 607
			public const int mask = 1024;
		}

		// Token: 0x02000078 RID: 120
		public static class Sprite
		{
			// Token: 0x04000260 RID: 608
			public const string name = "Sprite";

			// Token: 0x04000261 RID: 609
			public const int index = 11;

			// Token: 0x04000262 RID: 610
			public const int mask = 2048;
		}

		// Token: 0x02000079 RID: 121
		public static class CullStatic
		{
			// Token: 0x04000263 RID: 611
			public const string name = "CullStatic";

			// Token: 0x04000264 RID: 612
			public const int index = 12;

			// Token: 0x04000265 RID: 613
			public const int mask = 4096;
		}

		// Token: 0x0200007A RID: 122
		public static class ViewModel
		{
			// Token: 0x04000266 RID: 614
			public const string name = "View Model";

			// Token: 0x04000267 RID: 615
			public const int index = 13;

			// Token: 0x04000268 RID: 616
			public const int mask = 8192;
		}

		// Token: 0x0200007B RID: 123
		public static class CharacterCollision
		{
			// Token: 0x04000269 RID: 617
			public const string name = "Character Collision";

			// Token: 0x0400026A RID: 618
			public const int index = 16;

			// Token: 0x0400026B RID: 619
			public const int mask = 65536;
		}

		// Token: 0x0200007C RID: 124
		public static class Hitbox
		{
			// Token: 0x0400026C RID: 620
			public const string name = "Hitbox";

			// Token: 0x0400026D RID: 621
			public const int index = 17;

			// Token: 0x0400026E RID: 622
			public const int mask = 131072;
		}

		// Token: 0x0200007D RID: 125
		public static class Debris
		{
			// Token: 0x0400026F RID: 623
			public const string name = "Debris";

			// Token: 0x04000270 RID: 624
			public const int index = 18;

			// Token: 0x04000271 RID: 625
			public const int mask = 262144;
		}

		// Token: 0x0200007E RID: 126
		public static class Terrain
		{
			// Token: 0x04000272 RID: 626
			public const string name = "Terrain";

			// Token: 0x04000273 RID: 627
			public const int index = 19;

			// Token: 0x04000274 RID: 628
			public const int mask = 524288;
		}

		// Token: 0x0200007F RID: 127
		public static class Mechanical
		{
			// Token: 0x04000275 RID: 629
			public const string name = "Mechanical";

			// Token: 0x04000276 RID: 630
			public const int index = 20;

			// Token: 0x04000277 RID: 631
			public const int mask = 1048576;
		}

		// Token: 0x02000080 RID: 128
		public static class HitOnly
		{
			// Token: 0x04000278 RID: 632
			public const string name = "HitOnly";

			// Token: 0x04000279 RID: 633
			public const int index = 21;

			// Token: 0x0400027A RID: 634
			public const int mask = 2097152;
		}

		// Token: 0x02000081 RID: 129
		public static class MeshBatched
		{
			// Token: 0x0400027B RID: 635
			public const string name = "MeshBatched";

			// Token: 0x0400027C RID: 636
			public const int index = 22;

			// Token: 0x0400027D RID: 637
			public const int mask = 4194304;
		}

		// Token: 0x02000082 RID: 130
		public static class Skybox
		{
			// Token: 0x0400027E RID: 638
			public const string name = "Skybox";

			// Token: 0x0400027F RID: 639
			public const int index = 23;

			// Token: 0x04000280 RID: 640
			public const int mask = 8388608;
		}

		// Token: 0x02000083 RID: 131
		public static class Zone
		{
			// Token: 0x04000281 RID: 641
			public const string name = "Zone";

			// Token: 0x04000282 RID: 642
			public const int index = 26;

			// Token: 0x04000283 RID: 643
			public const int mask = 67108864;
		}

		// Token: 0x02000084 RID: 132
		public static class Ragdoll
		{
			// Token: 0x04000284 RID: 644
			public const string name = "Ragdoll";

			// Token: 0x04000285 RID: 645
			public const int index = 27;

			// Token: 0x04000286 RID: 646
			public const int mask = 134217728;
		}

		// Token: 0x02000085 RID: 133
		public static class Vehicle
		{
			// Token: 0x04000287 RID: 647
			public const string name = "Vehicle";

			// Token: 0x04000288 RID: 648
			public const int index = 28;

			// Token: 0x04000289 RID: 649
			public const int mask = 268435456;
		}

		// Token: 0x02000086 RID: 134
		public static class PlayerClip
		{
			// Token: 0x0400028A RID: 650
			public const string name = "PlayerClip";

			// Token: 0x0400028B RID: 651
			public const int index = 29;

			// Token: 0x0400028C RID: 652
			public const int mask = 536870912;
		}

		// Token: 0x02000087 RID: 135
		public static class GameUI
		{
			// Token: 0x0400028D RID: 653
			public const string name = "GameUI";

			// Token: 0x0400028E RID: 654
			public const int index = 31;

			// Token: 0x0400028F RID: 655
			public const int mask = -2147483648;
		}
	}

	// Token: 0x02000088 RID: 136
	private struct TagInfo
	{
		// Token: 0x0600036B RID: 875 RVA: 0x00010D84 File Offset: 0x0000EF84
		public TagInfo(string tag, int tagNumber, bool builtin)
		{
			this.tag = tag;
			this.tagNumber = tagNumber;
			this.builtin = builtin;
			this.valid = true;
		}

		// Token: 0x04000290 RID: 656
		public readonly string tag;

		// Token: 0x04000291 RID: 657
		public readonly int tagNumber;

		// Token: 0x04000292 RID: 658
		public readonly bool builtin;

		// Token: 0x04000293 RID: 659
		public readonly bool valid;
	}

	// Token: 0x02000089 RID: 137
	public struct Tag
	{
		// Token: 0x0600036C RID: 876 RVA: 0x00010DA4 File Offset: 0x0000EFA4
		private Tag(int tagNumber)
		{
			this.tagNumber = tagNumber;
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00010DB0 File Offset: 0x0000EFB0
		static Tag()
		{
			foreach (Type type in typeof(global::GameConstant.Tag).GetNestedTypes())
			{
				FieldInfo field = type.GetField("tag", BindingFlags.Static | BindingFlags.Public);
				FieldInfo field2 = type.GetField("tagNumber", BindingFlags.Static | BindingFlags.Public);
				FieldInfo field3 = type.GetField("builtin", BindingFlags.Static | BindingFlags.Public);
				if (field != null && field2 != null && field3 != null)
				{
					try
					{
						int num = (int)field2.GetValue(null);
						string tag = (string)field.GetValue(null);
						bool builtin = (bool)field3.GetValue(null);
						global::GameConstant.Tag.Info[num] = new global::GameConstant.TagInfo(tag, num, builtin);
					}
					catch (Exception ex)
					{
						Debug.LogError(ex);
					}
				}
			}
			for (int j = 0; j < 23; j++)
			{
				int num2;
				if (!global::GameConstant.Tag.Info[j].valid)
				{
					Debug.LogWarning(string.Format("Theres no tag specified for index {0}", j));
				}
				else if (global::GameConstant.Tag.Dictionary.TryGetValue(global::GameConstant.Tag.Info[j].tag, out num2))
				{
					Debug.LogWarning(string.Format("Duplicate tag at index {0} will be overriden by predicessor at index {1}", j, num2));
				}
				else
				{
					global::GameConstant.Tag.Dictionary.Add(global::GameConstant.Tag.Info[j].tag, j);
				}
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600036E RID: 878 RVA: 0x00010F60 File Offset: 0x0000F160
		public string tag
		{
			get
			{
				return global::GameConstant.Tag.Info[this.tagNumber].tag;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600036F RID: 879 RVA: 0x00010F78 File Offset: 0x0000F178
		public bool builtin
		{
			get
			{
				return global::GameConstant.Tag.Info[this.tagNumber].builtin;
			}
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00010F90 File Offset: 0x0000F190
		public bool Contains(GameObject gameObject)
		{
			return gameObject && gameObject.CompareTag(global::GameConstant.Tag.Info[this.tagNumber].tag);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00010FBC File Offset: 0x0000F1BC
		public bool Contains(Component component)
		{
			return component && component.CompareTag(global::GameConstant.Tag.Info[this.tagNumber].tag);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00010FE8 File Offset: 0x0000F1E8
		public static int Index(GameObject gameObject)
		{
			for (int i = 0; i < 23; i++)
			{
				if (gameObject.CompareTag(global::GameConstant.Tag.Info[i].tag))
				{
					return i;
				}
			}
			throw new InvalidProgramException(string.Format("There is a tag missing in this class for \"{0}\"", gameObject.tag));
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0001103C File Offset: 0x0000F23C
		public static int Index(Component component)
		{
			GameObject gameObject = component.gameObject;
			for (int i = 0; i < 23; i++)
			{
				if (gameObject.CompareTag(global::GameConstant.Tag.Info[i].tag))
				{
					return i;
				}
			}
			throw new InvalidProgramException(string.Format("There is a tag missing in this class for \"{0}\"", gameObject.tag));
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00011098 File Offset: 0x0000F298
		public static int Index(string tag)
		{
			int result;
			if (global::GameConstant.Tag.Dictionary.TryGetValue(tag, out result))
			{
				return result;
			}
			throw new InvalidProgramException(string.Format("There is a tag missing in this class for \"{0}\"", tag));
		}

		// Token: 0x06000375 RID: 885 RVA: 0x000110CC File Offset: 0x0000F2CC
		public static explicit operator global::GameConstant.Tag(GameObject gameObject)
		{
			return new global::GameConstant.Tag(global::GameConstant.Tag.Index(gameObject));
		}

		// Token: 0x06000376 RID: 886 RVA: 0x000110DC File Offset: 0x0000F2DC
		public static explicit operator global::GameConstant.Tag(Component component)
		{
			return new global::GameConstant.Tag(global::GameConstant.Tag.Index(component));
		}

		// Token: 0x04000294 RID: 660
		private const int kBuiltinTagCount = 7;

		// Token: 0x04000295 RID: 661
		public const int kTagCount = 23;

		// Token: 0x04000296 RID: 662
		public const int kCustomTagCount = 16;

		// Token: 0x04000297 RID: 663
		public readonly int tagNumber;

		// Token: 0x04000298 RID: 664
		private static readonly global::GameConstant.TagInfo[] Info = new global::GameConstant.TagInfo[23];

		// Token: 0x04000299 RID: 665
		private static readonly Dictionary<string, int> Dictionary = new Dictionary<string, int>(23);

		// Token: 0x0200008A RID: 138
		public static class Untagged
		{
			// Token: 0x0400029A RID: 666
			public const string tag = "Untagged";

			// Token: 0x0400029B RID: 667
			public const int tagNumber = 0;

			// Token: 0x0400029C RID: 668
			public const bool builtin = true;

			// Token: 0x0400029D RID: 669
			public static readonly global::GameConstant.Tag value = new global::GameConstant.Tag(0);
		}

		// Token: 0x0200008B RID: 139
		public static class Respawn
		{
			// Token: 0x0400029E RID: 670
			public const string tag = "Respawn";

			// Token: 0x0400029F RID: 671
			public const int tagNumber = 1;

			// Token: 0x040002A0 RID: 672
			public const bool builtin = true;

			// Token: 0x040002A1 RID: 673
			public static readonly global::GameConstant.Tag value = new global::GameConstant.Tag(1);
		}

		// Token: 0x0200008C RID: 140
		public static class Finish
		{
			// Token: 0x040002A2 RID: 674
			public const string tag = "Finish";

			// Token: 0x040002A3 RID: 675
			public const int tagNumber = 2;

			// Token: 0x040002A4 RID: 676
			public const bool builtin = true;

			// Token: 0x040002A5 RID: 677
			public static readonly global::GameConstant.Tag value = new global::GameConstant.Tag(2);
		}

		// Token: 0x0200008D RID: 141
		public static class EditorOnly
		{
			// Token: 0x040002A6 RID: 678
			public const string tag = "EditorOnly";

			// Token: 0x040002A7 RID: 679
			public const int tagNumber = 3;

			// Token: 0x040002A8 RID: 680
			public const bool builtin = true;

			// Token: 0x040002A9 RID: 681
			public static readonly global::GameConstant.Tag value = new global::GameConstant.Tag(3);
		}

		// Token: 0x0200008E RID: 142
		public static class MainCamera
		{
			// Token: 0x040002AA RID: 682
			public const string tag = "MainCamera";

			// Token: 0x040002AB RID: 683
			public const int tagNumber = 4;

			// Token: 0x040002AC RID: 684
			public const bool builtin = true;

			// Token: 0x040002AD RID: 685
			public static readonly global::GameConstant.Tag value = new global::GameConstant.Tag(4);
		}

		// Token: 0x0200008F RID: 143
		public static class Player
		{
			// Token: 0x040002AE RID: 686
			public const string tag = "Player";

			// Token: 0x040002AF RID: 687
			public const int tagNumber = 5;

			// Token: 0x040002B0 RID: 688
			public const bool builtin = true;

			// Token: 0x040002B1 RID: 689
			public static readonly global::GameConstant.Tag value = new global::GameConstant.Tag(5);
		}

		// Token: 0x02000090 RID: 144
		public static class GameController
		{
			// Token: 0x040002B2 RID: 690
			public const string tag = "GameController";

			// Token: 0x040002B3 RID: 691
			public const int tagNumber = 6;

			// Token: 0x040002B4 RID: 692
			public const bool builtin = true;

			// Token: 0x040002B5 RID: 693
			public static readonly global::GameConstant.Tag value = new global::GameConstant.Tag(6);
		}

		// Token: 0x02000091 RID: 145
		public static class SkyboxCamera
		{
			// Token: 0x040002B6 RID: 694
			public const string tag = "Skybox Camera";

			// Token: 0x040002B7 RID: 695
			public const int tagNumber = 7;

			// Token: 0x040002B8 RID: 696
			public const bool builtin = false;

			// Token: 0x040002B9 RID: 697
			public static readonly global::GameConstant.Tag value = new global::GameConstant.Tag(7);
		}

		// Token: 0x02000092 RID: 146
		public static class MainTerrain
		{
			// Token: 0x040002BA RID: 698
			public const string tag = "Main Terrain";

			// Token: 0x040002BB RID: 699
			public const int tagNumber = 8;

			// Token: 0x040002BC RID: 700
			public const bool builtin = false;

			// Token: 0x040002BD RID: 701
			public static readonly global::GameConstant.Tag value = new global::GameConstant.Tag(8);
		}

		// Token: 0x02000093 RID: 147
		public static class TreeCollider
		{
			// Token: 0x040002BE RID: 702
			public const string tag = "Tree Collider";

			// Token: 0x040002BF RID: 703
			public const int tagNumber = 9;

			// Token: 0x040002C0 RID: 704
			public const bool builtin = false;

			// Token: 0x040002C1 RID: 705
			public static readonly global::GameConstant.Tag value = new global::GameConstant.Tag(9);
		}

		// Token: 0x02000094 RID: 148
		public static class Meat
		{
			// Token: 0x040002C2 RID: 706
			public const string tag = "Meat";

			// Token: 0x040002C3 RID: 707
			public const int tagNumber = 10;

			// Token: 0x040002C4 RID: 708
			public const bool builtin = false;

			// Token: 0x040002C5 RID: 709
			public static readonly global::GameConstant.Tag value = new global::GameConstant.Tag(10);
		}

		// Token: 0x02000095 RID: 149
		public static class Shelter
		{
			// Token: 0x040002C6 RID: 710
			public const string tag = "Shelter";

			// Token: 0x040002C7 RID: 711
			public const int tagNumber = 11;

			// Token: 0x040002C8 RID: 712
			public const bool builtin = false;

			// Token: 0x040002C9 RID: 713
			public static readonly global::GameConstant.Tag value = new global::GameConstant.Tag(11);
		}

		// Token: 0x02000096 RID: 150
		public static class Door
		{
			// Token: 0x040002CA RID: 714
			public const string tag = "Door";

			// Token: 0x040002CB RID: 715
			public const int tagNumber = 12;

			// Token: 0x040002CC RID: 716
			public const bool builtin = false;

			// Token: 0x040002CD RID: 717
			public static readonly global::GameConstant.Tag value = new global::GameConstant.Tag(12);
		}

		// Token: 0x02000097 RID: 151
		public static class Barricade
		{
			// Token: 0x040002CE RID: 718
			public const string tag = "Barricade";

			// Token: 0x040002CF RID: 719
			public const int tagNumber = 13;

			// Token: 0x040002D0 RID: 720
			public const bool builtin = false;

			// Token: 0x040002D1 RID: 721
			public static readonly global::GameConstant.Tag value = new global::GameConstant.Tag(13);
		}

		// Token: 0x02000098 RID: 152
		public static class StorageBox
		{
			// Token: 0x040002D2 RID: 722
			public const string tag = "StorageBox";

			// Token: 0x040002D3 RID: 723
			public const int tagNumber = 14;

			// Token: 0x040002D4 RID: 724
			public const bool builtin = false;

			// Token: 0x040002D5 RID: 725
			public static readonly global::GameConstant.Tag value = new global::GameConstant.Tag(14);
		}

		// Token: 0x02000099 RID: 153
		public static class MeshBatched
		{
			// Token: 0x040002D6 RID: 726
			public const string tag = "mBC";

			// Token: 0x040002D7 RID: 727
			public const int tagNumber = 15;

			// Token: 0x040002D8 RID: 728
			public const bool builtin = false;

			// Token: 0x040002D9 RID: 729
			public static readonly global::GameConstant.Tag value = new global::GameConstant.Tag(15);
		}

		// Token: 0x0200009A RID: 154
		public static class RPOSCamera
		{
			// Token: 0x040002DA RID: 730
			public const string tag = "RPOS Camera";

			// Token: 0x040002DB RID: 731
			public const int tagNumber = 16;

			// Token: 0x040002DC RID: 732
			public const bool builtin = false;
		}

		// Token: 0x0200009B RID: 155
		public static class FPGrass
		{
			// Token: 0x040002DD RID: 733
			public const string tag = "FPGrass";

			// Token: 0x040002DE RID: 734
			public const int tagNumber = 17;

			// Token: 0x040002DF RID: 735
			public const bool builtin = false;

			// Token: 0x040002E0 RID: 736
			public static readonly global::GameConstant.Tag value = new global::GameConstant.Tag(17);
		}

		// Token: 0x0200009C RID: 156
		public static class ServerOnly
		{
			// Token: 0x040002E1 RID: 737
			public const string tag = "Server Only";

			// Token: 0x040002E2 RID: 738
			public const int tagNumber = 18;

			// Token: 0x040002E3 RID: 739
			public const bool builtin = false;

			// Token: 0x040002E4 RID: 740
			public static readonly global::GameConstant.Tag value = new global::GameConstant.Tag(18);
		}

		// Token: 0x0200009D RID: 157
		public static class ClientOnly
		{
			// Token: 0x040002E5 RID: 741
			public const string tag = "RPOS Camera";

			// Token: 0x040002E6 RID: 742
			public const int tagNumber = 19;

			// Token: 0x040002E7 RID: 743
			public const bool builtin = false;

			// Token: 0x040002E8 RID: 744
			public static readonly global::GameConstant.Tag value = new global::GameConstant.Tag(19);
		}

		// Token: 0x0200009E RID: 158
		public static class Folder
		{
			// Token: 0x040002E9 RID: 745
			public const string tag = "Folder";

			// Token: 0x040002EA RID: 746
			public const int tagNumber = 20;

			// Token: 0x040002EB RID: 747
			public const bool builtin = false;

			// Token: 0x040002EC RID: 748
			public static readonly global::GameConstant.Tag value = new global::GameConstant.Tag(20);
		}

		// Token: 0x0200009F RID: 159
		public static class ServerFolder
		{
			// Token: 0x040002ED RID: 749
			public const string tag = "Server Folder";

			// Token: 0x040002EE RID: 750
			public const int tagNumber = 21;

			// Token: 0x040002EF RID: 751
			public const bool builtin = false;

			// Token: 0x040002F0 RID: 752
			public static readonly global::GameConstant.Tag value = new global::GameConstant.Tag(21);
		}

		// Token: 0x020000A0 RID: 160
		public static class ClientFolder
		{
			// Token: 0x040002F1 RID: 753
			public const string tag = "Client Folder";

			// Token: 0x040002F2 RID: 754
			public const int tagNumber = 22;

			// Token: 0x040002F3 RID: 755
			public const bool builtin = false;

			// Token: 0x040002F4 RID: 756
			public static readonly global::GameConstant.Tag value = new global::GameConstant.Tag(22);
		}
	}
}
