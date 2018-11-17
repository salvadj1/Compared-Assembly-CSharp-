using System;
using UnityEngine;

// Token: 0x0200005E RID: 94
public class input : global::ConsoleSystem
{
	// Token: 0x0600030E RID: 782 RVA: 0x0000F720 File Offset: 0x0000D920
	[global::ConsoleSystem.Client]
	[global::ConsoleSystem.Help("Internal use only", "")]
	public static void bind(ref global::ConsoleSystem.Arg args)
	{
		if (!args.HasArgs(3))
		{
			return;
		}
		string strName = args.Args[0];
		string a = args.Args[1];
		string b = args.Args[2];
		global::GameInput.GameButton button = global::GameInput.GetButton(strName);
		if (button != null)
		{
			button.Bind(a, b);
		}
	}

	// Token: 0x0600030F RID: 783 RVA: 0x0000F770 File Offset: 0x0000D970
	[global::ConsoleSystem.Help("Internal use only", "")]
	[global::ConsoleSystem.Client]
	public static void keys(ref global::ConsoleSystem.Arg args)
	{
		Debug.Log(global::GameInput.GetConfig());
	}

	// Token: 0x06000310 RID: 784 RVA: 0x0000F77C File Offset: 0x0000D97C
	[global::ConsoleSystem.Saved]
	public static string save_bound_keys()
	{
		return global::GameInput.GetConfig();
	}

	// Token: 0x04000209 RID: 521
	[global::ConsoleSystem.Client]
	[global::ConsoleSystem.Help("The mouse sensitivity. Default is 5.0", "")]
	[global::ConsoleSystem.Saved]
	public static float mousespeed = 5f;

	// Token: 0x0400020A RID: 522
	[global::ConsoleSystem.Client]
	[global::ConsoleSystem.Saved]
	[global::ConsoleSystem.Help("Should we flip the mouse pitch movement? Default is false", "")]
	public static bool flipy;
}
