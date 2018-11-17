using System;

// Token: 0x0200004A RID: 74
public class grass : ConsoleSystem
{
	// Token: 0x17000070 RID: 112
	// (get) Token: 0x06000291 RID: 657 RVA: 0x0000E0CC File Offset: 0x0000C2CC
	// (set) Token: 0x06000292 RID: 658 RVA: 0x0000E0D4 File Offset: 0x0000C2D4
	[ConsoleSystem.Client]
	[ConsoleSystem.User]
	[ConsoleSystem.Saved]
	public static bool shadowcast
	{
		get
		{
			return FPGrass.castShadows;
		}
		set
		{
			FPGrass.castShadows = value;
		}
	}

	// Token: 0x17000071 RID: 113
	// (get) Token: 0x06000293 RID: 659 RVA: 0x0000E0DC File Offset: 0x0000C2DC
	// (set) Token: 0x06000294 RID: 660 RVA: 0x0000E0E4 File Offset: 0x0000C2E4
	[ConsoleSystem.Saved]
	[ConsoleSystem.User]
	[ConsoleSystem.Client]
	public static bool shadowreceive
	{
		get
		{
			return FPGrass.receiveShadows;
		}
		set
		{
			FPGrass.receiveShadows = value;
		}
	}

	// Token: 0x040001A3 RID: 419
	[ConsoleSystem.Client]
	[ConsoleSystem.Saved]
	[ConsoleSystem.User]
	public static bool on = FPGrass.Support.Supported;

	// Token: 0x040001A4 RID: 420
	[ConsoleSystem.Saved]
	[ConsoleSystem.User]
	[ConsoleSystem.Client]
	public static bool forceredraw = false;

	// Token: 0x040001A5 RID: 421
	[ConsoleSystem.Saved]
	[ConsoleSystem.User]
	[ConsoleSystem.Client]
	public static bool displacement = FPGrass.Support.Supported && !FPGrass.Support.DisplacementExpensive;

	// Token: 0x040001A6 RID: 422
	[ConsoleSystem.Saved]
	[ConsoleSystem.User]
	[ConsoleSystem.Client]
	public static float disp_trail_seconds = 10f;
}
