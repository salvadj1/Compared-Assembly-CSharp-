using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using uLink;
using UnityEngine;

// Token: 0x020002A4 RID: 676
public class CullGrid : MonoBehaviour
{
	// Token: 0x170006FA RID: 1786
	// (get) Token: 0x0600180F RID: 6159 RVA: 0x0005E980 File Offset: 0x0005CB80
	public static bool autoPrebindInInstantiate
	{
		get
		{
			return CullGrid.has_grid && CullGrid.cull_prebinding;
		}
	}

	// Token: 0x06001810 RID: 6160 RVA: 0x0005E994 File Offset: 0x0005CB94
	private Vector3 GetCenterSetup(int cell)
	{
		CullGridSetup cullGridSetup = this.setup;
		return base.transform.position + base.transform.forward * (((float)(cell / cullGridSetup.cellsWide) - ((float)cullGridSetup.cellsTall / 2f - (float)(2 - (cullGridSetup.cellsTall & 1)) / 2f)) * (float)cullGridSetup.cellSquareDimension) + base.transform.right * (((float)(cell % cullGridSetup.cellsWide) - ((float)cullGridSetup.cellsWide / 2f - (float)(2 - (cullGridSetup.cellsWide & 1)) / 2f)) * (float)cullGridSetup.cellSquareDimension);
	}

	// Token: 0x06001811 RID: 6161 RVA: 0x0005EA40 File Offset: 0x0005CC40
	private void DrawGrid(int cell)
	{
		if (cell != -1)
		{
			this.DrawGrid(this.GetCenterSetup(cell));
		}
	}

	// Token: 0x06001812 RID: 6162 RVA: 0x0005EA58 File Offset: 0x0005CC58
	private void DrawGrid(int centerCell, int xOffset, int yOffset)
	{
		this.DrawGrid(centerCell + xOffset + this.setup.cellsWide * 2 * yOffset);
	}

	// Token: 0x06001813 RID: 6163 RVA: 0x0005EA74 File Offset: 0x0005CC74
	private void DrawGrid(Vector3 center)
	{
		Vector3 vector = base.transform.right * ((float)this.setup.cellSquareDimension / 2f);
		Vector3 vector2 = base.transform.forward * ((float)this.setup.cellSquareDimension / 2f);
		CullGrid.DrawQuadRayCastDown(center + vector + vector2, center + vector - vector2, center - vector - vector2, center - vector + vector2);
	}

	// Token: 0x06001814 RID: 6164 RVA: 0x0005EB00 File Offset: 0x0005CD00
	private void DrawGrid(Vector3 center, float sizeX, float sizeY)
	{
		Vector3 vector = base.transform.right * (sizeX / 2f);
		Vector3 vector2 = base.transform.forward * (sizeY / 2f);
		CullGrid.DrawQuadRayCastDown(center + vector + vector2, center + vector - vector2, center - vector - vector2, center - vector + vector2);
	}

	// Token: 0x06001815 RID: 6165 RVA: 0x0005EB78 File Offset: 0x0005CD78
	private static void RegisterGrid(CullGrid grid)
	{
		if (grid)
		{
			CullGrid.grid = new CullGrid.CullGridRuntime(grid);
			CullGrid.has_grid = true;
		}
	}

	// Token: 0x06001816 RID: 6166 RVA: 0x0005EB98 File Offset: 0x0005CD98
	private void Awake()
	{
		CullGrid.RegisterGrid(this);
	}

	// Token: 0x06001817 RID: 6167 RVA: 0x0005EBA0 File Offset: 0x0005CDA0
	public static ushort CellFromGroupID(int groupID)
	{
		if (groupID < CullGrid.grid.groupBegin || groupID >= CullGrid.grid.groupEnd)
		{
			throw new ArgumentOutOfRangeException("groupID", groupID, "groupID < grid.groupBegin || groupID >= grid.groupEnd");
		}
		return (ushort)(groupID - CullGrid.grid.groupBegin);
	}

	// Token: 0x06001818 RID: 6168 RVA: 0x0005EBF0 File Offset: 0x0005CDF0
	public static ushort CellFromGroupID(int groupID, out ushort x, out ushort y)
	{
		ushort num = CullGrid.CellFromGroupID(groupID);
		x = (ushort)((int)num % CullGrid.grid.cellsWide);
		y = (ushort)((int)num / CullGrid.grid.cellsWide);
		return num;
	}

