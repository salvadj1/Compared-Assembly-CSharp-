using System;
using System.Threading;

// Token: 0x020004F1 RID: 1265
public static class StringEx
{
	// Token: 0x06002B03 RID: 11011 RVA: 0x000ABFF8 File Offset: 0x000AA1F8
	private static StringEx.L S(string s, int l, out char[] buffer)
	{
		if (s == null || l <= 0)
		{
			buffer = null;
			return default(StringEx.L);
		}
		StringEx.L result = new StringEx.L(l <= 1024);
		if (result.locked)
		{
			int sourceIndex = 0;
			char[] destination;
			buffer = (destination = StringEx.cb);
			s.CopyTo(sourceIndex, destination, 0, l);
		}
		else
		{
			buffer = s.ToCharArray();
		}
		return result;
	}

	// Token: 0x06002B04 RID: 11012 RVA: 0x000AC060 File Offset: 0x000AA260
	private static StringEx.L S(string s, int l, int minSafeSize, out char[] buffer)
	{
		if (s == null || l <= 0)
		{
			buffer = null;
			return default(StringEx.L);
		}
		StringEx.L result = new StringEx.L(minSafeSize <= 1024);
		if (result.locked)
		{
			int sourceIndex = 0;
			char[] destination;
			buffer = (destination = StringEx.cb);
			s.CopyTo(sourceIndex, destination, 0, l);
		}
		else
		{
			buffer = s.ToCharArray();
		}
		return result;
	}

	// Token: 0x06002B05 RID: 11013 RVA: 0x000AC0C8 File Offset: 0x000AA2C8
	private static string c2s(char[] c, int l)
	{
		return (l != 0) ? new string(c, 0, l) : string.Empty;
	}

	// Token: 0x06002B06 RID: 11014 RVA: 0x000AC0E4 File Offset: 0x000AA2E4
	private static string c2s(int l, char[] c)
	{
		return (l != 0) ? new string(c, 0, l) : string.Empty;
	}

	// Token: 0x06002B07 RID: 11015 RVA: 0x000AC100 File Offset: 0x000AA300
	public static string RemoveWhiteSpaces(this string s)
	{
		int num = (s != null) ? s.Length : 0;
		for (int i = 0; i < num; i++)
		{
			if (char.IsWhiteSpace(s[i]))
			{
				if (i == num - 1)
				{
					return s.Remove(num - 1);
				}
				char[] array;
				using (StringEx.L l = StringEx.S(s, num, out array))
				{
					if (!l.V)
					{
						return s;
					}
					int l2 = i;
					while (++i < num)
					{
						if (!char.IsWhiteSpace(array[i]))
						{
							array[l2++] = array[i];
						}
					}
					return StringEx.c2s(array, l2);
				}
			}
		}
		return s;
	}

	// Token: 0x06002B08 RID: 11016 RVA: 0x000AC1E0 File Offset: 0x000AA3E0
	public static string ToLowerEx(this string s)
	{
		int num = (s != null) ? s.Length : 0;
		for (int i = 0; i < num; i++)
		{
			if (char.IsUpper(s, i))
			{
				char[] array;
				using (StringEx.L l = StringEx.S(s, num, out array))
				{
					if (!l.V)
					{
						return s;
					}
					do
					{
						array[i] = char.ToLowerInvariant(array[i]);
					}
					while (++i < num);
					return StringEx.c2s(array, num);
				}
			}
		}
		return s;
	}

	// Token: 0x06002B09 RID: 11017 RVA: 0x000AC294 File Offset: 0x000AA494
	public static string ToUpperEx(this string s)
	{
		int num = (s != null) ? s.Length : 0;
		for (int i = 0; i < num; i++)
		{
			if (char.IsLower(s, i))
			{
				char[] array;
				using (StringEx.L l = StringEx.S(s, num, out array))
				{
					if (!l.V)
					{
						return s;
					}
					do
					{
						array[i] = char.ToUpperInvariant(array[i]);
					}
					while (++i < num);
					return StringEx.c2s(array, num);
				}
			}
		}
		return s;
	}

