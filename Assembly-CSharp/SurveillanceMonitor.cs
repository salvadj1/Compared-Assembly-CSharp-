using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200067B RID: 1659
[RequireComponent(typeof(Renderer))]
public class SurveillanceMonitor : MonoBehaviour
{
	// Token: 0x060039C7 RID: 14791 RVA: 0x000D550C File Offset: 0x000D370C
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

	// Token: 0x060039C8 RID: 14792 RVA: 0x000D5698 File Offset: 0x000D3898
	public void DropReference(RenderTexture texture)
	{
		if (this.lastTexture == texture)
		{
			this.lastTexture = null;
		}
	}

	// Token: 0x060039C9 RID: 14793 RVA: 0x000D56B4 File Offset: 0x000D38B4
	private void BindTexture(Texture tex)
	{
		foreach (Material material in this.replacementMaterials)
		{
			material.SetTexture(this.textureName, tex);
		}
	}

	// Token: 0x060039CA RID: 14794 RVA: 0x000D56F0 File Offset: 0x000D38F0
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

	// Token: 0x04001DAF RID: 7599
	[NonSerialized]
	public Renderer renderer;

	// Token: 0x04001DB0 RID: 7600
	[SerializeField]
	private int[] materialIds;

	// Token: 0x04001DB1 RID: 7601
	public string textureName = "_MainTex";

	// Token: 0x04001DB2 RID: 7602
	public float aspect = 1f;

	// Token: 0x04001DB3 RID: 7603
	public float viewDistance = 30f;

	// Token: 0x04001DB4 RID: 7604
	private Texture lastTexture;

	// Token: 0x04001DB5 RID: 7605
	public SurveillanceCamera surveillanceCamera;

	// Token: 0x04001DB6 RID: 7606
	private Material[] replacementMaterials;

	// Token: 0x04001DB7 RID: 7607
	private Material[] originalSharedMaterials;

	// Token: 0x04001DB8 RID: 7608
	private Material[] activeSharedMaterials;
}
