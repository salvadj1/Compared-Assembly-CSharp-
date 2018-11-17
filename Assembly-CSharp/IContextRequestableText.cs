using System;
using Facepunch;

// Token: 0x0200052A RID: 1322
public interface IContextRequestableText : global::IContextRequestable, global::IComponentInterface<global::IContextRequestable, MonoBehaviour, global::Contextual>, global::IComponentInterface<global::IContextRequestable, MonoBehaviour>, global::IComponentInterface<global::IContextRequestable>
{
	// Token: 0x06002C74 RID: 11380
	string ContextText(global::Controllable localControllable);
}
