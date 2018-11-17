using System;
using UnityEngine;

// Token: 0x0200023A RID: 570
public struct AABBox : IEquatable<AABBox>
{
	// Token: 0x060014D6 RID: 5334 RVA: 0x00045FFC File Offset: 0x000441FC
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

	// Token: 0x060014D7 RID: 5335 RVA: 0x0004612C File Offset: 0x0004432C
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

	// Token: 0x060014D8 RID: 5336 RVA: 0x00046248 File Offset: 0x00044448
	public AABBox(ref Vector3 center)
	{
		this.m.x = (this.M.x = center.x);
		this.m.y = (this.M.y = center.y);
		this.m.z = (this.M.z = center.z);
	}

	// Token: 0x060014D9 RID: 5337 RVA: 0x000462B4 File Offset: 0x000444B4
	public AABBox(Vector3 center)
	{
		this.m.x = (this.M.x = center.x);
		this.m.y = (this.M.y = center.y);
		this.m.z = (this.M.z = center.z);
	}

	// Token: 0x060014DA RID: 5338 RVA: 0x00046324 File Offset: 0x00044524
	public AABBox(Bounds bounds)
	{
		this = new AABBox(bounds.min, bounds.max);
	}

	// Token: 0x060014DB RID: 5339 RVA: 0x0004633C File Offset: 0x0004453C
	public AABBox(ref Bounds bounds)
	{
		this = new AABBox(bounds.min, bounds.max);
	}

	// Token: 0x17000603 RID: 1539
	// (get) Token: 0x060014DC RID: 5340 RVA: 0x00046350 File Offset: 0x00044550
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

	// Token: 0x17000604 RID: 1540
	// (get) Token: 0x060014DD RID: 5341 RVA: 0x00046394 File Offset: 0x00044594
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

	// Token: 0x17000605 RID: 1541
	// (get) Token: 0x060014DE RID: 5342 RVA: 0x000463D8 File Offset: 0x000445D8
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

	// Token: 0x17000606 RID: 1542
	// (get) Token: 0x060014DF RID: 5343 RVA: 0x0004641C File Offset: 0x0004461C
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

	// Token: 0x17000607 RID: 1543
	// (get) Token: 0x060014E0 RID: 5344 RVA: 0x00046460 File Offset: 0x00044660
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

	// Token: 0x17000608 RID: 1544
	// (get) Token: 0x060014E1 RID: 5345 RVA: 0x000464A4 File Offset: 0x000446A4
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

	// Token: 0x17000609 RID: 1545
	// (get) Token: 0x060014E2 RID: 5346 RVA: 0x000464E8 File Offset: 0x000446E8
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

	// Token: 0x1700060A RID: 1546
	// (get) Token: 0x060014E3 RID: 5347 RVA: 0x0004652C File Offset: 0x0004472C
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

	// Token: 0x1700060B RID: 1547
	// (get) Token: 0x060014E4 RID: 5348 RVA: 0x00046570 File Offset: 0x00044770
	public Vector3 line00
	{
		get
		{
			return this.a;
		}
	}

	// Token: 0x1700060C RID: 1548
	// (get) Token: 0x060014E5 RID: 5349 RVA: 0x00046578 File Offset: 0x00044778
	public Vector3 line01
	{
		get
		{
			return this.b;
		}
	}

	// Token: 0x1700060D RID: 1549
	// (get) Token: 0x060014E6 RID: 5350 RVA: 0x00046580 File Offset: 0x00044780
	public Vector3 line10
	{
		get
		{
			return this.a;
		}
	}

	// Token: 0x1700060E RID: 1550
	// (get) Token: 0x060014E7 RID: 5351 RVA: 0x00046588 File Offset: 0x00044788
	public Vector3 line11
	{
		get
		{
			return this.c;
		}
	}

	// Token: 0x1700060F RID: 1551
	// (get) Token: 0x060014E8 RID: 5352 RVA: 0x00046590 File Offset: 0x00044790
	public Vector3 line20
	{
		get
		{
			return this.a;
		}
	}

