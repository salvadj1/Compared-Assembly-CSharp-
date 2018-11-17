using System;
using uLink;

namespace Facepunch.Clocks.Counters.uLink
{
	// Token: 0x02000350 RID: 848
	public struct NetworkTime
	{
		// Token: 0x06002016 RID: 8214 RVA: 0x0007E0E4 File Offset: 0x0007C2E4
		public void Start()
		{
			if (double.IsNegativeInfinity(this.startTime))
			{
				this.startTime = NetworkTime.TIME_SOURCE.NOW;
				this.deductSeconds = 0.0;
				this.endTime = double.PositiveInfinity;
			}
			else if (!double.IsPositiveInfinity(this.endTime))
			{
				double num = this.endTime;
				this.endTime = double.PositiveInfinity;
				this.deductSeconds += NetworkTime.TIME_SOURCE.NOW - num;
			}
		}

		// Token: 0x06002017 RID: 8215 RVA: 0x0007E16C File Offset: 0x0007C36C
		public void Stop()
		{
			if (double.IsNegativeInfinity(this.startTime))
			{
				return;
			}
			if (double.IsPositiveInfinity(this.endTime))
			{
				this.endTime = NetworkTime.TIME_SOURCE.NOW;
			}
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x06002018 RID: 8216 RVA: 0x0007E1A8 File Offset: 0x0007C3A8
		public double ElapsedSeconds
		{
			get
			{
				if (double.IsNegativeInfinity(this.startTime))
				{
					return 0.0;
				}
				if (double.IsPositiveInfinity(this.endTime))
				{
					return NetworkTime.TIME_SOURCE.NOW - this.deductSeconds - this.startTime;
				}
				return this.endTime - this.deductSeconds - this.startTime;
			}
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06002019 RID: 8217 RVA: 0x0007E20C File Offset: 0x0007C40C
		public long ElapsedMilliseconds
		{
			get
			{
				return (long)Math.Floor(this.ElapsedSeconds * 1000.0);
			}
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x0600201A RID: 8218 RVA: 0x0007E224 File Offset: 0x0007C424
		public TimeSpan Elapsed
		{
			get
			{
				if (double.IsNegativeInfinity(this.startTime))
				{
					return TimeSpan.Zero;
				}
				return TimeSpan.FromSeconds(((!double.IsPositiveInfinity(this.endTime)) ? this.endTime : NetworkTime.TIME_SOURCE.NOW) - this.deductSeconds - this.startTime);
			}
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x0600201B RID: 8219 RVA: 0x0007E27C File Offset: 0x0007C47C
		public bool IsRunning
		{
			get
			{
				return double.IsPositiveInfinity(this.endTime) && !double.IsNegativeInfinity(this.startTime);
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x0600201C RID: 8220 RVA: 0x0007E2A0 File Offset: 0x0007C4A0
		public static NetworkTime Restart
		{
			get
			{
				NetworkTime result;
				result.deductSeconds = 0.0;
				result.endTime = double.PositiveInfinity;
				result.startTime = NetworkTime.TIME_SOURCE.NOW;
				return result;
			}
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x0600201D RID: 8221 RVA: 0x0007E2DC File Offset: 0x0007C4DC
		public static NetworkTime Reset
		{
			get
			{
				NetworkTime result;
				result.deductSeconds = 0.0;
				result.endTime = double.PositiveInfinity;
				result.startTime = double.NegativeInfinity;
				return result;
			}
		}

		// Token: 0x04000F62 RID: 3938
		private const double ZeroDeductions = 0.0;

		// Token: 0x04000F63 RID: 3939
		private const double OneThousand = 1000.0;

		// Token: 0x04000F64 RID: 3940
		private const double ZeroElapsed = 0.0;

		// Token: 0x04000F65 RID: 3941
		private double startTime;

		// Token: 0x04000F66 RID: 3942
		private double endTime;

		// Token: 0x04000F67 RID: 3943
		private double deductSeconds;

		// Token: 0x02000351 RID: 849
		private static class TIME_SOURCE
		{
			// Token: 0x1700081E RID: 2078
			// (get) Token: 0x0600201E RID: 8222 RVA: 0x0007E31C File Offset: 0x0007C51C
			public static double NOW
			{
				get
				{
					return Network.time;
				}
			}
		}
	}
}
