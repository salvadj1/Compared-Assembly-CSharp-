using System;
using Facepunch.Cursor;
using UnityEngine;

// Token: 0x020003F2 RID: 1010
public class LockEntry : MonoBehaviour
{
	// Token: 0x06002542 RID: 9538 RVA: 0x0008F258 File Offset: 0x0008D458
	public static void Show(bool changing)
	{
		LockEntry.singleton.changingEntry = changing;
		LockEntry.singleton.SetVisible(true);
	}

	// Token: 0x06002543 RID: 9539 RVA: 0x0008F270 File Offset: 0x0008D470
	public static bool IsVisible()
	{
		return !(LockEntry.singleton == null) && LockEntry.singleton.passwordInput.IsVisible;
	}

	// Token: 0x06002544 RID: 9540 RVA: 0x0008F294 File Offset: 0x0008D494
	public static void Hide()
	{
		LockEntry.singleton.SetVisible(false);
	}

	// Token: 0x06002545 RID: 9541 RVA: 0x0008F2A4 File Offset: 0x0008D4A4
	public void Start()
	{
		LockEntry.singleton = this;
		LockEntry.Hide();
	}

	// Token: 0x06002546 RID: 9542 RVA: 0x0008F2B4 File Offset: 0x0008D4B4
	public void SetVisible(bool visible)
	{
		this.entryLabel.Text = ((!this.changingEntry) ? "Password:" : "New Password:");
		dfPanel component = base.GetComponent<dfPanel>();
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

	// Token: 0x06002547 RID: 9543 RVA: 0x0008F33C File Offset: 0x0008D53C
	public void OnDisable()
	{
		if (this.cursorLocker)
		{
			this.cursorLocker.On = false;
		}
	}

	// Token: 0x06002548 RID: 9544 RVA: 0x0008F35C File Offset: 0x0008D55C
	public void OnEnable()
	{
		if (!this.cursorLocker)
		{
			this.cursorLocker = LockCursorManager.CreateCursorUnlockNode(false, "Lock Entry");
		}
		this.cursorLocker.On = true;
	}

	// Token: 0x06002549 RID: 9545 RVA: 0x0008F38C File Offset: 0x0008D58C
	public void CancelPressed()
	{
		LockEntry.Hide();
	}

	// Token: 0x0600254A RID: 9546 RVA: 0x0008F394 File Offset: 0x0008D594
	public void PasswordEntered()
	{
		string text = this.passwordInput.Text;
		if (text.Length != 4)
		{
			ConsoleSystem.Run("notice.popup 5  \"Password must be 4 digits.\"", false);
			return;
		}
		foreach (char c in text)
		{
			if (!char.IsDigit(c))
			{
				ConsoleSystem.Run("notice.popup 5  \"Must be digits only.\"", false);
				return;
			}
		}
		ConsoleNetworker.SendCommandToServer("lockentry.passwordentry " + text + " " + ((!this.changingEntry) ? "false" : "true"));
		LockEntry.Hide();
	}

	// Token: 0x04001211 RID: 4625
	private static LockEntry singleton;

	// Token: 0x04001212 RID: 4626
	private UnlockCursorNode cursorLocker;

	// Token: 0x04001213 RID: 4627
	public dfTextbox passwordInput;

	// Token: 0x04001214 RID: 4628
	public dfRichTextLabel entryLabel;

	// Token: 0x04001215 RID: 4629
	private bool changingEntry;
}
