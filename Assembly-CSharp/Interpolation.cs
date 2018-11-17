using System;

// Token: 0x020002F0 RID: 752
public static class Interpolation
{
	// Token: 0x06001A47 RID: 6727 RVA: 0x00066668 File Offset: 0x00064868
	static Interpolation()
	{
		global::Interpolation.BindTiming(20UL, 1.5, 5f);
	}

	// Token: 0x17000779 RID: 1913
	// (get) Token: 0x06001A48 RID: 6728 RVA: 0x00066680 File Offset: 0x00064880
	public static double deltaSeconds
	{
		get
		{
			return global::Interpolation._deltaSeconds;
		}
	}

	// Token: 0x1700077A RID: 1914
	// (get) Token: 0x06001A49 RID: 6729 RVA: 0x00066688 File Offset: 0x00064888
	public static double totalDelaySeconds
	{
		get
		{
			return global::Interpolation._totalDelaySeconds;
		}
	}

	// Token: 0x1700077B RID: 1915
	// (get) Token: 0x06001A4A RID: 6730 RVA: 0x00066690 File Offset: 0x00064890
	public static ulong totalDelayMillis
	{
		get
		{
			return global::Interpolation._totalDelayMillis;
		}
	}

	// Token: 0x1700077C RID: 1916
	// (get) Token: 0x06001A4B RID: 6731 RVA: 0x00066698 File Offset: 0x00064898
	// (set) Token: 0x06001A4C RID: 6732 RVA: 0x000666A0 File Offset: 0x000648A0
	public static double delaySeconds
	{
		get
		{
			return global::Interpolation._delaySeconds;
		}
		set
		{
			if (value < 0.0005)
			{
				global::Interpolation.delayMillis = 0UL;
			}
			else
			{
				global::Interpolation.delayMillis = (ulong)Math.Round(value * 1000.0);
			}
		}
	}

	// Token: 0x1700077D RID: 1917
	// (get) Token: 0x06001A4D RID: 6733 RVA: 0x000666D4 File Offset: 0x000648D4
	// (set) Token: 0x06001A4E RID: 6734 RVA: 0x000666DC File Offset: 0x000648DC
	public static ulong delayMillis
	{
		get
		{
			return global::Interpolation._delayMillis;
		}
		set
		{
			if (value != global::Interpolation._delayMillis)
			{
				global::Interpolation.BindTiming(value, global::Interpolation._ratio, global::Interpolation._sendRate);
			}
		}
	}

	// Token: 0x1700077E RID: 1918
	// (get) Token: 0x06001A4F RID: 6735 RVA: 0x000666FC File Offset: 0x000648FC
	public static ulong delayFromSendRateMillis
	{
		get
		{
			return global::Interpolation._delayFromSendRateMillis;
		}
	}

	// Token: 0x1700077F RID: 1919
	// (get) Token: 0x06001A50 RID: 6736 RVA: 0x00066704 File Offset: 0x00064904
	public static double delayFromSendRateSeconds
	{
		get
		{
			return global::Interpolation._delayFromSendRateSeconds;
		}
	}

	// Token: 0x17000780 RID: 1920
	// (get) Token: 0x06001A51 RID: 6737 RVA: 0x0006670C File Offset: 0x0006490C
	public static float delayFromSendRateSecondsf
	{
		get
		{
			return (float)global::Interpolation._delayFromSendRateSeconds;
		}
	}

	// Token: 0x17000781 RID: 1921
	// (get) Token: 0x06001A52 RID: 6738 RVA: 0x00066714 File Offset: 0x00064914
	// (set) Token: 0x06001A53 RID: 6739 RVA: 0x0006671C File Offset: 0x0006491C
	public static double sendRateRatio
	{
		get
		{
			return global::Interpolation._ratio;
		}
		set
		{
			if (value != global::Interpolation._ratio)
			{
				global::Interpolation.BindTiming(global::Interpolation._delayMillis, value, global::Interpolation._sendRate);
			}
		}
	}

