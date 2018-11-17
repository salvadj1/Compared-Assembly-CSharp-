using System;

// Token: 0x0200069B RID: 1691
public abstract class MagazineItem<T> : global::InventoryItem<T> where T : global::MagazineDataBlock
{
	// Token: 0x060039C3 RID: 14787 RVA: 0x000CB7C8 File Offset: 0x000C99C8
	protected MagazineItem(T db) : base(db)
	{
	}

	// Token: 0x17000B2D RID: 2861
	// (get) Token: 0x060039C4 RID: 14788 RVA: 0x000CB7D4 File Offset: 0x000C99D4
	public int numEmptyBulletSlots
	{
		get
		{
			return this.maxUses - base.uses;
		}
	}

	// Token: 0x17000B2E RID: 2862
	// (get) Token: 0x060039C5 RID: 14789 RVA: 0x000CB7E4 File Offset: 0x000C99E4
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

	// Token: 0x04001C6D RID: 7277
	private int? lastUsesStringCount;

	// Token: 0x04001C6E RID: 7278
	private string lastUsesString;
}
