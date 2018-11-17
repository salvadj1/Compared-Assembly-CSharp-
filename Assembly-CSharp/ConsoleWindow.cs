using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Facepunch.Cursor;
using UnityEngine;

// Token: 0x020003E4 RID: 996
public class ConsoleWindow : MonoBehaviour
{
	// Token: 0x060024F1 RID: 9457 RVA: 0x0008DAA8 File Offset: 0x0008BCA8
	public static bool IsVisible()
	{
		return ConsoleWindow.singleton && ConsoleWindow.singleton.GetComponent<dfPanel>().IsVisible;
	}

	// Token: 0x060024F2 RID: 9458 RVA: 0x0008DACC File Offset: 0x0008BCCC
	private void Awake()
	{
		ConsoleWindow.singleton = this;
	}

	// Token: 0x060024F3 RID: 9459 RVA: 0x0008DAD4 File Offset: 0x0008BCD4
	private void Start()
	{
		ConsoleSystem.RegisterLogCallback(new Application.LogCallback(this.CaptureLog), false);
		ConsoleWindow.singleton.GetComponent<dfPanel>().Hide();
	}

	// Token: 0x060024F4 RID: 9460 RVA: 0x0008DAF8 File Offset: 0x0008BCF8
	private void OnDestroy()
	{
		ConsoleSystem.UnregisterLogCallback(new Application.LogCallback(this.CaptureLog));
	}

	// Token: 0x060024F5 RID: 9461 RVA: 0x0008DB0C File Offset: 0x0008BD0C
	public void AddText(string str, bool bFromServer = false)
	{
		if (bFromServer)
		{
			str = "[color #00ffff]" + str + "[/color]\n";
		}
		dfLabel dfLabel = this.consoleOutput;
		dfLabel.Text = dfLabel.Text + str + "\n";
		this.TrimBuffer();
		if (this.consoleScroller.Value >= this.consoleScroller.MaxValue - this.consoleScroller.ScrollSize - 50f)
		{
			this.shouldScrollDown = true;
		}
	}

	// Token: 0x060024F6 RID: 9462 RVA: 0x0008DB88 File Offset: 0x0008BD88
	private void Update()
	{
		if (Input.GetKeyDown(282))
		{
			if (ConsoleWindow.IsVisible())
			{
				this.consoleInput.Unfocus();
				ConsoleWindow.singleton.GetComponent<dfPanel>().Hide();
				this.cursorManager.On = false;
			}
			else
			{
				ConsoleWindow.singleton.GetComponent<dfPanel>().Show();
				ConsoleWindow.singleton.GetComponent<dfPanel>().BringToFront();
				this.consoleInput.Focus();
				this.cursorManager.On = true;
			}
			this.consoleInput.Text = string.Empty;
			return;
		}
		if (this.shouldScrollDown && this.consoleScroller.Value != this.consoleScroller.MaxValue - this.consoleScroller.ScrollSize)
		{
			this.consoleScroller.Value = this.consoleScroller.MaxValue;
			this.shouldScrollDown = false;
		}
	}

	// Token: 0x060024F7 RID: 9463 RVA: 0x0008DC70 File Offset: 0x0008BE70
	public void OnInput()
	{
		string text = this.consoleInput.Text;
		this.consoleInput.Text = string.Empty;
		this.RunCommand(text);
	}

	// Token: 0x060024F8 RID: 9464 RVA: 0x0008DCA0 File Offset: 0x0008BEA0
	public void RunCommand(string strInput)
	{
		this.AddText("[color #00ff00]> " + strInput + "[/color]", false);
		string empty = string.Empty;
		if (ConsoleSystem.RunCommand_Clientside(strInput, out empty, true))
		{
			if (empty != string.Empty)
			{
				this.AddText("[color #ffff00]" + empty + "[/color]", false);
			}
		}
		else
		{
			ConsoleNetworker.SendCommandToServer(strInput);
		}
	}

	// Token: 0x060024F9 RID: 9465 RVA: 0x0008DD0C File Offset: 0x0008BF0C
	private void CaptureLog(string log, string stacktrace, LogType type)
	{
		if (!Application.isPlaying)
		{
			return;
		}
		if (log.StartsWith("This uLink evaluation license is temporary."))
		{
			return;
		}
		if (log.StartsWith("Failed to capture screen shot"))
		{
			return;
		}
		if (type == 3)
		{
			this.AddText("[color #eeeeee]> " + log + "[/color]", false);
		}
		else
		{
			this.AddText("[color #ff0000]> " + log + "[/color]", false);
		}
		if (log.StartsWith("Resynchronize Clock is still in progress"))
		{
			return;
		}
		if (type == 4 || type == null)
		{
			if (stacktrace.Length > 8)
			{
				ConsoleWindow.Client_Error(log, stacktrace);
			}
			else
			{
				string text = StackTraceUtility.ExtractStackTrace();
				if (text.Length > 8)
				{
					ConsoleWindow.Client_Error(log, text);
				}
				else
				{
					StackTrace stackTrace = new StackTrace();
					ConsoleWindow.Client_Error(log, stackTrace.ToString());
				}
			}
		}
	}

	// Token: 0x060024FA RID: 9466 RVA: 0x0008DDE8 File Offset: 0x0008BFE8
	private void TrimBuffer()
	{
		if (this.consoleOutput.Text.Length < 5000)
		{
			return;
		}
		int num = this.consoleOutput.Text.IndexOf('\n');
		if (num == -1)
		{
			return;
		}
		this.consoleOutput.Text = this.consoleOutput.Text.Substring(num + 1);
	}

	// Token: 0x060024FB RID: 9467
	[DllImport("librust")]
	public static extern void Client_Error(string strLog, string strTrace);

	// Token: 0x040011D6 RID: 4566
	public dfTextbox consoleInput;

	// Token: 0x040011D7 RID: 4567
	public dfLabel consoleOutput;

	// Token: 0x040011D8 RID: 4568
	public dfScrollbar consoleScroller;

	// Token: 0x040011D9 RID: 4569
	[NonSerialized]
	public UnlockCursorNode cursorManager = LockCursorManager.CreateCursorUnlockNode(false, "Console Window");

	// Token: 0x040011DA RID: 4570
	public static ConsoleWindow singleton;

	// Token: 0x040011DB RID: 4571
	[NonSerialized]
	protected bool shouldScrollDown = true;
}
