using System;

// Token: 0x0200005C RID: 92
public class grass : global::ConsoleSystem
{
	// Token: 0x17000086 RID: 134
	// (get) Token: 0x06000303 RID: 771 RVA: 0x0000F674 File Offset: 0x0000D874
	// (set) Token: 0x06000304 RID: 772 RVA: 0x0000F67C File Offset: 0x0000D87C
	[global::ConsoleSystem.Client]
	[global::ConsoleSystem.Saved]
	[global::ConsoleSystem.User]
	public static bool shadowcast
	{
		get
		{
			return global::FPGrass.castShadows;
		}
		set
		{
			global::FPGrass.castShadows = value;
		}
	}

	// Token: 0x17000087 RID: 135
	// (get) Token: 0x06000305 RID: 773 RVA: 0x0000F684 File Offset: 0x0000D884
	// (set) Token: 0x06000306 RID: 774 RVA: 0x0000F68C File Offset: 0x0000D88C
	[global::ConsoleSystem.User]
	[global::ConsoleSystem.Client]
	[global::ConsoleSystem.Saved]
	public static bool shadowreceive
	{
		get
		{
			return global::FPGrass.receiveShadows;
		}
		set
		{
			global::FPGrass.receiveShadows = value;
		}
	}

	// Token: 0x04000205 RID: 517
	[global::ConsoleSystem.User]
	[global::ConsoleSystem.Client]
	[global::ConsoleSystem.Saved]
	public static bool on = global::FPGrass.Support.Supported;

	// Token: 0x04000206 RID: 518
	[global::ConsoleSystem.Saved]
	[global::ConsoleSystem.Client]
	[global::ConsoleSystem.User]
	public static bool forceredraw = false;

	// Token: 0x04000207 RID: 519
	[global::ConsoleSystem.User]
	[global::ConsoleSystem.Client]
	[global::ConsoleSystem.Saved]
	public static bool displacement = global::FPGrass.Support.Supported && !global::FPGrass.Support.DisplacementExpensive;

	// Token: 0x04000208 RID: 520
	[global::ConsoleSystem.Saved]
	[global::ConsoleSystem.User]
	[global::ConsoleSystem.Client]
	public static float disp_trail_seconds = 10f;
}
