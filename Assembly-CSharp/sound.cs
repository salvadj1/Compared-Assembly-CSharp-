using System;
using UnityEngine;

// Token: 0x0200004E RID: 78
public class sound : ConsoleSystem
{
	// Token: 0x17000077 RID: 119
	// (get) Token: 0x060002AF RID: 687 RVA: 0x0000E3AC File Offset: 0x0000C5AC
	// (set) Token: 0x060002B0 RID: 688 RVA: 0x0000E3B4 File Offset: 0x0000C5B4
	[ConsoleSystem.Help("Global sound volume", "")]
	[ConsoleSystem.Client]
	[ConsoleSystem.Saved]
	public static float volume
	{
		get
		{
			return AudioListener.volume;
		}
		set
		{
			AudioListener.volume = value;
		}
	}

	// Token: 0x040001AD RID: 429
	[ConsoleSystem.Help("Global music volume", "")]
	[ConsoleSystem.Saved]
	[ConsoleSystem.Client]
	public static float music = 0.4f;
}
