using System;

// Token: 0x020003EE RID: 1006
public class chat : ConsoleSystem
{
	// Token: 0x0600252C RID: 9516 RVA: 0x0008EECC File Offset: 0x0008D0CC
	[ConsoleSystem.Client]
	public static void add(ref ConsoleSystem.Arg arg)
	{
		if (!chat.enabled)
		{
			return;
		}
		string @string = arg.GetString(0, string.Empty);
		string string2 = arg.GetString(1, string.Empty);
		if (@string == string.Empty || string2 == string.Empty)
		{
			return;
		}
		ChatUI.AddLine(@string, string2);
	}

	// Token: 0x0400120C RID: 4620
	[ConsoleSystem.Client]
	[ConsoleSystem.Help("Enable or disable chat displaying", "")]
	[ConsoleSystem.Admin]
	public static bool enabled = true;
}
