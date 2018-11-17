using System;
using System.Text.RegularExpressions;

namespace Facepunch.Utility
{
	// Token: 0x020001C1 RID: 449
	public static class String
	{
		// Token: 0x06000D2E RID: 3374 RVA: 0x00033DFC File Offset: 0x00031FFC
		public static string[] SplitQuotesStrings(string input)
		{
			input = input.Replace("\\\"", "&qute;");
			Regex regex = new Regex("\"([^\"]+)\"|'([^']+)'|\\S+", RegexOptions.Compiled);
			MatchCollection matchCollection = regex.Matches(input);
			string[] array = new string[matchCollection.Count];
			for (int i = 0; i < matchCollection.Count; i++)
			{
				array[i] = matchCollection[i].Groups[0].Value.Trim(new char[]
				{
					' ',
					'"'
				});
				array[i] = array[i].Replace("&qute;", "\"");
			}
			return array;
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x00033E98 File Offset: 0x00032098
		public static string QuoteSafe(string str)
		{
			str = str.Replace("\"", "\\\"");
			str = str.TrimEnd(new char[]
			{
				'\\'
			});
			return "\"" + str + "\"";
		}
	}
}
