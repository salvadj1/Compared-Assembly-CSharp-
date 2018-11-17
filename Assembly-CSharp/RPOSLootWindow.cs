using System;
using UnityEngine;

// Token: 0x020004CE RID: 1230
public class RPOSLootWindow : global::RPOSWindowScrollable
{
	// Token: 0x06002A52 RID: 10834 RVA: 0x0009D498 File Offset: 0x0009B698
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
			global::UIEventListener uieventListener = global::UIEventListener.Get(this.TakeAllButton.gameObject);
			global::UIEventListener uieventListener2 = uieventListener;
			uieventListener2.onClick = (global::UIEventListener.VoidDelegate)Delegate.Combine(uieventListener2.onClick, new global::UIEventListener.VoidDelegate(this.TakeAllButtonClicked));
		}
	}

	// Token: 0x06002A53 RID: 10835 RVA: 0x0009D518 File Offset: 0x0009B718
	public virtual void SetLootable(global::LootableObject lootable, bool doInit)
	{
		this.myLootable = lootable;
		this.Initialize();
	}

	// Token: 0x06002A54 RID: 10836 RVA: 0x0009D528 File Offset: 0x0009B728
	public void TakeAllButtonClicked(GameObject go)
	{
		global::RPOS.ChangeRPOSMode(false);
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06002A55 RID: 10837 RVA: 0x0009D53C File Offset: 0x0009B73C
	public void Initialize()
	{
		global::RPOSInvCellManager componentInChildren = base.GetComponentInChildren<global::RPOSInvCellManager>();
		componentInChildren.SetInventory(this.myLootable.GetComponent<global::Inventory>(), true);
		base.ResetScrolling();
	}

	// Token: 0x06002A56 RID: 10838 RVA: 0x0009D568 File Offset: 0x0009B768
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

	// Token: 0x06002A57 RID: 10839 RVA: 0x0009D5A4 File Offset: 0x0009B7A4
	protected override void OnRPOSClosed()
	{
		base.OnRPOSClosed();
		this.LootClosed();
	}

	// Token: 0x06002A58 RID: 10840 RVA: 0x0009D5B4 File Offset: 0x0009B7B4
	public virtual void LootClosed()
	{
		if (this.myLootable)
		{
			this.myLootable.ClientClosedLootWindow();
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06002A59 RID: 10841 RVA: 0x0009D5E8 File Offset: 0x0009B7E8
	protected override void OnExternalClose()
	{
		this.LootClosed();
	}

	// Token: 0x0400148C RID: 5260
	[NonSerialized]
	public global::LootableObject myLootable;

	// Token: 0x0400148D RID: 5261
	[NonSerialized]
	public bool initalized;

	// Token: 0x0400148E RID: 5262
	public global::UIButton TakeAllButton;
}
