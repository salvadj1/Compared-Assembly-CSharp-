using System;

// Token: 0x020005DD RID: 1501
public abstract class MagazineItem<T> : InventoryItem<T> where T : MagazineDataBlock
{
	// Token: 0x060035FB RID: 13819 RVA: 0x000C356C File Offset: 0x000C176C
	protected MagazineItem(T db) : base(db)
	{
	}

	// Token: 0x17000AB7 RID: 2743
	// (get) Token: 0x060035FC RID: 13820 RVA: 0x000C3578 File Offset: 0x000C1778
	public int numEmptyBulletSlots
	{
		get
		{
			return this.maxUses - base.uses;
		}
	}

	// Token: 0x17000AB8 RID: 2744
	// (get) Token: 0x060035FD RID: 13821 RVA: 0x000C3588 File Offset: 0x000C1788
	public override string toolTip
	{
		get
		{
			int uses = base.uses;
			if (this.lastUsesStringCount != uses)
			{
				if (uses <= 0)
				{
					string str = "Empty ";
					T datablock = this.datablock;
					this.lastUsesString = str + datablock.name;
				}
				else
				{
					string format = "{0} ({1})";
					T datablock2 = this.datablock;
					this.lastUsesString = string.Format(format, datablock2.name, this.lastUsesStringCount);
				}
				this.lastUsesStringCount = new int?(uses);
			}
			return this.lastUsesString;
		}
	}

	// Token: 0x04001A9C RID: 6812
	private int? lastUsesStringCount;

	// Token: 0x04001A9D RID: 6813
	private string lastUsesString;
}
