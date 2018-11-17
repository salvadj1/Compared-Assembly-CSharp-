using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000709 RID: 1801
public sealed class PrefabRenderer : IDisposable
{
	// Token: 0x06003BF6 RID: 15350 RVA: 0x000D5FA4 File Offset: 0x000D41A4
	private PrefabRenderer(int prefabId)
	{
		this.prefabId = prefabId;
		global::PrefabRenderer.Runtime.Register[this.prefabId] = new WeakReference(this);
	}

	// Token: 0x17000B8A RID: 2954
	// (get) Token: 0x06003BF7 RID: 15351 RVA: 0x000D5FCC File Offset: 0x000D41CC
	public int materialCount
	{
		get
		{
			return this.originalMaterials.Length;
		}
	}

	// Token: 0x06003BF8 RID: 15352 RVA: 0x000D5FD8 File Offset: 0x000D41D8
	public Material GetMaterial(int index)
	{
		return this.originalMaterials[index];
	}

	// Token: 0x06003BF9 RID: 15353 RVA: 0x000D5FE4 File Offset: 0x000D41E4
	public Material[] GetMaterialArrayCopy()
	{
		return (Material[])this.originalMaterials.Clone();
	}

	// Token: 0x06003BFA RID: 15354 RVA: 0x000D5FF8 File Offset: 0x000D41F8
	protected override void Finalize()
	{
		try
		{
			if (!this.disposed)
			{
				this.disposed = true;
				object @lock = global::PrefabRenderer.Runtime.Lock;
				lock (@lock)
				{
					global::PrefabRenderer.Runtime.Register.Remove(this.prefabId);
				}
			}
		}
		finally
		{
			base.Finalize();
		}
	}

	// Token: 0x06003BFB RID: 15355 RVA: 0x000D6080 File Offset: 0x000D4280
	public void Dispose()
	{
		if (!this.disposed)
		{
			this.disposed = true;
			GC.SuppressFinalize(this);
			object @lock = global::PrefabRenderer.Runtime.Lock;
			lock (@lock)
			{
				global::PrefabRenderer.Runtime.Register.Remove(this.prefabId);
			}
		}
	}

	// Token: 0x06003BFC RID: 15356 RVA: 0x000D60EC File Offset: 0x000D42EC
	public static global::PrefabRenderer GetOrCreateRender(GameObject prefab)
	{
		if (!prefab)
		{
			return null;
		}
		while (prefab.transform.parent)
		{
			prefab = prefab.transform.parent.gameObject;
		}
		int instanceID = prefab.GetInstanceID();
		object @lock = global::PrefabRenderer.Runtime.Lock;
		global::PrefabRenderer prefabRenderer;
		bool flag;
		lock (@lock)
		{
			WeakReference weakReference;
			if (global::PrefabRenderer.Runtime.Register.TryGetValue(instanceID, out weakReference))
			{
				prefabRenderer = (global::PrefabRenderer)weakReference.Target;
			}
			else
			{
				prefabRenderer = null;
			}
			flag = (prefabRenderer != null);
			if (!flag)
			{
				prefabRenderer = new global::PrefabRenderer(instanceID);
			}
		}
		if (!flag)
		{
			prefabRenderer.prefab = prefab;
			prefabRenderer.Refresh();
		}
		return prefabRenderer;
	}

	// Token: 0x06003BFD RID: 15357 RVA: 0x000D61BC File Offset: 0x000D43BC
	private static void DoNotCareResize<T>(ref T[] array, int size)
	{
		if (array == null || array.Length != size)
		{
			array = new T[size];
		}
	}

	// Token: 0x06003BFE RID: 15358 RVA: 0x000D61D8 File Offset: 0x000D43D8
	public void Refresh()
	{
		Transform transform = this.prefab.transform;
		HashSet<Material> hashSet = new HashSet<Material>();
		HashSet<Mesh> hashSet2 = new HashSet<Mesh>();
		List<Material[]> list = new List<Material[]>();
		List<Mesh> list2 = new List<Mesh>();
		int num = 0;
		Renderer[] componentsInChildren = this.prefab.GetComponentsInChildren<Renderer>(true);
		int num2 = 0;
		foreach (Renderer renderer in componentsInChildren)
		{
			if (renderer && renderer.enabled && !renderer.name.EndsWith("-lod", StringComparison.InvariantCultureIgnoreCase) && !renderer.name.EndsWith("_LOD_LOWEST", StringComparison.InvariantCultureIgnoreCase))
			{
				if (renderer is MeshRenderer)
				{
					componentsInChildren[num2++] = renderer;
					Mesh sharedMesh = renderer.GetComponent<MeshFilter>().sharedMesh;
					if (sharedMesh && hashSet2.Add(sharedMesh))
					{
						num++;
					}
					list2.Add(sharedMesh);
				}
				else
				{
					if (!(renderer is SkinnedMeshRenderer))
					{
						goto IL_15B;
					}
					componentsInChildren[num2++] = renderer;
					Mesh sharedMesh2 = ((SkinnedMeshRenderer)renderer).sharedMesh;
					if (sharedMesh2 && hashSet2.Add(sharedMesh2))
					{
						num++;
					}
					list2.Add(sharedMesh2);
				}
				Material[] sharedMaterials = renderer.sharedMaterials;
				list.Add(sharedMaterials);
				hashSet.UnionWith(sharedMaterials);
			}
			IL_15B:;
		}
		for (int j = num2; j < componentsInChildren.Length; j++)
		{
			componentsInChildren[j] = null;
		}
		int count = hashSet.Count;
		int num3 = (count % 32 <= 0) ? (count / 32) : (count / 32 + 1);
		global::PrefabRenderer.DoNotCareResize<int>(ref this.skipBits, num3);
		for (int k = 0; k < num3; k++)
		{
			this.skipBits[k] = 0;
		}
		Dictionary<Material, int> dictionary = new Dictionary<Material, int>(count);
		Dictionary<Mesh, int> dictionary2 = new Dictionary<Mesh, int>(num);
		global::PrefabRenderer.DoNotCareResize<Material>(ref this.originalMaterials, count);
		int num4 = 0;
		foreach (Material material in hashSet)
		{
			if (material.GetTag("IgnorePrefabRenderer", false, "False") == "True")
			{
				this.skipBits[num4 / 32] |= 1 << num4 % 32;
			}
			this.originalMaterials[num4] = material;
			dictionary[material] = num4++;
		}
		global::PrefabRenderer.DoNotCareResize<Mesh>(ref this.originalMeshes, num);
		int num5 = 0;
		foreach (Mesh mesh in hashSet2)
		{
			this.originalMeshes[num5] = mesh;
			dictionary2[mesh] = num5++;
		}
		global::PrefabRenderer.DoNotCareResize<global::PrefabRenderer.MeshRender>(ref this.meshes, num2);
		for (int l = 0; l < num2; l++)
		{
			Renderer renderer2 = componentsInChildren[l];
			Material[] array = list[l];
			int[] array2 = new int[array.Length];
			for (int m = 0; m < array.Length; m++)
			{
				array2[m] = dictionary[array[m]];
			}
			this.meshes[l].Set(dictionary2[list2[l]], array2, renderer2.transform.localToWorldMatrix * transform.worldToLocalMatrix, renderer2.gameObject.layer, renderer2.castShadows, renderer2.receiveShadows);
		}
	}

