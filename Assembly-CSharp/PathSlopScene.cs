using System;
using UnityEngine;

// Token: 0x02000707 RID: 1799
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class PathSlopScene : MonoBehaviour
{
	// Token: 0x17000B87 RID: 2951
	// (get) Token: 0x06003BF0 RID: 15344 RVA: 0x000D5E48 File Offset: 0x000D4048
	public MeshFilter filter
	{
		get
		{
			return base.GetComponent<MeshFilter>();
		}
	}

	// Token: 0x17000B88 RID: 2952
	// (get) Token: 0x06003BF1 RID: 15345 RVA: 0x000D5E50 File Offset: 0x000D4050
	public MeshRenderer renderer
	{
		get
		{
			return (MeshRenderer)base.renderer;
		}
	}

	// Token: 0x17000B89 RID: 2953
	// (get) Token: 0x06003BF2 RID: 15346 RVA: 0x000D5E60 File Offset: 0x000D4060
	public static global::PathSlopScene current
	{
		get
		{
			return null;
		}
	}

	// Token: 0x04001E11 RID: 7697
	public Vector4[] sloppymess;

	// Token: 0x04001E12 RID: 7698
	public float initialWidth = 1f;

	// Token: 0x04001E13 RID: 7699
	public float areaGrid = 8f;

	// Token: 0x04001E14 RID: 7700
	public float pushup = 0.05f;

	// Token: 0x04001E15 RID: 7701
	public LayerMask layerMask = 1;
}
