using System;
using UnityEngine;

namespace Facepunch.Clocks.Counters.Unity
{
	// Token: 0x0200034C RID: 844
	public struct SinceLevelLoad
	{
		// Token: 0x06002004 RID: 8196 RVA: 0x0007DC74 File Offset: 0x0007BE74
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

		// Token: 0x06002005 RID: 8197 RVA: 0x0007DCF4 File Offset: 0x0007BEF4
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

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x06002006 RID: 8198 RVA: 0x0007DD30 File Offset: 0x0007BF30
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

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x06002007 RID: 8199 RVA: 0x0007DD98 File Offset: 0x0007BF98
		public long ElapsedMilliseconds
		{
			get
			{
				return (long)Math.Floor(this.ElapsedSeconds * 1000.0);
			}
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x06002008 RID: 8200 RVA: 0x0007DDB0 File Offset: 0x0007BFB0
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

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06002009 RID: 8201 RVA: 0x0007DE08 File Offset: 0x0007C008
		public bool IsRunning
		{
			get
			{
				return float.IsPositiveInfinity(this.endTime) && !float.IsNegativeInfinity(this.startTime);
			}
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x0600200A RID: 8202 RVA: 0x0007DE2C File Offset: 0x0007C02C
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

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x0600200B RID: 8203 RVA: 0x0007DE64 File Offset: 0x0007C064
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

		// Token: 0x04000F56 RID: 3926
		private const double ZeroDeductions = 0.0;

		// Token: 0x04000F57 RID: 3927
		private const double OneThousand = 1000.0;

		// Token: 0x04000F58 RID: 3928
		private const double ZeroElapsed = 0.0;

		// Token: 0x04000F59 RID: 3929
		private float startTime;

		// Token: 0x04000F5A RID: 3930
		private float endTime;

		// Token: 0x04000F5B RID: 3931
		private double deductSeconds;

		// Token: 0x0200034D RID: 845
		private static class TIME_SOURCE
		{
			// Token: 0x17000810 RID: 2064
			// (get) Token: 0x0600200C RID: 8204 RVA: 0x0007DE9C File Offset: 0x0007C09C
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
