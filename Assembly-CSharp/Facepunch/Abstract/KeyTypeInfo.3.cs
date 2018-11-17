using System;

namespace Facepunch.Abstract
{
	// Token: 0x02000212 RID: 530
	internal static class KeyTypeInfo<Key, T> where Key : global::TraitKey where T : Key
	{
		// Token: 0x06000E88 RID: 3720 RVA: 0x00037988 File Offset: 0x00035B88
		static KeyTypeInfo()
		{
			if (typeof(T) == typeof(Key))
			{
				throw new KeyArgumentIsKeyTypeException("<T>", "You cannot use GetTrait<Key>. Must use a types inheriting Key");
			}
			KeyTypeInfo<Key, T>.Info = KeyTypeInfo<Key>.Registration.GetUnsafe(typeof(T));
		}

		// Token: 0x0400093D RID: 2365
		internal static readonly KeyTypeInfo<Key> Info;
	}
}
