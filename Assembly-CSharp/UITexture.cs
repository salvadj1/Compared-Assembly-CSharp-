using System;
using NGUI.Meshing;
using UnityEngine;

// Token: 0x020008FB RID: 2299
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Texture")]
public class UITexture : global::UIWidget
{
	// Token: 0x06004E74 RID: 20084 RVA: 0x00143D68 File Offset: 0x00141F68
	public UITexture() : base(global::UIWidget.WidgetFlags.KeepsMaterial)
	{
	}

	// Token: 0x17000F0F RID: 3855
	// (get) Token: 0x06004E75 RID: 20085 RVA: 0x00143D74 File Offset: 0x00141F74
	// (set) Token: 0x06004E76 RID: 20086 RVA: 0x00143D7C File Offset: 0x00141F7C
	public bool mirrorX
	{
		get
		{
			return this._mirrorX;
		}
		set
		{
			if (this._mirrorX != value)
			{
				this._mirrorX = value;
				base.ChangedAuto();
			}
		}
	}

	// Token: 0x17000F10 RID: 3856
	// (get) Token: 0x06004E77 RID: 20087 RVA: 0x00143D98 File Offset: 0x00141F98
	// (set) Token: 0x06004E78 RID: 20088 RVA: 0x00143DA0 File Offset: 0x00141FA0
	public bool mirrorY
	{
		get
		{
			return this._mirrorY;
		}
		set
		{
			if (this._mirrorY != value)
			{
				this._mirrorY = value;
				base.ChangedAuto();
			}
		}
	}

	// Token: 0x06004E79 RID: 20089 RVA: 0x00143DBC File Offset: 0x00141FBC
	public override void MakePixelPerfect()
	{
		Texture mainTexture = base.mainTexture;
		if (mainTexture != null)
		{
			Vector3 localScale = base.cachedTransform.localScale;
			localScale.x = (float)mainTexture.width;
			localScale.y = (float)mainTexture.height;
			localScale.z = 1f;
			base.cachedTransform.localScale = localScale;
		}
		base.MakePixelPerfect();
	}

	// Token: 0x06004E7A RID: 20090 RVA: 0x00143E24 File Offset: 0x00142024
	public override void OnFill(NGUI.Meshing.MeshBuffer m)
	{
		NGUI.Meshing.Vertex a;
		a.z = 0f;
		NGUI.Meshing.Vertex b;
		b.z = 0f;
		NGUI.Meshing.Vertex c;
		c.z = 0f;
		NGUI.Meshing.Vertex d;
		d.z = 0f;
		Color color = base.color;
		a.r = (b.r = (c.r = (d.r = color.r)));
		a.g = (b.g = (c.g = (d.g = color.g)));
		a.b = (b.b = (c.b = (d.b = color.b)));
		a.a = (b.a = (c.a = (d.a = color.a)));
		if (this._mirrorX)
		{
			if (this._mirrorY)
			{
				a.x = 0.5f;
				a.y = -0.5f;
				b.x = 0.5f;
				b.y = -1f;
				c.x = 0f;
				c.y = -1f;
				d.x = 0f;
				d.y = -0.5f;
				a.u = 1f;
				a.v = 1f;
				b.u = 1f;
				b.v = 0f;
				c.u = 0f;
				c.v = 0f;
				d.u = 0f;
				d.v = 1f;
				m.TextureQuad(a, b, c, d);
				a.x = 0.5f;
				a.y = -0f;
				b.x = 0.5f;
				b.y = -0.5f;
				c.x = 0f;
				c.y = -0.5f;
				d.x = 0f;
				d.y = -0f;
				a.u = 0f;
				a.v = 1f;
				b.u = 0f;
				b.v = 0f;
				c.u = 1f;
				c.v = 0f;
				d.u = 1f;
				d.v = 1f;
				m.TextureQuad(a, b, c, d);
				a.x = 1f;
				a.y = -0.5f;
				b.x = 1f;
				b.y = -1f;
				c.x = 0.5f;
				c.y = -1f;
				d.x = 0.5f;
				d.y = -0.5f;
				a.u = 1f;
				a.v = 1f;
				b.u = 1f;
				b.v = 0f;
				c.u = 0f;
				c.v = 0f;
				d.u = 0f;
				d.v = 1f;
				m.TextureQuad(a, b, c, d);
				a.x = 1f;
				a.y = -0f;
				b.x = 1f;
				b.y = -0.5f;
				c.x = 0.5f;
				c.y = -0.5f;
				d.x = 0.5f;
				d.y = -0f;
				a.u = 0f;
				a.v = 1f;
				b.u = 0f;
				b.v = 0f;
				c.u = 1f;
				c.v = 0f;
				d.u = 1f;
				d.v = 1f;
				m.TextureQuad(a, b, c, d);
			}
			else
			{
				a.x = 0.5f;
				a.y = 0f;
				b.x = 0.5f;
				b.y = -1f;
				c.x = 0f;
				c.y = -1f;
				d.x = 0f;
				d.y = -0f;
				a.u = 1f;
				a.v = 1f;
				b.u = 1f;
				b.v = 0f;
				c.u = 0f;
				c.v = 0f;
				d.u = 0f;
				d.v = 1f;
				m.TextureQuad(a, b, c, d);
				a.x = 1f;
				a.y = 0f;
				b.x = 1f;
				b.y = -1f;
				c.x = 0.5f;
				c.y = -1f;
				d.x = 0.5f;
				d.y = 0f;
				a.u = 0f;
				a.v = 1f;
				b.u = 0f;
				b.v = 0f;
				c.u = 1f;
				c.v = 0f;
				d.u = 1f;
				d.v = 1f;
				m.TextureQuad(a, b, c, d);
			}
		}
		else if (this._mirrorY)
		{
			a.x = 1f;
			a.y = -0.5f;
			b.x = 1f;
			b.y = -1f;
			c.x = 0f;
			c.y = -1f;
			d.x = 0f;
			d.y = -0.5f;
			a.u = 1f;
			a.v = 0f;
			b.u = 1f;
			b.v = 1f;
			c.u = 0f;
			c.v = 1f;
			d.u = 0f;
			d.v = 0f;
			m.TextureQuad(a, b, c, d);
			a.x = 1f;
			a.y = 0f;
			b.x = 1f;
			b.y = -0.5f;
			c.x = 0f;
			c.y = -0.5f;
			d.x = 0f;
			d.y = -0f;
			a.u = 1f;
			a.v = 1f;
			b.u = 1f;
			b.v = 0f;
			c.u = 0f;
			c.v = 0f;
			d.u = 0f;
			d.v = 1f;
			m.TextureQuad(a, b, c, d);
		}
		else
		{
			a.x = 1f;
			a.y = 0f;
			b.x = 1f;
			b.y = -1f;
			c.x = 0f;
			c.y = -1f;
			d.x = 0f;
			d.y = -0f;
			a.u = 1f;
			a.v = 1f;
			b.u = 1f;
			b.v = 0f;
			c.u = 0f;
			c.v = 0f;
			d.u = 0f;
			d.v = 1f;
			m.TextureQuad(a, b, c, d);
		}
	}

	// Token: 0x04002C0F RID: 11279
	[SerializeField]
	[HideInInspector]
	private bool _mirrorY;

	// Token: 0x04002C10 RID: 11280
	[SerializeField]
	[HideInInspector]
	private bool _mirrorX;
}
