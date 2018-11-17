using System;
using System.Collections.Generic;

namespace Facepunch.Utility
{
	// Token: 0x020001C0 RID: 448
	public static class CommandLine
	{
		// Token: 0x06000D29 RID: 3369 RVA: 0x00033BDC File Offset: 0x00031DDC
		public static void Force(string val)
		{
			CommandLine.commandline = val;
			CommandLine.initialized = false;
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x00033BEC File Offset: 0x00031DEC
		private static void Initalize()
		{
			if (CommandLine.initialized)
			{
				return;
			}
			CommandLine.initialized = true;
			if (CommandLine.commandline == string.Empty)
			{
				string[] commandLineArgs = Environment.GetCommandLineArgs();
				foreach (string str in commandLineArgs)
				{
					CommandLine.commandline = CommandLine.commandline + "\"" + str + "\" ";
				}
			}
			if (CommandLine.commandline == string.Empty)
			{
				return;
			}
			string text = string.Empty;
			string[] array2 = String.SplitQuotesStrings(CommandLine.commandline);
			foreach (string text2 in array2)
			{
				if (text2.Length != 0)
				{
					if (text2[0] == '-' || text2[0] == '+')
					{
						if (text != string.Empty && !CommandLine.switches.ContainsKey(text))
						{
							CommandLine.switches.Add(text, string.Empty);
						}
						text = text2;
					}
					else if (text != string.Empty)
					{
						if (!CommandLine.switches.ContainsKey(text))
						{
							CommandLine.switches.Add(text, text2);
						}
						text = string.Empty;
					}
				}
			}
			if (text != string.Empty && !CommandLine.switches.ContainsKey(text))
			{
				CommandLine.switches.Add(text, string.Empty);
			}
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x00033D7C File Offset: 0x00031F7C
		public static bool HasSwitch(string strName)
		{
			return CommandLine.switches.ContainsKey(strName);
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x00033D8C File Offset: 0x00031F8C
		public static string GetSwitch(string strName, string strDefault)
		{
			CommandLine.Initalize();
			string empty = string.Empty;
			if (!CommandLine.switches.TryGetValue(strName, out empty))
			{
				return strDefault;
			}
			return empty;
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x00033DBC File Offset: 0x00031FBC
		public static int GetSwitchInt(string strName, int iDefault)
		{
			CommandLine.Initalize();
			string empty = string.Empty;
			if (!CommandLine.switches.TryGetValue(strName, out empty))
			{
				return iDefault;
			}
			int result = iDefault;
			if (!int.TryParse(empty, out result))
			{
				return iDefault;
			}
			return result;
		}

		// Token: 0x04000874 RID: 2164
		private static bool initialized = false;

		// Token: 0x04000875 RID: 2165
		private static string commandline = string.Empty;

		// Token: 0x04000876 RID: 2166
		private static Dictionary<string, string> switches = new Dictionary<string, string>();
	}
}
