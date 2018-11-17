using System;

// Token: 0x020001FB RID: 507
public struct TimeStringFormatter
{
	// Token: 0x06000E3A RID: 3642 RVA: 0x000368E0 File Offset: 0x00034AE0
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

	// Token: 0x06000E3B RID: 3643 RVA: 0x00036934 File Offset: 0x00034B34
	private static string DoMerge(string value)
	{
		return value.Replace("{", "{{").Replace("}", "}}").Replace("<ꪻ뮪>", "{0}");
	}

	// Token: 0x06000E3C RID: 3644 RVA: 0x00036970 File Offset: 0x00034B70
	private static string Merge(string prefix)
	{
		return global::TimeStringFormatter.DoMerge(prefix ?? string.Empty);
	}

	// Token: 0x06000E3D RID: 3645 RVA: 0x00036984 File Offset: 0x00034B84
	private static string Merge(string prefix, string qualifier)
	{
		return global::TimeStringFormatter.DoMerge((prefix ?? string.Empty) + (qualifier ?? string.Empty));
	}

	// Token: 0x06000E3E RID: 3646 RVA: 0x000369B8 File Offset: 0x00034BB8
	private static string Merge(string prefix, string qualifier, string suffix)
	{
		return global::TimeStringFormatter.DoMerge((prefix ?? string.Empty) + (qualifier ?? string.Empty) + (suffix ?? string.Empty));
	}

	// Token: 0x06000E3F RID: 3647 RVA: 0x000369EC File Offset: 0x00034BEC
	public static global::TimeStringFormatter Define(global::TimeStringFormatter.Qualifier qualifier)
	{
		return new global::TimeStringFormatter(global::TimeStringFormatter.Merge(qualifier.aDay), global::TimeStringFormatter.Merge(qualifier.days), global::TimeStringFormatter.Merge(qualifier.aHour), global::TimeStringFormatter.Merge(qualifier.hours), global::TimeStringFormatter.Merge(qualifier.aMinute), global::TimeStringFormatter.Merge(qualifier.minutes), global::TimeStringFormatter.Merge(qualifier.aSecond), global::TimeStringFormatter.Merge(qualifier.seconds), global::TimeStringFormatter.Merge(qualifier.lessThanASecond));
	}

	// Token: 0x06000E40 RID: 3648 RVA: 0x00036A6C File Offset: 0x00034C6C
	public static global::TimeStringFormatter Define(string prefix, global::TimeStringFormatter.Qualifier qualifier)
	{
		if (string.IsNullOrEmpty(prefix))
		{
			return global::TimeStringFormatter.Define(qualifier);
		}
		return new global::TimeStringFormatter(global::TimeStringFormatter.Merge(prefix, qualifier.aDay), global::TimeStringFormatter.Merge(prefix, qualifier.days), global::TimeStringFormatter.Merge(prefix, qualifier.aHour), global::TimeStringFormatter.Merge(prefix, qualifier.hours), global::TimeStringFormatter.Merge(prefix, qualifier.aMinute), global::TimeStringFormatter.Merge(prefix, qualifier.minutes), global::TimeStringFormatter.Merge(prefix, qualifier.aSecond), global::TimeStringFormatter.Merge(prefix, qualifier.seconds), global::TimeStringFormatter.Merge(prefix, qualifier.lessThanASecond));
	}

	// Token: 0x06000E41 RID: 3649 RVA: 0x00036B08 File Offset: 0x00034D08
	public static global::TimeStringFormatter Define(global::TimeStringFormatter.Qualifier qualifier, string suffix)
	{
		if (string.IsNullOrEmpty(suffix))
		{
			return global::TimeStringFormatter.Define(qualifier);
		}
		return new global::TimeStringFormatter(global::TimeStringFormatter.Merge(qualifier.aDay, suffix), global::TimeStringFormatter.Merge(qualifier.days, suffix), global::TimeStringFormatter.Merge(qualifier.aHour, suffix), global::TimeStringFormatter.Merge(qualifier.hours, suffix), global::TimeStringFormatter.Merge(qualifier.aMinute, suffix), global::TimeStringFormatter.Merge(qualifier.minutes, suffix), global::TimeStringFormatter.Merge(qualifier.aSecond, suffix), global::TimeStringFormatter.Merge(qualifier.seconds, suffix), global::TimeStringFormatter.Merge(qualifier.lessThanASecond, suffix));
	}

