using System;
using Facepunch;

// Token: 0x02000475 RID: 1141
public interface IContextRequestableUpdatingText : IContextRequestable, IContextRequestableText, IComponentInterface<IContextRequestable, MonoBehaviour, Contextual>, IComponentInterface<IContextRequestable, MonoBehaviour>, IComponentInterface<IContextRequestable>
{
	// Token: 0x060028E5 RID: 10469
	string ContextTextUpdate(Controllable localControllable, string lastText);
}
