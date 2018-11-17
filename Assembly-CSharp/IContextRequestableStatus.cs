using System;
using Facepunch;

// Token: 0x02000473 RID: 1139
public interface IContextRequestableStatus : IContextRequestable, IComponentInterface<IContextRequestable, MonoBehaviour, Contextual>, IComponentInterface<IContextRequestable, MonoBehaviour>, IComponentInterface<IContextRequestable>
{
	// Token: 0x060028E3 RID: 10467
	ContextStatusFlags ContextStatusPoll();
}
