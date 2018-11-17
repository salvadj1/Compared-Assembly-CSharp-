using System;
using UnityEngine;

// Token: 0x0200004C RID: 76
public class input : ConsoleSystem
{
	// Token: 0x0600029C RID: 668 RVA: 0x0000E178 File Offset: 0x0000C378
	[ConsoleSystem.Client]
	[ConsoleSystem.Help("Internal use only", "")]
	public static void bind(ref ConsoleSystem.Arg args)
	{
		if (!args.HasArgs(3))
		{
			return;
		}
		string strName = args.Args[0];
		string a = args.Args[1];
		string b = args.Args[2];
		GameInput.GameButton button = GameInput.GetButton(strName);
		if (button != null)
		{
			button.Bind(a, b);
		}
	}

	// Token: 0x0600029D RID: 669 RVA: 0x0000E1C8 File Offset: 0x0000C3C8
	[ConsoleSystem.Help("Internal use only", "")]
	[ConsoleSystem.Client]
	public static void keys(ref ConsoleSystem.Arg args)
	{
		Debug.Log(GameInput.GetConfig());
	}

	// Token: 0x0600029E RID: 670 RVA: 0x0000E1D4 File Offset: 0x0000C3D4
	[ConsoleSystem.Saved]
	public static string save_bound_keys()
	{
		return GameInput.GetConfig();
	}

	// Token: 0x040001A7 RID: 423
	[ConsoleSystem.Saved]
	[ConsoleSystem.Help("The mouse sensitivity. Default is 5.0", "")]
	[ConsoleSystem.Client]
	public static float mousespeed = 5f;

	// Token: 0x040001A8 RID: 424
	[ConsoleSystem.Saved]
	[ConsoleSystem.Help("Should we flip the mouse pitch movement? Default is false", "")]
	[ConsoleSystem.Client]
	public static bool flipy;
}
