using System;
using NGUI.Meshing;
using UnityEngine;

// Token: 0x02000809 RID: 2057
[AddComponentMenu("NGUI/UI/Texture")]
[ExecuteInEditMode]
public class UITexture : UIWidget
{
	// Token: 0x060049C5 RID: 18885 RVA: 0x00139E04 File Offset: 0x00138004
	public UITexture() : base(UIWidget.WidgetFlags.KeepsMaterial)
	{
	}

	// Token: 0x17000E75 RID: 3701
	// (get) Token: 0x060049C6 RID: 18886 RVA: 0x00139E10 File Offset: 0x00138010
	// (set) Token: 0x060049C7 RID: 18887 RVA: 0x00139E18 File Offset: 0x00138018
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

	// Token: 0x17000E76 RID: 3702
	// (get) Token: 0x060049C8 RID: 18888 RVA: 0x00139E34 File Offset: 0x00138034
	// (set) Token: 0x060049C9 RID: 18889 RVA: 0x00139E3C File Offset: 0x0013803C
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

	// Token: 0x060049CA RID: 18890 RVA: 0x00139E58 File Offset: 0x00138058
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

	// Token: 0x060049CB RID: 18891 RVA: 0x00139EC0 File Offset: 0x001380C0
	public override void OnFill(MeshBuffer m)
	{
		Vertex a;
		a.z = 0f;
		Vertex b;
		b.z = 0f;
		Vertex c;
		c.z = 0f;
		Vertex d;
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

	// Token: 0x040029C1 RID: 10689
	[SerializeField]
	[HideInInspector]
	private bool _mirrorY;

	// Token: 0x040029C2 RID: 10690
	[HideInInspector]
	[SerializeField]
	private bool _mirrorX;
}
