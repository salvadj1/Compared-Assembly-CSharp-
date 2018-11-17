using System;
using uLink;

namespace Facepunch.Clocks.Counters.uLink
{
	// Token: 0x020003FD RID: 1021
	public struct NetworkTime
	{
		// Token: 0x06002378 RID: 9080 RVA: 0x000834E0 File Offset: 0x000816E0
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

		// Token: 0x06002379 RID: 9081 RVA: 0x00083568 File Offset: 0x00081768
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

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x0600237A RID: 9082 RVA: 0x000835A4 File Offset: 0x000817A4
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

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x0600237B RID: 9083 RVA: 0x00083608 File Offset: 0x00081808
		public long ElapsedMilliseconds
		{
			get
			{
				return (long)Math.Floor(this.ElapsedSeconds * 1000.0);
			}
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x0600237C RID: 9084 RVA: 0x00083620 File Offset: 0x00081820
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

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x0600237D RID: 9085 RVA: 0x00083678 File Offset: 0x00081878
		public bool IsRunning
		{
			get
			{
				return double.IsPositiveInfinity(this.endTime) && !double.IsNegativeInfinity(this.startTime);
			}
		}

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x0600237E RID: 9086 RVA: 0x0008369C File Offset: 0x0008189C
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

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x0600237F RID: 9087 RVA: 0x000836D8 File Offset: 0x000818D8
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

		// Token: 0x040010C8 RID: 4296
		private const double ZeroDeductions = 0.0;

		// Token: 0x040010C9 RID: 4297
		private const double OneThousand = 1000.0;

		// Token: 0x040010CA RID: 4298
		private const double ZeroElapsed = 0.0;

		// Token: 0x040010CB RID: 4299
		private double startTime;

		// Token: 0x040010CC RID: 4300
		private double endTime;

		// Token: 0x040010CD RID: 4301
		private double deductSeconds;

		// Token: 0x020003FE RID: 1022
		private static class TIME_SOURCE
		{
			// Token: 0x1700087C RID: 2172
			// (get) Token: 0x06002380 RID: 9088 RVA: 0x00083718 File Offset: 0x00081918
			public static double NOW
			{
				get
				{
					return uLink.Network.time;
				}
			}
		}
	}
}
