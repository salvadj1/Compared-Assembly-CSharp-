using System;

// Token: 0x020004AD RID: 1197
public class footsteps : ConsoleSystem
{
	// Token: 0x040015FF RID: 5631
	[ConsoleSystem.Client]
	[ConsoleSystem.Saved]
	[ConsoleSystem.Help("Footstep Quality, 0 = default sound, 1 = dynamic for local, 2 = dynamic for all. 0-2 (default 2)", "")]
	public static int quality = 2;
}
