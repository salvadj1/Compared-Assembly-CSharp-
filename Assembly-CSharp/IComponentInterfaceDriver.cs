using System;
using UnityEngine;

// Token: 0x020001EB RID: 491
public interface IComponentInterfaceDriver<InterfaceType, MonoBehaviourType, InterfaceDriverType> where InterfaceType : global::IComponentInterface<InterfaceType, MonoBehaviourType, InterfaceDriverType> where MonoBehaviourType : MonoBehaviour where InterfaceDriverType : MonoBehaviour, global::IComponentInterfaceDriver<InterfaceType, MonoBehaviourType, InterfaceDriverType>
{
	// Token: 0x1700035F RID: 863
	// (get) Token: 0x06000DA5 RID: 3493
	MonoBehaviourType implementor { get; }

	// Token: 0x17000360 RID: 864
	// (get) Token: 0x06000DA6 RID: 3494
	InterfaceType @interface { get; }

	// Token: 0x17000361 RID: 865
	// (get) Token: 0x06000DA7 RID: 3495
	bool exists { get; }

	// Token: 0x17000362 RID: 866
	// (get) Token: 0x06000DA8 RID: 3496
	InterfaceDriverType driver { get; }
}
