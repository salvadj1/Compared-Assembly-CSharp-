using System;
using UnityEngine;

// Token: 0x0200026D RID: 621
public struct AABBox : IEquatable<global::AABBox>
{
	// Token: 0x0600162A RID: 5674 RVA: 0x0004A3A4 File Offset: 0x000485A4
	public AABBox(Vector3 min, Vector3 max)
	{
		if (min.x > max.x)
		{
			this.m.x = max.x;
			this.M.x = min.x;
		}
		else
		{
			this.m.x = min.x;
			this.M.x = max.x;
		}
		if (min.y > max.y)
		{
			this.m.y = max.y;
			this.M.y = min.y;
		}
		else
		{
			this.m.y = min.y;
			this.M.y = max.y;
		}
		if (min.z > max.z)
		{
			this.m.z = max.z;
			this.M.z = min.z;
		}
		else
		{
			this.m.z = min.z;
			this.M.z = max.z;
		}
	}

	// Token: 0x0600162B RID: 5675 RVA: 0x0004A4D4 File Offset: 0x000486D4
	public AABBox(ref Vector3 min, ref Vector3 max)
	{
		if (min.x > max.x)
		{
			this.m.x = max.x;
			this.M.x = min.x;
		}
		else
		{
			this.m.x = min.x;
			this.M.x = max.x;
		}
		if (min.y > max.y)
		{
			this.m.y = max.y;
			this.M.y = min.y;
		}
		else
		{
			this.m.y = min.y;
			this.M.y = max.y;
		}
		if (min.z > max.z)
		{
			this.m.z = max.z;
			this.M.z = min.z;
		}
		else
		{
			this.m.z = min.z;
			this.M.z = max.z;
		}
	}

	// Token: 0x0600162C RID: 5676 RVA: 0x0004A5F0 File Offset: 0x000487F0
	public AABBox(ref Vector3 center)
	{
		this.m.x = (this.M.x = center.x);
		this.m.y = (this.M.y = center.y);
		this.m.z = (this.M.z = center.z);
	}

	// Token: 0x0600162D RID: 5677 RVA: 0x0004A65C File Offset: 0x0004885C
	public AABBox(Vector3 center)
	{
		this.m.x = (this.M.x = center.x);
		this.m.y = (this.M.y = center.y);
		this.m.z = (this.M.z = center.z);
	}

	// Token: 0x0600162E RID: 5678 RVA: 0x0004A6CC File Offset: 0x000488CC
	public AABBox(Bounds bounds)
	{
		this = new global::AABBox(bounds.min, bounds.max);
	}

	// Token: 0x0600162F RID: 5679 RVA: 0x0004A6E4 File Offset: 0x000488E4
	public AABBox(ref Bounds bounds)
	{
		this = new global::AABBox(bounds.min, bounds.max);
	}

	// Token: 0x1700064B RID: 1611
	// (get) Token: 0x06001630 RID: 5680 RVA: 0x0004A6F8 File Offset: 0x000488F8
	public Vector3 a
	{
		get
		{
			Vector3 result;
			result.x = this.m.x;
			result.y = this.m.y;
			result.z = this.m.z;
			return result;
		}
	}

	// Token: 0x1700064C RID: 1612
	// (get) Token: 0x06001631 RID: 5681 RVA: 0x0004A73C File Offset: 0x0004893C
	public Vector3 b
	{
		get
		{
			Vector3 result;
			result.x = this.m.x;
			result.y = this.m.y;
			result.z = this.M.z;
			return result;
		}
	}

	// Token: 0x1700064D RID: 1613
	// (get) Token: 0x06001632 RID: 5682 RVA: 0x0004A780 File Offset: 0x00048980
	public Vector3 c
	{
		get
		{
			Vector3 result;
			result.x = this.M.x;
			result.y = this.m.y;
			result.z = this.m.z;
			return result;
		}
	}

	// Token: 0x1700064E RID: 1614
	// (get) Token: 0x06001633 RID: 5683 RVA: 0x0004A7C4 File Offset: 0x000489C4
	public Vector3 d
	{
		get
		{
			Vector3 result;
			result.x = this.M.x;
			result.y = this.m.y;
			result.z = this.M.z;
			return result;
		}
	}

	// Token: 0x1700064F RID: 1615
	// (get) Token: 0x06001634 RID: 5684 RVA: 0x0004A808 File Offset: 0x00048A08
	public Vector3 e
	{
		get
		{
			Vector3 result;
			result.x = this.m.x;
			result.y = this.M.y;
			result.z = this.m.z;
			return result;
		}
	}

	// Token: 0x17000650 RID: 1616
	// (get) Token: 0x06001635 RID: 5685 RVA: 0x0004A84C File Offset: 0x00048A4C
	public Vector3 f
	{
		get
		{
			Vector3 result;
			result.x = this.m.x;
			result.y = this.M.y;
			result.z = this.M.z;
			return result;
		}
	}

	// Token: 0x17000651 RID: 1617
	// (get) Token: 0x06001636 RID: 5686 RVA: 0x0004A890 File Offset: 0x00048A90
	public Vector3 g
	{
		get
		{
			Vector3 result;
			result.x = this.M.x;
			result.y = this.M.y;
			result.z = this.m.z;
			return result;
		}
	}

	// Token: 0x17000652 RID: 1618
	// (get) Token: 0x06001637 RID: 5687 RVA: 0x0004A8D4 File Offset: 0x00048AD4
	public Vector3 h
	{
		get
		{
			Vector3 result;
			result.x = this.M.x;
			result.y = this.M.y;
			result.z = this.M.z;
			return result;
		}
	}

	// Token: 0x17000653 RID: 1619
	// (get) Token: 0x06001638 RID: 5688 RVA: 0x0004A918 File Offset: 0x00048B18
	public Vector3 line00
	{
		get
		{
			return this.a;
		}
	}

	// Token: 0x17000654 RID: 1620
	// (get) Token: 0x06001639 RID: 5689 RVA: 0x0004A920 File Offset: 0x00048B20
	public Vector3 line01
	{
		get
		{
			return this.b;
		}
	}

	// Token: 0x17000655 RID: 1621
	// (get) Token: 0x0600163A RID: 5690 RVA: 0x0004A928 File Offset: 0x00048B28
	public Vector3 line10
	{
		get
		{
			return this.a;
		}
	}

	// Token: 0x17000656 RID: 1622
	// (get) Token: 0x0600163B RID: 5691 RVA: 0x0004A930 File Offset: 0x00048B30
	public Vector3 line11
	{
		get
		{
			return this.c;
		}
	}

	// Token: 0x17000657 RID: 1623
	// (get) Token: 0x0600163C RID: 5692 RVA: 0x0004A938 File Offset: 0x00048B38
	public Vector3 line20
	{
		get
		{
			return this.a;
		}
	}

	// Token: 0x17000658 RID: 1624
	// (get) Token: 0x0600163D RID: 5693 RVA: 0x0004A940 File Offset: 0x00048B40
	public Vector3 line21
	{
		get
		{
			return this.e;
		}
	}

	// Token: 0x17000659 RID: 1625
	// (get) Token: 0x0600163E RID: 5694 RVA: 0x0004A948 File Offset: 0x00048B48
	public Vector3 line30
	{
		get
		{
			return this.b;
		}
	}

	// Token: 0x1700065A RID: 1626
	// (get) Token: 0x0600163F RID: 5695 RVA: 0x0004A950 File Offset: 0x00048B50
	public Vector3 line31
	{
		get
		{
			return this.d;
		}
	}

	// Token: 0x1700065B RID: 1627
	// (get) Token: 0x06001640 RID: 5696 RVA: 0x0004A958 File Offset: 0x00048B58
	public Vector3 line40
	{
		get
		{
			return this.b;
		}
	}

	// Token: 0x1700065C RID: 1628
	// (get) Token: 0x06001641 RID: 5697 RVA: 0x0004A960 File Offset: 0x00048B60
	public Vector3 line41
	{
		get
		{
			return this.f;
		}
	}

	// Token: 0x1700065D RID: 1629
	// (get) Token: 0x06001642 RID: 5698 RVA: 0x0004A968 File Offset: 0x00048B68
	public Vector3 line50
	{
		get
		{
			return this.c;
		}
	}

	// Token: 0x1700065E RID: 1630
	// (get) Token: 0x06001643 RID: 5699 RVA: 0x0004A970 File Offset: 0x00048B70
	public Vector3 line51
	{
		get
		{
			return this.d;
		}
	}

	// Token: 0x1700065F RID: 1631
	// (get) Token: 0x06001644 RID: 5700 RVA: 0x0004A978 File Offset: 0x00048B78
	public Vector3 line60
	{
		get
		{
			return this.c;
		}
	}

	// Token: 0x17000660 RID: 1632
	// (get) Token: 0x06001645 RID: 5701 RVA: 0x0004A980 File Offset: 0x00048B80
	public Vector3 line61
	{
		get
		{
			return this.g;
		}
	}

	// Token: 0x17000661 RID: 1633
	// (get) Token: 0x06001646 RID: 5702 RVA: 0x0004A988 File Offset: 0x00048B88
	public Vector3 line70
	{
		get
		{
			return this.d;
		}
	}

	// Token: 0x17000662 RID: 1634
	// (get) Token: 0x06001647 RID: 5703 RVA: 0x0004A990 File Offset: 0x00048B90
	public Vector3 line71
	{
		get
		{
			return this.h;
		}
	}

	// Token: 0x17000663 RID: 1635
	// (get) Token: 0x06001648 RID: 5704 RVA: 0x0004A998 File Offset: 0x00048B98
	public Vector3 line80
	{
		get
		{
			return this.e;
		}
	}

	// Token: 0x17000664 RID: 1636
	// (get) Token: 0x06001649 RID: 5705 RVA: 0x0004A9A0 File Offset: 0x00048BA0
	public Vector3 line81
	{
		get
		{
			return this.f;
		}
	}

	// Token: 0x17000665 RID: 1637
	// (get) Token: 0x0600164A RID: 5706 RVA: 0x0004A9A8 File Offset: 0x00048BA8
	public Vector3 line90
	{
		get
		{
			return this.e;
		}
	}

	// Token: 0x17000666 RID: 1638
	// (get) Token: 0x0600164B RID: 5707 RVA: 0x0004A9B0 File Offset: 0x00048BB0
	public Vector3 line91
	{
		get
		{
			return this.g;
		}
	}

	// Token: 0x17000667 RID: 1639
	// (get) Token: 0x0600164C RID: 5708 RVA: 0x0004A9B8 File Offset: 0x00048BB8
	public Vector3 lineA0
	{
		get
		{
			return this.f;
		}
	}