	// Token: 0x06000E42 RID: 3650 RVA: 0x00036BA4 File Offset: 0x00034DA4
	public static global::TimeStringFormatter Define(string prefix, global::TimeStringFormatter.Qualifier qualifier, string suffix)
	{
		if (string.IsNullOrEmpty(suffix))
		{
			if (string.IsNullOrEmpty(prefix))
			{
				return global::TimeStringFormatter.Define(qualifier);
			}
			return global::TimeStringFormatter.Define(prefix, qualifier);
		}
		else
		{
			if (string.IsNullOrEmpty(prefix))
			{
				return global::TimeStringFormatter.Define(qualifier, suffix);
			}
			return new global::TimeStringFormatter(global::TimeStringFormatter.Merge(prefix, qualifier.aDay, suffix), global::TimeStringFormatter.Merge(prefix, qualifier.days, suffix), global::TimeStringFormatter.Merge(prefix, qualifier.aHour, suffix), global::TimeStringFormatter.Merge(prefix, qualifier.hours, suffix), global::TimeStringFormatter.Merge(prefix, qualifier.aMinute, suffix), global::TimeStringFormatter.Merge(prefix, qualifier.minutes, suffix), global::TimeStringFormatter.Merge(prefix, qualifier.aSecond, suffix), global::TimeStringFormatter.Merge(prefix, qualifier.seconds, suffix), global::TimeStringFormatter.Merge(prefix, qualifier.lessThanASecond, suffix));
		}
	}

	// Token: 0x06000E43 RID: 3651 RVA: 0x00036C6C File Offset: 0x00034E6C
	public static global::TimeStringFormatter Define(global::TimeStringFormatter formatter, string lessThanASecond)
	{
		if (!object.ReferenceEquals(lessThanASecond, null))
		{
			formatter = new global::TimeStringFormatter(formatter.aDay, formatter.days, formatter.aHour, formatter.hours, formatter.aMinute, formatter.minutes, formatter.aSecond, formatter.seconds, global::TimeStringFormatter.Merge(lessThanASecond));
		}
		return formatter;
	}

	// Token: 0x06000E44 RID: 3652 RVA: 0x00036CCC File Offset: 0x00034ECC
	public static global::TimeStringFormatter Define(string prefix, global::TimeStringFormatter.Qualifier qualifier, string suffix, string lessThanASecond)
	{
		return global::TimeStringFormatter.Define(global::TimeStringFormatter.Define(prefix, qualifier, suffix), lessThanASecond);
	}

