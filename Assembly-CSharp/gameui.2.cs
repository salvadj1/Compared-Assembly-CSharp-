using System;

// Token: 0x020004AF RID: 1199
public class gameui : global::ConsoleSystem
{
	// Token: 0x0600290B RID: 10507 RVA: 0x000964F0 File Offset: 0x000946F0
	[global::ConsoleSystem.Client]
	public static void hide(ref global::ConsoleSystem.Arg arg)
	{
		global::MainMenu.singleton.Hide();
	}

	// Token: 0x0600290C RID: 10508 RVA: 0x000964FC File Offset: 0x000946FC
	[global::ConsoleSystem.Client]
	public static void show(ref global::ConsoleSystem.Arg arg)
	{
		global::MainMenu.singleton.Show();
	}
}
