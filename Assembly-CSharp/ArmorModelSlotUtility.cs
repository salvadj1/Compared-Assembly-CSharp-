using System;
using System.Collections.Generic;

// Token: 0x020005CD RID: 1485
public static class ArmorModelSlotUtility
{
	// Token: 0x06002F8A RID: 12170 RVA: 0x000B733C File Offset: 0x000B553C
	public static global::ArmorModelSlotMask ToMask(this global::ArmorModelSlot slot)
	{
		return (global::ArmorModelSlotMask)(1 << (int)slot & 15);
	}

	// Token: 0x06002F8B RID: 12171 RVA: 0x000B7348 File Offset: 0x000B5548
	public static global::ArmorModelSlotMask ToNotMask(this global::ArmorModelSlot slot)
	{
		return (global::ArmorModelSlotMask)(~(1 << (int)slot) & 15);
	}

	// Token: 0x06002F8C RID: 12172 RVA: 0x000B7354 File Offset: 0x000B5554
	public static bool Contains(this global::ArmorModelSlotMask slotMask, global::ArmorModelSlot slot)
	{
		return slot < (global::ArmorModelSlot)4 && (slotMask & (global::ArmorModelSlotMask)(1 << (int)slot)) != (global::ArmorModelSlotMask)0;
	}

	// Token: 0x06002F8D RID: 12173 RVA: 0x000B7370 File Offset: 0x000B5570
	public static bool Contains(this global::ArmorModelSlot slot, global::ArmorModelSlotMask slotMask)
	{
		return slot < (global::ArmorModelSlot)4 && (slotMask & (global::ArmorModelSlotMask)(1 << (int)slot)) != (global::ArmorModelSlotMask)0;
	}

	// Token: 0x06002F8E RID: 12174 RVA: 0x000B738C File Offset: 0x000B558C
	public static global::ArmorModelSlot[] ToArray(this global::ArmorModelSlotMask slotMask)
	{
		global::ArmorModelSlot[] array = global::ArmorModelSlotUtility.Mask2SlotArray.FlagToSlotArray[(int)(slotMask & (global::ArmorModelSlotMask.Feet | global::ArmorModelSlotMask.Legs | global::ArmorModelSlotMask.Torso | global::ArmorModelSlotMask.Head))];
		global::ArmorModelSlot[] array2 = new global::ArmorModelSlot[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array2[i] = array[i];
		}
		return array2;
	}

	// Token: 0x06002F8F RID: 12175 RVA: 0x000B73C8 File Offset: 0x000B55C8
	public static global::ArmorModelSlot[] EnumerateSlots(this global::ArmorModelSlotMask slotMask)
	{
		return global::ArmorModelSlotUtility.Mask2SlotArray.FlagToSlotArray[(int)(slotMask & (global::ArmorModelSlotMask.Feet | global::ArmorModelSlotMask.Legs | global::ArmorModelSlotMask.Torso | global::ArmorModelSlotMask.Head))];
	}

	// Token: 0x06002F90 RID: 12176 RVA: 0x000B73D4 File Offset: 0x000B55D4
	public static int GetMaskedSlotCount(this global::ArmorModelSlotMask slotMask)
	{
		uint num = (uint)(slotMask & (global::ArmorModelSlotMask.Feet | global::ArmorModelSlotMask.Legs | global::ArmorModelSlotMask.Torso | global::ArmorModelSlotMask.Head));
		int num2 = 0;
		while (num != 0u)
		{
			num2++;
			num &= num - 1u;
		}
		return num2;
	}

	// Token: 0x06002F91 RID: 12177 RVA: 0x000B7400 File Offset: 0x000B5600
	public static int GetUnmaskedSlotCount(this global::ArmorModelSlotMask slotMask)
	{
		uint num = (uint)(~slotMask & (global::ArmorModelSlotMask.Feet | global::ArmorModelSlotMask.Legs | global::ArmorModelSlotMask.Torso | global::ArmorModelSlotMask.Head));
		int num2 = 0;
		while (num != 0u)
		{
			num2++;
			num &= num - 1u;
		}
		return num2;
	}

	// Token: 0x06002F92 RID: 12178 RVA: 0x000B742C File Offset: 0x000B562C
	public static int GetMaskedSlotCount(this global::ArmorModelSlot slot)
	{
		return (slot >= (global::ArmorModelSlot)4) ? 0 : 1;
	}

	// Token: 0x06002F93 RID: 12179 RVA: 0x000B743C File Offset: 0x000B563C
	public static int GetUnmaskedSlotCount(this global::ArmorModelSlot slot)
	{
		return (slot >= (global::ArmorModelSlot)4) ? 4 : 3;
	}

	// Token: 0x06002F94 RID: 12180 RVA: 0x000B744C File Offset: 0x000B564C
	public static string GetRendererName(this global::ArmorModelSlot slot)
	{
		return (slot >= (global::ArmorModelSlot)4) ? "Armor Renderer" : global::ArmorModelSlotUtility.RendererNames.Array[(int)slot];
	}

