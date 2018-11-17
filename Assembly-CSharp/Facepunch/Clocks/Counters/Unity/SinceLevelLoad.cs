using System;
using UnityEngine;

namespace Facepunch.Clocks.Counters.Unity
{
	// Token: 0x020003F9 RID: 1017
	public struct SinceLevelLoad
	{
		// Token: 0x06002366 RID: 9062 RVA: 0x00083070 File Offset: 0x00081270
		public void Start()
		{
			if (float.IsNegativeInfinity(this.startTime))
			{
				this.startTime = SinceLevelLoad.TIME_SOURCE.NOW;
				this.deductSeconds = 0.0;
				this.endTime = float.PositiveInfinity;
			}
			else if (!float.IsPositiveInfinity(this.endTime))
			{
				float num = this.endTime;
				this.endTime = float.PositiveInfinity;
				this.deductSeconds += (double)SinceLevelLoad.TIME_SOURCE.NOW - (double)num;
			}
		}

		// Token: 0x06002367 RID: 9063 RVA: 0x000830F0 File Offset: 0x000812F0
		public void Stop()
		{
			if (float.IsNegativeInfinity(this.startTime))
			{
				return;
			}
			if (float.IsPositiveInfinity(this.endTime))
			{
				this.endTime = SinceLevelLoad.TIME_SOURCE.NOW;
			}
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06002368 RID: 9064 RVA: 0x0008312C File Offset: 0x0008132C
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
					return (double)SinceLevelLoad.TIME_SOURCE.NOW - this.deductSeconds - (double)this.startTime;
				}
				return (double)this.endTime - this.deductSeconds - (double)this.startTime;
			}
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06002369 RID: 9065 RVA: 0x00083194 File Offset: 0x00081394
		public long ElapsedMilliseconds
		{
			get
			{
				return (long)Math.Floor(this.ElapsedSeconds * 1000.0);
			}
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x0600236A RID: 9066 RVA: 0x000831AC File Offset: 0x000813AC
		public TimeSpan Elapsed
		{
			get
			{
				if (float.IsNegativeInfinity(this.startTime))
				{
					return TimeSpan.Zero;
				}
				return TimeSpan.FromSeconds((double)((!float.IsPositiveInfinity(this.endTime)) ? this.endTime : SinceLevelLoad.TIME_SOURCE.NOW) - this.deductSeconds - (double)this.startTime);
			}
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x0600236B RID: 9067 RVA: 0x00083204 File Offset: 0x00081404
		public bool IsRunning
		{
			get
			{
				return float.IsPositiveInfinity(this.endTime) && !float.IsNegativeInfinity(this.startTime);
			}
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x0600236C RID: 9068 RVA: 0x00083228 File Offset: 0x00081428
		public static SinceLevelLoad Restart
		{
			get
			{
				SinceLevelLoad result;
				result.deductSeconds = 0.0;
				result.endTime = float.PositiveInfinity;
				result.startTime = SinceLevelLoad.TIME_SOURCE.NOW;
				return result;
			}
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x0600236D RID: 9069 RVA: 0x00083260 File Offset: 0x00081460
		public static SinceLevelLoad Reset
		{
			get
			{
				SinceLevelLoad result;
				result.deductSeconds = 0.0;
				result.endTime = float.PositiveInfinity;
				result.startTime = float.NegativeInfinity;
				return result;
			}
		}

		// Token: 0x040010BC RID: 4284
		private const double ZeroDeductions = 0.0;

		// Token: 0x040010BD RID: 4285
		private const double OneThousand = 1000.0;

		// Token: 0x040010BE RID: 4286
		private const double ZeroElapsed = 0.0;

		// Token: 0x040010BF RID: 4287
		private float startTime;

		// Token: 0x040010C0 RID: 4288
		private float endTime;

		// Token: 0x040010C1 RID: 4289
		private double deductSeconds;

		// Token: 0x020003FA RID: 1018
		private static class TIME_SOURCE
		{
			// Token: 0x1700086E RID: 2158
			// (get) Token: 0x0600236E RID: 9070 RVA: 0x00083298 File Offset: 0x00081498
			public static float NOW
			{
				get
				{
					return Time.timeSinceLevelLoad;
				}
			}
		}
	}
}
