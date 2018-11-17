using System;
using UnityEngine;

namespace Facepunch.Clocks.Counters.Unity
{
	// Token: 0x020003F1 RID: 1009
	public struct FixedTime
	{
		// Token: 0x06002342 RID: 9026 RVA: 0x000827A0 File Offset: 0x000809A0
		public void Start()
		{
			if (float.IsNegativeInfinity(this.startTime))
			{
				this.startTime = FixedTime.TIME_SOURCE.NOW;
				this.deductSeconds = 0.0;
				this.endTime = float.PositiveInfinity;
			}
			else if (!float.IsPositiveInfinity(this.endTime))
			{
				float num = this.endTime;
				this.endTime = float.PositiveInfinity;
				this.deductSeconds += (double)FixedTime.TIME_SOURCE.NOW - (double)num;
			}
		}

		// Token: 0x06002343 RID: 9027 RVA: 0x00082820 File Offset: 0x00080A20
		public void Stop()
		{
			if (float.IsNegativeInfinity(this.startTime))
			{
				return;
			}
			if (float.IsPositiveInfinity(this.endTime))
			{
				this.endTime = FixedTime.TIME_SOURCE.NOW;
			}
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06002344 RID: 9028 RVA: 0x0008285C File Offset: 0x00080A5C
		public double ElapsedSeconds
		{
			get
			{
				if (float.IsNegativeInfinity(this.startTime))
				{
					return 0.0;
				}
				if (float.IsPositiveInfinity(this.endTime))
				{
					return (double)FixedTime.TIME_SOURCE.NOW - this.deductSeconds - (double)this.startTime;
				}
				return (double)this.endTime - this.deductSeconds - (double)this.startTime;
			}
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06002345 RID: 9029 RVA: 0x000828C4 File Offset: 0x00080AC4
		public long ElapsedMilliseconds
		{
			get
			{
				return (long)Math.Floor(this.ElapsedSeconds * 1000.0);
			}
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06002346 RID: 9030 RVA: 0x000828DC File Offset: 0x00080ADC
		public TimeSpan Elapsed
		{
			get
			{
				if (float.IsNegativeInfinity(this.startTime))
				{
					return TimeSpan.Zero;
				}
				return TimeSpan.FromSeconds((double)((!float.IsPositiveInfinity(this.endTime)) ? this.endTime : FixedTime.TIME_SOURCE.NOW) - this.deductSeconds - (double)this.startTime);
			}
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x06002347 RID: 9031 RVA: 0x00082934 File Offset: 0x00080B34
		public bool IsRunning
		{
			get
			{
				return float.IsPositiveInfinity(this.endTime) && !float.IsNegativeInfinity(this.startTime);
			}
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06002348 RID: 9032 RVA: 0x00082958 File Offset: 0x00080B58
		public static FixedTime Restart
		{
			get
			{
				FixedTime result;
				result.deductSeconds = 0.0;
				result.endTime = float.PositiveInfinity;
				result.startTime = FixedTime.TIME_SOURCE.NOW;
				return result;
			}
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x06002349 RID: 9033 RVA: 0x00082990 File Offset: 0x00080B90
		public static FixedTime Reset
		{
			get
			{
				FixedTime result;
				result.deductSeconds = 0.0;
				result.endTime = float.PositiveInfinity;
				result.startTime = float.NegativeInfinity;
				return result;
			}
		}

		// Token: 0x040010A4 RID: 4260
		private const double ZeroDeductions = 0.0;

		// Token: 0x040010A5 RID: 4261
		private const double OneThousand = 1000.0;

		// Token: 0x040010A6 RID: 4262
		private const double ZeroElapsed = 0.0;

		// Token: 0x040010A7 RID: 4263
		private float startTime;

		// Token: 0x040010A8 RID: 4264
		private float endTime;

		// Token: 0x040010A9 RID: 4265
		private double deductSeconds;

		// Token: 0x020003F2 RID: 1010
		private static class TIME_SOURCE
		{
			// Token: 0x17000852 RID: 2130
			// (get) Token: 0x0600234A RID: 9034 RVA: 0x000829C8 File Offset: 0x00080BC8
			public static float NOW
			{
				get
				{
					return Time.fixedTime;
				}
			}
		}
	}
}
