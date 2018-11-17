using System;
using System.IO;

// Token: 0x020001B3 RID: 435
public class config : global::ConsoleSystem
{
	// Token: 0x06000CFF RID: 3327 RVA: 0x00032888 File Offset: 0x00030A88
	public static string ConfigName()
	{
		return "cfg/client.cfg";
	}

	// Token: 0x06000D00 RID: 3328 RVA: 0x00032890 File Offset: 0x00030A90
	[global::ConsoleSystem.Help("Save the current config to config.cfg", "")]
	[global::ConsoleSystem.Client]
	[global::ConsoleSystem.User]
	[global::ConsoleSystem.Admin]
	public static void save(ref global::ConsoleSystem.Arg arg)
	{
		if (!Directory.Exists("cfg"))
		{
			Directory.CreateDirectory("cfg");
		}
		string path = global::config.ConfigName();
		string contents = global::ConsoleSystem.SaveToConfigString();
		File.WriteAllText(path, contents);
		arg.ReplyWith("Saved config.cfg");
	}

	// Token: 0x06000D01 RID: 3329 RVA: 0x000328D8 File Offset: 0x00030AD8
	[global::ConsoleSystem.User]
	[global::ConsoleSystem.Admin]
	[global::ConsoleSystem.Client]
	[global::ConsoleSystem.Help("Load the current config from config.cfg", "")]
	public static void load(ref global::ConsoleSystem.Arg arg)
	{
		string text = global::config.ConfigName();
		string strFile = "\r\necho default config\r\ninput.bind Left A None\r\ninput.bind Right D None\r\ninput.bind Up W None\r\ninput.bind Down S None\r\ninput.bind Jump Space None\r\ninput.bind Duck LeftControl None\r\ninput.bind Sprint LeftShift None\r\ninput.bind Fire Mouse0 None\r\ninput.bind AltFire Mouse1 None\r\ninput.bind Reload R None\r\ninput.bind Use E None\r\ninput.bind Inventory Tab None\r\ninput.bind Flashlight F None\r\ninput.bind Laser G None\r\ninput.bind Voice V None\r\ninput.bind Chat Return T\r\nrender.update\r\n";
		if (File.Exists(text))
		{
			strFile = File.ReadAllText(text);
		}
		global::ConsoleSystem.RunFile(strFile);
		arg.ReplyWith("Loaded " + text);
	}

	// Token: 0x0400084E RID: 2126
	public const string defaultConfig = "\r\necho default config\r\ninput.bind Left A None\r\ninput.bind Right D None\r\ninput.bind Up W None\r\ninput.bind Down S None\r\ninput.bind Jump Space None\r\ninput.bind Duck LeftControl None\r\ninput.bind Sprint LeftShift None\r\ninput.bind Fire Mouse0 None\r\ninput.bind AltFire Mouse1 None\r\ninput.bind Reload R None\r\ninput.bind Use E None\r\ninput.bind Inventory Tab None\r\ninput.bind Flashlight F None\r\ninput.bind Laser G None\r\ninput.bind Voice V None\r\ninput.bind Chat Return T\r\nrender.update\r\n";
}
