using System;
using UnityEngine;

// Token: 0x02000623 RID: 1571
internal class net : ConsoleSystem
{
	// Token: 0x060037B3 RID: 14259 RVA: 0x000CC9D8 File Offset: 0x000CABD8
	[ConsoleSystem.Client]
	[ConsoleSystem.Help("connect to a server", "string serverurl")]
	public static void connect(ref ConsoleSystem.Arg arg)
	{
		Object @object = Object.FindObjectOfType(typeof(ClientConnect));
		if (@object)
		{
			Debug.Log("Connect already in progress!");
			return;
		}
		if (NetCull.isClientRunning)
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
		ClientConnect clientConnect = ClientConnect.Instance();
		if (!clientConnect.DoConnect(text, num))
		{
			return;
		}
		LoadingScreen.Show();
		LoadingScreen.Update("connecting..");
	}

	// Token: 0x060037B4 RID: 14260 RVA: 0x000CCAC8 File Offset: 0x000CACC8
	[ConsoleSystem.Help("disconnect from server", "")]
	[ConsoleSystem.Client]
	public static void disconnect(ref ConsoleSystem.Arg arg)
	{
		if (!NetCull.isClientRunning)
		{
			Debug.Log("You're not connected to a server.");
			return;
		}
		NetCull.Disconnect();
	}

	// Token: 0x060037B5 RID: 14261 RVA: 0x000CCAE4 File Offset: 0x000CACE4
	[ConsoleSystem.Help("reconnect to last server", "")]
	[ConsoleSystem.Client]
	public static void reconnect(ref ConsoleSystem.Arg arg)
	{
		if (PlayerPrefs.HasKey("net.lasturl"))
		{
			ConsoleSystem.Run("net.connect " + PlayerPrefs.GetString("net.lasturl"), false);
		}
		else
		{
			Debug.Log("You havn't connected to a server yet");
		}
	}
}
