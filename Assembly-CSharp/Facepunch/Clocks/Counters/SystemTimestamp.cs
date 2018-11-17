using System;
using System.Diagnostics;
using UnityEngine;

namespace Facepunch.Clocks.Counters
{
	// Token: 0x02000341 RID: 833
	public struct SystemTimestamp
	{
		// Token: 0x06001FB6 RID: 8118 RVA: 0x0007CC04 File Offset: 0x0007AE04
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

		// Token: 0x06001FB7 RID: 8119 RVA: 0x0007CC8C File Offset: 0x0007AE8C
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

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06001FB8 RID: 8120 RVA: 0x0007CCC8 File Offset: 0x0007AEC8
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

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06001FB9 RID: 8121 RVA: 0x0007CD2C File Offset: 0x0007AF2C
		public long ElapsedMilliseconds
		{
			get
			{
				return (long)Math.Floor(this.ElapsedSeconds * 1000.0);
			}
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06001FBA RID: 8122 RVA: 0x0007CD44 File Offset: 0x0007AF44
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

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06001FBB RID: 8123 RVA: 0x0007CD9C File Offset: 0x0007AF9C
		public bool IsRunning
		{
			get
			{
				return double.IsPositiveInfinity(this.endTime) && !double.IsNegativeInfinity(this.startTime);
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06001FBC RID: 8124 RVA: 0x0007CDC0 File Offset: 0x0007AFC0
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

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06001FBD RID: 8125 RVA: 0x0007CDFC File Offset: 0x0007AFFC
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

		// Token: 0x04000F32 RID: 3890
		private const double ZeroDeductions = 0.0;

		// Token: 0x04000F33 RID: 3891
		private const double OneThousand = 1000.0;

		// Token: 0x04000F34 RID: 3892
		private const double ZeroElapsed = 0.0;

		// Token: 0x04000F35 RID: 3893
		private double startTime;

		// Token: 0x04000F36 RID: 3894
		private double endTime;

		// Token: 0x04000F37 RID: 3895
		private double deductSeconds;

		// Token: 0x02000342 RID: 834
		private static class TIME_SOURCE
		{
			// Token: 0x06001FBE RID: 8126 RVA: 0x0007CE3C File Offset: 0x0007B03C
			static TIME_SOURCE()
			{
				SystemTimestamp.TIME_SOURCE.ToSeconds = (double)(1m / SystemTimestamp.TIME_SOURCE.Frequency);
				string text = string.Format("SystemTimestampWatch settings={{IsHighResolution={0},Frequency={1},ToSecond={2}}}", SystemTimestamp.TIME_SOURCE.IsHighResolution, SystemTimestamp.TIME_SOURCE.Frequency, SystemTimestamp.TIME_SOURCE.ToSeconds);
				if (!SystemTimestamp.TIME_SOURCE.IsHighResolution)
				{
					Debug.LogWarning(text);
				}
			}

			// Token: 0x170007E3 RID: 2019
			// (get) Token: 0x06001FBF RID: 8127 RVA: 0x0007CEC0 File Offset: 0x0007B0C0
			public static double NOW
			{
				get
				{
					return (double)(Stopwatch.GetTimestamp() - SystemTimestamp.TIME_SOURCE.ThenTimestamp) * SystemTimestamp.TIME_SOURCE.ToSeconds;
				}
			}

			// Token: 0x04000F38 RID: 3896
			private static readonly long ThenTimestamp = Stopwatch.GetTimestamp();

			// Token: 0x04000F39 RID: 3897
			private static readonly long Frequency = Stopwatch.Frequency;

			// Token: 0x04000F3A RID: 3898
			private static readonly double ToSeconds;

			// Token: 0x04000F3B RID: 3899
			private static readonly bool IsHighResolution = Stopwatch.IsHighResolution;
		}
	}
}
