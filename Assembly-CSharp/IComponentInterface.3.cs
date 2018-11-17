using System;
using UnityEngine;

// Token: 0x020001E9 RID: 489
public interface IComponentInterface<InterfaceType, MonoBehaviourType> : global::IComponentInterface<InterfaceType> where InterfaceType : global::IComponentInterface<InterfaceType, MonoBehaviourType> where MonoBehaviourType : MonoBehaviour
{
}
