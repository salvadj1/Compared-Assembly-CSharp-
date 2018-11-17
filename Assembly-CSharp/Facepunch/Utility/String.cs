using System;
using System.Text.RegularExpressions;

namespace Facepunch.Utility
{
	// Token: 0x02000193 RID: 403
	public static class String
	{
		// Token: 0x06000BF6 RID: 3062 RVA: 0x0002FF10 File Offset: 0x0002E110
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

		// Token: 0x06000BF7 RID: 3063 RVA: 0x0002FFAC File Offset: 0x0002E1AC
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
