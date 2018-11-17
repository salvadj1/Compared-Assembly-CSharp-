using System;

// Token: 0x020003F5 RID: 1013
public class notice : ConsoleSystem
{
	// Token: 0x06002554 RID: 9556 RVA: 0x0008F538 File Offset: 0x0008D738
	[ConsoleSystem.Client]
	public static void popup(ref ConsoleSystem.Arg arg)
	{
		float @float = arg.GetFloat(0, 2f);
		string @string = arg.GetString(1, "!");
		string string2 = arg.GetString(2, "This is the text");
		PopupUI.singleton.CreateNotice(@float, @string, string2);
	}

	// Token: 0x06002555 RID: 9557 RVA: 0x0008F57C File Offset: 0x0008D77C
	[ConsoleSystem.Client]
	public static void inventory(ref ConsoleSystem.Arg arg)
	{
		string @string = arg.GetString(0, "This is the text");
		PopupUI.singleton.CreateInventory(@string);
	}

	// Token: 0x06002556 RID: 9558 RVA: 0x0008F5A4 File Offset: 0x0008D7A4
	[ConsoleSystem.Client]
	public static void test(ref ConsoleSystem.Arg arg)
	{
		PopupUI.singleton.StartCoroutine("DoTests");
	}
}
