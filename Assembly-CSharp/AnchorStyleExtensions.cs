using System;

// Token: 0x020006B7 RID: 1719
public static class AnchorStyleExtensions
{
	// Token: 0x06003C5D RID: 15453 RVA: 0x000E3A78 File Offset: 0x000E1C78
	public static bool IsFlagSet(this dfAnchorStyle value, dfAnchorStyle flag)
	{
		return flag == (value & flag);
	}

	// Token: 0x06003C5E RID: 15454 RVA: 0x000E3A80 File Offset: 0x000E1C80
	public static bool IsAnyFlagSet(this dfAnchorStyle value, dfAnchorStyle flag)
	{
		return dfAnchorStyle.None != (value & flag);
	}

	// Token: 0x06003C5F RID: 15455 RVA: 0x000E3A8C File Offset: 0x000E1C8C
	public static dfAnchorStyle SetFlag(this dfAnchorStyle value, dfAnchorStyle flag)
	{
		return value | flag;
	}

	// Token: 0x06003C60 RID: 15456 RVA: 0x000E3A94 File Offset: 0x000E1C94
	public static dfAnchorStyle SetFlag(this dfAnchorStyle value, dfAnchorStyle flag, bool on)
	{
		if (on)
		{
			return value | flag;
		}
		return value & ~flag;
	}
}
