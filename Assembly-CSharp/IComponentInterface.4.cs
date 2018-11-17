using System;
using UnityEngine;

// Token: 0x020001EA RID: 490
public interface IComponentInterface<InterfaceType, MonoBehaviourType, InterfaceDriverType> : global::IComponentInterface<InterfaceType, MonoBehaviourType>, global::IComponentInterface<InterfaceType> where InterfaceType : global::IComponentInterface<InterfaceType, MonoBehaviourType, InterfaceDriverType> where MonoBehaviourType : MonoBehaviour where InterfaceDriverType : MonoBehaviour, global::IComponentInterfaceDriver<InterfaceType, MonoBehaviourType, InterfaceDriverType>
{
}
