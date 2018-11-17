using System;
using Facepunch;

// Token: 0x0200052B RID: 1323
public interface IContextRequestableUpdatingText : global::IContextRequestable, global::IContextRequestableText, global::IComponentInterface<global::IContextRequestable, MonoBehaviour, global::Contextual>, global::IComponentInterface<global::IContextRequestable, MonoBehaviour>, global::IComponentInterface<global::IContextRequestable>
{
	// Token: 0x06002C75 RID: 11381
	string ContextTextUpdate(global::Controllable localControllable, string lastText);
}
