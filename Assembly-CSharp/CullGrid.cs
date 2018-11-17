using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using uLink;
using UnityEngine;

// Token: 0x020002E1 RID: 737
public class CullGrid : MonoBehaviour
{
	// Token: 0x1700074E RID: 1870
	// (get) Token: 0x0600199F RID: 6559 RVA: 0x000632F4 File Offset: 0x000614F4
	public static bool autoPrebindInInstantiate
	{
		get
		{
			return global::CullGrid.has_grid && global::CullGrid.cull_prebinding;
		}
	}

	// Token: 0x060019A0 RID: 6560 RVA: 0x00063308 File Offset: 0x00061508
	private Vector3 GetCenterSetup(int cell)
	{
		global::CullGridSetup cullGridSetup = this.setup;
		return base.transform.position + base.transform.forward * (((float)(cell / cullGridSetup.cellsWide) - ((float)cullGridSetup.cellsTall / 2f - (float)(2 - (cullGridSetup.cellsTall & 1)) / 2f)) * (float)cullGridSetup.cellSquareDimension) + base.transform.right * (((float)(cell % cullGridSetup.cellsWide) - ((float)cullGridSetup.cellsWide / 2f - (float)(2 - (cullGridSetup.cellsWide & 1)) / 2f)) * (float)cullGridSetup.cellSquareDimension);
	}

	// Token: 0x060019A1 RID: 6561 RVA: 0x000633B4 File Offset: 0x000615B4
	private void DrawGrid(int cell)
	{
		if (cell != -1)
		{
			this.DrawGrid(this.GetCenterSetup(cell));
		}
	}

	// Token: 0x060019A2 RID: 6562 RVA: 0x000633CC File Offset: 0x000615CC
	private void DrawGrid(int centerCell, int xOffset, int yOffset)
	{
		this.DrawGrid(centerCell + xOffset + this.setup.cellsWide * 2 * yOffset);
	}

	// Token: 0x060019A3 RID: 6563 RVA: 0x000633E8 File Offset: 0x000615E8
	private void DrawGrid(Vector3 center)
	{
		Vector3 vector = base.transform.right * ((float)this.setup.cellSquareDimension / 2f);
		Vector3 vector2 = base.transform.forward * ((float)this.setup.cellSquareDimension / 2f);
		global::CullGrid.DrawQuadRayCastDown(center + vector + vector2, center + vector - vector2, center - vector - vector2, center - vector + vector2);
	}

	// Token: 0x060019A4 RID: 6564 RVA: 0x00063474 File Offset: 0x00061674
	private void DrawGrid(Vector3 center, float sizeX, float sizeY)
	{
		Vector3 vector = base.transform.right * (sizeX / 2f);
		Vector3 vector2 = base.transform.forward * (sizeY / 2f);
		global::CullGrid.DrawQuadRayCastDown(center + vector + vector2, center + vector - vector2, center - vector - vector2, center - vector + vector2);
	}

	// Token: 0x060019A5 RID: 6565 RVA: 0x000634EC File Offset: 0x000616EC
	private static void RegisterGrid(global::CullGrid grid)
	{
		if (grid)
		{
			global::CullGrid.grid = new global::CullGrid.CullGridRuntime(grid);
			global::CullGrid.has_grid = true;
		}
	}

	// Token: 0x060019A6 RID: 6566 RVA: 0x0006350C File Offset: 0x0006170C
	private void Awake()
	{
		global::CullGrid.RegisterGrid(this);
	}

	// Token: 0x060019A7 RID: 6567 RVA: 0x00063514 File Offset: 0x00061714
	public static ushort CellFromGroupID(int groupID)
	{
		if (groupID < global::CullGrid.grid.groupBegin || groupID >= global::CullGrid.grid.groupEnd)
		{
			throw new ArgumentOutOfRangeException("groupID", groupID, "groupID < grid.groupBegin || groupID >= grid.groupEnd");
		}
		return (ushort)(groupID - global::CullGrid.grid.groupBegin);
	}

	// Token: 0x060019A8 RID: 6568 RVA: 0x00063564 File Offset: 0x00061764
	public static ushort CellFromGroupID(int groupID, out ushort x, out ushort y)
	{
		ushort num = global::CullGrid.CellFromGroupID(groupID);
		x = (ushort)((int)num % global::CullGrid.grid.cellsWide);
		y = (ushort)((int)num / global::CullGrid.grid.cellsWide);
		return num;
	}

	// Token: 0x060019A9 RID: 6569 RVA: 0x00063598 File Offset: 0x00061798
	public static int GroupIDFromCell(ushort cell)
	{
		if ((int)cell >= global::CullGrid.grid.numCells)
		{
			throw new ArgumentOutOfRangeException("cell", cell, "cell >= grid.numCells");
		}
		return global::CullGrid.grid.groupBegin + (int)cell;
	}

	// Token: 0x1700074F RID: 1871
	// (get) Token: 0x060019AA RID: 6570 RVA: 0x000635D8 File Offset: 0x000617D8
	public static int Wide
	{
		get
		{
			return global::CullGrid.grid.cellsWide;
		}
	}

	// Token: 0x17000750 RID: 1872
	// (get) Token: 0x060019AB RID: 6571 RVA: 0x000635E4 File Offset: 0x000617E4
	public static int Tall
	{
		get
		{
			return global::CullGrid.grid.cellsTall;
		}
	}

	// Token: 0x060019AC RID: 6572 RVA: 0x000635F0 File Offset: 0x000617F0
	public static bool CellContainsPoint(ushort cell, ref Vector2 flatPoint)
	{
		return cell == global::CullGrid.grid.FlatCell(ref flatPoint);
	}

