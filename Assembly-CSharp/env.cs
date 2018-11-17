using System;

// Token: 0x02000050 RID: 80
public class env : ConsoleSystem
{
	// Token: 0x060002BA RID: 698 RVA: 0x0000E568 File Offset: 0x0000C768
	[ConsoleSystem.Admin]
	[ConsoleSystem.Help("Gets or sets the current time", "")]
	public static void time(ref ConsoleSystem.Arg arg)
	{
		if (!EnvironmentControlCenter.Singleton)
		{
			return;
		}
		arg.ReplyWith("Current Time: " + EnvironmentControlCenter.Singleton.GetTime().ToString());
	}

	// Token: 0x040001B0 RID: 432
	[ConsoleSystem.Admin]
	[ConsoleSystem.Help("The length of a day in real minutes", "")]
	public static float daylength = 45f;

	// Token: 0x040001B1 RID: 433
	[ConsoleSystem.Help("The length of a night in real minutes", "")]
	[ConsoleSystem.Admin]
	public static float nightlength = 15f;
}
