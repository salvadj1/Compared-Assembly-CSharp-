using System;
using UnityEngine;

namespace Facepunch.Clocks.Counters.Unity
{
	// Token: 0x020003F7 RID: 1015
	public struct ScaledTime
	{
		// Token: 0x0600235D RID: 9053 RVA: 0x00082E40 File Offset: 0x00081040
		public void Start()
		{
			if (float.IsNegativeInfinity(this.startTime))
			{
				this.startTime = ScaledTime.TIME_SOURCE.NOW;
				this.deductSeconds = 0.0;
				this.endTime = float.PositiveInfinity;
			}
			else if (!float.IsPositiveInfinity(this.endTime))
			{
				float num = this.endTime;
				this.endTime = float.PositiveInfinity;
				this.deductSeconds += (double)ScaledTime.TIME_SOURCE.NOW - (double)num;
			}
		}

		// Token: 0x0600235E RID: 9054 RVA: 0x00082EC0 File Offset: 0x000810C0
		public void Stop()
		{
			if (float.IsNegativeInfinity(this.startTime))
			{
				return;
			}
			if (float.IsPositiveInfinity(this.endTime))
			{
				this.endTime = ScaledTime.TIME_SOURCE.NOW;
			}
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x0600235F RID: 9055 RVA: 0x00082EFC File Offset: 0x000810FC
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
					return (double)ScaledTime.TIME_SOURCE.NOW - this.deductSeconds - (double)this.startTime;
				}
				return (double)this.endTime - this.deductSeconds - (double)this.startTime;
			}
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x06002360 RID: 9056 RVA: 0x00082F64 File Offset: 0x00081164
		public long ElapsedMilliseconds
		{
			get
			{
				return (long)Math.Floor(this.ElapsedSeconds * 1000.0);
			}
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x06002361 RID: 9057 RVA: 0x00082F7C File Offset: 0x0008117C
		public TimeSpan Elapsed
		{
			get
			{
				if (float.IsNegativeInfinity(this.startTime))
				{
					return TimeSpan.Zero;
				}
				return TimeSpan.FromSeconds((double)((!float.IsPositiveInfinity(this.endTime)) ? this.endTime : ScaledTime.TIME_SOURCE.NOW) - this.deductSeconds - (double)this.startTime);
			}
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06002362 RID: 9058 RVA: 0x00082FD4 File Offset: 0x000811D4
		public bool IsRunning
		{
			get
			{
				return float.IsPositiveInfinity(this.endTime) && !float.IsNegativeInfinity(this.startTime);
			}
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06002363 RID: 9059 RVA: 0x00082FF8 File Offset: 0x000811F8
		public static ScaledTime Restart
		{
			get
			{
				ScaledTime result;
				result.deductSeconds = 0.0;
				result.endTime = float.PositiveInfinity;
				result.startTime = ScaledTime.TIME_SOURCE.NOW;
				return result;
			}
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06002364 RID: 9060 RVA: 0x00083030 File Offset: 0x00081230
		public static ScaledTime Reset
		{
			get
			{
				ScaledTime result;
				result.deductSeconds = 0.0;
				result.endTime = float.PositiveInfinity;
				result.startTime = float.NegativeInfinity;
				return result;
			}
		}

		// Token: 0x040010B6 RID: 4278
		private const double ZeroDeductions = 0.0;

		// Token: 0x040010B7 RID: 4279
		private const double OneThousand = 1000.0;

		// Token: 0x040010B8 RID: 4280
		private const double ZeroElapsed = 0.0;

		// Token: 0x040010B9 RID: 4281
		private float startTime;

		// Token: 0x040010BA RID: 4282
		private float endTime;

		// Token: 0x040010BB RID: 4283
		private double deductSeconds;

		// Token: 0x020003F8 RID: 1016
		private static class TIME_SOURCE
		{
			// Token: 0x17000867 RID: 2151
			// (get) Token: 0x06002365 RID: 9061 RVA: 0x00083068 File Offset: 0x00081268
			public static float NOW
			{
				get
				{
					return Time.time;
				}
			}
		}
	}
}
