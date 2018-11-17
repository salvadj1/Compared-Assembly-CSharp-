using System;

// Token: 0x0200063A RID: 1594
public class RustPlayerClient : PlayerClient
{
	// Token: 0x060037E3 RID: 14307 RVA: 0x000CD150 File Offset: 0x000CB350
	protected override void ClientInput()
	{
		if (MainMenu.IsVisible())
		{
			return;
		}
		if (ConsoleWindow.IsVisible())
		{
			return;
		}
		base.ClientInput();
	}
}
