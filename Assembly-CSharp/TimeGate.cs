using System;

// Token: 0x020003F0 RID: 1008
public struct TimeGate
{
	// Token: 0x17000842 RID: 2114
	// (get) Token: 0x06002322 RID: 8994 RVA: 0x000822D0 File Offset: 0x000804D0
	public bool started
	{
		get
		{
			return this.initialized;
		}
	}

	// Token: 0x17000843 RID: 2115
	// (get) Token: 0x06002323 RID: 8995 RVA: 0x000822D8 File Offset: 0x000804D8
	// (set) Token: 0x06002324 RID: 8996 RVA: 0x00082308 File Offset: 0x00080508
	public long elapsedMillis
	{
		get
		{
			return (!this.initialized) ? 2147483647L : (global::TimeGate.timeSource - this.startTime);
		}
		set
		{
			if (value == 2147483647L)
			{
				this.initialized = false;
			}
			else
			{
				this.startTime = global::TimeGate.timeSource - value;
				this.initialized = true;
			}
		}
	}

	// Token: 0x17000844 RID: 2116
	// (get) Token: 0x06002325 RID: 8997 RVA: 0x00082344 File Offset: 0x00080544
	// (set) Token: 0x06002326 RID: 8998 RVA: 0x00082384 File Offset: 0x00080584
	public double elapsedSeconds
	{
		get
		{
			return (!this.initialized) ? double.PositiveInfinity : ((double)(global::TimeGate.timeSource - this.startTime) / 1000.0);
		}
		set
		{
			if (double.IsPositiveInfinity(value))
			{
				this.initialized = false;
			}
			else
			{
				this.startTime = global::TimeGate.timeSource - global::TimeGate.SecondsToMS(value);
				this.initialized = true;
			}
		}
	}

	// Token: 0x17000845 RID: 2117
	// (get) Token: 0x06002327 RID: 8999 RVA: 0x000823C4 File Offset: 0x000805C4
	// (set) Token: 0x06002328 RID: 9000 RVA: 0x000823E0 File Offset: 0x000805E0
	public long timeInMillis
	{
		get
		{
			return (!this.initialized) ? 0L : this.startTime;
		}
		set
		{
			this.startTime = value;
			this.initialized = true;
		}
	}

	// Token: 0x17000846 RID: 2118
	// (get) Token: 0x06002329 RID: 9001 RVA: 0x000823F0 File Offset: 0x000805F0
	// (set) Token: 0x0600232A RID: 9002 RVA: 0x00082428 File Offset: 0x00080628
	public double timeInSeconds
	{
		get
		{
			return (!this.initialized) ? 0.0 : ((double)this.startTime / 1000.0);
		}
		set
		{
			this.startTime = global::TimeGate.SecondsToMS(value);
			this.initialized = true;
		}
	}

	// Token: 0x17000847 RID: 2119
	// (get) Token: 0x0600232B RID: 9003 RVA: 0x00082440 File Offset: 0x00080640
	public bool passedOrAtTime
	{
		get
		{
			return !this.initialized || this.startTime <= global::TimeGate.timeSource;
		}
	}

	// Token: 0x17000848 RID: 2120
	// (get) Token: 0x0600232C RID: 9004 RVA: 0x00082460 File Offset: 0x00080660
	public bool behindOrAtTime
	{
		get
		{
			return !this.initialized || this.startTime >= global::TimeGate.timeSource;
		}
	}

	// Token: 0x17000849 RID: 2121
	// (get) Token: 0x0600232D RID: 9005 RVA: 0x00082480 File Offset: 0x00080680
	public bool passedTime
	{
		get
		{
			return !this.initialized || this.startTime < global::TimeGate.timeSource;
		}
	}

	// Token: 0x1700084A RID: 2122
	// (get) Token: 0x0600232E RID: 9006 RVA: 0x000824A0 File Offset: 0x000806A0
	public bool behindTime
	{
		get
		{
			return !this.initialized || this.startTime > global::TimeGate.timeSource;
		}
	}

	// Token: 0x1700084B RID: 2123
	// (get) Token: 0x0600232F RID: 9007 RVA: 0x000824C0 File Offset: 0x000806C0
	private static long timeSource
	{
		get
		{
			return (long)global::NetCull.timeInMillis;
		}
	}

	// Token: 0x06002330 RID: 9008 RVA: 0x000824C8 File Offset: 0x000806C8
	private static long SecondsToMS(double seconds)
	{
		return (long)Math.Floor(seconds * 1000.0);
	}

	// Token: 0x06002331 RID: 9009 RVA: 0x000824DC File Offset: 0x000806DC
	public bool ElapsedMillis(long span)
	{
		return span <= 0L || !this.initialized || global::TimeGate.timeSource - this.startTime >= span;
	}

	// Token: 0x06002332 RID: 9010 RVA: 0x00082514 File Offset: 0x00080714
	public bool ElapsedSeconds(double seconds)
	{
		return seconds <= 0.0 || !this.initialized || global::TimeGate.timeSource - this.startTime >= global::TimeGate.SecondsToMS(seconds);
	}

