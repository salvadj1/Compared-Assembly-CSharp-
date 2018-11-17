using System;

// Token: 0x02000649 RID: 1609
public struct ModViewModelRemoveArgs
{
	// Token: 0x060035D0 RID: 13776 RVA: 0x000C4C10 File Offset: 0x000C2E10
	public ModViewModelRemoveArgs(global::ViewModel vm, global::IHeldItem item, global::ItemModRepresentation modRep)
	{
		this.vm = vm;
		this.item = item;
		this.modRep = modRep;
	}

	// Token: 0x060035D1 RID: 13777 RVA: 0x000C4C28 File Offset: 0x000C2E28
	public ModViewModelRemoveArgs(global::ViewModel vm, global::IHeldItem item)
	{
		this = new global::ModViewModelRemoveArgs(vm, item, null);
	}

	// Token: 0x04001BBD RID: 7101
	public readonly global::ViewModel vm;

	// Token: 0x04001BBE RID: 7102
	public global::ItemModRepresentation modRep;

	// Token: 0x04001BBF RID: 7103
	public readonly global::IHeldItem item;
}
