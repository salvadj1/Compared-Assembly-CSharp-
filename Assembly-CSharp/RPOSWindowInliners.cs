using System;

// Token: 0x02000421 RID: 1057
public static class RPOSWindowInliners
{
	// Token: 0x0600274D RID: 10061 RVA: 0x00099254 File Offset: 0x00097454
	public static TRPOSWindow EnsureAwake<TRPOSWindow>(this TRPOSWindow window) where TRPOSWindow : RPOSWindow
	{
		if (window)
		{
			RPOSWindow.EnsureAwake(window);
		}
		return window;
	}

	// Token: 0x0600274E RID: 10062 RVA: 0x00099274 File Offset: 0x00097474
	public static bool IsRegistered(this RPOSWindow window)
	{
		return window && window.ready;
	}
}
