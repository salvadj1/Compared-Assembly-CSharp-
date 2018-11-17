using System;
using UnityEngine;

// Token: 0x02000551 RID: 1361
public class CustomMeshMaker : ScriptableObject
{
	// Token: 0x0400175A RID: 5978
	public Vector3[] vertices;

	// Token: 0x0400175B RID: 5979
	public Vector3[] normals;

	// Token: 0x0400175C RID: 5980
	public Vector4[] tangents;

	// Token: 0x0400175D RID: 5981
	public Color[] colors;

	// Token: 0x0400175E RID: 5982
	public Vector2[] uv1;

	// Token: 0x0400175F RID: 5983
	public Vector2[] uv2;

	// Token: 0x04001760 RID: 5984
	public int[] triangles;

	// Token: 0x04001761 RID: 5985
	public Bounds bounds;

	// Token: 0x04001762 RID: 5986
	public bool optimize;

	// Token: 0x04001763 RID: 5987
	public bool autoBound;

	// Token: 0x04001764 RID: 5988
	public bool autoNormals;

	// Token: 0x04001765 RID: 5989
	public string output;
}
