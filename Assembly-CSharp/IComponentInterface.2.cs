using System;

// Token: 0x020001B8 RID: 440
public interface IComponentInterface<InterfaceType> where InterfaceType : IComponentInterface<InterfaceType>
{
}
