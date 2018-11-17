using System;

// Token: 0x0200050D RID: 1293
public class censor : ConsoleSystem
{
	// Token: 0x170009AB RID: 2475
	// (get) Token: 0x06002BC7 RID: 11207 RVA: 0x000AF280 File Offset: 0x000AD480
	// (set) Token: 0x06002BC8 RID: 11208 RVA: 0x000AF288 File Offset: 0x000AD488
	[ConsoleSystem.Client]
	[ConsoleSystem.User]
	public static bool nudity
	{
		get
		{
			return ArmorModelRenderer.Censored;
		}
		set
		{
			ArmorModelRenderer.Censored = value;
		}
	}
}
