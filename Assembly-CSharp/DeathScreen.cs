using System;
using Facepunch.Cursor;
using UnityEngine;

// Token: 0x020003EF RID: 1007
public class DeathScreen : MonoBehaviour
{
	// Token: 0x0600252F RID: 9519 RVA: 0x0008EF34 File Offset: 0x0008D134
	private void Start()
	{
		DeathScreen.singleton = this;
		this.Hide();
	}

	// Token: 0x06002530 RID: 9520 RVA: 0x0008EF44 File Offset: 0x0008D144
	private void OnDestroy()
	{
		this.cursorLocker.Dispose();
		this.cursorLocker = null;
	}

	// Token: 0x06002531 RID: 9521 RVA: 0x0008EF58 File Offset: 0x0008D158
	public static void Show()
	{
		if (DeathScreen.singleton == null)
		{
			return;
		}
		DeathScreen.singleton.CancelInvoke("Hide");
		DeathScreen.singleton.Hide();
		DeathScreen.singleton.gameObject.GetComponent<dfPanel>().Show();
		DeathScreen.singleton.lblDescription.Text = deathscreen.reason;
		DeathScreen.singleton.gameObject.SetActive(true);
		DeathScreen.singleton.IntroAnimations();
		deathscreen.reason = string.Empty;
	}

	// Token: 0x06002532 RID: 9522 RVA: 0x0008EFDC File Offset: 0x0008D1DC
	public void IntroAnimations()
	{
		dfTweenComponentBase[] componentsInChildren = base.gameObject.GetComponentsInChildren<dfTweenComponentBase>();
		foreach (dfTweenComponentBase dfTweenComponentBase in componentsInChildren)
		{
			if (!(dfTweenComponentBase.TweenName != "FadeIn"))
			{
				dfTweenComponentBase.Play();
			}
		}
	}

	// Token: 0x06002533 RID: 9523 RVA: 0x0008F030 File Offset: 0x0008D230
	public void OutroAnimations()
	{
		dfTweenComponentBase[] componentsInChildren = base.gameObject.GetComponentsInChildren<dfTweenComponentBase>();
		foreach (dfTweenComponentBase dfTweenComponentBase in componentsInChildren)
		{
			if (!(dfTweenComponentBase.TweenName != "FadeOut"))
			{
				dfTweenComponentBase.Play();
			}
		}
		this.cursorLocker.On = false;
		base.Invoke("Hide", 5f);
	}

	// Token: 0x06002534 RID: 9524 RVA: 0x0008F0A0 File Offset: 0x0008D2A0
	public void Hide()
	{
		base.gameObject.GetComponent<dfPanel>().Hide();
		base.gameObject.SetActive(false);
	}

	// Token: 0x06002535 RID: 9525 RVA: 0x0008F0CC File Offset: 0x0008D2CC
	public void OnDisable()
	{
		if (this.cursorLocker)
		{
			this.cursorLocker.On = false;
		}
	}

	// Token: 0x06002536 RID: 9526 RVA: 0x0008F0EC File Offset: 0x0008D2EC
	public void OnEnable()
	{
		if (!this.cursorLocker)
		{
			this.cursorLocker = LockCursorManager.CreateCursorUnlockNode(false, "Death Screen");
		}
		this.cursorLocker.On = true;
	}

	// Token: 0x06002537 RID: 9527 RVA: 0x0008F11C File Offset: 0x0008D31C
	public void RequestRespawn()
	{
		this.OutroAnimations();
		ServerManagement.Get().networkView.RPC<bool>("RequestRespawn", 0, false);
	}

	// Token: 0x06002538 RID: 9528 RVA: 0x0008F148 File Offset: 0x0008D348
	public void RequestRespawn_InCamp()
	{
		this.OutroAnimations();
		ServerManagement.Get().networkView.RPC<bool>("RequestRespawn", 0, true);
	}

	// Token: 0x0400120D RID: 4621
	public dfLabel lblDescription;

	// Token: 0x0400120E RID: 4622
	private static DeathScreen singleton;

	// Token: 0x0400120F RID: 4623
	private UnlockCursorNode cursorLocker;
}
