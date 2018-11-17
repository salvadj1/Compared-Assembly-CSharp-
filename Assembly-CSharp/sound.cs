using System;
using UnityEngine;

// Token: 0x02000060 RID: 96
public class sound : global::ConsoleSystem
{
	// Token: 0x1700008D RID: 141
	// (get) Token: 0x06000321 RID: 801 RVA: 0x0000F954 File Offset: 0x0000DB54
	// (set) Token: 0x06000322 RID: 802 RVA: 0x0000F95C File Offset: 0x0000DB5C
	[global::ConsoleSystem.Saved]
	[global::ConsoleSystem.Help("Global sound volume", "")]
	[global::ConsoleSystem.Client]
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

	// Token: 0x0400020F RID: 527
	[global::ConsoleSystem.Help("Global music volume", "")]
	[global::ConsoleSystem.Saved]
	[global::ConsoleSystem.Client]
	public static float music = 0.4f;
}
