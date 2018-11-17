using System;

namespace Facepunch.Clocks.Counters
{
	// Token: 0x020003EC RID: 1004
	public struct DateTimeWatch
	{
		// Token: 0x0600230E RID: 8974 RVA: 0x00081D44 File Offset: 0x0007FF44
		public void Start()
		{
			if (double.IsNegativeInfinity(this.startTime))
			{
				this.startTime = DateTimeWatch.TIME_SOURCE.NOW;
				this.deductSeconds = 0.0;
				this.endTime = double.PositiveInfinity;
			}
			else if (!double.IsPositiveInfinity(this.endTime))
			{
				double num = this.endTime;
				this.endTime = double.PositiveInfinity;
				this.deductSeconds += DateTimeWatch.TIME_SOURCE.NOW - num;
			}
		}

		// Token: 0x0600230F RID: 8975 RVA: 0x00081DCC File Offset: 0x0007FFCC
		public void Stop()
		{
			if (double.IsNegativeInfinity(this.startTime))
			{
				return;
			}
			if (double.IsPositiveInfinity(this.endTime))
			{
				this.endTime = DateTimeWatch.TIME_SOURCE.NOW;
			}
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06002310 RID: 8976 RVA: 0x00081E08 File Offset: 0x00080008
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
					return DateTimeWatch.TIME_SOURCE.NOW - this.deductSeconds - this.startTime;
				}
				return this.endTime - this.deductSeconds - this.startTime;
			}
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x06002311 RID: 8977 RVA: 0x00081E6C File Offset: 0x0008006C
		public long ElapsedMilliseconds
		{
			get
			{
				return (long)Math.Floor(this.ElapsedSeconds * 1000.0);
			}
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x06002312 RID: 8978 RVA: 0x00081E84 File Offset: 0x00080084
		public TimeSpan Elapsed
		{
			get
			{
				if (double.IsNegativeInfinity(this.startTime))
				{
					return TimeSpan.Zero;
				}
				return TimeSpan.FromSeconds(((!double.IsPositiveInfinity(this.endTime)) ? this.endTime : DateTimeWatch.TIME_SOURCE.NOW) - this.deductSeconds - this.startTime);
			}
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06002313 RID: 8979 RVA: 0x00081EDC File Offset: 0x000800DC
		public bool IsRunning
		{
			get
			{
				return double.IsPositiveInfinity(this.endTime) && !double.IsNegativeInfinity(this.startTime);
			}
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x06002314 RID: 8980 RVA: 0x00081F00 File Offset: 0x00080100
		public static DateTimeWatch Restart
		{
			get
			{
				DateTimeWatch result;
				result.deductSeconds = 0.0;
				result.endTime = double.PositiveInfinity;
				result.startTime = DateTimeWatch.TIME_SOURCE.NOW;
				return result;
			}
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06002315 RID: 8981 RVA: 0x00081F3C File Offset: 0x0008013C
		public static DateTimeWatch Reset
		{
			get
			{
				DateTimeWatch result;
				result.deductSeconds = 0.0;
				result.endTime = double.PositiveInfinity;
				result.startTime = double.NegativeInfinity;
				return result;
			}
		}

		// Token: 0x0400108F RID: 4239
		private const double ZeroDeductions = 0.0;

		// Token: 0x04001090 RID: 4240
		private const double OneThousand = 1000.0;

		// Token: 0x04001091 RID: 4241
		private const double ZeroElapsed = 0.0;

		// Token: 0x04001092 RID: 4242
		private double startTime;

		// Token: 0x04001093 RID: 4243
		private double endTime;

		// Token: 0x04001094 RID: 4244
		private double deductSeconds;

		// Token: 0x020003ED RID: 1005
		private static class TIME_SOURCE
		{
			// Token: 0x1700083A RID: 2106
			// (get) Token: 0x06002317 RID: 8983 RVA: 0x00081FBC File Offset: 0x000801BC
			public static double NOW
			{
				get
				{
					return (double)((DateTime.Now.Ticks - DateTimeWatch.TIME_SOURCE.ThenTicks) * 0.0000001000000000000000000000m);
				}
			}

			// Token: 0x04001095 RID: 4245
			private const decimal kTickToSecond = 0.0000001000000000000000000000m;

			// Token: 0x04001096 RID: 4246
			public static readonly DateTime Then = DateTime.Now;

			// Token: 0x04001097 RID: 4247
			public static readonly long ThenTicks = DateTimeWatch.TIME_SOURCE.Then.Ticks;
		}
	}
}