	// Token: 0x17000668 RID: 1640
	// (get) Token: 0x0600164D RID: 5709 RVA: 0x0004A9C0 File Offset: 0x00048BC0
	public Vector3 lineA1
	{
		get
		{
			return this.h;
		}
	}

	// Token: 0x17000669 RID: 1641
	// (get) Token: 0x0600164E RID: 5710 RVA: 0x0004A9C8 File Offset: 0x00048BC8
	public Vector3 lineB0
	{
		get
		{
			return this.g;
		}
	}

	// Token: 0x1700066A RID: 1642
	// (get) Token: 0x0600164F RID: 5711 RVA: 0x0004A9D0 File Offset: 0x00048BD0
	public Vector3 lineB1
	{
		get
		{
			return this.h;
		}
	}

	// Token: 0x06001650 RID: 5712 RVA: 0x0004A9D8 File Offset: 0x00048BD8
	public void SetMinMax(ref Vector3 min, ref Vector3 max)
	{
		if (min.x > max.x)
		{
			this.m.x = max.x;
			this.M.x = min.x;
		}
		else
		{
			this.m.x = min.x;
			this.M.x = max.x;
		}
		if (min.y > max.y)
		{
			this.m.y = max.y;
			this.M.y = min.y;
		}
		else
		{
			this.m.y = min.y;
			this.M.y = max.y;
		}
		if (min.z > max.z)
		{
			this.m.z = max.z;
			this.M.z = min.z;
		}
		else
		{
			this.m.z = min.z;
			this.M.z = max.z;
		}
	}

	// Token: 0x06001651 RID: 5713 RVA: 0x0004AAF4 File Offset: 0x00048CF4
	public void SetMinMax(ref Vector3 min, Vector3 max)
	{
		if (min.x > max.x)
		{
			this.m.x = max.x;
			this.M.x = min.x;
		}
		else
		{
			this.m.x = min.x;
			this.M.x = max.x;
		}
		if (min.y > max.y)
		{
			this.m.y = max.y;
			this.M.y = min.y;
		}
		else
		{
			this.m.y = min.y;
			this.M.y = max.y;
		}
		if (min.z > max.z)
		{
			this.m.z = max.z;
			this.M.z = min.z;
		}
		else
		{
			this.m.z = min.z;
			this.M.z = max.z;
		}
	}

	// Token: 0x06001652 RID: 5714 RVA: 0x0004AC18 File Offset: 0x00048E18
	public void SetMinMax(Vector3 min, ref Vector3 max)
	{
		if (min.x > max.x)
		{
			this.m.x = max.x;
			this.M.x = min.x;
		}
		else
		{
			this.m.x = min.x;
			this.M.x = max.x;
		}
		if (min.y > max.y)
		{
			this.m.y = max.y;
			this.M.y = min.y;
		}
		else
		{
			this.m.y = min.y;
			this.M.y = max.y;
		}
		if (min.z > max.z)
		{
			this.m.z = max.z;
			this.M.z = min.z;
		}
		else
		{
			this.m.z = min.z;
			this.M.z = max.z;
		}
	}

	// Token: 0x06001653 RID: 5715 RVA: 0x0004AD3C File Offset: 0x00048F3C
	public void SetMinMax(Vector3 min, Vector3 max)
	{
		if (min.x > max.x)
		{
			this.m.x = max.x;
			this.M.x = min.x;
		}
		else
		{
			this.m.x = min.x;
			this.M.x = max.x;
		}
		if (min.y > max.y)
		{
			this.m.y = max.y;
			this.M.y = min.y;
		}
		else
		{
			this.m.y = min.y;
			this.M.y = max.y;
		}
		if (min.z > max.z)
		{
			this.m.z = max.z;
			this.M.z = min.z;
		}
		else
		{
			this.m.z = min.z;
			this.M.z = max.z;
		}
	}

	// Token: 0x06001654 RID: 5716 RVA: 0x0004AE6C File Offset: 0x0004906C
	public void SetMinMax(Bounds bounds)
	{
		Vector3 min = bounds.min;
		Vector3 max = bounds.max;
		if (min.x > max.x)
		{
			this.m.x = max.x;
			this.M.x = min.x;
		}
		else
		{
			this.m.x = min.x;
			this.M.x = max.x;
		}
		if (min.y > max.y)
		{
			this.m.y = max.y;
			this.M.y = min.y;
		}
		else
		{
			this.m.y = min.y;
			this.M.y = max.y;
		}
		if (min.z > max.z)
		{
			this.m.z = max.z;
			this.M.z = min.z;
		}
		else
		{
			this.m.z = min.z;
			this.M.z = max.z;
		}
	}

	// Token: 0x06001655 RID: 5717 RVA: 0x0004AFAC File Offset: 0x000491AC
	public void SetMinMax(ref Bounds bounds)
	{
		Vector3 min = bounds.min;
		Vector3 max = bounds.max;
		if (min.x > max.x)
		{
			this.m.x = max.x;
			this.M.x = min.x;
		}
		else
		{
			this.m.x = min.x;
			this.M.x = max.x;
		}
		if (min.y > max.y)
		{
			this.m.y = max.y;
			this.M.y = min.y;
		}
		else
		{
			this.m.y = min.y;
			this.M.y = max.y;
		}
		if (min.z > max.z)
		{
			this.m.z = max.z;
			this.M.z = min.z;
		}
		else
		{
			this.m.z = min.z;
			this.M.z = max.z;
		}
	}

	// Token: 0x06001656 RID: 5718 RVA: 0x0004B0E8 File Offset: 0x000492E8
	public void EnsureMinMax()
	{
		if (this.m.x > this.M.x)
		{
			float x = this.m.x;
			this.m.x = this.M.x;
			this.M.x = x;
		}
		if (this.m.y > this.M.y)
		{
			float y = this.m.y;
			this.m.y = this.M.y;
			this.M.y = y;
		}
		if (this.m.z > this.M.z)
		{
			float z = this.m.z;
			this.m.z = this.M.z;
			this.M.z = z;
		}
	}

	// Token: 0x1700066B RID: 1643
	// (get) Token: 0x06001657 RID: 5719 RVA: 0x0004B1D0 File Offset: 0x000493D0
	public Vector3 min
	{
		get
		{
			Vector3 result;
			result.x = ((this.M.x >= this.m.x) ? this.m.x : this.M.x);
			result.y = ((this.M.y >= this.m.y) ? this.m.y : this.M.y);
			result.z = ((this.M.z >= this.m.z) ? this.m.z : this.M.z);
			return result;
		}
	}

	// Token: 0x1700066C RID: 1644
	// (get) Token: 0x06001658 RID: 5720 RVA: 0x0004B298 File Offset: 0x00049498
	public Vector3 max
	{
		get
		{
			Vector3 result;
			result.x = ((this.m.x <= this.M.x) ? this.M.x : this.m.x);
			result.y = ((this.m.y <= this.M.y) ? this.M.y : this.m.y);
			result.z = ((this.m.z <= this.M.z) ? this.M.z : this.m.z);
			return result;
		}
	}

	// Token: 0x1700066D RID: 1645
	// (get) Token: 0x06001659 RID: 5721 RVA: 0x0004B360 File Offset: 0x00049560
	// (set) Token: 0x0600165A RID: 5722 RVA: 0x0004B470 File Offset: 0x00049670
	public Vector3 size
	{
		get
		{
			Vector3 result;
			result.x = ((this.M.x >= this.m.x) ? (this.M.x - this.m.x) : (this.m.x - this.M.x));
			result.y = ((this.M.y >= this.m.y) ? (this.M.y - this.m.y) : (this.m.y - this.M.y));
			result.z = ((this.M.z >= this.m.z) ? (this.M.z - this.m.z) : (this.m.z - this.M.z));
			return result;
		}
		set
		{
			Vector3 vector;
			vector.x = this.m.x + (this.M.x - this.m.x) * 0.5f;
			vector.y = this.m.y + (this.M.y - this.m.y) * 0.5f;
			vector.z = this.m.z + (this.M.z - this.m.z) * 0.5f;
			if (value.x < 0f)
			{
				value.x *= -0.5f;
			}
			else
			{
				value.x *= 0.5f;
			}
			this.m.x = vector.x - value.x;
			this.M.x = vector.x + value.x;
			if (value.y < 0f)
			{
				value.y *= -0.5f;
			}
			else
			{
				value.y *= 0.5f;
			}
			this.m.y = vector.y - value.y;
			this.M.y = vector.y + value.y;
			if (value.z < 0f)
			{
				value.z *= -0.5f;
			}
			else
			{
				value.z *= 0.5f;
			}
			this.m.z = vector.z - value.z;
			this.M.z = vector.z + value.z;
		}
	}

	// Token: 0x1700066E RID: 1646
	// (get) Token: 0x0600165B RID: 5723 RVA: 0x0004B664 File Offset: 0x00049864
	// (set) Token: 0x0600165C RID: 5724 RVA: 0x0004B704 File Offset: 0x00049904
	public Vector3 center
	{
		get
		{
			Vector3 result;
			result.x = this.m.x + (this.M.x - this.m.x) * 0.5f;
			result.y = this.m.y + (this.M.y - this.m.y) * 0.5f;
			result.z = this.m.z + (this.M.z - this.m.z) * 0.5f;
			return result;
		}
		set
		{
			float num = value.x - (this.m.x + (this.M.x - this.m.x) * 0.5f);
			this.m.x = this.m.x + num;
			this.M.x = this.M.x + num;
			num = value.y - (this.m.y + (this.M.y - this.m.y) * 0.5f);
			this.m.y = this.m.y + num;
			this.M.y = this.M.y + num;
			num = value.z - (this.m.z + (this.M.z - this.m.z) * 0.5f);
			this.m.z = this.m.z + num;
			this.M.z = this.M.z + num;
		}
	}

	// Token: 0x1700066F RID: 1647
	// (get) Token: 0x0600165D RID: 5725 RVA: 0x0004B81C File Offset: 0x00049A1C
	public bool empty
	{
		get
		{
			return this.m.x == this.M.x && this.m.y == this.M.y && this.m.z == this.M.z;
		}
	}

