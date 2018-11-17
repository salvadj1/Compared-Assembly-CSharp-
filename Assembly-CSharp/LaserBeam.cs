using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000601 RID: 1537
[ExecuteInEditMode]
public sealed class LaserBeam : MonoBehaviour
{
	// Token: 0x060036F0 RID: 14064 RVA: 0x000C5C94 File Offset: 0x000C3E94
	public static List<LaserBeam> Collect()
	{
		LaserBeam.g.currentRendering.Clear();
		LaserBeam.g.currentRendering.AddRange(LaserBeam.g.allActiveBeams);
		return LaserBeam.g.currentRendering;
	}

	// Token: 0x060036F1 RID: 14065 RVA: 0x000C5CB4 File Offset: 0x000C3EB4
	private void OnEnable()
	{
		LaserBeam.g.allActiveBeams.Add(this);
		LaserGraphics.EnsureGraphicsExist();
	}

	// Token: 0x060036F2 RID: 14066 RVA: 0x000C5CC8 File Offset: 0x000C3EC8
	private void OnDisable()
	{
		LaserBeam.g.allActiveBeams.Remove(this);
	}

	// Token: 0x04001AFD RID: 6909
	public float beamMaxDistance = 100f;

	// Token: 0x04001AFE RID: 6910
	public Vector4 beamColor = Color.red;

	// Token: 0x04001AFF RID: 6911
	public float beamOutput = 1f;

	// Token: 0x04001B00 RID: 6912
	public float beamWidthStart = 0.1f;

	// Token: 0x04001B01 RID: 6913
	public float beamWidthEnd = 0.2f;

	// Token: 0x04001B02 RID: 6914
	public float dotRadiusStart = 0.15f;

	// Token: 0x04001B03 RID: 6915
	public float dotRadiusEnd = 0.25f;

	// Token: 0x04001B04 RID: 6916
	public bool isViewModel;

	// Token: 0x04001B05 RID: 6917
	public Vector4 dotColor = Color.red;

	// Token: 0x04001B06 RID: 6918
	public Material beamMaterial;

	// Token: 0x04001B07 RID: 6919
	public Material dotMaterial;

	// Token: 0x04001B08 RID: 6920
	public LayerMask beamLayers = 1;

	// Token: 0x04001B09 RID: 6921
	public LayerMask cullLayers = 1;

	// Token: 0x04001B0A RID: 6922
	public LaserBeam.FrameData frame;

	// Token: 0x02000602 RID: 1538
	public struct Quad<T>
	{
		// Token: 0x04001B0B RID: 6923
		public T m0;

		// Token: 0x04001B0C RID: 6924
		public T m1;

		// Token: 0x04001B0D RID: 6925
		public T m2;

		// Token: 0x04001B0E RID: 6926
		public T m3;
	}

	// Token: 0x02000603 RID: 1539
	public struct FrameData
	{
		// Token: 0x04001B0F RID: 6927
		public MaterialPropertyBlock block;

		// Token: 0x04001B10 RID: 6928
		public Bounds bounds;

		// Token: 0x04001B11 RID: 6929
		public bool hit;

		// Token: 0x04001B12 RID: 6930
		public Vector3 hitPoint;

		// Token: 0x04001B13 RID: 6931
		public Vector3 hitNormal;

		// Token: 0x04001B14 RID: 6932
		public LaserBeam.Quad<Vector3> beamVertices;

		// Token: 0x04001B15 RID: 6933
		public LaserBeam.Quad<Vector3> beamNormals;

		// Token: 0x04001B16 RID: 6934
		public LaserBeam.Quad<Vector2> beamUVs;

		// Token: 0x04001B17 RID: 6935
		public LaserBeam.Quad<Vector3> dotVertices1;

		// Token: 0x04001B18 RID: 6936
		public LaserBeam.Quad<Vector3> dotVertices2;

		// Token: 0x04001B19 RID: 6937
		public LaserBeam.Quad<Color> beamColor;

		// Token: 0x04001B1A RID: 6938
		public LaserBeam.Quad<Color> dotColor1;

		// Token: 0x04001B1B RID: 6939
		public LaserBeam.Quad<Color> dotColor2;

		// Token: 0x04001B1C RID: 6940
		public Vector3 direction;

		// Token: 0x04001B1D RID: 6941
		public Vector3 origin;

		// Token: 0x04001B1E RID: 6942
		public Vector3 point;

		// Token: 0x04001B1F RID: 6943
		public float distance;

		// Token: 0x04001B20 RID: 6944
		public float distanceFraction;

		// Token: 0x04001B21 RID: 6945
		public float pointWidth;

		// Token: 0x04001B22 RID: 6946
		public float originWidth;

		// Token: 0x04001B23 RID: 6947
		public float dotRadius;

		// Token: 0x04001B24 RID: 6948
		public bool didHit;

		// Token: 0x04001B25 RID: 6949
		public bool drawDot;

		// Token: 0x04001B26 RID: 6950
		public int beamsLayer;

		// Token: 0x04001B27 RID: 6951
		internal LaserGraphics.MeshBuffer bufBeam;

		// Token: 0x04001B28 RID: 6952
		internal LaserGraphics.MeshBuffer bufDot;
	}

	// Token: 0x02000604 RID: 1540
	private static class g
	{
		// Token: 0x04001B29 RID: 6953
		public static HashSet<LaserBeam> allActiveBeams = new HashSet<LaserBeam>();

		// Token: 0x04001B2A RID: 6954
		public static List<LaserBeam> currentRendering = new List<LaserBeam>();
	}
}
