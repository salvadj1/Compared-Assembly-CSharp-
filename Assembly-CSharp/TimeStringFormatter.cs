using System;

// Token: 0x020001CA RID: 458
public struct TimeStringFormatter
{
	// Token: 0x06000CF2 RID: 3314 RVA: 0x00032858 File Offset: 0x00030A58
	private TimeStringFormatter(string aDay, string days, string aHour, string hours, string aMinute, string minutes, string aSecond, string seconds, string lessThanASecond)
	{
		this.aDay = aDay;
		this.days = days;
		this.aHour = aHour;
		this.hours = hours;
		this.aMinute = aMinute;
		this.minutes = minutes;
		this.aSecond = aSecond;
		this.seconds = seconds;
		this.lessThanASecond = lessThanASecond;
	}

	// Token: 0x06000CF3 RID: 3315 RVA: 0x000328AC File Offset: 0x00030AAC
	private static string DoMerge(string value)
	{
		return value.Replace("{", "{{").Replace("}", "}}").Replace("<ꪻ뮪>", "{0}");
	}

	// Token: 0x06000CF4 RID: 3316 RVA: 0x000328E8 File Offset: 0x00030AE8
	private static string Merge(string prefix)
	{
		return TimeStringFormatter.DoMerge(prefix ?? string.Empty);
	}

	// Token: 0x06000CF5 RID: 3317 RVA: 0x000328FC File Offset: 0x00030AFC
	private static string Merge(string prefix, string qualifier)
	{
		return TimeStringFormatter.DoMerge((prefix ?? string.Empty) + (qualifier ?? string.Empty));
	}

	// Token: 0x06000CF6 RID: 3318 RVA: 0x00032930 File Offset: 0x00030B30
	private static string Merge(string prefix, string qualifier, string suffix)
	{
		return TimeStringFormatter.DoMerge((prefix ?? string.Empty) + (qualifier ?? string.Empty) + (suffix ?? string.Empty));
	}

	// Token: 0x06000CF7 RID: 3319 RVA: 0x00032964 File Offset: 0x00030B64
	public static TimeStringFormatter Define(TimeStringFormatter.Qualifier qualifier)
	{
		return new TimeStringFormatter(TimeStringFormatter.Merge(qualifier.aDay), TimeStringFormatter.Merge(qualifier.days), TimeStringFormatter.Merge(qualifier.aHour), TimeStringFormatter.Merge(qualifier.hours), TimeStringFormatter.Merge(qualifier.aMinute), TimeStringFormatter.Merge(qualifier.minutes), TimeStringFormatter.Merge(qualifier.aSecond), TimeStringFormatter.Merge(qualifier.seconds), TimeStringFormatter.Merge(qualifier.lessThanASecond));
	}

	// Token: 0x06000CF8 RID: 3320 RVA: 0x000329E4 File Offset: 0x00030BE4
	public static TimeStringFormatter Define(string prefix, TimeStringFormatter.Qualifier qualifier)
	{
		if (string.IsNullOrEmpty(prefix))
		{
			return TimeStringFormatter.Define(qualifier);
		}
		return new TimeStringFormatter(TimeStringFormatter.Merge(prefix, qualifier.aDay), TimeStringFormatter.Merge(prefix, qualifier.days), TimeStringFormatter.Merge(prefix, qualifier.aHour), TimeStringFormatter.Merge(prefix, qualifier.hours), TimeStringFormatter.Merge(prefix, qualifier.aMinute), TimeStringFormatter.Merge(prefix, qualifier.minutes), TimeStringFormatter.Merge(prefix, qualifier.aSecond), TimeStringFormatter.Merge(prefix, qualifier.seconds), TimeStringFormatter.Merge(prefix, qualifier.lessThanASecond));
	}

	// Token: 0x06000CF9 RID: 3321 RVA: 0x00032A80 File Offset: 0x00030C80
	public static TimeStringFormatter Define(TimeStringFormatter.Qualifier qualifier, string suffix)
	{
		if (string.IsNullOrEmpty(suffix))
		{
			return TimeStringFormatter.Define(qualifier);
		}
		return new TimeStringFormatter(TimeStringFormatter.Merge(qualifier.aDay, suffix), TimeStringFormatter.Merge(qualifier.days, suffix), TimeStringFormatter.Merge(qualifier.aHour, suffix), TimeStringFormatter.Merge(qualifier.hours, suffix), TimeStringFormatter.Merge(qualifier.aMinute, suffix), TimeStringFormatter.Merge(qualifier.minutes, suffix), TimeStringFormatter.Merge(qualifier.aSecond, suffix), TimeStringFormatter.Merge(qualifier.seconds, suffix), TimeStringFormatter.Merge(qualifier.lessThanASecond, suffix));
	}