	// Token: 0x06001819 RID: 6169 RVA: 0x0005EC24 File Offset: 0x0005CE24
	public static int GroupIDFromCell(ushort cell)
	{
		if ((int)cell >= CullGrid.grid.numCells)
		{
			throw new ArgumentOutOfRangeException("cell", cell, "cell >= grid.numCells");
		}
		return CullGrid.grid.groupBegin + (int)cell;
	}

	// Token: 0x170006FB RID: 1787
	// (get) Token: 0x0600181A RID: 6170 RVA: 0x0005EC64 File Offset: 0x0005CE64
	public static int Wide
	{
		get
		{
			return CullGrid.grid.cellsWide;
		}
	}

	// Token: 0x170006FC RID: 1788
	// (get) Token: 0x0600181B RID: 6171 RVA: 0x0005EC70 File Offset: 0x0005CE70
	public static int Tall
	{
		get
		{
			return CullGrid.grid.cellsTall;
		}
	}

	// Token: 0x0600181C RID: 6172 RVA: 0x0005EC7C File Offset: 0x0005CE7C
	public static bool CellContainsPoint(ushort cell, ref Vector2 flatPoint)
	{
		return cell == CullGrid.grid.FlatCell(ref flatPoint);
	}

	// Token: 0x0600181D RID: 6173 RVA: 0x0005EC8C File Offset: 0x0005CE8C
	public static bool CellContainsPoint(ushort cell, ref Vector2 flatPoint, out ushort cell_point)
	{
		cell_point = CullGrid.grid.FlatCell(ref flatPoint);
		return cell == cell_point;
	}

	// Token: 0x0600181E RID: 6174 RVA: 0x0005ECA0 File Offset: 0x0005CEA0
	public static bool CellContainsPoint(ushort cell, ref Vector3 worldPoint)
	{
		return cell == CullGrid.grid.WorldCell(ref worldPoint);
	}

	// Token: 0x0600181F RID: 6175 RVA: 0x0005ECB0 File Offset: 0x0005CEB0
	public static bool CellContainsPoint(ushort cell, ref Vector3 worldPoint, out ushort cell_point)
	{
		cell_point = CullGrid.grid.WorldCell(ref worldPoint);
		return cell_point == cell;
	}

	// Token: 0x06001820 RID: 6176 RVA: 0x0005ECC4 File Offset: 0x0005CEC4
	public static bool GroupIDContainsPoint(int groupID, ref Vector2 flatPoint, out int groupID_point)
	{
		if (groupID < CullGrid.grid.groupBegin || groupID >= CullGrid.grid.groupEnd)
		{
			NetworkGroup unassigned = NetworkGroup.unassigned;
			groupID_point = unassigned.id;
			return false;
		}
		ushort num;
		if (!CullGrid.CellContainsPoint((ushort)(groupID - CullGrid.grid.groupBegin), ref flatPoint, out num))
		{
			groupID_point = (int)num + CullGrid.grid.groupBegin;
			return false;
		}
		groupID_point = groupID;
		return true;
	}

	// Token: 0x06001821 RID: 6177 RVA: 0x0005ED30 File Offset: 0x0005CF30
	public static bool GroupIDContainsPoint(int groupID, ref Vector3 worldPoint, out int groupID_point)
	{
		if (groupID < CullGrid.grid.groupBegin || groupID >= CullGrid.grid.groupEnd)
		{
			NetworkGroup unassigned = NetworkGroup.unassigned;
			groupID_point = unassigned.id;
			return false;
		}
		ushort cell;
		if (!CullGrid.CellContainsPoint(CullGrid.CellFromGroupID(groupID), ref worldPoint, out cell))
		{
			groupID_point = CullGrid.GroupIDFromCell(cell);
			return false;
		}
		groupID_point = groupID;
		return true;
	}

	// Token: 0x06001822 RID: 6178 RVA: 0x0005ED90 File Offset: 0x0005CF90
	public static bool GroupIDContainsPoint(int groupID, ref Vector2 flatPoint)
	{
		return groupID >= CullGrid.grid.groupBegin && groupID < CullGrid.grid.groupEnd && CullGrid.CellContainsPoint((ushort)(groupID - CullGrid.grid.groupBegin), ref flatPoint);
	}

