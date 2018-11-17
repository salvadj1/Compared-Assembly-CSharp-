using System;

// Token: 0x020002B5 RID: 693
public class interp : ConsoleSystem
{
	// Token: 0x17000737 RID: 1847
	// (get) Token: 0x060018DD RID: 6365 RVA: 0x00062188 File Offset: 0x00060388
	// (set) Token: 0x060018DE RID: 6366 RVA: 0x00062190 File Offset: 0x00060390
	[ConsoleSystem.Admin]
	[ConsoleSystem.Help("This value determins how much time to append to interp delay ( on clients ) based on server.sendrate", "")]
	public static float ratio
	{
		get
		{
			return Interpolation.sendRateRatiof;
		}
		set
		{
			Interpolation.sendRateRatiof = value;
		}
	}

	// Token: 0x17000738 RID: 1848
	// (get) Token: 0x060018DF RID: 6367 RVA: 0x00062198 File Offset: 0x00060398
	// (set) Token: 0x060018E0 RID: 6368 RVA: 0x000621C0 File Offset: 0x000603C0
	[ConsoleSystem.Help("This value adds a fixed amount of delay ( in milliseconds ) to interp delay ( on clients ).", "")]
	[ConsoleSystem.Admin]
	public static int delayms
	{
		get
		{
			ulong delayMillis = Interpolation.delayMillis;
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
				Interpolation.delayMillis = 0UL;
			}
			else
			{
				Interpolation.delayMillis = (ulong)((long)value);
			}
		}
	}
}