	// Token: 0x06000CFA RID: 3322 RVA: 0x00032B1C File Offset: 0x00030D1C
	public static TimeStringFormatter Define(string prefix, TimeStringFormatter.Qualifier qualifier, string suffix)
	{
		if (string.IsNullOrEmpty(suffix))
		{
			if (string.IsNullOrEmpty(prefix))
			{
				return TimeStringFormatter.Define(qualifier);
			}
			return TimeStringFormatter.Define(prefix, qualifier);
		}
		else
		{
			if (string.IsNullOrEmpty(prefix))
			{
				return TimeStringFormatter.Define(qualifier, suffix);
			}
			return new TimeStringFormatter(TimeStringFormatter.Merge(prefix, qualifier.aDay, suffix), TimeStringFormatter.Merge(prefix, qualifier.days, suffix), TimeStringFormatter.Merge(prefix, qualifier.aHour, suffix), TimeStringFormatter.Merge(prefix, qualifier.hours, suffix), TimeStringFormatter.Merge(prefix, qualifier.aMinute, suffix), TimeStringFormatter.Merge(prefix, qualifier.minutes, suffix), TimeStringFormatter.Merge(prefix, qualifier.aSecond, suffix), TimeStringFormatter.Merge(prefix, qualifier.seconds, suffix), TimeStringFormatter.Merge(prefix, qualifier.lessThanASecond, suffix));
		}
	}

	// Token: 0x06000CFB RID: 3323 RVA: 0x00032BE4 File Offset: 0x00030DE4
	public static TimeStringFormatter Define(TimeStringFormatter formatter, string lessThanASecond)
	{
		if (!object.ReferenceEquals(lessThanASecond, null))
		{
			formatter = new TimeStringFormatter(formatter.aDay, formatter.days, formatter.aHour, formatter.hours, formatter.aMinute, formatter.minutes, formatter.aSecond, formatter.seconds, TimeStringFormatter.Merge(lessThanASecond));
		}
		return formatter;
	}

	// Token: 0x06000CFC RID: 3324 RVA: 0x00032C44 File Offset: 0x00030E44
	public static TimeStringFormatter Define(string prefix, TimeStringFormatter.Qualifier qualifier, string suffix, string lessThanASecond)
	{
		return TimeStringFormatter.Define(TimeStringFormatter.Define(prefix, qualifier, suffix), lessThanASecond);
	}

	// Token: 0x06000CFD RID: 3325 RVA: 0x00032C54 File Offset: 0x00030E54
	private static double Round(double total, TimeStringFormatter.Rounding rounding, int decimalPlaces, double fancyUnits)
	{
		if (total <= 0.0)
		{
			return 0.0;
		}
		switch (rounding)
		{
		case TimeStringFormatter.Rounding.Floor:
			return Math.Floor(total);
		case TimeStringFormatter.Rounding.Ceiling:
			return Math.Ceiling(total);
		case TimeStringFormatter.Rounding.Round:
			return Math.Round(total);
		case TimeStringFormatter.Rounding.Decimal:
			fancyUnits = 1.0;
			decimalPlaces = 0;
			break;
		case TimeStringFormatter.Rounding.RoundedDecimal:
			fancyUnits = 1.0;
			break;
		case TimeStringFormatter.Rounding.FancyDecimal:
			decimalPlaces = 0;
			break;
		}
		if (decimalPlaces == 0)
		{
			return total;
		}
		double num = Math.Floor(total);
		return num + Math.Floor((total - num) * fancyUnits * ((double)decimalPlaces * 10.0)) / (10.0 * (double)decimalPlaces);
	}

	// Token: 0x06000CFE RID: 3326 RVA: 0x00032D20 File Offset: 0x00030F20
	public string GetFormattingString(TimeSpan timePassed)
	{
		return this.GetFormattingString(timePassed, TimeStringFormatter.Rounding.Floor);
	}

