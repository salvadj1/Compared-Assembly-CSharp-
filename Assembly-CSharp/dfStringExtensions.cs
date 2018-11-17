using System;

// Token: 0x020006F7 RID: 1783
public static class dfStringExtensions
{
	// Token: 0x06004073 RID: 16499 RVA: 0x000F770C File Offset: 0x000F590C
	public static string MakeRelativePath(this string path)
	{
		if (string.IsNullOrEmpty(path))
		{
			return string.Empty;
		}
		return path.Substring(path.IndexOf("Assets/", StringComparison.InvariantCultureIgnoreCase));
	}

	// Token: 0x06004074 RID: 16500 RVA: 0x000F7734 File Offset: 0x000F5934
	public static bool Contains(this string value, string pattern, bool caseInsensitive)
	{
		if (caseInsensitive)
		{
			return value.IndexOf(pattern, StringComparison.InvariantCultureIgnoreCase) != -1;
		}
		return value.IndexOf(pattern) != -1;
	}
}
