using System;
using System.Collections;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x02000733 RID: 1843
[RequireComponent(typeof(global::Inventory))]
[global::NGCAutoAddScript]
public class WorkBench : IDMain, global::IUseable, global::IContextRequestable, global::IContextRequestableQuick, global::IContextRequestableText, global::IComponentInterface<global::IUseable, MonoBehaviour, global::Useable>, global::IComponentInterface<global::IUseable, MonoBehaviour>, global::IComponentInterface<global::IUseable>, global::IComponentInterface<global::IContextRequestable, MonoBehaviour, global::Contextual>, global::IComponentInterface<global::IContextRequestable, MonoBehaviour>, global::IComponentInterface<global::IContextRequestable>
{
	// Token: 0x06003D16 RID: 15638 RVA: 0x000DAC0C File Offset: 0x000D8E0C
	public WorkBench() : base(0)
	{
	}

	// Token: 0x17000BA8 RID: 2984
	// (get) Token: 0x06003D18 RID: 15640 RVA: 0x000DAC24 File Offset: 0x000D8E24
	private static bool debug_workbench
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06003D19 RID: 15641 RVA: 0x000DAC28 File Offset: 0x000D8E28
	public static void LogError<T>(T a, Object b)
	{
		if (global::WorkBench.debug_workbench)
		{
			Debug.LogError(a, b);
		}
	}

	// Token: 0x06003D1A RID: 15642 RVA: 0x000DAC40 File Offset: 0x000D8E40
	public static void LogWarning<T>(T a, Object b)
	{
		if (global::WorkBench.debug_workbench)
		{
			Debug.LogWarning(a, b);
		}
	}

	// Token: 0x06003D1B RID: 15643 RVA: 0x000DAC58 File Offset: 0x000D8E58
	public static void Log<T>(T a, Object b)
	{
		if (global::WorkBench.debug_workbench)
		{
			Debug.Log(a, b);
		}
	}

	// Token: 0x06003D1C RID: 15644 RVA: 0x000DAC70 File Offset: 0x000D8E70
	public static void LogError<T>(T a)
	{
		if (global::WorkBench.debug_workbench)
		{
			Debug.LogError(a);
		}
	}

	// Token: 0x06003D1D RID: 15645 RVA: 0x000DAC88 File Offset: 0x000D8E88
	public static void LogWarning<T>(T a)
	{
		if (global::WorkBench.debug_workbench)
		{
			Debug.LogWarning(a);
		}
	}

	// Token: 0x06003D1E RID: 15646 RVA: 0x000DACA0 File Offset: 0x000D8EA0
	public static void Log<T>(T a)
	{
		if (global::WorkBench.debug_workbench)
		{
			Debug.Log(a);
		}
	}

	// Token: 0x06003D1F RID: 15647 RVA: 0x000DACB8 File Offset: 0x000D8EB8
	protected void Awake()
	{
		this.SharedAwake();
	}

	// Token: 0x06003D20 RID: 15648 RVA: 0x000DACC0 File Offset: 0x000D8EC0
	private void SharedAwake()
	{
		this._inventory = base.GetComponent<global::Inventory>();
	}

	// Token: 0x06003D21 RID: 15649 RVA: 0x000DACD0 File Offset: 0x000D8ED0
	public void OnUseEnter(global::Useable use)
	{
	}

	// Token: 0x06003D22 RID: 15650 RVA: 0x000DACD4 File Offset: 0x000D8ED4
	public void OnUseExit(global::Useable use, global::UseExitReason reason)
	{
	}

	// Token: 0x06003D23 RID: 15651 RVA: 0x000DACD8 File Offset: 0x000D8ED8
	[RPC]
	private void SetUser(uLink.NetworkPlayer ply)
	{
		if (ply == global::NetCull.player)
		{
			global::RPOS.OpenWorkbenchWindow(this);
		}
		else if (this._currentlyUsingPlayer == global::NetCull.player && ply != this._currentlyUsingPlayer)
		{
			this._currentlyUsingPlayer = uLink.NetworkPlayer.unassigned;
			global::RPOS.CloseWorkbenchWindow();
		}
		this._currentlyUsingPlayer = ply;
	}

	// Token: 0x06003D24 RID: 15652 RVA: 0x000DAD40 File Offset: 0x000D8F40
	[RPC]
	private void StopUsing(uLink.NetworkMessageInfo info)
	{
		if (this._currentlyUsingPlayer == info.sender)
		{
			this._useable.Eject();
		}
	}

