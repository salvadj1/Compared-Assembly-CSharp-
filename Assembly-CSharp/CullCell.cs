using System;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x020002DE RID: 734
public class CullCell : MonoBehaviour
{
	// Token: 0x06001992 RID: 6546 RVA: 0x00062674 File Offset: 0x00060874
	private static float HeightCast(Vector2 point)
	{
		RaycastHit raycastHit;
		return (!Physics.Raycast(new Vector3(point.x, 5000f, point.y), Vector3.down, ref raycastHit, float.PositiveInfinity, global::CullCell.g.terrainMask)) ? 0f : raycastHit.point.y;
	}

	// Token: 0x06001993 RID: 6547 RVA: 0x000626D0 File Offset: 0x000608D0
	private void uLink_OnNetworkInstantiate(uLink.NetworkMessageInfo info)
	{
		uLink.NetworkView networkView = info.networkView;
		this.groupID = info.networkView.group;
		this.center = global::CullGrid.Flat(networkView.position);
		this.size = networkView.position.y;
		this.extent = this.size / 2f;
		ushort num;
		ushort num2;
		global::CullGrid.CellFromGroupID(this.groupID, out num, out num2);
		base.name = string.Format("GRID-CELL:{0:00000}-[{1},{2}]", this.groupID, num, num2);
		this.y_mc = global::CullCell.HeightCast(this.center);
		this.y_xy = global::CullCell.HeightCast(new Vector2(this.center.x - this.extent, this.center.y - this.extent));
		this.y_XY = global::CullCell.HeightCast(new Vector2(this.center.x + this.extent, this.center.y + this.extent));
		this.y_Xy = global::CullCell.HeightCast(new Vector2(this.center.x + this.extent, this.center.y - this.extent));
		this.y_xY = global::CullCell.HeightCast(new Vector2(this.center.x - this.extent, this.center.y + this.extent));
		this.y_xc = global::CullCell.HeightCast(new Vector2(this.center.x - this.extent, this.center.y));
		this.y_Xc = global::CullCell.HeightCast(new Vector2(this.center.x + this.extent, this.center.y));
		this.y_my = global::CullCell.HeightCast(new Vector2(this.center.x, this.center.y - this.extent));
		this.y_mY = global::CullCell.HeightCast(new Vector2(this.center.x, this.center.y + this.extent));
		base.transform.position = new Vector3(this.center.x, this.y_mc, this.center.y);
		float num3 = Mathf.Min(new float[]
		{
			this.y_xy,
			this.y_XY,
			this.y_Xy,
			this.y_xY,
			this.y_xc,
			this.y_Xc,
			this.y_my,
			this.y_mY,
			this.y_mc
		});
		float num4 = Mathf.Max(new float[]
		{
			this.y_xy,
			this.y_XY,
			this.y_Xy,
			this.y_xY,
			this.y_xc,
			this.y_Xc,
			this.y_my,
			this.y_mY,
			this.y_mc
		});
		float num5 = num4 - num3;
		this.bounds = new Bounds(new Vector3(this.center.x, num3 + num5 * 0.5f, this.center.y), new Vector3(this.size, num5, this.size));
		Transform transform = base.transform;
		this.t_xy = transform.FindChild("BL");
		this.t_XY = transform.FindChild("FR");
		this.t_Xy = transform.FindChild("BR");
		this.t_xY = transform.FindChild("FL");
		this.t_xc = transform.FindChild("ML");
		this.t_Xc = transform.FindChild("MR");
		this.t_my = transform.FindChild("BC");
		this.t_mY = transform.FindChild("FC");
		this.t_mc = transform.FindChild("MC");
		this.t_xy.position = new Vector3(this.center.x - this.extent, this.y_xy, this.center.y - this.extent);
		this.t_XY.position = new Vector3(this.center.x + this.extent, this.y_XY, this.center.y + this.extent);
		this.t_Xy.position = new Vector3(this.center.x + this.extent, this.y_Xy, this.center.y - this.extent);
		this.t_xY.position = new Vector3(this.center.x - this.extent, this.y_xY, this.center.y + this.extent);
		this.t_xc.position = new Vector3(this.center.x - this.extent, this.y_xc, this.center.y);
		this.t_Xc.position = new Vector3(this.center.x + this.extent, this.y_Xc, this.center.y);
		this.t_my.position = new Vector3(this.center.x, this.y_my, this.center.y - this.extent);
		this.t_mY.position = new Vector3(this.center.x, this.y_mY, this.center.y + this.extent);
		this.t_mc.position = new Vector3(this.center.x, this.y_mc, this.center.y);
		transform.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().localBounds = new Bounds(new Vector3(0f, this.y_mc - (num3 + num5 * 0.5f), 0f), new Vector3(this.size, num5, this.size));
	}

