using System;
using Facepunch;

// Token: 0x0200043C RID: 1084
public interface IActivatable : IComponentInterface<IActivatable, MonoBehaviour, Activatable>, IComponentInterface<IActivatable, MonoBehaviour>, IComponentInterface<IActivatable>
{
	// Token: 0x060027FC RID: 10236
	ActivationResult ActTrigger(Character instigator, ulong timestamp);
}
