using System;
using System.Text;
using UnityEngine;

// Token: 0x02000649 RID: 1609
[ExecuteInEditMode]
public class PrefabRendererTest : MonoBehaviour
{
	// Token: 0x06003818 RID: 14360 RVA: 0x000CDF04 File Offset: 0x000CC104
	[ContextMenu("Refresh")]
	private void RefreshRenderer()
	{
		if (this.renderer != null)
		{
			this.renderer.Refresh();
		}
	}

	// Token: 0x06003819 RID: 14361 RVA: 0x000CDF1C File Offset: 0x000CC11C
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

	// Token: 0x0600381A RID: 14362 RVA: 0x000CDF84 File Offset: 0x000CC184
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

	// Token: 0x0600381B RID: 14363 RVA: 0x000CDFD8 File Offset: 0x000CC1D8
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

	// Token: 0x0600381C RID: 14364 RVA: 0x000CE09C File Offset: 0x000CC29C
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
				this.renderer = PrefabRenderer.GetOrCreateRender(this.prefab);
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

	// Token: 0x04001C33 RID: 7219
	public GameObject prefab;

	// Token: 0x04001C34 RID: 7220
	public Material[] materialKeys;

	// Token: 0x04001C35 RID: 7221
	public Material[] materialValues;

	// Token: 0x04001C36 RID: 7222
	[NonSerialized]
	private PrefabRenderer renderer;

	// Token: 0x04001C37 RID: 7223
	[NonSerialized]
	private GameObject prefabRendering;

	// Token: 0x04001C38 RID: 7224
	[NonSerialized]
	private Material[] oldMaterialKeys;

	// Token: 0x04001C39 RID: 7225
	[NonSerialized]
	private Material[] oldMaterialValues;

	// Token: 0x04001C3A RID: 7226
	[NonSerialized]
	private bool oi;

	// Token: 0x04001C3B RID: 7227
	[NonSerialized]
	private Material[] overrideMaterials;
}
