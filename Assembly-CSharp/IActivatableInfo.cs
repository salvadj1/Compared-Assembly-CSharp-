using System;
using Facepunch;

// Token: 0x02000440 RID: 1088
public interface IActivatableInfo : IActivatable, IComponentInterface<IActivatable, MonoBehaviour, Activatable>, IComponentInterface<IActivatable, MonoBehaviour>, IComponentInterface<IActivatable>
{
	// Token: 0x060027FF RID: 10239
	void ActInfo(out ActivatableInfo info);
}
