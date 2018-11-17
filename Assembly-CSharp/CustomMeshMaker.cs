using System;
using UnityEngine;

// Token: 0x02000496 RID: 1174
public class CustomMeshMaker : ScriptableObject
{
	// Token: 0x0400159D RID: 5533
	public Vector3[] vertices;

	// Token: 0x0400159E RID: 5534
	public Vector3[] normals;

	// Token: 0x0400159F RID: 5535
	public Vector4[] tangents;

	// Token: 0x040015A0 RID: 5536
	public Color[] colors;

	// Token: 0x040015A1 RID: 5537
	public Vector2[] uv1;

	// Token: 0x040015A2 RID: 5538
	public Vector2[] uv2;

	// Token: 0x040015A3 RID: 5539
	public int[] triangles;

	// Token: 0x040015A4 RID: 5540
	public Bounds bounds;

	// Token: 0x040015A5 RID: 5541
	public bool optimize;

	// Token: 0x040015A6 RID: 5542
	public bool autoBound;

	// Token: 0x040015A7 RID: 5543
	public bool autoNormals;

	// Token: 0x040015A8 RID: 5544
	public string output;
}
