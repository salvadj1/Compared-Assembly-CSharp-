using System;
using System.Collections.Generic;

namespace Facepunch.Abstract
{
	// Token: 0x020001D9 RID: 473
	internal class KeyTypeInfo<Key> where Key : TraitKey
	{
		// Token: 0x06000D11 RID: 3345 RVA: 0x00033280 File Offset: 0x00031480
		private KeyTypeInfo(Type Type, KeyTypeInfo<Key> Base, KeyTypeInfo<Key> Root, int TraitDepth)
		{
			this.Type = Type;
			this.Base = Base;
			this.Root = (Root ?? this);
			this.TraitDepth = TraitDepth;
			if (this.Root == this)
			{
				this.AssignableTo = new HashSet<KeyTypeInfo<Key>>();
			}
			else
			{
				this.AssignableTo = new HashSet<KeyTypeInfo<Key>>(this.Base.AssignableTo);
			}
			this.AssignableTo.Add(this);
			KeyTypeInfo<Key>.Registration.Add(this);
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000D12 RID: 3346 RVA: 0x00033300 File Offset: 0x00031500
		public bool IsBaseTrait
		{
			get
			{
				return this.TraitDepth == 0;
			}
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x0003330C File Offset: 0x0003150C
		public static KeyTypeInfo<Key> Find(Type traitType)
		{
			if (!typeof(Key).IsAssignableFrom(traitType))
			{
				throw new ArgumentOutOfRangeException("traitType", "Must be a type assignable to Key");
			}
			if (traitType == typeof(Key))
			{
				throw new KeyArgumentIsKeyTypeException("You cannot use GetTrait(typeof(Key). Must use a types inheriting Key");
			}
			return KeyTypeInfo<Key>.Registration.GetUnsafe(traitType);
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x00033360 File Offset: 0x00031560
		public static bool Find(Type traitType, out KeyTypeInfo<Key> info)
		{
			if (typeof(Key).IsAssignableFrom(traitType) && traitType != typeof(Key))
			{
				info = null;
				return false;
			}
			info = KeyTypeInfo<Key>.Registration.GetUnsafe(traitType);
			return true;
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x00033398 File Offset: 0x00031598
		public static KeyTypeInfo<Key> Find<T>() where T : Key
		{
			return KeyTypeInfo<Key, T>.Info;
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x000333A0 File Offset: 0x000315A0
		public static bool Find<T>(out KeyTypeInfo<Key> info) where T : Key
		{
			bool result;
			try
			{
				info = KeyTypeInfo<Key, T>.Info;
				result = true;
			}
			catch (KeyArgumentIsKeyTypeException)
			{
				info = null;
				result = false;
			}
			return result;
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x000333F0 File Offset: 0x000315F0
		public bool IsAssignableFrom(KeyTypeInfo<Key> info)
		{
			return info.Root == this.Root && info.TraitDepth >= this.TraitDepth && info.AssignableTo.Contains(this);
		}

		// Token: 0x0400081C RID: 2076
		public readonly Type Type;

		// Token: 0x0400081D RID: 2077
		public readonly KeyTypeInfo<Key> Base;

		// Token: 0x0400081E RID: 2078
		public readonly KeyTypeInfo<Key> Root;

		// Token: 0x0400081F RID: 2079
		public readonly int TraitDepth;

		// Token: 0x04000820 RID: 2080
		public readonly HashSet<KeyTypeInfo<Key>> AssignableTo;

		// Token: 0x020001DA RID: 474
		internal static class Registration
		{
			// Token: 0x06000D19 RID: 3353 RVA: 0x00033430 File Offset: 0x00031630
			public static void Add(KeyTypeInfo<Key> info)
			{
				KeyTypeInfo<Key>.Registration.dict.Add(info.Type, info);
			}

			// Token: 0x06000D1A RID: 3354 RVA: 0x00033444 File Offset: 0x00031644
			public static KeyTypeInfo<Key> GetUnsafe(Type type)
			{
				KeyTypeInfo<Key> result;
				if (KeyTypeInfo<Key>.Registration.dict.TryGetValue(type, out result))
				{
					return result;
				}
				Type baseType = type.BaseType;
				if (typeof(Key) == baseType)
				{
					return new KeyTypeInfo<Key>(type, null, null, 0);
				}
				KeyTypeInfo<Key> @unsafe = KeyTypeInfo<Key>.Registration.GetUnsafe(baseType);
				return new KeyTypeInfo<Key>(type, @unsafe, @unsafe.Root, @unsafe.TraitDepth + 1);
			}

			// Token: 0x04000821 RID: 2081
			private static readonly Dictionary<Type, KeyTypeInfo<Key>> dict = new Dictionary<Type, KeyTypeInfo<Key>>();
		}

		// Token: 0x020001DB RID: 475
		public static class Comparison
		{
			// Token: 0x17000340 RID: 832
			// (get) Token: 0x06000D1B RID: 3355 RVA: 0x000334A4 File Offset: 0x000316A4
			public static IEqualityComparer<KeyTypeInfo<Key>> EqualityComparer
			{
				get
				{
					return KeyTypeInfo<Key>.Comparison.RootEqualityComparer.Singleton.Instance;
				}
			}

			// Token: 0x17000341 RID: 833
			// (get) Token: 0x06000D1C RID: 3356 RVA: 0x000334AC File Offset: 0x000316AC
			public static IComparer<KeyTypeInfo<Key>> Comparer
			{
				get
				{
					return KeyTypeInfo<Key>.Comparison.HierarchyComparer.Singleton.Instance;
				}
			}

			// Token: 0x020001DC RID: 476
			private class RootEqualityComparer : EqualityComparer<KeyTypeInfo<Key>>
			{
				// Token: 0x06000D1D RID: 3357 RVA: 0x000334B4 File Offset: 0x000316B4
				private RootEqualityComparer()
				{
				}

				// Token: 0x06000D1E RID: 3358 RVA: 0x000334BC File Offset: 0x000316BC
				public override bool Equals(KeyTypeInfo<Key> x, KeyTypeInfo<Key> y)
				{
					return x.Root == y.Root;
				}

				// Token: 0x06000D1F RID: 3359 RVA: 0x000334CC File Offset: 0x000316CC
				public override int GetHashCode(KeyTypeInfo<Key> obj)
				{
					return obj.Root.Type.GetHashCode();
				}

				// Token: 0x020001DD RID: 477
				public static class Singleton
				{
					// Token: 0x04000822 RID: 2082
					public static readonly IEqualityComparer<KeyTypeInfo<Key>> Instance = new KeyTypeInfo<Key>.Comparison.RootEqualityComparer();
				}
			}

			// Token: 0x020001DE RID: 478
			internal class HierarchyComparer : Comparer<KeyTypeInfo<Key>>
			{
				// Token: 0x06000D22 RID: 3362 RVA: 0x000334F4 File Offset: 0x000316F4
				private static int BaseCompare(KeyTypeInfo<Key> x, KeyTypeInfo<Key> y)
				{
					if (x.TraitDepth == 0 || x == y)
					{
						return 0;
					}
					int num = KeyTypeInfo<Key>.Comparison.HierarchyComparer.BaseCompare(x.Base, y.Base);
					if (num == 0)
					{
						num = KeyTypeInfo.ForcedDifCompareValue(x.Type, y.Type);
					}
					return num;
				}

				// Token: 0x06000D23 RID: 3363 RVA: 0x00033540 File Offset: 0x00031740
				private int CompareForward(KeyTypeInfo<Key> x, KeyTypeInfo<Key> y)
				{
					if (x.Root != y.Root)
					{
						return KeyTypeInfo.ForcedDifCompareValue(x.Root.Type, y.Root.Type);
					}
					if (x.TraitDepth == y.TraitDepth)
					{
						return KeyTypeInfo<Key>.Comparison.HierarchyComparer.BaseCompare(x, y);
					}
					return x.TraitDepth.CompareTo(y.TraitDepth);
				}

				// Token: 0x06000D24 RID: 3364 RVA: 0x000335A8 File Offset: 0x000317A8
				public override int Compare(KeyTypeInfo<Key> x, KeyTypeInfo<Key> y)
				{
					return -this.CompareForward(x, y);
				}

				// Token: 0x020001DF RID: 479
				public static class Singleton
				{
					// Token: 0x04000823 RID: 2083
					public static readonly IComparer<KeyTypeInfo<Key>> Instance = new KeyTypeInfo<Key>.Comparison.HierarchyComparer();
				}
			}
		}

		// Token: 0x020001E0 RID: 480
		internal class TraitDictionary
		{
			// Token: 0x06000D26 RID: 3366 RVA: 0x000335C0 File Offset: 0x000317C0
			public TraitDictionary(Key[] traitKeys)
			{
				if (traitKeys == null || traitKeys.Length == 0)
				{
					this.rootToKey = new Dictionary<KeyTypeInfo<Key>, Key>(0);
				}
				else
				{
					this.rootToKey = new Dictionary<KeyTypeInfo<Key>, Key>(traitKeys.Length, KeyTypeInfo<Key>.Comparison.EqualityComparer);
					foreach (Key key in traitKeys)
					{
						if (key)
						{
							this.rootToKey.Add(KeyTypeInfo<Key>.Find(key.GetType()), key);
						}
					}
				}
			}

			// Token: 0x06000D27 RID: 3367 RVA: 0x00033654 File Offset: 0x00031854
			private bool TryGet(KeyTypeInfo<Key> info, out Key key)
			{
				return this.rootToKey.TryGetValue(info, out key);
			}

			// Token: 0x06000D28 RID: 3368 RVA: 0x00033664 File Offset: 0x00031864
			private bool TryGetSoftCast<T>(KeyTypeInfo<Key> info, out T tkey) where T : Key
			{
				Key key;
				if (this.TryGet(info, out key))
				{
					tkey = (key as T);
					return true;
				}
				tkey = (T)((object)null);
				return false;
			}

			// Token: 0x06000D29 RID: 3369 RVA: 0x000336A4 File Offset: 0x000318A4
			private bool TryGetHardCast<T>(KeyTypeInfo<Key> info, out T tkey) where T : Key
			{
				Key key;
				if (this.TryGet(info, out key))
				{
					tkey = (T)((object)key);
					return true;
				}
				tkey = (T)((object)null);
				return false;
			}

			// Token: 0x06000D2A RID: 3370 RVA: 0x000336E0 File Offset: 0x000318E0
			public bool TryGet<T>(out Key key) where T : Key
			{
				return this.TryGet(KeyTypeInfo<Key, T>.Info, out key);
			}

			// Token: 0x06000D2B RID: 3371 RVA: 0x000336F0 File Offset: 0x000318F0
			public bool TryGetSoftCast<T>(out T key) where T : Key
			{
				return this.TryGetSoftCast<T>(KeyTypeInfo<Key, T>.Info, out key);
			}

			// Token: 0x06000D2C RID: 3372 RVA: 0x00033700 File Offset: 0x00031900
			public bool TryGetHardCast<T>(out T key) where T : Key
			{
				return this.TryGetHardCast<T>(KeyTypeInfo<Key, T>.Info, out key);
			}

			// Token: 0x06000D2D RID: 3373 RVA: 0x00033710 File Offset: 0x00031910
			public bool TryGet(Type traitType, out Key key)
			{
				return this.TryGet(KeyTypeInfo<Key>.Find(traitType), out key);
			}

			// Token: 0x06000D2E RID: 3374 RVA: 0x00033720 File Offset: 0x00031920
			public bool TryGetSoftCast<T>(Type traitType, out T key) where T : Key
			{
				return this.TryGetSoftCast<T>(KeyTypeInfo<Key>.Find(traitType), out key);
			}

			// Token: 0x06000D2F RID: 3375 RVA: 0x00033730 File Offset: 0x00031930
			public bool TryGetHardCast<T>(Type traitType, out T key) where T : Key
			{
				return this.TryGetHardCast<T>(KeyTypeInfo<Key>.Find(traitType), out key);
			}

			// Token: 0x06000D30 RID: 3376 RVA: 0x00033740 File Offset: 0x00031940
			public Key TryGet<T>() where T : Key
			{
				Key result;
				this.TryGet<T>(out result);
				return result;
			}

			// Token: 0x06000D31 RID: 3377 RVA: 0x00033758 File Offset: 0x00031958
			public T TryGetSoftCast<T>() where T : Key
			{
				T result;
				this.TryGetSoftCast<T>(out result);
				return result;
			}

			// Token: 0x06000D32 RID: 3378 RVA: 0x00033770 File Offset: 0x00031970
			public T TryGetHardCast<T>() where T : Key
			{
				T result;
				this.TryGetHardCast<T>(out result);
				return result;
			}

			// Token: 0x06000D33 RID: 3379 RVA: 0x00033788 File Offset: 0x00031988
			public Key TryGet(Type type)
			{
				Key result;
				this.TryGet(type, out result);
				return result;
			}

			// Token: 0x06000D34 RID: 3380 RVA: 0x000337A0 File Offset: 0x000319A0
			public T TryGetSoftCast<T>(Type type) where T : Key
			{
				T result;
				this.TryGetSoftCast<T>(type, out result);
				return result;
			}

			// Token: 0x06000D35 RID: 3381 RVA: 0x000337B8 File Offset: 0x000319B8
			public T TryGetHardCast<T>(Type type) where T : Key
			{
				T result;
				this.TryGetHardCast<T>(type, out result);
				return result;
			}

			// Token: 0x06000D36 RID: 3382 RVA: 0x000337D0 File Offset: 0x000319D0
			private Key Get(KeyTypeInfo<Key> info)
			{
				return this.rootToKey[info];
			}

			// Token: 0x06000D37 RID: 3383 RVA: 0x000337E0 File Offset: 0x000319E0
			private T GetSoftCast<T>(KeyTypeInfo<Key> info) where T : Key
			{
				return this.Get(info) as T;
			}

			// Token: 0x06000D38 RID: 3384 RVA: 0x000337F8 File Offset: 0x000319F8
			private T GetHardCast<T>(KeyTypeInfo<Key> info) where T : Key
			{
				return (T)((object)this.Get(info));
			}

			// Token: 0x06000D39 RID: 3385 RVA: 0x0003380C File Offset: 0x00031A0C
			public Key Get<T>() where T : Key
			{
				return this.Get(KeyTypeInfo<Key, T>.Info);
			}

			// Token: 0x06000D3A RID: 3386 RVA: 0x0003381C File Offset: 0x00031A1C
			public T GetSoftCast<T>() where T : Key
			{
				return this.GetSoftCast<T>(KeyTypeInfo<Key, T>.Info);
			}

			// Token: 0x06000D3B RID: 3387 RVA: 0x0003382C File Offset: 0x00031A2C
			public T GetHardCast<T>() where T : Key
			{
				return this.GetHardCast<T>(KeyTypeInfo<Key, T>.Info);
			}

			// Token: 0x06000D3C RID: 3388 RVA: 0x0003383C File Offset: 0x00031A3C
			public Key Get(Type type)
			{
				return this.Get(KeyTypeInfo<Key>.Find(type));
			}

			// Token: 0x06000D3D RID: 3389 RVA: 0x0003384C File Offset: 0x00031A4C
			public T GetSoftCast<T>(Type type) where T : Key
			{
				return this.GetSoftCast<T>(KeyTypeInfo<Key>.Find(type));
			}

			// Token: 0x06000D3E RID: 3390 RVA: 0x0003385C File Offset: 0x00031A5C
			public T GetHardCast<T>(Type type) where T : Key
			{
				return this.GetHardCast<T>(KeyTypeInfo<Key>.Find(type));
			}

			// Token: 0x06000D3F RID: 3391 RVA: 0x0003386C File Offset: 0x00031A6C
			public void MergeUpon(KeyTypeInfo<Key>.TraitDictionary fillGaps)
			{
				foreach (KeyValuePair<KeyTypeInfo<Key>, Key> keyValuePair in this.rootToKey)
				{
					if (!fillGaps.rootToKey.ContainsKey(keyValuePair.Key.Root))
					{
						fillGaps.rootToKey.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
			}

			// Token: 0x04000824 RID: 2084
			[NonSerialized]
			private readonly Dictionary<KeyTypeInfo<Key>, Key> rootToKey;
		}
	}
}
