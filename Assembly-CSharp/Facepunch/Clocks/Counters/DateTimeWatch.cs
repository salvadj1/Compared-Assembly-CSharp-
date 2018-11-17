using System;

namespace Facepunch.Clocks.Counters
{
	// Token: 0x0200033F RID: 831
	public struct DateTimeWatch
	{
		// Token: 0x06001FAC RID: 8108 RVA: 0x0007C948 File Offset: 0x0007AB48
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

		// Token: 0x06001FAD RID: 8109 RVA: 0x0007C9D0 File Offset: 0x0007ABD0
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

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06001FAE RID: 8110 RVA: 0x0007CA0C File Offset: 0x0007AC0C
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

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06001FAF RID: 8111 RVA: 0x0007CA70 File Offset: 0x0007AC70
		public long ElapsedMilliseconds
		{
			get
			{
				return (long)Math.Floor(this.ElapsedSeconds * 1000.0);
			}
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06001FB0 RID: 8112 RVA: 0x0007CA88 File Offset: 0x0007AC88
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

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06001FB1 RID: 8113 RVA: 0x0007CAE0 File Offset: 0x0007ACE0
		public bool IsRunning
		{
			get
			{
				return double.IsPositiveInfinity(this.endTime) && !double.IsNegativeInfinity(this.startTime);
			}
		}

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06001FB2 RID: 8114 RVA: 0x0007CB04 File Offset: 0x0007AD04
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

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06001FB3 RID: 8115 RVA: 0x0007CB40 File Offset: 0x0007AD40
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

		// Token: 0x04000F29 RID: 3881
		private const double ZeroDeductions = 0.0;

		// Token: 0x04000F2A RID: 3882
		private const double OneThousand = 1000.0;

		// Token: 0x04000F2B RID: 3883
		private const double ZeroElapsed = 0.0;

		// Token: 0x04000F2C RID: 3884
		private double startTime;

		// Token: 0x04000F2D RID: 3885
		private double endTime;

		// Token: 0x04000F2E RID: 3886
		private double deductSeconds;

		// Token: 0x02000340 RID: 832
		private static class TIME_SOURCE
		{
			// Token: 0x170007DC RID: 2012
			// (get) Token: 0x06001FB5 RID: 8117 RVA: 0x0007CBC0 File Offset: 0x0007ADC0
			public static double NOW
			{
				get
				{
					return (double)((DateTime.Now.Ticks - DateTimeWatch.TIME_SOURCE.ThenTicks) * 0.0000001000000000000000000000m);
				}
			}

			// Token: 0x04000F2F RID: 3887
			private const decimal kTickToSecond = 0.0000001000000000000000000000m;

			// Token: 0x04000F30 RID: 3888
			public static readonly DateTime Then = DateTime.Now;

			// Token: 0x04000F31 RID: 3889
			public static readonly long ThenTicks = DateTimeWatch.TIME_SOURCE.Then.Ticks;
		}
	}
}
