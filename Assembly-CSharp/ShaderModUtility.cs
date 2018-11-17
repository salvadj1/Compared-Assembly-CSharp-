using System;
using System.Collections.Generic;

// Token: 0x02000360 RID: 864
public static class ShaderModUtility
{
	// Token: 0x0600211E RID: 8478 RVA: 0x00081BDC File Offset: 0x0007FDDC
	public static int Replace(this ShaderMod[] mods, ShaderMod.Replacement replacement, string incoming, ref string outgoing)
	{
		if (mods != null)
		{
			int num = mods.Length;
			for (int i = 0; i < num; i++)
			{
				if (mods[i] && mods[i].Replace(replacement, incoming, ref outgoing))
				{
					return i;
				}
			}
		}
		return -1;
	}

	// Token: 0x0600211F RID: 8479 RVA: 0x00081C28 File Offset: 0x0007FE28
	public static int ReplaceReverse(this ShaderMod[] mods, ShaderMod.Replacement replacement, string incoming, ref string outgoing)
	{
		if (mods != null)
		{
			int num = mods.Length;
			for (int i = num - 1; i >= 0; i--)
			{
				if (mods[i] && mods[i].Replace(replacement, incoming, ref outgoing))
				{
					return i;
				}
			}
		}
		return -1;
	}

	// Token: 0x06002120 RID: 8480 RVA: 0x00081C74 File Offset: 0x0007FE74
	public static IEnumerable<ShaderMod.KV> MergeKeyValues(this ShaderMod[] mods, ShaderMod.Replacement replacement)
	{
		return null;
	}
}
