using System;
using Facepunch;

// Token: 0x02000529 RID: 1321
public interface IContextRequestableStatus : global::IContextRequestable, global::IComponentInterface<global::IContextRequestable, MonoBehaviour, global::Contextual>, global::IComponentInterface<global::IContextRequestable, MonoBehaviour>, global::IComponentInterface<global::IContextRequestable>
{
	// Token: 0x06002C73 RID: 11379
	global::ContextStatusFlags ContextStatusPoll();
}