	// Token: 0x17000670 RID: 1648
	// (get) Token: 0x0600165E RID: 5726 RVA: 0x0004B87C File Offset: 0x00049A7C
	public float volume
	{
		get
		{
			if (this.M.x == this.m.x || this.M.y == this.m.y || this.M.z == this.m.z)
			{
				return 0f;
			}
			if (this.M.x < this.m.x)
			{
				if (this.M.y < this.m.y)
				{
					if (this.M.z < this.m.z)
					{
						return (this.m.x - this.M.x) * (this.m.y - this.M.y) * (this.m.z - this.M.z);
					}
					return (this.m.x - this.M.x) * (this.m.y - this.M.y) * (this.M.z - this.m.z);
				}
				else
				{
					if (this.M.z < this.m.z)
					{
						return (this.m.x - this.M.x) * (this.M.y - this.m.y) * (this.m.z - this.M.z);
					}
					return (this.m.x - this.M.x) * (this.M.y - this.m.y) * (this.M.z - this.m.z);
				}
			}
			else if (this.M.y < this.m.y)
			{
				if (this.M.z < this.m.z)
				{
					return (this.M.x - this.m.x) * (this.m.y - this.M.y) * (this.m.z - this.M.z);
				}
				return (this.M.x - this.m.x) * (this.m.y - this.M.y) * (this.M.z - this.m.z);
			}
			else
			{
				if (this.M.z < this.m.z)
				{
					return (this.M.x - this.m.x) * (this.M.y - this.m.y) * (this.m.z - this.M.z);
				}
				return (this.M.x - this.m.x) * (this.M.y - this.m.y) * (this.M.z - this.m.z);
			}
		}
	}

	// Token: 0x17000671 RID: 1649
	// (get) Token: 0x0600165F RID: 5727 RVA: 0x0004BBDC File Offset: 0x00049DDC
	public float surfaceArea
	{
		get
		{
			Vector3 vector;
			vector.x = ((this.M.x >= this.m.x) ? (this.M.x - this.m.x) : (this.m.x - this.M.x));
			vector.y = ((this.M.y >= this.m.y) ? (this.M.y - this.m.y) : (this.m.y - this.M.y));
			vector.z = ((this.M.z >= this.m.z) ? (this.M.z - this.m.z) : (this.m.z - this.M.z));
			return 2f * vector.x * vector.y + 2f * vector.y * vector.z + 2f * vector.x * vector.z;
		}
	}

	// Token: 0x06001660 RID: 5728 RVA: 0x0004BD2C File Offset: 0x00049F2C
	public void Encapsulate(ref Vector3 v)
	{
		if (v.x < this.m.x)
		{
			this.m.x = v.x;
		}
		if (v.x > this.M.x)
		{
			this.M.x = v.x;
		}
		if (v.y < this.m.y)
		{
			this.m.y = v.y;
		}
		if (v.y > this.M.y)
		{
			this.M.y = v.y;
		}
		if (v.z < this.m.z)
		{
			this.m.z = v.z;
		}
		if (v.z > this.M.z)
		{
			this.M.z = v.z;
		}
	}

	// Token: 0x06001661 RID: 5729 RVA: 0x0004BE24 File Offset: 0x0004A024
	public void Encapsulate(Vector3 v)
	{
		if (v.x < this.m.x)
		{
			this.m.x = v.x;
		}
		if (v.x > this.M.x)
		{
			this.M.x = v.x;
		}
		if (v.y < this.m.y)
		{
			this.m.y = v.y;
		}
		if (v.y > this.M.y)
		{
			this.M.y = v.y;
		}
		if (v.z < this.m.z)
		{
			this.m.z = v.z;
		}
		if (v.z > this.M.z)
		{
			this.M.z = v.z;
		}
	}

	// Token: 0x06001662 RID: 5730 RVA: 0x0004BF28 File Offset: 0x0004A128
	public void Encapsulate(ref global::AABBox v)
	{
		if (v.M.x < v.m.x)
		{
			if (v.M.x < this.m.x)
			{
				this.m.x = v.M.x;
			}
			if (v.m.x > this.M.x)
			{
				this.M.x = v.m.x;
			}
		}
		else
		{
			if (v.m.x < this.m.x)
			{
				this.m.x = v.m.x;
			}
			if (v.M.x > this.M.x)
			{
				this.M.x = v.M.x;
			}
		}
		if (v.M.y < v.m.y)
		{
			if (v.M.y < this.m.y)
			{
				this.m.y = v.M.y;
			}
			if (v.m.y > this.M.y)
			{
				this.M.y = v.m.y;
			}
		}
		else
		{
			if (v.m.y < this.m.y)
			{
				this.m.y = v.m.y;
			}
			if (v.M.y > this.M.y)
			{
				this.M.y = v.M.y;
			}
		}
		if (v.M.z < v.m.z)
		{
			if (v.M.z < this.m.z)
			{
				this.m.z = v.M.z;
			}
			if (v.m.z > this.M.z)
			{
				this.M.z = v.m.z;
			}
		}
		else
		{
			if (v.m.z < this.m.z)
			{
				this.m.z = v.m.z;
			}
			if (v.M.z > this.M.z)
			{
				this.M.z = v.M.z;
			}
		}
	}

	// Token: 0x06001663 RID: 5731 RVA: 0x0004C1E4 File Offset: 0x0004A3E4
	public void Encapsulate(global::AABBox v)
	{
		if (v.M.x < v.m.x)
		{
			if (v.M.x < this.m.x)
			{
				this.m.x = v.M.x;
			}
			if (v.m.x > this.M.x)
			{
				this.M.x = v.m.x;
			}
		}
		else
		{
			if (v.m.x < this.m.x)
			{
				this.m.x = v.m.x;
			}
			if (v.M.x > this.M.x)
			{
				this.M.x = v.M.x;
			}
		}
		if (v.M.y < v.m.y)
		{
			if (v.M.y < this.m.y)
			{
				this.m.y = v.M.y;
			}
			if (v.m.y > this.M.y)
			{
				this.M.y = v.m.y;
			}
		}
		else
		{
			if (v.m.y < this.m.y)
			{
				this.m.y = v.m.y;
			}
			if (v.M.y > this.M.y)
			{
				this.M.y = v.M.y;
			}
		}
		if (v.M.z < v.m.z)
		{
			if (v.M.z < this.m.z)
			{
				this.m.z = v.M.z;
			}
			if (v.m.z > this.M.z)
			{
				this.M.z = v.m.z;
			}
		}
		else
		{
			if (v.m.z < this.m.z)
			{
				this.m.z = v.m.z;
			}
			if (v.M.z > this.M.z)
			{
				this.M.z = v.M.z;
			}
		}
	}

	// Token: 0x06001664 RID: 5732 RVA: 0x0004C4BC File Offset: 0x0004A6BC
	public void Encapsulate(ref Vector3 min, ref Vector3 max)
	{
		if (max.x < min.x)
		{
			if (max.x < this.m.x)
			{
				this.m.x = max.x;
			}
			if (min.x > this.M.x)
			{
				this.M.x = min.x;
			}
		}
		else
		{
			if (min.x < this.m.x)
			{
				this.m.x = min.x;
			}
			if (max.x > this.M.x)
			{
				this.M.x = max.x;
			}
		}
		if (max.y < min.y)
		{
			if (max.y < this.m.y)
			{
				this.m.y = max.y;
			}
			if (min.y > this.M.y)
			{
				this.M.y = min.y;
			}
		}
		else
		{
			if (min.y < this.m.y)
			{
				this.m.y = min.y;
			}
			if (max.y > this.M.y)
			{
				this.M.y = max.y;
			}
		}
		if (max.z < min.z)
		{
			if (max.z < this.m.z)
			{
				this.m.z = max.z;
			}
			if (min.z > this.M.z)
			{
				this.M.z = min.z;
			}
		}
		else
		{
			if (min.z < this.m.z)
			{
				this.m.z = min.z;
			}
			if (max.z > this.M.z)
			{
				this.M.z = max.z;
			}
		}
	}

	// Token: 0x06001665 RID: 5733 RVA: 0x0004C6E0 File Offset: 0x0004A8E0
	public void Encapsulate(Vector3 min, ref Vector3 max)
	{
		if (max.x < min.x)
		{
			if (max.x < this.m.x)
			{
				this.m.x = max.x;
			}
			if (min.x > this.M.x)
			{
				this.M.x = min.x;
			}
		}
		else
		{
			if (min.x < this.m.x)
			{
				this.m.x = min.x;
			}
			if (max.x > this.M.x)
			{
				this.M.x = max.x;
			}
		}
		if (max.y < min.y)
		{
			if (max.y < this.m.y)
			{
				this.m.y = max.y;
			}
			if (min.y > this.M.y)
			{
				this.M.y = min.y;
			}
		}
		else
		{
			if (min.y < this.m.y)
			{
				this.m.y = min.y;
			}
			if (max.y > this.M.y)
			{
				this.M.y = max.y;
			}
		}
		if (max.z < min.z)
		{
			if (max.z < this.m.z)
			{
				this.m.z = max.z;
			}
			if (min.z > this.M.z)
			{
				this.M.z = min.z;
			}
		}
		else
		{
			if (min.z < this.m.z)
			{
				this.m.z = min.z;
			}
			if (max.z > this.M.z)
			{
				this.M.z = max.z;
			}
		}
	}

	// Token: 0x06001666 RID: 5734 RVA: 0x0004C914 File Offset: 0x0004AB14
	public void Encapsulate(ref Vector3 min, Vector3 max)
	{
		if (max.x < min.x)
		{
			if (max.x < this.m.x)
			{
				this.m.x = max.x;
			}
			if (min.x > this.M.x)
			{
				this.M.x = min.x;
			}
		}
		else
		{
			if (min.x < this.m.x)
			{
				this.m.x = min.x;
			}
			if (max.x > this.M.x)
			{
				this.M.x = max.x;
			}
		}
		if (max.y < min.y)
		{
			if (max.y < this.m.y)
			{
				this.m.y = max.y;
			}
			if (min.y > this.M.y)
			{
				this.M.y = min.y;
			}
		}
		else
		{
			if (min.y < this.m.y)
			{
				this.m.y = min.y;
			}
			if (max.y > this.M.y)
			{
				this.M.y = max.y;
			}
		}
		if (max.z < min.z)
		{
			if (max.z < this.m.z)
			{
				this.m.z = max.z;
			}
			if (min.z > this.M.z)
			{
				this.M.z = min.z;
			}
		}
		else
		{
			if (min.z < this.m.z)
			{
				this.m.z = min.z;
			}
			if (max.z > this.M.z)
			{
				this.M.z = max.z;
			}
		}
	}

