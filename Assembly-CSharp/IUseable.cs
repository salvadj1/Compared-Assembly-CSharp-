using System;
using Facepunch;

// Token: 0x02000223 RID: 547
public interface IUseable : global::IComponentInterface<global::IUseable, MonoBehaviour, global::Useable>, global::IComponentInterface<global::IUseable, MonoBehaviour>, global::IComponentInterface<global::IUseable>
{
	// Token: 0x06000EF3 RID: 3827
	void OnUseEnter(global::Useable use);

	// Token: 0x06000EF4 RID: 3828
	void OnUseExit(global::Useable use, global::UseExitReason reason);
}
