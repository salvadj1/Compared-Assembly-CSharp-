using System;
using UnityEngine;

// Token: 0x0200005D RID: 93
public class gui : global::ConsoleSystem
{
	// Token: 0x06000308 RID: 776 RVA: 0x0000F69C File Offset: 0x0000D89C
	[global::ConsoleSystem.Help("Hides all GUI (useful for taking screenshots)", "")]
	[global::ConsoleSystem.Client]
	public static void hide(ref global::ConsoleSystem.Arg args)
	{
		global::GUIHide.SetVisible(false);
	}

	// Token: 0x06000309 RID: 777 RVA: 0x0000F6A4 File Offset: 0x0000D8A4
	[global::ConsoleSystem.Help("The opposite of gui.hide", "")]
	[global::ConsoleSystem.Client]
	public static void show(ref global::ConsoleSystem.Arg args)
	{
		global::GUIHide.SetVisible(true);
	}

	// Token: 0x0600030A RID: 778 RVA: 0x0000F6AC File Offset: 0x0000D8AC
	[global::ConsoleSystem.Help("Hides the alpha/branding on the top right", "")]
	[global::ConsoleSystem.Client]
	public static void hide_branding(ref global::ConsoleSystem.Arg args)
	{
		GameObject gameObject = GameObject.Find("BrandingPanel");
		if (gameObject == null)
		{
			return;
		}
		gameObject.GetComponent<global::dfPanel>().Hide();
	}

	// Token: 0x0600030B RID: 779 RVA: 0x0000F6DC File Offset: 0x0000D8DC
	[global::ConsoleSystem.Client]
	[global::ConsoleSystem.Help("The opposite of gui.hide_branding", "")]
	public static void show_branding(ref global::ConsoleSystem.Arg args)
	{
		GameObject gameObject = GameObject.Find("BrandingPanel");
		if (gameObject == null)
		{
			return;
		}
		gameObject.GetComponent<global::dfPanel>().Show();
	}
}
