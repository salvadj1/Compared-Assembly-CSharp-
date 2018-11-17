using System;
using Facepunch;

// Token: 0x020004F4 RID: 1268
public interface IActivatableToggle : global::IActivatable, global::IComponentInterface<global::IActivatable, MonoBehaviour, global::Activatable>, global::IComponentInterface<global::IActivatable, MonoBehaviour>, global::IComponentInterface<global::IActivatable>
{
	// Token: 0x06002B8D RID: 11149
	global::ActivationResult ActTrigger(global::Character instigator, global::ActivationToggleState toggleTarget, ulong timestamp);

	// Token: 0x06002B8E RID: 11150
	global::ActivationToggleState ActGetToggleState();
}
