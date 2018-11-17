using System;
using UnityEngine;

// Token: 0x020001BB RID: 443
public interface IComponentInterfaceDriver<InterfaceType, MonoBehaviourType, InterfaceDriverType> where InterfaceType : IComponentInterface<InterfaceType, MonoBehaviourType, InterfaceDriverType> where MonoBehaviourType : MonoBehaviour where InterfaceDriverType : MonoBehaviour, IComponentInterfaceDriver<InterfaceType, MonoBehaviourType, InterfaceDriverType>
{
	// Token: 0x1700031B RID: 795
	// (get) Token: 0x06000C65 RID: 3173
	MonoBehaviourType implementor { get; }

	// Token: 0x1700031C RID: 796
	// (get) Token: 0x06000C66 RID: 3174
	InterfaceType @interface { get; }

	// Token: 0x1700031D RID: 797
	// (get) Token: 0x06000C67 RID: 3175
	bool exists { get; }

	// Token: 0x1700031E RID: 798
	// (get) Token: 0x06000C68 RID: 3176
	InterfaceDriverType driver { get; }
}
