using System;
using UnityEngine;

namespace Facepunch.Clocks.Counters.Unity
{
	// Token: 0x02000346 RID: 838
	public struct NetworkTime
	{
		// Token: 0x06001FE9 RID: 8169 RVA: 0x0007D5D4 File Offset: 0x0007B7D4
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

		// Token: 0x06001FEA RID: 8170 RVA: 0x0007D65C File Offset: 0x0007B85C
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

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x06001FEB RID: 8171 RVA: 0x0007D698 File Offset: 0x0007B898
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

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06001FEC RID: 8172 RVA: 0x0007D6FC File Offset: 0x0007B8FC
		public long ElapsedMilliseconds
		{
			get
			{
				return (long)Math.Floor(this.ElapsedSeconds * 1000.0);
			}
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06001FED RID: 8173 RVA: 0x0007D714 File Offset: 0x0007B914
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

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06001FEE RID: 8174 RVA: 0x0007D76C File Offset: 0x0007B96C
		public bool IsRunning
		{
			get
			{
				return double.IsPositiveInfinity(this.endTime) && !double.IsNegativeInfinity(this.startTime);
			}
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06001FEF RID: 8175 RVA: 0x0007D790 File Offset: 0x0007B990
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

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06001FF0 RID: 8176 RVA: 0x0007D7CC File Offset: 0x0007B9CC
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

		// Token: 0x04000F44 RID: 3908
		private const double ZeroDeductions = 0.0;

		// Token: 0x04000F45 RID: 3909
		private const double OneThousand = 1000.0;

		// Token: 0x04000F46 RID: 3910
		private const double ZeroElapsed = 0.0;

		// Token: 0x04000F47 RID: 3911
		private double startTime;

		// Token: 0x04000F48 RID: 3912
		private double endTime;

		// Token: 0x04000F49 RID: 3913
		private double deductSeconds;

		// Token: 0x02000347 RID: 839
		private static class TIME_SOURCE
		{
			// Token: 0x170007FB RID: 2043
			// (get) Token: 0x06001FF1 RID: 8177 RVA: 0x0007D80C File Offset: 0x0007BA0C
			public static double NOW
			{
				get
				{
					return Network.time;
				}
			}
		}
	}
}
