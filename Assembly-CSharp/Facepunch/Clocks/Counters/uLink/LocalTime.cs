using System;
using uLink;

namespace Facepunch.Clocks.Counters.uLink
{
	// Token: 0x0200034E RID: 846
	public struct LocalTime
	{
		// Token: 0x0600200D RID: 8205 RVA: 0x0007DEA4 File Offset: 0x0007C0A4
		public void Start()
		{
			if (double.IsNegativeInfinity(this.startTime))
			{
				this.startTime = LocalTime.TIME_SOURCE.NOW;
				this.deductSeconds = 0.0;
				this.endTime = double.PositiveInfinity;
			}
			else if (!double.IsPositiveInfinity(this.endTime))
			{
				double num = this.endTime;
				this.endTime = double.PositiveInfinity;
				this.deductSeconds += LocalTime.TIME_SOURCE.NOW - num;
			}
		}

		// Token: 0x0600200E RID: 8206 RVA: 0x0007DF2C File Offset: 0x0007C12C
		public void Stop()
		{
			if (double.IsNegativeInfinity(this.startTime))
			{
				return;
			}
			if (double.IsPositiveInfinity(this.endTime))
			{
				this.endTime = LocalTime.TIME_SOURCE.NOW;
			}
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x0600200F RID: 8207 RVA: 0x0007DF68 File Offset: 0x0007C168
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
					return LocalTime.TIME_SOURCE.NOW - this.deductSeconds - this.startTime;
				}
				return this.endTime - this.deductSeconds - this.startTime;
			}
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x06002010 RID: 8208 RVA: 0x0007DFCC File Offset: 0x0007C1CC
		public long ElapsedMilliseconds
		{
			get
			{
				return (long)Math.Floor(this.ElapsedSeconds * 1000.0);
			}
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x06002011 RID: 8209 RVA: 0x0007DFE4 File Offset: 0x0007C1E4
		public TimeSpan Elapsed
		{
			get
			{
				if (double.IsNegativeInfinity(this.startTime))
				{
					return TimeSpan.Zero;
				}
				return TimeSpan.FromSeconds(((!double.IsPositiveInfinity(this.endTime)) ? this.endTime : LocalTime.TIME_SOURCE.NOW) - this.deductSeconds - this.startTime);
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06002012 RID: 8210 RVA: 0x0007E03C File Offset: 0x0007C23C
		public bool IsRunning
		{
			get
			{
				return double.IsPositiveInfinity(this.endTime) && !double.IsNegativeInfinity(this.startTime);
			}
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06002013 RID: 8211 RVA: 0x0007E060 File Offset: 0x0007C260
		public static LocalTime Restart
		{
			get
			{
				LocalTime result;
				result.deductSeconds = 0.0;
				result.endTime = double.PositiveInfinity;
				result.startTime = LocalTime.TIME_SOURCE.NOW;
				return result;
			}
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06002014 RID: 8212 RVA: 0x0007E09C File Offset: 0x0007C29C
		public static LocalTime Reset
		{
			get
			{
				LocalTime result;
				result.deductSeconds = 0.0;
				result.endTime = double.PositiveInfinity;
				result.startTime = double.NegativeInfinity;
				return result;
			}
		}

		// Token: 0x04000F5C RID: 3932
		private const double ZeroDeductions = 0.0;

		// Token: 0x04000F5D RID: 3933
		private const double OneThousand = 1000.0;

		// Token: 0x04000F5E RID: 3934
		private const double ZeroElapsed = 0.0;

		// Token: 0x04000F5F RID: 3935
		private double startTime;

		// Token: 0x04000F60 RID: 3936
		private double endTime;

		// Token: 0x04000F61 RID: 3937
		private double deductSeconds;

		// Token: 0x0200034F RID: 847
		private static class TIME_SOURCE
		{
			// Token: 0x17000817 RID: 2071
			// (get) Token: 0x06002015 RID: 8213 RVA: 0x0007E0DC File Offset: 0x0007C2DC
			public static double NOW
			{
				get
				{
					return Network.localTime;
				}
			}
		}
	}
}
