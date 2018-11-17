using System;
using UnityEngine;

// Token: 0x020001B9 RID: 441
public interface IComponentInterface<InterfaceType, MonoBehaviourType> : IComponentInterface<InterfaceType> where InterfaceType : IComponentInterface<InterfaceType, MonoBehaviourType> where MonoBehaviourType : MonoBehaviour
{
}
