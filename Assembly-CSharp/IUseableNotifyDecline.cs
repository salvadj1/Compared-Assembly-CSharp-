using System;
using Facepunch;

// Token: 0x02000229 RID: 553
public interface IUseableNotifyDecline : global::IUseable, global::IComponentInterface<global::IUseable, MonoBehaviour, global::Useable>, global::IComponentInterface<global::IUseable, MonoBehaviour>, global::IComponentInterface<global::IUseable>
{
	// Token: 0x06000EF9 RID: 3833
	void OnUseDeclined(global::Character user, global::UseResponse response, global::UseEnterRequest request);
}
