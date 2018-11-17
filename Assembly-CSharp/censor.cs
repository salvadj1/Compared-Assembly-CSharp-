using System;

// Token: 0x020005CA RID: 1482
public class censor : global::ConsoleSystem
{
	// Token: 0x17000A1F RID: 2591
	// (get) Token: 0x06002F87 RID: 12167 RVA: 0x000B731C File Offset: 0x000B551C
	// (set) Token: 0x06002F88 RID: 12168 RVA: 0x000B7324 File Offset: 0x000B5524
	[global::ConsoleSystem.Client]
	[global::ConsoleSystem.User]
	public static bool nudity
	{
		get
		{
			return global::ArmorModelRenderer.Censored;
		}
		set
		{
			global::ArmorModelRenderer.Censored = value;
		}
	}
}
