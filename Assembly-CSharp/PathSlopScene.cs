using System;
using UnityEngine;

// Token: 0x02000644 RID: 1604
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class PathSlopScene : MonoBehaviour
{
	// Token: 0x17000B07 RID: 2823
	// (get) Token: 0x06003804 RID: 14340 RVA: 0x000CD598 File Offset: 0x000CB798
	public MeshFilter filter
	{
		get
		{
			return base.GetComponent<MeshFilter>();
		}
	}

	// Token: 0x17000B08 RID: 2824
	// (get) Token: 0x06003805 RID: 14341 RVA: 0x000CD5A0 File Offset: 0x000CB7A0
	public MeshRenderer renderer
	{
		get
		{
			return (MeshRenderer)base.renderer;
		}
	}

	// Token: 0x17000B09 RID: 2825
	// (get) Token: 0x06003806 RID: 14342 RVA: 0x000CD5B0 File Offset: 0x000CB7B0
	public static PathSlopScene current
	{
		get
		{
			return null;
		}
	}

	// Token: 0x04001C1C RID: 7196
	public Vector4[] sloppymess;

	// Token: 0x04001C1D RID: 7197
	public float initialWidth = 1f;

	// Token: 0x04001C1E RID: 7198
	public float areaGrid = 8f;

	// Token: 0x04001C1F RID: 7199
	public float pushup = 0.05f;

	// Token: 0x04001C20 RID: 7200
	public LayerMask layerMask = 1;
}
