using System;
using Facepunch;

// Token: 0x02000474 RID: 1140
public interface IContextRequestableText : IContextRequestable, IComponentInterface<IContextRequestable, MonoBehaviour, Contextual>, IComponentInterface<IContextRequestable, MonoBehaviour>, IComponentInterface<IContextRequestable>
{
	// Token: 0x060028E4 RID: 10468
	string ContextText(Controllable localControllable);
}