	// Token: 0x06001823 RID: 6179 RVA: 0x0005EDD4 File Offset: 0x0005CFD4
	public static bool GroupIDContainsPoint(int groupID, ref Vector3 worldPoint)
	{
		return groupID >= CullGrid.grid.groupBegin && groupID < CullGrid.grid.groupEnd && CullGrid.CellContainsPoint((ushort)(groupID - CullGrid.grid.groupBegin), ref worldPoint);
	}

	// Token: 0x06001824 RID: 6180 RVA: 0x0005EE18 File Offset: 0x0005D018
	public static Vector2 Flat(Vector3 triD)
	{
		Vector2 result;
		result.x = triD.x;
		result.y = triD.z;
		return result;
	}

	// Token: 0x06001825 RID: 6181 RVA: 0x0005EE44 File Offset: 0x0005D044
	public static ushort FlatCell(Vector2 flat)
	{
		return CullGrid.grid.FlatCell(ref flat);
	}

	// Token: 0x06001826 RID: 6182 RVA: 0x0005EE54 File Offset: 0x0005D054
	public static ushort WorldCell(Vector3 world)
	{
		return CullGrid.grid.WorldCell(ref world);
	}

	// Token: 0x06001827 RID: 6183 RVA: 0x0005EE64 File Offset: 0x0005D064
	public static ushort FlatCell(ref Vector2 flat)
	{
		return CullGrid.grid.FlatCell(ref flat);
	}

	// Token: 0x06001828 RID: 6184 RVA: 0x0005EE74 File Offset: 0x0005D074
	public static ushort WorldCell(ref Vector3 world)
	{
		return CullGrid.grid.WorldCell(ref world);
	}

	// Token: 0x06001829 RID: 6185 RVA: 0x0005EE84 File Offset: 0x0005D084
	public static int FlatGroupID(ref Vector2 flat)
	{
		return (int)CullGrid.grid.FlatCell(ref flat) + CullGrid.grid.groupBegin;
	}

	// Token: 0x0600182A RID: 6186 RVA: 0x0005EE9C File Offset: 0x0005D09C
	public static int WorldGroupID(ref Vector3 world)
	{
		return (int)CullGrid.grid.WorldCell(ref world) + CullGrid.grid.groupBegin;
	}

