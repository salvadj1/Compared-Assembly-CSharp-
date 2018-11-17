using System;
using uLink;

// Token: 0x02000304 RID: 772
public struct NetClockTester
{
	// Token: 0x1700076B RID: 1899
	// (get) Token: 0x06001DA4 RID: 7588 RVA: 0x00074AA8 File Offset: 0x00072CA8
	public bool Any
	{
		get
		{
			return this.Count > 0UL;
		}
	}

	// Token: 0x1700076C RID: 1900
	// (get) Token: 0x06001DA5 RID: 7589 RVA: 0x00074AB4 File Offset: 0x00072CB4
	public bool Empty
	{
		get
		{
			return this.Count == 0UL;
		}
	}

	// Token: 0x06001DA6 RID: 7590 RVA: 0x00074AC0 File Offset: 0x00072CC0
	public static NetClockTester.ValidityFlags TestValidity(ref NetClockTester test, ref NetworkMessageInfo info, double intervalSec, NetClockTester.ValidityFlags testFor)
	{
		return NetClockTester.TestValidity(ref test, ref info, (long)Math.Floor(intervalSec * 1000.0), testFor);
	}

	// Token: 0x06001DA7 RID: 7591 RVA: 0x00074ADC File Offset: 0x00072CDC
	public static NetClockTester.ValidityFlags TestValidity(ref NetClockTester test, ref NetworkMessageInfo info, long intervalMS, NetClockTester.ValidityFlags testFor)
	{
		NetClockTester.ValidityFlags validityFlags = NetClockTester.TestValidity(ref test, info.timestampInMillis, intervalMS);
		test.Results.Add(validityFlags & testFor);
		return validityFlags;
	}

	// Token: 0x06001DA8 RID: 7592 RVA: 0x00074B08 File Offset: 0x00072D08
	private static NetClockTester.ValidityFlags TestValidity(ref NetClockTester test, ulong timeStamp, long minimalSendRateMS)
	{
		ulong timeInMillis = NetCull.timeInMillis;
		NetClockTester.ValidityFlags validityFlags = (timeInMillis >= timeStamp) ? ((NetClockTester.ValidityFlags)0) : NetClockTester.ValidityFlags.AheadOfServerTime;
		if (test.Count > 0UL)
		{
			long num = NetClockTester.Subtract(timeStamp, test.Send.Last);
			long num2 = NetClockTester.Subtract(timeInMillis, test.Receive.Last);
			test.Send.Sum = NetClockTester.Add(test.Send.Sum, num);
			test.Receive.Sum = NetClockTester.Add(test.Receive.Sum, num2);
			test.Count += 1UL;
			test.Send.Last = timeStamp;
			test.Receive.Last = timeInMillis;
			if (num < minimalSendRateMS)
			{
				validityFlags |= NetClockTester.ValidityFlags.TooFrequent;
			}
			long num3 = NetClockTester.Subtract(test.Send.Last, test.Send.First);
			long num4 = NetClockTester.Subtract(test.Receive.Last, test.Receive.First);
			if (test.Count >= 5UL)
			{
				if (num3 > num4 * 2L)
				{
					validityFlags |= NetClockTester.ValidityFlags.OverTimed;
				}
			}
			else if (test.Count >= 3UL && num3 > num4 * 4L)
			{
				validityFlags |= NetClockTester.ValidityFlags.OverTimed;
			}
			NetClockTester.ValidityFlags lastTestFlags = test.LastTestFlags;
			test.LastTestFlags = validityFlags;
			if ((validityFlags & NetClockTester.ValidityFlags.TooFrequent) == NetClockTester.ValidityFlags.TooFrequent && (lastTestFlags & NetClockTester.ValidityFlags.TooFrequent) != NetClockTester.ValidityFlags.TooFrequent)
			{
				validityFlags &= ~NetClockTester.ValidityFlags.TooFrequent;
				test.Count = 1UL;
				test.Send.First = test.Send.Last;
				test.Send.Sum = 0UL;
				if (num2 > 0L)
				{
					test.Receive.First = (ulong)NetClockTester.Subtract(test.Receive.Last, (ulong)num2);
					test.Receive.Sum = (ulong)num2;
				}
				else
				{
					test.Receive.First = test.Receive.Last;
					test.Receive.Sum = 0UL;
				}
			}
			return (validityFlags != (NetClockTester.ValidityFlags)0) ? validityFlags : NetClockTester.ValidityFlags.Valid;
		}
		test.Send.Sum = (test.Receive.Sum = 0UL);
		test.Send.First = timeStamp;
		test.Send.Last = timeStamp;
		test.Receive.Last = (test.Receive.First = timeInMillis);
		test.Count = 1UL;
		return validityFlags;
	}

	// Token: 0x1700076D RID: 1901
	// (get) Token: 0x06001DA9 RID: 7593 RVA: 0x00074D54 File Offset: 0x00072F54
	public static NetClockTester Reset
	{
		get
		{
			return default(NetClockTester);
		}
	}

	// Token: 0x06001DAA RID: 7594 RVA: 0x00074D6C File Offset: 0x00072F6C
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

	// Token: 0x06001DAB RID: 7595 RVA: 0x00074D88 File Offset: 0x00072F88
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

	// Token: 0x04000E16 RID: 3606
	public NetClockTester.Stamping Send;

	// Token: 0x04000E17 RID: 3607
	public NetClockTester.Stamping Receive;

