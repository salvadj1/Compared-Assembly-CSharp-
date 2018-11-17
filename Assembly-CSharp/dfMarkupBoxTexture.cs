using System;
using UnityEngine;

// Token: 0x020007F1 RID: 2033
public class dfMarkupBoxTexture : global::dfMarkupBox
{
	// Token: 0x060046BD RID: 18109 RVA: 0x0010AF6C File Offset: 0x0010916C
	public dfMarkupBoxTexture(global::dfMarkupElement element, global::dfMarkupDisplayType display, global::dfMarkupStyle style) : base(element, display, style)
	{
	}

	// Token: 0x17000D9A RID: 3482
	// (get) Token: 0x060046BF RID: 18111 RVA: 0x0010AF9C File Offset: 0x0010919C
	// (set) Token: 0x060046C0 RID: 18112 RVA: 0x0010AFA4 File Offset: 0x001091A4
	public Texture Texture { get; set; }

	// Token: 0x060046C1 RID: 18113 RVA: 0x0010AFB0 File Offset: 0x001091B0
	internal void LoadTexture(Texture texture)
	{
		if (texture == null)
		{
			throw new InvalidOperationException();
		}
		this.Texture = texture;
		this.Size = new Vector2((float)texture.width, (float)texture.height);
		this.Baseline = (int)this.Size.y;
	}

	// Token: 0x060046C2 RID: 18114 RVA: 0x0010B004 File Offset: 0x00109204
	protected override global::dfRenderData OnRebuildRenderData()
	{
		this.renderData.Clear();
		this.ensureMaterial();
		this.renderData.Material = this.material;
		this.renderData.Material.mainTexture = this.Texture;
		Vector3 zero = Vector3.zero;
		Vector3 vector = zero + Vector3.right * this.Size.x;
		Vector3 item = vector + Vector3.down * this.Size.y;
		Vector3 item2 = zero + Vector3.down * this.Size.y;
		this.renderData.Vertices.Add(zero);
		this.renderData.Vertices.Add(vector);
		this.renderData.Vertices.Add(item);
		this.renderData.Vertices.Add(item2);
		this.renderData.Triangles.AddRange(global::dfMarkupBoxTexture.TRIANGLE_INDICES);
		this.renderData.UV.Add(new Vector2(0f, 1f));
		this.renderData.UV.Add(new Vector2(1f, 1f));
		this.renderData.UV.Add(new Vector2(1f, 0f));
		this.renderData.UV.Add(new Vector2(0f, 0f));
		Color color = this.Style.Color;
		this.renderData.Colors.Add(color);
		this.renderData.Colors.Add(color);
		this.renderData.Colors.Add(color);
		this.renderData.Colors.Add(color);
		return this.renderData;
	}

	// Token: 0x060046C3 RID: 18115 RVA: 0x0010B1E8 File Offset: 0x001093E8
	private void ensureMaterial()
	{
		if (this.material != null || this.Texture == null)
		{
			return;
		}
		Shader shader = Shader.Find("Daikon Forge/Default UI Shader");
		if (shader == null)
		{
			Debug.LogError("Failed to find default shader");
			return;
		}
		this.material = new Material(shader)
		{
			name = "Default Texture Shader",
			hideFlags = 4,
			mainTexture = this.Texture
		};
	}

	// Token: 0x060046C4 RID: 18116 RVA: 0x0010B268 File Offset: 0x00109468
	private static void addTriangleIndices(global::dfList<Vector3> verts, global::dfList<int> triangles)
	{
		int count = verts.Count;
		int[] triangle_INDICES = global::dfMarkupBoxTexture.TRIANGLE_INDICES;
		for (int i = 0; i < triangle_INDICES.Length; i++)
		{
			triangles.Add(count + triangle_INDICES[i]);
		}
	}

	// Token: 0x0400252A RID: 9514
	private static int[] TRIANGLE_INDICES = new int[]
	{
		0,
		1,
		2,
		0,
		2,
		3
	};

	// Token: 0x0400252B RID: 9515
	private global::dfRenderData renderData = new global::dfRenderData(32);

	// Token: 0x0400252C RID: 9516
	private Material material;
}
