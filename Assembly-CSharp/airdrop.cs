using System;

// Token: 0x02000096 RID: 150
public class airdrop : ConsoleSystem
{
	// Token: 0x0600032B RID: 811 RVA: 0x0000FDE4 File Offset: 0x0000DFE4
	[ConsoleSystem.Admin]
	public static void drop(ref ConsoleSystem.Arg arg)
	{
	}

	// Token: 0x040002B1 RID: 689
	[ConsoleSystem.Admin]
	public static int min_players = 50;
}
