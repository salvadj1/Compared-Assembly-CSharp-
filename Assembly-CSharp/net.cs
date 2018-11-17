using System;
using UnityEngine;

// Token: 0x020006E5 RID: 1765
internal class net : global::ConsoleSystem
{
	// Token: 0x06003B99 RID: 15257 RVA: 0x000D5180 File Offset: 0x000D3380
	[global::ConsoleSystem.Client]
	[global::ConsoleSystem.Help("connect to a server", "string serverurl")]
	public static void connect(ref global::ConsoleSystem.Arg arg)
	{
		Object @object = Object.FindObjectOfType(typeof(global::ClientConnect));
		if (@object)
		{
			Debug.Log("Connect already in progress!");
			return;
		}
		if (global::NetCull.isClientRunning)
		{
			Debug.Log("Use net.disconnect before trying to connect to a new server.");
			return;
		}
		string[] array = arg.GetString(0, string.Empty).Split(new char[]
		{
			':'
		});
		if (array.Length != 2)
		{
			Debug.Log("Not a valid ip - or port missing");
			return;
		}
		string text = array[0];
		int num = int.Parse(array[1]);
		Debug.Log(string.Concat(new object[]
		{
			"Connecting to ",
			text,
			":",
			num
		}));
		PlayerPrefs.SetString("net.lasturl", arg.GetString(0, string.Empty));
		global::ClientConnect clientConnect = global::ClientConnect.Instance();
		if (!clientConnect.DoConnect(text, num))
		{
			return;
		}
		global::LoadingScreen.Show();
		global::LoadingScreen.Update("connecting..");
	}

	// Token: 0x06003B9A RID: 15258 RVA: 0x000D5270 File Offset: 0x000D3470
	[global::ConsoleSystem.Client]
	[global::ConsoleSystem.Help("disconnect from server", "")]
	public static void disconnect(ref global::ConsoleSystem.Arg arg)
	{
		if (!global::NetCull.isClientRunning)
		{
			Debug.Log("You're not connected to a server.");
			return;
		}
		global::NetCull.Disconnect();
	}

	// Token: 0x06003B9B RID: 15259 RVA: 0x000D528C File Offset: 0x000D348C
	[global::ConsoleSystem.Client]
	[global::ConsoleSystem.Help("reconnect to last server", "")]
	public static void reconnect(ref global::ConsoleSystem.Arg arg)
	{
		if (PlayerPrefs.HasKey("net.lasturl"))
		{
			global::ConsoleSystem.Run("net.connect " + PlayerPrefs.GetString("net.lasturl"), false);
		}
		else
		{
			Debug.Log("You havn't connected to a server yet");
		}
	}
}
