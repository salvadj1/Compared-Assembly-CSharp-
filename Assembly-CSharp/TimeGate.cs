using System;

// Token: 0x02000343 RID: 835
public struct TimeGate
{
	// Token: 0x170007E4 RID: 2020
	// (get) Token: 0x06001FC0 RID: 8128 RVA: 0x0007CED4 File Offset: 0x0007B0D4
	public bool started
	{
		get
		{
			return this.initialized;
		}
	}

	// Token: 0x170007E5 RID: 2021
	// (get) Token: 0x06001FC1 RID: 8129 RVA: 0x0007CEDC File Offset: 0x0007B0DC
	// (set) Token: 0x06001FC2 RID: 8130 RVA: 0x0007CF0C File Offset: 0x0007B10C
	public long elapsedMillis
	{
		get
		{
			return (!this.initialized) ? 2147483647L : (TimeGate.timeSource - this.startTime);
		}
		set
		{
			if (value == 2147483647L)
			{
				this.initialized = false;
			}
			else
			{
				this.startTime = TimeGate.timeSource - value;
				this.initialized = true;
			}
		}
	}

	// Token: 0x170007E6 RID: 2022
	// (get) Token: 0x06001FC3 RID: 8131 RVA: 0x0007CF48 File Offset: 0x0007B148
	// (set) Token: 0x06001FC4 RID: 8132 RVA: 0x0007CF88 File Offset: 0x0007B188
	public double elapsedSeconds
	{
		get
		{
			return (!this.initialized) ? double.PositiveInfinity : ((double)(TimeGate.timeSource - this.startTime) / 1000.0);
		}
		set
		{
			if (double.IsPositiveInfinity(value))
			{
				this.initialized = false;
			}
			else
			{
				this.startTime = TimeGate.timeSource - TimeGate.SecondsToMS(value);
				this.initialized = true;
			}
		}
	}

	// Token: 0x170007E7 RID: 2023
	// (get) Token: 0x06001FC5 RID: 8133 RVA: 0x0007CFC8 File Offset: 0x0007B1C8
	// (set) Token: 0x06001FC6 RID: 8134 RVA: 0x0007CFE4 File Offset: 0x0007B1E4
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

	// Token: 0x170007E8 RID: 2024
	// (get) Token: 0x06001FC7 RID: 8135 RVA: 0x0007CFF4 File Offset: 0x0007B1F4
	// (set) Token: 0x06001FC8 RID: 8136 RVA: 0x0007D02C File Offset: 0x0007B22C
	public double timeInSeconds
	{
		get
		{
			return (!this.initialized) ? 0.0 : ((double)this.startTime / 1000.0);
		}
		set
		{
			this.startTime = TimeGate.SecondsToMS(value);
			this.initialized = true;
		}
	}

	// Token: 0x170007E9 RID: 2025
	// (get) Token: 0x06001FC9 RID: 8137 RVA: 0x0007D044 File Offset: 0x0007B244
	public bool passedOrAtTime
	{
		get
		{
			return !this.initialized || this.startTime <= TimeGate.timeSource;
		}
	}

	// Token: 0x170007EA RID: 2026
	// (get) Token: 0x06001FCA RID: 8138 RVA: 0x0007D064 File Offset: 0x0007B264
	public bool behindOrAtTime
	{
		get
		{
			return !this.initialized || this.startTime >= TimeGate.timeSource;
		}
	}

	// Token: 0x170007EB RID: 2027
	// (get) Token: 0x06001FCB RID: 8139 RVA: 0x0007D084 File Offset: 0x0007B284
	public bool passedTime
	{
		get
		{
			return !this.initialized || this.startTime < TimeGate.timeSource;
		}
	}

	// Token: 0x170007EC RID: 2028
	// (get) Token: 0x06001FCC RID: 8140 RVA: 0x0007D0A4 File Offset: 0x0007B2A4
	public bool behindTime
	{
		get
		{
			return !this.initialized || this.startTime > TimeGate.timeSource;
		}
	}

	// Token: 0x170007ED RID: 2029
	// (get) Token: 0x06001FCD RID: 8141 RVA: 0x0007D0C4 File Offset: 0x0007B2C4
	private static long timeSource
	{
		get
		{
			return (long)NetCull.timeInMillis;
		}
	}

	// Token: 0x06001FCE RID: 8142 RVA: 0x0007D0CC File Offset: 0x0007B2CC
	private static long SecondsToMS(double seconds)
	{
		return (long)Math.Floor(seconds * 1000.0);
	}

	// Token: 0x06001FCF RID: 8143 RVA: 0x0007D0E0 File Offset: 0x0007B2E0
	public bool ElapsedMillis(long span)
	{
		return span <= 0L || !this.initialized || TimeGate.timeSource - this.startTime >= span;
	}

