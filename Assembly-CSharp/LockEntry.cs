using System;
using Facepunch.Cursor;
using UnityEngine;

// Token: 0x020004A2 RID: 1186
public class LockEntry : MonoBehaviour
{
	// Token: 0x060028B4 RID: 10420 RVA: 0x00094C44 File Offset: 0x00092E44
	public static void Show(bool changing)
	{
		global::LockEntry.singleton.changingEntry = changing;
		global::LockEntry.singleton.SetVisible(true);
	}

	// Token: 0x060028B5 RID: 10421 RVA: 0x00094C5C File Offset: 0x00092E5C
	public static bool IsVisible()
	{
		return !(global::LockEntry.singleton == null) && global::LockEntry.singleton.passwordInput.IsVisible;
	}

	// Token: 0x060028B6 RID: 10422 RVA: 0x00094C80 File Offset: 0x00092E80
	public static void Hide()
	{
		global::LockEntry.singleton.SetVisible(false);
	}

	// Token: 0x060028B7 RID: 10423 RVA: 0x00094C90 File Offset: 0x00092E90
	public void Start()
	{
		global::LockEntry.singleton = this;
		global::LockEntry.Hide();
	}

	// Token: 0x060028B8 RID: 10424 RVA: 0x00094CA0 File Offset: 0x00092EA0
	public void SetVisible(bool visible)
	{
		this.entryLabel.Text = ((!this.changingEntry) ? "Password:" : "New Password:");
		global::dfPanel component = base.GetComponent<global::dfPanel>();
		if (visible)
		{
			component.Show();
			component.BringToFront();
			this.passwordInput.Text = string.Empty;
			this.passwordInput.Focus();
		}
		else
		{
			component.Hide();
			this.passwordInput.Unfocus();
		}
		base.gameObject.SetActive(visible);
	}

	// Token: 0x060028B9 RID: 10425 RVA: 0x00094D28 File Offset: 0x00092F28
	public void OnDisable()
	{
		if (this.cursorLocker)
		{
			this.cursorLocker.On = false;
		}
	}

	// Token: 0x060028BA RID: 10426 RVA: 0x00094D48 File Offset: 0x00092F48
	public void OnEnable()
	{
		if (!this.cursorLocker)
		{
			this.cursorLocker = LockCursorManager.CreateCursorUnlockNode(false, "Lock Entry");
		}
		this.cursorLocker.On = true;
	}

	// Token: 0x060028BB RID: 10427 RVA: 0x00094D78 File Offset: 0x00092F78
	public void CancelPressed()
	{
		global::LockEntry.Hide();
	}

	// Token: 0x060028BC RID: 10428 RVA: 0x00094D80 File Offset: 0x00092F80
	public void PasswordEntered()
	{
		string text = this.passwordInput.Text;
		if (text.Length != 4)
		{
			global::ConsoleSystem.Run("notice.popup 5  \"Password must be 4 digits.\"", false);
			return;
		}
		foreach (char c in text)
		{
			if (!char.IsDigit(c))
			{
				global::ConsoleSystem.Run("notice.popup 5  \"Must be digits only.\"", false);
				return;
			}
		}
		global::ConsoleNetworker.SendCommandToServer("lockentry.passwordentry " + text + " " + ((!this.changingEntry) ? "false" : "true"));
		global::LockEntry.Hide();
	}

	// Token: 0x0400138B RID: 5003
	private static global::LockEntry singleton;

	// Token: 0x0400138C RID: 5004
	private UnlockCursorNode cursorLocker;

	// Token: 0x0400138D RID: 5005
	public global::dfTextbox passwordInput;

	// Token: 0x0400138E RID: 5006
	public global::dfRichTextLabel entryLabel;

	// Token: 0x0400138F RID: 5007
	private bool changingEntry;
}