	// Token: 0x17000782 RID: 1922
	// (get) Token: 0x06001A54 RID: 6740 RVA: 0x0006673C File Offset: 0x0006493C
	// (set) Token: 0x06001A55 RID: 6741 RVA: 0x00066744 File Offset: 0x00064944
	public static float sendRate
	{
		get
		{
			return global::Interpolation._sendRate;
		}
		set
		{
			if (value != global::Interpolation._sendRate)
			{
				global::Interpolation.BindTiming(global::Interpolation._delayMillis, global::Interpolation._ratio, value);
			}
		}
	}

	// Token: 0x17000783 RID: 1923
	// (get) Token: 0x06001A56 RID: 6742 RVA: 0x00066764 File Offset: 0x00064964
	// (set) Token: 0x06001A57 RID: 6743 RVA: 0x0006676C File Offset: 0x0006496C
	public static float delaySecondsf
	{
		get
		{
			return (float)global::Interpolation._delaySeconds;
		}
		set
		{
			global::Interpolation.delaySeconds = (double)value;
		}
	}

	// Token: 0x17000784 RID: 1924
	// (get) Token: 0x06001A58 RID: 6744 RVA: 0x00066778 File Offset: 0x00064978
	public static float deltaSecondsf
	{
		get
		{
			return (float)global::Interpolation._deltaSeconds;
		}
	}

	// Token: 0x17000785 RID: 1925
	// (get) Token: 0x06001A59 RID: 6745 RVA: 0x00066780 File Offset: 0x00064980
	// (set) Token: 0x06001A5A RID: 6746 RVA: 0x00066788 File Offset: 0x00064988
	public static float sendRateRatiof
	{
		get
		{
			return (float)global::Interpolation._ratio;
		}
		set
		{
			global::Interpolation.sendRateRatio = (double)value;
		}
	}

	// Token: 0x17000786 RID: 1926
	// (get) Token: 0x06001A5B RID: 6747 RVA: 0x00066794 File Offset: 0x00064994
	public static float totalDelaySecondsf
	{
		get
		{
			return (float)global::Interpolation._totalDelaySeconds;
		}
	}

	// Token: 0x06001A5C RID: 6748 RVA: 0x0006679C File Offset: 0x0006499C
	public static double AddDelayToTimeStampSeconds(double timeStamp)
	{
		return timeStamp + global::Interpolation._totalDelaySeconds;
	}

	// Token: 0x06001A5D RID: 6749 RVA: 0x000667A8 File Offset: 0x000649A8
	public static ulong AddDelayToTimeStampMillis(ulong timestamp)
	{
		return timestamp + global::Interpolation._totalDelayMillis;
	}

	// Token: 0x06001A5E RID: 6750 RVA: 0x000667B4 File Offset: 0x000649B4
	public static double GetInterpolationTimeSeconds(double timeStamp)
	{
		return timeStamp + global::Interpolation._deltaSeconds;
	}

	// Token: 0x06001A5F RID: 6751 RVA: 0x000667C0 File Offset: 0x000649C0
	public static ulong GetInterpolationTimeMillis(ulong timestamp)
	{
		if (timestamp < global::Interpolation._totalDelayMillis)
		{
			return 0UL;
		}
		return timestamp - global::Interpolation._totalDelayMillis;
	}

	// Token: 0x06001A60 RID: 6752 RVA: 0x000667D8 File Offset: 0x000649D8
	public static void BindTiming(ulong? delayMillis, double? sendRateRatio, float? sendRate)
	{
		global::Interpolation.BindTiming((delayMillis == null) ? global::Interpolation._delayMillis : delayMillis.Value, (sendRateRatio == null) ? global::Interpolation._ratio : sendRateRatio.Value, (sendRate == null) ? global::Interpolation._sendRate : sendRate.Value);
	}

