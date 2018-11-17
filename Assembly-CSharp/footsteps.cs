using System;

// Token: 0x02000568 RID: 1384
public class footsteps : global::ConsoleSystem
{
	// Token: 0x040017BC RID: 6076
	[global::ConsoleSystem.Help("Footstep Quality, 0 = default sound, 1 = dynamic for local, 2 = dynamic for all. 0-2 (default 2)", "")]
	[global::ConsoleSystem.Client]
	[global::ConsoleSystem.Saved]
	public static int quality = 2;
}
