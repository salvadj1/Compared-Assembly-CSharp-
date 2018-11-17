using System;

// Token: 0x02000782 RID: 1922
public static class AnchorStyleExtensions
{
	// Token: 0x06004067 RID: 16487 RVA: 0x000EC5BC File Offset: 0x000EA7BC
	public static bool IsFlagSet(this global::dfAnchorStyle value, global::dfAnchorStyle flag)
	{
		return flag == (value & flag);
	}

	// Token: 0x06004068 RID: 16488 RVA: 0x000EC5C4 File Offset: 0x000EA7C4
	public static bool IsAnyFlagSet(this global::dfAnchorStyle value, global::dfAnchorStyle flag)
	{
		return global::dfAnchorStyle.None != (value & flag);
	}

	// Token: 0x06004069 RID: 16489 RVA: 0x000EC5D0 File Offset: 0x000EA7D0
	public static global::dfAnchorStyle SetFlag(this global::dfAnchorStyle value, global::dfAnchorStyle flag)
	{
		return value | flag;
	}

	// Token: 0x0600406A RID: 16490 RVA: 0x000EC5D8 File Offset: 0x000EA7D8
	public static global::dfAnchorStyle SetFlag(this global::dfAnchorStyle value, global::dfAnchorStyle flag, bool on)
	{
		if (on)
		{
			return value | flag;
		}
		return value & ~flag;
	}
}
