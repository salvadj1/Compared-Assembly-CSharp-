using System;

// Token: 0x02000191 RID: 401
public static class GameEvent
{
	// Token: 0x14000005 RID: 5
	// (add) Token: 0x06000BEA RID: 3050 RVA: 0x0002FC44 File Offset: 0x0002DE44
	// (remove) Token: 0x06000BEB RID: 3051 RVA: 0x0002FC5C File Offset: 0x0002DE5C
	public static event GameEvent.OnPlayerConnectedHandler PlayerConnected;

	// Token: 0x14000006 RID: 6
	// (add) Token: 0x06000BEC RID: 3052 RVA: 0x0002FC74 File Offset: 0x0002DE74
	// (remove) Token: 0x06000BED RID: 3053 RVA: 0x0002FC8C File Offset: 0x0002DE8C
	public static event GameEvent.OnGenericEvent QualitySettingsRefresh;

	// Token: 0x06000BEE RID: 3054 RVA: 0x0002FCA4 File Offset: 0x0002DEA4
	public static void DoPlayerConnected(PlayerClient player)
	{
		if (GameEvent.PlayerConnected != null)
		{
			GameEvent.PlayerConnected(player);
		}
	}

	// Token: 0x06000BEF RID: 3055 RVA: 0x0002FCBC File Offset: 0x0002DEBC
	public static void DoQualitySettingsRefresh()
	{
		if (GameEvent.QualitySettingsRefresh != null)
		{
			GameEvent.QualitySettingsRefresh();
		}
	}

	// Token: 0x02000864 RID: 2148
	// (Invoke) Token: 0x06004B68 RID: 19304
	public delegate void OnGenericEvent();

	// Token: 0x02000865 RID: 2149
	// (Invoke) Token: 0x06004B6C RID: 19308
	public delegate void OnPlayerConnectedHandler(PlayerClient player);
}
