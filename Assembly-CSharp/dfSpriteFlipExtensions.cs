using System;

// Token: 0x020006B9 RID: 1721
public static class dfSpriteFlipExtensions
{
	// Token: 0x06003C61 RID: 15457 RVA: 0x000E3AA4 File Offset: 0x000E1CA4
	public static bool IsSet(this dfSpriteFlip value, dfSpriteFlip flag)
	{
		return flag == (value & flag);
	}

	// Token: 0x06003C62 RID: 15458 RVA: 0x000E3AAC File Offset: 0x000E1CAC
	public static dfSpriteFlip SetFlag(this dfSpriteFlip value, dfSpriteFlip flag, bool on)
	{
		if (on)
		{
			return value | flag;
		}
		return value & ~flag;
	}
}
