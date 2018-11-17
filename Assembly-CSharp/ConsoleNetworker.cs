using System;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x020003E3 RID: 995
public class ConsoleNetworker : MonoBehaviour
{
	// Token: 0x060024EB RID: 9451 RVA: 0x0008D9C4 File Offset: 0x0008BBC4
	private void Awake()
	{
		ConsoleNetworker.singleton = this;
	}

	// Token: 0x060024EC RID: 9452 RVA: 0x0008D9CC File Offset: 0x0008BBCC
	public static void SendCommandToServer(string strCommand)
	{
		if (!ConsoleNetworker.singleton)
		{
			return;
		}
		ConsoleNetworker.singleton.networkView.RPC<string>("SV_RunConsoleCommand", 0, strCommand);
	}

	// Token: 0x060024ED RID: 9453 RVA: 0x0008DA00 File Offset: 0x0008BC00
	[RPC]
	public void SV_RunConsoleCommand(string cmd, NetworkMessageInfo info)
	{
	}

	// Token: 0x060024EE RID: 9454 RVA: 0x0008DA04 File Offset: 0x0008BC04
	[RPC]
	public void CL_ConsoleMessage(string message, NetworkMessageInfo info)
	{
		ConsoleWindow consoleWindow = (ConsoleWindow)Object.FindObjectOfType(typeof(ConsoleWindow));
		if (!consoleWindow)
		{
			return;
		}
		consoleWindow.AddText(message, true);
	}

	// Token: 0x060024EF RID: 9455 RVA: 0x0008DA3C File Offset: 0x0008BC3C
	[RPC]
	public void CL_ConsoleCommand(string message, NetworkMessageInfo info)
	{
		ConsoleWindow consoleWindow = (ConsoleWindow)Object.FindObjectOfType(typeof(ConsoleWindow));
		if (!consoleWindow)
		{
			return;
		}
		if (!ConsoleSystem.Run(message, false))
		{
			Debug.Log("Unhandled command from server: " + message);
		}
	}

	// Token: 0x040011D5 RID: 4565
	public static ConsoleNetworker singleton;
}