	// Token: 0x06003D25 RID: 15653 RVA: 0x000DAD70 File Offset: 0x000D8F70
	public void ClientClosedWorkbenchWindow()
	{
		if (this.IsLocalUsing())
		{
			global::NetCull.RPC(this, "StopUsing", 0);
		}
	}

	// Token: 0x06003D26 RID: 15654 RVA: 0x000DAD8C File Offset: 0x000D8F8C
	public global::ContextExecution ContextQuery(global::Controllable controllable, ulong timestamp)
	{
		return global::ContextExecution.Quick;
	}

	// Token: 0x06003D27 RID: 15655 RVA: 0x000DAD90 File Offset: 0x000D8F90
	public global::ContextResponse ContextRespondQuick(global::Controllable controllable, ulong timestamp)
	{
		return global::ContextRequestable.UseableForwardFromContextRespond(this, controllable, this._useable);
	}

	// Token: 0x06003D28 RID: 15656 RVA: 0x000DADA0 File Offset: 0x000D8FA0
	public string ContextText(global::Controllable localControllable)
	{
		if (this._currentlyUsingPlayer == uLink.NetworkPlayer.unassigned)
		{
			return "Use";
		}
		if (this._currentlyUsingPlayer != global::NetCull.player)
		{
			return "Occupied";
		}
		return string.Empty;
	}

	// Token: 0x06003D29 RID: 15657 RVA: 0x000DADE0 File Offset: 0x000D8FE0
	public void RadialCheck()
	{
		if (this._useable.user && Vector3.Distance(this._useable.user.transform.position, base.transform.position) > 5f)
		{
			this._useable.Eject();
			base.CancelInvoke("RadialCheck");
		}
	}

	// Token: 0x06003D2A RID: 15658 RVA: 0x000DAE48 File Offset: 0x000D9048
	public bool IsLocalUsing()
	{
		return this._currentlyUsingPlayer == global::NetCull.player;
	}

	// Token: 0x06003D2B RID: 15659 RVA: 0x000DAE5C File Offset: 0x000D905C
	[RPC]
	private void DoAction()
	{
		if (this.IsWorking())
		{
			this.TryCancel();
		}
		else
		{
			this.StartWork();
		}
	}

	// Token: 0x06003D2C RID: 15660 RVA: 0x000DAE7C File Offset: 0x000D907C
	private void StartWork()
	{
		if (!this.EnsureWorkExists())
		{
			return;
		}
		global::IToolItem tool = this.GetTool();
		if (tool == null)
		{
			return;
		}
		this._startTime_network = global::NetCull.time;
		this._workDuration = this.GetWorkDuration();
		base.Invoke("CompleteWork", this._workDuration);
		this._inventory.locked = true;
		tool.StartWork();
		this.SendWorkStatusUpdate();
	}

	// Token: 0x06003D2D RID: 15661 RVA: 0x000DAEE4 File Offset: 0x000D90E4
	private void SendWorkStatusUpdate()
	{
		if (this._currentlyUsingPlayer == uLink.NetworkPlayer.unassigned)
		{
			return;
		}
		float p = (float)this._startTime_network;
		global::NetCull.RPC<float, float>(this, "WorkStatusUpdate", this._currentlyUsingPlayer, p, this._workDuration);
	}

	// Token: 0x06003D2E RID: 15662 RVA: 0x000DAF28 File Offset: 0x000D9128
	[RPC]
	private void WorkStatusUpdate(float startTime, float newWorkDuration)
	{
		this._startTime_network = (double)startTime;
		this._workDuration = newWorkDuration;
		global::RPOSWorkbenchWindow component = global::RPOS.GetWindowByName("Workbench").GetComponent<global::RPOSWorkbenchWindow>();
		component.BenchUpdate();
	}

	// Token: 0x06003D2F RID: 15663 RVA: 0x000DAF5C File Offset: 0x000D915C
	public void TryCancel()
	{
		this.CancelWork();
	}

	// Token: 0x06003D30 RID: 15664 RVA: 0x000DAF64 File Offset: 0x000D9164
	public void CancelWork()
	{
		global::IToolItem tool = this.GetTool();
		if (tool != null)
		{
			tool.CancelWork();
		}
		this._inventory.locked = false;
		this._startTime_network = 0.0;
		this._workDuration = -1f;
		base.CancelInvoke("CompleteWork");
		this.SendWorkStatusUpdate();
	}

