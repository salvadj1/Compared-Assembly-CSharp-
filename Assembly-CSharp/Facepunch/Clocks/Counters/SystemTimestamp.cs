using System;
using System.Diagnostics;
using UnityEngine;

namespace Facepunch.Clocks.Counters
{
	// Token: 0x020003EE RID: 1006
	public struct SystemTimestamp
	{
		// Token: 0x06002318 RID: 8984 RVA: 0x00082000 File Offset: 0x00080200
		public void Start()
		{
			if (double.IsNegativeInfinity(this.startTime))
			{
				this.startTime = SystemTimestamp.TIME_SOURCE.NOW;
				this.deductSeconds = 0.0;
				this.endTime = double.PositiveInfinity;
			}
			else if (!double.IsPositiveInfinity(this.endTime))
			{
				double num = this.endTime;
				this.endTime = double.PositiveInfinity;
				this.deductSeconds += SystemTimestamp.TIME_SOURCE.NOW - num;
			}
		}

		// Token: 0x06002319 RID: 8985 RVA: 0x00082088 File Offset: 0x00080288
		public void Stop()
		{
			if (double.IsNegativeInfinity(this.startTime))
			{
				return;
			}
			if (double.IsPositiveInfinity(this.endTime))
			{
				this.endTime = SystemTimestamp.TIME_SOURCE.NOW;
			}
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x0600231A RID: 8986 RVA: 0x000820C4 File Offset: 0x000802C4
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
					return SystemTimestamp.TIME_SOURCE.NOW - this.deductSeconds - this.startTime;
				}
				return this.endTime - this.deductSeconds - this.startTime;
			}
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x0600231B RID: 8987 RVA: 0x00082128 File Offset: 0x00080328
		public long ElapsedMilliseconds
		{
			get
			{
				return (long)Math.Floor(this.ElapsedSeconds * 1000.0);
			}
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x0600231C RID: 8988 RVA: 0x00082140 File Offset: 0x00080340
		public TimeSpan Elapsed
		{
			get
			{
				if (double.IsNegativeInfinity(this.startTime))
				{
					return TimeSpan.Zero;
				}
				return TimeSpan.FromSeconds(((!double.IsPositiveInfinity(this.endTime)) ? this.endTime : SystemTimestamp.TIME_SOURCE.NOW) - this.deductSeconds - this.startTime);
			}
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x0600231D RID: 8989 RVA: 0x00082198 File Offset: 0x00080398
		public bool IsRunning
		{
			get
			{
				return double.IsPositiveInfinity(this.endTime) && !double.IsNegativeInfinity(this.startTime);
			}
		}

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x0600231E RID: 8990 RVA: 0x000821BC File Offset: 0x000803BC
		public static SystemTimestamp Restart
		{
			get
			{
				SystemTimestamp result;
				result.deductSeconds = 0.0;
				result.endTime = double.PositiveInfinity;
				result.startTime = SystemTimestamp.TIME_SOURCE.NOW;
				return result;
			}
		}

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x0600231F RID: 8991 RVA: 0x000821F8 File Offset: 0x000803F8
		public static SystemTimestamp Reset
		{
			get
			{
				SystemTimestamp result;
				result.deductSeconds = 0.0;
				result.endTime = double.PositiveInfinity;
				result.startTime = double.NegativeInfinity;
				return result;
			}
		}

		// Token: 0x04001098 RID: 4248
		private const double ZeroDeductions = 0.0;

		// Token: 0x04001099 RID: 4249
		private const double OneThousand = 1000.0;

		// Token: 0x0400109A RID: 4250
		private const double ZeroElapsed = 0.0;

		// Token: 0x0400109B RID: 4251
		private double startTime;

		// Token: 0x0400109C RID: 4252
		private double endTime;

		// Token: 0x0400109D RID: 4253
		private double deductSeconds;

		// Token: 0x020003EF RID: 1007
		private static class TIME_SOURCE
		{
			// Token: 0x06002320 RID: 8992 RVA: 0x00082238 File Offset: 0x00080438
			static TIME_SOURCE()
			{
				SystemTimestamp.TIME_SOURCE.ToSeconds = (double)(1m / SystemTimestamp.TIME_SOURCE.Frequency);
				string text = string.Format("SystemTimestampWatch settings={{IsHighResolution={0},Frequency={1},ToSecond={2}}}", SystemTimestamp.TIME_SOURCE.IsHighResolution, SystemTimestamp.TIME_SOURCE.Frequency, SystemTimestamp.TIME_SOURCE.ToSeconds);
				if (!SystemTimestamp.TIME_SOURCE.IsHighResolution)
				{
					Debug.LogWarning(text);
				}
			}

			// Token: 0x17000841 RID: 2113
			// (get) Token: 0x06002321 RID: 8993 RVA: 0x000822BC File Offset: 0x000804BC
			public static double NOW
			{
				get
				{
					return (double)(Stopwatch.GetTimestamp() - SystemTimestamp.TIME_SOURCE.ThenTimestamp) * SystemTimestamp.TIME_SOURCE.ToSeconds;
				}
			}

			// Token: 0x0400109E RID: 4254
			private static readonly long ThenTimestamp = Stopwatch.GetTimestamp();

			// Token: 0x0400109F RID: 4255
			private static readonly long Frequency = Stopwatch.Frequency;

			// Token: 0x040010A0 RID: 4256
			private static readonly double ToSeconds;

			// Token: 0x040010A1 RID: 4257
			private static readonly bool IsHighResolution = Stopwatch.IsHighResolution;
		}
	}
}