	// Token: 0x06001FD0 RID: 8144 RVA: 0x0007D118 File Offset: 0x0007B318
	public bool ElapsedSeconds(double seconds)
	{
		return seconds <= 0.0 || !this.initialized || TimeGate.timeSource - this.startTime >= TimeGate.SecondsToMS(seconds);
	}

	// Token: 0x06001FD1 RID: 8145 RVA: 0x0007D15C File Offset: 0x0007B35C
	public bool FireMillis(long minimumElapsedTime)
	{
		return minimumElapsedTime <= 0L || this.RefireMillis(-minimumElapsedTime);
	}

	// Token: 0x06001FD2 RID: 8146 RVA: 0x0007D174 File Offset: 0x0007B374
	public bool RefireMillis(long intervalMS)
	{
		long timeSource = TimeGate.timeSource;
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

	// Token: 0x06001FD3 RID: 8147 RVA: 0x0007D204 File Offset: 0x0007B404
	public bool RefireSeconds(double intervalSeconds)
	{
		return this.RefireMillis(TimeGate.SecondsToMS(intervalSeconds));
	}

	// Token: 0x06001FD4 RID: 8148 RVA: 0x0007D214 File Offset: 0x0007B414
	public static implicit operator TimeGate(double timeRemaining)
	{
		TimeGate result;
		result.initialized = true;
		result.startTime = TimeGate.SecondsToMS((double)TimeGate.timeSource / 1000.0 - timeRemaining);
		return result;
	}

	// Token: 0x06001FD5 RID: 8149 RVA: 0x0007D248 File Offset: 0x0007B448
	public static implicit operator TimeGate(float timeRemaining)
	{
		return (double)timeRemaining;
	}

	// Token: 0x06001FD6 RID: 8150 RVA: 0x0007D254 File Offset: 0x0007B454
	public static implicit operator TimeGate(long timeRemaining)
	{
		TimeGate result;
		result.initialized = true;
		result.startTime = TimeGate.timeSource - timeRemaining;
		return result;
	}

	// Token: 0x06001FD7 RID: 8151 RVA: 0x0007D278 File Offset: 0x0007B478
	public static implicit operator TimeGate(ulong timeRemaining)
	{
		TimeGate result;
		result.initialized = true;
		result.startTime = TimeGate.timeSource - (long)timeRemaining;
		return result;
	}

	// Token: 0x06001FD8 RID: 8152 RVA: 0x0007D29C File Offset: 0x0007B49C
	public static implicit operator TimeGate(int timeRemaining)
	{
		TimeGate result;
		result.initialized = true;
		result.startTime = TimeGate.timeSource - (long)timeRemaining;
		return result;
	}

	// Token: 0x06001FD9 RID: 8153 RVA: 0x0007D2C4 File Offset: 0x0007B4C4
	public static implicit operator TimeGate(uint timeRemaining)
	{
		TimeGate result;
		result.initialized = true;
		result.startTime = TimeGate.timeSource - (long)((ulong)timeRemaining);
		return result;
	}

	// Token: 0x06001FDA RID: 8154 RVA: 0x0007D2EC File Offset: 0x0007B4EC
	public static implicit operator TimeGate(short timeRemaining)
	{
		TimeGate result;
		result.initialized = true;
		result.startTime = TimeGate.timeSource - (long)timeRemaining;
		return result;
	}

	// Token: 0x06001FDB RID: 8155 RVA: 0x0007D314 File Offset: 0x0007B514
	public static implicit operator TimeGate(ushort timeRemaining)
	{
		TimeGate result;
		result.initialized = true;
		result.startTime = TimeGate.timeSource - (long)timeRemaining;
		return result;
	}

	// Token: 0x06001FDC RID: 8156 RVA: 0x0007D33C File Offset: 0x0007B53C
	public static implicit operator TimeGate(byte timeRemaining)
	{
		TimeGate result;
		result.initialized = true;
		result.startTime = TimeGate.timeSource - (long)timeRemaining;
		return result;
	}

	// Token: 0x06001FDD RID: 8157 RVA: 0x0007D364 File Offset: 0x0007B564
	public static implicit operator TimeGate(sbyte timeRemaining)
	{
		TimeGate result;
		result.initialized = true;
		result.startTime = TimeGate.timeSource - (long)timeRemaining;
		return result;
	}

	// Token: 0x06001FDE RID: 8158 RVA: 0x0007D38C File Offset: 0x0007B58C
	public static bool operator true(TimeGate gate)
	{
		return gate.passedOrAtTime;
	}

	// Token: 0x06001FDF RID: 8159 RVA: 0x0007D398 File Offset: 0x0007B598
	public static bool operator false(TimeGate gate)
	{
		return gate.behindTime;
	}

	// Token: 0x04000F3C RID: 3900
	[NonSerialized]
	private bool initialized;

	// Token: 0x04000F3D RID: 3901
	[NonSerialized]
	private long startTime;
}
