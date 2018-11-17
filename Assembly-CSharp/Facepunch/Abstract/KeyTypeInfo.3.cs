using System;

namespace Facepunch.Abstract
{
	// Token: 0x020001E1 RID: 481
	internal static class KeyTypeInfo<Key, T> where Key : TraitKey where T : Key
	{
		// Token: 0x06000D40 RID: 3392 RVA: 0x00033900 File Offset: 0x00031B00
		static KeyTypeInfo()
		{
			if (typeof(T) == typeof(Key))
			{
				throw new KeyArgumentIsKeyTypeException("<T>", "You cannot use GetTrait<Key>. Must use a types inheriting Key");
			}
			KeyTypeInfo<Key, T>.Info = KeyTypeInfo<Key>.Registration.GetUnsafe(typeof(T));
		}

		// Token: 0x04000825 RID: 2085
		internal static readonly KeyTypeInfo<Key> Info;
	}
}
