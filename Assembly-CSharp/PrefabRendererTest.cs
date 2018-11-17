using System;
using System.Text;
using UnityEngine;

// Token: 0x0200070C RID: 1804
[ExecuteInEditMode]
public class PrefabRendererTest : MonoBehaviour
{
	// Token: 0x06003C04 RID: 15364 RVA: 0x000D67B4 File Offset: 0x000D49B4
	[ContextMenu("Refresh")]
	private void RefreshRenderer()
	{
		if (this.renderer != null)
		{
			this.renderer.Refresh();
		}
	}

	// Token: 0x06003C05 RID: 15365 RVA: 0x000D67CC File Offset: 0x000D49CC
	[ContextMenu("Print info")]
	private void PrintINfo()
	{
		if (this.renderer == null)
		{
			Debug.Log("No Renderer", this);
		}
		else
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (Material material in this.renderer.GetMaterialArrayCopy())
			{
				stringBuilder.AppendLine(material.ToString());
			}
			Debug.Log(stringBuilder, this);
		}
	}

	// Token: 0x06003C06 RID: 15366 RVA: 0x000D6834 File Offset: 0x000D4A34
	[ContextMenu("List Materials")]
	private void ListMaterials()
	{
		if (this.renderer == null)
		{
			return;
		}
		int materialCount = this.renderer.materialCount;
		for (int i = 0; i < materialCount; i++)
		{
			Debug.Log(this.renderer.GetMaterial(i), this.renderer.GetMaterial(i));
		}
	}

	// Token: 0x06003C07 RID: 15367 RVA: 0x000D6888 File Offset: 0x000D4A88
	[ContextMenu("Refresh material overrides")]
	private void ApplyOverrides()
	{
		if (this.renderer == null)
		{
			return;
		}
		this.overrideMaterials = this.renderer.GetMaterialArrayCopy();
		if (this.overrideMaterials.Length == 0 || this.materialKeys == null || this.materialValues == null)
		{
			return;
		}
		int num = Mathf.Min(this.overrideMaterials.Length, Mathf.Min(this.materialKeys.Length, this.materialValues.Length));
		for (int i = 0; i < num; i++)
		{
			int num2 = Array.IndexOf<Material>(this.materialKeys, this.overrideMaterials[i]);
			if (num2 != -1 && num2 < this.materialValues.Length)
			{
				this.overrideMaterials[i] = this.materialValues[num2];
			}
		}
	}

	// Token: 0x06003C08 RID: 15368 RVA: 0x000D694C File Offset: 0x000D4B4C
	private void Update()
	{
		if (this.prefabRendering != this.prefab || !this.oi)
		{
			if (this.prefabRendering)
			{
				this.renderer = null;
			}
			if (this.prefab)
			{
				this.renderer = global::PrefabRenderer.GetOrCreateRender(this.prefab);
			}
			this.prefabRendering = this.prefab;
			this.oi = true;
			this.ApplyOverrides();
		}
		if (this.renderer == null)
		{
			Debug.Log("None", this);
			return;
		}
		this.renderer.Render(null, base.transform.localToWorldMatrix, null, this.overrideMaterials);
	}

	// Token: 0x04001E28 RID: 7720
	public GameObject prefab;

	// Token: 0x04001E29 RID: 7721
	public Material[] materialKeys;

	// Token: 0x04001E2A RID: 7722
	public Material[] materialValues;

	// Token: 0x04001E2B RID: 7723
	[NonSerialized]
	private global::PrefabRenderer renderer;

	// Token: 0x04001E2C RID: 7724
	[NonSerialized]
	private GameObject prefabRendering;

	// Token: 0x04001E2D RID: 7725
	[NonSerialized]
	private Material[] oldMaterialKeys;

	// Token: 0x04001E2E RID: 7726
	[NonSerialized]
	private Material[] oldMaterialValues;

	// Token: 0x04001E2F RID: 7727
	[NonSerialized]
	private bool oi;

	// Token: 0x04001E30 RID: 7728
	[NonSerialized]
	private Material[] overrideMaterials;
}
