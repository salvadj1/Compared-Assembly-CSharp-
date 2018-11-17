using System;
using System.Collections.Generic;

// Token: 0x02000510 RID: 1296
public static class ArmorModelSlotUtility
{
	// Token: 0x06002BCA RID: 11210 RVA: 0x000AF2A0 File Offset: 0x000AD4A0
	public static ArmorModelSlotMask ToMask(this ArmorModelSlot slot)
	{
		return (ArmorModelSlotMask)(1 << (int)slot & 15);
	}

	// Token: 0x06002BCB RID: 11211 RVA: 0x000AF2AC File Offset: 0x000AD4AC
	public static ArmorModelSlotMask ToNotMask(this ArmorModelSlot slot)
	{
		return (ArmorModelSlotMask)(~(1 << (int)slot) & 15);
	}

	// Token: 0x06002BCC RID: 11212 RVA: 0x000AF2B8 File Offset: 0x000AD4B8
	public static bool Contains(this ArmorModelSlotMask slotMask, ArmorModelSlot slot)
	{
		return slot < (ArmorModelSlot)4 && (slotMask & (ArmorModelSlotMask)(1 << (int)slot)) != (ArmorModelSlotMask)0;
	}

	// Token: 0x06002BCD RID: 11213 RVA: 0x000AF2D4 File Offset: 0x000AD4D4
	public static bool Contains(this ArmorModelSlot slot, ArmorModelSlotMask slotMask)
	{
		return slot < (ArmorModelSlot)4 && (slotMask & (ArmorModelSlotMask)(1 << (int)slot)) != (ArmorModelSlotMask)0;
	}

	// Token: 0x06002BCE RID: 11214 RVA: 0x000AF2F0 File Offset: 0x000AD4F0
	public static ArmorModelSlot[] ToArray(this ArmorModelSlotMask slotMask)
	{
		ArmorModelSlot[] array = ArmorModelSlotUtility.Mask2SlotArray.FlagToSlotArray[(int)(slotMask & (ArmorModelSlotMask.Feet | ArmorModelSlotMask.Legs | ArmorModelSlotMask.Torso | ArmorModelSlotMask.Head))];
		ArmorModelSlot[] array2 = new ArmorModelSlot[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array2[i] = array[i];
		}
		return array2;
	}

	// Token: 0x06002BCF RID: 11215 RVA: 0x000AF32C File Offset: 0x000AD52C
	public static ArmorModelSlot[] EnumerateSlots(this ArmorModelSlotMask slotMask)
	{
		return ArmorModelSlotUtility.Mask2SlotArray.FlagToSlotArray[(int)(slotMask & (ArmorModelSlotMask.Feet | ArmorModelSlotMask.Legs | ArmorModelSlotMask.Torso | ArmorModelSlotMask.Head))];
	}

	// Token: 0x06002BD0 RID: 11216 RVA: 0x000AF338 File Offset: 0x000AD538
	public static int GetMaskedSlotCount(this ArmorModelSlotMask slotMask)
	{
		uint num = (uint)(slotMask & (ArmorModelSlotMask.Feet | ArmorModelSlotMask.Legs | ArmorModelSlotMask.Torso | ArmorModelSlotMask.Head));
		int num2 = 0;
		while (num != 0u)
		{
			num2++;
			num &= num - 1u;
		}
		return num2;
	}

	// Token: 0x06002BD1 RID: 11217 RVA: 0x000AF364 File Offset: 0x000AD564
	public static int GetUnmaskedSlotCount(this ArmorModelSlotMask slotMask)
	{
		uint num = (uint)(~slotMask & (ArmorModelSlotMask.Feet | ArmorModelSlotMask.Legs | ArmorModelSlotMask.Torso | ArmorModelSlotMask.Head));
		int num2 = 0;
		while (num != 0u)
		{
			num2++;
			num &= num - 1u;
		}
		return num2;
	}

	// Token: 0x06002BD2 RID: 11218 RVA: 0x000AF390 File Offset: 0x000AD590
	public static int GetMaskedSlotCount(this ArmorModelSlot slot)
	{
		return (slot >= (ArmorModelSlot)4) ? 0 : 1;
	}

	// Token: 0x06002BD3 RID: 11219 RVA: 0x000AF3A0 File Offset: 0x000AD5A0
	public static int GetUnmaskedSlotCount(this ArmorModelSlot slot)
	{
		return (slot >= (ArmorModelSlot)4) ? 4 : 3;
	}

	// Token: 0x06002BD4 RID: 11220 RVA: 0x000AF3B0 File Offset: 0x000AD5B0
	public static string GetRendererName(this ArmorModelSlot slot)
	{
		return (slot >= (ArmorModelSlot)4) ? "Armor Renderer" : ArmorModelSlotUtility.RendererNames.Array[(int)slot];
	}

