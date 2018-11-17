using System;
using UnityEngine;

namespace Facepunch.Clocks.Counters.Unity
{
	// Token: 0x020003F5 RID: 1013
	public struct RealtimeSinceStartup
	{
		// Token: 0x06002354 RID: 9044 RVA: 0x00082C10 File Offset: 0x00080E10
		public void Start()
		{
			if (float.IsNegativeInfinity(this.startTime))
			{
				this.startTime = RealtimeSinceStartup.TIME_SOURCE.NOW;
				this.deductSeconds = 0.0;
				this.endTime = float.PositiveInfinity;
			}
			else if (!float.IsPositiveInfinity(this.endTime))
			{
				float num = this.endTime;
				this.endTime = float.PositiveInfinity;
				this.deductSeconds += (double)RealtimeSinceStartup.TIME_SOURCE.NOW - (double)num;
			}
		}

		// Token: 0x06002355 RID: 9045 RVA: 0x00082C90 File Offset: 0x00080E90
		public void Stop()
		{
			if (float.IsNegativeInfinity(this.startTime))
			{
				return;
			}
			if (float.IsPositiveInfinity(this.endTime))
			{
				this.endTime = RealtimeSinceStartup.TIME_SOURCE.NOW;
			}
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x06002356 RID: 9046 RVA: 0x00082CCC File Offset: 0x00080ECC
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
					return (double)RealtimeSinceStartup.TIME_SOURCE.NOW - this.deductSeconds - (double)this.startTime;
				}
				return (double)this.endTime - this.deductSeconds - (double)this.startTime;
			}
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06002357 RID: 9047 RVA: 0x00082D34 File Offset: 0x00080F34
		public long ElapsedMilliseconds
		{
			get
			{
				return (long)Math.Floor(this.ElapsedSeconds * 1000.0);
			}
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06002358 RID: 9048 RVA: 0x00082D4C File Offset: 0x00080F4C
		public TimeSpan Elapsed
		{
			get
			{
				if (float.IsNegativeInfinity(this.startTime))
				{
					return TimeSpan.Zero;
				}
				return TimeSpan.FromSeconds((double)((!float.IsPositiveInfinity(this.endTime)) ? this.endTime : RealtimeSinceStartup.TIME_SOURCE.NOW) - this.deductSeconds - (double)this.startTime);
			}
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06002359 RID: 9049 RVA: 0x00082DA4 File Offset: 0x00080FA4
		public bool IsRunning
		{
			get
			{
				return float.IsPositiveInfinity(this.endTime) && !float.IsNegativeInfinity(this.startTime);
			}
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x0600235A RID: 9050 RVA: 0x00082DC8 File Offset: 0x00080FC8
		public static RealtimeSinceStartup Restart
		{
			get
			{
				RealtimeSinceStartup result;
				result.deductSeconds = 0.0;
				result.endTime = float.PositiveInfinity;
				result.startTime = RealtimeSinceStartup.TIME_SOURCE.NOW;
				return result;
			}
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x0600235B RID: 9051 RVA: 0x00082E00 File Offset: 0x00081000
		public static RealtimeSinceStartup Reset
		{
			get
			{
				RealtimeSinceStartup result;
				result.deductSeconds = 0.0;
				result.endTime = float.PositiveInfinity;
				result.startTime = float.NegativeInfinity;
				return result;
			}
		}

		// Token: 0x040010B0 RID: 4272
		private const double ZeroDeductions = 0.0;

		// Token: 0x040010B1 RID: 4273
		private const double OneThousand = 1000.0;

		// Token: 0x040010B2 RID: 4274
		private const double ZeroElapsed = 0.0;

		// Token: 0x040010B3 RID: 4275
		private float startTime;

		// Token: 0x040010B4 RID: 4276
		private float endTime;

		// Token: 0x040010B5 RID: 4277
		private double deductSeconds;

		// Token: 0x020003F6 RID: 1014
		private static class TIME_SOURCE
		{
			// Token: 0x17000860 RID: 2144
			// (get) Token: 0x0600235C RID: 9052 RVA: 0x00082E38 File Offset: 0x00081038
			public static float NOW
			{
				get
				{
					return Time.realtimeSinceStartup;
				}
			}
		}
	}
}