	// Token: 0x06000CFF RID: 3327 RVA: 0x00032D2C File Offset: 0x00030F2C
	public string GetFormattingString(TimeSpan timePassed, TimeStringFormatter.Rounding rounding)
	{
		int num2;
		double num = TimeStringFormatter.Round(timePassed.TotalSeconds, rounding, num2 = 2, 1.0);
		string format;
		if (num <= 0.0)
		{
			format = this.lessThanASecond;
		}
		else if (num == 1.0)
		{
			format = this.aSecond;
		}
		else if (num < 60.0)
		{
			format = this.seconds;
		}
		else if ((num = TimeStringFormatter.Round(timePassed.TotalMinutes, rounding, num2 = 2, 0.6)) == 1.0)
		{
			format = this.aMinute;
		}
		else if (num < 60.0)
		{
			format = this.minutes;
		}
		else if ((num = TimeStringFormatter.Round(timePassed.TotalHours, rounding, num2 = 2, 1.0)) == 1.0)
		{
			format = this.aHour;
		}
		else if (num < 24.0)
		{
			format = this.hours;
		}
		else if ((num = TimeStringFormatter.Round(timePassed.TotalDays, rounding, num2 = 2, 0.24)) == 1.0)
		{
			format = this.aDay;
		}
		else
		{
			format = this.days;
		}
		object arg;
		if (rounding == TimeStringFormatter.Rounding.RoundedDecimal || rounding == TimeStringFormatter.Rounding.FancyDecimal || rounding == TimeStringFormatter.Rounding.RoundedFancyDecimal)
		{
			string text;
			if (rounding == TimeStringFormatter.Rounding.RoundedDecimal || rounding == TimeStringFormatter.Rounding.RoundedFancyDecimal)
			{
				if (num2 != 2)
				{
					throw new NotSupportedException("We gotta add support for that");
				}
				text = num.ToString("0.00");
			}
			else
			{
				text = num.ToString();
			}
			if (rounding == TimeStringFormatter.Rounding.FancyDecimal || rounding == TimeStringFormatter.Rounding.RoundedFancyDecimal)
			{
				arg = text.Replace('.', ':');
			}
			else
			{
				arg = text;
			}
		}
		else if (rounding != TimeStringFormatter.Rounding.Decimal && !double.IsNaN(num) && !double.IsInfinity(num))
		{
			arg = (int)num;
		}
		else
		{
			arg = num;
		}
		return string.Format(format, arg);
	}

	// Token: 0x040007A7 RID: 1959
	public const string kArgumentTime = "<ꪻ뮪>";

	// Token: 0x040007A8 RID: 1960
	private const string kArgumentTimeReplacement = "{0}";

	// Token: 0x040007A9 RID: 1961
	public const string kPeriod = ".";

	// Token: 0x040007AA RID: 1962
	public readonly string aDay;

	// Token: 0x040007AB RID: 1963
	public readonly string days;

	// Token: 0x040007AC RID: 1964
	public readonly string aHour;

	// Token: 0x040007AD RID: 1965
	public readonly string hours;

	// Token: 0x040007AE RID: 1966
	public readonly string aMinute;

	// Token: 0x040007AF RID: 1967
	public readonly string minutes;

	// Token: 0x040007B0 RID: 1968
	public readonly string aSecond;

	// Token: 0x040007B1 RID: 1969
	public readonly string seconds;

	// Token: 0x040007B2 RID: 1970
	public readonly string lessThanASecond;

	// Token: 0x020001CB RID: 459
	public static class Quantity
	{
		// Token: 0x040007B3 RID: 1971
		public const string kPrefix = " ";

		// Token: 0x040007B4 RID: 1972
		public const string aDay = " a day";

		// Token: 0x040007B5 RID: 1973
		public const string days = " <ꪻ뮪> days";

		// Token: 0x040007B6 RID: 1974
		public const string aHour = " an hour";

		// Token: 0x040007B7 RID: 1975
		public const string hours = " <ꪻ뮪> hours";

		// Token: 0x040007B8 RID: 1976
		public const string aMinute = " a minute";

		// Token: 0x040007B9 RID: 1977
		public const string minutes = " <ꪻ뮪> minutes";

