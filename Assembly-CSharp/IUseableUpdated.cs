using System;
using Facepunch;

// Token: 0x02000225 RID: 549
public interface IUseableUpdated : global::IUseable, global::IComponentInterface<global::IUseable, MonoBehaviour, global::Useable>, global::IComponentInterface<global::IUseable, MonoBehaviour>, global::IComponentInterface<global::IUseable>
{
	// Token: 0x17000395 RID: 917
	// (get) Token: 0x06000EF5 RID: 3829
	global::UseUpdateFlags UseUpdateFlags { get; }

	// Token: 0x06000EF6 RID: 3830
	void OnUseUpdate(global::Useable use);
}
