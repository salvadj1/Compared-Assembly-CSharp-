using System;

// Token: 0x0200077B RID: 1915
public static class dfMouseButtonsExtensions
{
	// Token: 0x06004064 RID: 16484 RVA: 0x000EC554 File Offset: 0x000EA754
	public static bool IsSet(this global::dfMouseButtons value, global::dfMouseButtons flag)
	{
		return flag == (value & flag);
	}
}
