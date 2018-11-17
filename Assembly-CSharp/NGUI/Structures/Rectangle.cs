using System;
using UnityEngine;

namespace NGUI.Structures
{
	// Token: 0x020007FE RID: 2046
	public struct Rectangle
	{
		// Token: 0x17000E55 RID: 3669
		// (get) Token: 0x06004970 RID: 18800 RVA: 0x0012DFE0 File Offset: 0x0012C1E0
		// (set) Token: 0x06004971 RID: 18801 RVA: 0x0012E014 File Offset: 0x0012C214
		public Vector2 b
		{
			get
			{
				Vector2 result;
				result.x = this.d.x;
				result.y = this.a.y;
				return result;
			}
			set
			{
				this.d.x = value.x;
				this.a.y = value.y;
			}
		}

		// Token: 0x17000E56 RID: 3670
		// (get) Token: 0x06004972 RID: 18802 RVA: 0x0012E048 File Offset: 0x0012C248
		// (set) Token: 0x06004973 RID: 18803 RVA: 0x0012E07C File Offset: 0x0012C27C
		public Vector2 c
		{
			get
			{
				Vector2 result;
				result.x = this.a.x;
				result.y = this.d.y;
				return result;
			}
			set
			{
				this.a.x = value.x;
				this.d.y = value.y;
			}
		}

		// Token: 0x17000E57 RID: 3671
		// (get) Token: 0x06004974 RID: 18804 RVA: 0x0012E0B0 File Offset: 0x0012C2B0
		public Vector2 dim
		{
			get
			{
				Vector2 result;
				result.x = this.d.x - this.a.x;
				result.y = this.d.y - this.a.y;
				return result;
			}
		}

		// Token: 0x17000E58 RID: 3672
		// (get) Token: 0x06004975 RID: 18805 RVA: 0x0012E0FC File Offset: 0x0012C2FC
		public Vector2 center
		{
			get
			{
				Vector2 result;
				result.x = this.a.x + (this.d.x - this.a.x) * 0.5f;
				result.y = this.a.y + (this.d.y - this.a.y) * 0.5f;
				return result;
			}
		}

		// Token: 0x17000E59 RID: 3673
		// (get) Token: 0x06004976 RID: 18806 RVA: 0x0012E16C File Offset: 0x0012C36C
		public float height
		{
			get
			{
				return this.d.y - this.a.y;
			}
		}

		// Token: 0x17000E5A RID: 3674
		// (get) Token: 0x06004977 RID: 18807 RVA: 0x0012E188 File Offset: 0x0012C388
		public float width
		{
			get
			{
				return this.d.x - this.a.x;
			}
		}

		// Token: 0x17000E5B RID: 3675
		public Vector2 this[int i]
		{
			get
			{
				Vector2 result;
				result.x = (((i & 1) != 1) ? this.a.x : this.d.x);
				result.y = (((i & 2) != 2) ? this.a.y : this.d.y);
				return result;
			}
			set
			{
				if ((i & 1) == 1)
				{
					this.d.x = value.x;
				}
				else
				{
					this.a.x = value.x;
				}
				if ((i & 2) == 2)
				{
					this.d.y = value.y;
				}
				else
				{
					this.a.y = value.y;
				}
			}
		}

		// Token: 0x04002981 RID: 10625
		public const int size = 16;

		// Token: 0x04002982 RID: 10626
		public const int halfSize = 8;

		// Token: 0x04002983 RID: 10627
		public Vector2 a;

		// Token: 0x04002984 RID: 10628
		public Vector2 d;
	}
}
