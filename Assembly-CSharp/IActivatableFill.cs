using System;
using Facepunch;

// Token: 0x02000441 RID: 1089
public interface IActivatableFill : IActivatable, IComponentInterface<IActivatable, MonoBehaviour, Activatable>, IComponentInterface<IActivatable, MonoBehaviour>, IComponentInterface<IActivatable>
{
	// Token: 0x06002800 RID: 10240
	void ActivatableChanged(Activatable activatable, bool nonNull);
}
