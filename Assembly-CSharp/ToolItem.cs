using System;

// Token: 0x020006A9 RID: 1705
public abstract class ToolItem<T> : global::InventoryItem<T> where T : global::ToolDataBlock
{
	// Token: 0x06003A08 RID: 14856 RVA: 0x000CC7E4 File Offset: 0x000CA9E4
	protected ToolItem(T db) : base(db)
	{
	}

	// Token: 0x17000B3E RID: 2878
	// (get) Token: 0x06003A09 RID: 14857 RVA: 0x000CC7F0 File Offset: 0x000CA9F0
	public virtual bool canWork
	{
		get
		{
			T datablock = this.datablock;
			return datablock.CanWork(this.iface as global::IToolItem, base.inventory);
		}
	}

	// Token: 0x06003A0A RID: 14858 RVA: 0x000CC824 File Offset: 0x000CAA24
	public virtual void StartWork()
	{
	}

	// Token: 0x06003A0B RID: 14859 RVA: 0x000CC828 File Offset: 0x000CAA28
	public virtual void CancelWork()
	{
	}

	// Token: 0x06003A0C RID: 14860 RVA: 0x000CC82C File Offset: 0x000CAA2C
	public virtual void CompleteWork()
	{
		T datablock = this.datablock;
		datablock.CompleteWork(this.iface as global::IToolItem, base.inventory);
	}

	// Token: 0x17000B3F RID: 2879
	// (get) Token: 0x06003A0D RID: 14861 RVA: 0x000CC860 File Offset: 0x000CAA60
	public virtual float workDuration
	{
		get
		{
			T datablock = this.datablock;
			return datablock.GetWorkDuration(this.iface as global::IToolItem);
		}
	}
}
