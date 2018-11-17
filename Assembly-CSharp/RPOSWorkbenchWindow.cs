using System;
using UnityEngine;

// Token: 0x020004D9 RID: 1241
public class RPOSWorkbenchWindow : global::RPOSWindow
{
	// Token: 0x06002AE6 RID: 10982 RVA: 0x0009F3D8 File Offset: 0x0009D5D8
	public void SetWorkbench(global::WorkBench workbenchObj)
	{
		this._myWorkBench = workbenchObj;
		this.Initialize();
	}

	// Token: 0x06002AE7 RID: 10983 RVA: 0x0009F3E8 File Offset: 0x0009D5E8
	protected override void WindowAwake()
	{
		base.WindowAwake();
		global::UIEventListener uieventListener = global::UIEventListener.Get(this.actionButton.gameObject);
		global::UIEventListener uieventListener2 = uieventListener;
		uieventListener2.onClick = (global::UIEventListener.VoidDelegate)Delegate.Combine(uieventListener2.onClick, new global::UIEventListener.VoidDelegate(this.ActionButtonClicked));
		global::UIEventListener uieventListener3 = global::UIEventListener.Get(this.takeAllButton.gameObject);
		global::UIEventListener uieventListener4 = uieventListener3;
		uieventListener4.onClick = (global::UIEventListener.VoidDelegate)Delegate.Combine(uieventListener4.onClick, new global::UIEventListener.VoidDelegate(this.TakeAllButtonClicked));
	}

	// Token: 0x06002AE8 RID: 10984 RVA: 0x0009F464 File Offset: 0x0009D664
	public void Initialize()
	{
		global::RPOSInvCellManager componentInChildren = base.GetComponentInChildren<global::RPOSInvCellManager>();
		componentInChildren.SetInventory(this._myWorkBench.GetComponent<global::Inventory>(), false);
	}

	// Token: 0x06002AE9 RID: 10985 RVA: 0x0009F48C File Offset: 0x0009D68C
	protected override void OnWindowClosed()
	{
		base.OnWindowClosed();
		this.WorkbenchClosed();
	}

	// Token: 0x06002AEA RID: 10986 RVA: 0x0009F49C File Offset: 0x0009D69C
	protected override void OnRPOSClosed()
	{
		base.OnRPOSClosed();
		this.WorkbenchClosed();
	}

	// Token: 0x06002AEB RID: 10987 RVA: 0x0009F4AC File Offset: 0x0009D6AC
	public virtual void WorkbenchClosed()
	{
		if (this._myWorkBench)
		{
			this._myWorkBench.ClientClosedWorkbenchWindow();
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06002AEC RID: 10988 RVA: 0x0009F4E0 File Offset: 0x0009D6E0
	protected override void OnExternalClose()
	{
		this.WorkbenchClosed();
	}

	// Token: 0x06002AED RID: 10989 RVA: 0x0009F4E8 File Offset: 0x0009D6E8
	public void BenchUpdate()
	{
		if (!this._myWorkBench)
		{
			Debug.Log("NO BENCH!?!?!");
		}
		if (this._myWorkBench.IsWorking())
		{
			this.startSound.Play();
			this.actionButton.GetComponentInChildren<global::UILabel>().text = "Cancel";
			this.takeAllButton.enabled = false;
			this.SetCellsLocked(true);
		}
		else
		{
			this.actionButton.GetComponentInChildren<global::UILabel>().text = "Start";
			this.takeAllButton.enabled = true;
			this.SetCellsLocked(false);
			if (this._myWorkBench._inventory.IsSlotOccupied(12))
			{
				this.finishedSound.Play();
			}
		}
	}

	// Token: 0x06002AEE RID: 10990 RVA: 0x0009F5A4 File Offset: 0x0009D7A4
	private void SetCellsLocked(bool isLocked)
	{
		global::RPOSInvCellManager componentInChildren = base.GetComponentInChildren<global::RPOSInvCellManager>();
		for (int i = 0; i < componentInChildren._inventoryCells.Length; i++)
		{
			global::RPOSInventoryCell rposinventoryCell = componentInChildren._inventoryCells[i];
			if (rposinventoryCell)
			{
				rposinventoryCell.SetItemLocked(isLocked);
			}
		}
	}

	// Token: 0x06002AEF RID: 10991 RVA: 0x0009F5EC File Offset: 0x0009D7EC
	private void TakeAllButtonClicked(GameObject go)
	{
		this._myWorkBench.networkView.RPC("TakeAll", 0, new object[0]);
	}

	// Token: 0x06002AF0 RID: 10992 RVA: 0x0009F60C File Offset: 0x0009D80C
	private void ActionButtonClicked(GameObject go)
	{
		Debug.Log("Action button clicked");
		this._myWorkBench.networkView.RPC("DoAction", 0, new object[0]);
		Debug.Log("Action post");
	}

	// Token: 0x06002AF1 RID: 10993 RVA: 0x0009F64C File Offset: 0x0009D84C
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

	// Token: 0x040014E0 RID: 5344
	private global::WorkBench _myWorkBench;

	// Token: 0x040014E1 RID: 5345
	public global::UIButton actionButton;

	// Token: 0x040014E2 RID: 5346
	public global::UIButton takeAllButton;

	// Token: 0x040014E3 RID: 5347
	public global::UISlider progressBar;

	// Token: 0x040014E4 RID: 5348
	public global::UILabel percentLabel;

	// Token: 0x040014E5 RID: 5349
	public AudioClip startSound;

	// Token: 0x040014E6 RID: 5350
	public AudioClip finishedSound;
}
