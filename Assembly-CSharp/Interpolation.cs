using System;

// Token: 0x020002B3 RID: 691
public static class Interpolation
{
	// Token: 0x060018B7 RID: 6327 RVA: 0x00061CF4 File Offset: 0x0005FEF4
	static Interpolation()
	{
		Interpolation.BindTiming(20UL, 1.5, 5f);
	}

	// Token: 0x17000725 RID: 1829
	// (get) Token: 0x060018B8 RID: 6328 RVA: 0x00061D0C File Offset: 0x0005FF0C
	public static double deltaSeconds
	{
		get
		{
			return Interpolation._deltaSeconds;
		}
	}

	// Token: 0x17000726 RID: 1830
	// (get) Token: 0x060018B9 RID: 6329 RVA: 0x00061D14 File Offset: 0x0005FF14
	public static double totalDelaySeconds
	{
		get
		{
			return Interpolation._totalDelaySeconds;
		}
	}

	// Token: 0x17000727 RID: 1831
	// (get) Token: 0x060018BA RID: 6330 RVA: 0x00061D1C File Offset: 0x0005FF1C
	public static ulong totalDelayMillis
	{
		get
		{
			return Interpolation._totalDelayMillis;
		}
	}

	// Token: 0x17000728 RID: 1832
	// (get) Token: 0x060018BB RID: 6331 RVA: 0x00061D24 File Offset: 0x0005FF24
	// (set) Token: 0x060018BC RID: 6332 RVA: 0x00061D2C File Offset: 0x0005FF2C
	public static double delaySeconds
	{
		get
		{
			return Interpolation._delaySeconds;
		}
		set
		{
			if (value < 0.0005)
			{
				Interpolation.delayMillis = 0UL;
			}
			else
			{
				Interpolation.delayMillis = (ulong)Math.Round(value * 1000.0);
			}
		}
	}

	// Token: 0x17000729 RID: 1833
	// (get) Token: 0x060018BD RID: 6333 RVA: 0x00061D60 File Offset: 0x0005FF60
	// (set) Token: 0x060018BE RID: 6334 RVA: 0x00061D68 File Offset: 0x0005FF68
	public static ulong delayMillis
	{
		get
		{
			return Interpolation._delayMillis;
		}
		set
		{
			if (value != Interpolation._delayMillis)
			{
				Interpolation.BindTiming(value, Interpolation._ratio, Interpolation._sendRate);
			}
		}
	}

	// Token: 0x1700072A RID: 1834
	// (get) Token: 0x060018BF RID: 6335 RVA: 0x00061D88 File Offset: 0x0005FF88
	public static ulong delayFromSendRateMillis
	{
		get
		{
			return Interpolation._delayFromSendRateMillis;
		}
	}

	// Token: 0x1700072B RID: 1835
	// (get) Token: 0x060018C0 RID: 6336 RVA: 0x00061D90 File Offset: 0x0005FF90
	public static double delayFromSendRateSeconds
	{
		get
		{
			return Interpolation._delayFromSendRateSeconds;
		}
	}

	// Token: 0x1700072C RID: 1836
	// (get) Token: 0x060018C1 RID: 6337 RVA: 0x00061D98 File Offset: 0x0005FF98
	public static float delayFromSendRateSecondsf
	{
		get
		{
			return (float)Interpolation._delayFromSendRateSeconds;
		}
	}

	// Token: 0x1700072D RID: 1837
	// (get) Token: 0x060018C2 RID: 6338 RVA: 0x00061DA0 File Offset: 0x0005FFA0
	// (set) Token: 0x060018C3 RID: 6339 RVA: 0x00061DA8 File Offset: 0x0005FFA8
	public static double sendRateRatio
	{
		get
		{
			return Interpolation._ratio;
		}
		set
		{
			if (value != Interpolation._ratio)
			{
				Interpolation.BindTiming(Interpolation._delayMillis, value, Interpolation._sendRate);
			}
		}
	}