	// Token: 0x17000610 RID: 1552
	// (get) Token: 0x060014E9 RID: 5353 RVA: 0x00046598 File Offset: 0x00044798
	public Vector3 line21
	{
		get
		{
			return this.e;
		}
	}

	// Token: 0x17000611 RID: 1553
	// (get) Token: 0x060014EA RID: 5354 RVA: 0x000465A0 File Offset: 0x000447A0
	public Vector3 line30
	{
		get
		{
			return this.b;
		}
	}

	// Token: 0x17000612 RID: 1554
	// (get) Token: 0x060014EB RID: 5355 RVA: 0x000465A8 File Offset: 0x000447A8
	public Vector3 line31
	{
		get
		{
			return this.d;
		}
	}

	// Token: 0x17000613 RID: 1555
	// (get) Token: 0x060014EC RID: 5356 RVA: 0x000465B0 File Offset: 0x000447B0
	public Vector3 line40
	{
		get
		{
			return this.b;
		}
	}

	// Token: 0x17000614 RID: 1556
	// (get) Token: 0x060014ED RID: 5357 RVA: 0x000465B8 File Offset: 0x000447B8
	public Vector3 line41
	{
		get
		{
			return this.f;
		}
	}

	// Token: 0x17000615 RID: 1557
	// (get) Token: 0x060014EE RID: 5358 RVA: 0x000465C0 File Offset: 0x000447C0
	public Vector3 line50
	{
		get
		{
			return this.c;
		}
	}

	// Token: 0x17000616 RID: 1558
	// (get) Token: 0x060014EF RID: 5359 RVA: 0x000465C8 File Offset: 0x000447C8
	public Vector3 line51
	{
		get
		{
			return this.d;
		}
	}

	// Token: 0x17000617 RID: 1559
	// (get) Token: 0x060014F0 RID: 5360 RVA: 0x000465D0 File Offset: 0x000447D0
	public Vector3 line60
	{
		get
		{
			return this.c;
		}
	}

	// Token: 0x17000618 RID: 1560
	// (get) Token: 0x060014F1 RID: 5361 RVA: 0x000465D8 File Offset: 0x000447D8
	public Vector3 line61
	{
		get
		{
			return this.g;
		}
	}

	// Token: 0x17000619 RID: 1561
	// (get) Token: 0x060014F2 RID: 5362 RVA: 0x000465E0 File Offset: 0x000447E0
	public Vector3 line70
	{
		get
		{
			return this.d;
		}
	}

	// Token: 0x1700061A RID: 1562
	// (get) Token: 0x060014F3 RID: 5363 RVA: 0x000465E8 File Offset: 0x000447E8
	public Vector3 line71
	{
		get
		{
			return this.h;
		}
	}

	// Token: 0x1700061B RID: 1563
	// (get) Token: 0x060014F4 RID: 5364 RVA: 0x000465F0 File Offset: 0x000447F0
	public Vector3 line80
	{
		get
		{
			return this.e;
		}
	}

	// Token: 0x1700061C RID: 1564
	// (get) Token: 0x060014F5 RID: 5365 RVA: 0x000465F8 File Offset: 0x000447F8
	public Vector3 line81
	{
		get
		{
			return this.f;
		}
	}

	// Token: 0x1700061D RID: 1565
	// (get) Token: 0x060014F6 RID: 5366 RVA: 0x00046600 File Offset: 0x00044800
	public Vector3 line90
	{
		get
		{
			return this.e;
		}
	}

	// Token: 0x1700061E RID: 1566
	// (get) Token: 0x060014F7 RID: 5367 RVA: 0x00046608 File Offset: 0x00044808
	public Vector3 line91
	{
		get
		{
			return this.g;
		}
	}

	// Token: 0x1700061F RID: 1567
	// (get) Token: 0x060014F8 RID: 5368 RVA: 0x00046610 File Offset: 0x00044810
	public Vector3 lineA0
	{
		get
		{
			return this.f;
		}
	}

	// Token: 0x17000620 RID: 1568
	// (get) Token: 0x060014F9 RID: 5369 RVA: 0x00046618 File Offset: 0x00044818
	public Vector3 lineA1
	{
		get
		{
			return this.h;
		}
	}

