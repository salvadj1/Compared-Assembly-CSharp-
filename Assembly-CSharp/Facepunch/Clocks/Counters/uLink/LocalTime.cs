using System;
using uLink;

namespace Facepunch.Clocks.Counters.uLink
{
	// Token: 0x020003FB RID: 1019
	public struct LocalTime
	{
		// Token: 0x0600236F RID: 9071 RVA: 0x000832A0 File Offset: 0x000814A0
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

		// Token: 0x06002370 RID: 9072 RVA: 0x00083328 File Offset: 0x00081528
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

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06002371 RID: 9073 RVA: 0x00083364 File Offset: 0x00081564
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

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06002372 RID: 9074 RVA: 0x000833C8 File Offset: 0x000815C8
		public long ElapsedMilliseconds
		{
			get
			{
				return (long)Math.Floor(this.ElapsedSeconds * 1000.0);
			}
		}

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06002373 RID: 9075 RVA: 0x000833E0 File Offset: 0x000815E0
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

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x06002374 RID: 9076 RVA: 0x00083438 File Offset: 0x00081638
		public bool IsRunning
		{
			get
			{
				return double.IsPositiveInfinity(this.endTime) && !double.IsNegativeInfinity(this.startTime);
			}
		}

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06002375 RID: 9077 RVA: 0x0008345C File Offset: 0x0008165C
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

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x06002376 RID: 9078 RVA: 0x00083498 File Offset: 0x00081698
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

		// Token: 0x040010C2 RID: 4290
		private const double ZeroDeductions = 0.0;

		// Token: 0x040010C3 RID: 4291
		private const double OneThousand = 1000.0;

		// Token: 0x040010C4 RID: 4292
		private const double ZeroElapsed = 0.0;

		// Token: 0x040010C5 RID: 4293
		private double startTime;

		// Token: 0x040010C6 RID: 4294
		private double endTime;

		// Token: 0x040010C7 RID: 4295
		private double deductSeconds;

		// Token: 0x020003FC RID: 1020
		private static class TIME_SOURCE
		{
			// Token: 0x17000875 RID: 2165
			// (get) Token: 0x06002377 RID: 9079 RVA: 0x000834D8 File Offset: 0x000816D8
			public static double NOW
			{
				get
				{
					return uLink.Network.localTime;
				}
			}
		}
	}
}
