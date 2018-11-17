using System;
using Facepunch;

// Token: 0x02000228 RID: 552
public interface IUseableChecked : global::IUseable, global::IComponentInterface<global::IUseable, MonoBehaviour, global::Useable>, global::IComponentInterface<global::IUseable, MonoBehaviour>, global::IComponentInterface<global::IUseable>
{
	// Token: 0x06000EF8 RID: 3832
	global::UseCheck CanUse(global::Character user, global::UseEnterRequest request);
}