	// Token: 0x06001A61 RID: 6753 RVA: 0x00066844 File Offset: 0x00064A44
	public static void BindTiming(ulong delayMillis, double sendRateRatio, float sendRate)
	{
		global::Interpolation._sendRate = sendRate;
		global::Interpolation._ratio = sendRateRatio;
		if (sendRate == 0f || sendRateRatio == 0.0 || sendRate < 0f != sendRateRatio < 0.0)
		{
			global::Interpolation._delayFromSendRateMillis = 0UL;
		}
		else
		{
			global::Interpolation._delayFromSendRateMillis = (ulong)Math.Ceiling(1000.0 * sendRateRatio / (double)sendRate);
		}
		global::Interpolation._delayMillis = delayMillis;
		global::Interpolation._totalDelayMillis = global::Interpolation._delayFromSendRateMillis + global::Interpolation._delayMillis;
		global::Interpolation._delaySeconds = global::Interpolation._delayMillis * 0.001;
		global::Interpolation._delayFromSendRateSeconds = global::Interpolation._delayFromSendRateMillis * 0.001;
		global::Interpolation._totalDelaySeconds = global::Interpolation._totalDelayMillis * 0.001;
		global::Interpolation._deltaSeconds = -global::Interpolation._totalDelaySeconds;
		global::Interpolation.@struct = global::Interpolation.Capture();
	}

	// Token: 0x06001A62 RID: 6754 RVA: 0x00066924 File Offset: 0x00064B24
	public static void BindTimingNetCull(ulong delayMillis, double sendRateRatio)
	{
		global::Interpolation.BindTiming(delayMillis, sendRateRatio, global::NetCull.sendRate);
	}

	// Token: 0x06001A63 RID: 6755 RVA: 0x00066934 File Offset: 0x00064B34
	public static void BindTimingNetCull(ulong? delayMillis, double? sendRateRatio)
	{
		global::Interpolation.BindTiming((delayMillis == null) ? global::Interpolation._delayMillis : delayMillis.Value, (sendRateRatio == null) ? global::Interpolation._ratio : sendRateRatio.Value, global::NetCull.sendRate);
	}

	// Token: 0x06001A64 RID: 6756 RVA: 0x00066988 File Offset: 0x00064B88
	public static void BindTiming()
	{
		global::Interpolation.BindTiming(global::Interpolation._delayMillis, global::Interpolation._ratio, global::Interpolation._sendRate);
	}

	// Token: 0x06001A65 RID: 6757 RVA: 0x000669A0 File Offset: 0x00064BA0
	public static void BindTimingNetCull()
	{
		global::Interpolation.BindTiming(global::Interpolation._delayMillis, global::Interpolation._ratio, global::NetCull.sendRate);
	}

	// Token: 0x06001A66 RID: 6758 RVA: 0x000669B8 File Offset: 0x00064BB8
	public static global::Interpolation.TimingData Capture()
	{
		return new global::Interpolation.TimingData(global::Interpolation._ratio, global::Interpolation._deltaSeconds, global::Interpolation._totalDelaySeconds, global::Interpolation._delaySeconds, global::Interpolation._delayFromSendRateSeconds, global::Interpolation._totalDelayMillis, global::Interpolation._delayFromSendRateMillis, global::Interpolation._delayMillis, global::Interpolation._sendRate);
	}

	// Token: 0x17000787 RID: 1927
	// (get) Token: 0x06001A67 RID: 6759 RVA: 0x000669F8 File Offset: 0x00064BF8
	public static double time
	{
		get
		{
			return global::NetCull.time + global::Interpolation._deltaSeconds;
		}
	}

	// Token: 0x17000788 RID: 1928
	// (get) Token: 0x06001A68 RID: 6760 RVA: 0x00066A08 File Offset: 0x00064C08
	public static double localTime
	{
		get
		{
			return global::NetCull.localTime + global::Interpolation._deltaSeconds;
		}
	}

	// Token: 0x17000789 RID: 1929
	// (get) Token: 0x06001A69 RID: 6761 RVA: 0x00066A18 File Offset: 0x00064C18
	public static ulong timeInMillis
	{
		get
		{
			ulong num = global::NetCull.timeInMillis;
			if (num < global::Interpolation._totalDelayMillis)
			{
				num = 0UL;
			}
			else
			{
				num -= global::Interpolation._totalDelayMillis;
			}
			return num;
		}
	}

