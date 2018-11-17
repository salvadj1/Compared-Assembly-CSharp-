using System;
using UnityEngine;

// Token: 0x020001BA RID: 442
public interface IComponentInterface<InterfaceType, MonoBehaviourType, InterfaceDriverType> : IComponentInterface<InterfaceType, MonoBehaviourType>, IComponentInterface<InterfaceType> where InterfaceType : IComponentInterface<InterfaceType, MonoBehaviourType, InterfaceDriverType> where MonoBehaviourType : MonoBehaviour where InterfaceDriverType : MonoBehaviour, IComponentInterfaceDriver<InterfaceType, MonoBehaviourType, InterfaceDriverType>
{
}
