using System;
using System.Collections.Generic;

namespace Facepunch.Utility
{
	// Token: 0x02000192 RID: 402
	public static class CommandLine
	{
		// Token: 0x06000BF1 RID: 3057 RVA: 0x0002FCF0 File Offset: 0x0002DEF0
		public static void Force(string val)
		{
			CommandLine.commandline = val;
			CommandLine.initialized = false;
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x0002FD00 File Offset: 0x0002DF00
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

		// Token: 0x06000BF3 RID: 3059 RVA: 0x0002FE90 File Offset: 0x0002E090
		public static bool HasSwitch(string strName)
		{
			return CommandLine.switches.ContainsKey(strName);
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x0002FEA0 File Offset: 0x0002E0A0
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

		// Token: 0x06000BF5 RID: 3061 RVA: 0x0002FED0 File Offset: 0x0002E0D0
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

		// Token: 0x04000760 RID: 1888
		private static bool initialized = false;

		// Token: 0x04000761 RID: 1889
		private static string commandline = string.Empty;

		// Token: 0x04000762 RID: 1890
		private static Dictionary<string, string> switches = new Dictionary<string, string>();
	}
}
