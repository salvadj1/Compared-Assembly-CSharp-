using System;

// Token: 0x02000062 RID: 98
public class env : global::ConsoleSystem
{
	// Token: 0x0600032C RID: 812 RVA: 0x0000FB10 File Offset: 0x0000DD10
	[global::ConsoleSystem.Admin]
	[global::ConsoleSystem.Help("Gets or sets the current time", "")]
	public static void time(ref global::ConsoleSystem.Arg arg)
	{
		if (!global::EnvironmentControlCenter.Singleton)
		{
			return;
		}
		arg.ReplyWith("Current Time: " + global::EnvironmentControlCenter.Singleton.GetTime().ToString());
	}

	// Token: 0x04000212 RID: 530
	[global::ConsoleSystem.Admin]
	[global::ConsoleSystem.Help("The length of a day in real minutes", "")]
	public static float daylength = 45f;

	// Token: 0x04000213 RID: 531
	[global::ConsoleSystem.Admin]
	[global::ConsoleSystem.Help("The length of a night in real minutes", "")]
	public static float nightlength = 15f;
}
