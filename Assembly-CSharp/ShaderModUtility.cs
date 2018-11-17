using System;
using System.Collections.Generic;

// Token: 0x0200040D RID: 1037
public static class ShaderModUtility
{
	// Token: 0x06002480 RID: 9344 RVA: 0x00086FD8 File Offset: 0x000851D8
	public static int Replace(this global::ShaderMod[] mods, global::ShaderMod.Replacement replacement, string incoming, ref string outgoing)
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

	// Token: 0x06002481 RID: 9345 RVA: 0x00087024 File Offset: 0x00085224
	public static int ReplaceReverse(this global::ShaderMod[] mods, global::ShaderMod.Replacement replacement, string incoming, ref string outgoing)
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

	// Token: 0x06002482 RID: 9346 RVA: 0x00087070 File Offset: 0x00085270
	public static IEnumerable<global::ShaderMod.KV> MergeKeyValues(this global::ShaderMod[] mods, global::ShaderMod.Replacement replacement)
	{
		return null;
	}
}
