using System;

// Token: 0x020007C9 RID: 1993
public static class dfStringExtensions
{
	// Token: 0x0600448F RID: 17551 RVA: 0x00100310 File Offset: 0x000FE510
	public static string MakeRelativePath(this string path)
	{
		if (string.IsNullOrEmpty(path))
		{
			return string.Empty;
		}
		return path.Substring(path.IndexOf("Assets/", StringComparison.InvariantCultureIgnoreCase));
	}

	// Token: 0x06004490 RID: 17552 RVA: 0x00100338 File Offset: 0x000FE538
	public static bool Contains(this string value, string pattern, bool caseInsensitive)
	{
		if (caseInsensitive)
		{
			return value.IndexOf(pattern, StringComparison.InvariantCultureIgnoreCase) != -1;
		}
		return value.IndexOf(pattern) != -1;
	}
}