	// Token: 0x17000621 RID: 1569
	// (get) Token: 0x060014FA RID: 5370 RVA: 0x00046620 File Offset: 0x00044820
	public Vector3 lineB0
	{
		get
		{
			return this.g;
		}
	}

	// Token: 0x17000622 RID: 1570
	// (get) Token: 0x060014FB RID: 5371 RVA: 0x00046628 File Offset: 0x00044828
	public Vector3 lineB1
	{
		get
		{
			return this.h;
		}
	}

	// Token: 0x060014FC RID: 5372 RVA: 0x00046630 File Offset: 0x00044830
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

	// Token: 0x060014FD RID: 5373 RVA: 0x0004674C File Offset: 0x0004494C
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

	// Token: 0x060014FE RID: 5374 RVA: 0x00046870 File Offset: 0x00044A70
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

	// Token: 0x060014FF RID: 5375 RVA: 0x00046994 File Offset: 0x00044B94
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

	// Token: 0x06001500 RID: 5376 RVA: 0x00046AC4 File Offset: 0x00044CC4
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

	// Token: 0x06001501 RID: 5377 RVA: 0x00046C04 File Offset: 0x00044E04
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

	// Token: 0x06001502 RID: 5378 RVA: 0x00046D40 File Offset: 0x00044F40
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

	// Token: 0x17000623 RID: 1571
	// (get) Token: 0x06001503 RID: 5379 RVA: 0x00046E28 File Offset: 0x00045028
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

	// Token: 0x17000624 RID: 1572
	// (get) Token: 0x06001504 RID: 5380 RVA: 0x00046EF0 File Offset: 0x000450F0
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

	// Token: 0x17000625 RID: 1573
	// (get) Token: 0x06001505 RID: 5381 RVA: 0x00046FB8 File Offset: 0x000451B8
	// (set) Token: 0x06001506 RID: 5382 RVA: 0x000470C8 File Offset: 0x000452C8
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

	// Token: 0x17000626 RID: 1574
	// (get) Token: 0x06001507 RID: 5383 RVA: 0x000472BC File Offset: 0x000454BC
	// (set) Token: 0x06001508 RID: 5384 RVA: 0x0004735C File Offset: 0x0004555C
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

	// Token: 0x17000627 RID: 1575
	// (get) Token: 0x06001509 RID: 5385 RVA: 0x00047474 File Offset: 0x00045674
	public bool empty
	{
		get
		{
			return this.m.x == this.M.x && this.m.y == this.M.y && this.m.z == this.M.z;
		}
	}

	// Token: 0x17000628 RID: 1576
	// (get) Token: 0x0600150A RID: 5386 RVA: 0x000474D4 File Offset: 0x000456D4
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

	// Token: 0x17000629 RID: 1577
	// (get) Token: 0x0600150B RID: 5387 RVA: 0x00047834 File Offset: 0x00045A34
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

	// Token: 0x0600150C RID: 5388 RVA: 0x00047984 File Offset: 0x00045B84
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

	// Token: 0x0600150D RID: 5389 RVA: 0x00047A7C File Offset: 0x00045C7C
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

	// Token: 0x0600150E RID: 5390 RVA: 0x00047B80 File Offset: 0x00045D80
	public void Encapsulate(ref AABBox v)
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

	// Token: 0x0600150F RID: 5391 RVA: 0x00047E3C File Offset: 0x0004603C
	public void Encapsulate(AABBox v)
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

	// Token: 0x06001510 RID: 5392 RVA: 0x00048114 File Offset: 0x00046314
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

	// Token: 0x06001511 RID: 5393 RVA: 0x00048338 File Offset: 0x00046538
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

	// Token: 0x06001512 RID: 5394 RVA: 0x0004856C File Offset: 0x0004676C
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

	// Token: 0x06001513 RID: 5395 RVA: 0x000487A0 File Offset: 0x000469A0
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

	// Token: 0x06001514 RID: 5396 RVA: 0x000489E4 File Offset: 0x00046BE4
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

