using System;

// Token: 0x020004A6 RID: 1190
public class notice : global::ConsoleSystem
{
	// Token: 0x060028CC RID: 10444 RVA: 0x00095370 File Offset: 0x00093570
	[global::ConsoleSystem.Client]
	public static void popup(ref global::ConsoleSystem.Arg arg)
	{
		float @float = arg.GetFloat(0, 2f);
		string @string = arg.GetString(1, "!");
		string string2 = arg.GetString(2, "This is the text");
		global::PopupUI.singleton.CreateNotice(@float, @string, string2);
	}

	// Token: 0x060028CD RID: 10445 RVA: 0x000953B4 File Offset: 0x000935B4
	[global::ConsoleSystem.Client]
	public static void inventory(ref global::ConsoleSystem.Arg arg)
	{
		string @string = arg.GetString(0, "This is the text");
		global::PopupUI.singleton.CreateInventory(@string);
	}

	// Token: 0x060028CE RID: 10446 RVA: 0x000953DC File Offset: 0x000935DC
	[global::ConsoleSystem.Client]
	public static void test(ref global::ConsoleSystem.Arg arg)
	{
		global::PopupUI.singleton.StartCoroutine("DoTests");
	}
}
