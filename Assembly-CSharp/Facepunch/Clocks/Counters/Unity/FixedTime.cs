using System;
using UnityEngine;

namespace Facepunch.Clocks.Counters.Unity
{
	// Token: 0x02000344 RID: 836
	public struct FixedTime
	{
		// Token: 0x06001FE0 RID: 8160 RVA: 0x0007D3A4 File Offset: 0x0007B5A4
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

		// Token: 0x06001FE1 RID: 8161 RVA: 0x0007D424 File Offset: 0x0007B624
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

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06001FE2 RID: 8162 RVA: 0x0007D460 File Offset: 0x0007B660
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

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06001FE3 RID: 8163 RVA: 0x0007D4C8 File Offset: 0x0007B6C8
		public long ElapsedMilliseconds
		{
			get
			{
				return (long)Math.Floor(this.ElapsedSeconds * 1000.0);
			}
		}

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x06001FE4 RID: 8164 RVA: 0x0007D4E0 File Offset: 0x0007B6E0
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

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06001FE5 RID: 8165 RVA: 0x0007D538 File Offset: 0x0007B738
		public bool IsRunning
		{
			get
			{
				return float.IsPositiveInfinity(this.endTime) && !float.IsNegativeInfinity(this.startTime);
			}
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06001FE6 RID: 8166 RVA: 0x0007D55C File Offset: 0x0007B75C
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

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x06001FE7 RID: 8167 RVA: 0x0007D594 File Offset: 0x0007B794
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

		// Token: 0x04000F3E RID: 3902
		private const double ZeroDeductions = 0.0;

		// Token: 0x04000F3F RID: 3903
		private const double OneThousand = 1000.0;

		// Token: 0x04000F40 RID: 3904
		private const double ZeroElapsed = 0.0;

		// Token: 0x04000F41 RID: 3905
		private float startTime;

		// Token: 0x04000F42 RID: 3906
		private float endTime;

		// Token: 0x04000F43 RID: 3907
		private double deductSeconds;

		// Token: 0x02000345 RID: 837
		private static class TIME_SOURCE
		{
			// Token: 0x170007F4 RID: 2036
			// (get) Token: 0x06001FE8 RID: 8168 RVA: 0x0007D5CC File Offset: 0x0007B7CC
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
