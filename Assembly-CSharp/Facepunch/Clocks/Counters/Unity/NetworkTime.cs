using System;
using UnityEngine;

namespace Facepunch.Clocks.Counters.Unity
{
	// Token: 0x020003F3 RID: 1011
	public struct NetworkTime
	{
		// Token: 0x0600234B RID: 9035 RVA: 0x000829D0 File Offset: 0x00080BD0
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

		// Token: 0x0600234C RID: 9036 RVA: 0x00082A58 File Offset: 0x00080C58
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

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x0600234D RID: 9037 RVA: 0x00082A94 File Offset: 0x00080C94
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

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x0600234E RID: 9038 RVA: 0x00082AF8 File Offset: 0x00080CF8
		public long ElapsedMilliseconds
		{
			get
			{
				return (long)Math.Floor(this.ElapsedSeconds * 1000.0);
			}
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x0600234F RID: 9039 RVA: 0x00082B10 File Offset: 0x00080D10
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

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06002350 RID: 9040 RVA: 0x00082B68 File Offset: 0x00080D68
		public bool IsRunning
		{
			get
			{
				return double.IsPositiveInfinity(this.endTime) && !double.IsNegativeInfinity(this.startTime);
			}
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06002351 RID: 9041 RVA: 0x00082B8C File Offset: 0x00080D8C
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

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06002352 RID: 9042 RVA: 0x00082BC8 File Offset: 0x00080DC8
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

		// Token: 0x040010AA RID: 4266
		private const double ZeroDeductions = 0.0;

		// Token: 0x040010AB RID: 4267
		private const double OneThousand = 1000.0;

		// Token: 0x040010AC RID: 4268
		private const double ZeroElapsed = 0.0;

		// Token: 0x040010AD RID: 4269
		private double startTime;

		// Token: 0x040010AE RID: 4270
		private double endTime;

		// Token: 0x040010AF RID: 4271
		private double deductSeconds;

		// Token: 0x020003F4 RID: 1012
		private static class TIME_SOURCE
		{
			// Token: 0x17000859 RID: 2137
			// (get) Token: 0x06002353 RID: 9043 RVA: 0x00082C08 File Offset: 0x00080E08
			public static double NOW
			{
				get
				{
					return UnityEngine.Network.time;
				}
			}
		}
	}
}
