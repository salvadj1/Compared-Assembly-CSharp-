using System;
using Facepunch;

// Token: 0x020001F2 RID: 498
public interface IUseableUpdated : IUseable, IComponentInterface<IUseable, MonoBehaviour, Useable>, IComponentInterface<IUseable, MonoBehaviour>, IComponentInterface<IUseable>
{
	// Token: 0x1700034D RID: 845
	// (get) Token: 0x06000DA1 RID: 3489
	UseUpdateFlags UseUpdateFlags { get; }

	// Token: 0x06000DA2 RID: 3490
	void OnUseUpdate(Useable use);
}
