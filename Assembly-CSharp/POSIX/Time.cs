using System;

namespace POSIX
{
	// Token: 0x020001F5 RID: 501
	public static class Time
	{
		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000E03 RID: 3587 RVA: 0x00035CA8 File Offset: 0x00033EA8
		public static int NowStamp
		{
			get
			{
				double totalSeconds = DateTime.UtcNow.Subtract(Time.epoch).TotalSeconds;
				int num = (int)totalSeconds;
				if ((double)num > totalSeconds)
				{
					num--;
				}
				return num;
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000E04 RID: 3588 RVA: 0x00035CE0 File Offset: 0x00033EE0
		public static double NowSeconds
		{
			get
			{
				return DateTime.UtcNow.Subtract(Time.epoch).TotalSeconds;
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000E05 RID: 3589 RVA: 0x00035D08 File Offset: 0x00033F08
		public static TimeSpan NowSpan
		{
			get
			{
				return DateTime.UtcNow.Subtract(Time.epoch);
			}
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x00035D28 File Offset: 0x00033F28
		public static TimeSpan ElapsedSince(int timeStamp)
		{
			return DateTime.UtcNow.Subtract(Time.epoch.AddSeconds((double)timeStamp));
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x00035D54 File Offset: 0x00033F54
		public static TimeSpan ElapsedSince(TimeSpan sinceEpoch)
		{
			return DateTime.UtcNow.Subtract(Time.epoch.Add(sinceEpoch));
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x00035D7C File Offset: 0x00033F7C
		public static TimeSpan ElapsedSince(DateTime dateTime)
		{
			return DateTime.UtcNow.Subtract(dateTime.ToUniversalTime());
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x00035DA0 File Offset: 0x00033FA0
		public static double ElapsedSecondsSince(int timeStamp)
		{
			return DateTime.UtcNow.Subtract(Time.epoch.AddSeconds((double)timeStamp)).TotalSeconds;
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x00035DD4 File Offset: 0x00033FD4
		public static double ElapsedSecondsSince(TimeSpan sinceEpoch)
		{
			return DateTime.UtcNow.Subtract(Time.epoch.Add(sinceEpoch)).TotalSeconds;
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x00035E04 File Offset: 0x00034004
		public static double ElapsedSecondsSince(DateTime dateTime)
		{
			return DateTime.UtcNow.Subtract(dateTime.ToUniversalTime()).TotalSeconds;
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x00035E30 File Offset: 0x00034030
		public static int ElapsedStampSince(int timeStamp)
		{
			double totalSeconds = DateTime.UtcNow.Subtract(Time.epoch.AddSeconds((double)timeStamp)).TotalSeconds;
			int num = (int)totalSeconds;
			if ((double)num > totalSeconds)
			{
				num--;
			}
			return num;
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x00035E74 File Offset: 0x00034074
		public static int ElapsedStampSince(TimeSpan sinceEpoch)
		{
			double totalSeconds = DateTime.UtcNow.Subtract(Time.epoch.Add(sinceEpoch)).TotalSeconds;
			int num = (int)totalSeconds;
			if ((double)num > totalSeconds)
			{
				num--;
			}
			return num;
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x00035EB8 File Offset: 0x000340B8
		public static int ElapsedStampSince(DateTime dateTime)
		{
			double totalSeconds = DateTime.UtcNow.Subtract(dateTime.ToUniversalTime()).TotalSeconds;
			int num = (int)totalSeconds;
			if ((double)num > totalSeconds)
			{
				num--;
			}
			return num;
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x00035EF4 File Offset: 0x000340F4
		public static TimeSpan Elapsed(int timeStampStart, int timeStampEnd)
		{
			return TimeSpan.FromSeconds((double)(timeStampEnd - timeStampStart));
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x00035F00 File Offset: 0x00034100
		public static TimeSpan Elapsed(TimeSpan sinceEpochStart, TimeSpan sinceEpochEnd)
		{
			return sinceEpochEnd.Subtract(sinceEpochStart);
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x00035F0C File Offset: 0x0003410C
		public static TimeSpan Elapsed(DateTime dateTimeStart, DateTime dateTimeEnd)
		{
			return dateTimeEnd.ToUniversalTime().Subtract(dateTimeStart.ToUniversalTime());
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x00035F30 File Offset: 0x00034130
		public static double ElapsedSeconds(int timeStampStart, int timeStampEnd)
		{
			return TimeSpan.FromSeconds((double)(timeStampEnd - timeStampStart)).TotalSeconds;
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x00035F50 File Offset: 0x00034150
		public static double ElapsedSeconds(TimeSpan sinceEpochStart, TimeSpan sinceEpochEnd)
		{
			return sinceEpochEnd.Subtract(sinceEpochStart).TotalSeconds;
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x00035F70 File Offset: 0x00034170
		public static double ElapsedSeconds(DateTime dateTimeStart, DateTime dateTimeEnd)
		{
			return dateTimeEnd.ToUniversalTime().Subtract(dateTimeStart.ToUniversalTime()).TotalSeconds;
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x00035F9C File Offset: 0x0003419C
		public static int ElapsedStamp(int timeStampStart, int timeStampEnd)
		{
			double totalSeconds = TimeSpan.FromSeconds((double)(timeStampEnd - timeStampStart)).TotalSeconds;
			int num = (int)totalSeconds;
			if ((double)num > totalSeconds)
			{
				num--;
			}
			return num;
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x00035FCC File Offset: 0x000341CC
		public static int ElapsedStamp(TimeSpan sinceEpochStart, TimeSpan sinceEpochEnd)
		{
			double totalSeconds = sinceEpochEnd.Subtract(sinceEpochStart).TotalSeconds;
			int num = (int)totalSeconds;
			if ((double)num > totalSeconds)
			{
				num--;
			}
			return num;
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x00035FFC File Offset: 0x000341FC
		public static int ElapsedStamp(DateTime dateTimeStart, DateTime dateTimeEnd)
		{
			double totalSeconds = dateTimeEnd.ToUniversalTime().Subtract(dateTimeStart.ToUniversalTime()).TotalSeconds;
			int num = (int)totalSeconds;
			if ((double)num > totalSeconds)
			{
				num--;
			}
			return num;
		}

		// Token: 0x040008AF RID: 2223
		private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
	}
}
