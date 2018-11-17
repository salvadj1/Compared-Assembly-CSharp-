using System;
using UnityEngine;

// Token: 0x02000572 RID: 1394
[Serializable]
public class ResourceGivePair
{
	// Token: 0x170009D7 RID: 2519
	// (get) Token: 0x06002DE9 RID: 11753 RVA: 0x000AD538 File Offset: 0x000AB738
	public global::ItemDataBlock ResourceItemDataBlock
	{
		get
		{
			if (!this._setResourceItemDatablock)
			{
				this._resourceItemDatablock = this.ResourceItemName;
				this._setResourceItemDatablock = true;
			}
			return (global::ItemDataBlock)this._resourceItemDatablock.datablock;
		}
	}

	// Token: 0x06002DEA RID: 11754 RVA: 0x000AD570 File Offset: 0x000AB770
	public void CalcAmount()
	{
		this.realAmount = Random.Range(this.amountMin, this.amountMax + 1);
	}

	// Token: 0x06002DEB RID: 11755 RVA: 0x000AD58C File Offset: 0x000AB78C
	public bool AnyLeft()
	{
		return this.realAmount > 0;
	}

	// Token: 0x06002DEC RID: 11756 RVA: 0x000AD598 File Offset: 0x000AB798
	public int AmountLeft()
	{
		return this.realAmount;
	}

	// Token: 0x06002DED RID: 11757 RVA: 0x000AD5A0 File Offset: 0x000AB7A0
	public void Subtract(int amount)
	{
		this.realAmount -= amount;
	}

	// Token: 0x040017ED RID: 6125
	[NonSerialized]
	private global::Datablock.Ident _resourceItemDatablock;

	// Token: 0x040017EE RID: 6126
	[NonSerialized]
	private bool _setResourceItemDatablock;

	// Token: 0x040017EF RID: 6127
	public string ResourceItemName = string.Empty;

	// Token: 0x040017F0 RID: 6128
	public int amountMin;

	// Token: 0x040017F1 RID: 6129
	public int amountMax;

	// Token: 0x040017F2 RID: 6130
	private int realAmount;
}