	// Token: 0x06002F95 RID: 12181 RVA: 0x000B7468 File Offset: 0x000B5668
	public static Type GetArmorModelType(this global::ArmorModelSlot slot)
	{
		return (slot >= (global::ArmorModelSlot)4) ? null : global::ArmorModelSlotUtility.ClassToArmorModelSlot.ArmorModelSlotToType[slot];
	}

	// Token: 0x06002F96 RID: 12182 RVA: 0x000B7484 File Offset: 0x000B5684
	public static global::ArmorModelSlot GetArmorModelSlotForClass<T>() where T : global::ArmorModel, new()
	{
		return global::ArmorModelSlotUtility.ClassToArmorModelSlot<T>.ArmorModelSlot;
	}

	// Token: 0x040019BA RID: 6586
	public const int Count = 4;

	// Token: 0x040019BB RID: 6587
	public const global::ArmorModelSlotMask All = global::ArmorModelSlotMask.Feet | global::ArmorModelSlotMask.Legs | global::ArmorModelSlotMask.Torso | global::ArmorModelSlotMask.Head;

	// Token: 0x040019BC RID: 6588
	public const global::ArmorModelSlot Last = global::ArmorModelSlot.Head;

	// Token: 0x040019BD RID: 6589
	public const global::ArmorModelSlot First = global::ArmorModelSlot.Feet;

	// Token: 0x040019BE RID: 6590
	public const global::ArmorModelSlotMask None = (global::ArmorModelSlotMask)0;

	// Token: 0x040019BF RID: 6591
	public const global::ArmorModelSlot Begin = global::ArmorModelSlot.Feet;

	// Token: 0x040019C0 RID: 6592
	public const global::ArmorModelSlot End = (global::ArmorModelSlot)4;

	// Token: 0x020005CE RID: 1486
	private static class RendererNames
	{
		// Token: 0x06002F97 RID: 12183 RVA: 0x000B748C File Offset: 0x000B568C
		static RendererNames()
		{
			for (global::ArmorModelSlot armorModelSlot = global::ArmorModelSlot.Feet; armorModelSlot < (global::ArmorModelSlot)4; armorModelSlot += 1)
			{
				global::ArmorModelSlotUtility.RendererNames.Array[(int)armorModelSlot] = string.Format("{0} Renderer", armorModelSlot);
			}
		}

		// Token: 0x040019C1 RID: 6593
		public static readonly string[] Array = new string[4];
	}

	// Token: 0x020005CF RID: 1487
	private static class Mask2SlotArray
	{
		// Token: 0x06002F98 RID: 12184 RVA: 0x000B74D0 File Offset: 0x000B56D0
		static Mask2SlotArray()
		{
			for (int i = 0; i <= 15; i++)
			{
				int num = 0;
				for (int j = 0; j < 4; j++)
				{
					if ((i & 1 << j) == 1 << j)
					{
						num++;
					}
				}
				global::ArmorModelSlotUtility.Mask2SlotArray.FlagToSlotArray[i] = new global::ArmorModelSlot[num];
				int num2 = 0;
				for (int k = 0; k < 4; k++)
				{
					if ((i & 1 << k) == 1 << k)
					{
						global::ArmorModelSlotUtility.Mask2SlotArray.FlagToSlotArray[i][num2++] = (global::ArmorModelSlot)k;
					}
				}
			}
		}

		// Token: 0x040019C2 RID: 6594
		public static readonly global::ArmorModelSlot[][] FlagToSlotArray = new global::ArmorModelSlot[16][];
	}

	// Token: 0x020005D0 RID: 1488
	private static class ClassToArmorModelSlot
	{
		// Token: 0x06002F99 RID: 12185 RVA: 0x000B7574 File Offset: 0x000B5774
		static ClassToArmorModelSlot()
		{
			List<Type> list = new List<Type>();
			foreach (Type type in typeof(global::ArmorModelSlotUtility.ClassToArmorModelSlot).Assembly.GetTypes())
			{
				if (type.IsSubclassOf(typeof(global::ArmorModel)) && !type.IsAbstract && type.IsDefined(typeof(global::ArmorModelSlotClassAttribute), false))
				{
					list.Add(type);
				}
			}
			global::ArmorModelSlotUtility.ClassToArmorModelSlot.ArmorModelSlotToType = new Dictionary<global::ArmorModelSlot, Type>(list.Count);
			foreach (Type type2 in list)
			{
				global::ArmorModelSlotClassAttribute armorModelSlotClassAttribute = (global::ArmorModelSlotClassAttribute)Attribute.GetCustomAttribute(type2, typeof(global::ArmorModelSlotClassAttribute));
				global::ArmorModelSlotUtility.ClassToArmorModelSlot.ArmorModelSlotToType.Add(armorModelSlotClassAttribute.ArmorModelSlot, type2);
			}
		}

		// Token: 0x040019C3 RID: 6595
		public static readonly Dictionary<global::ArmorModelSlot, Type> ArmorModelSlotToType;
	}

	// Token: 0x020005D1 RID: 1489
	private static class ClassToArmorModelSlot<T> where T : global::ArmorModel, new()
	{
		// Token: 0x040019C4 RID: 6596
		public static readonly global::ArmorModelSlot ArmorModelSlot = ((global::ArmorModelSlotClassAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(global::ArmorModelSlotClassAttribute))).ArmorModelSlot;
	}
}
