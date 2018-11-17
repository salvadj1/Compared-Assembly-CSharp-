using System;

// Token: 0x020005BB RID: 1467
public abstract class BlueprintItem<T> : ToolItem<T> where T : BlueprintDataBlock
{
	// Token: 0x06003504 RID: 13572 RVA: 0x000C16A0 File Offset: 0x000BF8A0
	protected BlueprintItem(T db) : base(db)
	{
	}

	// Token: 0x17000A69 RID: 2665
	// (get) Token: 0x06003505 RID: 13573 RVA: 0x000C16AC File Offset: 0x000BF8AC
	public override float workDuration
	{
		get
		{
			T datablock = this.datablock;
			return datablock.GetWorkDuration(this.iface as IToolItem);
		}
	}
}