	// Token: 0x06001515 RID: 5397 RVA: 0x00048C34 File Offset: 0x00046E34
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

	// Token: 0x06001516 RID: 5398 RVA: 0x00048E88 File Offset: 0x00047088
	public bool Contains(ref Vector3 v)
	{
		return this.m.x <= this.M.x && this.m.y <= this.M.y && this.m.z <= this.M.z && v.x >= this.m.x && v.y >= this.m.y && v.z >= this.m.z && v.x <= this.M.x && v.y <= this.M.y && v.z <= this.M.z;
	}

	// Token: 0x1700062A RID: 1578
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

	// Token: 0x1700062B RID: 1579
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

	// Token: 0x06001519 RID: 5401 RVA: 0x000490AC File Offset: 0x000472AC
	public BBox ToBBox()
	{
		BBox result;
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

	// Token: 0x0600151A RID: 5402 RVA: 0x000492E4 File Offset: 0x000474E4
	public void ToBBox(out BBox box)
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

	// Token: 0x0600151B RID: 5403 RVA: 0x00049504 File Offset: 0x00047704
	public void TransformedAABB3x4(ref Matrix4x4 t, out AABBox mM)
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

	// Token: 0x0600151C RID: 5404 RVA: 0x0004A0B8 File Offset: 0x000482B8
	public void TransformedAABB3x4(ref Matrix4x4 t, out Bounds bounds)
	{
		AABBox aabbox;
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

	// Token: 0x0600151D RID: 5405 RVA: 0x0004A19C File Offset: 0x0004839C
	public void ToBoxCorners3x4(ref Matrix4x4 t, out BBox box, out AABBox mM)
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

	// Token: 0x0600151E RID: 5406 RVA: 0x0004B14C File Offset: 0x0004934C
	public void TransformedAABB4x4(ref Matrix4x4 t, out AABBox mM)
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

	// Token: 0x0600151F RID: 5407 RVA: 0x0004BFC0 File Offset: 0x0004A1C0
	public void TransformedAABB4x4(ref Matrix4x4 t, out Bounds bounds)
	{
		AABBox aabbox;
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

	// Token: 0x06001520 RID: 5408 RVA: 0x0004C0A4 File Offset: 0x0004A2A4
	public void ToBoxCorners4x4(ref Matrix4x4 t, out BBox box, out AABBox mM)
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

	// Token: 0x06001521 RID: 5409 RVA: 0x0004D314 File Offset: 0x0004B514
	public void ToBoxCorners3x4(ref Matrix4x4 t, out BBox box)
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

	// Token: 0x06001522 RID: 5410 RVA: 0x0004DAA4 File Offset: 0x0004BCA4
	public void ToBoxCorners4x4(ref Matrix4x4 t, out BBox box)
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

	// Token: 0x06001523 RID: 5411 RVA: 0x0004E4F4 File Offset: 0x0004C6F4
	public static void Transform3x4(ref AABBox src, ref Matrix4x4 transform, out AABBox dst)
	{
		src.TransformedAABB3x4(ref transform, out dst);
	}

	// Token: 0x06001524 RID: 5412 RVA: 0x0004E500 File Offset: 0x0004C700
	public static void Transform4x4(ref AABBox src, ref Matrix4x4 transform, out AABBox dst)
	{
		src.TransformedAABB4x4(ref transform, out dst);
	}

	// Token: 0x06001525 RID: 5413 RVA: 0x0004E50C File Offset: 0x0004C70C
	public static void Transform3x4(ref AABBox src, ref Matrix4x4 transform, out Bounds dst)
	{
		src.TransformedAABB3x4(ref transform, out dst);
	}

	// Token: 0x06001526 RID: 5414 RVA: 0x0004E518 File Offset: 0x0004C718
	public static void Transform4x4(ref AABBox src, ref Matrix4x4 transform, out Bounds dst)
	{
		src.TransformedAABB4x4(ref transform, out dst);
	}

	// Token: 0x06001527 RID: 5415 RVA: 0x0004E524 File Offset: 0x0004C724
	public static void Transform3x4(ref Bounds boundsSrc, ref Matrix4x4 transform, out AABBox dst)
	{
		AABBox aabbox = new AABBox(boundsSrc.min, boundsSrc.max);
		AABBox.Transform3x4(ref aabbox, ref transform, out dst);
	}

	// Token: 0x06001528 RID: 5416 RVA: 0x0004E550 File Offset: 0x0004C750
	public static void Transform4x4(ref Bounds boundsSrc, ref Matrix4x4 transform, out AABBox dst)
	{
		AABBox aabbox = new AABBox(boundsSrc.min, boundsSrc.max);
		AABBox.Transform4x4(ref aabbox, ref transform, out dst);
	}

	// Token: 0x06001529 RID: 5417 RVA: 0x0004E57C File Offset: 0x0004C77C
	public static void Transform3x4(ref Bounds boundsSrc, ref Matrix4x4 transform, out Bounds dst)
	{
		AABBox aabbox = new AABBox(boundsSrc.min, boundsSrc.max);
		AABBox.Transform3x4(ref aabbox, ref transform, out dst);
	}

	// Token: 0x0600152A RID: 5418 RVA: 0x0004E5A8 File Offset: 0x0004C7A8
	public static void Transform4x4(ref Bounds boundsSrc, ref Matrix4x4 transform, out Bounds dst)
	{
		AABBox aabbox = new AABBox(boundsSrc.min, boundsSrc.max);
		AABBox.Transform4x4(ref aabbox, ref transform, out dst);
	}

	// Token: 0x0600152B RID: 5419 RVA: 0x0004E5D4 File Offset: 0x0004C7D4
	public static void Transform3x4(AABBox src, ref Matrix4x4 transform, out AABBox dst)
	{
		src.TransformedAABB3x4(ref transform, out dst);
	}

	// Token: 0x0600152C RID: 5420 RVA: 0x0004E5E0 File Offset: 0x0004C7E0
	public static void Transform4x4(AABBox src, ref Matrix4x4 transform, out AABBox dst)
	{
		src.TransformedAABB4x4(ref transform, out dst);
	}

	// Token: 0x0600152D RID: 5421 RVA: 0x0004E5EC File Offset: 0x0004C7EC
	public static void Transform3x4(AABBox src, ref Matrix4x4 transform, out Bounds dst)
	{
		src.TransformedAABB3x4(ref transform, out dst);
	}

	// Token: 0x0600152E RID: 5422 RVA: 0x0004E5F8 File Offset: 0x0004C7F8
	public static void Transform4x4(AABBox src, ref Matrix4x4 transform, out Bounds dst)
	{
		src.TransformedAABB4x4(ref transform, out dst);
	}

	// Token: 0x0600152F RID: 5423 RVA: 0x0004E604 File Offset: 0x0004C804
	public static void Transform3x4(Bounds boundsSrc, ref Matrix4x4 transform, out AABBox dst)
	{
		AABBox aabbox = new AABBox(boundsSrc.min, boundsSrc.max);
		AABBox.Transform3x4(ref aabbox, ref transform, out dst);
	}

	// Token: 0x06001530 RID: 5424 RVA: 0x0004E630 File Offset: 0x0004C830
	public static void Transform4x4(Bounds boundsSrc, ref Matrix4x4 transform, out AABBox dst)
	{
		AABBox aabbox = new AABBox(boundsSrc.min, boundsSrc.max);
		AABBox.Transform4x4(ref aabbox, ref transform, out dst);
	}

	// Token: 0x06001531 RID: 5425 RVA: 0x0004E65C File Offset: 0x0004C85C
	public static void Transform3x4(Bounds boundsSrc, ref Matrix4x4 transform, out Bounds dst)
	{
		AABBox aabbox = new AABBox(boundsSrc.min, boundsSrc.max);
		AABBox.Transform3x4(ref aabbox, ref transform, out dst);
	}

	// Token: 0x06001532 RID: 5426 RVA: 0x0004E688 File Offset: 0x0004C888
	public static void Transform4x4(Bounds boundsSrc, ref Matrix4x4 transform, out Bounds dst)
	{
		AABBox aabbox = new AABBox(boundsSrc.min, boundsSrc.max);
		AABBox.Transform4x4(ref aabbox, ref transform, out dst);
	}

	// Token: 0x06001533 RID: 5427 RVA: 0x0004E6B4 File Offset: 0x0004C8B4
	public static AABBox CenterAndSize(Vector3 center, Vector3 size)
	{
		center.x -= size.x * 0.5f;
		center.y -= size.y * 0.5f;
		center.z -= size.z * 0.5f;
		size.x = center.x + size.x;
		size.y = center.y + size.y;
		size.z = center.z + size.z;
		return new AABBox(ref center, ref size);
	}

	// Token: 0x06001534 RID: 5428 RVA: 0x0004E760 File Offset: 0x0004C960
	public override bool Equals(object obj)
	{
		return obj is AABBox && this.Equals((AABBox)obj);
	}

	// Token: 0x06001535 RID: 5429 RVA: 0x0004E77C File Offset: 0x0004C97C
	public override int GetHashCode()
	{
		int num = ((this.m.x + this.M.x) * 0.5f).GetHashCode() ^ ((this.m.y + this.M.y) * 0.5f).GetHashCode();
		int num2 = ((this.m.x + this.M.x - (this.m.y + this.M.y)).GetHashCode() & int.MaxValue) % 32;
		return num << num2 ^ num >> num2;
	}

	// Token: 0x06001536 RID: 5430 RVA: 0x0004E824 File Offset: 0x0004CA24
	public bool Equals(AABBox other)
	{
		return this.m.x.Equals(other.m.x) && this.m.y.Equals(other.m.y) && this.m.z.Equals(other.m.z) && this.M.x.Equals(other.M.x) && this.M.y.Equals(other.M.y) && this.M.z.Equals(other.M.z);
	}

	// Token: 0x06001537 RID: 5431 RVA: 0x0004E8F8 File Offset: 0x0004CAF8
	public bool Equals(ref AABBox other)
	{
		return this.m.x.Equals(other.m.x) && this.m.y.Equals(other.m.y) && this.m.z.Equals(other.m.z) && this.M.x.Equals(other.M.x) && this.M.y.Equals(other.M.y) && this.M.z.Equals(other.M.z);
	}

	// Token: 0x06001538 RID: 5432 RVA: 0x0004E9C4 File Offset: 0x0004CBC4
	public static explicit operator Bounds(AABBox mM)
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

	// Token: 0x06001539 RID: 5433 RVA: 0x0004EAFC File Offset: 0x0004CCFC
	public static explicit operator AABBox(Bounds bounds)
	{
		Vector3 min = bounds.min;
		Vector3 max = bounds.max;
		AABBox result;
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

	// Token: 0x0600153A RID: 5434 RVA: 0x0004EC48 File Offset: 0x0004CE48
	public static explicit operator BBox(AABBox mM)
	{
		BBox result;
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

	// Token: 0x0600153B RID: 5435 RVA: 0x0004EE98 File Offset: 0x0004D098
	public static explicit operator AABBox(BBox box)
	{
		AABBox result;
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

	// Token: 0x04000A7A RID: 2682
	public const int kX = 2;

	// Token: 0x04000A7B RID: 2683
	public const int kY = 4;

	// Token: 0x04000A7C RID: 2684
	public const int kZ = 1;

	// Token: 0x04000A7D RID: 2685
	public const int kA = 0;

	// Token: 0x04000A7E RID: 2686
	public const int kB = 1;

	// Token: 0x04000A7F RID: 2687
	public const int kC = 2;

	// Token: 0x04000A80 RID: 2688
	public const int kD = 3;

	// Token: 0x04000A81 RID: 2689
	public const int kE = 4;

	// Token: 0x04000A82 RID: 2690
	public const int kF = 5;

	// Token: 0x04000A83 RID: 2691
	public const int kG = 6;

	// Token: 0x04000A84 RID: 2692
	public const int kH = 7;

	// Token: 0x04000A85 RID: 2693
	public Vector3 m;

	// Token: 0x04000A86 RID: 2694
	public Vector3 M;
}