	// Token: 0x06001667 RID: 5735 RVA: 0x0004CB48 File Offset: 0x0004AD48
	public void Encapsulate(Vector3 min, Vector3 max)
	{
		if (max.x < min.x)
		{
			if (max.x < this.m.x)
			{
				this.m.x = max.x;
			}
			if (min.x > this.M.x)
			{
				this.M.x = min.x;
			}
		}
		else
		{
			if (min.x < this.m.x)
			{
				this.m.x = min.x;
			}
			if (max.x > this.M.x)
			{
				this.M.x = max.x;
			}
		}
		if (max.y < min.y)
		{
			if (max.y < this.m.y)
			{
				this.m.y = max.y;
			}
			if (min.y > this.M.y)
			{
				this.M.y = min.y;
			}
		}
		else
		{
			if (min.y < this.m.y)
			{
				this.m.y = min.y;
			}
			if (max.y > this.M.y)
			{
				this.M.y = max.y;
			}
		}
		if (max.z < min.z)
		{
			if (max.z < this.m.z)
			{
				this.m.z = max.z;
			}
			if (min.z > this.M.z)
			{
				this.M.z = min.z;
			}
		}
		else
		{
			if (min.z < this.m.z)
			{
				this.m.z = min.z;
			}
			if (max.z > this.M.z)
			{
				this.M.z = max.z;
			}
		}
	}

	// Token: 0x06001668 RID: 5736 RVA: 0x0004CD8C File Offset: 0x0004AF8C
	public void Encapsulate(ref Bounds bounds)
	{
		Vector3 min = bounds.min;
		Vector3 max = bounds.max;
		if (max.x < min.x)
		{
			if (max.x < this.m.x)
			{
				this.m.x = max.x;
			}
			if (min.x > this.M.x)
			{
				this.M.x = min.x;
			}
		}
		else
		{
			if (min.x < this.m.x)
			{
				this.m.x = min.x;
			}
			if (max.x > this.M.x)
			{
				this.M.x = max.x;
			}
		}
		if (max.y < min.y)
		{
			if (max.y < this.m.y)
			{
				this.m.y = max.y;
			}
			if (min.y > this.M.y)
			{
				this.M.y = min.y;
			}
		}
		else
		{
			if (min.y < this.m.y)
			{
				this.m.y = min.y;
			}
			if (max.y > this.M.y)
			{
				this.M.y = max.y;
			}
		}
		if (max.z < min.z)
		{
			if (max.z < this.m.z)
			{
				this.m.z = max.z;
			}
			if (min.z > this.M.z)
			{
				this.M.z = min.z;
			}
		}
		else
		{
			if (min.z < this.m.z)
			{
				this.m.z = min.z;
			}
			if (max.z > this.M.z)
			{
				this.M.z = max.z;
			}
		}
	}

	// Token: 0x06001669 RID: 5737 RVA: 0x0004CFDC File Offset: 0x0004B1DC
	public void Encapsulate(Bounds bounds)
	{
		Vector3 min = bounds.min;
		Vector3 max = bounds.max;
		if (max.x < min.x)
		{
			if (max.x < this.m.x)
			{
				this.m.x = max.x;
			}
			if (min.x > this.M.x)
			{
				this.M.x = min.x;
			}
		}
		else
		{
			if (min.x < this.m.x)
			{
				this.m.x = min.x;
			}
			if (max.x > this.M.x)
			{
				this.M.x = max.x;
			}
		}
		if (max.y < min.y)
		{
			if (max.y < this.m.y)
			{
				this.m.y = max.y;
			}
			if (min.y > this.M.y)
			{
				this.M.y = min.y;
			}
		}
		else
		{
			if (min.y < this.m.y)
			{
				this.m.y = min.y;
			}
			if (max.y > this.M.y)
			{
				this.M.y = max.y;
			}
		}
		if (max.z < min.z)
		{
			if (max.z < this.m.z)
			{
				this.m.z = max.z;
			}
			if (min.z > this.M.z)
			{
				this.M.z = min.z;
			}
		}
		else
		{
			if (min.z < this.m.z)
			{
				this.m.z = min.z;
			}
			if (max.z > this.M.z)
			{
				this.M.z = max.z;
			}
		}
	}

	// Token: 0x0600166A RID: 5738 RVA: 0x0004D230 File Offset: 0x0004B430
	public bool Contains(ref Vector3 v)
	{
		return this.m.x <= this.M.x && this.m.y <= this.M.y && this.m.z <= this.M.z && v.x >= this.m.x && v.y >= this.m.y && v.z >= this.m.z && v.x <= this.M.x && v.y <= this.M.y && v.z <= this.M.z;
	}

	// Token: 0x17000672 RID: 1650
	public Vector3 this[int corner]
	{
		get
		{
			Vector3 result;
			result.x = (((corner & 2) != 2) ? this.m.x : this.M.x);
			result.y = (((corner & 4) != 4) ? this.m.y : this.M.y);
			result.z = (((corner & 1) != 1) ? this.m.z : this.M.z);
			return result;
		}
	}

	// Token: 0x17000673 RID: 1651
	public float this[int corner, int axis]
	{
		get
		{
			switch (axis)
			{
			case 0:
				return ((corner & 2) != 2) ? this.m.x : this.M.x;
			case 1:
				return ((corner & 4) != 4) ? this.m.y : this.M.y;
			case 2:
				return ((corner & 1) != 1) ? this.m.z : this.M.z;
			default:
				throw new ArgumentOutOfRangeException("axis", axis, "axis<0||axis>2");
			}
		}
	}

	// Token: 0x0600166D RID: 5741 RVA: 0x0004D454 File Offset: 0x0004B654
	public global::BBox ToBBox()
	{
		global::BBox result;
		result.a.x = this.m.x;
		result.a.y = this.m.y;
		result.a.z = this.m.z;
		result.b.x = this.m.x;
		result.b.y = this.m.y;
		result.b.z = this.M.z;
		result.c.x = this.M.x;
		result.c.y = this.m.y;
		result.c.z = this.m.z;
		result.d.x = this.M.x;
		result.d.y = this.m.y;
		result.d.z = this.M.z;
		result.e.x = this.m.x;
		result.e.y = this.M.y;
		result.e.z = this.m.z;
		result.f.x = this.m.x;
		result.f.y = this.M.y;
		result.f.z = this.M.z;
		result.g.x = this.M.x;
		result.g.y = this.M.y;
		result.g.z = this.m.z;
		result.h.x = this.M.x;
		result.h.y = this.M.y;
		result.h.z = this.M.z;
		return result;
	}

	// Token: 0x0600166E RID: 5742 RVA: 0x0004D68C File Offset: 0x0004B88C
	public void ToBBox(out global::BBox box)
	{
		box.a.x = this.m.x;
		box.a.y = this.m.y;
		box.a.z = this.m.z;
		box.b.x = this.m.x;
		box.b.y = this.m.y;
		box.b.z = this.M.z;
		box.c.x = this.M.x;
		box.c.y = this.m.y;
		box.c.z = this.m.z;
		box.d.x = this.M.x;
		box.d.y = this.m.y;
		box.d.z = this.M.z;
		box.e.x = this.m.x;
		box.e.y = this.M.y;
		box.e.z = this.m.z;
		box.f.x = this.m.x;
		box.f.y = this.M.y;
		box.f.z = this.M.z;
		box.g.x = this.M.x;
		box.g.y = this.M.y;
		box.g.z = this.m.z;
		box.h.x = this.M.x;
		box.h.y = this.M.y;
		box.h.z = this.M.z;
	}

	// Token: 0x0600166F RID: 5743 RVA: 0x0004D8AC File Offset: 0x0004BAAC
	public void TransformedAABB3x4(ref Matrix4x4 t, out global::AABBox mM)
	{
		Vector3 vector;
		vector.x = this.m.x;
		vector.y = this.m.y;
		vector.z = this.m.z;
		float num = t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03;
		float num2 = t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13;
		float num3 = t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23;
		mM.m.x = (mM.M.x = num);
		mM.m.y = (mM.M.y = num2);
		mM.m.z = (mM.M.z = num3);
		vector.x = this.m.x;
		vector.y = this.m.y;
		vector.z = this.M.z;
		num = t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03;
		num2 = t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13;
		num3 = t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23;
		if (num < mM.m.x)
		{
			mM.m.x = num;
		}
		if (num > mM.M.x)
		{
			mM.M.x = num;
		}
		if (num2 < mM.m.y)
		{
			mM.m.y = num2;
		}
		if (num2 > mM.M.y)
		{
			mM.M.y = num2;
		}
		if (num3 < mM.m.z)
		{
			mM.m.z = num3;
		}
		if (num3 > mM.M.z)
		{
			mM.M.z = num3;
		}
		vector.x = this.M.x;
		vector.y = this.m.y;
		vector.z = this.m.z;
		num = t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03;
		num2 = t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13;
		num3 = t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23;
		if (num < mM.m.x)
		{
			mM.m.x = num;
		}
		if (num > mM.M.x)
		{
			mM.M.x = num;
		}
		if (num2 < mM.m.y)
		{
			mM.m.y = num2;
		}
		if (num2 > mM.M.y)
		{
			mM.M.y = num2;
		}
		if (num3 < mM.m.z)
		{
			mM.m.z = num3;
		}
		if (num3 > mM.M.z)
		{
			mM.M.z = num3;
		}
		vector.x = this.M.x;
		vector.y = this.m.y;
		vector.z = this.M.z;
		num = t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03;
		num2 = t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13;
		num3 = t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23;
		if (num < mM.m.x)
		{
			mM.m.x = num;
		}
		if (num > mM.M.x)
		{
			mM.M.x = num;
		}
		if (num2 < mM.m.y)
		{
			mM.m.y = num2;
		}
		if (num2 > mM.M.y)
		{
			mM.M.y = num2;
		}
		if (num3 < mM.m.z)
		{
			mM.m.z = num3;
		}
		if (num3 > mM.M.z)
		{
			mM.M.z = num3;
		}
		vector.x = this.m.x;
		vector.y = this.M.y;
		vector.z = this.m.z;
		num = t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03;
		num2 = t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13;
		num3 = t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23;
		if (num < mM.m.x)
		{
			mM.m.x = num;
		}
		if (num > mM.M.x)
		{
			mM.M.x = num;
		}
		if (num2 < mM.m.y)
		{
			mM.m.y = num2;
		}
		if (num2 > mM.M.y)
		{
			mM.M.y = num2;
		}
		if (num3 < mM.m.z)
		{
			mM.m.z = num3;
		}
		if (num3 > mM.M.z)
		{
			mM.M.z = num3;
		}
		vector.x = this.m.x;
		vector.y = this.M.y;
		vector.z = this.M.z;
		num = t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03;
		num2 = t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13;
		num3 = t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23;
		if (num < mM.m.x)
		{
			mM.m.x = num;
		}
		if (num > mM.M.x)
		{
			mM.M.x = num;
		}
		if (num2 < mM.m.y)
		{
			mM.m.y = num2;
		}
		if (num2 > mM.M.y)
		{
			mM.M.y = num2;
		}
		if (num3 < mM.m.z)
		{
			mM.m.z = num3;
		}
		if (num3 > mM.M.z)
		{
			mM.M.z = num3;
		}
		vector.x = this.M.x;
		vector.y = this.M.y;
		vector.z = this.m.z;
		num = t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03;
		num2 = t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13;
		num3 = t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23;
		if (num < mM.m.x)
		{
			mM.m.x = num;
		}
		if (num > mM.M.x)
		{
			mM.M.x = num;
		}
		if (num2 < mM.m.y)
		{
			mM.m.y = num2;
		}
		if (num2 > mM.M.y)
		{
			mM.M.y = num2;
		}
		if (num3 < mM.m.z)
		{
			mM.m.z = num3;
		}
		if (num3 > mM.M.z)
		{
			mM.M.z = num3;
		}
		vector.x = this.M.x;
		vector.y = this.M.y;
		vector.z = this.M.z;
		num = t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03;
		num2 = t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13;
		num3 = t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23;
		if (num < mM.m.x)
		{
			mM.m.x = num;
		}
		if (num > mM.M.x)
		{
			mM.M.x = num;
		}
		if (num2 < mM.m.y)
		{
			mM.m.y = num2;
		}
		if (num2 > mM.M.y)
		{
			mM.M.y = num2;
		}
		if (num3 < mM.m.z)
		{
			mM.m.z = num3;
		}
		if (num3 > mM.M.z)
		{
			mM.M.z = num3;
		}
	}

