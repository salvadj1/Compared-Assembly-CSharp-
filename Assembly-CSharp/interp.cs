using System;

// Token: 0x020002F2 RID: 754
public class interp : global::ConsoleSystem
{
	// Token: 0x1700078B RID: 1931
	// (get) Token: 0x06001A6D RID: 6765 RVA: 0x00066AFC File Offset: 0x00064CFC
	// (set) Token: 0x06001A6E RID: 6766 RVA: 0x00066B04 File Offset: 0x00064D04
	[global::ConsoleSystem.Admin]
	[global::ConsoleSystem.Help("This value determins how much time to append to interp delay ( on clients ) based on server.sendrate", "")]
	public static float ratio
	{
		get
		{
			return global::Interpolation.sendRateRatiof;
		}
		set
		{
			global::Interpolation.sendRateRatiof = value;
		}
	}

	// Token: 0x1700078C RID: 1932
	// (get) Token: 0x06001A6F RID: 6767 RVA: 0x00066B0C File Offset: 0x00064D0C
	// (set) Token: 0x06001A70 RID: 6768 RVA: 0x00066B34 File Offset: 0x00064D34
	[global::ConsoleSystem.Help("This value adds a fixed amount of delay ( in milliseconds ) to interp delay ( on clients ).", "")]
	[global::ConsoleSystem.Admin]
	public static int delayms
	{
		get
		{
			ulong delayMillis = global::Interpolation.delayMillis;
			if (delayMillis > 2147483647UL)
			{
				return int.MaxValue;
			}
			return (int)delayMillis;
		}
		set
		{
			if (value < 0)
			{
				global::Interpolation.delayMillis = 0UL;
			}
			else
			{
				global::Interpolation.delayMillis = (ulong)((long)value);
			}
		}
	}
}
