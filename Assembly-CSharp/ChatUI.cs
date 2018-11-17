using System;
using Facepunch.Cursor;
using Facepunch.Utility;
using UnityEngine;

// Token: 0x020003ED RID: 1005
public class ChatUI : MonoBehaviour
{
	// Token: 0x0600251D RID: 9501 RVA: 0x0008EB58 File Offset: 0x0008CD58
	private void Awake()
	{
		this.unlockNode = LockCursorManager.CreateCursorUnlockNode(false, "ChatUI");
	}

	// Token: 0x0600251E RID: 9502 RVA: 0x0008EB6C File Offset: 0x0008CD6C
	private void OnDestroy()
	{
		if (this.unlockNode != null)
		{
			this.unlockNode.Dispose();
			this.unlockNode = null;
		}
	}

	// Token: 0x0600251F RID: 9503 RVA: 0x0008EB8C File Offset: 0x0008CD8C
	private void Start()
	{
		ChatUI.singleton = this;
		this.textInput.Hide();
	}

	// Token: 0x06002520 RID: 9504 RVA: 0x0008EBA0 File Offset: 0x0008CDA0
	public static bool IsVisible()
	{
		return !(ChatUI.singleton == null) && ChatUI.singleton.textInput.IsVisible;
	}

	// Token: 0x06002521 RID: 9505 RVA: 0x0008EBC4 File Offset: 0x0008CDC4
	public static void Open()
	{
		if (ChatUI.singleton == null)
		{
			return;
		}
		ChatUI.singleton.textInput.Text = string.Empty;
		ChatUI.singleton.textInput.Show();
		ChatUI.singleton.textInput.Focus();
		ChatUI.singleton.Invoke("ClearText", 0.05f);
		if (ChatUI.singleton.unlockNode != null)
		{
			ChatUI.singleton.unlockNode.On = true;
		}
	}

	// Token: 0x06002522 RID: 9506 RVA: 0x0008EC48 File Offset: 0x0008CE48
	public static void Close()
	{
		if (ChatUI.singleton == null)
		{
			return;
		}
		ChatUI.singleton.CancelChatting();
	}

	// Token: 0x06002523 RID: 9507 RVA: 0x0008EC68 File Offset: 0x0008CE68
	public static void AddLine(string name, string text)
	{
		if (ChatUI.singleton == null)
		{
			return;
		}
		GameObject gameObject = (GameObject)Object.Instantiate(ChatUI.singleton.chatLine);
		if (gameObject == null)
		{
			return;
		}
		ChatLine component = gameObject.GetComponent<ChatLine>();
		component.Setup(name + ":", text);
		ChatUI.singleton.chatContainer.AddControl(component.GetComponent<dfPanel>());
	}

	// Token: 0x06002524 RID: 9508 RVA: 0x0008ECD8 File Offset: 0x0008CED8
	public void ReLayout()
	{
		this.chatContainer.RelativePosition = new Vector2(0f, 0f);
		dfPanel[] componentsInChildren = this.chatContainer.GetComponentsInChildren<dfPanel>();
		float num = 0f;
		foreach (dfPanel dfPanel in componentsInChildren)
		{
			if (!(dfPanel.gameObject == this.chatContainer.gameObject))
			{
				num += dfPanel.Height;
			}
		}
		Vector2 vector;
		vector..ctor(0f, this.chatContainer.Height - num);
		foreach (dfPanel dfPanel2 in componentsInChildren)
		{
			if (!(dfPanel2.gameObject == this.chatContainer.gameObject))
			{
				dfPanel2.RelativePosition = vector;
				vector.y += dfPanel2.Height;
			}
		}
	}

	// Token: 0x06002525 RID: 9509 RVA: 0x0008EDE0 File Offset: 0x0008CFE0
	public void CancelChatting()
	{
		this.textInput.Text = string.Empty;
		ChatUI.singleton.Invoke("CancelChatting_Delayed", 0.2f);
	}

	// Token: 0x06002526 RID: 9510 RVA: 0x0008EE14 File Offset: 0x0008D014
	public void CancelChatting_Delayed()
	{
		this.unlockNode.TryLock();
		this.textInput.Text = string.Empty;
		this.textInput.Unfocus();
		this.textInput.Hide();
	}

	// Token: 0x06002527 RID: 9511 RVA: 0x0008EE54 File Offset: 0x0008D054
	public void ClearText()
	{
		this.textInput.Text = string.Empty;
	}

	// Token: 0x06002528 RID: 9512 RVA: 0x0008EE68 File Offset: 0x0008D068
	public void SendChat()
	{
		if (this.textInput.Text != string.Empty)
		{
			ConsoleNetworker.SendCommandToServer("chat.say " + Facepunch.Utility.String.QuoteSafe(this.textInput.Text));
		}
		this.CancelChatting();
	}

	// Token: 0x06002529 RID: 9513 RVA: 0x0008EEB4 File Offset: 0x0008D0B4
	private void OnLoseFocus()
	{
		this.CancelChatting();
	}

	// Token: 0x04001207 RID: 4615
	public dfTextbox textInput;

	// Token: 0x04001208 RID: 4616
	public dfPanel chatContainer;

	// Token: 0x04001209 RID: 4617
	public Object chatLine;

	// Token: 0x0400120A RID: 4618
	public static ChatUI singleton;

	// Token: 0x0400120B RID: 4619
	[NonSerialized]
	private UnlockCursorNode unlockNode;
}
