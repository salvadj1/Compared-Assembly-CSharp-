using System;
using UnityEngine;

// Token: 0x02000419 RID: 1049
public class RPOSLootWindow : RPOSWindowScrollable
{
	// Token: 0x060026C8 RID: 9928 RVA: 0x000975D4 File Offset: 0x000957D4
	protected override void WindowAwake()
	{
		this.autoResetScrolling = false;
		base.WindowAwake();
		if (!this.initalized && this.myLootable)
		{
			this.Initialize();
		}
		if (this.TakeAllButton)
		{
			UIEventListener uieventListener = UIEventListener.Get(this.TakeAllButton.gameObject);
			UIEventListener uieventListener2 = uieventListener;
			uieventListener2.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uieventListener2.onClick, new UIEventListener.VoidDelegate(this.TakeAllButtonClicked));
		}
	}

	// Token: 0x060026C9 RID: 9929 RVA: 0x00097654 File Offset: 0x00095854
	public virtual void SetLootable(LootableObject lootable, bool doInit)
	{
		this.myLootable = lootable;
		this.Initialize();
	}

	// Token: 0x060026CA RID: 9930 RVA: 0x00097664 File Offset: 0x00095864
	public void TakeAllButtonClicked(GameObject go)
	{
		RPOS.ChangeRPOSMode(false);
		Object.Destroy(base.gameObject);
	}

	// Token: 0x060026CB RID: 9931 RVA: 0x00097678 File Offset: 0x00095878
	public void Initialize()
	{
		RPOSInvCellManager componentInChildren = base.GetComponentInChildren<RPOSInvCellManager>();
		componentInChildren.SetInventory(this.myLootable.GetComponent<Inventory>(), true);
		base.ResetScrolling();
	}

	// Token: 0x060026CC RID: 9932 RVA: 0x000976A4 File Offset: 0x000958A4
	protected override void OnWindowHide()
	{
		try
		{
			base.OnWindowHide();
		}
		finally
		{
			this.LootClosed();
		}
	}

	// Token: 0x060026CD RID: 9933 RVA: 0x000976E0 File Offset: 0x000958E0
	protected override void OnRPOSClosed()
	{
		base.OnRPOSClosed();
		this.LootClosed();
	}

	// Token: 0x060026CE RID: 9934 RVA: 0x000976F0 File Offset: 0x000958F0
	public virtual void LootClosed()
	{
		if (this.myLootable)
		{
			this.myLootable.ClientClosedLootWindow();
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x060026CF RID: 9935 RVA: 0x00097724 File Offset: 0x00095924
	protected override void OnExternalClose()
	{
		this.LootClosed();
	}

	// Token: 0x0400130C RID: 4876
	[NonSerialized]
	public LootableObject myLootable;

	// Token: 0x0400130D RID: 4877
	[NonSerialized]
	public bool initalized;

	// Token: 0x0400130E RID: 4878
	public UIButton TakeAllButton;
}
