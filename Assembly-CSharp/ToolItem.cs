using System;

// Token: 0x020005EB RID: 1515
public abstract class ToolItem<T> : InventoryItem<T> where T : ToolDataBlock
{
	// Token: 0x06003640 RID: 13888 RVA: 0x000C4588 File Offset: 0x000C2788
	protected ToolItem(T db) : base(db)
	{
	}

	// Token: 0x17000AC8 RID: 2760
	// (get) Token: 0x06003641 RID: 13889 RVA: 0x000C4594 File Offset: 0x000C2794
	public virtual bool canWork
	{
		get
		{
			T datablock = this.datablock;
			return datablock.CanWork(this.iface as IToolItem, base.inventory);
		}
	}

	// Token: 0x06003642 RID: 13890 RVA: 0x000C45C8 File Offset: 0x000C27C8
	public virtual void StartWork()
	{
	}

	// Token: 0x06003643 RID: 13891 RVA: 0x000C45CC File Offset: 0x000C27CC
	public virtual void CancelWork()
	{
	}

	// Token: 0x06003644 RID: 13892 RVA: 0x000C45D0 File Offset: 0x000C27D0
	public virtual void CompleteWork()
	{
		T datablock = this.datablock;
		datablock.CompleteWork(this.iface as IToolItem, base.inventory);
	}

	// Token: 0x17000AC9 RID: 2761
	// (get) Token: 0x06003645 RID: 13893 RVA: 0x000C4604 File Offset: 0x000C2804
	public virtual float workDuration
	{
		get
		{
			T datablock = this.datablock;
			return datablock.GetWorkDuration(this.iface as IToolItem);
		}
	}
}
