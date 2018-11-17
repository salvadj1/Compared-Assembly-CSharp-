using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000646 RID: 1606
public sealed class PrefabRenderer : IDisposable
{
	// Token: 0x0600380A RID: 14346 RVA: 0x000CD6F4 File Offset: 0x000CB8F4
	private PrefabRenderer(int prefabId)
	{
		this.prefabId = prefabId;
		PrefabRenderer.Runtime.Register[this.prefabId] = new WeakReference(this);
	}

	// Token: 0x17000B0A RID: 2826
	// (get) Token: 0x0600380B RID: 14347 RVA: 0x000CD71C File Offset: 0x000CB91C
	public int materialCount
	{
		get
		{
			return this.originalMaterials.Length;
		}
	}

	// Token: 0x0600380C RID: 14348 RVA: 0x000CD728 File Offset: 0x000CB928
	public Material GetMaterial(int index)
	{
		return this.originalMaterials[index];
	}

	// Token: 0x0600380D RID: 14349 RVA: 0x000CD734 File Offset: 0x000CB934
	public Material[] GetMaterialArrayCopy()
	{
		return (Material[])this.originalMaterials.Clone();
	}

	// Token: 0x0600380E RID: 14350 RVA: 0x000CD748 File Offset: 0x000CB948
	protected override void Finalize()
	{
		try
		{
			if (!this.disposed)
			{
				this.disposed = true;
				object @lock = PrefabRenderer.Runtime.Lock;
				lock (@lock)
				{
					PrefabRenderer.Runtime.Register.Remove(this.prefabId);
				}
			}
		}
		finally
		{
			base.Finalize();
		}
	}

	// Token: 0x0600380F RID: 14351 RVA: 0x000CD7D0 File Offset: 0x000CB9D0
	public void Dispose()
	{
		if (!this.disposed)
		{
			this.disposed = true;
			GC.SuppressFinalize(this);
			object @lock = PrefabRenderer.Runtime.Lock;
			lock (@lock)
			{
				PrefabRenderer.Runtime.Register.Remove(this.prefabId);
			}
		}
	}

	// Token: 0x06003810 RID: 14352 RVA: 0x000CD83C File Offset: 0x000CBA3C
	public static PrefabRenderer GetOrCreateRender(GameObject prefab)
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
		object @lock = PrefabRenderer.Runtime.Lock;
		PrefabRenderer prefabRenderer;
		bool flag;
		lock (@lock)
		{
			WeakReference weakReference;
			if (PrefabRenderer.Runtime.Register.TryGetValue(instanceID, out weakReference))
			{
				prefabRenderer = (PrefabRenderer)weakReference.Target;
			}
			else
			{
				prefabRenderer = null;
			}
			flag = (prefabRenderer != null);
			if (!flag)
			{
				prefabRenderer = new PrefabRenderer(instanceID);
			}
		}
		if (!flag)
		{
			prefabRenderer.prefab = prefab;
			prefabRenderer.Refresh();
		}
		return prefabRenderer;
	}

	// Token: 0x06003811 RID: 14353 RVA: 0x000CD90C File Offset: 0x000CBB0C
	private static void DoNotCareResize<T>(ref T[] array, int size)
	{
		if (array == null || array.Length != size)
		{
			array = new T[size];
		}
	}

	// Token: 0x06003812 RID: 14354 RVA: 0x000CD928 File Offset: 0x000CBB28
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
		PrefabRenderer.DoNotCareResize<int>(ref this.skipBits, num3);
		for (int k = 0; k < num3; k++)
		{
			this.skipBits[k] = 0;
		}
		Dictionary<Material, int> dictionary = new Dictionary<Material, int>(count);
		Dictionary<Mesh, int> dictionary2 = new Dictionary<Mesh, int>(num);
		PrefabRenderer.DoNotCareResize<Material>(ref this.originalMaterials, count);
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
		PrefabRenderer.DoNotCareResize<Mesh>(ref this.originalMeshes, num);
		int num5 = 0;
		foreach (Mesh mesh in hashSet2)
		{
			this.originalMeshes[num5] = mesh;
			dictionary2[mesh] = num5++;
		}
		PrefabRenderer.DoNotCareResize<PrefabRenderer.MeshRender>(ref this.meshes, num2);
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

	// Token: 0x06003813 RID: 14355 RVA: 0x000CDD1C File Offset: 0x000CBF1C
	public void Render(Camera camera, Matrix4x4 world, MaterialPropertyBlock props, Material[] overrideMaterials)
	{
		Material[] array = overrideMaterials ?? this.originalMaterials;
		foreach (PrefabRenderer.MeshRender meshRender in this.meshes)
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

	// Token: 0x06003814 RID: 14356 RVA: 0x000CDDEC File Offset: 0x000CBFEC
	public void RenderOneMaterial(Camera camera, Matrix4x4 world, MaterialPropertyBlock props, Material overrideMaterial)
	{
		if (!overrideMaterial)
		{
			return;
		}
		foreach (PrefabRenderer.MeshRender meshRender in this.meshes)
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

	// Token: 0x04001C24 RID: 7204
	private Material[] originalMaterials;

	// Token: 0x04001C25 RID: 7205
	private Mesh[] originalMeshes;

	// Token: 0x04001C26 RID: 7206
	private PrefabRenderer.MeshRender[] meshes;

	// Token: 0x04001C27 RID: 7207
	private int[] skipBits;

	// Token: 0x04001C28 RID: 7208
	private GameObject prefab;

	// Token: 0x04001C29 RID: 7209
	private bool disposed;

	// Token: 0x04001C2A RID: 7210
	private readonly int prefabId;

	// Token: 0x02000647 RID: 1607
	private static class Runtime
	{
		// Token: 0x04001C2B RID: 7211
		public static object Lock = new object();

		// Token: 0x04001C2C RID: 7212
		public static Dictionary<int, WeakReference> Register = new Dictionary<int, WeakReference>();
	}

	// Token: 0x02000648 RID: 1608
	private struct MeshRender
	{
		// Token: 0x06003816 RID: 14358 RVA: 0x000CDECC File Offset: 0x000CC0CC
		public void Set(int mesh, int[] materials, Matrix4x4 transform, int layer, bool castShadows, bool receiveShadows)
		{
			this.mesh = mesh;
			this.materials = materials;
			this.transform = transform;
			this.layer = layer;
			this.castShadows = castShadows;
			this.receiveShadows = receiveShadows;
		}

		// Token: 0x04001C2D RID: 7213
		public int mesh;

		// Token: 0x04001C2E RID: 7214
		public Matrix4x4 transform;

		// Token: 0x04001C2F RID: 7215
		public int[] materials;

		// Token: 0x04001C30 RID: 7216
		public int layer;

		// Token: 0x04001C31 RID: 7217
		public bool castShadows;

		// Token: 0x04001C32 RID: 7218
		public bool receiveShadows;
	}
}
