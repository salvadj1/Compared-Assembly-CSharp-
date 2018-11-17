using System;
using System.Collections.Generic;
using Facepunch.Load;
using UnityEngine;

namespace Facepunch
{
	// Token: 0x02000101 RID: 257
	public static class Bundling
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060005E6 RID: 1510 RVA: 0x0001C79C File Offset: 0x0001A99C
		// (remove) Token: 0x060005E7 RID: 1511 RVA: 0x0001C7D4 File Offset: 0x0001A9D4
		public static event Bundling.OnLoadedEventHandler OnceLoaded
		{
			add
			{
				if (Bundling.Loaded)
				{
					value();
				}
				else
				{
					Bundling.nextLoadEvents = (Bundling.OnLoadedEventHandler)Delegate.Combine(Bundling.nextLoadEvents, value);
				}
			}
			remove
			{
				Bundling.nextLoadEvents = (Bundling.OnLoadedEventHandler)Delegate.Remove(Bundling.nextLoadEvents, value);
			}
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0001C7EC File Offset: 0x0001A9EC
		public static bool Load(string path, Type type, out Object asset)
		{
			if (!Bundling.HasLoadedBundleMap)
			{
				throw new InvalidOperationException("Bundles were not loaded");
			}
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				asset = null;
				return false;
			}
			return Bundling.Map.Assets.Load(path, type, out asset);
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0001C844 File Offset: 0x0001AA44
		public static Object Load(string path, Type type)
		{
			Object result;
			Bundling.Load(path, type, out result);
			return result;
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0001C85C File Offset: 0x0001AA5C
		public static bool Load<T>(string path, out T asset) where T : Object
		{
			Object @object;
			if (Bundling.Load(path, typeof(T), out @object))
			{
				asset = (T)((object)@object);
				return true;
			}
			asset = (T)((object)null);
			return false;
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0001C89C File Offset: 0x0001AA9C
		public static T Load<T>(string path) where T : Object
		{
			T result;
			Bundling.Load<T>(path, out result);
			return result;
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0001C8B4 File Offset: 0x0001AAB4
		public static bool Load<T>(string path, Type type, out T asset) where T : Object
		{
			if (!typeof(T).IsAssignableFrom(type))
			{
				throw new ArgumentException(string.Format("The given type ({1}) cannot cast to {0}", typeof(T), type), "type");
			}
			Object @object;
			if (Bundling.Load(path, type, out @object))
			{
				asset = (T)((object)@object);
				return true;
			}
			asset = (T)((object)null);
			return false;
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0001C920 File Offset: 0x0001AB20
		public static T Load<T>(string path, Type type) where T : Object
		{
			T result;
			Bundling.Load<T>(path, type, out result);
			return result;
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0001C938 File Offset: 0x0001AB38
		[Obsolete("This only works outside of editor for now, avoid it")]
		public static bool LoadAsync(string path, Type type, out AssetBundleRequest request)
		{
			if (!Bundling.HasLoadedBundleMap)
			{
				throw new InvalidOperationException("Bundles were not loaded");
			}
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				request = null;
				return false;
			}
			return Bundling.Map.Assets.LoadAsync(path, type, out request);
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0001C990 File Offset: 0x0001AB90
		[Obsolete("This only works outside of editor for now, avoid it")]
		public static AssetBundleRequest LoadAsync(string path, Type type)
		{
			AssetBundleRequest result;
			Bundling.LoadAsync(path, type, out result);
			return result;
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0001C9A8 File Offset: 0x0001ABA8
		[Obsolete("This only works outside of editor for now, avoid it")]
		public static bool LoadAsync<T>(string path, out AssetBundleRequest request) where T : Object
		{
			return Bundling.LoadAsync(path, typeof(T), out request);
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0001C9BC File Offset: 0x0001ABBC
		[Obsolete("This only works outside of editor for now, avoid it")]
		public static AssetBundleRequest LoadAsync<T>(string path)
		{
			return Bundling.LoadAsync(path, typeof(T));
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0001C9D0 File Offset: 0x0001ABD0
		public static Object[] LoadAll()
		{
			if (!Bundling.HasLoadedBundleMap)
			{
				throw new InvalidOperationException("Bundles were not loaded");
			}
			return new List<Object>(Bundling.Map.Assets.LoadAll()).ToArray();
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0001CA0C File Offset: 0x0001AC0C
		public static Object[] LoadAll(Type type)
		{
			if (type == typeof(Object))
			{
				return Bundling.LoadAll();
			}
			if (!Bundling.HasLoadedBundleMap)
			{
				throw new InvalidOperationException("Bundles were not loaded");
			}
			return new List<Object>(Bundling.Map.Assets.LoadAll(type)).ToArray();
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0001CA60 File Offset: 0x0001AC60
		public static T[] LoadAll<T>() where T : Object
		{
			if (!Bundling.HasLoadedBundleMap)
			{
				throw new InvalidOperationException("Bundles were not loaded");
			}
			List<T> list = new List<T>();
			foreach (Object @object in Bundling.Map.Assets.LoadAll(typeof(T)))
			{
				list.Add((T)((object)@object));
			}
			return list.ToArray();
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0001CAFC File Offset: 0x0001ACFC
		public static T[] LoadAll<T>(Type type) where T : Object
		{
			if (!typeof(T).IsAssignableFrom(type))
			{
				throw new ArgumentException(string.Format("The given type ({1}) cannot cast to {0}", typeof(T), type), "type");
			}
			if (!Bundling.HasLoadedBundleMap)
			{
				throw new InvalidOperationException("Bundles were not loaded");
			}
			List<T> list = new List<T>();
			foreach (Object @object in Bundling.Map.Assets.LoadAll(type))
			{
				list.Add((T)((object)@object));
			}
			return list.ToArray();
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0001CBC4 File Offset: 0x0001ADC4
		public static void BindToLoader(Facepunch.Load.Loader loader)
		{
			Bundling.BundleBridger @object = new Bundling.BundleBridger();
			loader.OnGroupedAssetBundlesLoaded += @object.AddArrays;
			loader.OnAllAssetBundlesLoaded += @object.FinalizeAndInstall;
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0001CBFC File Offset: 0x0001ADFC
		public static void Unload()
		{
			if (Bundling.HasLoadedBundleMap)
			{
				Bundling.Map.Dispose();
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x0001CC14 File Offset: 0x0001AE14
		public static bool Loaded
		{
			get
			{
				return Bundling.HasLoadedBundleMap;
			}
		}

		// Token: 0x0400050D RID: 1293
		private const bool kBundleUnloadClearsEverything = true;

		// Token: 0x0400050E RID: 1294
		private const string kUnloadedBundlesMessage = "Bundles were not loaded";

		// Token: 0x0400050F RID: 1295
		private static Bundling.LoadedBundleMap Map;

		// Token: 0x04000510 RID: 1296
		private static bool HasLoadedBundleMap;

		// Token: 0x04000511 RID: 1297
		private static Bundling.OnLoadedEventHandler nextLoadEvents;

		// Token: 0x02000102 RID: 258
		private class BundleBridger
		{
			// Token: 0x060005FA RID: 1530 RVA: 0x0001CC3C File Offset: 0x0001AE3C
			private List<Bundling.LoadedBundle> AssetListOfType(Type type)
			{
				if (type != this.lastAssetMapSearchKey)
				{
					Dictionary<Type, List<Bundling.LoadedBundle>> dictionary = this.assetsMap;
					this.lastAssetMapSearchKey = type;
					if (!dictionary.TryGetValue(type, out this.lastAssetMapSearchValue))
					{
						this.assetsMap[this.lastAssetMapSearchKey] = (this.lastAssetMapSearchValue = new List<Bundling.LoadedBundle>());
					}
				}
				return this.lastAssetMapSearchValue;
			}

			// Token: 0x060005FB RID: 1531 RVA: 0x0001CC9C File Offset: 0x0001AE9C
			private bool Remove(Type type)
			{
				if (this.assetsMap.Remove(type))
				{
					if (type == this.lastAssetMapSearchKey)
					{
						this.lastAssetMapSearchValue = null;
						this.lastAssetMapSearchKey = null;
					}
					return true;
				}
				return false;
			}

			// Token: 0x060005FC RID: 1532 RVA: 0x0001CCD8 File Offset: 0x0001AED8
			public void Add(AssetBundle bundle, Facepunch.Load.Item item)
			{
				if (item.ContentType == Facepunch.Load.ContentType.Assets)
				{
					this.AssetListOfType(item.TypeOfAssets).Add(new Bundling.LoadedBundle(bundle, item));
				}
				else
				{
					this.scenes.Add(new Bundling.LoadedBundle(bundle, item));
				}
			}

			// Token: 0x060005FD RID: 1533 RVA: 0x0001CD24 File Offset: 0x0001AF24
			public void AddArrays(AssetBundle[] bundles, Facepunch.Load.Item[] items)
			{
				for (int i = 0; i < bundles.Length; i++)
				{
					this.Add(bundles[i], items[i]);
				}
			}

			// Token: 0x060005FE RID: 1534 RVA: 0x0001CD5C File Offset: 0x0001AF5C
			public void FinalizeAndInstall(AssetBundle[] bundles, Facepunch.Load.Item[] items)
			{
				Bundling.LoadedBundleMap loadedBundleMap = new Bundling.LoadedBundleMap(this.assetsMap, this.scenes);
				if (loadedBundleMap == Bundling.Map && Bundling.nextLoadEvents != null)
				{
					Bundling.OnLoadedEventHandler nextLoadEvents = Bundling.nextLoadEvents;
					Bundling.nextLoadEvents = null;
					try
					{
						nextLoadEvents();
					}
					catch (Exception ex)
					{
						Debug.LogException(ex);
					}
				}
			}

			// Token: 0x04000512 RID: 1298
			private readonly List<Bundling.LoadedBundle> scenes = new List<Bundling.LoadedBundle>();

			// Token: 0x04000513 RID: 1299
			private readonly Dictionary<Type, List<Bundling.LoadedBundle>> assetsMap = new Dictionary<Type, List<Bundling.LoadedBundle>>();

			// Token: 0x04000514 RID: 1300
			private Type lastAssetMapSearchKey;

			// Token: 0x04000515 RID: 1301
			private List<Bundling.LoadedBundle> lastAssetMapSearchValue;
		}

		// Token: 0x02000103 RID: 259
		private class LoadedBundle
		{
			// Token: 0x060005FF RID: 1535 RVA: 0x0001CDD0 File Offset: 0x0001AFD0
			public LoadedBundle(AssetBundle bundle, Facepunch.Load.Item item)
			{
				this.Bundle = bundle;
				this.Item = item;
			}

			// Token: 0x06000600 RID: 1536 RVA: 0x0001CDE8 File Offset: 0x0001AFE8
			internal void Unload()
			{
				if (this.Bundle)
				{
					this.Bundle.Unload(true);
				}
				this.Bundle = null;
			}

			// Token: 0x06000601 RID: 1537 RVA: 0x0001CE10 File Offset: 0x0001B010
			public Object Load(string path)
			{
				return this.Bundle.Load(path);
			}

			// Token: 0x06000602 RID: 1538 RVA: 0x0001CE20 File Offset: 0x0001B020
			public Object Load(string path, Type type)
			{
				return this.Bundle.Load(path, type);
			}

			// Token: 0x06000603 RID: 1539 RVA: 0x0001CE30 File Offset: 0x0001B030
			public AssetBundleRequest LoadAsync(string path, Type type)
			{
				return this.Bundle.LoadAsync(path, type);
			}

			// Token: 0x06000604 RID: 1540 RVA: 0x0001CE40 File Offset: 0x0001B040
			public Object[] LoadAll()
			{
				return this.Bundle.LoadAll();
			}

			// Token: 0x06000605 RID: 1541 RVA: 0x0001CE50 File Offset: 0x0001B050
			public Object[] LoadAll(Type type)
			{
				return this.Bundle.LoadAll(type);
			}

			// Token: 0x06000606 RID: 1542 RVA: 0x0001CE60 File Offset: 0x0001B060
			public bool Contains(string path)
			{
				return this.Bundle.Contains(path);
			}

			// Token: 0x04000516 RID: 1302
			public readonly Facepunch.Load.Item Item;

			// Token: 0x04000517 RID: 1303
			private AssetBundle Bundle;
		}

		// Token: 0x02000104 RID: 260
		private class LoadedBundleAssetMap
		{
			// Token: 0x06000607 RID: 1543 RVA: 0x0001CE70 File Offset: 0x0001B070
			internal LoadedBundleAssetMap(IEnumerable<KeyValuePair<Type, List<Bundling.LoadedBundle>>> assets)
			{
				List<KeyValuePair<Type, List<Bundling.LoadedBundle>>> list = new List<KeyValuePair<Type, List<Bundling.LoadedBundle>>>(assets);
				list.Sort(delegate(KeyValuePair<Type, List<Bundling.LoadedBundle>> x, KeyValuePair<Type, List<Bundling.LoadedBundle>> y)
				{
					int num = (!typeof(GameObject).IsAssignableFrom(x.Key)) ? ((!typeof(ScriptableObject).IsAssignableFrom(x.Key)) ? 2 : 1) : 0;
					int value = (!typeof(GameObject).IsAssignableFrom(y.Key)) ? ((!typeof(ScriptableObject).IsAssignableFrom(y.Key)) ? 2 : 1) : 0;
					return num.CompareTo(value);
				});
				this.AllLoadedBundleAssetLists = new Bundling.LoadedBundleListOfAssets[list.Count];
				for (int i = 0; i < this.AllLoadedBundleAssetLists.Length; i++)
				{
					KeyValuePair<Type, List<Bundling.LoadedBundle>> keyValuePair = list[i];
					this.AllLoadedBundleAssetLists[i] = new Bundling.LoadedBundleListOfAssets(keyValuePair.Key, keyValuePair.Value);
				}
				this.tempBuffer = new short[this.AllLoadedBundleAssetLists.Length];
			}

			// Token: 0x06000608 RID: 1544 RVA: 0x0001CF18 File Offset: 0x0001B118
			internal void Unload()
			{
				foreach (Bundling.LoadedBundleListOfAssets loadedBundleListOfAssets in this.AllLoadedBundleAssetLists)
				{
					loadedBundleListOfAssets.Unload();
				}
			}

			// Token: 0x06000609 RID: 1545 RVA: 0x0001CF4C File Offset: 0x0001B14C
			private bool TypeIndices(Type key, out short[] value)
			{
				if (key == null)
				{
					throw new ArgumentNullException("type");
				}
				if (this.typeMap.TryGetValue(key, out value))
				{
					return value != null;
				}
				if (!typeof(Object).IsAssignableFrom(key))
				{
					throw new ArgumentOutOfRangeException("type", string.Format("type {0} is not assignable to UnityEngine.Object", key));
				}
				if (typeof(Component).IsAssignableFrom(key))
				{
					if (typeof(Component) == key)
					{
						bool result = this.TypeIndices(typeof(GameObject), out value);
						value = (short[])value.Clone();
						for (int i = 0; i < value.Length; i++)
						{
							if (value[i] >= 0)
							{
								value[i] = -(value[i] + 1);
							}
						}
						this.typeMap[key] = value;
						return result;
					}
					bool result2 = this.TypeIndices(typeof(Component), out value);
					this.typeMap[key] = value;
					return result2;
				}
				else
				{
					int num = 0;
					for (int j = 0; j < this.AllLoadedBundleAssetLists.Length; j++)
					{
						if (key.IsAssignableFrom(this.AllLoadedBundleAssetLists[j].TypeOfAssets))
						{
							this.tempBuffer[num++] = (short)j;
						}
					}
					int num2 = 0;
					int num3 = num;
					for (int k = 0; k < this.AllLoadedBundleAssetLists.Length; k++)
					{
						if (num2 < num3 && k == (int)this.tempBuffer[num2])
						{
							num2++;
						}
						else if (this.AllLoadedBundleAssetLists[k].TypeOfAssets.IsAssignableFrom(key))
						{
							this.tempBuffer[num++] = (short)(-(short)(k + 1));
						}
					}
					if (num == 0)
					{
						Dictionary<Type, short[]> dictionary = this.typeMap;
						short[] value2;
						value = (value2 = null);
						dictionary[key] = value2;
						return false;
					}
					value = new short[num];
					while (--num >= 0)
					{
						value[num] = this.tempBuffer[num];
					}
					this.typeMap[key] = value;
					return true;
				}
			}

			// Token: 0x0600060A RID: 1546 RVA: 0x0001D15C File Offset: 0x0001B35C
			public bool Load(string path, Type type, out Object asset)
			{
				short[] array;
				if (!this.TypeIndices(type, out array))
				{
					Debug.Log("no type index for " + type);
					asset = null;
					return false;
				}
				int i = 0;
				while (array[i] >= 0)
				{
					if (this.AllLoadedBundleAssetLists[(int)array[i]].Load(path, out asset))
					{
						return true;
					}
					if (++i >= array.Length)
					{
						asset = null;
						return false;
					}
				}
				while (i < array.Length)
				{
					Bundling.LoadedBundleListOfAssets loadedBundleListOfAssets = this.AllLoadedBundleAssetLists[(int)(-(int)(array[i] + 1))];
					if (loadedBundleListOfAssets.Load(path, type, out asset))
					{
						return true;
					}
					i++;
				}
				asset = null;
				return false;
			}

			// Token: 0x0600060B RID: 1547 RVA: 0x0001D1FC File Offset: 0x0001B3FC
			public bool LoadAsync(string path, Type type, out AssetBundleRequest request)
			{
				short[] array;
				if (!this.TypeIndices(type, out array))
				{
					request = null;
					return false;
				}
				int i = 0;
				while (array[i] >= 0)
				{
					if (this.AllLoadedBundleAssetLists[(int)array[i]].LoadAsync(path, out request))
					{
						return true;
					}
					if (++i >= array.Length)
					{
						request = null;
						return false;
					}
				}
				while (i < array.Length)
				{
					Bundling.LoadedBundleListOfAssets loadedBundleListOfAssets = this.AllLoadedBundleAssetLists[(int)(-(int)(array[i] + 1))];
					if (loadedBundleListOfAssets.LoadAsync(path, type, out request))
					{
						return true;
					}
					i++;
				}
				request = null;
				return false;
			}

			// Token: 0x0600060C RID: 1548 RVA: 0x0001D28C File Offset: 0x0001B48C
			public IEnumerable<Object> LoadAll()
			{
				foreach (Bundling.LoadedBundleListOfAssets listOfBundles in this.AllLoadedBundleAssetLists)
				{
					foreach (Bundling.LoadedBundle bundle in listOfBundles.Bundles)
					{
						foreach (Object asset in bundle.LoadAll())
						{
							yield return asset;
						}
					}
				}
				yield break;
			}

			// Token: 0x0600060D RID: 1549 RVA: 0x0001D2B0 File Offset: 0x0001B4B0
			public IEnumerable<Object> LoadAll(Type type)
			{
				short[] indices;
				if (!this.TypeIndices(type, out indices))
				{
					yield break;
				}
				int i = 0;
				while (indices[i] >= 0)
				{
					foreach (Bundling.LoadedBundle bundle in this.AllLoadedBundleAssetLists[(int)indices[i]].Bundles)
					{
						foreach (Object asset in bundle.LoadAll())
						{
							yield return asset;
						}
					}
					if (++i >= indices.Length)
					{
						yield break;
					}
				}
				while (i < indices.Length)
				{
					Bundling.LoadedBundleListOfAssets test = this.AllLoadedBundleAssetLists[(int)(-(int)(indices[i] + 1))];
					foreach (Bundling.LoadedBundle bundle2 in test.Bundles)
					{
						foreach (Object asset2 in bundle2.LoadAll(type))
						{
							yield return asset2;
						}
					}
					if (++i >= indices.Length)
					{
						break;
					}
				}
				yield break;
			}

			// Token: 0x04000518 RID: 1304
			public readonly Bundling.LoadedBundleListOfAssets[] AllLoadedBundleAssetLists;

			// Token: 0x04000519 RID: 1305
			private readonly Dictionary<Type, short[]> typeMap = new Dictionary<Type, short[]>();

			// Token: 0x0400051A RID: 1306
			private readonly short[] tempBuffer;
		}

		// Token: 0x02000107 RID: 263
		private class LoadedBundleListOfAssets
		{
			// Token: 0x0600061F RID: 1567 RVA: 0x0001D834 File Offset: 0x0001BA34
			public LoadedBundleListOfAssets(Type typeOfAssets, List<Bundling.LoadedBundle> bundles)
			{
				this.TypeOfAssets = typeOfAssets;
				this.Bundles = bundles.ToArray();
				this.pathsToFoundBundles = new Dictionary<string, short>(StringComparer.InvariantCultureIgnoreCase);
			}

			// Token: 0x06000620 RID: 1568 RVA: 0x0001D860 File Offset: 0x0001BA60
			private bool PathIndex(string path, out short index)
			{
				if (!this.pathsToFoundBundles.TryGetValue(path, out index))
				{
					for (int i = 0; i < this.Bundles.Length; i++)
					{
						if (this.Bundles[i].Contains(path))
						{
							this.pathsToFoundBundles[path] = (index = (short)i);
							return true;
						}
					}
					this.pathsToFoundBundles[path] = -1;
					return false;
				}
				return index != -1;
			}

			// Token: 0x06000621 RID: 1569 RVA: 0x0001D8DC File Offset: 0x0001BADC
			public bool Load(string path, Type type, out Object asset)
			{
				short num;
				if (this.PathIndex(path, out num))
				{
					Object @object;
					asset = (@object = this.Bundles[(int)num].Load(path, type));
					return @object;
				}
				asset = null;
				return false;
			}

			// Token: 0x06000622 RID: 1570 RVA: 0x0001D918 File Offset: 0x0001BB18
			public bool Load(string path, out Object asset)
			{
				short num;
				if (this.PathIndex(path, out num))
				{
					Object @object;
					asset = (@object = this.Bundles[(int)num].Load(path));
					return @object;
				}
				asset = null;
				return false;
			}

			// Token: 0x06000623 RID: 1571 RVA: 0x0001D950 File Offset: 0x0001BB50
			public bool LoadAsync(string path, Type type, out AssetBundleRequest request)
			{
				short num;
				if (this.PathIndex(path, out num))
				{
					AssetBundleRequest assetBundleRequest;
					request = (assetBundleRequest = this.Bundles[(int)num].LoadAsync(path, type));
					return assetBundleRequest != null;
				}
				request = null;
				return false;
			}

			// Token: 0x06000624 RID: 1572 RVA: 0x0001D98C File Offset: 0x0001BB8C
			public bool LoadAsync(string path, out AssetBundleRequest request)
			{
				return this.LoadAsync(path, this.TypeOfAssets, out request);
			}

			// Token: 0x06000625 RID: 1573 RVA: 0x0001D99C File Offset: 0x0001BB9C
			public IEnumerable<Object> LoadAll()
			{
				foreach (Bundling.LoadedBundle bundle in this.Bundles)
				{
					foreach (Object asset in bundle.LoadAll())
					{
						yield return asset;
					}
				}
				yield break;
			}

			// Token: 0x06000626 RID: 1574 RVA: 0x0001D9C0 File Offset: 0x0001BBC0
			public IEnumerable<Object> LoadAll(Type type)
			{
				foreach (Bundling.LoadedBundle bundle in this.Bundles)
				{
					foreach (Object asset in bundle.LoadAll(type))
					{
						yield return asset;
					}
				}
				yield break;
			}

			// Token: 0x06000627 RID: 1575 RVA: 0x0001D9F4 File Offset: 0x0001BBF4
			internal void Unload()
			{
				foreach (Bundling.LoadedBundle loadedBundle in this.Bundles)
				{
					loadedBundle.Unload();
				}
			}

			// Token: 0x0400053C RID: 1340
			public readonly Type TypeOfAssets;

			// Token: 0x0400053D RID: 1341
			public readonly Bundling.LoadedBundle[] Bundles;

			// Token: 0x0400053E RID: 1342
			private readonly Dictionary<string, short> pathsToFoundBundles;
		}

		// Token: 0x0200010A RID: 266
		private class LoadedBundleListOfScenes
		{
			// Token: 0x06000638 RID: 1592 RVA: 0x0001DCF4 File Offset: 0x0001BEF4
			public LoadedBundleListOfScenes(IEnumerable<Bundling.LoadedBundle> bundles)
			{
				if (bundles is List<Bundling.LoadedBundle>)
				{
					this.Bundles = ((List<Bundling.LoadedBundle>)bundles).ToArray();
				}
				else
				{
					this.Bundles = new List<Bundling.LoadedBundle>(bundles).ToArray();
				}
			}

			// Token: 0x06000639 RID: 1593 RVA: 0x0001DD3C File Offset: 0x0001BF3C
			internal void Unload()
			{
				foreach (Bundling.LoadedBundle loadedBundle in this.Bundles)
				{
					loadedBundle.Unload();
				}
			}

			// Token: 0x04000553 RID: 1363
			public readonly Bundling.LoadedBundle[] Bundles;
		}

		// Token: 0x0200010B RID: 267
		private class LoadedBundleMap : IDisposable
		{
			// Token: 0x0600063A RID: 1594 RVA: 0x0001DD70 File Offset: 0x0001BF70
			public LoadedBundleMap(IEnumerable<KeyValuePair<Type, List<Bundling.LoadedBundle>>> assets, IEnumerable<Bundling.LoadedBundle> scenes)
			{
				this.Assets = new Bundling.LoadedBundleAssetMap(assets);
				this.Scenes = new Bundling.LoadedBundleListOfScenes(scenes);
				Bundling.Map = this;
				Bundling.HasLoadedBundleMap = true;
			}

			// Token: 0x0600063B RID: 1595 RVA: 0x0001DDA8 File Offset: 0x0001BFA8
			public void Dispose()
			{
				if (!this.disposed)
				{
					if (Bundling.Map == this)
					{
						Bundling.Map = null;
						Bundling.HasLoadedBundleMap = false;
					}
					this.disposed = true;
					this.Assets.Unload();
					this.Scenes.Unload();
				}
			}

			// Token: 0x04000554 RID: 1364
			public readonly Bundling.LoadedBundleListOfScenes Scenes;

			// Token: 0x04000555 RID: 1365
			public readonly Bundling.LoadedBundleAssetMap Assets;

			// Token: 0x04000556 RID: 1366
			private bool disposed;
		}

		// Token: 0x0200010C RID: 268
		// (Invoke) Token: 0x0600063D RID: 1597
		public delegate void OnLoadedEventHandler();
	}
}