	// Token: 0x1700074D RID: 1869
	// (get) Token: 0x06001994 RID: 6548 RVA: 0x00062D04 File Offset: 0x00060F04
	public static Quaternion instantiateRotation
	{
		get
		{
			return new Quaternion(0f, 0.7071068f, 0.7071068f, -0f);
		}
	}

	// Token: 0x06001995 RID: 6549 RVA: 0x00062D20 File Offset: 0x00060F20
	private void OnGUI()
	{
		if (Event.current.type != 7)
		{
			return;
		}
		Vector3 position = this.t_mc.position;
		Camera main = Camera.main;
		if (main)
		{
			Vector3 vector = main.WorldToScreenPoint(position);
			if (vector.z > 0f && vector.z < 150f)
			{
				Vector2 vector2 = GUIUtility.ScreenToGUIPoint(vector);
				vector2.y = (float)Screen.height - (vector2.y + 1f);
				if (vector.z > 10f)
				{
					GUI.color *= new Color(1f, 1f, 1f, 1f - (vector.z - 10f) / 140f);
				}
				Rect rect;
				rect..ctor(vector2.x - 64f, vector2.y - 12f, 128f, 24f);
				if (string.IsNullOrEmpty(this.groupString))
				{
					this.groupString = base.networkView.group.ToString();
				}
				GUI.Label(rect, this.groupString);
			}
		}
	}

	// Token: 0x04000DF7 RID: 3575
	private const float kMaxDistance = 150f;

	// Token: 0x04000DF8 RID: 3576
	private const float kFadeDistance = 10f;

	// Token: 0x04000DF9 RID: 3577
	[NonSerialized]
	public int groupID;

	// Token: 0x04000DFA RID: 3578
	[NonSerialized]
	public Vector2 center;

	// Token: 0x04000DFB RID: 3579
	[NonSerialized]
	public float extent;

	// Token: 0x04000DFC RID: 3580
	[NonSerialized]
	public float size;

	// Token: 0x04000DFD RID: 3581
	[NonSerialized]
	public float y_xy;

	// Token: 0x04000DFE RID: 3582
	[NonSerialized]
	public float y_Xy;

	// Token: 0x04000DFF RID: 3583
	[NonSerialized]
	public float y_xY;

	// Token: 0x04000E00 RID: 3584
	[NonSerialized]
	public float y_XY;

	// Token: 0x04000E01 RID: 3585
	[NonSerialized]
	public float y_mc;

	// Token: 0x04000E02 RID: 3586
	[NonSerialized]
	public float y_my;

	// Token: 0x04000E03 RID: 3587
	[NonSerialized]
	public float y_mY;

	// Token: 0x04000E04 RID: 3588
	[NonSerialized]
	public float y_xc;

	// Token: 0x04000E05 RID: 3589
	[NonSerialized]
	public float y_Xc;

	// Token: 0x04000E06 RID: 3590
	[NonSerialized]
	public Bounds bounds;

	// Token: 0x04000E07 RID: 3591
	[NonSerialized]
	private Transform t_xy;

	// Token: 0x04000E08 RID: 3592
	[NonSerialized]
	private Transform t_XY;

	// Token: 0x04000E09 RID: 3593
	[NonSerialized]
	private Transform t_Xy;

	// Token: 0x04000E0A RID: 3594
	[NonSerialized]
	private Transform t_xY;

	// Token: 0x04000E0B RID: 3595
	[NonSerialized]
	private Transform t_xc;

	// Token: 0x04000E0C RID: 3596
	[NonSerialized]
	private Transform t_Xc;

	// Token: 0x04000E0D RID: 3597
	[NonSerialized]
	private Transform t_my;

	// Token: 0x04000E0E RID: 3598
	[NonSerialized]
	private Transform t_mY;

	// Token: 0x04000E0F RID: 3599
	[NonSerialized]
	private Transform t_mc;

	// Token: 0x04000E10 RID: 3600
	private string groupString;

	// Token: 0x020002DF RID: 735
	private static class g
	{
		// Token: 0x04000E11 RID: 3601
		public static readonly int terrainMask = 1 << LayerMask.NameToLayer("Terrain");
	}
}