	// Token: 0x04000E18 RID: 3608
	[NonSerialized]
	public ulong Count;

	// Token: 0x04000E19 RID: 3609
	public NetClockTester.Validity Results;

	// Token: 0x04000E1A RID: 3610
	public NetClockTester.ValidityFlags LastTestFlags;

	// Token: 0x02000305 RID: 773
	public struct Stamping
	{
		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06001DAC RID: 7596 RVA: 0x00074DA8 File Offset: 0x00072FA8
		public long Duration
		{
			get
			{
				return NetClockTester.Subtract(this.Last, this.First);
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06001DAD RID: 7597 RVA: 0x00074DBC File Offset: 0x00072FBC
		public long Variance
		{
			get
			{
				return (long)(this.Sum - (ulong)NetClockTester.Subtract(this.Last, this.First));
			}
		}

		// Token: 0x04000E1B RID: 3611
		public ulong Last;

		// Token: 0x04000E1C RID: 3612
		public ulong First;

		// Token: 0x04000E1D RID: 3613
		public ulong Sum;
	}

	// Token: 0x02000306 RID: 774
	[Flags]
	public enum ValidityFlags
	{
		// Token: 0x04000E1F RID: 3615
		Valid = 1,
		// Token: 0x04000E20 RID: 3616
		TooFrequent = 2,
		// Token: 0x04000E21 RID: 3617
		OverTimed = 4,
		// Token: 0x04000E22 RID: 3618
		AheadOfServerTime = 8
	}

	// Token: 0x02000307 RID: 775
	public struct Validity
	{
		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06001DAE RID: 7598 RVA: 0x00074DD8 File Offset: 0x00072FD8
		public NetClockTester.ValidityFlags Flags
		{
			get
			{
				if (this.TooFrequent > 0u)
				{
					if (this.OverTimed > 0u)
					{
						if (this.AheadOfServerTime > 0u)
						{
							return NetClockTester.ValidityFlags.TooFrequent | NetClockTester.ValidityFlags.OverTimed | NetClockTester.ValidityFlags.AheadOfServerTime;
						}
						return NetClockTester.ValidityFlags.TooFrequent | NetClockTester.ValidityFlags.OverTimed;
					}
					else
					{
						if (this.AheadOfServerTime > 0u)
						{
							return NetClockTester.ValidityFlags.TooFrequent | NetClockTester.ValidityFlags.AheadOfServerTime;
						}
						return NetClockTester.ValidityFlags.TooFrequent;
					}
				}
				else if (this.OverTimed > 0u)
				{
					if (this.AheadOfServerTime > 0u)
					{
						return NetClockTester.ValidityFlags.OverTimed | NetClockTester.ValidityFlags.AheadOfServerTime;
					}
					return NetClockTester.ValidityFlags.OverTimed;
				}
				else
				{
					if (this.AheadOfServerTime > 0u)
					{
						return NetClockTester.ValidityFlags.AheadOfServerTime;
					}
					if (this.Valid > 0u)
					{
						return NetClockTester.ValidityFlags.Valid;
					}
					return (NetClockTester.ValidityFlags)0;
				}
			}
		}

		// Token: 0x06001DAF RID: 7599 RVA: 0x00074E5C File Offset: 0x0007305C
		public void Add(NetClockTester.ValidityFlags vf)
		{
			switch (vf & (NetClockTester.ValidityFlags.TooFrequent | NetClockTester.ValidityFlags.OverTimed | NetClockTester.ValidityFlags.AheadOfServerTime))
			{
			case (NetClockTester.ValidityFlags)0:
				if ((vf & NetClockTester.ValidityFlags.Valid) == NetClockTester.ValidityFlags.Valid)
				{
					this.Valid += 1u;
				}
				break;
			case NetClockTester.ValidityFlags.TooFrequent:
				this.TooFrequent += 1u;
				break;
			case NetClockTester.ValidityFlags.OverTimed:
				this.OverTimed += 1u;
				break;
			case NetClockTester.ValidityFlags.TooFrequent | NetClockTester.ValidityFlags.OverTimed:
				this.OverTimed += 1u;
				this.TooFrequent += 1u;
				break;
			case NetClockTester.ValidityFlags.AheadOfServerTime:
				this.AheadOfServerTime += 1u;
				break;
			case NetClockTester.ValidityFlags.TooFrequent | NetClockTester.ValidityFlags.AheadOfServerTime:
				this.AheadOfServerTime += 1u;
				this.TooFrequent += 1u;
				break;
			case NetClockTester.ValidityFlags.OverTimed | NetClockTester.ValidityFlags.AheadOfServerTime:
				this.AheadOfServerTime += 1u;
				this.OverTimed += 1u;
				break;
			case NetClockTester.ValidityFlags.TooFrequent | NetClockTester.ValidityFlags.OverTimed | NetClockTester.ValidityFlags.AheadOfServerTime:
				this.AheadOfServerTime += 1u;
				this.OverTimed += 1u;
				this.TooFrequent += 1u;
				break;
			}
		}

		// Token: 0x04000E23 RID: 3619
		public uint TooFrequent;

		// Token: 0x04000E24 RID: 3620
		public uint OverTimed;

		// Token: 0x04000E25 RID: 3621
		public uint AheadOfServerTime;

		// Token: 0x04000E26 RID: 3622
		public uint Valid;
	}
}
