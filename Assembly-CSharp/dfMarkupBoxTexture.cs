using System;
using UnityEngine;

// Token: 0x02000715 RID: 1813
public class dfMarkupBoxTexture : dfMarkupBox
{
	// Token: 0x06004279 RID: 17017 RVA: 0x00101C5C File Offset: 0x000FFE5C
	public dfMarkupBoxTexture(dfMarkupElement element, dfMarkupDisplayType display, dfMarkupStyle style) : base(element, display, style)
	{
	}

	// Token: 0x17000D10 RID: 3344
	// (get) Token: 0x0600427B RID: 17019 RVA: 0x00101C8C File Offset: 0x000FFE8C
	// (set) Token: 0x0600427C RID: 17020 RVA: 0x00101C94 File Offset: 0x000FFE94
	public Texture Texture { get; set; }

	// Token: 0x0600427D RID: 17021 RVA: 0x00101CA0 File Offset: 0x000FFEA0
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

	// Token: 0x0600427E RID: 17022 RVA: 0x00101CF4 File Offset: 0x000FFEF4
	protected override dfRenderData OnRebuildRenderData()
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
		this.renderData.Triangles.AddRange(dfMarkupBoxTexture.TRIANGLE_INDICES);
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

	// Token: 0x0600427F RID: 17023 RVA: 0x00101ED8 File Offset: 0x001000D8
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

	// Token: 0x06004280 RID: 17024 RVA: 0x00101F58 File Offset: 0x00100158
	private static void addTriangleIndices(dfList<Vector3> verts, dfList<int> triangles)
	{
		int count = verts.Count;
		int[] triangle_INDICES = dfMarkupBoxTexture.TRIANGLE_INDICES;
		for (int i = 0; i < triangle_INDICES.Length; i++)
		{
			triangles.Add(count + triangle_INDICES[i]);
		}
	}

	// Token: 0x04002307 RID: 8967
	private static int[] TRIANGLE_INDICES = new int[]
	{
		0,
		1,
		2,
		0,
		2,
		3
	};

	// Token: 0x04002308 RID: 8968
	private dfRenderData renderData = new dfRenderData(32);

	// Token: 0x04002309 RID: 8969
	private Material material;
}
