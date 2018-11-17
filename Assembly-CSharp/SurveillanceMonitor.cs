using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200073F RID: 1855
[RequireComponent(typeof(Renderer))]
public class SurveillanceMonitor : MonoBehaviour
{
	// Token: 0x06003DBB RID: 15803 RVA: 0x000DDEEC File Offset: 0x000DC0EC
	private void Awake()
	{
		this.renderer = base.renderer;
		this.originalSharedMaterials = this.renderer.sharedMaterials;
		if (this.materialIds == null || this.materialIds.Length == 0)
		{
			Debug.LogWarning("Please, set the material ids for this SurveillanceMonitor. Assuming you meant to use id 0 only.", this);
			this.materialIds = new int[1];
		}
		HashSet<Material> hashSet = new HashSet<Material>();
		int num = 0;
		int[] array = new int[this.materialIds.Length];
		for (int i = 0; i < this.materialIds.Length; i++)
		{
			if (hashSet.Add(this.originalSharedMaterials[this.materialIds[i]]))
			{
				array[i] = i;
				num++;
			}
			else
			{
				for (int j = 0; j < i; j++)
				{
					if (this.originalSharedMaterials[this.materialIds[j]] == this.originalSharedMaterials[this.materialIds[i]])
					{
						array[i] = j;
					}
				}
			}
		}
		this.replacementMaterials = new Material[num];
		this.activeSharedMaterials = (Material[])this.originalSharedMaterials.Clone();
		for (int k = 0; k < this.materialIds.Length; k++)
		{
			Material material;
			if (array[k] == k)
			{
				material = (this.replacementMaterials[k] = new Material(this.originalSharedMaterials[this.materialIds[k]]));
			}
			else
			{
				material = this.replacementMaterials[this.materialIds[array[k]]];
			}
			this.activeSharedMaterials[this.materialIds[k]] = material;
		}
	}

	// Token: 0x06003DBC RID: 15804 RVA: 0x000DE078 File Offset: 0x000DC278
	public void DropReference(RenderTexture texture)
	{
		if (this.lastTexture == texture)
		{
			this.lastTexture = null;
		}
	}

	// Token: 0x06003DBD RID: 15805 RVA: 0x000DE094 File Offset: 0x000DC294
	private void BindTexture(Texture tex)
	{
		foreach (Material material in this.replacementMaterials)
		{
			material.SetTexture(this.textureName, tex);
		}
	}

	// Token: 0x06003DBE RID: 15806 RVA: 0x000DE0D0 File Offset: 0x000DC2D0
	private void OnWillRenderObject()
	{
		if (this.surveillanceCamera)
		{
			Camera current = Camera.current;
			if (this.surveillanceCamera.camera == current)
			{
				return;
			}
			Transform transform = current.transform;
			Vector3 vector = base.transform.position - transform.position;
			float sqrMagnitude = vector.sqrMagnitude;
			Texture texture;
			if (sqrMagnitude <= this.viewDistance * this.viewDistance && Vector3.Dot(transform.forward, vector) > 0f && (texture = this.surveillanceCamera.Render()))
			{
				foreach (Material material in this.replacementMaterials)
				{
					material.SetTexture(this.textureName, texture);
				}
				this.renderer.sharedMaterials = this.activeSharedMaterials;
			}
			else
			{
				this.renderer.sharedMaterials = this.originalSharedMaterials;
			}
		}
	}

	// Token: 0x04001FA7 RID: 8103
	[NonSerialized]
	public Renderer renderer;

	// Token: 0x04001FA8 RID: 8104
	[SerializeField]
	private int[] materialIds;

	// Token: 0x04001FA9 RID: 8105
	public string textureName = "_MainTex";

	// Token: 0x04001FAA RID: 8106
	public float aspect = 1f;

	// Token: 0x04001FAB RID: 8107
	public float viewDistance = 30f;

	// Token: 0x04001FAC RID: 8108
	private Texture lastTexture;

	// Token: 0x04001FAD RID: 8109
	public global::SurveillanceCamera surveillanceCamera;

	// Token: 0x04001FAE RID: 8110
	private Material[] replacementMaterials;

	// Token: 0x04001FAF RID: 8111
	private Material[] originalSharedMaterials;

	// Token: 0x04001FB0 RID: 8112
	private Material[] activeSharedMaterials;
}
