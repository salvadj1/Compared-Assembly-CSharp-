using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000944 RID: 2372
[AddComponentMenu("Mesh/Combine Children")]
public class CombineChildren : MonoBehaviour
{
	// Token: 0x06004FA8 RID: 20392 RVA: 0x00151A2C File Offset: 0x0014FC2C
	public void DoCombine()
	{
		Component[] componentsInChildren = base.GetComponentsInChildren(typeof(MeshFilter));
		Matrix4x4 worldToLocalMatrix = base.transform.worldToLocalMatrix;
		Hashtable hashtable = new Hashtable();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			MeshFilter meshFilter = (MeshFilter)componentsInChildren[i];
			Renderer renderer = componentsInChildren[i].renderer;
			global::MeshCombineUtility.MeshInstance meshInstance = default(global::MeshCombineUtility.MeshInstance);
			meshInstance.mesh = meshFilter.sharedMesh;
			if (renderer != null && renderer.enabled && meshInstance.mesh != null)
			{
				meshInstance.transform = worldToLocalMatrix * meshFilter.transform.localToWorldMatrix;
				Material[] sharedMaterials = renderer.sharedMaterials;
				for (int j = 0; j < sharedMaterials.Length; j++)
				{
					meshInstance.subMeshIndex = Math.Min(j, meshInstance.mesh.subMeshCount - 1);
					ArrayList arrayList = (ArrayList)hashtable[sharedMaterials[j]];
					if (arrayList != null)
					{
						arrayList.Add(meshInstance);
					}
					else
					{
						arrayList = new ArrayList();
						arrayList.Add(meshInstance);
						hashtable.Add(sharedMaterials[j], arrayList);
					}
				}
				renderer.enabled = false;
			}
		}
		foreach (object obj in hashtable)
		{
			DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
			ArrayList arrayList2 = (ArrayList)dictionaryEntry.Value;
			global::MeshCombineUtility.MeshInstance[] combines = (global::MeshCombineUtility.MeshInstance[])arrayList2.ToArray(typeof(global::MeshCombineUtility.MeshInstance));
			if (hashtable.Count == 1)
			{
				if (base.GetComponent(typeof(MeshFilter)) == null)
				{
					base.gameObject.AddComponent(typeof(MeshFilter));
				}
				if (!base.GetComponent("MeshRenderer"))
				{
					base.gameObject.AddComponent("MeshRenderer");
				}
				MeshFilter meshFilter2 = (MeshFilter)base.GetComponent(typeof(MeshFilter));
				meshFilter2.mesh = global::MeshCombineUtility.Combine(combines, this.generateTriangleStrips);
				base.renderer.material = (Material)dictionaryEntry.Key;
				base.renderer.enabled = true;
			}
			else
			{
				GameObject gameObject = new GameObject("Combined mesh");
				gameObject.transform.parent = base.transform;
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localRotation = Quaternion.identity;
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.AddComponent(typeof(MeshFilter));
				gameObject.AddComponent("MeshRenderer");
				gameObject.renderer.material = (Material)dictionaryEntry.Key;
				MeshFilter meshFilter3 = (MeshFilter)gameObject.GetComponent(typeof(MeshFilter));
				meshFilter3.mesh = global::MeshCombineUtility.Combine(combines, this.generateTriangleStrips);
			}
		}
	}

	// Token: 0x06004FA9 RID: 20393 RVA: 0x00151D64 File Offset: 0x0014FF64
	private void Start()
	{
		if (this.combineOnStart)
		{
			this.DoCombine();
		}
	}

	// Token: 0x04002E6E RID: 11886
	public bool generateTriangleStrips = true;

	// Token: 0x04002E6F RID: 11887
	public bool combineOnStart = true;
}
