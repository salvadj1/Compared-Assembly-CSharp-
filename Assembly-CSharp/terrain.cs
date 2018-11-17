using System;

// Token: 0x02000755 RID: 1877
internal class terrain : global::ConsoleSystem
{
	// Token: 0x06003E16 RID: 15894 RVA: 0x000E1030 File Offset: 0x000DF230
	[global::ConsoleSystem.Client]
	public static void reassign(ref global::ConsoleSystem.Arg arg)
	{
		global::TerrainControl.ter_reassign();
	}

	// Token: 0x06003E17 RID: 15895 RVA: 0x000E1038 File Offset: 0x000DF238
	[global::ConsoleSystem.Client]
	public static void reassign_nocopy(ref global::ConsoleSystem.Arg arg)
	{
		global::TerrainControl.ter_reassign_nocopy();
	}

	// Token: 0x06003E18 RID: 15896 RVA: 0x000E1040 File Offset: 0x000DF240
	[global::ConsoleSystem.Client]
	public static void mat(ref global::ConsoleSystem.Arg arg)
	{
		global::TerrainControl.ter_mat();
	}

	// Token: 0x06003E19 RID: 15897 RVA: 0x000E1048 File Offset: 0x000DF248
	[global::ConsoleSystem.Client]
	public static void flush(ref global::ConsoleSystem.Arg arg)
	{
		global::TerrainControl.ter_flush();
	}

	// Token: 0x06003E1A RID: 15898 RVA: 0x000E1050 File Offset: 0x000DF250
	[global::ConsoleSystem.Client]
	public static void flushtrees(ref global::ConsoleSystem.Arg arg)
	{
		global::TerrainControl.ter_flushtrees();
	}

	// Token: 0x04002038 RID: 8248
	[global::ConsoleSystem.Client]
	public static bool manual;

	// Token: 0x04002039 RID: 8249
	[global::ConsoleSystem.Help("The interval (seconds) to force tree redrawing when there is no camera movement. Set to zero if you do not want forced tree drawing", "")]
	[global::ConsoleSystem.Client]
	public static float idleinterval = 3.2f;
}
