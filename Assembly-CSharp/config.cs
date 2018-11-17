using System;
using System.IO;

// Token: 0x02000187 RID: 391
public class config : ConsoleSystem
{
	// Token: 0x06000BCF RID: 3023 RVA: 0x0002E99C File Offset: 0x0002CB9C
	public static string ConfigName()
	{
		return "cfg/client.cfg";
	}

	// Token: 0x06000BD0 RID: 3024 RVA: 0x0002E9A4 File Offset: 0x0002CBA4
	[ConsoleSystem.Help("Save the current config to config.cfg", "")]
	[ConsoleSystem.Admin]
	[ConsoleSystem.User]
	[ConsoleSystem.Client]
	public static void save(ref ConsoleSystem.Arg arg)
	{
		if (!Directory.Exists("cfg"))
		{
			Directory.CreateDirectory("cfg");
		}
		string path = config.ConfigName();
		string contents = ConsoleSystem.SaveToConfigString();
		File.WriteAllText(path, contents);
		arg.ReplyWith("Saved config.cfg");
	}

	// Token: 0x06000BD1 RID: 3025 RVA: 0x0002E9EC File Offset: 0x0002CBEC
	[ConsoleSystem.User]
	[ConsoleSystem.Client]
	[ConsoleSystem.Help("Load the current config from config.cfg", "")]
	[ConsoleSystem.Admin]
	public static void load(ref ConsoleSystem.Arg arg)
	{
		string text = config.ConfigName();
		string strFile = "\r\necho default config\r\ninput.bind Left A None\r\ninput.bind Right D None\r\ninput.bind Up W None\r\ninput.bind Down S None\r\ninput.bind Jump Space None\r\ninput.bind Duck LeftControl None\r\ninput.bind Sprint LeftShift None\r\ninput.bind Fire Mouse0 None\r\ninput.bind AltFire Mouse1 None\r\ninput.bind Reload R None\r\ninput.bind Use E None\r\ninput.bind Inventory Tab None\r\ninput.bind Flashlight F None\r\ninput.bind Laser G None\r\ninput.bind Voice V None\r\ninput.bind Chat Return T\r\nrender.update\r\n";
		if (File.Exists(text))
		{
			strFile = File.ReadAllText(text);
		}
		ConsoleSystem.RunFile(strFile);
		arg.ReplyWith("Loaded " + text);
	}

	// Token: 0x0400073A RID: 1850
	public const string defaultConfig = "\r\necho default config\r\ninput.bind Left A None\r\ninput.bind Right D None\r\ninput.bind Up W None\r\ninput.bind Down S None\r\ninput.bind Jump Space None\r\ninput.bind Duck LeftControl None\r\ninput.bind Sprint LeftShift None\r\ninput.bind Fire Mouse0 None\r\ninput.bind AltFire Mouse1 None\r\ninput.bind Reload R None\r\ninput.bind Use E None\r\ninput.bind Inventory Tab None\r\ninput.bind Flashlight F None\r\ninput.bind Laser G None\r\ninput.bind Voice V None\r\ninput.bind Chat Return T\r\nrender.update\r\n";
}
