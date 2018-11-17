using System;
using Facepunch;

// Token: 0x020004F2 RID: 1266
public interface IActivatable : global::IComponentInterface<global::IActivatable, MonoBehaviour, global::Activatable>, global::IComponentInterface<global::IActivatable, MonoBehaviour>, global::IComponentInterface<global::IActivatable>
{
	// Token: 0x06002B8C RID: 11148
	global::ActivationResult ActTrigger(global::Character instigator, ulong timestamp);
}
