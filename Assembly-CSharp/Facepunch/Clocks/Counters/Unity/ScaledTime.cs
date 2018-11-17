using System;
using UnityEngine;

namespace Facepunch.Clocks.Counters.Unity
{
	// Token: 0x0200034A RID: 842
	public struct ScaledTime
	{
		// Token: 0x06001FFB RID: 8187 RVA: 0x0007DA44 File Offset: 0x0007BC44
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

		// Token: 0x06001FFC RID: 8188 RVA: 0x0007DAC4 File Offset: 0x0007BCC4
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

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06001FFD RID: 8189 RVA: 0x0007DB00 File Offset: 0x0007BD00
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

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06001FFE RID: 8190 RVA: 0x0007DB68 File Offset: 0x0007BD68
		public long ElapsedMilliseconds
		{
			get
			{
				return (long)Math.Floor(this.ElapsedSeconds * 1000.0);
			}
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x06001FFF RID: 8191 RVA: 0x0007DB80 File Offset: 0x0007BD80
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

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x06002000 RID: 8192 RVA: 0x0007DBD8 File Offset: 0x0007BDD8
		public bool IsRunning
		{
			get
			{
				return float.IsPositiveInfinity(this.endTime) && !float.IsNegativeInfinity(this.startTime);
			}
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x06002001 RID: 8193 RVA: 0x0007DBFC File Offset: 0x0007BDFC
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

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x06002002 RID: 8194 RVA: 0x0007DC34 File Offset: 0x0007BE34
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

		// Token: 0x04000F50 RID: 3920
		private const double ZeroDeductions = 0.0;

		// Token: 0x04000F51 RID: 3921
		private const double OneThousand = 1000.0;

		// Token: 0x04000F52 RID: 3922
		private const double ZeroElapsed = 0.0;

		// Token: 0x04000F53 RID: 3923
		private float startTime;

		// Token: 0x04000F54 RID: 3924
		private float endTime;

		// Token: 0x04000F55 RID: 3925
		private double deductSeconds;

		// Token: 0x0200034B RID: 843
		private static class TIME_SOURCE
		{
			// Token: 0x17000809 RID: 2057
			// (get) Token: 0x06002003 RID: 8195 RVA: 0x0007DC6C File Offset: 0x0007BE6C
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
