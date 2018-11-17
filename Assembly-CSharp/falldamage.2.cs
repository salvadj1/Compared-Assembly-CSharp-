using System;

// Token: 0x02000547 RID: 1351
internal class falldamage : global::ConsoleSystem
{
	// Token: 0x040016E3 RID: 5859
	[global::ConsoleSystem.Admin]
	[global::ConsoleSystem.Help("Fall velocity to begin fall damage calculations - min 18", "")]
	public static float min_vel = 24f;

	// Token: 0x040016E4 RID: 5860
	[global::ConsoleSystem.Admin]
	[global::ConsoleSystem.Help("Fall Velocity when damage of maxhealth will be applied", "")]
	public static float max_vel = 38f;

	// Token: 0x040016E5 RID: 5861
	[global::ConsoleSystem.Help("enable/disable fall damage", "")]
	[global::ConsoleSystem.Admin]
	public static bool enabled = true;

	// Token: 0x040016E6 RID: 5862
	[global::ConsoleSystem.Admin]
	[global::ConsoleSystem.Help("Average amount of time a leg injury lasts", "")]
	public static float injury_length = 40f;
}