		// Token: 0x040007BA RID: 1978
		public const string aSecond = " a second";

		// Token: 0x040007BB RID: 1979
		public const string seconds = " <ꪻ뮪> seconds";

		// Token: 0x040007BC RID: 1980
		public const string lessThanASecond = "";

		// Token: 0x040007BD RID: 1981
		public static readonly TimeStringFormatter.Qualifier Qualifier = new TimeStringFormatter.Qualifier(" a day", " <ꪻ뮪> days", " an hour", " <ꪻ뮪> hours", " a minute", " <ꪻ뮪> minutes", " a second", " <ꪻ뮪> seconds", string.Empty);

		// Token: 0x020001CC RID: 460
		public static class Period
		{
			// Token: 0x040007BE RID: 1982
			public const string kSuffix = ".";

			// Token: 0x040007BF RID: 1983
			public const string aDay = " a day.";

			// Token: 0x040007C0 RID: 1984
			public const string days = " <ꪻ뮪> days.";

			// Token: 0x040007C1 RID: 1985
			public const string aHour = " an hour.";

			// Token: 0x040007C2 RID: 1986
			public const string hours = " <ꪻ뮪> hours.";

			// Token: 0x040007C3 RID: 1987
			public const string aMinute = " a minute.";

			// Token: 0x040007C4 RID: 1988
			public const string minutes = " <ꪻ뮪> minutes.";

			// Token: 0x040007C5 RID: 1989
			public const string aSecond = " a second.";

			// Token: 0x040007C6 RID: 1990
			public const string seconds = " <ꪻ뮪> seconds.";

			// Token: 0x040007C7 RID: 1991
			public const string lessThanASecond = ".";

			// Token: 0x040007C8 RID: 1992
			public static readonly TimeStringFormatter.Qualifier Qualifier = new TimeStringFormatter.Qualifier(" a day.", " <ꪻ뮪> days.", " an hour.", " <ꪻ뮪> hours.", " a minute.", " <ꪻ뮪> minutes.", " a second.", " <ꪻ뮪> seconds.", ".");
		}
	}

	// Token: 0x020001CD RID: 461
	public static class For
	{
		// Token: 0x040007C9 RID: 1993
		public const string kPrefix = " for";

		// Token: 0x040007CA RID: 1994
		public const string aDay = " for a day";

		// Token: 0x040007CB RID: 1995
		public const string days = " for <ꪻ뮪> days";

		// Token: 0x040007CC RID: 1996
		public const string aHour = " for an hour";

		// Token: 0x040007CD RID: 1997
		public const string hours = " for <ꪻ뮪> hours";

		// Token: 0x040007CE RID: 1998
		public const string aMinute = " for a minute";

		// Token: 0x040007CF RID: 1999
		public const string minutes = " for <ꪻ뮪> minutes";

		// Token: 0x040007D0 RID: 2000
		public const string aSecond = " for a second";

		// Token: 0x040007D1 RID: 2001
		public const string seconds = " for <ꪻ뮪> seconds";

		// Token: 0x040007D2 RID: 2002
		public const string lessThanASecond = "";

		// Token: 0x040007D3 RID: 2003
		public static readonly TimeStringFormatter.Qualifier Qualifier = new TimeStringFormatter.Qualifier(" for a day", " for <ꪻ뮪> days", " for an hour", " for <ꪻ뮪> hours", " for a minute", " for <ꪻ뮪> minutes", " for a second", " for <ꪻ뮪> seconds", string.Empty);

		// Token: 0x020001CE RID: 462
		public static class Period
		{
			// Token: 0x040007D4 RID: 2004
			public const string kSuffix = ".";

			// Token: 0x040007D5 RID: 2005
			public const string aDay = " for a day.";

			// Token: 0x040007D6 RID: 2006
			public const string days = " for <ꪻ뮪> days.";

			// Token: 0x040007D7 RID: 2007
			public const string aHour = " for an hour.";

			// Token: 0x040007D8 RID: 2008
			public const string hours = " for <ꪻ뮪> hours.";

			// Token: 0x040007D9 RID: 2009
			public const string aMinute = " for a minute.";

			// Token: 0x040007DA RID: 2010
			public const string minutes = " for <ꪻ뮪> minutes.";

			// Token: 0x040007DB RID: 2011
			public const string aSecond = " for a second.";

