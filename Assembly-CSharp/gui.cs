using System;
using UnityEngine;

// Token: 0x0200004B RID: 75
public class gui : ConsoleSystem
{
	// Token: 0x06000296 RID: 662 RVA: 0x0000E0F4 File Offset: 0x0000C2F4
	[ConsoleSystem.Help("Hides all GUI (useful for taking screenshots)", "")]
	[ConsoleSystem.Client]
	public static void hide(ref ConsoleSystem.Arg args)
	{
		GUIHide.SetVisible(false);
	}

	// Token: 0x06000297 RID: 663 RVA: 0x0000E0FC File Offset: 0x0000C2FC
	[ConsoleSystem.Help("The opposite of gui.hide", "")]
	[ConsoleSystem.Client]
	public static void show(ref ConsoleSystem.Arg args)
	{
		GUIHide.SetVisible(true);
	}

	// Token: 0x06000298 RID: 664 RVA: 0x0000E104 File Offset: 0x0000C304
	[ConsoleSystem.Client]
	[ConsoleSystem.Help("Hides the alpha/branding on the top right", "")]
	public static void hide_branding(ref ConsoleSystem.Arg args)
	{
		GameObject gameObject = GameObject.Find("BrandingPanel");
		if (gameObject == null)
		{
			return;
		}
		gameObject.GetComponent<dfPanel>().Hide();
	}

	// Token: 0x06000299 RID: 665 RVA: 0x0000E134 File Offset: 0x0000C334
	[ConsoleSystem.Client]
	[ConsoleSystem.Help("The opposite of gui.hide_branding", "")]
	public static void show_branding(ref ConsoleSystem.Arg args)
	{
		GameObject gameObject = GameObject.Find("BrandingPanel");
		if (gameObject == null)
		{
			return;
		}
		gameObject.GetComponent<dfPanel>().Show();
	}
}
