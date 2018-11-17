using System;
using Facepunch.Cursor;
using Facepunch.Utility;
using UnityEngine;

// Token: 0x0200049D RID: 1181
public class ChatUI : MonoBehaviour
{
	// Token: 0x0600288F RID: 10383 RVA: 0x00094544 File Offset: 0x00092744
	private void Awake()
	{
		this.unlockNode = LockCursorManager.CreateCursorUnlockNode(false, "ChatUI");
	}

	// Token: 0x06002890 RID: 10384 RVA: 0x00094558 File Offset: 0x00092758
	private void OnDestroy()
	{
		if (this.unlockNode != null)
		{
			this.unlockNode.Dispose();
			this.unlockNode = null;
		}
	}

	// Token: 0x06002891 RID: 10385 RVA: 0x00094578 File Offset: 0x00092778
	private void Start()
	{
		global::ChatUI.singleton = this;
		this.textInput.Hide();
	}

	// Token: 0x06002892 RID: 10386 RVA: 0x0009458C File Offset: 0x0009278C
	public static bool IsVisible()
	{
		return !(global::ChatUI.singleton == null) && global::ChatUI.singleton.textInput.IsVisible;
	}

	// Token: 0x06002893 RID: 10387 RVA: 0x000945B0 File Offset: 0x000927B0
	public static void Open()
	{
		if (global::ChatUI.singleton == null)
		{
			return;
		}
		global::ChatUI.singleton.textInput.Text = string.Empty;
		global::ChatUI.singleton.textInput.Show();
		global::ChatUI.singleton.textInput.Focus();
		global::ChatUI.singleton.Invoke("ClearText", 0.05f);
		if (global::ChatUI.singleton.unlockNode != null)
		{
			global::ChatUI.singleton.unlockNode.On = true;
		}
	}

	// Token: 0x06002894 RID: 10388 RVA: 0x00094634 File Offset: 0x00092834
	public static void Close()
	{
		if (global::ChatUI.singleton == null)
		{
			return;
		}
		global::ChatUI.singleton.CancelChatting();
	}

	// Token: 0x06002895 RID: 10389 RVA: 0x00094654 File Offset: 0x00092854
	public static void AddLine(string name, string text)
	{
		if (global::ChatUI.singleton == null)
		{
			return;
		}
		GameObject gameObject = (GameObject)Object.Instantiate(global::ChatUI.singleton.chatLine);
		if (gameObject == null)
		{
			return;
		}
		global::ChatLine component = gameObject.GetComponent<global::ChatLine>();
		component.Setup(name + ":", text);
		global::ChatUI.singleton.chatContainer.AddControl(component.GetComponent<global::dfPanel>());
	}

	// Token: 0x06002896 RID: 10390 RVA: 0x000946C4 File Offset: 0x000928C4
	public void ReLayout()
	{
		this.chatContainer.RelativePosition = new Vector2(0f, 0f);
		global::dfPanel[] componentsInChildren = this.chatContainer.GetComponentsInChildren<global::dfPanel>();
		float num = 0f;
		foreach (global::dfPanel dfPanel in componentsInChildren)
		{
			if (!(dfPanel.gameObject == this.chatContainer.gameObject))
			{
				num += dfPanel.Height;
			}
		}
		Vector2 vector;
		vector..ctor(0f, this.chatContainer.Height - num);
		foreach (global::dfPanel dfPanel2 in componentsInChildren)
		{
			if (!(dfPanel2.gameObject == this.chatContainer.gameObject))
			{
				dfPanel2.RelativePosition = vector;
				vector.y += dfPanel2.Height;
			}
		}
	}

	// Token: 0x06002897 RID: 10391 RVA: 0x000947CC File Offset: 0x000929CC
	public void CancelChatting()
	{
		this.textInput.Text = string.Empty;
		global::ChatUI.singleton.Invoke("CancelChatting_Delayed", 0.2f);
	}

	// Token: 0x06002898 RID: 10392 RVA: 0x00094800 File Offset: 0x00092A00
	public void CancelChatting_Delayed()
	{
		this.unlockNode.TryLock();
		this.textInput.Text = string.Empty;
		this.textInput.Unfocus();
		this.textInput.Hide();
	}

	// Token: 0x06002899 RID: 10393 RVA: 0x00094840 File Offset: 0x00092A40
	public void ClearText()
	{
		this.textInput.Text = string.Empty;
	}

	// Token: 0x0600289A RID: 10394 RVA: 0x00094854 File Offset: 0x00092A54
	public void SendChat()
	{
		if (this.textInput.Text != string.Empty)
		{
			global::ConsoleNetworker.SendCommandToServer("chat.say " + Facepunch.Utility.String.QuoteSafe(this.textInput.Text));
		}
		this.CancelChatting();
	}

	// Token: 0x0600289B RID: 10395 RVA: 0x000948A0 File Offset: 0x00092AA0
	private void OnLoseFocus()
	{
		this.CancelChatting();
	}

	// Token: 0x04001381 RID: 4993
	public global::dfTextbox textInput;

	// Token: 0x04001382 RID: 4994
	public global::dfPanel chatContainer;

	// Token: 0x04001383 RID: 4995
	public Object chatLine;

	// Token: 0x04001384 RID: 4996
	public static global::ChatUI singleton;

	// Token: 0x04001385 RID: 4997
	[NonSerialized]
	private UnlockCursorNode unlockNode;
}
