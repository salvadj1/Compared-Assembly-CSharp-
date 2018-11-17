using System;
using uLink;

// Token: 0x020003AC RID: 940
public struct NetClockTester
{
	// Token: 0x170007C1 RID: 1985
	// (get) Token: 0x060020E2 RID: 8418 RVA: 0x00079528 File Offset: 0x00077728
	public bool Any
	{
		get
		{
			return this.Count > 0UL;
		}
	}

	// Token: 0x170007C2 RID: 1986
	// (get) Token: 0x060020E3 RID: 8419 RVA: 0x00079534 File Offset: 0x00077734
	public bool Empty
	{
		get
		{
			return this.Count == 0UL;
		}
	}

	// Token: 0x060020E4 RID: 8420 RVA: 0x00079540 File Offset: 0x00077740
	public static global::NetClockTester.ValidityFlags TestValidity(ref global::NetClockTester test, ref uLink.NetworkMessageInfo info, double intervalSec, global::NetClockTester.ValidityFlags testFor)
	{
		return global::NetClockTester.TestValidity(ref test, ref info, (long)Math.Floor(intervalSec * 1000.0), testFor);
	}

	// Token: 0x060020E5 RID: 8421 RVA: 0x0007955C File Offset: 0x0007775C
	public static global::NetClockTester.ValidityFlags TestValidity(ref global::NetClockTester test, ref uLink.NetworkMessageInfo info, long intervalMS, global::NetClockTester.ValidityFlags testFor)
	{
		global::NetClockTester.ValidityFlags validityFlags = global::NetClockTester.TestValidity(ref test, info.timestampInMillis, intervalMS);
		test.Results.Add(validityFlags & testFor);
		return validityFlags;
	}

	// Token: 0x060020E6 RID: 8422 RVA: 0x00079588 File Offset: 0x00077788
	private static global::NetClockTester.ValidityFlags TestValidity(ref global::NetClockTester test, ulong timeStamp, long minimalSendRateMS)
	{
		ulong timeInMillis = global::NetCull.timeInMillis;
		global::NetClockTester.ValidityFlags validityFlags = (timeInMillis >= timeStamp) ? ((global::NetClockTester.ValidityFlags)0) : global::NetClockTester.ValidityFlags.AheadOfServerTime;
		if (test.Count > 0UL)
		{
			long num = global::NetClockTester.Subtract(timeStamp, test.Send.Last);
			long num2 = global::NetClockTester.Subtract(timeInMillis, test.Receive.Last);
			test.Send.Sum = global::NetClockTester.Add(test.Send.Sum, num);
			test.Receive.Sum = global::NetClockTester.Add(test.Receive.Sum, num2);
			test.Count += 1UL;
			test.Send.Last = timeStamp;
			test.Receive.Last = timeInMillis;
			if (num < minimalSendRateMS)
			{
				validityFlags |= global::NetClockTester.ValidityFlags.TooFrequent;
			}
			long num3 = global::NetClockTester.Subtract(test.Send.Last, test.Send.First);
			long num4 = global::NetClockTester.Subtract(test.Receive.Last, test.Receive.First);
			if (test.Count >= 5UL)
			{
				if (num3 > num4 * 2L)
				{
					validityFlags |= global::NetClockTester.ValidityFlags.OverTimed;
				}
			}
			else if (test.Count >= 3UL && num3 > num4 * 4L)
			{
				validityFlags |= global::NetClockTester.ValidityFlags.OverTimed;
			}
			global::NetClockTester.ValidityFlags lastTestFlags = test.LastTestFlags;
			test.LastTestFlags = validityFlags;
			if ((validityFlags & global::NetClockTester.ValidityFlags.TooFrequent) == global::NetClockTester.ValidityFlags.TooFrequent && (lastTestFlags & global::NetClockTester.ValidityFlags.TooFrequent) != global::NetClockTester.ValidityFlags.TooFrequent)
			{
				validityFlags &= ~global::NetClockTester.ValidityFlags.TooFrequent;
				test.Count = 1UL;
				test.Send.First = test.Send.Last;
				test.Send.Sum = 0UL;
				if (num2 > 0L)
				{
					test.Receive.First = (ulong)global::NetClockTester.Subtract(test.Receive.Last, (ulong)num2);
					test.Receive.Sum = (ulong)num2;
				}
				else
				{
					test.Receive.First = test.Receive.Last;
					test.Receive.Sum = 0UL;
				}
			}
			return (validityFlags != (global::NetClockTester.ValidityFlags)0) ? validityFlags : global::NetClockTester.ValidityFlags.Valid;
		}
		test.Send.Sum = (test.Receive.Sum = 0UL);
		test.Send.First = timeStamp;
		test.Send.Last = timeStamp;
		test.Receive.Last = (test.Receive.First = timeInMillis);
		test.Count = 1UL;
		return validityFlags;
	}

	// Token: 0x170007C3 RID: 1987
	// (get) Token: 0x060020E7 RID: 8423 RVA: 0x000797D4 File Offset: 0x000779D4
	public static global::NetClockTester Reset
	{
		get
		{
			return default(global::NetClockTester);
		}
	}

	// Token: 0x060020E8 RID: 8424 RVA: 0x000797EC File Offset: 0x000779EC
	private static long Subtract(ulong a, ulong b)
	{
		if (a > b)
		{
			return (long)(a - b);
		}
		if (a < b)
		{
			return (long)(-(long)(b - a));
		}
		return 0L;
	}

	// Token: 0x060020E9 RID: 8425 RVA: 0x00079808 File Offset: 0x00077A08
	private static ulong Add(ulong a, long b)
	{
		if (b >= 0L)
		{
			return a + (ulong)b;
		}
		if (a > (ulong)(-(ulong)b))
		{
			return a - (ulong)(-(ulong)b);
		}
		return 0UL;
	}