	// Token: 0x06003D31 RID: 15665 RVA: 0x000DAFBC File Offset: 0x000D91BC
	public void CompleteWork()
	{
	}

	// Token: 0x06003D32 RID: 15666 RVA: 0x000DAFC0 File Offset: 0x000D91C0
	public bool EnsureWorkExists()
	{
		global::IToolItem tool = this.GetTool();
		return tool != null && tool.canWork;
	}

	// Token: 0x06003D33 RID: 15667 RVA: 0x000DAFE8 File Offset: 0x000D91E8
	public virtual bool HasTool()
	{
		return this.GetTool() != null;
	}

	// Token: 0x06003D34 RID: 15668 RVA: 0x000DAFF8 File Offset: 0x000D91F8
	public virtual global::IToolItem GetTool()
	{
		return this._inventory.FindItemType<global::IToolItem>();
	}

	// Token: 0x06003D35 RID: 15669 RVA: 0x000DB014 File Offset: 0x000D9214
	public virtual global::BlueprintDataBlock GetMatchingDBForItems()
	{
		ArrayList arrayList = new ArrayList();
		foreach (global::ItemDataBlock itemDataBlock in global::DatablockDictionary.All)
		{
			if (itemDataBlock is global::BlueprintDataBlock)
			{
				global::BlueprintDataBlock blueprintDataBlock = itemDataBlock as global::BlueprintDataBlock;
				bool flag = true;
				foreach (global::BlueprintDataBlock.IngredientEntry ingredientEntry in blueprintDataBlock.ingredients)
				{
					int num = 0;
					global::IInventoryItem inventoryItem = this._inventory.FindItem(ingredientEntry.Ingredient, out num);
					if (inventoryItem == null || num < ingredientEntry.amount)
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					arrayList.Add(blueprintDataBlock);
				}
			}
		}
		if (arrayList.Count > 0)
		{
			global::BlueprintDataBlock result = null;
			int num2 = -1;
			foreach (object obj in arrayList)
			{
				global::BlueprintDataBlock blueprintDataBlock2 = (global::BlueprintDataBlock)obj;
				if (blueprintDataBlock2.ingredients.Length > num2)
				{
					result = blueprintDataBlock2;
					num2 = blueprintDataBlock2.ingredients.Length;
				}
			}
			return result;
		}
		return null;
	}

	// Token: 0x06003D36 RID: 15670 RVA: 0x000DB164 File Offset: 0x000D9364
	[RPC]
	public void TakeAll()
	{
	}

	// Token: 0x06003D37 RID: 15671 RVA: 0x000DB168 File Offset: 0x000D9368
	public bool IsWorking()
	{
		return this._workDuration != -1f;
	}

	// Token: 0x06003D38 RID: 15672 RVA: 0x000DB17C File Offset: 0x000D937C
	public double GetTimePassed()
	{
		if (this._workDuration == -1f)
		{
			return -1.0;
		}
		return global::NetCull.time - this._startTime_network;
	}

	// Token: 0x06003D39 RID: 15673 RVA: 0x000DB1B0 File Offset: 0x000D93B0
	public float GetFractionComplete()
	{
		if (!this.IsWorking())
		{
			return 0f;
		}
		return (float)(this.GetTimePassed() / (double)this._workDuration);
	}

	// Token: 0x06003D3A RID: 15674 RVA: 0x000DB1E0 File Offset: 0x000D93E0
	public float GetWorkDuration()
	{
		global::IToolItem tool = this.GetTool();
		if (tool != null)
		{
			return tool.workDuration;
		}
		return 0f;
	}

	// Token: 0x04001F42 RID: 8002
	[HideInInspector]
	public global::Inventory _inventory;

	// Token: 0x04001F43 RID: 8003
	private global::Useable _useable;

	// Token: 0x04001F44 RID: 8004
	private uLink.NetworkPlayer _currentlyUsingPlayer;

	// Token: 0x04001F45 RID: 8005
	private double _startTime_network;

	// Token: 0x04001F46 RID: 8006
	private float _workDuration = -1f;

	// Token: 0x04001F47 RID: 8007
	private static bool _debug_workbench;
}