			// Token: 0x040007DC RID: 2012
			public const string seconds = " for <ꪻ뮪> seconds.";

			// Token: 0x040007DD RID: 2013
			public const string lessThanASecond = ".";

			// Token: 0x040007DE RID: 2014
			public static readonly TimeStringFormatter.Qualifier Qualifier = new TimeStringFormatter.Qualifier(" for a day.", " for <ꪻ뮪> days.", " for an hour.", " for <ꪻ뮪> hours.", " for a minute.", " for <ꪻ뮪> minutes.", " for a second.", " for <ꪻ뮪> seconds.", ".");
		}
	}

	// Token: 0x020001CF RID: 463
	public static class Ago
	{
		// Token: 0x040007DF RID: 2015
		public const string kSuffix = " ago";

		// Token: 0x040007E0 RID: 2016
		public const string aDay = " a day ago";

		// Token: 0x040007E1 RID: 2017
		public const string days = " <ꪻ뮪> days ago";

		// Token: 0x040007E2 RID: 2018
		public const string aHour = " an hour ago";

		// Token: 0x040007E3 RID: 2019
		public const string hours = " <ꪻ뮪> hours ago";

		// Token: 0x040007E4 RID: 2020
		public const string aMinute = " a minute ago";

		// Token: 0x040007E5 RID: 2021
		public const string minutes = " <ꪻ뮪> minutes ago";

		// Token: 0x040007E6 RID: 2022
		public const string aSecond = " a second ago";

		// Token: 0x040007E7 RID: 2023
		public const string seconds = " <ꪻ뮪> seconds ago";

		// Token: 0x040007E8 RID: 2024
		public const string lessThanASecond = "";

		// Token: 0x040007E9 RID: 2025
		public static readonly TimeStringFormatter.Qualifier Qualifier = new TimeStringFormatter.Qualifier(" a day ago", " <ꪻ뮪> days ago", " an hour ago", " <ꪻ뮪> hours ago", " a minute ago", " <ꪻ뮪> minutes ago", " a second ago", " <ꪻ뮪> seconds ago", string.Empty);

		// Token: 0x020001D0 RID: 464
		public static class Period
		{
			// Token: 0x040007EA RID: 2026
			public const string kSuffix = ".";

			// Token: 0x040007EB RID: 2027
			public const string aDay = " a day ago.";

			// Token: 0x040007EC RID: 2028
			public const string days = " <ꪻ뮪> days ago.";

			// Token: 0x040007ED RID: 2029
			public const string aHour = " an hour ago.";

			// Token: 0x040007EE RID: 2030
			public const string hours = " <ꪻ뮪> hours ago.";

			// Token: 0x040007EF RID: 2031
			public const string aMinute = " a minute ago.";

			// Token: 0x040007F0 RID: 2032
			public const string minutes = " <ꪻ뮪> minutes ago.";

			// Token: 0x040007F1 RID: 2033
			public const string aSecond = " a second ago.";

			// Token: 0x040007F2 RID: 2034
			public const string seconds = " <ꪻ뮪> seconds ago.";

			// Token: 0x040007F3 RID: 2035
			public const string lessThanASecond = ".";

			// Token: 0x040007F4 RID: 2036
			public static readonly TimeStringFormatter.Qualifier Qualifier = new TimeStringFormatter.Qualifier(" a day ago.", " <ꪻ뮪> days ago.", " an hour ago.", " <ꪻ뮪> hours ago.", " a minute ago.", " <ꪻ뮪> minutes ago.", " a second ago.", " <ꪻ뮪> seconds ago.", ".");
		}
	}

	// Token: 0x020001D1 RID: 465
	public static class SinceAgo
	{
		// Token: 0x040007F5 RID: 2037
		public const string kPrefix = " since";

		// Token: 0x040007F6 RID: 2038
		public const string aDay = " since a day ago";

		// Token: 0x040007F7 RID: 2039
		public const string days = " since <ꪻ뮪> days ago";

		// Token: 0x040007F8 RID: 2040
		public const string aHour = " since an hour ago";

		// Token: 0x040007F9 RID: 2041
		public const string hours = " since <ꪻ뮪> hours ago";

		// Token: 0x040007FA RID: 2042
		public const string aMinute = " since a minute ago";

