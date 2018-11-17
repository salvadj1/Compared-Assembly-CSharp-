using System;

// Token: 0x02000690 RID: 1680
internal class terrain : ConsoleSystem
{
	// Token: 0x06003A1E RID: 14878 RVA: 0x000D8650 File Offset: 0x000D6850
	[ConsoleSystem.Client]
	public static void reassign(ref ConsoleSystem.Arg arg)
	{
		TerrainControl.ter_reassign();
	}

	// Token: 0x06003A1F RID: 14879 RVA: 0x000D8658 File Offset: 0x000D6858
	[ConsoleSystem.Client]
	public static void reassign_nocopy(ref ConsoleSystem.Arg arg)
	{
		TerrainControl.ter_reassign_nocopy();
	}

	// Token: 0x06003A20 RID: 14880 RVA: 0x000D8660 File Offset: 0x000D6860
	[ConsoleSystem.Client]
	public static void mat(ref ConsoleSystem.Arg arg)
	{
		TerrainControl.ter_mat();
	}

	// Token: 0x06003A21 RID: 14881 RVA: 0x000D8668 File Offset: 0x000D6868
	[ConsoleSystem.Client]
	public static void flush(ref ConsoleSystem.Arg arg)
	{
		TerrainControl.ter_flush();
	}

	// Token: 0x06003A22 RID: 14882 RVA: 0x000D8670 File Offset: 0x000D6870
	[ConsoleSystem.Client]
	public static void flushtrees(ref ConsoleSystem.Arg arg)
	{
		TerrainControl.ter_flushtrees();
	}

	// Token: 0x04001E40 RID: 7744
	[ConsoleSystem.Client]
	public static bool manual;

	// Token: 0x04001E41 RID: 7745
	[ConsoleSystem.Client]
	[ConsoleSystem.Help("The interval (seconds) to force tree redrawing when there is no camera movement. Set to zero if you do not want forced tree drawing", "")]
	public static float idleinterval = 3.2f;
}
