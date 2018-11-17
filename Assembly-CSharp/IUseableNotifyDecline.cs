using System;
using Facepunch;

// Token: 0x020001F6 RID: 502
public interface IUseableNotifyDecline : IUseable, IComponentInterface<IUseable, MonoBehaviour, Useable>, IComponentInterface<IUseable, MonoBehaviour>, IComponentInterface<IUseable>
{
	// Token: 0x06000DA5 RID: 3493
	void OnUseDeclined(Character user, UseResponse response, UseEnterRequest request);
}
