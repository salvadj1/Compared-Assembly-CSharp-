using System;
using Facepunch;

// Token: 0x020004F6 RID: 1270
public interface IActivatableInfo : global::IActivatable, global::IComponentInterface<global::IActivatable, MonoBehaviour, global::Activatable>, global::IComponentInterface<global::IActivatable, MonoBehaviour>, global::IComponentInterface<global::IActivatable>
{
	// Token: 0x06002B8F RID: 11151
	void ActInfo(out global::ActivatableInfo info);
}
