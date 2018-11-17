using System;

// Token: 0x020006B0 RID: 1712
public static class dfMouseButtonsExtensions
{
	// Token: 0x06003C5A RID: 15450 RVA: 0x000E3A10 File Offset: 0x000E1C10
	public static bool IsSet(this dfMouseButtons value, dfMouseButtons flag)
	{
		return flag == (value & flag);
	}
}
