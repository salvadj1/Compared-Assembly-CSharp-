using System;

// Token: 0x020003F3 RID: 1011
public class lockentry : ConsoleSystem
{
	// Token: 0x0600254C RID: 9548 RVA: 0x0008F444 File Offset: 0x0008D644
	[ConsoleSystem.Client]
	public static void show(ref ConsoleSystem.Arg arg)
	{
		bool changing = false;
		bool.TryParse(arg.Args[0], out changing);
		LockEntry.Show(changing);
	}

	// Token: 0x0600254D RID: 9549 RVA: 0x0008F46C File Offset: 0x0008D66C
	[ConsoleSystem.Client]
	public static void hide(ref ConsoleSystem.Arg arg)
	{
		LockEntry.Hide();
	}
}
