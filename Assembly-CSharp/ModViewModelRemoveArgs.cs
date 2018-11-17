using System;

// Token: 0x0200058B RID: 1419
public struct ModViewModelRemoveArgs
{
	// Token: 0x06003208 RID: 12808 RVA: 0x000BC9B4 File Offset: 0x000BABB4
	public ModViewModelRemoveArgs(ViewModel vm, IHeldItem item, ItemModRepresentation modRep)
	{
		this.vm = vm;
		this.item = item;
		this.modRep = modRep;
	}

	// Token: 0x06003209 RID: 12809 RVA: 0x000BC9CC File Offset: 0x000BABCC
	public ModViewModelRemoveArgs(ViewModel vm, IHeldItem item)
	{
		this = new ModViewModelRemoveArgs(vm, item, null);
	}

	// Token: 0x040019EC RID: 6636
	public readonly ViewModel vm;

	// Token: 0x040019ED RID: 6637
	public ItemModRepresentation modRep;

	// Token: 0x040019EE RID: 6638
	public readonly IHeldItem item;
}
