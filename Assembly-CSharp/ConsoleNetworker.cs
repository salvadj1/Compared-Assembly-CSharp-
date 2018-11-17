using System;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x02000493 RID: 1171
public class ConsoleNetworker : MonoBehaviour
{
	// Token: 0x0600285D RID: 10333 RVA: 0x000933B0 File Offset: 0x000915B0
	private void Awake()
	{
		global::ConsoleNetworker.singleton = this;
	}

	// Token: 0x0600285E RID: 10334 RVA: 0x000933B8 File Offset: 0x000915B8
	public static void SendCommandToServer(string strCommand)
	{
		if (!global::ConsoleNetworker.singleton)
		{
			return;
		}
		global::ConsoleNetworker.singleton.networkView.RPC<string>("SV_RunConsoleCommand", 0, strCommand);
	}

	// Token: 0x0600285F RID: 10335 RVA: 0x000933EC File Offset: 0x000915EC
	[RPC]
	public void SV_RunConsoleCommand(string cmd, uLink.NetworkMessageInfo info)
	{
	}

	// Token: 0x06002860 RID: 10336 RVA: 0x000933F0 File Offset: 0x000915F0
	[RPC]
	public void CL_ConsoleMessage(string message, uLink.NetworkMessageInfo info)
	{
		global::ConsoleWindow consoleWindow = (global::ConsoleWindow)Object.FindObjectOfType(typeof(global::ConsoleWindow));
		if (!consoleWindow)
		{
			return;
		}
		consoleWindow.AddText(message, true);
	}

	// Token: 0x06002861 RID: 10337 RVA: 0x00093428 File Offset: 0x00091628
	[RPC]
	public void CL_ConsoleCommand(string message, uLink.NetworkMessageInfo info)
	{
		global::ConsoleWindow consoleWindow = (global::ConsoleWindow)Object.FindObjectOfType(typeof(global::ConsoleWindow));
		if (!consoleWindow)
		{
			return;
		}
		if (!global::ConsoleSystem.Run(message, false))
		{
			Debug.Log("Unhandled command from server: " + message);
		}
	}

	// Token: 0x0400134F RID: 4943
	public static global::ConsoleNetworker singleton;
}
