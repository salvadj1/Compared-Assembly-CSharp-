using System;
using System.Collections;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x0200066F RID: 1647
[RequireComponent(typeof(Inventory))]
[NGCAutoAddScript]
public class WorkBench : IDMain, IUseable, IContextRequestable, IContextRequestableQuick, IContextRequestableText, IComponentInterface<IUseable, MonoBehaviour, Useable>, IComponentInterface<IUseable, MonoBehaviour>, IComponentInterface<IUseable>, IComponentInterface<IContextRequestable, MonoBehaviour, Contextual>, IComponentInterface<IContextRequestable, MonoBehaviour>, IComponentInterface<IContextRequestable>
{
	// Token: 0x06003922 RID: 14626 RVA: 0x000D222C File Offset: 0x000D042C
	public WorkBench() : base(0)
	{
	}

	// Token: 0x17000B26 RID: 2854
	// (get) Token: 0x06003924 RID: 14628 RVA: 0x000D2244 File Offset: 0x000D0444
	private static bool debug_workbench
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06003925 RID: 14629 RVA: 0x000D2248 File Offset: 0x000D0448
	public static void LogError<T>(T a, Object b)
	{
		if (WorkBench.debug_workbench)
		{
			Debug.LogError(a, b);
		}
	}

	// Token: 0x06003926 RID: 14630 RVA: 0x000D2260 File Offset: 0x000D0460
	public static void LogWarning<T>(T a, Object b)
	{
		if (WorkBench.debug_workbench)
		{
			Debug.LogWarning(a, b);
		}
	}

	// Token: 0x06003927 RID: 14631 RVA: 0x000D2278 File Offset: 0x000D0478
	public static void Log<T>(T a, Object b)
	{
		if (WorkBench.debug_workbench)
		{
			Debug.Log(a, b);
		}
	}

	// Token: 0x06003928 RID: 14632 RVA: 0x000D2290 File Offset: 0x000D0490
	public static void LogError<T>(T a)
	{
		if (WorkBench.debug_workbench)
		{
			Debug.LogError(a);
		}
	}

	// Token: 0x06003929 RID: 14633 RVA: 0x000D22A8 File Offset: 0x000D04A8
	public static void LogWarning<T>(T a)
	{
		if (WorkBench.debug_workbench)
		{
			Debug.LogWarning(a);
		}
	}

	// Token: 0x0600392A RID: 14634 RVA: 0x000D22C0 File Offset: 0x000D04C0
	public static void Log<T>(T a)
	{
		if (WorkBench.debug_workbench)
		{
			Debug.Log(a);
		}
	}

	// Token: 0x0600392B RID: 14635 RVA: 0x000D22D8 File Offset: 0x000D04D8
	protected void Awake()
	{
		this.SharedAwake();
	}

	// Token: 0x0600392C RID: 14636 RVA: 0x000D22E0 File Offset: 0x000D04E0
	private void SharedAwake()
	{
		this._inventory = base.GetComponent<Inventory>();
	}

	// Token: 0x0600392D RID: 14637 RVA: 0x000D22F0 File Offset: 0x000D04F0
	public void OnUseEnter(Useable use)
	{
	}

	// Token: 0x0600392E RID: 14638 RVA: 0x000D22F4 File Offset: 0x000D04F4
	public void OnUseExit(Useable use, UseExitReason reason)
	{
	}

	// Token: 0x0600392F RID: 14639 RVA: 0x000D22F8 File Offset: 0x000D04F8
	[RPC]
	private void SetUser(NetworkPlayer ply)
	{
		if (ply == NetCull.player)
		{
			RPOS.OpenWorkbenchWindow(this);
		}
		else if (this._currentlyUsingPlayer == NetCull.player && ply != this._currentlyUsingPlayer)
		{
			this._currentlyUsingPlayer = NetworkPlayer.unassigned;
			RPOS.CloseWorkbenchWindow();
		}
		this._currentlyUsingPlayer = ply;
	}

