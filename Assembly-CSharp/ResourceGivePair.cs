using System;
using UnityEngine;

// Token: 0x020004B7 RID: 1207
[Serializable]
public class ResourceGivePair
{
	// Token: 0x17000967 RID: 2407
	// (get) Token: 0x06002A37 RID: 10807 RVA: 0x000A57A0 File Offset: 0x000A39A0
	public ItemDataBlock ResourceItemDataBlock
	{
		get
		{
			if (!this._setResourceItemDatablock)
			{
				this._resourceItemDatablock = this.ResourceItemName;
				this._setResourceItemDatablock = true;
			}
			return (ItemDataBlock)this._resourceItemDatablock.datablock;
		}
	}

	// Token: 0x06002A38 RID: 10808 RVA: 0x000A57D8 File Offset: 0x000A39D8
	public void CalcAmount()
	{
		this.realAmount = Random.Range(this.amountMin, this.amountMax + 1);
	}

	// Token: 0x06002A39 RID: 10809 RVA: 0x000A57F4 File Offset: 0x000A39F4
	public bool AnyLeft()
	{
		return this.realAmount > 0;
	}

	// Token: 0x06002A3A RID: 10810 RVA: 0x000A5800 File Offset: 0x000A3A00
	public int AmountLeft()
	{
		return this.realAmount;
	}

	// Token: 0x06002A3B RID: 10811 RVA: 0x000A5808 File Offset: 0x000A3A08
	public void Subtract(int amount)
	{
		this.realAmount -= amount;
	}

	// Token: 0x04001630 RID: 5680
	[NonSerialized]
	private Datablock.Ident _resourceItemDatablock;

	// Token: 0x04001631 RID: 5681
	[NonSerialized]
	private bool _setResourceItemDatablock;

	// Token: 0x04001632 RID: 5682
	public string ResourceItemName = string.Empty;

	// Token: 0x04001633 RID: 5683
	public int amountMin;

	// Token: 0x04001634 RID: 5684
	public int amountMax;

	// Token: 0x04001635 RID: 5685
	private int realAmount;
}
