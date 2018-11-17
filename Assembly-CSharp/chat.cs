using System;

// Token: 0x0200049E RID: 1182
public class chat : global::ConsoleSystem
{
	// Token: 0x0600289E RID: 10398 RVA: 0x000948B8 File Offset: 0x00092AB8
	[global::ConsoleSystem.Client]
	public static void add(ref global::ConsoleSystem.Arg arg)
	{
		if (!global::chat.enabled)
		{
			return;
		}
		string @string = arg.GetString(0, string.Empty);
		string string2 = arg.GetString(1, string.Empty);
		if (@string == string.Empty || string2 == string.Empty)
		{
			return;
		}
		global::ChatUI.AddLine(@string, string2);
	}

	// Token: 0x04001386 RID: 4998
	[global::ConsoleSystem.Admin]
	[global::ConsoleSystem.Client]
	[global::ConsoleSystem.Help("Enable or disable chat displaying", "")]
	public static bool enabled = true;
}
