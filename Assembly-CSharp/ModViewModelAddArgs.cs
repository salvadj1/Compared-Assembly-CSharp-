using System;

// Token: 0x02000648 RID: 1608
public struct ModViewModelAddArgs
{
	// Token: 0x060035CE RID: 13774 RVA: 0x000C4BE4 File Offset: 0x000C2DE4
	public ModViewModelAddArgs(global::ViewModel vm, global::IHeldItem item, bool isMesh, global::ItemModRepresentation modRep)
	{
		this.vm = vm;
		this.item = item;
		this.isMesh = isMesh;
		this.modRep = modRep;
	}

	// Token: 0x060035CF RID: 13775 RVA: 0x000C4C04 File Offset: 0x000C2E04
	public ModViewModelAddArgs(global::ViewModel vm, global::IHeldItem item, bool isMesh)
	{
		this = new global::ModViewModelAddArgs(vm, item, isMesh, null);
	}

	// Token: 0x04001BB9 RID: 7097
	public readonly global::ViewModel vm;

	// Token: 0x04001BBA RID: 7098
	public global::ItemModRepresentation modRep;

	// Token: 0x04001BBB RID: 7099
	public readonly global::IHeldItem item;

	// Token: 0x04001BBC RID: 7100
	public readonly bool isMesh;
}
