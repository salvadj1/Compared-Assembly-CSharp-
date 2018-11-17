using System;
using UnityEngine;

namespace NGUI.Structures
{
	// Token: 0x020008F0 RID: 2288
	public struct Rectangle
	{
		// Token: 0x17000EEF RID: 3823
		// (get) Token: 0x06004E1F RID: 19999 RVA: 0x00137F44 File Offset: 0x00136144
		// (set) Token: 0x06004E20 RID: 20000 RVA: 0x00137F78 File Offset: 0x00136178
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

		// Token: 0x17000EF0 RID: 3824
		// (get) Token: 0x06004E21 RID: 20001 RVA: 0x00137FAC File Offset: 0x001361AC
		// (set) Token: 0x06004E22 RID: 20002 RVA: 0x00137FE0 File Offset: 0x001361E0
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

		// Token: 0x17000EF1 RID: 3825
		// (get) Token: 0x06004E23 RID: 20003 RVA: 0x00138014 File Offset: 0x00136214
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

		// Token: 0x17000EF2 RID: 3826
		// (get) Token: 0x06004E24 RID: 20004 RVA: 0x00138060 File Offset: 0x00136260
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

		// Token: 0x17000EF3 RID: 3827
		// (get) Token: 0x06004E25 RID: 20005 RVA: 0x001380D0 File Offset: 0x001362D0
		public float height
		{
			get
			{
				return this.d.y - this.a.y;
			}
		}

		// Token: 0x17000EF4 RID: 3828
		// (get) Token: 0x06004E26 RID: 20006 RVA: 0x001380EC File Offset: 0x001362EC
		public float width
		{
			get
			{
				return this.d.x - this.a.x;
			}
		}

		// Token: 0x17000EF5 RID: 3829
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

		// Token: 0x04002BCF RID: 11215
		public const int size = 16;

		// Token: 0x04002BD0 RID: 11216
		public const int halfSize = 8;

		// Token: 0x04002BD1 RID: 11217
		public Vector2 a;

		// Token: 0x04002BD2 RID: 11218
		public Vector2 d;
	}
}