	// Token: 0x06003930 RID: 14640 RVA: 0x000D2360 File Offset: 0x000D0560
	[RPC]
	private void StopUsing(NetworkMessageInfo info)
	{
		if (this._currentlyUsingPlayer == info.sender)
		{
			this._useable.Eject();
		}
	}

	// Token: 0x06003931 RID: 14641 RVA: 0x000D2390 File Offset: 0x000D0590
	public void ClientClosedWorkbenchWindow()
	{
		if (this.IsLocalUsing())
		{
			NetCull.RPC(this, "StopUsing", 0);
		}
	}

	// Token: 0x06003932 RID: 14642 RVA: 0x000D23AC File Offset: 0x000D05AC
	public ContextExecution ContextQuery(Controllable controllable, ulong timestamp)
	{
		return ContextExecution.Quick;
	}

	// Token: 0x06003933 RID: 14643 RVA: 0x000D23B0 File Offset: 0x000D05B0
	public ContextResponse ContextRespondQuick(Controllable controllable, ulong timestamp)
	{
		return ContextRequestable.UseableForwardFromContextRespond(this, controllable, this._useable);
	}

	// Token: 0x06003934 RID: 14644 RVA: 0x000D23C0 File Offset: 0x000D05C0
	public string ContextText(Controllable localControllable)
	{
		if (this._currentlyUsingPlayer == NetworkPlayer.unassigned)
		{
			return "Use";
		}
		if (this._currentlyUsingPlayer != NetCull.player)
		{
			return "Occupied";
		}
		return string.Empty;
	}

	// Token: 0x06003935 RID: 14645 RVA: 0x000D2400 File Offset: 0x000D0600
	public void RadialCheck()
	{
		if (this._useable.user && Vector3.Distance(this._useable.user.transform.position, base.transform.position) > 5f)
		{
			this._useable.Eject();
			base.CancelInvoke("RadialCheck");
		}
	}

	// Token: 0x06003936 RID: 14646 RVA: 0x000D2468 File Offset: 0x000D0668
	public bool IsLocalUsing()
	{
		return this._currentlyUsingPlayer == NetCull.player;
	}

	// Token: 0x06003937 RID: 14647 RVA: 0x000D247C File Offset: 0x000D067C
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

	// Token: 0x06003938 RID: 14648 RVA: 0x000D249C File Offset: 0x000D069C
	private void StartWork()
	{
		if (!this.EnsureWorkExists())
		{
			return;
		}
		IToolItem tool = this.GetTool();
		if (tool == null)
		{
			return;
		}
		this._startTime_network = NetCull.time;
		this._workDuration = this.GetWorkDuration();
		base.Invoke("CompleteWork", this._workDuration);
		this._inventory.locked = true;
		tool.StartWork();
		this.SendWorkStatusUpdate();
	}

	// Token: 0x06003939 RID: 14649 RVA: 0x000D2504 File Offset: 0x000D0704
	private void SendWorkStatusUpdate()
	{
		if (this._currentlyUsingPlayer == NetworkPlayer.unassigned)
		{
			return;
		}
		float p = (float)this._startTime_network;
		NetCull.RPC<float, float>(this, "WorkStatusUpdate", this._currentlyUsingPlayer, p, this._workDuration);
	}

	// Token: 0x0600393A RID: 14650 RVA: 0x000D2548 File Offset: 0x000D0748
	[RPC]
	private void WorkStatusUpdate(float startTime, float newWorkDuration)
	{
		this._startTime_network = (double)startTime;
		this._workDuration = newWorkDuration;
		RPOSWorkbenchWindow component = RPOS.GetWindowByName("Workbench").GetComponent<RPOSWorkbenchWindow>();
		component.BenchUpdate();
	}

