using System;
using System.Collections.Generic;

namespace Facepunch.Abstract
{
	// Token: 0x0200020A RID: 522
	internal class KeyTypeInfo<Key> where Key : global::TraitKey
	{
		// Token: 0x06000E59 RID: 3673 RVA: 0x00037308 File Offset: 0x00035508
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

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000E5A RID: 3674 RVA: 0x00037388 File Offset: 0x00035588
		public bool IsBaseTrait
		{
			get
			{
				return this.TraitDepth == 0;
			}
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x00037394 File Offset: 0x00035594
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

		// Token: 0x06000E5C RID: 3676 RVA: 0x000373E8 File Offset: 0x000355E8
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

		// Token: 0x06000E5D RID: 3677 RVA: 0x00037420 File Offset: 0x00035620
		public static KeyTypeInfo<Key> Find<T>() where T : Key
		{
			return KeyTypeInfo<Key, T>.Info;
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x00037428 File Offset: 0x00035628
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

		// Token: 0x06000E5F RID: 3679 RVA: 0x00037478 File Offset: 0x00035678
		public bool IsAssignableFrom(KeyTypeInfo<Key> info)
		{
			return info.Root == this.Root && info.TraitDepth >= this.TraitDepth && info.AssignableTo.Contains(this);
		}

		// Token: 0x04000934 RID: 2356
		public readonly Type Type;

		// Token: 0x04000935 RID: 2357
		public readonly KeyTypeInfo<Key> Base;

		// Token: 0x04000936 RID: 2358
		public readonly KeyTypeInfo<Key> Root;

		// Token: 0x04000937 RID: 2359
		public readonly int TraitDepth;

		// Token: 0x04000938 RID: 2360
		public readonly HashSet<KeyTypeInfo<Key>> AssignableTo;

		// Token: 0x0200020B RID: 523
		internal static class Registration
		{
			// Token: 0x06000E61 RID: 3681 RVA: 0x000374B8 File Offset: 0x000356B8
			public static void Add(KeyTypeInfo<Key> info)
			{
				KeyTypeInfo<Key>.Registration.dict.Add(info.Type, info);
			}

			// Token: 0x06000E62 RID: 3682 RVA: 0x000374CC File Offset: 0x000356CC
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

			// Token: 0x04000939 RID: 2361
			private static readonly Dictionary<Type, KeyTypeInfo<Key>> dict = new Dictionary<Type, KeyTypeInfo<Key>>();
		}

		// Token: 0x0200020C RID: 524
		public static class Comparison
		{
			// Token: 0x17000386 RID: 902
			// (get) Token: 0x06000E63 RID: 3683 RVA: 0x0003752C File Offset: 0x0003572C
			public static IEqualityComparer<KeyTypeInfo<Key>> EqualityComparer
			{
				get
				{
					return KeyTypeInfo<Key>.Comparison.RootEqualityComparer.Singleton.Instance;
				}
			}

			// Token: 0x17000387 RID: 903
			// (get) Token: 0x06000E64 RID: 3684 RVA: 0x00037534 File Offset: 0x00035734
			public static IComparer<KeyTypeInfo<Key>> Comparer
			{
				get
				{
					return KeyTypeInfo<Key>.Comparison.HierarchyComparer.Singleton.Instance;
				}
			}

			// Token: 0x0200020D RID: 525
			private class RootEqualityComparer : EqualityComparer<KeyTypeInfo<Key>>
			{
				// Token: 0x06000E65 RID: 3685 RVA: 0x0003753C File Offset: 0x0003573C
				private RootEqualityComparer()
				{
				}

				// Token: 0x06000E66 RID: 3686 RVA: 0x00037544 File Offset: 0x00035744
				public override bool Equals(KeyTypeInfo<Key> x, KeyTypeInfo<Key> y)
				{
					return x.Root == y.Root;
				}

				// Token: 0x06000E67 RID: 3687 RVA: 0x00037554 File Offset: 0x00035754
				public override int GetHashCode(KeyTypeInfo<Key> obj)
				{
					return obj.Root.Type.GetHashCode();
				}

				// Token: 0x0200020E RID: 526
				public static class Singleton
				{
					// Token: 0x0400093A RID: 2362
					public static readonly IEqualityComparer<KeyTypeInfo<Key>> Instance = new KeyTypeInfo<Key>.Comparison.RootEqualityComparer();
				}
			}

			// Token: 0x0200020F RID: 527
			internal class HierarchyComparer : Comparer<KeyTypeInfo<Key>>
			{
				// Token: 0x06000E6A RID: 3690 RVA: 0x0003757C File Offset: 0x0003577C
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

				// Token: 0x06000E6B RID: 3691 RVA: 0x000375C8 File Offset: 0x000357C8
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

				// Token: 0x06000E6C RID: 3692 RVA: 0x00037630 File Offset: 0x00035830
				public override int Compare(KeyTypeInfo<Key> x, KeyTypeInfo<Key> y)
				{
					return -this.CompareForward(x, y);
				}

				// Token: 0x02000210 RID: 528
				public static class Singleton
				{
					// Token: 0x0400093B RID: 2363
					public static readonly IComparer<KeyTypeInfo<Key>> Instance = new KeyTypeInfo<Key>.Comparison.HierarchyComparer();
				}
			}
		}

		// Token: 0x02000211 RID: 529
		internal class TraitDictionary
		{
			// Token: 0x06000E6E RID: 3694 RVA: 0x00037648 File Offset: 0x00035848
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

			// Token: 0x06000E6F RID: 3695 RVA: 0x000376DC File Offset: 0x000358DC
			private bool TryGet(KeyTypeInfo<Key> info, out Key key)
			{
				return this.rootToKey.TryGetValue(info, out key);
			}

			// Token: 0x06000E70 RID: 3696 RVA: 0x000376EC File Offset: 0x000358EC
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

			// Token: 0x06000E71 RID: 3697 RVA: 0x0003772C File Offset: 0x0003592C
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

			// Token: 0x06000E72 RID: 3698 RVA: 0x00037768 File Offset: 0x00035968
			public bool TryGet<T>(out Key key) where T : Key
			{
				return this.TryGet(KeyTypeInfo<Key, T>.Info, out key);
			}

			// Token: 0x06000E73 RID: 3699 RVA: 0x00037778 File Offset: 0x00035978
			public bool TryGetSoftCast<T>(out T key) where T : Key
			{
				return this.TryGetSoftCast<T>(KeyTypeInfo<Key, T>.Info, out key);
			}

			// Token: 0x06000E74 RID: 3700 RVA: 0x00037788 File Offset: 0x00035988
			public bool TryGetHardCast<T>(out T key) where T : Key
			{
				return this.TryGetHardCast<T>(KeyTypeInfo<Key, T>.Info, out key);
			}

			// Token: 0x06000E75 RID: 3701 RVA: 0x00037798 File Offset: 0x00035998
			public bool TryGet(Type traitType, out Key key)
			{
				return this.TryGet(KeyTypeInfo<Key>.Find(traitType), out key);
			}

			// Token: 0x06000E76 RID: 3702 RVA: 0x000377A8 File Offset: 0x000359A8
			public bool TryGetSoftCast<T>(Type traitType, out T key) where T : Key
			{
				return this.TryGetSoftCast<T>(KeyTypeInfo<Key>.Find(traitType), out key);
			}

			// Token: 0x06000E77 RID: 3703 RVA: 0x000377B8 File Offset: 0x000359B8
			public bool TryGetHardCast<T>(Type traitType, out T key) where T : Key
			{
				return this.TryGetHardCast<T>(KeyTypeInfo<Key>.Find(traitType), out key);
			}

			// Token: 0x06000E78 RID: 3704 RVA: 0x000377C8 File Offset: 0x000359C8
			public Key TryGet<T>() where T : Key
			{
				Key result;
				this.TryGet<T>(out result);
				return result;
			}

			// Token: 0x06000E79 RID: 3705 RVA: 0x000377E0 File Offset: 0x000359E0
			public T TryGetSoftCast<T>() where T : Key
			{
				T result;
				this.TryGetSoftCast<T>(out result);
				return result;
			}

			// Token: 0x06000E7A RID: 3706 RVA: 0x000377F8 File Offset: 0x000359F8
			public T TryGetHardCast<T>() where T : Key
			{
				T result;
				this.TryGetHardCast<T>(out result);
				return result;
			}

			// Token: 0x06000E7B RID: 3707 RVA: 0x00037810 File Offset: 0x00035A10
			public Key TryGet(Type type)
			{
				Key result;
				this.TryGet(type, out result);
				return result;
			}

			// Token: 0x06000E7C RID: 3708 RVA: 0x00037828 File Offset: 0x00035A28
			public T TryGetSoftCast<T>(Type type) where T : Key
			{
				T result;
				this.TryGetSoftCast<T>(type, out result);
				return result;
			}

			// Token: 0x06000E7D RID: 3709 RVA: 0x00037840 File Offset: 0x00035A40
			public T TryGetHardCast<T>(Type type) where T : Key
			{
				T result;
				this.TryGetHardCast<T>(type, out result);
				return result;
			}

			// Token: 0x06000E7E RID: 3710 RVA: 0x00037858 File Offset: 0x00035A58
			private Key Get(KeyTypeInfo<Key> info)
			{
				return this.rootToKey[info];
			}

			// Token: 0x06000E7F RID: 3711 RVA: 0x00037868 File Offset: 0x00035A68
			private T GetSoftCast<T>(KeyTypeInfo<Key> info) where T : Key
			{
				return this.Get(info) as T;
			}

			// Token: 0x06000E80 RID: 3712 RVA: 0x00037880 File Offset: 0x00035A80
			private T GetHardCast<T>(KeyTypeInfo<Key> info) where T : Key
			{
				return (T)((object)this.Get(info));
			}

			// Token: 0x06000E81 RID: 3713 RVA: 0x00037894 File Offset: 0x00035A94
			public Key Get<T>() where T : Key
			{
				return this.Get(KeyTypeInfo<Key, T>.Info);
			}

			// Token: 0x06000E82 RID: 3714 RVA: 0x000378A4 File Offset: 0x00035AA4
			public T GetSoftCast<T>() where T : Key
			{
				return this.GetSoftCast<T>(KeyTypeInfo<Key, T>.Info);
			}

			// Token: 0x06000E83 RID: 3715 RVA: 0x000378B4 File Offset: 0x00035AB4
			public T GetHardCast<T>() where T : Key
			{
				return this.GetHardCast<T>(KeyTypeInfo<Key, T>.Info);
			}

			// Token: 0x06000E84 RID: 3716 RVA: 0x000378C4 File Offset: 0x00035AC4
			public Key Get(Type type)
			{
				return this.Get(KeyTypeInfo<Key>.Find(type));
			}

			// Token: 0x06000E85 RID: 3717 RVA: 0x000378D4 File Offset: 0x00035AD4
			public T GetSoftCast<T>(Type type) where T : Key
			{
				return this.GetSoftCast<T>(KeyTypeInfo<Key>.Find(type));
			}

			// Token: 0x06000E86 RID: 3718 RVA: 0x000378E4 File Offset: 0x00035AE4
			public T GetHardCast<T>(Type type) where T : Key
			{
				return this.GetHardCast<T>(KeyTypeInfo<Key>.Find(type));
			}

			// Token: 0x06000E87 RID: 3719 RVA: 0x000378F4 File Offset: 0x00035AF4
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

			// Token: 0x0400093C RID: 2364
			[NonSerialized]
			private readonly Dictionary<KeyTypeInfo<Key>, Key> rootToKey;
		}
	}
}