	// Token: 0x0600182B RID: 6187 RVA: 0x0005EEB4 File Offset: 0x0005D0B4
	private static void RaycastDownVect(ref Vector3 a)
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(new Vector3(a.x, 10000f, a.z), Vector3.down, ref raycastHit, float.PositiveInfinity))
		{
			a = raycastHit.point + Vector3.up * a.y;
		}
	}

	// Token: 0x0600182C RID: 6188 RVA: 0x0005EF10 File Offset: 0x0005D110
	private static void DrawQuadRayCastDown(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
	{
		CullGrid.RaycastDownVect(ref a);
		CullGrid.RaycastDownVect(ref b);
		CullGrid.RaycastDownVect(ref c);
		CullGrid.RaycastDownVect(ref d);
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

	// Token: 0x0600182D RID: 6189 RVA: 0x0005F078 File Offset: 0x0005D278
	private void DrawGizmosNow()
	{
	}

	// Token: 0x0600182E RID: 6190 RVA: 0x0005F07C File Offset: 0x0005D27C
	public static bool IsCellGroupID(int usedGroup)
	{
		return CullGrid.has_grid && usedGroup >= CullGrid.grid.groupBegin && usedGroup < CullGrid.grid.groupEnd;
	}

	// Token: 0x0600182F RID: 6191 RVA: 0x0005F0B4 File Offset: 0x0005D2B4
	public static bool IsCellGroup(NetworkGroup group)
	{
		return CullGrid.IsCellGroupID(group.id);
	}

	// Token: 0x04000CDF RID: 3295
	private static bool cull_prebinding = true;

	// Token: 0x04000CE0 RID: 3296
	[SerializeField]
	private CullGridSetup setup;

	// Token: 0x04000CE1 RID: 3297
	private static CullGrid.CullGridRuntime grid;

	// Token: 0x04000CE2 RID: 3298
	private static bool has_grid;

	// Token: 0x020002A5 RID: 677
	[StructLayout(LayoutKind.Explicit, Size = 2)]
	public struct CellID
	{
		// Token: 0x06001830 RID: 6192 RVA: 0x0005F0C4 File Offset: 0x0005D2C4
		public CellID(ushort cellID)
		{
			this.id = cellID;
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06001831 RID: 6193 RVA: 0x0005F0D0 File Offset: 0x0005D2D0
		public Vector2 flatCenter
		{
			get
			{
				Vector2 result;
				CullGrid.grid.GetCenter((int)this.id, out result);
				return result;
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06001832 RID: 6194 RVA: 0x0005F0F0 File Offset: 0x0005D2F0
		public Vector2 flatMax
		{
			get
			{
				Vector2 result;
				CullGrid.grid.GetMin((int)this.id, out result);
				return result;
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06001833 RID: 6195 RVA: 0x0005F110 File Offset: 0x0005D310
		public Vector2 flatMin
		{
			get
			{
				Vector2 result;
				CullGrid.grid.GetMax((int)this.id, out result);
				return result;
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06001834 RID: 6196 RVA: 0x0005F130 File Offset: 0x0005D330
		public Rect flatRect
		{
			get
			{
				Rect result;
				CullGrid.grid.GetRect((int)this.id, out result);
				return result;
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06001835 RID: 6197 RVA: 0x0005F150 File Offset: 0x0005D350
		public Vector3 worldCenter
		{
			get
			{
				Vector3 result;
				CullGrid.grid.GetCenter((int)this.id, out result);
				return result;
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06001836 RID: 6198 RVA: 0x0005F170 File Offset: 0x0005D370
		public Vector3 worldMax
		{
			get
			{
				Vector3 result;
				CullGrid.grid.GetMin((int)this.id, out result);
				return result;
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06001837 RID: 6199 RVA: 0x0005F190 File Offset: 0x0005D390
		public Vector3 worldMin
		{
			get
			{
				Vector3 result;
				CullGrid.grid.GetMax((int)this.id, out result);
				return result;
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06001838 RID: 6200 RVA: 0x0005F1B0 File Offset: 0x0005D3B0
		public Bounds worldBounds
		{
			get
			{
				Bounds result;
				CullGrid.grid.GetBounds((int)this.id, out result);
				return result;
			}
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x0005F1D0 File Offset: 0x0005D3D0
		public bool ContainsWorldPoint(Vector3 worldPoint)
		{
			return CullGrid.grid.Contains((int)this.id, ref worldPoint);
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x0005F1E4 File Offset: 0x0005D3E4
		public bool ContainsFlatPoint(Vector2 flatPoint)
		{
			return CullGrid.grid.Contains((int)this.id, ref flatPoint);
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x0005F1F8 File Offset: 0x0005D3F8
		public bool ContainsWorldPoint(ref Vector3 worldPoint)
		{
			return this.valid && CullGrid.grid.Contains((int)this.id, ref worldPoint);
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x0005F21C File Offset: 0x0005D41C
		public bool ContainsFlatPoint(ref Vector2 flatPoint)
		{
			return this.valid && CullGrid.grid.Contains((int)this.id, ref flatPoint);
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x0600183D RID: 6205 RVA: 0x0005F240 File Offset: 0x0005D440
		public int column
		{
			get
			{
				return (!this.valid) ? -1 : ((int)this.id % CullGrid.grid.cellsWide);
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x0600183E RID: 6206 RVA: 0x0005F270 File Offset: 0x0005D470
		public int row
		{
			get
			{
				return (!this.valid) ? -1 : ((int)this.id / CullGrid.grid.cellsWide);
			}
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x0005F2A0 File Offset: 0x0005D4A0
		private static ushort NextRight(ushort id)
		{
			return ((int)id % CullGrid.grid.cellsWide != (int)CullGrid.grid.cellWideLast) ? (id + 1) : ushort.MaxValue;
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x0005F2CC File Offset: 0x0005D4CC
		private static ushort NextLeft(ushort id)
		{
			return ((int)id % CullGrid.grid.cellsWide != 0) ? (id - 1) : ushort.MaxValue;
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x0005F2F0 File Offset: 0x0005D4F0
		private static ushort NextUp(ushort id)
		{
			return ((int)id / CullGrid.grid.cellsWide != (int)CullGrid.grid.cellTallLast) ? ((ushort)((int)id + CullGrid.grid.cellsWide)) : ushort.MaxValue;
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x0005F330 File Offset: 0x0005D530
		private static ushort NextDown(ushort id)
		{
			return ((int)id / CullGrid.grid.cellsWide != 0) ? ((ushort)((int)id - CullGrid.grid.cellsWide)) : ushort.MaxValue;
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06001843 RID: 6211 RVA: 0x0005F368 File Offset: 0x0005D568
		public CullGrid.CellID right
		{
			get
			{
				CullGrid.CellID result;
				result.id = ((!this.valid) ? ushort.MaxValue : CullGrid.CellID.NextRight(this.id));
				return result;
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06001844 RID: 6212 RVA: 0x0005F3A0 File Offset: 0x0005D5A0
		public CullGrid.CellID left
		{
			get
			{
				CullGrid.CellID result;
				result.id = ((!this.valid) ? ushort.MaxValue : CullGrid.CellID.NextLeft(this.id));
				return result;
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06001845 RID: 6213 RVA: 0x0005F3D8 File Offset: 0x0005D5D8
		public CullGrid.CellID up
		{
			get
			{
				CullGrid.CellID result;
				result.id = ((!this.valid) ? ushort.MaxValue : CullGrid.CellID.NextUp(this.id));
				return result;
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06001846 RID: 6214 RVA: 0x0005F410 File Offset: 0x0005D610
		public CullGrid.CellID down
		{
			get
			{
				CullGrid.CellID result;
				result.id = ((!this.valid) ? ushort.MaxValue : CullGrid.CellID.NextDown(this.id));
				return result;
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06001847 RID: 6215 RVA: 0x0005F448 File Offset: 0x0005D648
		public bool mostRight
		{
			get
			{
				return this.valid && (int)this.id % CullGrid.grid.cellsWide == (int)CullGrid.grid.cellWideLast;
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06001848 RID: 6216 RVA: 0x0005F478 File Offset: 0x0005D678
		public bool mostLeft
		{
			get
			{
				return this.valid && (int)this.id % CullGrid.grid.cellsWide == 0;
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06001849 RID: 6217 RVA: 0x0005F4A8 File Offset: 0x0005D6A8
		public bool mostTop
		{
			get
			{
				return this.valid && (int)this.id / CullGrid.grid.cellsWide == (int)CullGrid.grid.cellTallLast;
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x0600184A RID: 6218 RVA: 0x0005F4D8 File Offset: 0x0005D6D8
		public bool mostBottom
		{
			get
			{
				return this.valid && (int)this.id / CullGrid.grid.cellsWide == 0;
			}
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x0600184B RID: 6219 RVA: 0x0005F508 File Offset: 0x0005D708
		public bool valid
		{
			get
			{
				return (int)this.id < CullGrid.grid.numCells;
			}
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x0600184C RID: 6220 RVA: 0x0005F51C File Offset: 0x0005D71C
		public int groupID
		{
			get
			{
				int result;
				if (this.valid)
				{
					result = CullGrid.GroupIDFromCell(this.id);
				}
				else
				{
					NetworkGroup unassigned = NetworkGroup.unassigned;
					result = unassigned.id;
				}
				return result;
			}
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x0600184D RID: 6221 RVA: 0x0005F554 File Offset: 0x0005D754
		public NetworkGroup group
		{
			get
			{
				return (!this.valid) ? NetworkGroup.unassigned : CullGrid.GroupIDFromCell(this.id);
			}
		}

		// Token: 0x04000CE3 RID: 3299
		private const ushort kInvalidID = 65535;

		// Token: 0x04000CE4 RID: 3300
		[FieldOffset(0)]
		public ushort id;
	}

	// Token: 0x020002A6 RID: 678
	private class CullGridRuntime : CullGridSetup
	{
		// Token: 0x0600184E RID: 6222 RVA: 0x0005F57C File Offset: 0x0005D77C
		public CullGridRuntime(CullGrid cullGrid) : base(cullGrid.setup)
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

		// Token: 0x0600184F RID: 6223 RVA: 0x0005F860 File Offset: 0x0005DA60
		public void GetCenter(int cell, out Vector3 center)
		{
			double num = ((double)(cell % this.cellsWide) - this.halfCellWideMinusHalfTwoMinusOddWide) * (double)this.cellSquareDimension;
			double num2 = ((double)(cell / this.cellsWide) - this.halfCellTallMinusHalfTwoMinusOddTall) * (double)this.cellSquareDimension;
			center.x = (float)(this.px + this.fx * num2 + this.rx * num);
			center.y = (float)(this.py + this.fy * num2 + this.ry * num);
			center.z = (float)(this.pz + this.fz * num2 + this.rz * num);
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x0005F8FC File Offset: 0x0005DAFC
		public void GetCenter(int cell, out Vector2 center)
		{
			double num = ((double)(cell % this.cellsWide) - this.halfCellWideMinusHalfTwoMinusOddWide) * (double)this.cellSquareDimension;
			double num2 = ((double)(cell / this.cellsWide) - this.halfCellTallMinusHalfTwoMinusOddTall) * (double)this.cellSquareDimension;
			center.x = (float)(this.px + this.fx * num2 + this.rx * num);
			center.y = (float)(this.pz + this.fz * num2 + this.rz * num);
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x0005F97C File Offset: 0x0005DB7C
		public void GetCenter(int x, int y, out Vector3 center)
		{
			double num = ((double)x - this.halfCellWideMinusHalfTwoMinusOddWide) * (double)this.cellSquareDimension;
			double num2 = ((double)y - this.halfCellTallMinusHalfTwoMinusOddTall) * (double)this.cellSquareDimension;
			center.x = (float)(this.px + this.fx * num2 + this.rx * num);
			center.y = (float)(this.py + this.fy * num2 + this.ry * num);
			center.z = (float)(this.pz + this.fz * num2 + this.rz * num);
		}

		// Token: 0x06001852 RID: 6226 RVA: 0x0005FA0C File Offset: 0x0005DC0C
		public void GetCenter(int x, int y, out Vector2 center)
		{
			double num = ((double)x - this.halfCellWideMinusHalfTwoMinusOddWide) * (double)this.cellSquareDimension;
			double num2 = ((double)y - this.halfCellTallMinusHalfTwoMinusOddTall) * (double)this.cellSquareDimension;
			center.x = (float)(this.px + this.fx * num2 + this.rx * num);
			center.y = (float)(this.pz + this.fz * num2 + this.rz * num);
		}

		// Token: 0x06001853 RID: 6227 RVA: 0x0005FA7C File Offset: 0x0005DC7C
		public void GetMin(int cell, out Vector3 min)
		{
			double num = ((double)(cell % this.cellsWide) - this.halfCellWideMinusHalfTwoMinusOddWide - 0.5) * (double)this.cellSquareDimension;
			double num2 = ((double)(cell / this.cellsWide) - this.halfCellTallMinusHalfTwoMinusOddTall - 0.5) * (double)this.cellSquareDimension;
			min.x = (float)(this.px + this.fx * num2 + this.rx * num);
			min.y = (float)(-32000.0 + (this.py + this.fy * num2 + this.ry * num));
			min.z = (float)(this.pz + this.fz * num2 + this.rz * num);
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x0005FB38 File Offset: 0x0005DD38
		public void GetMin(int cell, out Vector2 min)
		{
			double num = ((double)(cell % this.cellsWide) - this.halfCellWideMinusHalfTwoMinusOddWide - 0.5) * (double)this.cellSquareDimension;
			double num2 = ((double)(cell / this.cellsWide) - this.halfCellTallMinusHalfTwoMinusOddTall - 0.5) * (double)this.cellSquareDimension;
			min.x = (float)(this.px + this.fx * num2 + this.rx * num);
			min.y = (float)(this.pz + this.fz * num2 + this.rz * num);
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x0005FBCC File Offset: 0x0005DDCC
		public void GetMin(int x, int y, out Vector3 min)
		{
			double num = ((double)x - this.halfCellWideMinusHalfTwoMinusOddWide - 0.5) * (double)this.cellSquareDimension;
			double num2 = ((double)y - this.halfCellTallMinusHalfTwoMinusOddTall - 0.5) * (double)this.cellSquareDimension;
			min.x = (float)(this.px + this.fx * num2 + this.rx * num);
			min.y = (float)(-32000.0 + (this.py + this.fy * num2 + this.ry * num));
			min.z = (float)(this.pz + this.fz * num2 + this.rz * num);
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x0005FC78 File Offset: 0x0005DE78
		public void GetMin(int x, int y, out Vector2 min)
		{
			double num = ((double)x - this.halfCellWideMinusHalfTwoMinusOddWide - 0.5) * (double)this.cellSquareDimension;
			double num2 = ((double)y - this.halfCellTallMinusHalfTwoMinusOddTall - 0.5) * (double)this.cellSquareDimension;
			min.x = (float)(this.px + this.fx * num2 + this.rx * num);
			min.y = (float)(this.pz + this.fz * num2 + this.rz * num);
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x0005FCFC File Offset: 0x0005DEFC
		public bool Contains(int cell, ref Vector2 flatPoint)
		{
			return cell >= 0 && cell < this.numCells && (int)this.FlatCell(ref flatPoint) == cell;
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x0005FD2C File Offset: 0x0005DF2C
		public bool Contains(int cell, ref Vector3 worldPoint)
		{
			return cell >= 0 && cell < this.numCells && (int)this.WorldCell(ref worldPoint) == cell;
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x0005FD5C File Offset: 0x0005DF5C
		public bool Contains(int x, int y, ref Vector2 flatPoint)
		{
			return this.Contains(y * this.cellsWide + x, ref flatPoint);
		}

		// Token: 0x0600185A RID: 6234 RVA: 0x0005FD70 File Offset: 0x0005DF70
		public bool Contains(int x, int y, ref Vector3 worldPoint)
		{
			return this.Contains(y * this.cellsWide + x, ref worldPoint);
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x0005FD84 File Offset: 0x0005DF84
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

		// Token: 0x0600185C RID: 6236 RVA: 0x0005FEA4 File Offset: 0x0005E0A4
		public void GetBounds(int x, int y, out Bounds bounds)
		{
			Vector3 vector;
			this.GetCenter(x, y, out vector);
			bounds..ctor(vector, new Vector3((float)this.cellSquareDimension, 64000f, (float)this.cellSquareDimension));
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x0005FEDC File Offset: 0x0005E0DC
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

		// Token: 0x0600185E RID: 6238 RVA: 0x00060014 File Offset: 0x0005E214
		public void GetBounds(int cell, out Bounds bounds)
		{
			int x = cell % this.cellsWide;
			int y = cell / this.cellsWide;
			Vector3 vector;
			this.GetCenter(x, y, out vector);
			bounds..ctor(vector, new Vector3((float)this.cellSquareDimension, 64000f, (float)this.cellSquareDimension));
		}

		// Token: 0x0600185F RID: 6239 RVA: 0x0006005C File Offset: 0x0005E25C
		public void GetMax(int cell, out Vector3 max)
		{
			double num = ((double)(cell % this.cellsWide) - this.halfCellWideMinusHalfTwoMinusOddWide + 0.5) * (double)this.cellSquareDimension;
			double num2 = ((double)(cell / this.cellsWide) - this.halfCellTallMinusHalfTwoMinusOddTall + 0.5) * (double)this.cellSquareDimension;
			max.x = (float)(this.px + this.fx * num2 + this.rx * num);
			max.y = (float)(32000.0 + (this.py + this.fy * num2 + this.ry * num));
			max.z = (float)(this.pz + this.fz * num2 + this.rz * num);
		}

		// Token: 0x06001860 RID: 6240 RVA: 0x00060118 File Offset: 0x0005E318
		public void GetMax(int cell, out Vector2 max)
		{
			double num = ((double)(cell % this.cellsWide) - this.halfCellWideMinusHalfTwoMinusOddWide + 0.5) * (double)this.cellSquareDimension;
			double num2 = ((double)(cell / this.cellsWide) - this.halfCellTallMinusHalfTwoMinusOddTall + 0.5) * (double)this.cellSquareDimension;
			max.x = (float)(this.px + this.fx * num2 + this.rx * num);
			max.y = (float)(this.pz + this.fz * num2 + this.rz * num);
		}

		// Token: 0x06001861 RID: 6241 RVA: 0x000601AC File Offset: 0x0005E3AC
		public void GetMax(int x, int y, out Vector3 max)
		{
			double num = ((double)x - this.halfCellWideMinusHalfTwoMinusOddWide + 0.5) * (double)this.cellSquareDimension;
			double num2 = ((double)y - this.halfCellTallMinusHalfTwoMinusOddTall + 0.5) * (double)this.cellSquareDimension;
			max.x = (float)(this.px + this.fx * num2 + this.rx * num);
			max.y = (float)(32000.0 + (this.py + this.fy * num2 + this.ry * num));
			max.z = (float)(this.pz + this.fz * num2 + this.rz * num);
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x00060258 File Offset: 0x0005E458
		public void GetMax(int x, int y, out Vector2 max)
		{
			double num = ((double)x - this.halfCellTallMinusHalfTwoMinusOddTall + 0.5) * (double)this.cellSquareDimension;
			double num2 = ((double)y - this.halfCellTallMinusHalfTwoMinusOddTall + 0.5) * (double)this.cellSquareDimension;
			max.x = (float)(this.px + this.fx * num2 + this.rx * num);
			max.y = (float)(this.pz + this.fz * num2 + this.rz * num);
		}

		// Token: 0x06001863 RID: 6243 RVA: 0x000602DC File Offset: 0x0005E4DC
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

		// Token: 0x06001864 RID: 6244 RVA: 0x00060398 File Offset: 0x0005E598
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

		// Token: 0x06001865 RID: 6245 RVA: 0x0006044C File Offset: 0x0005E64C
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

		// Token: 0x06001866 RID: 6246 RVA: 0x00060508 File Offset: 0x0005E708
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

		// Token: 0x06001867 RID: 6247 RVA: 0x000605BC File Offset: 0x0005E7BC
		public List<ushort> EnumerateNearbyCells(int cell)
		{
			return this.EnumerateNearbyCells(cell, cell % CullGrid.grid.cellsWide, cell / CullGrid.grid.cellsWide);
		}

		// Token: 0x06001868 RID: 6248 RVA: 0x000605E0 File Offset: 0x0005E7E0
		public List<ushort> EnumerateNearbyCells(int x, int y)
		{
			return this.EnumerateNearbyCells(y * this.cellsWide + x, x, y);
		}

		// Token: 0x06001869 RID: 6249 RVA: 0x000605F4 File Offset: 0x0005E7F4
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

		// Token: 0x04000CE5 RID: 3301
		private const double kMAX_WORLD_Y = 32000.0;

		// Token: 0x04000CE6 RID: 3302
		private const double kMIN_WORLD_Y = -32000.0;

		// Token: 0x04000CE7 RID: 3303
		public int groupEnd;

		// Token: 0x04000CE8 RID: 3304
		public int numCells;

		// Token: 0x04000CE9 RID: 3305
		public CullGrid cullGrid;

		// Token: 0x04000CEA RID: 3306
		public Transform transform;

		// Token: 0x04000CEB RID: 3307
		public double halfCellTall;

		// Token: 0x04000CEC RID: 3308
		public double halfCellWide;

		// Token: 0x04000CED RID: 3309
		public int twoMinusOddTall;

		// Token: 0x04000CEE RID: 3310
		public int twoMinusOddWide;

		// Token: 0x04000CEF RID: 3311
		public double halfTwoMinusOddTall;

		// Token: 0x04000CF0 RID: 3312
		public double halfTwoMinusOddWide;

		// Token: 0x04000CF1 RID: 3313
		public double halfCellTallMinusHalfTwoMinusOddTall;

		// Token: 0x04000CF2 RID: 3314
		public double halfCellWideMinusHalfTwoMinusOddWide;

		// Token: 0x04000CF3 RID: 3315
		public double px;

		// Token: 0x04000CF4 RID: 3316
		public double py;

		// Token: 0x04000CF5 RID: 3317
		public double pz;

		// Token: 0x04000CF6 RID: 3318
		public double fx;

		// Token: 0x04000CF7 RID: 3319
		public double fy;

		// Token: 0x04000CF8 RID: 3320
		public double fz;

		// Token: 0x04000CF9 RID: 3321
		public double rx;

		// Token: 0x04000CFA RID: 3322
		public double ry;

		// Token: 0x04000CFB RID: 3323
		public double rz;

		// Token: 0x04000CFC RID: 3324
		public double flat_wide_ofs;

		// Token: 0x04000CFD RID: 3325
		public double flat_tall_ofs;

		// Token: 0x04000CFE RID: 3326
		public ushort cellWideLast;

		// Token: 0x04000CFF RID: 3327
		public ushort cellTallLast;

		// Token: 0x04000D00 RID: 3328
		public double cellWideLastTimesSquareDimension;

		// Token: 0x04000D01 RID: 3329
		public double cellTallLastTimesSquareDimension;
	}
}
