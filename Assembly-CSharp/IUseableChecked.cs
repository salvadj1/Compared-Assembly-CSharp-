using System;
using Facepunch;

// Token: 0x020001F5 RID: 501
public interface IUseableChecked : IUseable, IComponentInterface<IUseable, MonoBehaviour, Useable>, IComponentInterface<IUseable, MonoBehaviour>, IComponentInterface<IUseable>
{
	// Token: 0x06000DA4 RID: 3492
	UseCheck CanUse(Character user, UseEnterRequest request);
}