	// Token: 0x06001670 RID: 5744 RVA: 0x0004E460 File Offset: 0x0004C660
	public void TransformedAABB3x4(ref Matrix4x4 t, out Bounds bounds)
	{
		global::AABBox aabbox;
		this.TransformedAABB3x4(ref t, out aabbox);
		Vector3 vector;
		vector.x = aabbox.M.x - aabbox.m.x;
		Vector3 vector2;
		vector2.x = aabbox.m.x + vector.x * 0.5f;
		vector.y = aabbox.M.y - aabbox.m.y;
		vector2.y = aabbox.m.y + vector.y * 0.5f;
		vector.z = aabbox.M.z - aabbox.m.z;
		vector2.z = aabbox.m.z + vector.z * 0.5f;
		bounds..ctor(vector2, vector);
	}

	// Token: 0x06001671 RID: 5745 RVA: 0x0004E544 File Offset: 0x0004C744
	public void ToBoxCorners3x4(ref Matrix4x4 t, out global::BBox box, out global::AABBox mM)
	{
		Vector3 vector;
		vector.x = this.m.x;
		vector.y = this.m.y;
		vector.z = this.m.z;
		box.a.x = t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03;
		box.a.y = t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13;
		box.a.z = t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23;
		mM.m = box.a;
		mM.M = box.a;
		vector.x = this.m.x;
		vector.y = this.m.y;
		vector.z = this.M.z;
		box.b.x = t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03;
		box.b.y = t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13;
		box.b.z = t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23;
		if (box.b.x < mM.m.x)
		{
			mM.m.x = box.b.x;
		}
		if (box.b.x > mM.M.x)
		{
			mM.M.x = box.b.x;
		}
		if (box.b.y < mM.m.y)
		{
			mM.m.y = box.b.y;
		}
		if (box.b.y > mM.M.y)
		{
			mM.M.y = box.b.y;
		}
		if (box.b.z < mM.m.z)
		{
			mM.m.z = box.b.z;
		}
		if (box.b.z > mM.M.z)
		{
			mM.M.z = box.b.z;
		}
		vector.x = this.M.x;
		vector.y = this.m.y;
		vector.z = this.m.z;
		box.c.x = t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03;
		box.c.y = t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13;
		box.c.z = t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23;
		if (box.c.x < mM.m.x)
		{
			mM.m.x = box.c.x;
		}
		if (box.c.x > mM.M.x)
		{
			mM.M.x = box.c.x;
		}
		if (box.c.y < mM.m.y)
		{
			mM.m.y = box.c.y;
		}
		if (box.c.y > mM.M.y)
		{
			mM.M.y = box.c.y;
		}
		if (box.c.z < mM.m.z)
		{
			mM.m.z = box.c.z;
		}
		if (box.c.z > mM.M.z)
		{
			mM.M.z = box.c.z;
		}
		vector.x = this.M.x;
		vector.y = this.m.y;
		vector.z = this.M.z;
		box.d.x = t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03;
		box.d.y = t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13;
		box.d.z = t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23;
		if (box.d.x < mM.m.x)
		{
			mM.m.x = box.d.x;
		}
		if (box.d.x > mM.M.x)
		{
			mM.M.x = box.d.x;
		}
		if (box.d.y < mM.m.y)
		{
			mM.m.y = box.d.y;
		}
		if (box.d.y > mM.M.y)
		{
			mM.M.y = box.d.y;
		}
		if (box.d.z < mM.m.z)
		{
			mM.m.z = box.d.z;
		}
		if (box.d.z > mM.M.z)
		{
			mM.M.z = box.d.z;
		}
		vector.x = this.m.x;
		vector.y = this.M.y;
		vector.z = this.m.z;
		box.e.x = t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03;
		box.e.y = t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13;
		box.e.z = t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23;
		if (box.e.x < mM.m.x)
		{
			mM.m.x = box.e.x;
		}
		if (box.e.x > mM.M.x)
		{
			mM.M.x = box.e.x;
		}
		if (box.e.y < mM.m.y)
		{
			mM.m.y = box.e.y;
		}
		if (box.e.y > mM.M.y)
		{
			mM.M.y = box.e.y;
		}
		if (box.e.z < mM.m.z)
		{
			mM.m.z = box.e.z;
		}
		if (box.e.z > mM.M.z)
		{
			mM.M.z = box.e.z;
		}
		vector.x = this.m.x;
		vector.y = this.M.y;
		vector.z = this.M.z;
		box.f.x = t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03;
		box.f.y = t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13;
		box.f.z = t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23;
		if (box.f.x < mM.m.x)
		{
			mM.m.x = box.f.x;
		}
		if (box.f.x > mM.M.x)
		{
			mM.M.x = box.f.x;
		}
		if (box.f.y < mM.m.y)
		{
			mM.m.y = box.f.y;
		}
		if (box.f.y > mM.M.y)
		{
			mM.M.y = box.f.y;
		}
		if (box.f.z < mM.m.z)
		{
			mM.m.z = box.f.z;
		}
		if (box.f.z > mM.M.z)
		{
			mM.M.z = box.f.z;
		}
		vector.x = this.M.x;
		vector.y = this.M.y;
		vector.z = this.m.z;
		box.g.x = t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03;
		box.g.y = t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13;
		box.g.z = t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23;
		if (box.g.x < mM.m.x)
		{
			mM.m.x = box.g.x;
		}
		if (box.g.x > mM.M.x)
		{
			mM.M.x = box.g.x;
		}
		if (box.g.y < mM.m.y)
		{
			mM.m.y = box.g.y;
		}
		if (box.g.y > mM.M.y)
		{
			mM.M.y = box.g.y;
		}
		if (box.g.z < mM.m.z)
		{
			mM.m.z = box.g.z;
		}
		if (box.g.z > mM.M.z)
		{
			mM.M.z = box.g.z;
		}
		vector.x = this.M.x;
		vector.y = this.M.y;
		vector.z = this.M.z;
		box.h.x = t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03;
		box.h.y = t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13;
		box.h.z = t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23;
		if (box.h.x < mM.m.x)
		{
			mM.m.x = box.h.x;
		}
		if (box.h.x > mM.M.x)
		{
			mM.M.x = box.h.x;
		}
		if (box.h.y < mM.m.y)
		{
			mM.m.y = box.h.y;
		}
		if (box.h.y > mM.M.y)
		{
			mM.M.y = box.h.y;
		}
		if (box.h.z < mM.m.z)
		{
			mM.m.z = box.h.z;
		}
		if (box.h.z > mM.M.z)
		{
			mM.M.z = box.h.z;
		}
	}

