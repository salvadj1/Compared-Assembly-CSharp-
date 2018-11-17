using System;

// Token: 0x0200058A RID: 1418
public struct ModViewModelAddArgs
{
	// Token: 0x06003206 RID: 12806 RVA: 0x000BC988 File Offset: 0x000BAB88
	public ModViewModelAddArgs(ViewModel vm, IHeldItem item, bool isMesh, ItemModRepresentation modRep)
	{
		this.vm = vm;
		this.item = item;
		this.isMesh = isMesh;
		this.modRep = modRep;
	}

	// Token: 0x06003207 RID: 12807 RVA: 0x000BC9A8 File Offset: 0x000BABA8
	public ModViewModelAddArgs(ViewModel vm, IHeldItem item, bool isMesh)
	{
		this = new ModViewModelAddArgs(vm, item, isMesh, null);
	}

	// Token: 0x040019E8 RID: 6632
	public readonly ViewModel vm;

	// Token: 0x040019E9 RID: 6633
	public ItemModRepresentation modRep;

	// Token: 0x040019EA RID: 6634
	public readonly IHeldItem item;

	// Token: 0x040019EB RID: 6635
	public readonly bool isMesh;
}
