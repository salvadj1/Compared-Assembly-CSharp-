using System;
using UnityEngine;

// Token: 0x02000761 RID: 1889
[AddComponentMenu("Water/Mesher")]
public class WaterMesher : MonoBehaviour
{
	// Token: 0x17000BCD RID: 3021
	// (get) Token: 0x06003E79 RID: 15993 RVA: 0x000E3C84 File Offset: 0x000E1E84
	// (set) Token: 0x06003E7A RID: 15994 RVA: 0x000E3CCC File Offset: 0x000E1ECC
	public Vector2 position
	{
		get
		{
			Vector3 position = base.transform.position;
			position.y = position.z;
			position.z = 0f;
			return new Vector2(position.x, position.y);
		}
		set
		{
			Vector3 position = base.transform.position;
			position.x = value.x;
			position.z = value.y;
			base.transform.position = position;
		}
	}

	// Token: 0x17000BCE RID: 3022
	// (get) Token: 0x06003E7B RID: 15995 RVA: 0x000E3D10 File Offset: 0x000E1F10
	public Vector3 position3
	{
		get
		{
			return base.transform.position;
		}
	}

	// Token: 0x06003E7C RID: 15996 RVA: 0x000E3D20 File Offset: 0x000E1F20
	public Vector2 Point(float t, Vector2 p3)
	{
		Vector2 position = this.position;
		Vector2 vector = position + this.inTangent;
		Vector2 vector2 = p3 + this.outTangent;
		float num = 1f - t;
		position.x = position.x * num + vector.x * t;
		position.y = position.y * num + vector.y * t;
		vector.x = vector.x * num + vector2.x * t;
		vector.y = vector.y * num + vector2.y * t;
		vector2.x = vector2.x * num + p3.x * t;
		vector2.y = vector2.y * num + p3.y * t;
		position.x = position.x * num + vector.x * t;
		position.y = position.y * num + vector.y * t;
		vector.x = vector.x * num + vector2.x * t;
		vector.y = vector.y * num + vector2.y * t;
		position.x = position.x * num + vector.x * t;
		position.y = position.y * num + vector.y * t;
		return position;
	}

	// Token: 0x17000BCF RID: 3023
	// (get) Token: 0x06003E7D RID: 15997 RVA: 0x000E3E90 File Offset: 0x000E2090
	public Vector2 smoothInTangent
	{
		get
		{
			return (!this.prev) ? this.inTangent : ((this.inTangent - this.prev.outTangent) / 2f);
		}
	}

	// Token: 0x17000BD0 RID: 3024
	// (get) Token: 0x06003E7E RID: 15998 RVA: 0x000E3ED0 File Offset: 0x000E20D0
	public Vector2 smoothOutTangent
	{
		get
		{
			return (!this.next) ? this.inTangent : ((this.outTangent - this.next.inTangent) / 2f);
		}
	}

	// Token: 0x06003E7F RID: 15999 RVA: 0x000E3F10 File Offset: 0x000E2110
	public Vector2 SmoothPoint(float t, Vector2 p3)
	{
		Vector2 position = this.position;
		Vector2 vector = position + this.smoothInTangent;
		Vector2 vector2 = p3 + this.smoothOutTangent;
		float num = 1f - t;
		position.x = position.x * num + vector.x * t;
		position.y = position.y * num + vector.y * t;
		vector.x = vector.x * num + vector2.x * t;
		vector.y = vector.y * num + vector2.y * t;
		vector2.x = vector2.x * num + p3.x * t;
		vector2.y = vector2.y * num + p3.y * t;
		position.x = position.x * num + vector.x * t;
		position.y = position.y * num + vector.y * t;
		vector.x = vector.x * num + vector2.x * t;
		vector.y = vector.y * num + vector2.y * t;
		position.x = position.x * num + vector.x * t;
		position.y = position.y * num + vector.y * t;
		return position;
	}

	// Token: 0x06003E80 RID: 16000 RVA: 0x000E4080 File Offset: 0x000E2280
	public Vector3 Point3(float t, Vector2 p3)
	{
		Vector2 vector = this.Point(t, p3);
		Vector3 result;
		result.x = vector.x;
		result.y = base.transform.position.y;
		result.z = vector.y;
		return result;
	}

	// Token: 0x040020E8 RID: 8424
	private const int kPoints = 16;

	// Token: 0x040020E9 RID: 8425
	public global::WaterMesher next;

	// Token: 0x040020EA RID: 8426
	public global::WaterMesher prev;

	// Token: 0x040020EB RID: 8427
	public Vector2 inTangent;

	// Token: 0x040020EC RID: 8428
	public Vector2 outTangent;

	// Token: 0x040020ED RID: 8429
	public bool isRoot;
}
