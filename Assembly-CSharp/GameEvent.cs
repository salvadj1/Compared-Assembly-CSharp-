using System;

// Token: 0x020001BD RID: 445
public static class GameEvent
{
	// Token: 0x14000005 RID: 5
	// (add) Token: 0x06000D1A RID: 3354 RVA: 0x00033B30 File Offset: 0x00031D30
	// (remove) Token: 0x06000D1B RID: 3355 RVA: 0x00033B48 File Offset: 0x00031D48
	public static event global::GameEvent.OnPlayerConnectedHandler PlayerConnected;

	// Token: 0x14000006 RID: 6
	// (add) Token: 0x06000D1C RID: 3356 RVA: 0x00033B60 File Offset: 0x00031D60
	// (remove) Token: 0x06000D1D RID: 3357 RVA: 0x00033B78 File Offset: 0x00031D78
	public static event global::GameEvent.OnGenericEvent QualitySettingsRefresh;

	// Token: 0x06000D1E RID: 3358 RVA: 0x00033B90 File Offset: 0x00031D90
	public static void DoPlayerConnected(global::PlayerClient player)
	{
		if (global::GameEvent.PlayerConnected != null)
		{
			global::GameEvent.PlayerConnected(player);
		}
	}

	// Token: 0x06000D1F RID: 3359 RVA: 0x00033BA8 File Offset: 0x00031DA8
	public static void DoQualitySettingsRefresh()
	{
		if (global::GameEvent.QualitySettingsRefresh != null)
		{
			global::GameEvent.QualitySettingsRefresh();
		}
	}

	// Token: 0x020001BE RID: 446
	// (Invoke) Token: 0x06000D21 RID: 3361
	public delegate void OnGenericEvent();

	// Token: 0x020001BF RID: 447
	// (Invoke) Token: 0x06000D25 RID: 3365
	public delegate void OnPlayerConnectedHandler(global::PlayerClient player);
}