	// Token: 0x06003BFF RID: 15359 RVA: 0x000D65CC File Offset: 0x000D47CC
	public void Render(Camera camera, Matrix4x4 world, MaterialPropertyBlock props, Material[] overrideMaterials)
	{
		Material[] array = overrideMaterials ?? this.originalMaterials;
		foreach (global::PrefabRenderer.MeshRender meshRender in this.meshes)
		{
			Mesh mesh = this.originalMeshes[meshRender.mesh];
			int num = 0;
			foreach (int num2 in meshRender.materials)
			{
				if ((this.skipBits[num2 / 32] & 1 << num2 % 32) == 0)
				{
					Material material = array[num2];
					Graphics.DrawMesh(mesh, world, material, meshRender.layer, camera, num++, props, meshRender.castShadows, meshRender.receiveShadows);
				}
			}
		}
	}

	// Token: 0x06003C00 RID: 15360 RVA: 0x000D669C File Offset: 0x000D489C
	public void RenderOneMaterial(Camera camera, Matrix4x4 world, MaterialPropertyBlock props, Material overrideMaterial)
	{
		if (!overrideMaterial)
		{
			return;
		}
		foreach (global::PrefabRenderer.MeshRender meshRender in this.meshes)
		{
			Mesh mesh = this.originalMeshes[meshRender.mesh];
			int num = 0;
			for (int j = 0; j < meshRender.materials.Length; j++)
			{
				int num2 = meshRender.materials[j];
				if ((this.skipBits[num2 / 32] & 1 << num2 % 32) == 0)
				{
					Graphics.DrawMesh(mesh, world, overrideMaterial, meshRender.layer, camera, num++, props, meshRender.castShadows, meshRender.receiveShadows);
				}
			}
		}
	}

	// Token: 0x04001E19 RID: 7705
	private Material[] originalMaterials;

	// Token: 0x04001E1A RID: 7706
	private Mesh[] originalMeshes;

	// Token: 0x04001E1B RID: 7707
	private global::PrefabRenderer.MeshRender[] meshes;

	// Token: 0x04001E1C RID: 7708
	private int[] skipBits;

	// Token: 0x04001E1D RID: 7709
	private GameObject prefab;

	// Token: 0x04001E1E RID: 7710
	private bool disposed;

	// Token: 0x04001E1F RID: 7711
	private readonly int prefabId;

	// Token: 0x0200070A RID: 1802
	private static class Runtime
	{
		// Token: 0x04001E20 RID: 7712
		public static object Lock = new object();

		// Token: 0x04001E21 RID: 7713
		public static Dictionary<int, WeakReference> Register = new Dictionary<int, WeakReference>();
	}

	// Token: 0x0200070B RID: 1803
	private struct MeshRender
	{
		// Token: 0x06003C02 RID: 15362 RVA: 0x000D677C File Offset: 0x000D497C
		public void Set(int mesh, int[] materials, Matrix4x4 transform, int layer, bool castShadows, bool receiveShadows)
		{
			this.mesh = mesh;
			this.materials = materials;
			this.transform = transform;
			this.layer = layer;
			this.castShadows = castShadows;
			this.receiveShadows = receiveShadows;
		}

		// Token: 0x04001E22 RID: 7714
		public int mesh;

		// Token: 0x04001E23 RID: 7715
		public Matrix4x4 transform;

		// Token: 0x04001E24 RID: 7716
		public int[] materials;

		// Token: 0x04001E25 RID: 7717
		public int layer;

		// Token: 0x04001E26 RID: 7718
		public bool castShadows;

		// Token: 0x04001E27 RID: 7719
		public bool receiveShadows;
	}
}
