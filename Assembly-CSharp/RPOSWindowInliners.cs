using System;

// Token: 0x020004D6 RID: 1238
public static class RPOSWindowInliners
{
	// Token: 0x06002AD7 RID: 10967 RVA: 0x0009F118 File Offset: 0x0009D318
	public static TRPOSWindow EnsureAwake<TRPOSWindow>(this TRPOSWindow window) where TRPOSWindow : global::RPOSWindow
	{
		if (window)
		{
			global::RPOSWindow.EnsureAwake(window);
		}
		return window;
	}

	// Token: 0x06002AD8 RID: 10968 RVA: 0x0009F138 File Offset: 0x0009D338
	public static bool IsRegistered(this global::RPOSWindow window)
	{
		return window && window.ready;
	}
}