	// Token: 0x04000F56 RID: 3926
	public global::NetClockTester.Stamping Send;

	// Token: 0x04000F57 RID: 3927
	public global::NetClockTester.Stamping Receive;

	// Token: 0x04000F58 RID: 3928
	[NonSerialized]
	public ulong Count;

	// Token: 0x04000F59 RID: 3929
	public global::NetClockTester.Validity Results;

	// Token: 0x04000F5A RID: 3930
	public global::NetClockTester.ValidityFlags LastTestFlags;

	// Token: 0x020003AD RID: 941
	public struct Stamping
	{
		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x060020EA RID: 8426 RVA: 0x00079828 File Offset: 0x00077A28
		public long Duration
		{
			get
			{
				return global::NetClockTester.Subtract(this.Last, this.First);
			}
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x060020EB RID: 8427 RVA: 0x0007983C File Offset: 0x00077A3C
		public long Variance
		{
			get
			{
				return (long)(this.Sum - (ulong)global::NetClockTester.Subtract(this.Last, this.First));
			}
		}

		// Token: 0x04000F5B RID: 3931
		public ulong Last;

		// Token: 0x04000F5C RID: 3932
		public ulong First;

		// Token: 0x04000F5D RID: 3933
		public ulong Sum;
	}

	// Token: 0x020003AE RID: 942
	[Flags]
	public enum ValidityFlags
	{
		// Token: 0x04000F5F RID: 3935
		Valid = 1,
		// Token: 0x04000F60 RID: 3936
		TooFrequent = 2,
		// Token: 0x04000F61 RID: 3937
		OverTimed = 4,
		// Token: 0x04000F62 RID: 3938
		AheadOfServerTime = 8
	}

	// Token: 0x020003AF RID: 943
	public struct Validity
	{
		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x060020EC RID: 8428 RVA: 0x00079858 File Offset: 0x00077A58
		public global::NetClockTester.ValidityFlags Flags
		{
			get
			{
				if (this.TooFrequent > 0u)
				{
					if (this.OverTimed > 0u)
					{
						if (this.AheadOfServerTime > 0u)
						{
							return global::NetClockTester.ValidityFlags.TooFrequent | global::NetClockTester.ValidityFlags.OverTimed | global::NetClockTester.ValidityFlags.AheadOfServerTime;
						}
						return global::NetClockTester.ValidityFlags.TooFrequent | global::NetClockTester.ValidityFlags.OverTimed;
					}
					else
					{
						if (this.AheadOfServerTime > 0u)
						{
							return global::NetClockTester.ValidityFlags.TooFrequent | global::NetClockTester.ValidityFlags.AheadOfServerTime;
						}
						return global::NetClockTester.ValidityFlags.TooFrequent;
					}
				}
				else if (this.OverTimed > 0u)
				{
					if (this.AheadOfServerTime > 0u)
					{
						return global::NetClockTester.ValidityFlags.OverTimed | global::NetClockTester.ValidityFlags.AheadOfServerTime;
					}
					return global::NetClockTester.ValidityFlags.OverTimed;
				}
				else
				{
					if (this.AheadOfServerTime > 0u)
					{
						return global::NetClockTester.ValidityFlags.AheadOfServerTime;
					}
					if (this.Valid > 0u)
					{
						return global::NetClockTester.ValidityFlags.Valid;
					}
					return (global::NetClockTester.ValidityFlags)0;
				}
			}
		}

		// Token: 0x060020ED RID: 8429 RVA: 0x000798DC File Offset: 0x00077ADC
		public void Add(global::NetClockTester.ValidityFlags vf)
		{
			switch (vf & (global::NetClockTester.ValidityFlags.TooFrequent | global::NetClockTester.ValidityFlags.OverTimed | global::NetClockTester.ValidityFlags.AheadOfServerTime))
			{
			case (global::NetClockTester.ValidityFlags)0:
				if ((vf & global::NetClockTester.ValidityFlags.Valid) == global::NetClockTester.ValidityFlags.Valid)
				{
					this.Valid += 1u;
				}
				break;
			case global::NetClockTester.ValidityFlags.TooFrequent:
				this.TooFrequent += 1u;
				break;
			case global::NetClockTester.ValidityFlags.OverTimed:
				this.OverTimed += 1u;
				break;
			case global::NetClockTester.ValidityFlags.TooFrequent | global::NetClockTester.ValidityFlags.OverTimed:
				this.OverTimed += 1u;
				this.TooFrequent += 1u;
				break;
			case global::NetClockTester.ValidityFlags.AheadOfServerTime:
				this.AheadOfServerTime += 1u;
				break;
			case global::NetClockTester.ValidityFlags.TooFrequent | global::NetClockTester.ValidityFlags.AheadOfServerTime:
				this.AheadOfServerTime += 1u;
				this.TooFrequent += 1u;
				break;
			case global::NetClockTester.ValidityFlags.OverTimed | global::NetClockTester.ValidityFlags.AheadOfServerTime:
				this.AheadOfServerTime += 1u;
				this.OverTimed += 1u;
				break;
			case global::NetClockTester.ValidityFlags.TooFrequent | global::NetClockTester.ValidityFlags.OverTimed | global::NetClockTester.ValidityFlags.AheadOfServerTime:
				this.AheadOfServerTime += 1u;
				this.OverTimed += 1u;
				this.TooFrequent += 1u;
				break;
			}
		}

		// Token: 0x04000F63 RID: 3939
		public uint TooFrequent;

		// Token: 0x04000F64 RID: 3940
		public uint OverTimed;

		// Token: 0x04000F65 RID: 3941
		public uint AheadOfServerTime;

		// Token: 0x04000F66 RID: 3942
		public uint Valid;
	}
}
