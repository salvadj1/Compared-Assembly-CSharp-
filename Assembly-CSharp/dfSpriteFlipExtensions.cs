using System;

// Token: 0x02000784 RID: 1924
public static class dfSpriteFlipExtensions
{
	// Token: 0x0600406B RID: 16491 RVA: 0x000EC5E8 File Offset: 0x000EA7E8
	public static bool IsSet(this global::dfSpriteFlip value, global::dfSpriteFlip flag)
	{
		return flag == (value & flag);
	}

	// Token: 0x0600406C RID: 16492 RVA: 0x000EC5F0 File Offset: 0x000EA7F0
	public static global::dfSpriteFlip SetFlag(this global::dfSpriteFlip value, global::dfSpriteFlip flag, bool on)
	{
		if (on)
		{
			return value | flag;
		}
		return value & ~flag;
	}
}