	// Token: 0x06000E45 RID: 3653 RVA: 0x00036CDC File Offset: 0x00034EDC
	private static double Round(double total, global::TimeStringFormatter.Rounding rounding, int decimalPlaces, double fancyUnits)
	{
		if (total <= 0.0)
		{
			return 0.0;
		}
		switch (rounding)
		{
		case global::TimeStringFormatter.Rounding.Floor:
			return Math.Floor(total);
		case global::TimeStringFormatter.Rounding.Ceiling:
			return Math.Ceiling(total);
		case global::TimeStringFormatter.Rounding.Round:
			return Math.Round(total);
		case global::TimeStringFormatter.Rounding.Decimal:
			fancyUnits = 1.0;
			decimalPlaces = 0;
			break;
		case global::TimeStringFormatter.Rounding.RoundedDecimal:
			fancyUnits = 1.0;
			break;
		case global::TimeStringFormatter.Rounding.FancyDecimal:
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

	// Token: 0x06000E46 RID: 3654 RVA: 0x00036DA8 File Offset: 0x00034FA8
	public string GetFormattingString(TimeSpan timePassed)
	{
		return this.GetFormattingString(timePassed, global::TimeStringFormatter.Rounding.Floor);
	}

	// Token: 0x06000E47 RID: 3655 RVA: 0x00036DB4 File Offset: 0x00034FB4
	public string GetFormattingString(TimeSpan timePassed, global::TimeStringFormatter.Rounding rounding)
	{
		int num2;
		double num = global::TimeStringFormatter.Round(timePassed.TotalSeconds, rounding, num2 = 2, 1.0);
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
		else if ((num = global::TimeStringFormatter.Round(timePassed.TotalMinutes, rounding, num2 = 2, 0.6)) == 1.0)
		{
			format = this.aMinute;
		}
		else if (num < 60.0)
		{
			format = this.minutes;
		}
		else if ((num = global::TimeStringFormatter.Round(timePassed.TotalHours, rounding, num2 = 2, 1.0)) == 1.0)
		{
			format = this.aHour;
		}
		else if (num < 24.0)
		{
			format = this.hours;
		}
		else if ((num = global::TimeStringFormatter.Round(timePassed.TotalDays, rounding, num2 = 2, 0.24)) == 1.0)
		{
			format = this.aDay;
		}
		else
		{
			format = this.days;
		}
		object arg;
		if (rounding == global::TimeStringFormatter.Rounding.RoundedDecimal || rounding == global::TimeStringFormatter.Rounding.FancyDecimal || rounding == global::TimeStringFormatter.Rounding.RoundedFancyDecimal)
		{
			string text;
			if (rounding == global::TimeStringFormatter.Rounding.RoundedDecimal || rounding == global::TimeStringFormatter.Rounding.RoundedFancyDecimal)
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
			if (rounding == global::TimeStringFormatter.Rounding.FancyDecimal || rounding == global::TimeStringFormatter.Rounding.RoundedFancyDecimal)
			{
				arg = text.Replace('.', ':');
			}
			else
			{
				arg = text;
			}
		}
		else if (rounding != global::TimeStringFormatter.Rounding.Decimal && !double.IsNaN(num) && !double.IsInfinity(num))
		{
			arg = (int)num;
		}
		else
		{
			arg = num;
		}
		return string.Format(format, arg);
	}

	// Token: 0x040008BF RID: 2239
	public const string kArgumentTime = "<ꪻ뮪>";

	// Token: 0x040008C0 RID: 2240
	private const string kArgumentTimeReplacement = "{0}";

	// Token: 0x040008C1 RID: 2241
	public const string kPeriod = ".";

	// Token: 0x040008C2 RID: 2242
	public readonly string aDay;

	// Token: 0x040008C3 RID: 2243
	public readonly string days;

	// Token: 0x040008C4 RID: 2244
	public readonly string aHour;

	// Token: 0x040008C5 RID: 2245
	public readonly string hours;

	// Token: 0x040008C6 RID: 2246
	public readonly string aMinute;

	// Token: 0x040008C7 RID: 2247
	public readonly string minutes;

	// Token: 0x040008C8 RID: 2248
	public readonly string aSecond;

	// Token: 0x040008C9 RID: 2249
	public readonly string seconds;

	// Token: 0x040008CA RID: 2250
	public readonly string lessThanASecond;

	// Token: 0x020001FC RID: 508
	public static class Quantity
	{
		// Token: 0x040008CB RID: 2251
		public const string kPrefix = " ";

		// Token: 0x040008CC RID: 2252
		public const string aDay = " a day";

		// Token: 0x040008CD RID: 2253
		public const string days = " <ꪻ뮪> days";

		// Token: 0x040008CE RID: 2254
		public const string aHour = " an hour";

		// Token: 0x040008CF RID: 2255
		public const string hours = " <ꪻ뮪> hours";

		// Token: 0x040008D0 RID: 2256
		public const string aMinute = " a minute";

		// Token: 0x040008D1 RID: 2257
		public const string minutes = " <ꪻ뮪> minutes";

		// Token: 0x040008D2 RID: 2258
		public const string aSecond = " a second";

		// Token: 0x040008D3 RID: 2259
		public const string seconds = " <ꪻ뮪> seconds";

		// Token: 0x040008D4 RID: 2260
		public const string lessThanASecond = "";

		// Token: 0x040008D5 RID: 2261
		public static readonly global::TimeStringFormatter.Qualifier Qualifier = new global::TimeStringFormatter.Qualifier(" a day", " <ꪻ뮪> days", " an hour", " <ꪻ뮪> hours", " a minute", " <ꪻ뮪> minutes", " a second", " <ꪻ뮪> seconds", string.Empty);

		// Token: 0x020001FD RID: 509
		public static class Period
		{
			// Token: 0x040008D6 RID: 2262
			public const string kSuffix = ".";

			// Token: 0x040008D7 RID: 2263
			public const string aDay = " a day.";

			// Token: 0x040008D8 RID: 2264
			public const string days = " <ꪻ뮪> days.";

			// Token: 0x040008D9 RID: 2265
			public const string aHour = " an hour.";

			// Token: 0x040008DA RID: 2266
			public const string hours = " <ꪻ뮪> hours.";

			// Token: 0x040008DB RID: 2267
			public const string aMinute = " a minute.";

			// Token: 0x040008DC RID: 2268
			public const string minutes = " <ꪻ뮪> minutes.";

			// Token: 0x040008DD RID: 2269
			public const string aSecond = " a second.";

			// Token: 0x040008DE RID: 2270
			public const string seconds = " <ꪻ뮪> seconds.";

			// Token: 0x040008DF RID: 2271
			public const string lessThanASecond = ".";

			// Token: 0x040008E0 RID: 2272
			public static readonly global::TimeStringFormatter.Qualifier Qualifier = new global::TimeStringFormatter.Qualifier(" a day.", " <ꪻ뮪> days.", " an hour.", " <ꪻ뮪> hours.", " a minute.", " <ꪻ뮪> minutes.", " a second.", " <ꪻ뮪> seconds.", ".");
		}
	}

	// Token: 0x020001FE RID: 510
	public static class For
	{
		// Token: 0x040008E1 RID: 2273
		public const string kPrefix = " for";

		// Token: 0x040008E2 RID: 2274
		public const string aDay = " for a day";

		// Token: 0x040008E3 RID: 2275
		public const string days = " for <ꪻ뮪> days";

		// Token: 0x040008E4 RID: 2276
		public const string aHour = " for an hour";

		// Token: 0x040008E5 RID: 2277
		public const string hours = " for <ꪻ뮪> hours";

		// Token: 0x040008E6 RID: 2278
		public const string aMinute = " for a minute";

		// Token: 0x040008E7 RID: 2279
		public const string minutes = " for <ꪻ뮪> minutes";

		// Token: 0x040008E8 RID: 2280
		public const string aSecond = " for a second";

		// Token: 0x040008E9 RID: 2281
		public const string seconds = " for <ꪻ뮪> seconds";

		// Token: 0x040008EA RID: 2282
		public const string lessThanASecond = "";

		// Token: 0x040008EB RID: 2283
		public static readonly global::TimeStringFormatter.Qualifier Qualifier = new global::TimeStringFormatter.Qualifier(" for a day", " for <ꪻ뮪> days", " for an hour", " for <ꪻ뮪> hours", " for a minute", " for <ꪻ뮪> minutes", " for a second", " for <ꪻ뮪> seconds", string.Empty);

		// Token: 0x020001FF RID: 511
		public static class Period
		{
			// Token: 0x040008EC RID: 2284
			public const string kSuffix = ".";

			// Token: 0x040008ED RID: 2285
			public const string aDay = " for a day.";

			// Token: 0x040008EE RID: 2286
			public const string days = " for <ꪻ뮪> days.";

			// Token: 0x040008EF RID: 2287
			public const string aHour = " for an hour.";

			// Token: 0x040008F0 RID: 2288
			public const string hours = " for <ꪻ뮪> hours.";

			// Token: 0x040008F1 RID: 2289
			public const string aMinute = " for a minute.";

			// Token: 0x040008F2 RID: 2290
			public const string minutes = " for <ꪻ뮪> minutes.";

			// Token: 0x040008F3 RID: 2291
			public const string aSecond = " for a second.";

			// Token: 0x040008F4 RID: 2292
			public const string seconds = " for <ꪻ뮪> seconds.";

			// Token: 0x040008F5 RID: 2293
			public const string lessThanASecond = ".";

			// Token: 0x040008F6 RID: 2294
			public static readonly global::TimeStringFormatter.Qualifier Qualifier = new global::TimeStringFormatter.Qualifier(" for a day.", " for <ꪻ뮪> days.", " for an hour.", " for <ꪻ뮪> hours.", " for a minute.", " for <ꪻ뮪> minutes.", " for a second.", " for <ꪻ뮪> seconds.", ".");
		}
	}

	// Token: 0x02000200 RID: 512
	public static class Ago
	{
		// Token: 0x040008F7 RID: 2295
		public const string kSuffix = " ago";

		// Token: 0x040008F8 RID: 2296
		public const string aDay = " a day ago";

		// Token: 0x040008F9 RID: 2297
		public const string days = " <ꪻ뮪> days ago";

		// Token: 0x040008FA RID: 2298
		public const string aHour = " an hour ago";

		// Token: 0x040008FB RID: 2299
		public const string hours = " <ꪻ뮪> hours ago";

		// Token: 0x040008FC RID: 2300
		public const string aMinute = " a minute ago";

		// Token: 0x040008FD RID: 2301
		public const string minutes = " <ꪻ뮪> minutes ago";

		// Token: 0x040008FE RID: 2302
		public const string aSecond = " a second ago";

		// Token: 0x040008FF RID: 2303
		public const string seconds = " <ꪻ뮪> seconds ago";

		// Token: 0x04000900 RID: 2304
		public const string lessThanASecond = "";

		// Token: 0x04000901 RID: 2305
		public static readonly global::TimeStringFormatter.Qualifier Qualifier = new global::TimeStringFormatter.Qualifier(" a day ago", " <ꪻ뮪> days ago", " an hour ago", " <ꪻ뮪> hours ago", " a minute ago", " <ꪻ뮪> minutes ago", " a second ago", " <ꪻ뮪> seconds ago", string.Empty);

		// Token: 0x02000201 RID: 513
		public static class Period
		{
			// Token: 0x04000902 RID: 2306
			public const string kSuffix = ".";

			// Token: 0x04000903 RID: 2307
			public const string aDay = " a day ago.";

			// Token: 0x04000904 RID: 2308
			public const string days = " <ꪻ뮪> days ago.";

			// Token: 0x04000905 RID: 2309
			public const string aHour = " an hour ago.";

			// Token: 0x04000906 RID: 2310
			public const string hours = " <ꪻ뮪> hours ago.";

			// Token: 0x04000907 RID: 2311
			public const string aMinute = " a minute ago.";

			// Token: 0x04000908 RID: 2312
			public const string minutes = " <ꪻ뮪> minutes ago.";

			// Token: 0x04000909 RID: 2313
			public const string aSecond = " a second ago.";

			// Token: 0x0400090A RID: 2314
			public const string seconds = " <ꪻ뮪> seconds ago.";

			// Token: 0x0400090B RID: 2315
			public const string lessThanASecond = ".";

			// Token: 0x0400090C RID: 2316
			public static readonly global::TimeStringFormatter.Qualifier Qualifier = new global::TimeStringFormatter.Qualifier(" a day ago.", " <ꪻ뮪> days ago.", " an hour ago.", " <ꪻ뮪> hours ago.", " a minute ago.", " <ꪻ뮪> minutes ago.", " a second ago.", " <ꪻ뮪> seconds ago.", ".");
		}
	}

	// Token: 0x02000202 RID: 514
	public static class SinceAgo
	{
		// Token: 0x0400090D RID: 2317
		public const string kPrefix = " since";

		// Token: 0x0400090E RID: 2318
		public const string aDay = " since a day ago";

		// Token: 0x0400090F RID: 2319
		public const string days = " since <ꪻ뮪> days ago";

		// Token: 0x04000910 RID: 2320
		public const string aHour = " since an hour ago";

		// Token: 0x04000911 RID: 2321
		public const string hours = " since <ꪻ뮪> hours ago";

		// Token: 0x04000912 RID: 2322
		public const string aMinute = " since a minute ago";

		// Token: 0x04000913 RID: 2323
		public const string minutes = " since <ꪻ뮪> minutes ago";

		// Token: 0x04000914 RID: 2324
		public const string aSecond = " since a second ago";

		// Token: 0x04000915 RID: 2325
		public const string seconds = " since <ꪻ뮪> seconds ago";

		// Token: 0x04000916 RID: 2326
		public const string lessThanASecond = "";

		// Token: 0x04000917 RID: 2327
		public static readonly global::TimeStringFormatter.Qualifier Qualifier = new global::TimeStringFormatter.Qualifier(" since a day ago", " since <ꪻ뮪> days ago", " since an hour ago", " since <ꪻ뮪> hours ago", " since a minute ago", " since <ꪻ뮪> minutes ago", " since a second ago", " since <ꪻ뮪> seconds ago", string.Empty);

		// Token: 0x02000203 RID: 515
		public static class Period
		{
			// Token: 0x04000918 RID: 2328
			public const string kSuffix = ".";

			// Token: 0x04000919 RID: 2329
			public const string aDay = " since a day ago.";

			// Token: 0x0400091A RID: 2330
			public const string days = " since <ꪻ뮪> days ago.";

			// Token: 0x0400091B RID: 2331
			public const string aHour = " since an hour ago.";

			// Token: 0x0400091C RID: 2332
			public const string hours = " since <ꪻ뮪> hours ago.";

			// Token: 0x0400091D RID: 2333
			public const string aMinute = " since a minute ago.";

			// Token: 0x0400091E RID: 2334
			public const string minutes = " since <ꪻ뮪> minutes ago.";

			// Token: 0x0400091F RID: 2335
			public const string aSecond = " since a second ago.";

			// Token: 0x04000920 RID: 2336
			public const string seconds = " since <ꪻ뮪> seconds ago.";

			// Token: 0x04000921 RID: 2337
			public const string lessThanASecond = ".";

			// Token: 0x04000922 RID: 2338
			public static readonly global::TimeStringFormatter.Qualifier Qualifier = new global::TimeStringFormatter.Qualifier(" since a day ago.", " since <ꪻ뮪> days ago.", " since an hour ago.", " since <ꪻ뮪> hours ago.", " since a minute ago.", " since <ꪻ뮪> minutes ago.", " since a second ago.", " since <ꪻ뮪> seconds ago.", ".");
		}
	}

	// Token: 0x02000204 RID: 516
	public struct Qualifier
	{
		// Token: 0x06000E50 RID: 3664 RVA: 0x000371E8 File Offset: 0x000353E8
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

		// Token: 0x04000923 RID: 2339
		public readonly string aDay;

		// Token: 0x04000924 RID: 2340
		public readonly string days;

		// Token: 0x04000925 RID: 2341
		public readonly string aHour;

		// Token: 0x04000926 RID: 2342
		public readonly string hours;

		// Token: 0x04000927 RID: 2343
		public readonly string aMinute;

		// Token: 0x04000928 RID: 2344
		public readonly string minutes;

		// Token: 0x04000929 RID: 2345
		public readonly string aSecond;

		// Token: 0x0400092A RID: 2346
		public readonly string seconds;

		// Token: 0x0400092B RID: 2347
		public readonly string lessThanASecond;
	}

	// Token: 0x02000205 RID: 517
	public enum Rounding
	{
		// Token: 0x0400092D RID: 2349
		Floor,
		// Token: 0x0400092E RID: 2350
		Ceiling,
		// Token: 0x0400092F RID: 2351
		Round,
		// Token: 0x04000930 RID: 2352
		Decimal,
		// Token: 0x04000931 RID: 2353
		RoundedDecimal,
		// Token: 0x04000932 RID: 2354
		FancyDecimal,
		// Token: 0x04000933 RID: 2355
		RoundedFancyDecimal
	}
}