	// Token: 0x06002B0A RID: 11018 RVA: 0x000AC348 File Offset: 0x000AA548
	public static string MakeNice(this string s)
	{
		int length;
		if (s != null && (length = s.Length) > 1)
		{
			int num = -1;
			while (++num < length)
			{
				if (char.IsLetterOrDigit(s, num))
				{
					if (num == length - 1)
					{
						return s.Substring(length - 1, 1);
					}
					bool flag = char.IsDigit(s, num);
					bool flag2 = true;
					bool flag3 = true;
					int num2 = 0;
					char[] array;
					using (StringEx.L l = StringEx.S(s, length - num, (length - (num + 1)) * 2, out array))
					{
						if (!l.V)
						{
							return s;
						}
						if (!flag)
						{
							array[num2++] = char.ToUpper(s[num]);
						}
						else
						{
							array[num2++] = s[num];
						}
						while (++num < length)
						{
							if (flag != char.IsNumber(s, num))
							{
								flag = !flag;
								if (!flag3)
								{
									array[num2++] = ' ';
								}
								else
								{
									flag3 = false;
								}
								array[num2++] = ((!flag) ? char.ToUpperInvariant(s[num]) : s[num]);
								flag2 = true;
							}
							else if (flag)
							{
								array[num2++] = s[num];
							}
							else if (char.IsUpper(s, num))
							{
								if (!flag2)
								{
									if (!flag3)
									{
										array[num2++] = ' ';
									}
									else
									{
										flag3 = false;
									}
									flag2 = true;
								}
								array[num2++] = s[num];
							}
							else if (char.IsLower(s, num))
							{
								array[num2++] = s[num];
								flag2 = false;
							}
							else if (!flag3)
							{
								array[num2++] = ' ';
								flag3 = true;
							}
						}
						return StringEx.c2s(array, (!flag3) ? num2 : (num2 - 1));
					}
					continue;
				}
			}
			return string.Empty;
		}
		return s;
	}

	// Token: 0x06002B0B RID: 11019 RVA: 0x000AC564 File Offset: 0x000AA764
	[Obsolete("You gotta specify at least one char", true)]
	public static string RemoveChars(this string s)
	{
		return s;
	}

	// Token: 0x06002B0C RID: 11020 RVA: 0x000AC568 File Offset: 0x000AA768
	public static string RemoveChars(this string s, params char[] rem)
	{
		int num = rem.Length;
		if (num == 0)
		{
			return s;
		}
		int num2 = (s != null) ? s.Length : 0;
		for (int i = 0; i < num2; i++)
		{
			for (int j = 0; j < num; j++)
			{
				if (s[i] == rem[j])
				{
					if (i == num2 - 1)
					{
						return s.Remove(num2 - 1);
					}
					char[] array;
					using (StringEx.L l = StringEx.S(s, num2, out array))
					{
						if (!l.V)
						{
							return s;
						}
						int l2 = i;
						while (++i < num2)
						{
							for (j = 0; j < num; j++)
							{
								if (rem[j] == array[i])
								{
								}
							}
							array[l2++] = array[i];
						}
						return StringEx.c2s(array, l2);
					}
				}
			}
		}
		return s;
	}

	// Token: 0x06002B0D RID: 11021 RVA: 0x000AC680 File Offset: 0x000AA880
	public static string RemoveChars(this string s, char rem)
	{
		int num = (s != null) ? s.Length : 0;
		for (int i = 0; i < num; i++)
		{
			if (s[i] == rem)
			{
				if (i == num - 1)
				{
					return s.Remove(num - 1);
				}
				char[] array;
				using (StringEx.L l = StringEx.S(s, num, out array))
				{
					if (!l.V)
					{
						return s;
					}
					int l2 = i;
					while (++i < num)
					{
						if (array[i] != rem)
						{
							array[l2++] = array[i];
						}
					}
					return StringEx.c2s(array, l2);
				}
			}
		}
		return s;
	}

	// Token: 0x04001799 RID: 6041
	private const int maxLockSize = 1024;

	// Token: 0x0400179A RID: 6042
	private static uint lockCount;

	// Token: 0x0400179B RID: 6043
	private static readonly char[] cb = new char[1024];

	// Token: 0x0400179C RID: 6044
	private static readonly object cbLock = new object();

	// Token: 0x020004F2 RID: 1266
	private struct L : IDisposable
	{
		// Token: 0x06002B0E RID: 11022 RVA: 0x000AC760 File Offset: 0x000AA960
		public L(bool locked)
		{
			this._locked = (locked && Monitor.TryEnter(StringEx.cbLock));
			this._valid = true;
		}

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x06002B0F RID: 11023 RVA: 0x000AC790 File Offset: 0x000AA990
		public bool locked
		{
			get
			{
				return this._locked;
			}
		}

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x06002B10 RID: 11024 RVA: 0x000AC798 File Offset: 0x000AA998
		public bool V
		{
			get
			{
				return this._valid;
			}
		}

		// Token: 0x06002B11 RID: 11025 RVA: 0x000AC7A0 File Offset: 0x000AA9A0
		public void Dispose()
		{
			if (this._locked)
			{
				Monitor.Exit(StringEx.cbLock);
				this._locked = false;
			}
		}

		// Token: 0x0400179D RID: 6045
		private bool _locked;

		// Token: 0x0400179E RID: 6046
		private bool _valid;
	}
}
