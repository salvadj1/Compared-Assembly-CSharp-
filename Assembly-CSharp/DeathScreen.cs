using System;
using Facepunch.Cursor;
using UnityEngine;

// Token: 0x0200049F RID: 1183
public class DeathScreen : MonoBehaviour
{
	// Token: 0x060028A1 RID: 10401 RVA: 0x00094920 File Offset: 0x00092B20
	private void Start()
	{
		global::DeathScreen.singleton = this;
		this.Hide();
	}

	// Token: 0x060028A2 RID: 10402 RVA: 0x00094930 File Offset: 0x00092B30
	private void OnDestroy()
	{
		this.cursorLocker.Dispose();
		this.cursorLocker = null;
	}

	// Token: 0x060028A3 RID: 10403 RVA: 0x00094944 File Offset: 0x00092B44
	public static void Show()
	{
		if (global::DeathScreen.singleton == null)
		{
			return;
		}
		global::DeathScreen.singleton.CancelInvoke("Hide");
		global::DeathScreen.singleton.Hide();
		global::DeathScreen.singleton.gameObject.GetComponent<global::dfPanel>().Show();
		global::DeathScreen.singleton.lblDescription.Text = global::deathscreen.reason;
		global::DeathScreen.singleton.gameObject.SetActive(true);
		global::DeathScreen.singleton.IntroAnimations();
		global::deathscreen.reason = string.Empty;
	}

	// Token: 0x060028A4 RID: 10404 RVA: 0x000949C8 File Offset: 0x00092BC8
	public void IntroAnimations()
	{
		global::dfTweenComponentBase[] componentsInChildren = base.gameObject.GetComponentsInChildren<global::dfTweenComponentBase>();
		foreach (global::dfTweenComponentBase dfTweenComponentBase in componentsInChildren)
		{
			if (!(dfTweenComponentBase.TweenName != "FadeIn"))
			{
				dfTweenComponentBase.Play();
			}
		}
	}

	// Token: 0x060028A5 RID: 10405 RVA: 0x00094A1C File Offset: 0x00092C1C
	public void OutroAnimations()
	{
		global::dfTweenComponentBase[] componentsInChildren = base.gameObject.GetComponentsInChildren<global::dfTweenComponentBase>();
		foreach (global::dfTweenComponentBase dfTweenComponentBase in componentsInChildren)
		{
			if (!(dfTweenComponentBase.TweenName != "FadeOut"))
			{
				dfTweenComponentBase.Play();
			}
		}
		this.cursorLocker.On = false;
		base.Invoke("Hide", 5f);
	}

	// Token: 0x060028A6 RID: 10406 RVA: 0x00094A8C File Offset: 0x00092C8C
	public void Hide()
	{
		base.gameObject.GetComponent<global::dfPanel>().Hide();
		base.gameObject.SetActive(false);
	}

	// Token: 0x060028A7 RID: 10407 RVA: 0x00094AB8 File Offset: 0x00092CB8
	public void OnDisable()
	{
		if (this.cursorLocker)
		{
			this.cursorLocker.On = false;
		}
	}

	// Token: 0x060028A8 RID: 10408 RVA: 0x00094AD8 File Offset: 0x00092CD8
	public void OnEnable()
	{
		if (!this.cursorLocker)
		{
			this.cursorLocker = LockCursorManager.CreateCursorUnlockNode(false, "Death Screen");
		}
		this.cursorLocker.On = true;
	}

	// Token: 0x060028A9 RID: 10409 RVA: 0x00094B08 File Offset: 0x00092D08
	public void RequestRespawn()
	{
		this.OutroAnimations();
		global::ServerManagement.Get().networkView.RPC<bool>("RequestRespawn", 0, false);
	}

	// Token: 0x060028AA RID: 10410 RVA: 0x00094B34 File Offset: 0x00092D34
	public void RequestRespawn_InCamp()
	{
		this.OutroAnimations();
		global::ServerManagement.Get().networkView.RPC<bool>("RequestRespawn", 0, true);
	}

	// Token: 0x04001387 RID: 4999
	public global::dfLabel lblDescription;

	// Token: 0x04001388 RID: 5000
	private static global::DeathScreen singleton;

	// Token: 0x04001389 RID: 5001
	private UnlockCursorNode cursorLocker;
}