	// Token: 0x060019AD RID: 6573 RVA: 0x00063600 File Offset: 0x00061800
	public static bool CellContainsPoint(ushort cell, ref Vector2 flatPoint, out ushort cell_point)
	{
		cell_point = global::CullGrid.grid.FlatCell(ref flatPoint);
		return cell == cell_point;
	}

	// Token: 0x060019AE RID: 6574 RVA: 0x00063614 File Offset: 0x00061814
	public static bool CellContainsPoint(ushort cell, ref Vector3 worldPoint)
	{
		return cell == global::CullGrid.grid.WorldCell(ref worldPoint);
	}

	// Token: 0x060019AF RID: 6575 RVA: 0x00063624 File Offset: 0x00061824
	public static bool CellContainsPoint(ushort cell, ref Vector3 worldPoint, out ushort cell_point)
	{
		cell_point = global::CullGrid.grid.WorldCell(ref worldPoint);
		return cell_point == cell;
	}

	// Token: 0x060019B0 RID: 6576 RVA: 0x00063638 File Offset: 0x00061838
	public static bool GroupIDContainsPoint(int groupID, ref Vector2 flatPoint, out int groupID_point)
	{
		if (groupID < global::CullGrid.grid.groupBegin || groupID >= global::CullGrid.grid.groupEnd)
		{
			NetworkGroup unassigned = NetworkGroup.unassigned;
			groupID_point = unassigned.id;
			return false;
		}
		ushort num;
		if (!global::CullGrid.CellContainsPoint((ushort)(groupID - global::CullGrid.grid.groupBegin), ref flatPoint, out num))
		{
			groupID_point = (int)num + global::CullGrid.grid.groupBegin;
			return false;
		}
		groupID_point = groupID;
		return true;
	}

	// Token: 0x060019B1 RID: 6577 RVA: 0x000636A4 File Offset: 0x000618A4
	public static bool GroupIDContainsPoint(int groupID, ref Vector3 worldPoint, out int groupID_point)
	{
		if (groupID < global::CullGrid.grid.groupBegin || groupID >= global::CullGrid.grid.groupEnd)
		{
			NetworkGroup unassigned = NetworkGroup.unassigned;
			groupID_point = unassigned.id;
			return false;
		}
		ushort cell;
		if (!global::CullGrid.CellContainsPoint(global::CullGrid.CellFromGroupID(groupID), ref worldPoint, out cell))
		{
			groupID_point = global::CullGrid.GroupIDFromCell(cell);
			return false;
		}
		groupID_point = groupID;
		return true;
	}

	// Token: 0x060019B2 RID: 6578 RVA: 0x00063704 File Offset: 0x00061904
	public static bool GroupIDContainsPoint(int groupID, ref Vector2 flatPoint)
	{
		return groupID >= global::CullGrid.grid.groupBegin && groupID < global::CullGrid.grid.groupEnd && global::CullGrid.CellContainsPoint((ushort)(groupID - global::CullGrid.grid.groupBegin), ref flatPoint);
	}

	// Token: 0x060019B3 RID: 6579 RVA: 0x00063748 File Offset: 0x00061948
	public static bool GroupIDContainsPoint(int groupID, ref Vector3 worldPoint)
	{
		return groupID >= global::CullGrid.grid.groupBegin && groupID < global::CullGrid.grid.groupEnd && global::CullGrid.CellContainsPoint((ushort)(groupID - global::CullGrid.grid.groupBegin), ref worldPoint);
	}

	// Token: 0x060019B4 RID: 6580 RVA: 0x0006378C File Offset: 0x0006198C
	public static Vector2 Flat(Vector3 triD)
	{
		Vector2 result;
		result.x = triD.x;
		result.y = triD.z;
		return result;
	}

	// Token: 0x060019B5 RID: 6581 RVA: 0x000637B8 File Offset: 0x000619B8
	public static ushort FlatCell(Vector2 flat)
	{
		return global::CullGrid.grid.FlatCell(ref flat);
	}

	// Token: 0x060019B6 RID: 6582 RVA: 0x000637C8 File Offset: 0x000619C8
	public static ushort WorldCell(Vector3 world)
	{
		return global::CullGrid.grid.WorldCell(ref world);
	}

	// Token: 0x060019B7 RID: 6583 RVA: 0x000637D8 File Offset: 0x000619D8
	public static ushort FlatCell(ref Vector2 flat)
	{
		return global::CullGrid.grid.FlatCell(ref flat);
	}

	// Token: 0x060019B8 RID: 6584 RVA: 0x000637E8 File Offset: 0x000619E8
	public static ushort WorldCell(ref Vector3 world)
	{
		return global::CullGrid.grid.WorldCell(ref world);
	}

	// Token: 0x060019B9 RID: 6585 RVA: 0x000637F8 File Offset: 0x000619F8
	public static int FlatGroupID(ref Vector2 flat)
	{
		return (int)global::CullGrid.grid.FlatCell(ref flat) + global::CullGrid.grid.groupBegin;
	}

	// Token: 0x060019BA RID: 6586 RVA: 0x00063810 File Offset: 0x00061A10
	public static int WorldGroupID(ref Vector3 world)
	{
		return (int)global::CullGrid.grid.WorldCell(ref world) + global::CullGrid.grid.groupBegin;
	}