	// Token: 0x1700072E RID: 1838
	// (get) Token: 0x060018C4 RID: 6340 RVA: 0x00061DC8 File Offset: 0x0005FFC8
	// (set) Token: 0x060018C5 RID: 6341 RVA: 0x00061DD0 File Offset: 0x0005FFD0
	public static float sendRate
	{
		get
		{
			return Interpolation._sendRate;
		}
		set
		{
			if (value != Interpolation._sendRate)
			{
				Interpolation.BindTiming(Interpolation._delayMillis, Interpolation._ratio, value);
			}
		}
	}

	// Token: 0x1700072F RID: 1839
	// (get) Token: 0x060018C6 RID: 6342 RVA: 0x00061DF0 File Offset: 0x0005FFF0
	// (set) Token: 0x060018C7 RID: 6343 RVA: 0x00061DF8 File Offset: 0x0005FFF8
	public static float delaySecondsf
	{
		get
		{
			return (float)Interpolation._delaySeconds;
		}
		set
		{
			Interpolation.delaySeconds = (double)value;
		}
	}

	// Token: 0x17000730 RID: 1840
	// (get) Token: 0x060018C8 RID: 6344 RVA: 0x00061E04 File Offset: 0x00060004
	public static float deltaSecondsf
	{
		get
		{
			return (float)Interpolation._deltaSeconds;
		}
	}

	// Token: 0x17000731 RID: 1841
	// (get) Token: 0x060018C9 RID: 6345 RVA: 0x00061E0C File Offset: 0x0006000C
	// (set) Token: 0x060018CA RID: 6346 RVA: 0x00061E14 File Offset: 0x00060014
	public static float sendRateRatiof
	{
		get
		{
			return (float)Interpolation._ratio;
		}
		set
		{
			Interpolation.sendRateRatio = (double)value;
		}
	}

	// Token: 0x17000732 RID: 1842
	// (get) Token: 0x060018CB RID: 6347 RVA: 0x00061E20 File Offset: 0x00060020
	public static float totalDelaySecondsf
	{
		get
		{
			return (float)Interpolation._totalDelaySeconds;
		}
	}

	// Token: 0x060018CC RID: 6348 RVA: 0x00061E28 File Offset: 0x00060028
	public static double AddDelayToTimeStampSeconds(double timeStamp)
	{
		return timeStamp + Interpolation._totalDelaySeconds;
	}

	// Token: 0x060018CD RID: 6349 RVA: 0x00061E34 File Offset: 0x00060034
	public static ulong AddDelayToTimeStampMillis(ulong timestamp)
	{
		return timestamp + Interpolation._totalDelayMillis;
	}

	// Token: 0x060018CE RID: 6350 RVA: 0x00061E40 File Offset: 0x00060040
	public static double GetInterpolationTimeSeconds(double timeStamp)
	{
		return timeStamp + Interpolation._deltaSeconds;
	}

	// Token: 0x060018CF RID: 6351 RVA: 0x00061E4C File Offset: 0x0006004C
	public static ulong GetInterpolationTimeMillis(ulong timestamp)
	{
		if (timestamp < Interpolation._totalDelayMillis)
		{
			return 0UL;
		}
		return timestamp - Interpolation._totalDelayMillis;
	}

	// Token: 0x060018D0 RID: 6352 RVA: 0x00061E64 File Offset: 0x00060064
	public static void BindTiming(ulong? delayMillis, double? sendRateRatio, float? sendRate)
	{
		Interpolation.BindTiming((delayMillis == null) ? Interpolation._delayMillis : delayMillis.Value, (sendRateRatio == null) ? Interpolation._ratio : sendRateRatio.Value, (sendRate == null) ? Interpolation._sendRate : sendRate.Value);
	}

