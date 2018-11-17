using System;
using Facepunch;

// Token: 0x0200043E RID: 1086
public interface IActivatableToggle : IActivatable, IComponentInterface<IActivatable, MonoBehaviour, Activatable>, IComponentInterface<IActivatable, MonoBehaviour>, IComponentInterface<IActivatable>
{
	// Token: 0x060027FD RID: 10237
	ActivationResult ActTrigger(Character instigator, ActivationToggleState toggleTarget, ulong timestamp);

	// Token: 0x060027FE RID: 10238
	ActivationToggleState ActGetToggleState();
}
