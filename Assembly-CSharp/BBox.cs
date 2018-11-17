using System;
using UnityEngine;

// Token: 0x02000271 RID: 625
public struct BBox
{
	// Token: 0x17000686 RID: 1670
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

	// Token: 0x17000687 RID: 1671
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

	// Token: 0x04000BB7 RID: 2999
	public Vector3 a;

	// Token: 0x04000BB8 RID: 3000
	public Vector3 b;

	// Token: 0x04000BB9 RID: 3001
	public Vector3 c;

	// Token: 0x04000BBA RID: 3002
	public Vector3 d;

	// Token: 0x04000BBB RID: 3003
	public Vector3 e;

	// Token: 0x04000BBC RID: 3004
	public Vector3 f;

	// Token: 0x04000BBD RID: 3005
	public Vector3 g;

	// Token: 0x04000BBE RID: 3006
	public Vector3 h;
}