		// Token: 0x040007FB RID: 2043
		public const string minutes = " since <ꪻ뮪> minutes ago";

		// Token: 0x040007FC RID: 2044
		public const string aSecond = " since a second ago";

		// Token: 0x040007FD RID: 2045
		public const string seconds = " since <ꪻ뮪> seconds ago";

		// Token: 0x040007FE RID: 2046
		public const string lessThanASecond = "";

		// Token: 0x040007FF RID: 2047
		public static readonly TimeStringFormatter.Qualifier Qualifier = new TimeStringFormatter.Qualifier(" since a day ago", " since <ꪻ뮪> days ago", " since an hour ago", " since <ꪻ뮪> hours ago", " since a minute ago", " since <ꪻ뮪> minutes ago", " since a second ago", " since <ꪻ뮪> seconds ago", string.Empty);

		// Token: 0x020001D2 RID: 466
		public static class Period
		{
			// Token: 0x04000800 RID: 2048
			public const string kSuffix = ".";

			// Token: 0x04000801 RID: 2049
			public const string aDay = " since a day ago.";

			// Token: 0x04000802 RID: 2050
			public const string days = " since <ꪻ뮪> days ago.";

			// Token: 0x04000803 RID: 2051
			public const string aHour = " since an hour ago.";

			// Token: 0x04000804 RID: 2052
			public const string hours = " since <ꪻ뮪> hours ago.";

			// Token: 0x04000805 RID: 2053
			public const string aMinute = " since a minute ago.";

			// Token: 0x04000806 RID: 2054
			public const string minutes = " since <ꪻ뮪> minutes ago.";

			// Token: 0x04000807 RID: 2055
			public const string aSecond = " since a second ago.";

			// Token: 0x04000808 RID: 2056
			public const string seconds = " since <ꪻ뮪> seconds ago.";

			// Token: 0x04000809 RID: 2057
			public const string lessThanASecond = ".";

			// Token: 0x0400080A RID: 2058
			public static readonly TimeStringFormatter.Qualifier Qualifier = new TimeStringFormatter.Qualifier(" since a day ago.", " since <ꪻ뮪> days ago.", " since an hour ago.", " since <ꪻ뮪> hours ago.", " since a minute ago.", " since <ꪻ뮪> minutes ago.", " since a second ago.", " since <ꪻ뮪> seconds ago.", ".");
		}
	}

	// Token: 0x020001D3 RID: 467
	public struct Qualifier
	{
		// Token: 0x06000D08 RID: 3336 RVA: 0x00033160 File Offset: 0x00031360
		public Qualifier(string aDay, string days, string aHour, string hours, string aMinute, string minutes, string aSecond, string seconds, string lessThanASecond)
		{
			this.aDay = aDay;
			this.days = days;
			this.aHour = aHour;
			this.hours = hours;
			this.aMinute = aMinute;
			this.minutes = minutes;
			this.aSecond = aSecond;
			this.seconds = seconds;
			this.lessThanASecond = lessThanASecond;
		}

		// Token: 0x0400080B RID: 2059
		public readonly string aDay;

		// Token: 0x0400080C RID: 2060
		public readonly string days;

		// Token: 0x0400080D RID: 2061
		public readonly string aHour;

		// Token: 0x0400080E RID: 2062
		public readonly string hours;

		// Token: 0x0400080F RID: 2063
		public readonly string aMinute;

		// Token: 0x04000810 RID: 2064
		public readonly string minutes;

		// Token: 0x04000811 RID: 2065
		public readonly string aSecond;

		// Token: 0x04000812 RID: 2066
		public readonly string seconds;

		// Token: 0x04000813 RID: 2067
		public readonly string lessThanASecond;
	}

	// Token: 0x020001D4 RID: 468
	public enum Rounding
	{
		// Token: 0x04000815 RID: 2069
		Floor,
		// Token: 0x04000816 RID: 2070
		Ceiling,
		// Token: 0x04000817 RID: 2071
		Round,
		// Token: 0x04000818 RID: 2072
		Decimal,
		// Token: 0x04000819 RID: 2073
		RoundedDecimal,
		// Token: 0x0400081A RID: 2074
		FancyDecimal,
		// Token: 0x0400081B RID: 2075
		RoundedFancyDecimal
	}
}