	// Token: 0x060019BB RID: 6587 RVA: 0x00063828 File Offset: 0x00061A28
	private static void RaycastDownVect(ref Vector3 a)
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(new Vector3(a.x, 10000f, a.z), Vector3.down, ref raycastHit, float.PositiveInfinity))
		{
			a = raycastHit.point + Vector3.up * a.y;
		}
	}

	// Token: 0x060019BC RID: 6588 RVA: 0x00063884 File Offset: 0x00061A84
	private static void DrawQuadRayCastDown(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
	{
		global::CullGrid.RaycastDownVect(ref a);
		global::CullGrid.RaycastDownVect(ref b);
		global::CullGrid.RaycastDownVect(ref c);
		global::CullGrid.RaycastDownVect(ref d);
		Gizmos.DrawLine(a, b);
		Gizmos.DrawLine(b, c);
		Gizmos.DrawLine(c, d);
		Gizmos.DrawLine(d, a);
		if (a.y > c.y)
		{
			if (b.y > d.y)
			{
				if (b.y - d.y > a.y - c.y)
				{
					Gizmos.DrawLine(b, d);
				}
				else
				{
					Gizmos.DrawLine(a, c);
				}
			}
			else if (d.y - b.y > a.y - c.y)
			{
				Gizmos.DrawLine(d, b);
			}
			else
			{
				Gizmos.DrawLine(a, c);
			}
		}
		else if (b.y > d.y)
		{
			if (b.y - d.y > c.y - a.y)
			{
				Gizmos.DrawLine(b, d);
			}
			else
			{
				Gizmos.DrawLine(c, a);
			}
		}
		else if (d.y - b.y > c.y - a.y)
		{
			Gizmos.DrawLine(d, b);
		}
		else
		{
			Gizmos.DrawLine(c, a);
		}
	}

	// Token: 0x060019BD RID: 6589 RVA: 0x000639EC File Offset: 0x00061BEC
	private void DrawGizmosNow()
	{
	}

	// Token: 0x060019BE RID: 6590 RVA: 0x000639F0 File Offset: 0x00061BF0
	public static bool IsCellGroupID(int usedGroup)
	{
		return global::CullGrid.has_grid && usedGroup >= global::CullGrid.grid.groupBegin && usedGroup < global::CullGrid.grid.groupEnd;
	}

	// Token: 0x060019BF RID: 6591 RVA: 0x00063A28 File Offset: 0x00061C28
	public static bool IsCellGroup(NetworkGroup group)
	{
		return global::CullGrid.IsCellGroupID(group.id);
	}

	// Token: 0x04000E1A RID: 3610
	private static bool cull_prebinding = true;

	// Token: 0x04000E1B RID: 3611
	[SerializeField]
	private global::CullGridSetup setup;

	// Token: 0x04000E1C RID: 3612
	private static global::CullGrid.CullGridRuntime grid;

	// Token: 0x04000E1D RID: 3613
	private static bool has_grid;

	// Token: 0x020002E2 RID: 738
	[StructLayout(LayoutKind.Explicit, Size = 2)]
	public struct CellID
	{
		// Token: 0x060019C0 RID: 6592 RVA: 0x00063A38 File Offset: 0x00061C38
		public CellID(ushort cellID)
		{
			this.id = cellID;
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x060019C1 RID: 6593 RVA: 0x00063A44 File Offset: 0x00061C44
		public Vector2 flatCenter
		{
			get
			{
				Vector2 result;
				global::CullGrid.grid.GetCenter((int)this.id, out result);
				return result;
			}
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x060019C2 RID: 6594 RVA: 0x00063A64 File Offset: 0x00061C64
		public Vector2 flatMax
		{
			get
			{
				Vector2 result;
				global::CullGrid.grid.GetMin((int)this.id, out result);
				return result;
			}
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x060019C3 RID: 6595 RVA: 0x00063A84 File Offset: 0x00061C84
		public Vector2 flatMin
		{
			get
			{
				Vector2 result;
				global::CullGrid.grid.GetMax((int)this.id, out result);
				return result;
			}
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x060019C4 RID: 6596 RVA: 0x00063AA4 File Offset: 0x00061CA4
		public Rect flatRect
		{
			get
			{
				Rect result;
				global::CullGrid.grid.GetRect((int)this.id, out result);
				return result;
			}
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x060019C5 RID: 6597 RVA: 0x00063AC4 File Offset: 0x00061CC4
		public Vector3 worldCenter
		{
			get
			{
				Vector3 result;
				global::CullGrid.grid.GetCenter((int)this.id, out result);
				return result;
			}
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x060019C6 RID: 6598 RVA: 0x00063AE4 File Offset: 0x00061CE4
		public Vector3 worldMax
		{
			get
			{
				Vector3 result;
				global::CullGrid.grid.GetMin((int)this.id, out result);
				return result;
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x060019C7 RID: 6599 RVA: 0x00063B04 File Offset: 0x00061D04
		public Vector3 worldMin
		{
			get
			{
				Vector3 result;
				global::CullGrid.grid.GetMax((int)this.id, out result);
				return result;
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x060019C8 RID: 6600 RVA: 0x00063B24 File Offset: 0x00061D24
		public Bounds worldBounds
		{
			get
			{
				Bounds result;
				global::CullGrid.grid.GetBounds((int)this.id, out result);
				return result;
			}
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x00063B44 File Offset: 0x00061D44
		public bool ContainsWorldPoint(Vector3 worldPoint)
		{
			return global::CullGrid.grid.Contains((int)this.id, ref worldPoint);
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x00063B58 File Offset: 0x00061D58
		public bool ContainsFlatPoint(Vector2 flatPoint)
		{
			return global::CullGrid.grid.Contains((int)this.id, ref flatPoint);
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x00063B6C File Offset: 0x00061D6C
		public bool ContainsWorldPoint(ref Vector3 worldPoint)
		{
			return this.valid && global::CullGrid.grid.Contains((int)this.id, ref worldPoint);
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x00063B90 File Offset: 0x00061D90
		public bool ContainsFlatPoint(ref Vector2 flatPoint)
		{
			return this.valid && global::CullGrid.grid.Contains((int)this.id, ref flatPoint);
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x060019CD RID: 6605 RVA: 0x00063BB4 File Offset: 0x00061DB4
		public int column
		{
			get
			{
				return (!this.valid) ? -1 : ((int)this.id % global::CullGrid.grid.cellsWide);
			}
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x060019CE RID: 6606 RVA: 0x00063BE4 File Offset: 0x00061DE4
		public int row
		{
			get
			{
				return (!this.valid) ? -1 : ((int)this.id / global::CullGrid.grid.cellsWide);
			}
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x00063C14 File Offset: 0x00061E14
		private static ushort NextRight(ushort id)
		{
			return ((int)id % global::CullGrid.grid.cellsWide != (int)global::CullGrid.grid.cellWideLast) ? (id + 1) : ushort.MaxValue;
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x00063C40 File Offset: 0x00061E40
		private static ushort NextLeft(ushort id)
		{
			return ((int)id % global::CullGrid.grid.cellsWide != 0) ? (id - 1) : ushort.MaxValue;
		}

		// Token: 0x060019D1 RID: 6609 RVA: 0x00063C64 File Offset: 0x00061E64
		private static ushort NextUp(ushort id)
		{
			return ((int)id / global::CullGrid.grid.cellsWide != (int)global::CullGrid.grid.cellTallLast) ? ((ushort)((int)id + global::CullGrid.grid.cellsWide)) : ushort.MaxValue;
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x00063CA4 File Offset: 0x00061EA4
		private static ushort NextDown(ushort id)
		{
			return ((int)id / global::CullGrid.grid.cellsWide != 0) ? ((ushort)((int)id - global::CullGrid.grid.cellsWide)) : ushort.MaxValue;
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x060019D3 RID: 6611 RVA: 0x00063CDC File Offset: 0x00061EDC
		public global::CullGrid.CellID right
		{
			get
			{
				global::CullGrid.CellID result;
				result.id = ((!this.valid) ? ushort.MaxValue : global::CullGrid.CellID.NextRight(this.id));
				return result;
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x060019D4 RID: 6612 RVA: 0x00063D14 File Offset: 0x00061F14
		public global::CullGrid.CellID left
		{
			get
			{
				global::CullGrid.CellID result;
				result.id = ((!this.valid) ? ushort.MaxValue : global::CullGrid.CellID.NextLeft(this.id));
				return result;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x060019D5 RID: 6613 RVA: 0x00063D4C File Offset: 0x00061F4C
		public global::CullGrid.CellID up
		{
			get
			{
				global::CullGrid.CellID result;
				result.id = ((!this.valid) ? ushort.MaxValue : global::CullGrid.CellID.NextUp(this.id));
				return result;
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x060019D6 RID: 6614 RVA: 0x00063D84 File Offset: 0x00061F84
		public global::CullGrid.CellID down
		{
			get
			{
				global::CullGrid.CellID result;
				result.id = ((!this.valid) ? ushort.MaxValue : global::CullGrid.CellID.NextDown(this.id));
				return result;
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x060019D7 RID: 6615 RVA: 0x00063DBC File Offset: 0x00061FBC
		public bool mostRight
		{
			get
			{
				return this.valid && (int)this.id % global::CullGrid.grid.cellsWide == (int)global::CullGrid.grid.cellWideLast;
			}
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x060019D8 RID: 6616 RVA: 0x00063DEC File Offset: 0x00061FEC
		public bool mostLeft
		{
			get
			{
				return this.valid && (int)this.id % global::CullGrid.grid.cellsWide == 0;
			}
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x060019D9 RID: 6617 RVA: 0x00063E1C File Offset: 0x0006201C
		public bool mostTop
		{
			get
			{
				return this.valid && (int)this.id / global::CullGrid.grid.cellsWide == (int)global::CullGrid.grid.cellTallLast;
			}
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x060019DA RID: 6618 RVA: 0x00063E4C File Offset: 0x0006204C
		public bool mostBottom
		{
			get
			{
				return this.valid && (int)this.id / global::CullGrid.grid.cellsWide == 0;
			}
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x060019DB RID: 6619 RVA: 0x00063E7C File Offset: 0x0006207C
		public bool valid
		{
			get
			{
				return (int)this.id < global::CullGrid.grid.numCells;
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x060019DC RID: 6620 RVA: 0x00063E90 File Offset: 0x00062090
		public int groupID
		{
			get
			{
				int result;
				if (this.valid)
				{
					result = global::CullGrid.GroupIDFromCell(this.id);
				}
				else
				{
					NetworkGroup unassigned = NetworkGroup.unassigned;
					result = unassigned.id;
				}
				return result;
			}
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x060019DD RID: 6621 RVA: 0x00063EC8 File Offset: 0x000620C8
		public NetworkGroup group
		{
			get
			{
				return (!this.valid) ? NetworkGroup.unassigned : global::CullGrid.GroupIDFromCell(this.id);
			}
		}

		// Token: 0x04000E1E RID: 3614
		private const ushort kInvalidID = 65535;

		// Token: 0x04000E1F RID: 3615
		[FieldOffset(0)]
		public ushort id;
	}

	// Token: 0x020002E3 RID: 739
	private class CullGridRuntime : global::CullGridSetup
	{
		// Token: 0x060019DE RID: 6622 RVA: 0x00063EF0 File Offset: 0x000620F0
		public CullGridRuntime(global::CullGrid cullGrid) : base(cullGrid.setup)
		{
			this.cullGrid = cullGrid;
			this.transform = cullGrid.transform;
			this.halfCellTall = (double)this.cellsTall / 2.0;
			this.halfCellWide = (double)this.cellsWide / 2.0;
			this.twoMinusOddTall = 2 - (this.cellsTall & 1);
			this.twoMinusOddWide = 2 - (this.cellsWide & 1);
			this.halfTwoMinusOddTall = (double)this.twoMinusOddTall / 2.0;
			this.halfTwoMinusOddWide /= 2.0;
			this.halfCellTallMinusHalfTwoMinusOddTall = this.halfCellTall - this.halfTwoMinusOddTall;
			this.halfCellWideMinusHalfTwoMinusOddWide = this.halfCellWide - this.halfTwoMinusOddWide;
			Vector3 forward = this.transform.forward;
			Vector3 right = this.transform.right;
			Vector3 position = this.transform.position;
			this.fx = (double)forward.x;
			this.fy = (double)forward.y;
			this.fz = (double)forward.z;
			double num = Math.Sqrt(this.fx * this.fx + this.fy * this.fy + this.fz * this.fz);
			this.fx /= num;
			this.fy /= num;
			this.fz /= num;
			this.rx = (double)right.x;
			this.ry = (double)right.y;
			this.rz = (double)right.z;
			num = Math.Sqrt(this.rx * this.rx + this.ry * this.ry + this.rz * this.rz);
			this.rx /= num;
			this.ry /= num;
			this.rz /= num;
			this.px = (double)position.x;
			this.py = (double)position.y;
			this.pz = (double)position.z;
			this.flat_wide_ofs = (double)this.cellSquareDimension * (this.halfCellWide - (double)(1 - (this.cellsWide & 1)) / 2.0);
			this.flat_tall_ofs = (double)this.cellSquareDimension * (this.halfCellTall - (double)(1 - (this.cellsTall & 1)) / 2.0);
			this.cellTallLast = (ushort)(this.cellsTall - 1);
			this.cellWideLast = (ushort)(this.cellsWide - 1);
			this.cellTallLastTimesSquareDimension = (double)this.cellTallLast * (double)this.cellSquareDimension;
			this.cellWideLastTimesSquareDimension = (double)this.cellWideLast * (double)this.cellSquareDimension;
			this.numCells = this.cellsTall * this.cellsWide;
			this.groupEnd = this.groupBegin + this.numCells;
		}

		// Token: 0x060019DF RID: 6623 RVA: 0x000641D4 File Offset: 0x000623D4
		public void GetCenter(int cell, out Vector3 center)
		{
			double num = ((double)(cell % this.cellsWide) - this.halfCellWideMinusHalfTwoMinusOddWide) * (double)this.cellSquareDimension;
			double num2 = ((double)(cell / this.cellsWide) - this.halfCellTallMinusHalfTwoMinusOddTall) * (double)this.cellSquareDimension;
			center.x = (float)(this.px + this.fx * num2 + this.rx * num);
			center.y = (float)(this.py + this.fy * num2 + this.ry * num);
			center.z = (float)(this.pz + this.fz * num2 + this.rz * num);
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x00064270 File Offset: 0x00062470
		public void GetCenter(int cell, out Vector2 center)
		{
			double num = ((double)(cell % this.cellsWide) - this.halfCellWideMinusHalfTwoMinusOddWide) * (double)this.cellSquareDimension;
			double num2 = ((double)(cell / this.cellsWide) - this.halfCellTallMinusHalfTwoMinusOddTall) * (double)this.cellSquareDimension;
			center.x = (float)(this.px + this.fx * num2 + this.rx * num);
			center.y = (float)(this.pz + this.fz * num2 + this.rz * num);
		}

		// Token: 0x060019E1 RID: 6625 RVA: 0x000642F0 File Offset: 0x000624F0
		public void GetCenter(int x, int y, out Vector3 center)
		{
			double num = ((double)x - this.halfCellWideMinusHalfTwoMinusOddWide) * (double)this.cellSquareDimension;
			double num2 = ((double)y - this.halfCellTallMinusHalfTwoMinusOddTall) * (double)this.cellSquareDimension;
			center.x = (float)(this.px + this.fx * num2 + this.rx * num);
			center.y = (float)(this.py + this.fy * num2 + this.ry * num);
			center.z = (float)(this.pz + this.fz * num2 + this.rz * num);
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x00064380 File Offset: 0x00062580
		public void GetCenter(int x, int y, out Vector2 center)
		{
			double num = ((double)x - this.halfCellWideMinusHalfTwoMinusOddWide) * (double)this.cellSquareDimension;
			double num2 = ((double)y - this.halfCellTallMinusHalfTwoMinusOddTall) * (double)this.cellSquareDimension;
			center.x = (float)(this.px + this.fx * num2 + this.rx * num);
			center.y = (float)(this.pz + this.fz * num2 + this.rz * num);
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x000643F0 File Offset: 0x000625F0
		public void GetMin(int cell, out Vector3 min)
		{
			double num = ((double)(cell % this.cellsWide) - this.halfCellWideMinusHalfTwoMinusOddWide - 0.5) * (double)this.cellSquareDimension;
			double num2 = ((double)(cell / this.cellsWide) - this.halfCellTallMinusHalfTwoMinusOddTall - 0.5) * (double)this.cellSquareDimension;
			min.x = (float)(this.px + this.fx * num2 + this.rx * num);
			min.y = (float)(-32000.0 + (this.py + this.fy * num2 + this.ry * num));
			min.z = (float)(this.pz + this.fz * num2 + this.rz * num);
		}

		// Token: 0x060019E4 RID: 6628 RVA: 0x000644AC File Offset: 0x000626AC
		public void GetMin(int cell, out Vector2 min)
		{
			double num = ((double)(cell % this.cellsWide) - this.halfCellWideMinusHalfTwoMinusOddWide - 0.5) * (double)this.cellSquareDimension;
			double num2 = ((double)(cell / this.cellsWide) - this.halfCellTallMinusHalfTwoMinusOddTall - 0.5) * (double)this.cellSquareDimension;
			min.x = (float)(this.px + this.fx * num2 + this.rx * num);
			min.y = (float)(this.pz + this.fz * num2 + this.rz * num);
		}

		// Token: 0x060019E5 RID: 6629 RVA: 0x00064540 File Offset: 0x00062740
		public void GetMin(int x, int y, out Vector3 min)
		{
			double num = ((double)x - this.halfCellWideMinusHalfTwoMinusOddWide - 0.5) * (double)this.cellSquareDimension;
			double num2 = ((double)y - this.halfCellTallMinusHalfTwoMinusOddTall - 0.5) * (double)this.cellSquareDimension;
			min.x = (float)(this.px + this.fx * num2 + this.rx * num);
			min.y = (float)(-32000.0 + (this.py + this.fy * num2 + this.ry * num));
			min.z = (float)(this.pz + this.fz * num2 + this.rz * num);
		}

		// Token: 0x060019E6 RID: 6630 RVA: 0x000645EC File Offset: 0x000627EC
		public void GetMin(int x, int y, out Vector2 min)
		{
			double num = ((double)x - this.halfCellWideMinusHalfTwoMinusOddWide - 0.5) * (double)this.cellSquareDimension;
			double num2 = ((double)y - this.halfCellTallMinusHalfTwoMinusOddTall - 0.5) * (double)this.cellSquareDimension;
			min.x = (float)(this.px + this.fx * num2 + this.rx * num);
			min.y = (float)(this.pz + this.fz * num2 + this.rz * num);
		}

		// Token: 0x060019E7 RID: 6631 RVA: 0x00064670 File Offset: 0x00062870
		public bool Contains(int cell, ref Vector2 flatPoint)
		{
			return cell >= 0 && cell < this.numCells && (int)this.FlatCell(ref flatPoint) == cell;
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x000646A0 File Offset: 0x000628A0
		public bool Contains(int cell, ref Vector3 worldPoint)
		{
			return cell >= 0 && cell < this.numCells && (int)this.WorldCell(ref worldPoint) == cell;
		}

		// Token: 0x060019E9 RID: 6633 RVA: 0x000646D0 File Offset: 0x000628D0
		public bool Contains(int x, int y, ref Vector2 flatPoint)
		{
			return this.Contains(y * this.cellsWide + x, ref flatPoint);
		}

		// Token: 0x060019EA RID: 6634 RVA: 0x000646E4 File Offset: 0x000628E4
		public bool Contains(int x, int y, ref Vector3 worldPoint)
		{
			return this.Contains(y * this.cellsWide + x, ref worldPoint);
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x000646F8 File Offset: 0x000628F8
		public void GetRect(int x, int y, out Rect rect)
		{
			double num = ((double)x - this.halfCellWideMinusHalfTwoMinusOddWide - 0.5) * (double)this.cellSquareDimension;
			double num2 = ((double)y - this.halfCellTallMinusHalfTwoMinusOddTall - 0.5) * (double)this.cellSquareDimension;
			double num3 = num + (double)this.cellSquareDimension;
			double num4 = num2 + (double)this.cellSquareDimension;
			double num5 = this.px + this.fx * num2 + this.rx * num;
			double num6 = this.px + this.fx * num4 + this.ry * num3;
			float num7;
			float num8;
			if (num5 < num6)
			{
				num7 = (float)num5;
				num8 = (float)(num6 - num5);
			}
			else
			{
				num7 = (float)num6;
				num8 = (float)(num5 - num6);
			}
			num5 = this.pz + this.fz * num2 + this.rx * num;
			num6 = this.pz + this.fz * num4 + this.rx * num3;
			float num9;
			float num10;
			if (num5 < num6)
			{
				num9 = (float)num5;
				num10 = (float)(num6 - num5);
			}
			else
			{
				num9 = (float)num6;
				num10 = (float)(num5 - num6);
			}
			rect..ctor(num7, num9, num8, num10);
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x00064818 File Offset: 0x00062A18
		public void GetBounds(int x, int y, out Bounds bounds)
		{
			Vector3 vector;
			this.GetCenter(x, y, out vector);
			bounds..ctor(vector, new Vector3((float)this.cellSquareDimension, 64000f, (float)this.cellSquareDimension));
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x00064850 File Offset: 0x00062A50
		public void GetRect(int cell, out Rect rect)
		{
			int num = cell % this.cellsWide;
			int num2 = cell / this.cellsWide;
			double num3 = ((double)num - this.halfCellWideMinusHalfTwoMinusOddWide - 0.5) * (double)this.cellSquareDimension;
			double num4 = ((double)num2 - this.halfCellTallMinusHalfTwoMinusOddTall - 0.5) * (double)this.cellSquareDimension;
			double num5 = num3 + (double)this.cellSquareDimension;
			double num6 = num4 + (double)this.cellSquareDimension;
			double num7 = this.px + this.fx * num4 + this.rx * num3;
			double num8 = this.px + this.fx * num6 + this.ry * num5;
			float num9;
			float num10;
			if (num7 < num8)
			{
				num9 = (float)num7;
				num10 = (float)(num8 - num7);
			}
			else
			{
				num9 = (float)num8;
				num10 = (float)(num7 - num8);
			}
			num7 = this.pz + this.fz * num4 + this.rx * num3;
			num8 = this.pz + this.fz * num6 + this.rx * num5;
			float num11;
			float num12;
			if (num7 < num8)
			{
				num11 = (float)num7;
				num12 = (float)(num8 - num7);
			}
			else
			{
				num11 = (float)num8;
				num12 = (float)(num7 - num8);
			}
			rect..ctor(num9, num11, num10, num12);
		}

		// Token: 0x060019EE RID: 6638 RVA: 0x00064988 File Offset: 0x00062B88
		public void GetBounds(int cell, out Bounds bounds)
		{
			int x = cell % this.cellsWide;
			int y = cell / this.cellsWide;
			Vector3 vector;
			this.GetCenter(x, y, out vector);
			bounds..ctor(vector, new Vector3((float)this.cellSquareDimension, 64000f, (float)this.cellSquareDimension));
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x000649D0 File Offset: 0x00062BD0
		public void GetMax(int cell, out Vector3 max)
		{
			double num = ((double)(cell % this.cellsWide) - this.halfCellWideMinusHalfTwoMinusOddWide + 0.5) * (double)this.cellSquareDimension;
			double num2 = ((double)(cell / this.cellsWide) - this.halfCellTallMinusHalfTwoMinusOddTall + 0.5) * (double)this.cellSquareDimension;
			max.x = (float)(this.px + this.fx * num2 + this.rx * num);
			max.y = (float)(32000.0 + (this.py + this.fy * num2 + this.ry * num));
			max.z = (float)(this.pz + this.fz * num2 + this.rz * num);
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x00064A8C File Offset: 0x00062C8C
		public void GetMax(int cell, out Vector2 max)
		{
			double num = ((double)(cell % this.cellsWide) - this.halfCellWideMinusHalfTwoMinusOddWide + 0.5) * (double)this.cellSquareDimension;
			double num2 = ((double)(cell / this.cellsWide) - this.halfCellTallMinusHalfTwoMinusOddTall + 0.5) * (double)this.cellSquareDimension;
			max.x = (float)(this.px + this.fx * num2 + this.rx * num);
			max.y = (float)(this.pz + this.fz * num2 + this.rz * num);
		}

		// Token: 0x060019F1 RID: 6641 RVA: 0x00064B20 File Offset: 0x00062D20
		public void GetMax(int x, int y, out Vector3 max)
		{
			double num = ((double)x - this.halfCellWideMinusHalfTwoMinusOddWide + 0.5) * (double)this.cellSquareDimension;
			double num2 = ((double)y - this.halfCellTallMinusHalfTwoMinusOddTall + 0.5) * (double)this.cellSquareDimension;
			max.x = (float)(this.px + this.fx * num2 + this.rx * num);
			max.y = (float)(32000.0 + (this.py + this.fy * num2 + this.ry * num));
			max.z = (float)(this.pz + this.fz * num2 + this.rz * num);
		}

		// Token: 0x060019F2 RID: 6642 RVA: 0x00064BCC File Offset: 0x00062DCC
		public void GetMax(int x, int y, out Vector2 max)
		{
			double num = ((double)x - this.halfCellTallMinusHalfTwoMinusOddTall + 0.5) * (double)this.cellSquareDimension;
			double num2 = ((double)y - this.halfCellTallMinusHalfTwoMinusOddTall + 0.5) * (double)this.cellSquareDimension;
			max.x = (float)(this.px + this.fx * num2 + this.rx * num);
			max.y = (float)(this.pz + this.fz * num2 + this.rz * num);
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x00064C50 File Offset: 0x00062E50
		public ushort FlatCell(ref Vector2 point, out ushort x, out ushort y)
		{
			double num = (double)point.x + this.flat_wide_ofs;
			if (num <= 0.0)
			{
				x = 0;
			}
			else if (num >= this.cellWideLastTimesSquareDimension)
			{
				x = this.cellWideLast;
			}
			else
			{
				x = (ushort)Math.Floor(num / (double)this.cellSquareDimension);
			}
			double num2 = (double)point.y + this.flat_tall_ofs;
			if (num2 <= 0.0)
			{
				y = 0;
			}
			else if (num2 >= this.cellTallLastTimesSquareDimension)
			{
				y = this.cellTallLast;
			}
			else
			{
				y = (ushort)Math.Floor(num2 / (double)this.cellSquareDimension);
			}
			return (ushort)((int)y * this.cellsWide + (int)x);
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x00064D0C File Offset: 0x00062F0C
		public ushort FlatCell(ref Vector2 point)
		{
			double num = (double)point.x + this.flat_wide_ofs;
			int num2;
			if (num <= 0.0)
			{
				num2 = 0;
			}
			else if (num >= this.cellWideLastTimesSquareDimension)
			{
				num2 = (int)this.cellWideLast;
			}
			else
			{
				num2 = (int)Math.Floor(num / (double)this.cellSquareDimension);
			}
			double num3 = (double)point.y + this.flat_tall_ofs;
			int num4;
			if (num3 <= 0.0)
			{
				num4 = 0;
			}
			else if (num3 >= this.cellTallLastTimesSquareDimension)
			{
				num4 = (int)this.cellTallLast;
			}
			else
			{
				num4 = (int)Math.Floor(num3 / (double)this.cellSquareDimension);
			}
			return (ushort)(num4 * this.cellsWide + num2);
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x00064DC0 File Offset: 0x00062FC0
		public ushort WorldCell(ref Vector3 point, out ushort x, out ushort y)
		{
			double num = (double)point.x + this.flat_wide_ofs;
			if (num <= 0.0)
			{
				x = 0;
			}
			else if (num >= this.cellWideLastTimesSquareDimension)
			{
				x = this.cellWideLast;
			}
			else
			{
				x = (ushort)Math.Floor(num / (double)this.cellSquareDimension);
			}
			double num2 = (double)point.z + this.flat_tall_ofs;
			if (num2 <= 0.0)
			{
				y = 0;
			}
			else if (num2 >= this.cellTallLastTimesSquareDimension)
			{
				y = this.cellTallLast;
			}
			else
			{
				y = (ushort)Math.Floor(num2 / (double)this.cellSquareDimension);
			}
			return (ushort)((int)y * this.cellsWide + (int)x);
		}

		// Token: 0x060019F6 RID: 6646 RVA: 0x00064E7C File Offset: 0x0006307C
		public ushort WorldCell(ref Vector3 point)
		{
			double num = (double)point.x + this.flat_wide_ofs;
			int num2;
			if (num <= 0.0)
			{
				num2 = 0;
			}
			else if (num >= this.cellWideLastTimesSquareDimension)
			{
				num2 = (int)this.cellWideLast;
			}
			else
			{
				num2 = (int)Math.Floor(num / (double)this.cellSquareDimension);
			}
			double num3 = (double)point.z + this.flat_tall_ofs;
			int num4;
			if (num3 <= 0.0)
			{
				num4 = 0;
			}
			else if (num3 >= this.cellTallLastTimesSquareDimension)
			{
				num4 = (int)this.cellTallLast;
			}
			else
			{
				num4 = (int)Math.Floor(num3 / (double)this.cellSquareDimension);
			}
			return (ushort)(num4 * this.cellsWide + num2);
		}

		// Token: 0x060019F7 RID: 6647 RVA: 0x00064F30 File Offset: 0x00063130
		public List<ushort> EnumerateNearbyCells(int cell)
		{
			return this.EnumerateNearbyCells(cell, cell % global::CullGrid.grid.cellsWide, cell / global::CullGrid.grid.cellsWide);
		}

		// Token: 0x060019F8 RID: 6648 RVA: 0x00064F54 File Offset: 0x00063154
		public List<ushort> EnumerateNearbyCells(int x, int y)
		{
			return this.EnumerateNearbyCells(y * this.cellsWide + x, x, y);
		}

		// Token: 0x060019F9 RID: 6649 RVA: 0x00064F68 File Offset: 0x00063168
		public List<ushort> EnumerateNearbyCells(int i, int x, int y)
		{
			if (i < 0)
			{
				throw new ArgumentOutOfRangeException("i", i, "i<0");
			}
			if (x < 0)
			{
				throw new ArgumentOutOfRangeException("x", x, "x<0");
			}
			if (y < 0)
			{
				throw new ArgumentOutOfRangeException("y", y, "y<0");
			}
			List<ushort> list = new List<ushort>();
			int num = -(this.gatheringCellsCenter % this.gatheringCellsWide);
			int num2 = -(this.gatheringCellsCenter / this.gatheringCellsWide);
			for (int j = 0; j < this.gatheringCellsWide; j++)
			{
				int num3 = j + num;
				int num4 = x + num3;
				if (num4 >= 0 && num4 < this.cellsWide)
				{
					for (int k = 0; k < this.gatheringCellsTall; k++)
					{
						int num5 = k + num2;
						int num6 = y + num5;
						if (num6 >= 0 && num6 < this.cellsTall && base.GetGatheringBit(j, k))
						{
							ushort item = (ushort)(num4 + num6 * this.cellsWide);
							if (num6 == y && num4 == x)
							{
								list.Insert(0, item);
							}
							else
							{
								list.Add(item);
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x04000E20 RID: 3616
		private const double kMAX_WORLD_Y = 32000.0;

		// Token: 0x04000E21 RID: 3617
		private const double kMIN_WORLD_Y = -32000.0;

		// Token: 0x04000E22 RID: 3618
		public int groupEnd;

		// Token: 0x04000E23 RID: 3619
		public int numCells;

		// Token: 0x04000E24 RID: 3620
		public global::CullGrid cullGrid;

		// Token: 0x04000E25 RID: 3621
		public Transform transform;

		// Token: 0x04000E26 RID: 3622
		public double halfCellTall;

		// Token: 0x04000E27 RID: 3623
		public double halfCellWide;

		// Token: 0x04000E28 RID: 3624
		public int twoMinusOddTall;

		// Token: 0x04000E29 RID: 3625
		public int twoMinusOddWide;

		// Token: 0x04000E2A RID: 3626
		public double halfTwoMinusOddTall;

		// Token: 0x04000E2B RID: 3627
		public double halfTwoMinusOddWide;

		// Token: 0x04000E2C RID: 3628
		public double halfCellTallMinusHalfTwoMinusOddTall;

		// Token: 0x04000E2D RID: 3629
		public double halfCellWideMinusHalfTwoMinusOddWide;

		// Token: 0x04000E2E RID: 3630
		public double px;

		// Token: 0x04000E2F RID: 3631
		public double py;

		// Token: 0x04000E30 RID: 3632
		public double pz;

		// Token: 0x04000E31 RID: 3633
		public double fx;

		// Token: 0x04000E32 RID: 3634
		public double fy;

		// Token: 0x04000E33 RID: 3635
		public double fz;

		// Token: 0x04000E34 RID: 3636
		public double rx;

		// Token: 0x04000E35 RID: 3637
		public double ry;

		// Token: 0x04000E36 RID: 3638
		public double rz;

		// Token: 0x04000E37 RID: 3639
		public double flat_wide_ofs;

		// Token: 0x04000E38 RID: 3640
		public double flat_tall_ofs;

		// Token: 0x04000E39 RID: 3641
		public ushort cellWideLast;

		// Token: 0x04000E3A RID: 3642
		public ushort cellTallLast;

		// Token: 0x04000E3B RID: 3643
		public double cellWideLastTimesSquareDimension;

		// Token: 0x04000E3C RID: 3644
		public double cellTallLastTimesSquareDimension;
	}
}
