using System;

// Token: 0x0200048C RID: 1164
internal class falldamage : ConsoleSystem
{
	// Token: 0x0400154C RID: 5452
	[ConsoleSystem.Admin]
	[ConsoleSystem.Help("Fall velocity to begin fall damage calculations - min 18", "")]
	public static float min_vel = 24f;

	// Token: 0x0400154D RID: 5453
	[ConsoleSystem.Admin]
	[ConsoleSystem.Help("Fall Velocity when damage of maxhealth will be applied", "")]
	public static float max_vel = 38f;

	// Token: 0x0400154E RID: 5454
	[ConsoleSystem.Admin]
	[ConsoleSystem.Help("enable/disable fall damage", "")]
	public static bool enabled = true;

	// Token: 0x0400154F RID: 5455
	[ConsoleSystem.Help("Average amount of time a leg injury lasts", "")]
	[ConsoleSystem.Admin]
	public static float injury_length = 40f;
}
