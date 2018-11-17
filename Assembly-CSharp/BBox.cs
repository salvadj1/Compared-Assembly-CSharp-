using System;
using UnityEngine;

// Token: 0x0200023E RID: 574
public struct BBox
{
	// Token: 0x1700063E RID: 1598
	public Vector3 this[int corner]
	{
		get
		{
			switch (corner)
			{
			case 0:
				return this.a;
			case 1:
				return this.b;
			case 2:
				return this.c;
			case 3:
				return this.d;
			case 4:
				return this.e;
			case 5:
				return this.f;
			case 6:
				return this.g;
			case 7:
				return this.h;
			default:
				throw new ArgumentOutOfRangeException("corner");
			}
		}
		set
		{
			switch (corner)
			{
			case 0:
				this.a = value;
				break;
			case 1:
				this.b = value;
				break;
			case 2:
				this.c = value;
				break;
			case 3:
				this.d = value;
				break;
			case 4:
				this.e = value;
				break;
			case 5:
				this.f = value;
				break;
			case 6:
				this.g = value;
				break;
			case 7:
				this.h = value;
				break;
			default:
				throw new ArgumentOutOfRangeException("corner");
			}
		}
	}

	// Token: 0x1700063F RID: 1599
	public float this[int corner, int axis]
	{
		get
		{
			switch (corner)
			{
			case 0:
				return this.a[axis];
			case 1:
				return this.b[axis];
			case 2:
				return this.c[axis];
			case 3:
				return this.d[axis];
			case 4:
				return this.e[axis];
			case 5:
				return this.f[axis];
			case 6:
				return this.g[axis];
			case 7:
				return this.h[axis];
			default:
				throw new ArgumentOutOfRangeException("corner");
			}
		}
		set
		{
			switch (corner)
			{
			case 0:
				this.a[axis] = value;
				break;
			case 1:
				this.b[axis] = value;
				break;
			case 2:
				this.c[axis] = value;
				break;
			case 3:
				this.d[axis] = value;
				break;
			case 4:
				this.e[axis] = value;
				break;
			case 5:
				this.f[axis] = value;
				break;
			case 6:
				this.g[axis] = value;
				break;
			case 7:
				this.h[axis] = value;
				break;
			default:
				throw new ArgumentOutOfRangeException("corner");
			}
		}
	}

	// Token: 0x04000A94 RID: 2708
	public Vector3 a;

	// Token: 0x04000A95 RID: 2709
	public Vector3 b;

	// Token: 0x04000A96 RID: 2710
	public Vector3 c;

	// Token: 0x04000A97 RID: 2711
	public Vector3 d;

	// Token: 0x04000A98 RID: 2712
	public Vector3 e;

	// Token: 0x04000A99 RID: 2713
	public Vector3 f;

	// Token: 0x04000A9A RID: 2714
	public Vector3 g;

	// Token: 0x04000A9B RID: 2715
	public Vector3 h;
}
