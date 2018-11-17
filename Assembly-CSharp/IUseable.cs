using System;
using Facepunch;

// Token: 0x020001F0 RID: 496
public interface IUseable : IComponentInterface<IUseable, MonoBehaviour, Useable>, IComponentInterface<IUseable, MonoBehaviour>, IComponentInterface<IUseable>
{
	// Token: 0x06000D9F RID: 3487
	void OnUseEnter(Useable use);

	// Token: 0x06000DA0 RID: 3488
	void OnUseExit(Useable use, UseExitReason reason);
}