	// Token: 0x0600393B RID: 14651 RVA: 0x000D257C File Offset: 0x000D077C
	public void TryCancel()
	{
		this.CancelWork();
	}

	// Token: 0x0600393C RID: 14652 RVA: 0x000D2584 File Offset: 0x000D0784
	public void CancelWork()
	{
		IToolItem tool = this.GetTool();
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

	// Token: 0x0600393D RID: 14653 RVA: 0x000D25DC File Offset: 0x000D07DC
	public void CompleteWork()
	{
	}

	// Token: 0x0600393E RID: 14654 RVA: 0x000D25E0 File Offset: 0x000D07E0
	public bool EnsureWorkExists()
	{
		IToolItem tool = this.GetTool();
		return tool != null && tool.canWork;
	}

	// Token: 0x0600393F RID: 14655 RVA: 0x000D2608 File Offset: 0x000D0808
	public virtual bool HasTool()
	{
		return this.GetTool() != null;
	}

	// Token: 0x06003940 RID: 14656 RVA: 0x000D2618 File Offset: 0x000D0818
	public virtual IToolItem GetTool()
	{
		return this._inventory.FindItemType<IToolItem>();
	}

	// Token: 0x06003941 RID: 14657 RVA: 0x000D2634 File Offset: 0x000D0834
	public virtual BlueprintDataBlock GetMatchingDBForItems()
	{
		ArrayList arrayList = new ArrayList();
		foreach (ItemDataBlock itemDataBlock in DatablockDictionary.All)
		{
			if (itemDataBlock is BlueprintDataBlock)
			{
				BlueprintDataBlock blueprintDataBlock = itemDataBlock as BlueprintDataBlock;
				bool flag = true;
				foreach (BlueprintDataBlock.IngredientEntry ingredientEntry in blueprintDataBlock.ingredients)
				{
					int num = 0;
					IInventoryItem inventoryItem = this._inventory.FindItem(ingredientEntry.Ingredient, out num);
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
			BlueprintDataBlock result = null;
			int num2 = -1;
			foreach (object obj in arrayList)
			{
				BlueprintDataBlock blueprintDataBlock2 = (BlueprintDataBlock)obj;
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

	// Token: 0x06003942 RID: 14658 RVA: 0x000D2784 File Offset: 0x000D0984
	[RPC]
	public void TakeAll()
	{
	}

	// Token: 0x06003943 RID: 14659 RVA: 0x000D2788 File Offset: 0x000D0988
	public bool IsWorking()
	{
		return this._workDuration != -1f;
	}

	// Token: 0x06003944 RID: 14660 RVA: 0x000D279C File Offset: 0x000D099C
	public double GetTimePassed()
	{
		if (this._workDuration == -1f)
		{
			return -1.0;
		}
		return NetCull.time - this._startTime_network;
	}

	// Token: 0x06003945 RID: 14661 RVA: 0x000D27D0 File Offset: 0x000D09D0
	public float GetFractionComplete()
	{
		if (!this.IsWorking())
		{
			return 0f;
		}
		return (float)(this.GetTimePassed() / (double)this._workDuration);
	}

	// Token: 0x06003946 RID: 14662 RVA: 0x000D2800 File Offset: 0x000D0A00
	public float GetWorkDuration()
	{
		IToolItem tool = this.GetTool();
		if (tool != null)
		{
			return tool.workDuration;
		}
		return 0f;
	}

	// Token: 0x04001D4A RID: 7498
	[HideInInspector]
	public Inventory _inventory;

	// Token: 0x04001D4B RID: 7499
	private Useable _useable;

	// Token: 0x04001D4C RID: 7500
	private NetworkPlayer _currentlyUsingPlayer;

	// Token: 0x04001D4D RID: 7501
	private double _startTime_network;

	// Token: 0x04001D4E RID: 7502
	private float _workDuration = -1f;

	// Token: 0x04001D4F RID: 7503
	private static bool _debug_workbench;
}