	// Token: 0x060018D1 RID: 6353 RVA: 0x00061ED0 File Offset: 0x000600D0
	public static void BindTiming(ulong delayMillis, double sendRateRatio, float sendRate)
	{
		Interpolation._sendRate = sendRate;
		Interpolation._ratio = sendRateRatio;
		if (sendRate == 0f || sendRateRatio == 0.0 || sendRate < 0f != sendRateRatio < 0.0)
		{
			Interpolation._delayFromSendRateMillis = 0UL;
		}
		else
		{
			Interpolation._delayFromSendRateMillis = (ulong)Math.Ceiling(1000.0 * sendRateRatio / (double)sendRate);
		}
		Interpolation._delayMillis = delayMillis;
		Interpolation._totalDelayMillis = Interpolation._delayFromSendRateMillis + Interpolation._delayMillis;
		Interpolation._delaySeconds = Interpolation._delayMillis * 0.001;
		Interpolation._delayFromSendRateSeconds = Interpolation._delayFromSendRateMillis * 0.001;
		Interpolation._totalDelaySeconds = Interpolation._totalDelayMillis * 0.001;
		Interpolation._deltaSeconds = -Interpolation._totalDelaySeconds;
		Interpolation.@struct = Interpolation.Capture();
	}

	// Token: 0x060018D2 RID: 6354 RVA: 0x00061FB0 File Offset: 0x000601B0
	public static void BindTimingNetCull(ulong delayMillis, double sendRateRatio)
	{
		Interpolation.BindTiming(delayMillis, sendRateRatio, NetCull.sendRate);
	}

	// Token: 0x060018D3 RID: 6355 RVA: 0x00061FC0 File Offset: 0x000601C0
	public static void BindTimingNetCull(ulong? delayMillis, double? sendRateRatio)
	{
		Interpolation.BindTiming((delayMillis == null) ? Interpolation._delayMillis : delayMillis.Value, (sendRateRatio == null) ? Interpolation._ratio : sendRateRatio.Value, NetCull.sendRate);
	}

	// Token: 0x060018D4 RID: 6356 RVA: 0x00062014 File Offset: 0x00060214
	public static void BindTiming()
	{
		Interpolation.BindTiming(Interpolation._delayMillis, Interpolation._ratio, Interpolation._sendRate);
	}

	// Token: 0x060018D5 RID: 6357 RVA: 0x0006202C File Offset: 0x0006022C
	public static void BindTimingNetCull()
	{
		Interpolation.BindTiming(Interpolation._delayMillis, Interpolation._ratio, NetCull.sendRate);
	}

	// Token: 0x060018D6 RID: 6358 RVA: 0x00062044 File Offset: 0x00060244
	public static Interpolation.TimingData Capture()
	{
		return new Interpolation.TimingData(Interpolation._ratio, Interpolation._deltaSeconds, Interpolation._totalDelaySeconds, Interpolation._delaySeconds, Interpolation._delayFromSendRateSeconds, Interpolation._totalDelayMillis, Interpolation._delayFromSendRateMillis, Interpolation._delayMillis, Interpolation._sendRate);
	}

	// Token: 0x17000733 RID: 1843
	// (get) Token: 0x060018D7 RID: 6359 RVA: 0x00062084 File Offset: 0x00060284
	public static double time
	{
		get
		{
			return NetCull.time + Interpolation._deltaSeconds;
		}
	}

	// Token: 0x17000734 RID: 1844
	// (get) Token: 0x060018D8 RID: 6360 RVA: 0x00062094 File Offset: 0x00060294
	public static double localTime
	{
		get
		{
			return NetCull.localTime + Interpolation._deltaSeconds;
		}
	}

	// Token: 0x17000735 RID: 1845
	// (get) Token: 0x060018D9 RID: 6361 RVA: 0x000620A4 File Offset: 0x000602A4
	public static ulong timeInMillis
	{
		get
		{
			ulong num = NetCull.timeInMillis;
			if (num < Interpolation._totalDelayMillis)
			{
				num = 0UL;
			}
			else
			{
				num -= Interpolation._totalDelayMillis;
			}
			return num;
		}
	}

