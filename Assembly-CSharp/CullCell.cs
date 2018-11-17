using System;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x020002A1 RID: 673
public class CullCell : MonoBehaviour
{
	// Token: 0x06001802 RID: 6146 RVA: 0x0005DD00 File Offset: 0x0005BF00
	private static float HeightCast(Vector2 point)
	{
		RaycastHit raycastHit;
		return (!Physics.Raycast(new Vector3(point.x, 5000f, point.y), Vector3.down, ref raycastHit, float.PositiveInfinity, CullCell.g.terrainMask)) ? 0f : raycastHit.point.y;
	}

	// Token: 0x06001803 RID: 6147 RVA: 0x0005DD5C File Offset: 0x0005BF5C
	private void uLink_OnNetworkInstantiate(NetworkMessageInfo info)
	{
		NetworkView networkView = info.networkView;
		this.groupID = info.networkView.group;
		this.center = CullGrid.Flat(networkView.position);
		this.size = networkView.position.y;
		this.extent = this.size / 2f;
		ushort num;
		ushort num2;
		CullGrid.CellFromGroupID(this.groupID, out num, out num2);
		base.name = string.Format("GRID-CELL:{0:00000}-[{1},{2}]", this.groupID, num, num2);
		this.y_mc = CullCell.HeightCast(this.center);
		this.y_xy = CullCell.HeightCast(new Vector2(this.center.x - this.extent, this.center.y - this.extent));
		this.y_XY = CullCell.HeightCast(new Vector2(this.center.x + this.extent, this.center.y + this.extent));
		this.y_Xy = CullCell.HeightCast(new Vector2(this.center.x + this.extent, this.center.y - this.extent));
		this.y_xY = CullCell.HeightCast(new Vector2(this.center.x - this.extent, this.center.y + this.extent));
		this.y_xc = CullCell.HeightCast(new Vector2(this.center.x - this.extent, this.center.y));
		this.y_Xc = CullCell.HeightCast(new Vector2(this.center.x + this.extent, this.center.y));
		this.y_my = CullCell.HeightCast(new Vector2(this.center.x, this.center.y - this.extent));
		this.y_mY = CullCell.HeightCast(new Vector2(this.center.x, this.center.y + this.extent));
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

	// Token: 0x170006F9 RID: 1785
	// (get) Token: 0x06001804 RID: 6148 RVA: 0x0005E390 File Offset: 0x0005C590
	public static Quaternion instantiateRotation
	{
		get
		{
			return new Quaternion(0f, 0.7071068f, 0.7071068f, -0f);
		}
	}

	// Token: 0x06001805 RID: 6149 RVA: 0x0005E3AC File Offset: 0x0005C5AC
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

	// Token: 0x04000CBC RID: 3260
	private const float kMaxDistance = 150f;

	// Token: 0x04000CBD RID: 3261
	private const float kFadeDistance = 10f;

	// Token: 0x04000CBE RID: 3262
	[NonSerialized]
	public int groupID;

	// Token: 0x04000CBF RID: 3263
	[NonSerialized]
	public Vector2 center;

	// Token: 0x04000CC0 RID: 3264
	[NonSerialized]
	public float extent;

	// Token: 0x04000CC1 RID: 3265
	[NonSerialized]
	public float size;

	// Token: 0x04000CC2 RID: 3266
	[NonSerialized]
	public float y_xy;

	// Token: 0x04000CC3 RID: 3267
	[NonSerialized]
	public float y_Xy;

	// Token: 0x04000CC4 RID: 3268
	[NonSerialized]
	public float y_xY;

	// Token: 0x04000CC5 RID: 3269
	[NonSerialized]
	public float y_XY;

	// Token: 0x04000CC6 RID: 3270
	[NonSerialized]
	public float y_mc;

	// Token: 0x04000CC7 RID: 3271
	[NonSerialized]
	public float y_my;

	// Token: 0x04000CC8 RID: 3272
	[NonSerialized]
	public float y_mY;

	// Token: 0x04000CC9 RID: 3273
	[NonSerialized]
	public float y_xc;

	// Token: 0x04000CCA RID: 3274
	[NonSerialized]
	public float y_Xc;

	// Token: 0x04000CCB RID: 3275
	[NonSerialized]
	public Bounds bounds;

	// Token: 0x04000CCC RID: 3276
	[NonSerialized]
	private Transform t_xy;

	// Token: 0x04000CCD RID: 3277
	[NonSerialized]
	private Transform t_XY;

	// Token: 0x04000CCE RID: 3278
	[NonSerialized]
	private Transform t_Xy;

	// Token: 0x04000CCF RID: 3279
	[NonSerialized]
	private Transform t_xY;

	// Token: 0x04000CD0 RID: 3280
	[NonSerialized]
	private Transform t_xc;

	// Token: 0x04000CD1 RID: 3281
	[NonSerialized]
	private Transform t_Xc;

	// Token: 0x04000CD2 RID: 3282
	[NonSerialized]
	private Transform t_my;

	// Token: 0x04000CD3 RID: 3283
	[NonSerialized]
	private Transform t_mY;

	// Token: 0x04000CD4 RID: 3284
	[NonSerialized]
	private Transform t_mc;

	// Token: 0x04000CD5 RID: 3285
	private string groupString;

	// Token: 0x020002A2 RID: 674
	private static class g
	{
		// Token: 0x04000CD6 RID: 3286
		public static readonly int terrainMask = 1 << LayerMask.NameToLayer("Terrain");
	}
}
