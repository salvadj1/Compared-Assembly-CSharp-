using System;
using UnityEngine;

// Token: 0x020000C4 RID: 196
public class RunConsoleCommand : MonoBehaviour
{
	// Token: 0x0600041D RID: 1053 RVA: 0x00015530 File Offset: 0x00013730
	public void RunCommandImmediately()
	{
		if (this.asIfTypedIntoConsole)
		{
			ConsoleWindow.singleton.RunCommand(this.consoleCommand);
			return;
		}
		ConsoleSystem.Run(this.consoleCommand, false);
	}

	// Token: 0x040003A5 RID: 933
	public string consoleCommand = "echo Missing Console Command!";

	// Token: 0x040003A6 RID: 934
	public bool asIfTypedIntoConsole;
}
