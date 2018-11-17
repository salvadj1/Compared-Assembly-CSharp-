using System;
using Facepunch;

// Token: 0x020004F7 RID: 1271
public interface IActivatableFill : global::IActivatable, global::IComponentInterface<global::IActivatable, MonoBehaviour, global::Activatable>, global::IComponentInterface<global::IActivatable, MonoBehaviour>, global::IComponentInterface<global::IActivatable>
{
	// Token: 0x06002B90 RID: 11152
	void ActivatableChanged(global::Activatable activatable, bool nonNull);
}