	// Token: 0x06001672 RID: 5746 RVA: 0x0004F4F4 File Offset: 0x0004D6F4
	public void TransformedAABB4x4(ref Matrix4x4 t, out global::AABBox mM)
	{
		Vector4 vector;
		vector.x = this.m.x;
		vector.y = this.m.y;
		vector.z = this.m.z;
		vector.w = 1f / (t.m30 * vector.x + t.m31 * vector.y + t.m32 * vector.z + t.m33);
		float num = (t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03) * vector.w;
		float num2 = (t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13) * vector.w;
		float num3 = (t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23) * vector.w;
		mM.m.x = (mM.M.x = num);
		mM.m.y = (mM.M.y = num2);
		mM.m.z = (mM.M.z = num3);
		vector.x = this.m.x;
		vector.y = this.m.y;
		vector.z = this.M.z;
		vector.w = 1f / (t.m30 * vector.x + t.m31 * vector.y + t.m32 * vector.z + t.m33);
		num = (t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03) * vector.w;
		num2 = (t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13) * vector.w;
		num3 = (t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23) * vector.w;
		if (num < mM.m.x)
		{
			mM.m.x = num;
		}
		if (num > mM.M.x)
		{
			mM.M.x = num;
		}
		if (num2 < mM.m.y)
		{
			mM.m.y = num2;
		}
		if (num2 > mM.M.y)
		{
			mM.M.y = num2;
		}
		if (num3 < mM.m.z)
		{
			mM.m.z = num3;
		}
		if (num3 > mM.M.z)
		{
			mM.M.z = num3;
		}
		vector.x = this.M.x;
		vector.y = this.m.y;
		vector.z = this.m.z;
		vector.w = 1f / (t.m30 * vector.x + t.m31 * vector.y + t.m32 * vector.z + t.m33);
		num = (t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03) * vector.w;
		num2 = (t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13) * vector.w;
		num3 = (t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23) * vector.w;
		if (num < mM.m.x)
		{
			mM.m.x = num;
		}
		if (num > mM.M.x)
		{
			mM.M.x = num;
		}
		if (num2 < mM.m.y)
		{
			mM.m.y = num2;
		}
		if (num2 > mM.M.y)
		{
			mM.M.y = num2;
		}
		if (num3 < mM.m.z)
		{
			mM.m.z = num3;
		}
		if (num3 > mM.M.z)
		{
			mM.M.z = num3;
		}
		vector.x = this.M.x;
		vector.y = this.m.y;
		vector.z = this.M.z;
		vector.w = 1f / (t.m30 * vector.x + t.m31 * vector.y + t.m32 * vector.z + t.m33);
		num = (t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03) * vector.w;
		num2 = (t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13) * vector.w;
		num3 = (t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23) * vector.w;
		if (num < mM.m.x)
		{
			mM.m.x = num;
		}
		if (num > mM.M.x)
		{
			mM.M.x = num;
		}
		if (num2 < mM.m.y)
		{
			mM.m.y = num2;
		}
		if (num2 > mM.M.y)
		{
			mM.M.y = num2;
		}
		if (num3 < mM.m.z)
		{
			mM.m.z = num3;
		}
		if (num3 > mM.M.z)
		{
			mM.M.z = num3;
		}
		vector.x = this.m.x;
		vector.y = this.M.y;
		vector.z = this.m.z;
		vector.w = 1f / (t.m30 * vector.x + t.m31 * vector.y + t.m32 * vector.z + t.m33);
		num = (t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03) * vector.w;
		num2 = (t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13) * vector.w;
		num3 = (t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23) * vector.w;
		if (num < mM.m.x)
		{
			mM.m.x = num;
		}
		if (num > mM.M.x)
		{
			mM.M.x = num;
		}
		if (num2 < mM.m.y)
		{
			mM.m.y = num2;
		}
		if (num2 > mM.M.y)
		{
			mM.M.y = num2;
		}
		if (num3 < mM.m.z)
		{
			mM.m.z = num3;
		}
		if (num3 > mM.M.z)
		{
			mM.M.z = num3;
		}
		vector.x = this.m.x;
		vector.y = this.M.y;
		vector.z = this.M.z;
		vector.w = 1f / (t.m30 * vector.x + t.m31 * vector.y + t.m32 * vector.z + t.m33);
		num = (t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03) * vector.w;
		num2 = (t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13) * vector.w;
		num3 = (t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23) * vector.w;
		if (num < mM.m.x)
		{
			mM.m.x = num;
		}
		if (num > mM.M.x)
		{
			mM.M.x = num;
		}
		if (num2 < mM.m.y)
		{
			mM.m.y = num2;
		}
		if (num2 > mM.M.y)
		{
			mM.M.y = num2;
		}
		if (num3 < mM.m.z)
		{
			mM.m.z = num3;
		}
		if (num3 > mM.M.z)
		{
			mM.M.z = num3;
		}
		vector.x = this.M.x;
		vector.y = this.M.y;
		vector.z = this.m.z;
		vector.w = 1f / (t.m30 * vector.x + t.m31 * vector.y + t.m32 * vector.z + t.m33);
		num = (t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03) * vector.w;
		num2 = (t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13) * vector.w;
		num3 = (t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23) * vector.w;
		if (num < mM.m.x)
		{
			mM.m.x = num;
		}
		if (num > mM.M.x)
		{
			mM.M.x = num;
		}
		if (num2 < mM.m.y)
		{
			mM.m.y = num2;
		}
		if (num2 > mM.M.y)
		{
			mM.M.y = num2;
		}
		if (num3 < mM.m.z)
		{
			mM.m.z = num3;
		}
		if (num3 > mM.M.z)
		{
			mM.M.z = num3;
		}
		vector.x = this.M.x;
		vector.y = this.M.y;
		vector.z = this.M.z;
		vector.w = 1f / (t.m30 * vector.x + t.m31 * vector.y + t.m32 * vector.z + t.m33);
		num = (t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03) * vector.w;
		num2 = (t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13) * vector.w;
		num3 = (t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23) * vector.w;
		if (num < mM.m.x)
		{
			mM.m.x = num;
		}
		if (num > mM.M.x)
		{
			mM.M.x = num;
		}
		if (num2 < mM.m.y)
		{
			mM.m.y = num2;
		}
		if (num2 > mM.M.y)
		{
			mM.M.y = num2;
		}
		if (num3 < mM.m.z)
		{
			mM.m.z = num3;
		}
		if (num3 > mM.M.z)
		{
			mM.M.z = num3;
		}
	}

	// Token: 0x06001673 RID: 5747 RVA: 0x00050368 File Offset: 0x0004E568
	public void TransformedAABB4x4(ref Matrix4x4 t, out Bounds bounds)
	{
		global::AABBox aabbox;
		this.TransformedAABB4x4(ref t, out aabbox);
		Vector3 vector;
		vector.x = aabbox.M.x - aabbox.m.x;
		Vector3 vector2;
		vector2.x = aabbox.m.x + vector.x * 0.5f;
		vector.y = aabbox.M.y - aabbox.m.y;
		vector2.y = aabbox.m.y + vector.y * 0.5f;
		vector.z = aabbox.M.z - aabbox.m.z;
		vector2.z = aabbox.m.z + vector.z * 0.5f;
		bounds..ctor(vector2, vector);
	}

	// Token: 0x06001674 RID: 5748 RVA: 0x0005044C File Offset: 0x0004E64C
	public void ToBoxCorners4x4(ref Matrix4x4 t, out global::BBox box, out global::AABBox mM)
	{
		Vector4 vector;
		vector.x = this.m.x;
		vector.y = this.m.y;
		vector.z = this.m.z;
		vector.w = 1f / (t.m30 * vector.x + t.m31 * vector.y + t.m32 * vector.z + t.m33);
		box.a.x = (t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03) * vector.w;
		box.a.y = (t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13) * vector.w;
		box.a.z = (t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23) * vector.w;
		mM.m = box.a;
		mM.M = box.a;
		vector.x = this.m.x;
		vector.y = this.m.y;
		vector.z = this.M.z;
		vector.w = 1f / (t.m30 * vector.x + t.m31 * vector.y + t.m32 * vector.z + t.m33);
		box.b.x = (t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03) * vector.w;
		box.b.y = (t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13) * vector.w;
		box.b.z = (t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23) * vector.w;
		if (box.b.x < mM.m.x)
		{
			mM.m.x = box.b.x;
		}
		if (box.b.x > mM.M.x)
		{
			mM.M.x = box.b.x;
		}
		if (box.b.y < mM.m.y)
		{
			mM.m.y = box.b.y;
		}
		if (box.b.y > mM.M.y)
		{
			mM.M.y = box.b.y;
		}
		if (box.b.z < mM.m.z)
		{
			mM.m.z = box.b.z;
		}
		if (box.b.z > mM.M.z)
		{
			mM.M.z = box.b.z;
		}
		vector.x = this.M.x;
		vector.y = this.m.y;
		vector.z = this.m.z;
		vector.w = 1f / (t.m30 * vector.x + t.m31 * vector.y + t.m32 * vector.z + t.m33);
		box.c.x = (t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03) * vector.w;
		box.c.y = (t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13) * vector.w;
		box.c.z = (t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23) * vector.w;
		if (box.c.x < mM.m.x)
		{
			mM.m.x = box.c.x;
		}
		if (box.c.x > mM.M.x)
		{
			mM.M.x = box.c.x;
		}
		if (box.c.y < mM.m.y)
		{
			mM.m.y = box.c.y;
		}
		if (box.c.y > mM.M.y)
		{
			mM.M.y = box.c.y;
		}
		if (box.c.z < mM.m.z)
		{
			mM.m.z = box.c.z;
		}
		if (box.c.z > mM.M.z)
		{
			mM.M.z = box.c.z;
		}
		vector.x = this.M.x;
		vector.y = this.m.y;
		vector.z = this.M.z;
		vector.w = 1f / (t.m30 * vector.x + t.m31 * vector.y + t.m32 * vector.z + t.m33);
		box.d.x = (t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03) * vector.w;
		box.d.y = (t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13) * vector.w;
		box.d.z = (t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23) * vector.w;
		if (box.d.x < mM.m.x)
		{
			mM.m.x = box.d.x;
		}
		if (box.d.x > mM.M.x)
		{
			mM.M.x = box.d.x;
		}
		if (box.d.y < mM.m.y)
		{
			mM.m.y = box.d.y;
		}
		if (box.d.y > mM.M.y)
		{
			mM.M.y = box.d.y;
		}
		if (box.d.z < mM.m.z)
		{
			mM.m.z = box.d.z;
		}
		if (box.d.z > mM.M.z)
		{
			mM.M.z = box.d.z;
		}
		vector.x = this.m.x;
		vector.y = this.M.y;
		vector.z = this.m.z;
		vector.w = 1f / (t.m30 * vector.x + t.m31 * vector.y + t.m32 * vector.z + t.m33);
		box.e.x = (t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03) * vector.w;
		box.e.y = (t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13) * vector.w;
		box.e.z = (t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23) * vector.w;
		if (box.e.x < mM.m.x)
		{
			mM.m.x = box.e.x;
		}
		if (box.e.x > mM.M.x)
		{
			mM.M.x = box.e.x;
		}
		if (box.e.y < mM.m.y)
		{
			mM.m.y = box.e.y;
		}
		if (box.e.y > mM.M.y)
		{
			mM.M.y = box.e.y;
		}
		if (box.e.z < mM.m.z)
		{
			mM.m.z = box.e.z;
		}
		if (box.e.z > mM.M.z)
		{
			mM.M.z = box.e.z;
		}
		vector.x = this.m.x;
		vector.y = this.M.y;
		vector.z = this.M.z;
		vector.w = 1f / (t.m30 * vector.x + t.m31 * vector.y + t.m32 * vector.z + t.m33);
		box.f.x = (t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03) * vector.w;
		box.f.y = (t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13) * vector.w;
		box.f.z = (t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23) * vector.w;
		if (box.f.x < mM.m.x)
		{
			mM.m.x = box.f.x;
		}
		if (box.f.x > mM.M.x)
		{
			mM.M.x = box.f.x;
		}
		if (box.f.y < mM.m.y)
		{
			mM.m.y = box.f.y;
		}
		if (box.f.y > mM.M.y)
		{
			mM.M.y = box.f.y;
		}
		if (box.f.z < mM.m.z)
		{
			mM.m.z = box.f.z;
		}
		if (box.f.z > mM.M.z)
		{
			mM.M.z = box.f.z;
		}
		vector.x = this.M.x;
		vector.y = this.M.y;
		vector.z = this.m.z;
		vector.w = 1f / (t.m30 * vector.x + t.m31 * vector.y + t.m32 * vector.z + t.m33);
		box.g.x = (t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03) * vector.w;
		box.g.y = (t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13) * vector.w;
		box.g.z = (t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23) * vector.w;
		if (box.g.x < mM.m.x)
		{
			mM.m.x = box.g.x;
		}
		if (box.g.x > mM.M.x)
		{
			mM.M.x = box.g.x;
		}
		if (box.g.y < mM.m.y)
		{
			mM.m.y = box.g.y;
		}
		if (box.g.y > mM.M.y)
		{
			mM.M.y = box.g.y;
		}
		if (box.g.z < mM.m.z)
		{
			mM.m.z = box.g.z;
		}
		if (box.g.z > mM.M.z)
		{
			mM.M.z = box.g.z;
		}
		vector.x = this.M.x;
		vector.y = this.M.y;
		vector.z = this.M.z;
		vector.w = 1f / (t.m30 * vector.x + t.m31 * vector.y + t.m32 * vector.z + t.m33);
		box.h.x = (t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03) * vector.w;
		box.h.y = (t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13) * vector.w;
		box.h.z = (t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23) * vector.w;
		if (box.h.x < mM.m.x)
		{
			mM.m.x = box.h.x;
		}
		if (box.h.x > mM.M.x)
		{
			mM.M.x = box.h.x;
		}
		if (box.h.y < mM.m.y)
		{
			mM.m.y = box.h.y;
		}
		if (box.h.y > mM.M.y)
		{
			mM.M.y = box.h.y;
		}
		if (box.h.z < mM.m.z)
		{
			mM.m.z = box.h.z;
		}
		if (box.h.z > mM.M.z)
		{
			mM.M.z = box.h.z;
		}
	}

