using System;
using UnityEngine;

// Token: 0x0200059A RID: 1434
public struct HealthDimmer
{
	// Token: 0x06002E81 RID: 11905 RVA: 0x000B2BF4 File Offset: 0x000B0DF4
	private void Initialize(IDBase self)
	{
		global::TakeDamage local;
		MeshRenderer[] componentsInChildren;
		Material material;
		if (!self || !(local = self.GetLocal<global::TakeDamage>()) || !global::HealthDimmer.GetFirstMaterial<MeshRenderer>(componentsInChildren = self.GetComponentsInChildren<MeshRenderer>(true), out material))
		{
			this.renderers = null;
			this.valid = false;
			this.takeDamage = null;
		}
		else
		{
			this.renderers = componentsInChildren;
			this.takeDamage = local;
			this.valid = true;
			this.structureStyle = (self.idMain is global::StructureComponent);
			Color color = material.GetColor(global::HealthDimmer.PropOnce._Color);
			this.averageRGB = (color.r + color.g + color.b) * 0.333333343f;
			this.propBlock = new MaterialPropertyBlock();
			this.percent = null;
		}
	}

	// Token: 0x06002E82 RID: 11906 RVA: 0x000B2CC0 File Offset: 0x000B0EC0
	private void MakeColor(float percent, out Color color)
	{
		float b;
		if (this.structureStyle)
		{
			b = 0.35f + (this.averageRGB - 0.35f) * percent;
		}
		else
		{
			float num = this.averageRGB * 0.33f;
			b = num + (this.averageRGB - num) * percent;
		}
		color.r = (color.g = (color.b = b));
		color.a = 1f;
	}

	// Token: 0x06002E83 RID: 11907 RVA: 0x000B2D30 File Offset: 0x000B0F30
	public void Reset()
	{
		this.percent = null;
		if (!this.initialized)
		{
			return;
		}
		if (this.propBlock != null)
		{
			this.propBlock.Clear();
		}
		if (this.valid)
		{
			foreach (MeshRenderer meshRenderer in this.renderers)
			{
				if (meshRenderer)
				{
					meshRenderer.SetPropertyBlock(null);
				}
			}
		}
	}

	// Token: 0x06002E84 RID: 11908 RVA: 0x000B2DAC File Offset: 0x000B0FAC
	public void UpdateHealthAmount(IDBase self, float newHealth, bool force = false)
	{
		if (!this.initialized)
		{
			this.initialized = true;
			this.Initialize(self);
		}
		if (!this.takeDamage)
		{
			return;
		}
		this.takeDamage.health = newHealth;
		if (this.disabled || !this.valid)
		{
			return;
		}
		float num = this.takeDamage.health / this.takeDamage.maxHealth;
		if (!force && this.percent != null && this.percent.Value == num)
		{
			return;
		}
		this.percent = new float?(num);
		Color color;
		this.MakeColor(num, out color);
		this.propBlock.Clear();
		this.propBlock.AddColor(global::HealthDimmer.PropOnce._Color, color);
		foreach (MeshRenderer meshRenderer in this.renderers)
		{
			if (meshRenderer)
			{
				meshRenderer.SetPropertyBlock(this.propBlock);
			}
		}
	}

	// Token: 0x06002E85 RID: 11909 RVA: 0x000B2EB4 File Offset: 0x000B10B4
	private static bool GetFirstMaterial<TRenderer>(TRenderer[] renderers, out Material material) where TRenderer : Renderer
	{
		int num;
		if (renderers != null && (num = renderers.Length) > 0)
		{
			for (int i = 0; i < num; i++)
			{
				TRenderer trenderer;
				Material sharedMaterial;
				if ((trenderer = renderers[i]) && (sharedMaterial = trenderer.sharedMaterial) && sharedMaterial.HasProperty(global::HealthDimmer.PropOnce._Color))
				{
					material = sharedMaterial;
					return true;
				}
			}
		}
		material = null;
		return false;
	}

	// Token: 0x04001913 RID: 6419
	[NonSerialized]
	private float averageRGB;

	// Token: 0x04001914 RID: 6420
	[NonSerialized]
	private float? percent;

	// Token: 0x04001915 RID: 6421
	[NonSerialized]
	private bool initialized;

	// Token: 0x04001916 RID: 6422
	[NonSerialized]
	private bool valid;

	// Token: 0x04001917 RID: 6423
	[NonSerialized]
	private bool structureStyle;

	// Token: 0x04001918 RID: 6424
	[NonSerialized]
	private MeshRenderer[] renderers;

	// Token: 0x04001919 RID: 6425
	[NonSerialized]
	private MaterialPropertyBlock propBlock;

	// Token: 0x0400191A RID: 6426
	[NonSerialized]
	private global::TakeDamage takeDamage;

	// Token: 0x0400191B RID: 6427
	[NonSerialized]
	public bool disabled;

	// Token: 0x0200059B RID: 1435
	private static class PropOnce
	{
		// Token: 0x0400191C RID: 6428
		public static readonly int _Color = Shader.PropertyToID("_Color");
	}
}
