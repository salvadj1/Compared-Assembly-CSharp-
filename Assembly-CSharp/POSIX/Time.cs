using System;

namespace POSIX
{
	// Token: 0x020001C5 RID: 453
	public static class Time
	{
		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000CC3 RID: 3267 RVA: 0x00031DBC File Offset: 0x0002FFBC
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

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000CC4 RID: 3268 RVA: 0x00031DF4 File Offset: 0x0002FFF4
		public static double NowSeconds
		{
			get
			{
				return DateTime.UtcNow.Subtract(Time.epoch).TotalSeconds;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000CC5 RID: 3269 RVA: 0x00031E1C File Offset: 0x0003001C
		public static TimeSpan NowSpan
		{
			get
			{
				return DateTime.UtcNow.Subtract(Time.epoch);
			}
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x00031E3C File Offset: 0x0003003C
		public static TimeSpan ElapsedSince(int timeStamp)
		{
			return DateTime.UtcNow.Subtract(Time.epoch.AddSeconds((double)timeStamp));
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x00031E68 File Offset: 0x00030068
		public static TimeSpan ElapsedSince(TimeSpan sinceEpoch)
		{
			return DateTime.UtcNow.Subtract(Time.epoch.Add(sinceEpoch));
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x00031E90 File Offset: 0x00030090
		public static TimeSpan ElapsedSince(DateTime dateTime)
		{
			return DateTime.UtcNow.Subtract(dateTime.ToUniversalTime());
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x00031EB4 File Offset: 0x000300B4
		public static double ElapsedSecondsSince(int timeStamp)
		{
			return DateTime.UtcNow.Subtract(Time.epoch.AddSeconds((double)timeStamp)).TotalSeconds;
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x00031EE8 File Offset: 0x000300E8
		public static double ElapsedSecondsSince(TimeSpan sinceEpoch)
		{
			return DateTime.UtcNow.Subtract(Time.epoch.Add(sinceEpoch)).TotalSeconds;
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x00031F18 File Offset: 0x00030118
		public static double ElapsedSecondsSince(DateTime dateTime)
		{
			return DateTime.UtcNow.Subtract(dateTime.ToUniversalTime()).TotalSeconds;
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x00031F44 File Offset: 0x00030144
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

		// Token: 0x06000CCD RID: 3277 RVA: 0x00031F88 File Offset: 0x00030188
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

		// Token: 0x06000CCE RID: 3278 RVA: 0x00031FCC File Offset: 0x000301CC
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

		// Token: 0x06000CCF RID: 3279 RVA: 0x00032008 File Offset: 0x00030208
		public static TimeSpan Elapsed(int timeStampStart, int timeStampEnd)
		{
			return TimeSpan.FromSeconds((double)(timeStampEnd - timeStampStart));
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x00032014 File Offset: 0x00030214
		public static TimeSpan Elapsed(TimeSpan sinceEpochStart, TimeSpan sinceEpochEnd)
		{
			return sinceEpochEnd.Subtract(sinceEpochStart);
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x00032020 File Offset: 0x00030220
		public static TimeSpan Elapsed(DateTime dateTimeStart, DateTime dateTimeEnd)
		{
			return dateTimeEnd.ToUniversalTime().Subtract(dateTimeStart.ToUniversalTime());
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x00032044 File Offset: 0x00030244
		public static double ElapsedSeconds(int timeStampStart, int timeStampEnd)
		{
			return TimeSpan.FromSeconds((double)(timeStampEnd - timeStampStart)).TotalSeconds;
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x00032064 File Offset: 0x00030264
		public static double ElapsedSeconds(TimeSpan sinceEpochStart, TimeSpan sinceEpochEnd)
		{
			return sinceEpochEnd.Subtract(sinceEpochStart).TotalSeconds;
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x00032084 File Offset: 0x00030284
		public static double ElapsedSeconds(DateTime dateTimeStart, DateTime dateTimeEnd)
		{
			return dateTimeEnd.ToUniversalTime().Subtract(dateTimeStart.ToUniversalTime()).TotalSeconds;
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x000320B0 File Offset: 0x000302B0
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

		// Token: 0x06000CD6 RID: 3286 RVA: 0x000320E0 File Offset: 0x000302E0
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

		// Token: 0x06000CD7 RID: 3287 RVA: 0x00032110 File Offset: 0x00030310
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

		// Token: 0x0400079B RID: 1947
		private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
	}
}
