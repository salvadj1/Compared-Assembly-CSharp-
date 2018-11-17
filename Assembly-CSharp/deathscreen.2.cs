using System;

// Token: 0x020003F0 RID: 1008
public class deathscreen : ConsoleSystem
{
	// Token: 0x0600253B RID: 9531 RVA: 0x0008F188 File Offset: 0x0008D388
	[ConsoleSystem.Client]
	public static void show(ref ConsoleSystem.Arg arg)
	{
		DeathScreen.Show();
	}

	// Token: 0x04001210 RID: 4624
	[ConsoleSystem.Client]
	public static string reason = "...";
}
