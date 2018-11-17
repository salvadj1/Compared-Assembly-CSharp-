using System;
using UnityEngine;

// Token: 0x020000D8 RID: 216
public class RunConsoleCommand : MonoBehaviour
{
	// Token: 0x0600049B RID: 1179 RVA: 0x00016EF8 File Offset: 0x000150F8
	public void RunCommandImmediately()
	{
		if (this.asIfTypedIntoConsole)
		{
			global::ConsoleWindow.singleton.RunCommand(this.consoleCommand);
			return;
		}
		global::ConsoleSystem.Run(this.consoleCommand, false);
	}

	// Token: 0x04000414 RID: 1044
	public string consoleCommand = "echo Missing Console Command!";

	// Token: 0x04000415 RID: 1045
	public bool asIfTypedIntoConsole;
}