	// Token: 0x06001675 RID: 5749 RVA: 0x000516BC File Offset: 0x0004F8BC
	public void ToBoxCorners3x4(ref Matrix4x4 t, out global::BBox box)
	{
		Vector3 vector;
		vector.x = this.m.x;
		vector.y = this.m.y;
		vector.z = this.m.z;
		box.a.x = t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03;
		box.a.y = t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13;
		box.a.z = t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23;
		vector.x = this.m.x;
		vector.y = this.m.y;
		vector.z = this.M.z;
		box.b.x = t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03;
		box.b.y = t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13;
		box.b.z = t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23;
		vector.x = this.M.x;
		vector.y = this.m.y;
		vector.z = this.m.z;
		box.c.x = t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03;
		box.c.y = t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13;
		box.c.z = t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23;
		vector.x = this.M.x;
		vector.y = this.m.y;
		vector.z = this.M.z;
		box.d.x = t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03;
		box.d.y = t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13;
		box.d.z = t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23;
		vector.x = this.m.x;
		vector.y = this.M.y;
		vector.z = this.m.z;
		box.e.x = t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03;
		box.e.y = t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13;
		box.e.z = t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23;
		vector.x = this.m.x;
		vector.y = this.M.y;
		vector.z = this.M.z;
		box.f.x = t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03;
		box.f.y = t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13;
		box.f.z = t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23;
		vector.x = this.M.x;
		vector.y = this.M.y;
		vector.z = this.m.z;
		box.g.x = t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03;
		box.g.y = t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13;
		box.g.z = t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23;
		vector.x = this.M.x;
		vector.y = this.M.y;
		vector.z = this.M.z;
		box.h.x = t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03;
		box.h.y = t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13;
		box.h.z = t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23;
	}

	// Token: 0x06001676 RID: 5750 RVA: 0x00051E4C File Offset: 0x0005004C
	public void ToBoxCorners4x4(ref Matrix4x4 t, out global::BBox box)
	{
		Vector4 vector;
		vector.x = this.m.x;
		vector.y = this.m.y;
		vector.z = this.m.z;
		vector.w = 1f / (t.m30 * vector.x + t.m31 * vector.y + t.m32 * vector.z + t.m33);
		box.a.x = (t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03) * vector.w;
		box.a.y = (t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13) * vector.w;
		box.a.z = (t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23) * vector.w;
		vector.x = this.m.x;
		vector.y = this.m.y;
		vector.z = this.M.z;
		vector.w = 1f / (t.m30 * vector.x + t.m31 * vector.y + t.m32 * vector.z + t.m33);
		box.b.x = (t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03) * vector.w;
		box.b.y = (t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13) * vector.w;
		box.b.z = (t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23) * vector.w;
		vector.x = this.M.x;
		vector.y = this.m.y;
		vector.z = this.m.z;
		vector.w = 1f / (t.m30 * vector.x + t.m31 * vector.y + t.m32 * vector.z + t.m33);
		box.c.x = (t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03) * vector.w;
		box.c.y = (t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13) * vector.w;
		box.c.z = (t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23) * vector.w;
		vector.x = this.M.x;
		vector.y = this.m.y;
		vector.z = this.M.z;
		vector.w = 1f / (t.m30 * vector.x + t.m31 * vector.y + t.m32 * vector.z + t.m33);
		box.d.x = (t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03) * vector.w;
		box.d.y = (t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13) * vector.w;
		box.d.z = (t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23) * vector.w;
		vector.x = this.m.x;
		vector.y = this.M.y;
		vector.z = this.m.z;
		vector.w = 1f / (t.m30 * vector.x + t.m31 * vector.y + t.m32 * vector.z + t.m33);
		box.e.x = (t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03) * vector.w;
		box.e.y = (t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13) * vector.w;
		box.e.z = (t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23) * vector.w;
		vector.x = this.m.x;
		vector.y = this.M.y;
		vector.z = this.M.z;
		vector.w = 1f / (t.m30 * vector.x + t.m31 * vector.y + t.m32 * vector.z + t.m33);
		box.f.x = (t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03) * vector.w;
		box.f.y = (t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13) * vector.w;
		box.f.z = (t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23) * vector.w;
		vector.x = this.M.x;
		vector.y = this.M.y;
		vector.z = this.m.z;
		vector.w = 1f / (t.m30 * vector.x + t.m31 * vector.y + t.m32 * vector.z + t.m33);
		box.g.x = (t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03) * vector.w;
		box.g.y = (t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13) * vector.w;
		box.g.z = (t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23) * vector.w;
		vector.x = this.M.x;
		vector.y = this.M.y;
		vector.z = this.M.z;
		vector.w = 1f / (t.m30 * vector.x + t.m31 * vector.y + t.m32 * vector.z + t.m33);
		box.h.x = (t.m00 * vector.x + t.m01 * vector.y + t.m02 * vector.z + t.m03) * vector.w;
		box.h.y = (t.m10 * vector.x + t.m11 * vector.y + t.m12 * vector.z + t.m13) * vector.w;
		box.h.z = (t.m20 * vector.x + t.m21 * vector.y + t.m22 * vector.z + t.m23) * vector.w;
	}

	// Token: 0x06001677 RID: 5751 RVA: 0x0005289C File Offset: 0x00050A9C
	public static void Transform3x4(ref global::AABBox src, ref Matrix4x4 transform, out global::AABBox dst)
	{
		src.TransformedAABB3x4(ref transform, out dst);
	}

	// Token: 0x06001678 RID: 5752 RVA: 0x000528A8 File Offset: 0x00050AA8
	public static void Transform4x4(ref global::AABBox src, ref Matrix4x4 transform, out global::AABBox dst)
	{
		src.TransformedAABB4x4(ref transform, out dst);
	}

	// Token: 0x06001679 RID: 5753 RVA: 0x000528B4 File Offset: 0x00050AB4
	public static void Transform3x4(ref global::AABBox src, ref Matrix4x4 transform, out Bounds dst)
	{
		src.TransformedAABB3x4(ref transform, out dst);
	}

	// Token: 0x0600167A RID: 5754 RVA: 0x000528C0 File Offset: 0x00050AC0
	public static void Transform4x4(ref global::AABBox src, ref Matrix4x4 transform, out Bounds dst)
	{
		src.TransformedAABB4x4(ref transform, out dst);
	}

	// Token: 0x0600167B RID: 5755 RVA: 0x000528CC File Offset: 0x00050ACC
	public static void Transform3x4(ref Bounds boundsSrc, ref Matrix4x4 transform, out global::AABBox dst)
	{
		global::AABBox aabbox = new global::AABBox(boundsSrc.min, boundsSrc.max);
		global::AABBox.Transform3x4(ref aabbox, ref transform, out dst);
	}

	// Token: 0x0600167C RID: 5756 RVA: 0x000528F8 File Offset: 0x00050AF8
	public static void Transform4x4(ref Bounds boundsSrc, ref Matrix4x4 transform, out global::AABBox dst)
	{
		global::AABBox aabbox = new global::AABBox(boundsSrc.min, boundsSrc.max);
		global::AABBox.Transform4x4(ref aabbox, ref transform, out dst);
	}

	// Token: 0x0600167D RID: 5757 RVA: 0x00052924 File Offset: 0x00050B24
	public static void Transform3x4(ref Bounds boundsSrc, ref Matrix4x4 transform, out Bounds dst)
	{
		global::AABBox aabbox = new global::AABBox(boundsSrc.min, boundsSrc.max);
		global::AABBox.Transform3x4(ref aabbox, ref transform, out dst);
	}

	// Token: 0x0600167E RID: 5758 RVA: 0x00052950 File Offset: 0x00050B50
	public static void Transform4x4(ref Bounds boundsSrc, ref Matrix4x4 transform, out Bounds dst)
	{
		global::AABBox aabbox = new global::AABBox(boundsSrc.min, boundsSrc.max);
		global::AABBox.Transform4x4(ref aabbox, ref transform, out dst);
	}

	// Token: 0x0600167F RID: 5759 RVA: 0x0005297C File Offset: 0x00050B7C
	public static void Transform3x4(global::AABBox src, ref Matrix4x4 transform, out global::AABBox dst)
	{
		src.TransformedAABB3x4(ref transform, out dst);
	}

	// Token: 0x06001680 RID: 5760 RVA: 0x00052988 File Offset: 0x00050B88
	public static void Transform4x4(global::AABBox src, ref Matrix4x4 transform, out global::AABBox dst)
	{
		src.TransformedAABB4x4(ref transform, out dst);
	}

	// Token: 0x06001681 RID: 5761 RVA: 0x00052994 File Offset: 0x00050B94
	public static void Transform3x4(global::AABBox src, ref Matrix4x4 transform, out Bounds dst)
	{
		src.TransformedAABB3x4(ref transform, out dst);
	}

	// Token: 0x06001682 RID: 5762 RVA: 0x000529A0 File Offset: 0x00050BA0
	public static void Transform4x4(global::AABBox src, ref Matrix4x4 transform, out Bounds dst)
	{
		src.TransformedAABB4x4(ref transform, out dst);
	}

	// Token: 0x06001683 RID: 5763 RVA: 0x000529AC File Offset: 0x00050BAC
	public static void Transform3x4(Bounds boundsSrc, ref Matrix4x4 transform, out global::AABBox dst)
	{
		global::AABBox aabbox = new global::AABBox(boundsSrc.min, boundsSrc.max);
		global::AABBox.Transform3x4(ref aabbox, ref transform, out dst);
	}

	// Token: 0x06001684 RID: 5764 RVA: 0x000529D8 File Offset: 0x00050BD8
	public static void Transform4x4(Bounds boundsSrc, ref Matrix4x4 transform, out global::AABBox dst)
	{
		global::AABBox aabbox = new global::AABBox(boundsSrc.min, boundsSrc.max);
		global::AABBox.Transform4x4(ref aabbox, ref transform, out dst);
	}