	// Token: 0x17000736 RID: 1846
	// (get) Token: 0x060018DA RID: 6362 RVA: 0x000620D4 File Offset: 0x000602D4
	public static ulong localTimeInMillis
	{
		get
		{
			ulong num = NetCull.localTimeInMillis;
			if (num < Interpolation._totalDelayMillis)
			{
				num = 0UL;
			}
			else
			{
				num -= Interpolation._totalDelayMillis;
			}
			return num;
		}
	}

	// Token: 0x04000D2A RID: 3370
	private const float kDefaultSendRateRatio = 1.5f;

	// Token: 0x04000D2B RID: 3371
	private const int kDefaultDelayMillis = 20;

	// Token: 0x04000D2C RID: 3372
	private const float kDefaultSendRate = 5f;

	// Token: 0x04000D2D RID: 3373
	private static double _ratio;

	// Token: 0x04000D2E RID: 3374
	private static ulong _totalDelayMillis;

	// Token: 0x04000D2F RID: 3375
	private static ulong _delayFromSendRateMillis;

	// Token: 0x04000D30 RID: 3376
	private static ulong _delayMillis;

	// Token: 0x04000D31 RID: 3377
	private static double _delaySeconds;

	// Token: 0x04000D32 RID: 3378
	private static double _totalDelaySeconds;

	// Token: 0x04000D33 RID: 3379
	private static double _deltaSeconds;

	// Token: 0x04000D34 RID: 3380
	private static double _delayFromSendRateSeconds;

	// Token: 0x04000D35 RID: 3381
	private static float _sendRate;

	// Token: 0x04000D36 RID: 3382
	public static Interpolation.TimingData @struct;

	// Token: 0x020002B4 RID: 692
	public struct TimingData
	{
		// Token: 0x060018DB RID: 6363 RVA: 0x00062104 File Offset: 0x00060304
		public TimingData(double sendRateRatio, double deltaSeconds, double totalDelaySeconds, double delaySeconds, double delayFromSendRateSeconds, ulong totalDelayMillis, ulong delayFromSendRateMillis, ulong delayMillis, float sendRate)
		{
			this.sendRateRatio = sendRateRatio;
			this.deltaSeconds = deltaSeconds;
			this.totalDelaySeconds = totalDelaySeconds;
			this.delaySeconds = delaySeconds;
			this.delayFromSendRateSeconds = delayFromSendRateSeconds;
			this.totalDelayMillis = totalDelayMillis;
			this.delayFromSendRateMillis = delayFromSendRateMillis;
			this.delayMillis = delayMillis;
			this.sendRate = sendRate;
			this.sendRateRatioF = (float)sendRateRatio;
			this.deltaSecondsF = (float)deltaSeconds;
			this.totalDelaySecondsF = (float)totalDelaySeconds;
			this.delaySecondsF = (float)delaySeconds;
			this.delayFromSendRateSecondsF = (float)delayFromSendRateSeconds;
		}

		// Token: 0x04000D37 RID: 3383
		public readonly double sendRateRatio;

		// Token: 0x04000D38 RID: 3384
		public readonly double deltaSeconds;

		// Token: 0x04000D39 RID: 3385
		public readonly double totalDelaySeconds;

		// Token: 0x04000D3A RID: 3386
		public readonly double delaySeconds;

		// Token: 0x04000D3B RID: 3387
		public readonly double delayFromSendRateSeconds;

		// Token: 0x04000D3C RID: 3388
		public readonly float sendRateRatioF;

		// Token: 0x04000D3D RID: 3389
		public readonly float deltaSecondsF;

		// Token: 0x04000D3E RID: 3390
		public readonly float totalDelaySecondsF;

		// Token: 0x04000D3F RID: 3391
		public readonly float delaySecondsF;

		// Token: 0x04000D40 RID: 3392
		public readonly float delayFromSendRateSecondsF;

		// Token: 0x04000D41 RID: 3393
		public readonly ulong totalDelayMillis;

		// Token: 0x04000D42 RID: 3394
		public readonly ulong delayFromSendRateMillis;

		// Token: 0x04000D43 RID: 3395
		public readonly ulong delayMillis;

		// Token: 0x04000D44 RID: 3396
		public readonly float sendRate;
	}
}
