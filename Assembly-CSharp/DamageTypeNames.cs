using System;
using System.Collections.Generic;

// Token: 0x02000183 RID: 387
public static class DamageTypeNames
{
	// Token: 0x06000BA1 RID: 2977 RVA: 0x0002D970 File Offset: 0x0002BB70
	static DamageTypeNames()
	{
		global::DamageTypeNames.Strings = new string[6];
		global::DamageTypeNames.Values = new Dictionary<string, global::DamageTypeIndex>(6);
		for (global::DamageTypeIndex damageTypeIndex = global::DamageTypeIndex.damage_generic; damageTypeIndex < global::DamageTypeIndex.damage_last; damageTypeIndex++)
		{
			global::DamageTypeNames.Values.Add(global::DamageTypeNames.Strings[(int)damageTypeIndex] = damageTypeIndex.ToString().Substring("damage_".Length), damageTypeIndex);
		}
		uint num = 63u;
		global::DamageTypeNames.Mask = (global::DamageTypeFlags)num;
		global::DamageTypeNames.Flags = new string[num];
		global::DamageTypeNames.Flags[0] = "none";
		for (uint num2 = 1u; num2 < num; num2 += 1u)
		{
			uint num3 = num2;
			int i = 0;
			while (i < 6)
			{
				if ((num2 & 1u << i) == 1u << i)
				{
					string str = global::DamageTypeNames.Strings[i];
					if ((num3 &= ~(1u << i)) == 0u)
					{
						break;
					}
					while ((long)(++i) < 6L)
					{
						if ((num2 & 1u << i) == 1u << i)
						{
							str = str + "|" + global::DamageTypeNames.Strings[i];
							num3 &= ~(1u << i);
							if (num3 == 0u)
							{
								break;
							}
						}
					}
					break;
				}
				else
				{
					i++;
				}
			}
		}
	}

	// Token: 0x06000BA2 RID: 2978 RVA: 0x0002DAC8 File Offset: 0x0002BCC8
	public static bool Convert(string name, out global::DamageTypeIndex index)
	{
		return global::DamageTypeNames.Values.TryGetValue(name, out index);
	}

	// Token: 0x06000BA3 RID: 2979 RVA: 0x0002DAD8 File Offset: 0x0002BCD8
	public static bool Convert(string[] names, out global::DamageTypeFlags flags)
	{
		for (int i = 0; i < names.Length; i++)
		{
			global::DamageTypeIndex damageTypeIndex;
			if (global::DamageTypeNames.Values.TryGetValue(names[i], out damageTypeIndex))
			{
				flags = (global::DamageTypeFlags)(1 << (int)damageTypeIndex);
				while (++i < names.Length)
				{
					if (global::DamageTypeNames.Values.TryGetValue(names[i], out damageTypeIndex))
					{
						flags |= (global::DamageTypeFlags)(1 << (int)damageTypeIndex);
					}
				}
				return true;
			}
		}
		flags = (global::DamageTypeFlags)0;
		return false;
	}

	// Token: 0x06000BA4 RID: 2980 RVA: 0x0002DB4C File Offset: 0x0002BD4C
	public static bool Convert(string name, out global::DamageTypeFlags flags)
	{
		global::DamageTypeIndex damageTypeIndex;
		if (global::DamageTypeNames.Values.TryGetValue(name, out damageTypeIndex))
		{
			flags = (global::DamageTypeFlags)(1 << (int)damageTypeIndex);
			return true;
		}
		if (name.Length == 0 || name == "none")
		{
			flags = (global::DamageTypeFlags)0;
			return true;
		}
		return global::DamageTypeNames.Convert(name.Split(global::DamageTypeNames.SplitCharacters, StringSplitOptions.RemoveEmptyEntries), out flags);
	}

	// Token: 0x06000BA5 RID: 2981 RVA: 0x0002DBA8 File Offset: 0x0002BDA8
	public static bool Convert(global::DamageTypeIndex index, out global::DamageTypeFlags flags)
	{
		flags = (global::DamageTypeFlags)(1 << (int)index);
		return (flags & global::DamageTypeNames.Mask) == flags;
	}

	// Token: 0x06000BA6 RID: 2982 RVA: 0x0002DBC0 File Offset: 0x0002BDC0
	public static bool Convert(global::DamageTypeIndex index, out string name)
	{
		if (index == global::DamageTypeIndex.damage_generic || (index > global::DamageTypeIndex.damage_generic && index < global::DamageTypeIndex.damage_last))
		{
			name = global::DamageTypeNames.Strings[(int)index];
			return true;
		}
		name = null;
		return false;
	}

	// Token: 0x040007DF RID: 2015
	private static readonly string[] Strings;

	// Token: 0x040007E0 RID: 2016
	private static readonly string[] Flags;

	// Token: 0x040007E1 RID: 2017
	private static readonly Dictionary<string, global::DamageTypeIndex> Values;

	// Token: 0x040007E2 RID: 2018
	private static readonly global::DamageTypeFlags Mask;

	// Token: 0x040007E3 RID: 2019
	private static readonly char[] SplitCharacters = new char[]
	{
		'|'
	};
}