	// Token: 0x06001685 RID: 5765 RVA: 0x00052A04 File Offset: 0x00050C04
	public static void Transform3x4(Bounds boundsSrc, ref Matrix4x4 transform, out Bounds dst)
	{
		global::AABBox aabbox = new global::AABBox(boundsSrc.min, boundsSrc.max);
		global::AABBox.Transform3x4(ref aabbox, ref transform, out dst);
	}

	// Token: 0x06001686 RID: 5766 RVA: 0x00052A30 File Offset: 0x00050C30
	public static void Transform4x4(Bounds boundsSrc, ref Matrix4x4 transform, out Bounds dst)
	{
		global::AABBox aabbox = new global::AABBox(boundsSrc.min, boundsSrc.max);
		global::AABBox.Transform4x4(ref aabbox, ref transform, out dst);
	}

	// Token: 0x06001687 RID: 5767 RVA: 0x00052A5C File Offset: 0x00050C5C
	public static global::AABBox CenterAndSize(Vector3 center, Vector3 size)
	{
		center.x -= size.x * 0.5f;
		center.y -= size.y * 0.5f;
		center.z -= size.z * 0.5f;
		size.x = center.x + size.x;
		size.y = center.y + size.y;
		size.z = center.z + size.z;
		return new global::AABBox(ref center, ref size);
	}

	// Token: 0x06001688 RID: 5768 RVA: 0x00052B08 File Offset: 0x00050D08
	public override bool Equals(object obj)
	{
		return obj is global::AABBox && this.Equals((global::AABBox)obj);
	}

	// Token: 0x06001689 RID: 5769 RVA: 0x00052B24 File Offset: 0x00050D24
	public override int GetHashCode()
	{
		int num = ((this.m.x + this.M.x) * 0.5f).GetHashCode() ^ ((this.m.y + this.M.y) * 0.5f).GetHashCode();
		int num2 = ((this.m.x + this.M.x - (this.m.y + this.M.y)).GetHashCode() & int.MaxValue) % 32;
		return num << num2 ^ num >> num2;
	}

	// Token: 0x0600168A RID: 5770 RVA: 0x00052BCC File Offset: 0x00050DCC
	public bool Equals(global::AABBox other)
	{
		return this.m.x.Equals(other.m.x) && this.m.y.Equals(other.m.y) && this.m.z.Equals(other.m.z) && this.M.x.Equals(other.M.x) && this.M.y.Equals(other.M.y) && this.M.z.Equals(other.M.z);
	}

	// Token: 0x0600168B RID: 5771 RVA: 0x00052CA0 File Offset: 0x00050EA0
	public bool Equals(ref global::AABBox other)
	{
		return this.m.x.Equals(other.m.x) && this.m.y.Equals(other.m.y) && this.m.z.Equals(other.m.z) && this.M.x.Equals(other.M.x) && this.M.y.Equals(other.M.y) && this.M.z.Equals(other.M.z);
	}

	// Token: 0x0600168C RID: 5772 RVA: 0x00052D6C File Offset: 0x00050F6C
	public static explicit operator Bounds(global::AABBox mM)
	{
		Vector3 vector;
		vector.x = mM.M.x - mM.m.x;
		Vector3 vector2;
		vector2.x = mM.m.x + vector.x * 0.5f;
		if (vector.x < 0f)
		{
			vector.x = -vector.x;
		}
		vector.y = mM.M.y - mM.m.y;
		vector2.y = mM.m.y + vector.y * 0.5f;
		if (vector.y < 0f)
		{
			vector.y = -vector.y;
		}
		vector.z = mM.M.z - mM.m.z;
		vector2.z = mM.m.z + vector.z * 0.5f;
		if (vector.z < 0f)
		{
			vector.z = -vector.z;
		}
		return new Bounds(vector2, vector);
	}

	// Token: 0x0600168D RID: 5773 RVA: 0x00052EA4 File Offset: 0x000510A4
	public static explicit operator global::AABBox(Bounds bounds)
	{
		Vector3 min = bounds.min;
		Vector3 max = bounds.max;
		global::AABBox result;
		if (min.x > max.x)
		{
			result.M.x = min.x;
			result.m.x = max.x;
		}
		else
		{
			result.M.x = max.x;
			result.m.x = min.x;
		}
		if (min.y > max.y)
		{
			result.M.y = min.y;
			result.m.y = max.y;
		}
		else
		{
			result.M.y = max.y;
			result.m.y = min.y;
		}
		if (min.z > max.z)
		{
			result.M.z = min.z;
			result.m.z = max.z;
		}
		else
		{
			result.M.z = max.z;
			result.m.z = min.z;
		}
		return result;
	}

	// Token: 0x0600168E RID: 5774 RVA: 0x00052FF0 File Offset: 0x000511F0
	public static explicit operator global::BBox(global::AABBox mM)
	{
		global::BBox result;
		result.a.x = mM.m.x;
		result.a.y = mM.m.y;
		result.a.z = mM.m.z;
		result.b.x = mM.m.x;
		result.b.y = mM.m.y;
		result.b.z = mM.M.z;
		result.c.x = mM.M.x;
		result.c.y = mM.m.y;
		result.c.z = mM.m.z;
		result.d.x = mM.M.x;
		result.d.y = mM.m.y;
		result.d.z = mM.M.z;
		result.e.x = mM.m.x;
		result.e.y = mM.M.y;
		result.e.z = mM.m.z;
		result.f.x = mM.m.x;
		result.f.y = mM.M.y;
		result.f.z = mM.M.z;
		result.g.x = mM.M.x;
		result.g.y = mM.M.y;
		result.g.z = mM.m.z;
		result.h.x = mM.M.x;
		result.h.y = mM.M.y;
		result.h.z = mM.M.z;
		return result;
	}

	// Token: 0x0600168F RID: 5775 RVA: 0x00053240 File Offset: 0x00051440
	public static explicit operator global::AABBox(global::BBox box)
	{
		global::AABBox result;
		result.m.x = (result.M.x = box.a.x);
		result.m.y = (result.M.y = box.a.y);
		result.m.z = (result.M.z = box.a.z);
		if (box.b.x < result.m.x)
		{
			result.m.x = box.b.x;
		}
		if (box.b.x > result.M.x)
		{
			result.M.x = box.b.x;
		}
		if (box.b.y < result.m.y)
		{
			result.m.y = box.b.y;
		}
		if (box.b.y > result.M.y)
		{
			result.M.y = box.b.y;
		}
		if (box.b.z < result.m.z)
		{
			result.m.z = box.b.z;
		}
		if (box.b.z > result.M.z)
		{
			result.M.z = box.b.z;
		}
		if (box.c.x < result.m.x)
		{
			result.m.x = box.c.x;
		}
		if (box.c.x > result.M.x)
		{
			result.M.x = box.c.x;
		}
		if (box.c.y < result.m.y)
		{
			result.m.y = box.c.y;
		}
		if (box.c.y > result.M.y)
		{
			result.M.y = box.c.y;
		}
		if (box.c.z < result.m.z)
		{
			result.m.z = box.c.z;
		}
		if (box.c.z > result.M.z)
		{
			result.M.z = box.c.z;
		}
		if (box.d.x < result.m.x)
		{
			result.m.x = box.d.x;
		}
		if (box.d.x > result.M.x)
		{
			result.M.x = box.d.x;
		}
		if (box.d.y < result.m.y)
		{
			result.m.y = box.d.y;
		}
		if (box.d.y > result.M.y)
		{
			result.M.y = box.d.y;
		}
		if (box.d.z < result.m.z)
		{
			result.m.z = box.d.z;
		}
		if (box.d.z > result.M.z)
		{
			result.M.z = box.d.z;
		}
		if (box.e.x < result.m.x)
		{
			result.m.x = box.e.x;
		}
		if (box.e.x > result.M.x)
		{
			result.M.x = box.e.x;
		}
		if (box.e.y < result.m.y)
		{
			result.m.y = box.e.y;
		}
		if (box.e.y > result.M.y)
		{
			result.M.y = box.e.y;
		}
		if (box.e.z < result.m.z)
		{
			result.m.z = box.e.z;
		}
		if (box.e.z > result.M.z)
		{
			result.M.z = box.e.z;
		}
		if (box.f.x < result.m.x)
		{
			result.m.x = box.f.x;
		}
		if (box.f.x > result.M.x)
		{
			result.M.x = box.f.x;
		}
		if (box.f.y < result.m.y)
		{
			result.m.y = box.f.y;
		}
		if (box.f.y > result.M.y)
		{
			result.M.y = box.f.y;
		}
		if (box.f.z < result.m.z)
		{
			result.m.z = box.f.z;
		}
		if (box.f.z > result.M.z)
		{
			result.M.z = box.f.z;
		}
		if (box.g.x < result.m.x)
		{
			result.m.x = box.g.x;
		}
		if (box.g.x > result.M.x)
		{
			result.M.x = box.g.x;
		}
		if (box.g.y < result.m.y)
		{
			result.m.y = box.g.y;
		}
		if (box.g.y > result.M.y)
		{
			result.M.y = box.g.y;
		}
		if (box.g.z < result.m.z)
		{
			result.m.z = box.g.z;
		}
		if (box.g.z > result.M.z)
		{
			result.M.z = box.g.z;
		}
		if (box.h.x < result.m.x)
		{
			result.m.x = box.h.x;
		}
		if (box.h.x > result.M.x)
		{
			result.M.x = box.h.x;
		}
		if (box.h.y < result.m.y)
		{
			result.m.y = box.h.y;
		}
		if (box.h.y > result.M.y)
		{
			result.M.y = box.h.y;
		}
		if (box.h.z < result.m.z)
		{
			result.m.z = box.h.z;
		}
		if (box.h.z > result.M.z)
		{
			result.M.z = box.h.z;
		}
		return result;
	}

	// Token: 0x04000B9D RID: 2973
	public const int kX = 2;

	// Token: 0x04000B9E RID: 2974
	public const int kY = 4;

	// Token: 0x04000B9F RID: 2975
	public const int kZ = 1;

	// Token: 0x04000BA0 RID: 2976
	public const int kA = 0;

	// Token: 0x04000BA1 RID: 2977
	public const int kB = 1;

	// Token: 0x04000BA2 RID: 2978
	public const int kC = 2;

	// Token: 0x04000BA3 RID: 2979
	public const int kD = 3;

	// Token: 0x04000BA4 RID: 2980
	public const int kE = 4;

	// Token: 0x04000BA5 RID: 2981
	public const int kF = 5;

	// Token: 0x04000BA6 RID: 2982
	public const int kG = 6;

	// Token: 0x04000BA7 RID: 2983
	public const int kH = 7;

	// Token: 0x04000BA8 RID: 2984
	public Vector3 m;

	// Token: 0x04000BA9 RID: 2985
	public Vector3 M;
}