	// Token: 0x1700078A RID: 1930
	// (get) Token: 0x06001A6A RID: 6762 RVA: 0x00066A48 File Offset: 0x00064C48
	public static ulong localTimeInMillis
	{
		get
		{
			ulong num = global::NetCull.localTimeInMillis;
			if (num < global::Interpolation._totalDelayMillis)
			{
				num = 0UL;
			}
			else
			{
				num -= global::Interpolation._totalDelayMillis;
			}
			return num;
		}
	}

	// Token: 0x04000E65 RID: 3685
	private const float kDefaultSendRateRatio = 1.5f;

	// Token: 0x04000E66 RID: 3686
	private const int kDefaultDelayMillis = 20;

	// Token: 0x04000E67 RID: 3687
	private const float kDefaultSendRate = 5f;

	// Token: 0x04000E68 RID: 3688
	private static double _ratio;

	// Token: 0x04000E69 RID: 3689
	private static ulong _totalDelayMillis;

	// Token: 0x04000E6A RID: 3690
	private static ulong _delayFromSendRateMillis;

	// Token: 0x04000E6B RID: 3691
	private static ulong _delayMillis;

	// Token: 0x04000E6C RID: 3692
	private static double _delaySeconds;

	// Token: 0x04000E6D RID: 3693
	private static double _totalDelaySeconds;

	// Token: 0x04000E6E RID: 3694
	private static double _deltaSeconds;

	// Token: 0x04000E6F RID: 3695
	private static double _delayFromSendRateSeconds;

	// Token: 0x04000E70 RID: 3696
	private static float _sendRate;

	// Token: 0x04000E71 RID: 3697
	public static global::Interpolation.TimingData @struct;

	// Token: 0x020002F1 RID: 753
	public struct TimingData
	{
		// Token: 0x06001A6B RID: 6763 RVA: 0x00066A78 File Offset: 0x00064C78
		public TimingData(double sendRateRatio, double deltaSeconds, double totalDelaySeconds, double delaySeconds, double delayFromSendRateSeconds, ulong totalDelayMillis, ulong delayFromSendRateMillis, ulong delayMillis, float sendRate)
		{
			this.sendRateRatio = sendRateRatio;
			this.deltaSeconds = deltaSeconds;
			this.totalDelaySeconds = totalDelaySeconds;
			this.delaySeconds = delaySeconds;
			this.delayFromSendRateSeconds = delayFromSendRateSeconds;
			this.totalDelayMillis = totalDelayMillis;
			this.delayFromSendRateMillis = delayFromSendRateMillis;
			this.delayMillis = delayMillis;
			this.sendRate = sendRate;
			this.sendRateRatioF = (float)sendRateRatio;
			this.deltaSecondsF = (float)deltaSeconds;
			this.totalDelaySecondsF = (float)totalDelaySeconds;
			this.delaySecondsF = (float)delaySeconds;
			this.delayFromSendRateSecondsF = (float)delayFromSendRateSeconds;
		}

		// Token: 0x04000E72 RID: 3698
		public readonly double sendRateRatio;

		// Token: 0x04000E73 RID: 3699
		public readonly double deltaSeconds;

		// Token: 0x04000E74 RID: 3700
		public readonly double totalDelaySeconds;

		// Token: 0x04000E75 RID: 3701
		public readonly double delaySeconds;

		// Token: 0x04000E76 RID: 3702
		public readonly double delayFromSendRateSeconds;

		// Token: 0x04000E77 RID: 3703
		public readonly float sendRateRatioF;

		// Token: 0x04000E78 RID: 3704
		public readonly float deltaSecondsF;

		// Token: 0x04000E79 RID: 3705
		public readonly float totalDelaySecondsF;

		// Token: 0x04000E7A RID: 3706
		public readonly float delaySecondsF;

		// Token: 0x04000E7B RID: 3707
		public readonly float delayFromSendRateSecondsF;

		// Token: 0x04000E7C RID: 3708
		public readonly ulong totalDelayMillis;

		// Token: 0x04000E7D RID: 3709
		public readonly ulong delayFromSendRateMillis;

		// Token: 0x04000E7E RID: 3710
		public readonly ulong delayMillis;

		// Token: 0x04000E7F RID: 3711
		public readonly float sendRate;
	}
}