	// Token: 0x06002BD5 RID: 11221 RVA: 0x000AF3CC File Offset: 0x000AD5CC
	public static Type GetArmorModelType(this ArmorModelSlot slot)
	{
		return (slot >= (ArmorModelSlot)4) ? null : ArmorModelSlotUtility.ClassToArmorModelSlot.ArmorModelSlotToType[slot];
	}

	// Token: 0x06002BD6 RID: 11222 RVA: 0x000AF3E8 File Offset: 0x000AD5E8
	public static ArmorModelSlot GetArmorModelSlotForClass<T>() where T : ArmorModel, new()
	{
		return ArmorModelSlotUtility.ClassToArmorModelSlot<T>.ArmorModelSlot;
	}

	// Token: 0x040017EE RID: 6126
	public const int Count = 4;

	// Token: 0x040017EF RID: 6127
	public const ArmorModelSlotMask All = ArmorModelSlotMask.Feet | ArmorModelSlotMask.Legs | ArmorModelSlotMask.Torso | ArmorModelSlotMask.Head;

	// Token: 0x040017F0 RID: 6128
	public const ArmorModelSlot Last = ArmorModelSlot.Head;

	// Token: 0x040017F1 RID: 6129
	public const ArmorModelSlot First = ArmorModelSlot.Feet;

	// Token: 0x040017F2 RID: 6130
	public const ArmorModelSlotMask None = (ArmorModelSlotMask)0;

	// Token: 0x040017F3 RID: 6131
	public const ArmorModelSlot Begin = ArmorModelSlot.Feet;

	// Token: 0x040017F4 RID: 6132
	public const ArmorModelSlot End = (ArmorModelSlot)4;

	// Token: 0x02000511 RID: 1297
	private static class RendererNames
	{
		// Token: 0x06002BD7 RID: 11223 RVA: 0x000AF3F0 File Offset: 0x000AD5F0
		static RendererNames()
		{
			for (ArmorModelSlot armorModelSlot = ArmorModelSlot.Feet; armorModelSlot < (ArmorModelSlot)4; armorModelSlot += 1)
			{
				ArmorModelSlotUtility.RendererNames.Array[(int)armorModelSlot] = string.Format("{0} Renderer", armorModelSlot);
			}
		}

		// Token: 0x040017F5 RID: 6133
		public static readonly string[] Array = new string[4];
	}

	// Token: 0x02000512 RID: 1298
	private static class Mask2SlotArray
	{
		// Token: 0x06002BD8 RID: 11224 RVA: 0x000AF434 File Offset: 0x000AD634
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
				ArmorModelSlotUtility.Mask2SlotArray.FlagToSlotArray[i] = new ArmorModelSlot[num];
				int num2 = 0;
				for (int k = 0; k < 4; k++)
				{
					if ((i & 1 << k) == 1 << k)
					{
						ArmorModelSlotUtility.Mask2SlotArray.FlagToSlotArray[i][num2++] = (ArmorModelSlot)k;
					}
				}
			}
		}

		// Token: 0x040017F6 RID: 6134
		public static readonly ArmorModelSlot[][] FlagToSlotArray = new ArmorModelSlot[16][];
	}

	// Token: 0x02000513 RID: 1299
	private static class ClassToArmorModelSlot
	{
		// Token: 0x06002BD9 RID: 11225 RVA: 0x000AF4D8 File Offset: 0x000AD6D8
		static ClassToArmorModelSlot()
		{
			List<Type> list = new List<Type>();
			foreach (Type type in typeof(ArmorModelSlotUtility.ClassToArmorModelSlot).Assembly.GetTypes())
			{
				if (type.IsSubclassOf(typeof(ArmorModel)) && !type.IsAbstract && type.IsDefined(typeof(ArmorModelSlotClassAttribute), false))
				{
					list.Add(type);
				}
			}
			ArmorModelSlotUtility.ClassToArmorModelSlot.ArmorModelSlotToType = new Dictionary<ArmorModelSlot, Type>(list.Count);
			foreach (Type type2 in list)
			{
				ArmorModelSlotClassAttribute armorModelSlotClassAttribute = (ArmorModelSlotClassAttribute)Attribute.GetCustomAttribute(type2, typeof(ArmorModelSlotClassAttribute));
				ArmorModelSlotUtility.ClassToArmorModelSlot.ArmorModelSlotToType.Add(armorModelSlotClassAttribute.ArmorModelSlot, type2);
			}
		}

		// Token: 0x040017F7 RID: 6135
		public static readonly Dictionary<ArmorModelSlot, Type> ArmorModelSlotToType;
	}

	// Token: 0x02000514 RID: 1300
	private static class ClassToArmorModelSlot<T> where T : ArmorModel, new()
	{
		// Token: 0x040017F8 RID: 6136
		public static readonly ArmorModelSlot ArmorModelSlot = ((ArmorModelSlotClassAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(ArmorModelSlotClassAttribute))).ArmorModelSlot;
	}
}
