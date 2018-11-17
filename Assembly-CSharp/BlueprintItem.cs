using System;

// Token: 0x02000679 RID: 1657
public abstract class BlueprintItem<T> : global::ToolItem<T> where T : global::BlueprintDataBlock
{
	// Token: 0x060038CC RID: 14540 RVA: 0x000C98FC File Offset: 0x000C7AFC
	protected BlueprintItem(T db) : base(db)
	{
	}

	// Token: 0x17000ADF RID: 2783
	// (get) Token: 0x060038CD RID: 14541 RVA: 0x000C9908 File Offset: 0x000C7B08
	public override float workDuration
	{
		get
		{
			T datablock = this.datablock;
			return datablock.GetWorkDuration(this.iface as global::IToolItem);
		}
	}
}
