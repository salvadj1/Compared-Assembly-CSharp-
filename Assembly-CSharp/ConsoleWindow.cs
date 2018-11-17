using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Facepunch.Cursor;
using UnityEngine;

// Token: 0x02000494 RID: 1172
public class ConsoleWindow : MonoBehaviour
{
	// Token: 0x06002863 RID: 10339 RVA: 0x00093494 File Offset: 0x00091694
	public static bool IsVisible()
	{
		return global::ConsoleWindow.singleton && global::ConsoleWindow.singleton.GetComponent<global::dfPanel>().IsVisible;
	}

	// Token: 0x06002864 RID: 10340 RVA: 0x000934B8 File Offset: 0x000916B8
	private void Awake()
	{
		global::ConsoleWindow.singleton = this;
	}

	// Token: 0x06002865 RID: 10341 RVA: 0x000934C0 File Offset: 0x000916C0
	private void Start()
	{
		global::ConsoleSystem.RegisterLogCallback(new Application.LogCallback(this.CaptureLog), false);
		global::ConsoleWindow.singleton.GetComponent<global::dfPanel>().Hide();
	}

	// Token: 0x06002866 RID: 10342 RVA: 0x000934E4 File Offset: 0x000916E4
	private void OnDestroy()
	{
		global::ConsoleSystem.UnregisterLogCallback(new Application.LogCallback(this.CaptureLog));
	}

	// Token: 0x06002867 RID: 10343 RVA: 0x000934F8 File Offset: 0x000916F8
	public void AddText(string str, bool bFromServer = false)
	{
		if (bFromServer)
		{
			str = "[color #00ffff]" + str + "[/color]\n";
		}
		global::dfLabel dfLabel = this.consoleOutput;
		dfLabel.Text = dfLabel.Text + str + "\n";
		this.TrimBuffer();
		if (this.consoleScroller.Value >= this.consoleScroller.MaxValue - this.consoleScroller.ScrollSize - 50f)
		{
			this.shouldScrollDown = true;
		}
	}

	// Token: 0x06002868 RID: 10344 RVA: 0x00093574 File Offset: 0x00091774
	private void Update()
	{
		if (Input.GetKeyDown(282))
		{
			if (global::ConsoleWindow.IsVisible())
			{
				this.consoleInput.Unfocus();
				global::ConsoleWindow.singleton.GetComponent<global::dfPanel>().Hide();
				this.cursorManager.On = false;
			}
			else
			{
				global::ConsoleWindow.singleton.GetComponent<global::dfPanel>().Show();
				global::ConsoleWindow.singleton.GetComponent<global::dfPanel>().BringToFront();
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

	// Token: 0x06002869 RID: 10345 RVA: 0x0009365C File Offset: 0x0009185C
	public void OnInput()
	{
		string text = this.consoleInput.Text;
		this.consoleInput.Text = string.Empty;
		this.RunCommand(text);
	}

	// Token: 0x0600286A RID: 10346 RVA: 0x0009368C File Offset: 0x0009188C
	public void RunCommand(string strInput)
	{
		this.AddText("[color #00ff00]> " + strInput + "[/color]", false);
		string empty = string.Empty;
		if (global::ConsoleSystem.RunCommand_Clientside(strInput, out empty, true))
		{
			if (empty != string.Empty)
			{
				this.AddText("[color #ffff00]" + empty + "[/color]", false);
			}
		}
		else
		{
			global::ConsoleNetworker.SendCommandToServer(strInput);
		}
	}

	// Token: 0x0600286B RID: 10347 RVA: 0x000936F8 File Offset: 0x000918F8
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
				global::ConsoleWindow.Client_Error(log, stacktrace);
			}
			else
			{
				string text = StackTraceUtility.ExtractStackTrace();
				if (text.Length > 8)
				{
					global::ConsoleWindow.Client_Error(log, text);
				}
				else
				{
					StackTrace stackTrace = new StackTrace();
					global::ConsoleWindow.Client_Error(log, stackTrace.ToString());
				}
			}
		}
	}

	// Token: 0x0600286C RID: 10348 RVA: 0x000937D4 File Offset: 0x000919D4
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

	// Token: 0x0600286D RID: 10349
	[DllImport("librust")]
	public static extern void Client_Error(string strLog, string strTrace);

	// Token: 0x04001350 RID: 4944
	public global::dfTextbox consoleInput;

	// Token: 0x04001351 RID: 4945
	public global::dfLabel consoleOutput;

	// Token: 0x04001352 RID: 4946
	public global::dfScrollbar consoleScroller;

	// Token: 0x04001353 RID: 4947
	[NonSerialized]
	public UnlockCursorNode cursorManager = LockCursorManager.CreateCursorUnlockNode(false, "Console Window");

	// Token: 0x04001354 RID: 4948
	public static global::ConsoleWindow singleton;

	// Token: 0x04001355 RID: 4949
	[NonSerialized]
	protected bool shouldScrollDown = true;
}
