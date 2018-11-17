using System;
using Facepunch;
using UnityEngine;

// Token: 0x02000478 RID: 1144
public interface IContextRequestablePointText : IContextRequestable, IContextRequestableText, IComponentInterface<IContextRequestable, MonoBehaviour, Contextual>, IComponentInterface<IContextRequestable, MonoBehaviour>, IComponentInterface<IContextRequestable>
{
	// Token: 0x060028E7 RID: 10471
	bool ContextTextPoint(out Vector3 worldPoint);
}
