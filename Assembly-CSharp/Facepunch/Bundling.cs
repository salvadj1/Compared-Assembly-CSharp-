using System;
using System.Collections.Generic;
using Facepunch.Load;
using UnityEngine;

namespace Facepunch
{
	// Token: 0x020000ED RID: 237
	public static class Bundling
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000568 RID: 1384 RVA: 0x0001ADD4 File Offset: 0x00018FD4
		// (remove) Token: 0x06000569 RID: 1385 RVA: 0x0001AE0C File Offset: 0x0001900C
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

		// Token: 0x0600056A RID: 1386 RVA: 0x0001AE24 File Offset: 0x00019024
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

		// Token: 0x0600056B RID: 1387 RVA: 0x0001AE7C File Offset: 0x0001907C
		public static Object Load(string path, Type type)
		{
			Object result;
			Bundling.Load(path, type, out result);
			return result;
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0001AE94 File Offset: 0x00019094
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

		// Token: 0x0600056D RID: 1389 RVA: 0x0001AED4 File Offset: 0x000190D4
		public static T Load<T>(string path) where T : Object
		{
			T result;
			Bundling.Load<T>(path, out result);
			return result;
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0001AEEC File Offset: 0x000190EC
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

		// Token: 0x0600056F RID: 1391 RVA: 0x0001AF58 File Offset: 0x00019158
		public static T Load<T>(string path, Type type) where T : Object
		{
			T result;
			Bundling.Load<T>(path, type, out result);
			return result;
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0001AF70 File Offset: 0x00019170
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

		// Token: 0x06000571 RID: 1393 RVA: 0x0001AFC8 File Offset: 0x000191C8
		[Obsolete("This only works outside of editor for now, avoid it")]
		public static AssetBundleRequest LoadAsync(string path, Type type)
		{
			AssetBundleRequest result;
			Bundling.LoadAsync(path, type, out result);
			return result;
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0001AFE0 File Offset: 0x000191E0
		[Obsolete("This only works outside of editor for now, avoid it")]
		public static bool LoadAsync<T>(string path, out AssetBundleRequest request) where T : Object
		{
			return Bundling.LoadAsync(path, typeof(T), out request);
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0001AFF4 File Offset: 0x000191F4
		[Obsolete("This only works outside of editor for now, avoid it")]
		public static AssetBundleRequest LoadAsync<T>(string path)
		{
			return Bundling.LoadAsync(path, typeof(T));
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0001B008 File Offset: 0x00019208
		public static Object[] LoadAll()
		{
			if (!Bundling.HasLoadedBundleMap)
			{
				throw new InvalidOperationException("Bundles were not loaded");
			}
			return new List<Object>(Bundling.Map.Assets.LoadAll()).ToArray();
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0001B044 File Offset: 0x00019244
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

		// Token: 0x06000576 RID: 1398 RVA: 0x0001B098 File Offset: 0x00019298
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

		// Token: 0x06000577 RID: 1399 RVA: 0x0001B134 File Offset: 0x00019334
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

		// Token: 0x06000578 RID: 1400 RVA: 0x0001B1FC File Offset: 0x000193FC
		public static void BindToLoader(Loader loader)
		{
			Bundling.BundleBridger @object = new Bundling.BundleBridger();
			loader.OnGroupedAssetBundlesLoaded += @object.AddArrays;
			loader.OnAllAssetBundlesLoaded += @object.FinalizeAndInstall;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0001B234 File Offset: 0x00019434
		public static void Unload()
		{
			if (Bundling.HasLoadedBundleMap)
			{
				Bundling.Map.Dispose();
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x0001B24C File Offset: 0x0001944C
		public static bool Loaded
		{
			get
			{
				return Bundling.HasLoadedBundleMap;
			}
		}

		// Token: 0x0400049E RID: 1182
		private const bool kBundleUnloadClearsEverything = true;

		// Token: 0x0400049F RID: 1183
		private const string kUnloadedBundlesMessage = "Bundles were not loaded";

		// Token: 0x040004A0 RID: 1184
		private static Bundling.LoadedBundleMap Map;

		// Token: 0x040004A1 RID: 1185
		private static bool HasLoadedBundleMap;

		// Token: 0x040004A2 RID: 1186
		private static Bundling.OnLoadedEventHandler nextLoadEvents;

		// Token: 0x020000EE RID: 238
		private class BundleBridger
		{
			// Token: 0x0600057C RID: 1404 RVA: 0x0001B274 File Offset: 0x00019474
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

			// Token: 0x0600057D RID: 1405 RVA: 0x0001B2D4 File Offset: 0x000194D4
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

			// Token: 0x0600057E RID: 1406 RVA: 0x0001B310 File Offset: 0x00019510
			public void Add(AssetBundle bundle, Item item)
			{
				if (item.ContentType == ContentType.Assets)
				{
					this.AssetListOfType(item.TypeOfAssets).Add(new Bundling.LoadedBundle(bundle, item));
				}
				else
				{
					this.scenes.Add(new Bundling.LoadedBundle(bundle, item));
				}
			}

			// Token: 0x0600057F RID: 1407 RVA: 0x0001B35C File Offset: 0x0001955C
			public void AddArrays(AssetBundle[] bundles, Item[] items)
			{
				for (int i = 0; i < bundles.Length; i++)
				{
					this.Add(bundles[i], items[i]);
				}
			}

			// Token: 0x06000580 RID: 1408 RVA: 0x0001B394 File Offset: 0x00019594
			public void FinalizeAndInstall(AssetBundle[] bundles, Item[] items)
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

			// Token: 0x040004A3 RID: 1187
			private readonly List<Bundling.LoadedBundle> scenes = new List<Bundling.LoadedBundle>();

			// Token: 0x040004A4 RID: 1188
			private readonly Dictionary<Type, List<Bundling.LoadedBundle>> assetsMap = new Dictionary<Type, List<Bundling.LoadedBundle>>();

			// Token: 0x040004A5 RID: 1189
			private Type lastAssetMapSearchKey;

			// Token: 0x040004A6 RID: 1190
			private List<Bundling.LoadedBundle> lastAssetMapSearchValue;
		}

		// Token: 0x020000EF RID: 239
		private class LoadedBundle
		{
			// Token: 0x06000581 RID: 1409 RVA: 0x0001B408 File Offset: 0x00019608
			public LoadedBundle(AssetBundle bundle, Item item)
			{
				this.Bundle = bundle;
				this.Item = item;
			}

			// Token: 0x06000582 RID: 1410 RVA: 0x0001B420 File Offset: 0x00019620
			internal void Unload()
			{
				if (this.Bundle)
				{
					this.Bundle.Unload(true);
				}
				this.Bundle = null;
			}

			// Token: 0x06000583 RID: 1411 RVA: 0x0001B448 File Offset: 0x00019648
			public Object Load(string path)
			{
				return this.Bundle.Load(path);
			}

			// Token: 0x06000584 RID: 1412 RVA: 0x0001B458 File Offset: 0x00019658
			public Object Load(string path, Type type)
			{
				return this.Bundle.Load(path, type);
			}

			// Token: 0x06000585 RID: 1413 RVA: 0x0001B468 File Offset: 0x00019668
			public AssetBundleRequest LoadAsync(string path, Type type)
			{
				return this.Bundle.LoadAsync(path, type);
			}

			// Token: 0x06000586 RID: 1414 RVA: 0x0001B478 File Offset: 0x00019678
			public Object[] LoadAll()
			{
				return this.Bundle.LoadAll();
			}

			// Token: 0x06000587 RID: 1415 RVA: 0x0001B488 File Offset: 0x00019688
			public Object[] LoadAll(Type type)
			{
				return this.Bundle.LoadAll(type);
			}

			// Token: 0x06000588 RID: 1416 RVA: 0x0001B498 File Offset: 0x00019698
			public bool Contains(string path)
			{
				return this.Bundle.Contains(path);
			}

			// Token: 0x040004A7 RID: 1191
			public readonly Item Item;

			// Token: 0x040004A8 RID: 1192
			private AssetBundle Bundle;
		}

		// Token: 0x020000F0 RID: 240
		private class LoadedBundleAssetMap
		{
			// Token: 0x06000589 RID: 1417 RVA: 0x0001B4A8 File Offset: 0x000196A8
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

			// Token: 0x0600058A RID: 1418 RVA: 0x0001B550 File Offset: 0x00019750
			internal void Unload()
			{
				foreach (Bundling.LoadedBundleListOfAssets loadedBundleListOfAssets in this.AllLoadedBundleAssetLists)
				{
					loadedBundleListOfAssets.Unload();
				}
			}

			// Token: 0x0600058B RID: 1419 RVA: 0x0001B584 File Offset: 0x00019784
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

			// Token: 0x0600058C RID: 1420 RVA: 0x0001B794 File Offset: 0x00019994
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

			// Token: 0x0600058D RID: 1421 RVA: 0x0001B834 File Offset: 0x00019A34
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

			// Token: 0x0600058E RID: 1422 RVA: 0x0001B8C4 File Offset: 0x00019AC4
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

			// Token: 0x0600058F RID: 1423 RVA: 0x0001B8E8 File Offset: 0x00019AE8
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

			// Token: 0x040004A9 RID: 1193
			public readonly Bundling.LoadedBundleListOfAssets[] AllLoadedBundleAssetLists;

			// Token: 0x040004AA RID: 1194
			private readonly Dictionary<Type, short[]> typeMap = new Dictionary<Type, short[]>();

			// Token: 0x040004AB RID: 1195
			private readonly short[] tempBuffer;
		}

		// Token: 0x020000F1 RID: 241
		private class LoadedBundleListOfAssets
		{
			// Token: 0x06000591 RID: 1425 RVA: 0x0001B9BC File Offset: 0x00019BBC
			public LoadedBundleListOfAssets(Type typeOfAssets, List<Bundling.LoadedBundle> bundles)
			{
				this.TypeOfAssets = typeOfAssets;
				this.Bundles = bundles.ToArray();
				this.pathsToFoundBundles = new Dictionary<string, short>(StringComparer.InvariantCultureIgnoreCase);
			}

			// Token: 0x06000592 RID: 1426 RVA: 0x0001B9E8 File Offset: 0x00019BE8
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

			// Token: 0x06000593 RID: 1427 RVA: 0x0001BA64 File Offset: 0x00019C64
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

			// Token: 0x06000594 RID: 1428 RVA: 0x0001BAA0 File Offset: 0x00019CA0
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

			// Token: 0x06000595 RID: 1429 RVA: 0x0001BAD8 File Offset: 0x00019CD8
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

			// Token: 0x06000596 RID: 1430 RVA: 0x0001BB14 File Offset: 0x00019D14
			public bool LoadAsync(string path, out AssetBundleRequest request)
			{
				return this.LoadAsync(path, this.TypeOfAssets, out request);
			}

			// Token: 0x06000597 RID: 1431 RVA: 0x0001BB24 File Offset: 0x00019D24
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

			// Token: 0x06000598 RID: 1432 RVA: 0x0001BB48 File Offset: 0x00019D48
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

			// Token: 0x06000599 RID: 1433 RVA: 0x0001BB7C File Offset: 0x00019D7C
			internal void Unload()
			{
				foreach (Bundling.LoadedBundle loadedBundle in this.Bundles)
				{
					loadedBundle.Unload();
				}
			}

			// Token: 0x040004AD RID: 1197
			public readonly Type TypeOfAssets;

			// Token: 0x040004AE RID: 1198
			public readonly Bundling.LoadedBundle[] Bundles;

			// Token: 0x040004AF RID: 1199
			private readonly Dictionary<string, short> pathsToFoundBundles;
		}

		// Token: 0x020000F2 RID: 242
		private class LoadedBundleListOfScenes
		{
			// Token: 0x0600059A RID: 1434 RVA: 0x0001BBB0 File Offset: 0x00019DB0
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

			// Token: 0x0600059B RID: 1435 RVA: 0x0001BBF8 File Offset: 0x00019DF8
			internal void Unload()
			{
				foreach (Bundling.LoadedBundle loadedBundle in this.Bundles)
				{
					loadedBundle.Unload();
				}
			}

			// Token: 0x040004B0 RID: 1200
			public readonly Bundling.LoadedBundle[] Bundles;
		}

		// Token: 0x020000F3 RID: 243
		private class LoadedBundleMap : IDisposable
		{
			// Token: 0x0600059C RID: 1436 RVA: 0x0001BC2C File Offset: 0x00019E2C
			public LoadedBundleMap(IEnumerable<KeyValuePair<Type, List<Bundling.LoadedBundle>>> assets, IEnumerable<Bundling.LoadedBundle> scenes)
			{
				this.Assets = new Bundling.LoadedBundleAssetMap(assets);
				this.Scenes = new Bundling.LoadedBundleListOfScenes(scenes);
				Bundling.Map = this;
				Bundling.HasLoadedBundleMap = true;
			}

			// Token: 0x0600059D RID: 1437 RVA: 0x0001BC64 File Offset: 0x00019E64
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

			// Token: 0x040004B1 RID: 1201
			public readonly Bundling.LoadedBundleListOfScenes Scenes;

			// Token: 0x040004B2 RID: 1202
			public readonly Bundling.LoadedBundleAssetMap Assets;

			// Token: 0x040004B3 RID: 1203
			private bool disposed;
		}

		// Token: 0x02000861 RID: 2145
		// (Invoke) Token: 0x06004B5C RID: 19292
		public delegate void OnLoadedEventHandler();
	}
}
