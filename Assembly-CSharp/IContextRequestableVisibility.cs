using System;
using Facepunch;

// Token: 0x02000526 RID: 1318
public interface IContextRequestableVisibility : global::IContextRequestable, global::IComponentInterface<global::IContextRequestable, MonoBehaviour, global::Contextual>, global::IComponentInterface<global::IContextRequestable, MonoBehaviour>, global::IComponentInterface<global::IContextRequestable>
{
	// Token: 0x06002C70 RID: 11376
	void OnContextVisibilityChanged(global::ContextSprite sprite, bool nowVisible);
}
