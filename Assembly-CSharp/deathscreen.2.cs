using System;

// Token: 0x020004A0 RID: 1184
public class deathscreen : global::ConsoleSystem
{
	// Token: 0x060028AD RID: 10413 RVA: 0x00094B74 File Offset: 0x00092D74
	[global::ConsoleSystem.Client]
	public static void show(ref global::ConsoleSystem.Arg arg)
	{
		global::DeathScreen.Show();
	}

	// Token: 0x0400138A RID: 5002
	[global::ConsoleSystem.Client]
	public static string reason = "...";
}
