using System;

// Token: 0x020006FD RID: 1789
public class RustPlayerClient : global::PlayerClient
{
	// Token: 0x06003BCF RID: 15311 RVA: 0x000D5A00 File Offset: 0x000D3C00
	protected override void ClientInput()
	{
		if (global::MainMenu.IsVisible())
		{
			return;
		}
		if (global::ConsoleWindow.IsVisible())
		{
			return;
		}
		base.ClientInput();
	}
}
