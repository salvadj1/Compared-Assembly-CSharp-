using System;
using Facepunch;

// Token: 0x02000470 RID: 1136
public interface IContextRequestableVisibility : IContextRequestable, IComponentInterface<IContextRequestable, MonoBehaviour, Contextual>, IComponentInterface<IContextRequestable, MonoBehaviour>, IComponentInterface<IContextRequestable>
{
	// Token: 0x060028E0 RID: 10464
	void OnContextVisibilityChanged(ContextSprite sprite, bool nowVisible);
}