	// Token: 0x06002333 RID: 9011 RVA: 0x00082558 File Offset: 0x00080758
	public bool FireMillis(long minimumElapsedTime)
	{
		return minimumElapsedTime <= 0L || this.RefireMillis(-minimumElapsedTime);
	}

	// Token: 0x06002334 RID: 9012 RVA: 0x00082570 File Offset: 0x00080770
	public bool RefireMillis(long intervalMS)
	{
		long timeSource = global::TimeGate.timeSource;
		if (!this.initialized)
		{
			this.initialized = true;
			this.startTime = timeSource;
			return true;
		}
		if (intervalMS == 0L)
		{
			bool result = timeSource != this.startTime;
			this.startTime = timeSource;
			return result;
		}
		if (intervalMS < 0L)
		{
			long num = this.startTime - timeSource;
			if (num <= intervalMS)
			{
				this.startTime = timeSource;
				return true;
			}
			return false;
		}
		else
		{
			long num2 = timeSource - this.startTime;
			if (num2 >= intervalMS)
			{
				this.startTime += intervalMS;
				return true;
			}
			return false;
		}
	}

	// Token: 0x06002335 RID: 9013 RVA: 0x00082600 File Offset: 0x00080800
	public bool RefireSeconds(double intervalSeconds)
	{
		return this.RefireMillis(global::TimeGate.SecondsToMS(intervalSeconds));
	}

	// Token: 0x06002336 RID: 9014 RVA: 0x00082610 File Offset: 0x00080810
	public static implicit operator global::TimeGate(double timeRemaining)
	{
		global::TimeGate result;
		result.initialized = true;
		result.startTime = global::TimeGate.SecondsToMS((double)global::TimeGate.timeSource / 1000.0 - timeRemaining);
		return result;
	}

	// Token: 0x06002337 RID: 9015 RVA: 0x00082644 File Offset: 0x00080844
	public static implicit operator global::TimeGate(float timeRemaining)
	{
		return (double)timeRemaining;
	}

	// Token: 0x06002338 RID: 9016 RVA: 0x00082650 File Offset: 0x00080850
	public static implicit operator global::TimeGate(long timeRemaining)
	{
		global::TimeGate result;
		result.initialized = true;
		result.startTime = global::TimeGate.timeSource - timeRemaining;
		return result;
	}

	// Token: 0x06002339 RID: 9017 RVA: 0x00082674 File Offset: 0x00080874
	public static implicit operator global::TimeGate(ulong timeRemaining)
	{
		global::TimeGate result;
		result.initialized = true;
		result.startTime = global::TimeGate.timeSource - (long)timeRemaining;
		return result;
	}

	// Token: 0x0600233A RID: 9018 RVA: 0x00082698 File Offset: 0x00080898
	public static implicit operator global::TimeGate(int timeRemaining)
	{
		global::TimeGate result;
		result.initialized = true;
		result.startTime = global::TimeGate.timeSource - (long)timeRemaining;
		return result;
	}

	// Token: 0x0600233B RID: 9019 RVA: 0x000826C0 File Offset: 0x000808C0
	public static implicit operator global::TimeGate(uint timeRemaining)
	{
		global::TimeGate result;
		result.initialized = true;
		result.startTime = global::TimeGate.timeSource - (long)((ulong)timeRemaining);
		return result;
	}

	// Token: 0x0600233C RID: 9020 RVA: 0x000826E8 File Offset: 0x000808E8
	public static implicit operator global::TimeGate(short timeRemaining)
	{
		global::TimeGate result;
		result.initialized = true;
		result.startTime = global::TimeGate.timeSource - (long)timeRemaining;
		return result;
	}

	// Token: 0x0600233D RID: 9021 RVA: 0x00082710 File Offset: 0x00080910
	public static implicit operator global::TimeGate(ushort timeRemaining)
	{
		global::TimeGate result;
		result.initialized = true;
		result.startTime = global::TimeGate.timeSource - (long)timeRemaining;
		return result;
	}

	// Token: 0x0600233E RID: 9022 RVA: 0x00082738 File Offset: 0x00080938
	public static implicit operator global::TimeGate(byte timeRemaining)
	{
		global::TimeGate result;
		result.initialized = true;
		result.startTime = global::TimeGate.timeSource - (long)timeRemaining;
		return result;
	}

	// Token: 0x0600233F RID: 9023 RVA: 0x00082760 File Offset: 0x00080960
	public static implicit operator global::TimeGate(sbyte timeRemaining)
	{
		global::TimeGate result;
		result.initialized = true;
		result.startTime = global::TimeGate.timeSource - (long)timeRemaining;
		return result;
	}

	// Token: 0x06002340 RID: 9024 RVA: 0x00082788 File Offset: 0x00080988
	public static bool operator true(global::TimeGate gate)
	{
		return gate.passedOrAtTime;
	}

	// Token: 0x06002341 RID: 9025 RVA: 0x00082794 File Offset: 0x00080994
	public static bool operator false(global::TimeGate gate)
	{
		return gate.behindTime;
	}

	// Token: 0x040010A2 RID: 4258
	[NonSerialized]
	private bool initialized;

	// Token: 0x040010A3 RID: 4259
	[NonSerialized]
	private long startTime;
}
