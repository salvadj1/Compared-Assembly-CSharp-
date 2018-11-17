using System;
using UnityEngine;

// Token: 0x020004DF RID: 1247
public struct HealthDimmer
{
	// Token: 0x06002ACF RID: 10959 RVA: 0x000AAE5C File Offset: 0x000A905C
	private void Initialize(IDBase self)
	{
		TakeDamage local;
		MeshRenderer[] componentsInChildren;
		Material material;
		if (!self || !(local = self.GetLocal<TakeDamage>()) || !HealthDimmer.GetFirstMaterial<MeshRenderer>(componentsInChildren = self.GetComponentsInChildren<MeshRenderer>(true), out material))
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
			this.structureStyle = (self.idMain is StructureComponent);
			Color color = material.GetColor(HealthDimmer.PropOnce._Color);
			this.averageRGB = (color.r + color.g + color.b) * 0.333333343f;
			this.propBlock = new MaterialPropertyBlock();
			this.percent = null;
		}
	}

	// Token: 0x06002AD0 RID: 10960 RVA: 0x000AAF28 File Offset: 0x000A9128
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

	// Token: 0x06002AD1 RID: 10961 RVA: 0x000AAF98 File Offset: 0x000A9198
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

	// Token: 0x06002AD2 RID: 10962 RVA: 0x000AB014 File Offset: 0x000A9214
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
		this.propBlock.AddColor(HealthDimmer.PropOnce._Color, color);
		foreach (MeshRenderer meshRenderer in this.renderers)
		{
			if (meshRenderer)
			{
				meshRenderer.SetPropertyBlock(this.propBlock);
			}
		}
	}

	// Token: 0x06002AD3 RID: 10963 RVA: 0x000AB11C File Offset: 0x000A931C
	private static bool GetFirstMaterial<TRenderer>(TRenderer[] renderers, out Material material) where TRenderer : Renderer
	{
		int num;
		if (renderers != null && (num = renderers.Length) > 0)
		{
			for (int i = 0; i < num; i++)
			{
				TRenderer trenderer;
				Material sharedMaterial;
				if ((trenderer = renderers[i]) && (sharedMaterial = trenderer.sharedMaterial) && sharedMaterial.HasProperty(HealthDimmer.PropOnce._Color))
				{
					material = sharedMaterial;
					return true;
				}
			}
		}
		material = null;
		return false;
	}

	// Token: 0x04001756 RID: 5974
	[NonSerialized]
	private float averageRGB;

	// Token: 0x04001757 RID: 5975
	[NonSerialized]
	private float? percent;

	// Token: 0x04001758 RID: 5976
	[NonSerialized]
	private bool initialized;

	// Token: 0x04001759 RID: 5977
	[NonSerialized]
	private bool valid;

	// Token: 0x0400175A RID: 5978
	[NonSerialized]
	private bool structureStyle;

	// Token: 0x0400175B RID: 5979
	[NonSerialized]
	private MeshRenderer[] renderers;

	// Token: 0x0400175C RID: 5980
	[NonSerialized]
	private MaterialPropertyBlock propBlock;

	// Token: 0x0400175D RID: 5981
	[NonSerialized]
	private TakeDamage takeDamage;

	// Token: 0x0400175E RID: 5982
	[NonSerialized]
	public bool disabled;

	// Token: 0x020004E0 RID: 1248
	private static class PropOnce
	{
		// Token: 0x0400175F RID: 5983
		public static readonly int _Color = Shader.PropertyToID("_Color");
	}
}
