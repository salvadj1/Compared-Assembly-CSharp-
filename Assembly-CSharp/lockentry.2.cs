using System;

// Token: 0x020004A3 RID: 1187
public class lockentry : global::ConsoleSystem
{
	// Token: 0x060028BE RID: 10430 RVA: 0x00094E30 File Offset: 0x00093030
	[global::ConsoleSystem.Client]
	public static void show(ref global::ConsoleSystem.Arg arg)
	{
		bool changing = false;
		bool.TryParse(arg.Args[0], out changing);
		global::LockEntry.Show(changing);
	}

	// Token: 0x060028BF RID: 10431 RVA: 0x00094E58 File Offset: 0x00093058
	[global::ConsoleSystem.Client]
	public static void hide(ref global::ConsoleSystem.Arg arg)
	{
		global::LockEntry.Hide();
	}
}
