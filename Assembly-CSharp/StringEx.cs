using System;
using System.Threading;

// Token: 0x020005AE RID: 1454
public static class StringEx
{
	// Token: 0x06002EC3 RID: 11971 RVA: 0x000B4094 File Offset: 0x000B2294
	private static global::StringEx.L S(string s, int l, out char[] buffer)
	{
		if (s == null || l <= 0)
		{
			buffer = null;
			return default(global::StringEx.L);
		}
		global::StringEx.L result = new global::StringEx.L(l <= 1024);
		if (result.locked)
		{
			int sourceIndex = 0;
			char[] destination;
			buffer = (destination = global::StringEx.cb);
			s.CopyTo(sourceIndex, destination, 0, l);
		}
		else
		{
			buffer = s.ToCharArray();
		}
		return result;
	}

	// Token: 0x06002EC4 RID: 11972 RVA: 0x000B40FC File Offset: 0x000B22FC
	private static global::StringEx.L S(string s, int l, int minSafeSize, out char[] buffer)
	{
		if (s == null || l <= 0)
		{
			buffer = null;
			return default(global::StringEx.L);
		}
		global::StringEx.L result = new global::StringEx.L(minSafeSize <= 1024);
		if (result.locked)
		{
			int sourceIndex = 0;
			char[] destination;
			buffer = (destination = global::StringEx.cb);
			s.CopyTo(sourceIndex, destination, 0, l);
		}
		else
		{
			buffer = s.ToCharArray();
		}
		return result;
	}

	// Token: 0x06002EC5 RID: 11973 RVA: 0x000B4164 File Offset: 0x000B2364
	private static string c2s(char[] c, int l)
	{
		return (l != 0) ? new string(c, 0, l) : string.Empty;
	}

	// Token: 0x06002EC6 RID: 11974 RVA: 0x000B4180 File Offset: 0x000B2380
	private static string c2s(int l, char[] c)
	{
		return (l != 0) ? new string(c, 0, l) : string.Empty;
	}

	// Token: 0x06002EC7 RID: 11975 RVA: 0x000B419C File Offset: 0x000B239C
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
				using (global::StringEx.L l = global::StringEx.S(s, num, out array))
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
					return global::StringEx.c2s(array, l2);
				}
			}
		}
		return s;
	}

	// Token: 0x06002EC8 RID: 11976 RVA: 0x000B427C File Offset: 0x000B247C
	public static string ToLowerEx(this string s)
	{
		int num = (s != null) ? s.Length : 0;
		for (int i = 0; i < num; i++)
		{
			if (char.IsUpper(s, i))
			{
				char[] array;
				using (global::StringEx.L l = global::StringEx.S(s, num, out array))
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
					return global::StringEx.c2s(array, num);
				}
			}
		}
		return s;
	}

	// Token: 0x06002EC9 RID: 11977 RVA: 0x000B4330 File Offset: 0x000B2530
	public static string ToUpperEx(this string s)
	{
		int num = (s != null) ? s.Length : 0;
		for (int i = 0; i < num; i++)
		{
			if (char.IsLower(s, i))
			{
				char[] array;
				using (global::StringEx.L l = global::StringEx.S(s, num, out array))
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
					return global::StringEx.c2s(array, num);
				}
			}
		}
		return s;
	}

	// Token: 0x06002ECA RID: 11978 RVA: 0x000B43E4 File Offset: 0x000B25E4
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
					using (global::StringEx.L l = global::StringEx.S(s, length - num, (length - (num + 1)) * 2, out array))
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
						return global::StringEx.c2s(array, (!flag3) ? num2 : (num2 - 1));
					}
					continue;
				}
			}
			return string.Empty;
		}
		return s;
	}

	// Token: 0x06002ECB RID: 11979 RVA: 0x000B4600 File Offset: 0x000B2800
	[Obsolete("You gotta specify at least one char", true)]
	public static string RemoveChars(this string s)
	{
		return s;
	}

	// Token: 0x06002ECC RID: 11980 RVA: 0x000B4604 File Offset: 0x000B2804
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
					using (global::StringEx.L l = global::StringEx.S(s, num2, out array))
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
						return global::StringEx.c2s(array, l2);
					}
				}
			}
		}
		return s;
	}

	// Token: 0x06002ECD RID: 11981 RVA: 0x000B471C File Offset: 0x000B291C
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
				using (global::StringEx.L l = global::StringEx.S(s, num, out array))
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
					return global::StringEx.c2s(array, l2);
				}
			}
		}
		return s;
	}

	// Token: 0x04001965 RID: 6501
	private const int maxLockSize = 1024;

	// Token: 0x04001966 RID: 6502
	private static uint lockCount;

	// Token: 0x04001967 RID: 6503
	private static readonly char[] cb = new char[1024];

	// Token: 0x04001968 RID: 6504
	private static readonly object cbLock = new object();

	// Token: 0x020005AF RID: 1455
	private struct L : IDisposable
	{
		// Token: 0x06002ECE RID: 11982 RVA: 0x000B47FC File Offset: 0x000B29FC
		public L(bool locked)
		{
			this._locked = (locked && Monitor.TryEnter(global::StringEx.cbLock));
			this._valid = true;
		}

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x06002ECF RID: 11983 RVA: 0x000B482C File Offset: 0x000B2A2C
		public bool locked
		{
			get
			{
				return this._locked;
			}
		}

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x06002ED0 RID: 11984 RVA: 0x000B4834 File Offset: 0x000B2A34
		public bool V
		{
			get
			{
				return this._valid;
			}
		}

		// Token: 0x06002ED1 RID: 11985 RVA: 0x000B483C File Offset: 0x000B2A3C
		public void Dispose()
		{
			if (this._locked)
			{
				Monitor.Exit(global::StringEx.cbLock);
				this._locked = false;
			}
		}

		// Token: 0x04001969 RID: 6505
		private bool _locked;

		// Token: 0x0400196A RID: 6506
		private bool _valid;
	}
}
