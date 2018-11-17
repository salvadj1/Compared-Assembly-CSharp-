using System;

// Token: 0x020003FE RID: 1022
public class gameui : ConsoleSystem
{
	// Token: 0x06002593 RID: 9619 RVA: 0x000906B8 File Offset: 0x0008E8B8
	[ConsoleSystem.Client]
	public static void hide(ref ConsoleSystem.Arg arg)
	{
		MainMenu.singleton.Hide();
	}

	// Token: 0x06002594 RID: 9620 RVA: 0x000906C4 File Offset: 0x0008E8C4
	[ConsoleSystem.Client]
	public static void show(ref ConsoleSystem.Arg arg)
	{
		MainMenu.singleton.Show();
	}
}
