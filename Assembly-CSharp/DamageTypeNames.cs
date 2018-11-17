using System;
using System.Collections.Generic;

// Token: 0x02000159 RID: 345
public static class DamageTypeNames
{
	// Token: 0x06000A7B RID: 2683 RVA: 0x00029BF4 File Offset: 0x00027DF4
	static DamageTypeNames()
	{
		DamageTypeNames.Strings = new string[6];
		DamageTypeNames.Values = new Dictionary<string, DamageTypeIndex>(6);
		for (DamageTypeIndex damageTypeIndex = DamageTypeIndex.damage_generic; damageTypeIndex < DamageTypeIndex.damage_last; damageTypeIndex++)
		{
			DamageTypeNames.Values.Add(DamageTypeNames.Strings[(int)damageTypeIndex] = damageTypeIndex.ToString().Substring("damage_".Length), damageTypeIndex);
		}
		uint num = 63u;
		DamageTypeNames.Mask = (DamageTypeFlags)num;
		DamageTypeNames.Flags = new string[num];
		DamageTypeNames.Flags[0] = "none";
		for (uint num2 = 1u; num2 < num; num2 += 1u)
		{
			uint num3 = num2;
			int i = 0;
			while (i < 6)
			{
				if ((num2 & 1u << i) == 1u << i)
				{
					string str = DamageTypeNames.Strings[i];
					if ((num3 &= ~(1u << i)) == 0u)
					{
						break;
					}
					while ((long)(++i) < 6L)
					{
						if ((num2 & 1u << i) == 1u << i)
						{
							str = str + "|" + DamageTypeNames.Strings[i];
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

	// Token: 0x06000A7C RID: 2684 RVA: 0x00029D4C File Offset: 0x00027F4C
	public static bool Convert(string name, out DamageTypeIndex index)
	{
		return DamageTypeNames.Values.TryGetValue(name, out index);
	}

	// Token: 0x06000A7D RID: 2685 RVA: 0x00029D5C File Offset: 0x00027F5C
	public static bool Convert(string[] names, out DamageTypeFlags flags)
	{
		for (int i = 0; i < names.Length; i++)
		{
			DamageTypeIndex damageTypeIndex;
			if (DamageTypeNames.Values.TryGetValue(names[i], out damageTypeIndex))
			{
				flags = (DamageTypeFlags)(1 << (int)damageTypeIndex);
				while (++i < names.Length)
				{
					if (DamageTypeNames.Values.TryGetValue(names[i], out damageTypeIndex))
					{
						flags |= (DamageTypeFlags)(1 << (int)damageTypeIndex);
					}
				}
				return true;
			}
		}
		flags = (DamageTypeFlags)0;
		return false;
	}

	// Token: 0x06000A7E RID: 2686 RVA: 0x00029DD0 File Offset: 0x00027FD0
	public static bool Convert(string name, out DamageTypeFlags flags)
	{
		DamageTypeIndex damageTypeIndex;
		if (DamageTypeNames.Values.TryGetValue(name, out damageTypeIndex))
		{
			flags = (DamageTypeFlags)(1 << (int)damageTypeIndex);
			return true;
		}
		if (name.Length == 0 || name == "none")
		{
			flags = (DamageTypeFlags)0;
			return true;
		}
		return DamageTypeNames.Convert(name.Split(DamageTypeNames.SplitCharacters, StringSplitOptions.RemoveEmptyEntries), out flags);
	}

	// Token: 0x06000A7F RID: 2687 RVA: 0x00029E2C File Offset: 0x0002802C
	public static bool Convert(DamageTypeIndex index, out DamageTypeFlags flags)
	{
		flags = (DamageTypeFlags)(1 << (int)index);
		return (flags & DamageTypeNames.Mask) == flags;
	}

	// Token: 0x06000A80 RID: 2688 RVA: 0x00029E44 File Offset: 0x00028044
	public static bool Convert(DamageTypeIndex index, out string name)
	{
		if (index == DamageTypeIndex.damage_generic || (index > DamageTypeIndex.damage_generic && index < DamageTypeIndex.damage_last))
		{
			name = DamageTypeNames.Strings[(int)index];
			return true;
		}
		name = null;
		return false;
	}

	// Token: 0x040006D0 RID: 1744
	private static readonly string[] Strings;

	// Token: 0x040006D1 RID: 1745
	private static readonly string[] Flags;

	// Token: 0x040006D2 RID: 1746
	private static readonly Dictionary<string, DamageTypeIndex> Values;

	// Token: 0x040006D3 RID: 1747
	private static readonly DamageTypeFlags Mask;

	// Token: 0x040006D4 RID: 1748
	private static readonly char[] SplitCharacters = new char[]
	{
		'|'
	};
}
