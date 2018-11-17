using System;
using UnityEngine;

namespace Facepunch.Clocks.Counters.Unity
{
	// Token: 0x02000348 RID: 840
	public struct RealtimeSinceStartup
	{
		// Token: 0x06001FF2 RID: 8178 RVA: 0x0007D814 File Offset: 0x0007BA14
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

		// Token: 0x06001FF3 RID: 8179 RVA: 0x0007D894 File Offset: 0x0007BA94
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

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06001FF4 RID: 8180 RVA: 0x0007D8D0 File Offset: 0x0007BAD0
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

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x06001FF5 RID: 8181 RVA: 0x0007D938 File Offset: 0x0007BB38
		public long ElapsedMilliseconds
		{
			get
			{
				return (long)Math.Floor(this.ElapsedSeconds * 1000.0);
			}
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06001FF6 RID: 8182 RVA: 0x0007D950 File Offset: 0x0007BB50
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

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06001FF7 RID: 8183 RVA: 0x0007D9A8 File Offset: 0x0007BBA8
		public bool IsRunning
		{
			get
			{
				return float.IsPositiveInfinity(this.endTime) && !float.IsNegativeInfinity(this.startTime);
			}
		}

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x06001FF8 RID: 8184 RVA: 0x0007D9CC File Offset: 0x0007BBCC
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

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x06001FF9 RID: 8185 RVA: 0x0007DA04 File Offset: 0x0007BC04
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

		// Token: 0x04000F4A RID: 3914
		private const double ZeroDeductions = 0.0;

		// Token: 0x04000F4B RID: 3915
		private const double OneThousand = 1000.0;

		// Token: 0x04000F4C RID: 3916
		private const double ZeroElapsed = 0.0;

		// Token: 0x04000F4D RID: 3917
		private float startTime;

		// Token: 0x04000F4E RID: 3918
		private float endTime;

		// Token: 0x04000F4F RID: 3919
		private double deductSeconds;

		// Token: 0x02000349 RID: 841
		private static class TIME_SOURCE
		{
			// Token: 0x17000802 RID: 2050
			// (get) Token: 0x06001FFA RID: 8186 RVA: 0x0007DA3C File Offset: 0x0007BC3C
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
