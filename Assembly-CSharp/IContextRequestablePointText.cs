using System;
using Facepunch;
using UnityEngine;

// Token: 0x0200052E RID: 1326
public interface IContextRequestablePointText : global::IContextRequestable, global::IContextRequestableText, global::IComponentInterface<global::IContextRequestable, MonoBehaviour, global::Contextual>, global::IComponentInterface<global::IContextRequestable, MonoBehaviour>, global::IComponentInterface<global::IContextRequestable>
{
	// Token: 0x06002C77 RID: 11383
	bool ContextTextPoint(out Vector3 worldPoint);
}
