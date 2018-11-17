using System;
using UnityEngine;

// Token: 0x02000423 RID: 1059
public class RPOSWorkbenchWindow : RPOSWindow
{
	// Token: 0x06002756 RID: 10070 RVA: 0x00099458 File Offset: 0x00097658
	public void SetWorkbench(WorkBench workbenchObj)
	{
		this._myWorkBench = workbenchObj;
		this.Initialize();
	}

	// Token: 0x06002757 RID: 10071 RVA: 0x00099468 File Offset: 0x00097668
	protected override void WindowAwake()
	{
		base.WindowAwake();
		UIEventListener uieventListener = UIEventListener.Get(this.actionButton.gameObject);
		UIEventListener uieventListener2 = uieventListener;
		uieventListener2.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uieventListener2.onClick, new UIEventListener.VoidDelegate(this.ActionButtonClicked));
		UIEventListener uieventListener3 = UIEventListener.Get(this.takeAllButton.gameObject);
		UIEventListener uieventListener4 = uieventListener3;
		uieventListener4.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uieventListener4.onClick, new UIEventListener.VoidDelegate(this.TakeAllButtonClicked));
	}

	// Token: 0x06002758 RID: 10072 RVA: 0x000994E4 File Offset: 0x000976E4
	public void Initialize()
	{
		RPOSInvCellManager componentInChildren = base.GetComponentInChildren<RPOSInvCellManager>();
		componentInChildren.SetInventory(this._myWorkBench.GetComponent<Inventory>(), false);
	}

	// Token: 0x06002759 RID: 10073 RVA: 0x0009950C File Offset: 0x0009770C
	protected override void OnWindowClosed()
	{
		base.OnWindowClosed();
		this.WorkbenchClosed();
	}

	// Token: 0x0600275A RID: 10074 RVA: 0x0009951C File Offset: 0x0009771C
	protected override void OnRPOSClosed()
	{
		base.OnRPOSClosed();
		this.WorkbenchClosed();
	}

	// Token: 0x0600275B RID: 10075 RVA: 0x0009952C File Offset: 0x0009772C
	public virtual void WorkbenchClosed()
	{
		if (this._myWorkBench)
		{
			this._myWorkBench.ClientClosedWorkbenchWindow();
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x0600275C RID: 10076 RVA: 0x00099560 File Offset: 0x00097760
	protected override void OnExternalClose()
	{
		this.WorkbenchClosed();
	}

	// Token: 0x0600275D RID: 10077 RVA: 0x00099568 File Offset: 0x00097768
	public void BenchUpdate()
	{
		if (!this._myWorkBench)
		{
			Debug.Log("NO BENCH!?!?!");
		}
		if (this._myWorkBench.IsWorking())
		{
			this.startSound.Play();
			this.actionButton.GetComponentInChildren<UILabel>().text = "Cancel";
			this.takeAllButton.enabled = false;
			this.SetCellsLocked(true);
		}
		else
		{
			this.actionButton.GetComponentInChildren<UILabel>().text = "Start";
			this.takeAllButton.enabled = true;
			this.SetCellsLocked(false);
			if (this._myWorkBench._inventory.IsSlotOccupied(12))
			{
				this.finishedSound.Play();
			}
		}
	}

	// Token: 0x0600275E RID: 10078 RVA: 0x00099624 File Offset: 0x00097824
	private void SetCellsLocked(bool isLocked)
	{
		RPOSInvCellManager componentInChildren = base.GetComponentInChildren<RPOSInvCellManager>();
		for (int i = 0; i < componentInChildren._inventoryCells.Length; i++)
		{
			RPOSInventoryCell rposinventoryCell = componentInChildren._inventoryCells[i];
			if (rposinventoryCell)
			{
				rposinventoryCell.SetItemLocked(isLocked);
			}
		}
	}

	// Token: 0x0600275F RID: 10079 RVA: 0x0009966C File Offset: 0x0009786C
	private void TakeAllButtonClicked(GameObject go)
	{
		this._myWorkBench.networkView.RPC("TakeAll", 0, new object[0]);
	}

	// Token: 0x06002760 RID: 10080 RVA: 0x0009968C File Offset: 0x0009788C
	private void ActionButtonClicked(GameObject go)
	{
		Debug.Log("Action button clicked");
		this._myWorkBench.networkView.RPC("DoAction", 0, new object[0]);
		Debug.Log("Action post");
	}

	// Token: 0x06002761 RID: 10081 RVA: 0x000996CC File Offset: 0x000978CC
	public void Update()
	{
		if (this._myWorkBench && this._myWorkBench.IsWorking())
		{
			this.percentLabel.enabled = true;
			this.progressBar.sliderValue = this._myWorkBench.GetFractionComplete();
			this.percentLabel.text = (Mathf.Clamp01(this._myWorkBench.GetFractionComplete()) * 100f).ToString("N0") + "%";
		}
		else
		{
			this.percentLabel.enabled = false;
			this.progressBar.sliderValue = 0f;
		}
	}

	// Token: 0x0400135D RID: 4957
	private WorkBench _myWorkBench;

	// Token: 0x0400135E RID: 4958
	public UIButton actionButton;

	// Token: 0x0400135F RID: 4959
	public UIButton takeAllButton;

	// Token: 0x04001360 RID: 4960
	public UISlider progressBar;

	// Token: 0x04001361 RID: 4961
	public UILabel percentLabel;

	// Token: 0x04001362 RID: 4962
	public AudioClip startSound;

	// Token: 0x04001363 RID: 4963
	public AudioClip finishedSound;
}
