using System;
using UnityEngine;

// Token: 0x0200069B RID: 1691
[AddComponentMenu("Water/Mesher")]
public class WaterMesher : MonoBehaviour
{
	// Token: 0x17000B49 RID: 2889
	// (get) Token: 0x06003A7B RID: 14971 RVA: 0x000DB1F4 File Offset: 0x000D93F4
	// (set) Token: 0x06003A7C RID: 14972 RVA: 0x000DB23C File Offset: 0x000D943C
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

	// Token: 0x17000B4A RID: 2890
	// (get) Token: 0x06003A7D RID: 14973 RVA: 0x000DB280 File Offset: 0x000D9480
	public Vector3 position3
	{
		get
		{
			return base.transform.position;
		}
	}

	// Token: 0x06003A7E RID: 14974 RVA: 0x000DB290 File Offset: 0x000D9490
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

	// Token: 0x17000B4B RID: 2891
	// (get) Token: 0x06003A7F RID: 14975 RVA: 0x000DB400 File Offset: 0x000D9600
	public Vector2 smoothInTangent
	{
		get
		{
			return (!this.prev) ? this.inTangent : ((this.inTangent - this.prev.outTangent) / 2f);
		}
	}

	// Token: 0x17000B4C RID: 2892
	// (get) Token: 0x06003A80 RID: 14976 RVA: 0x000DB440 File Offset: 0x000D9640
	public Vector2 smoothOutTangent
	{
		get
		{
			return (!this.next) ? this.inTangent : ((this.outTangent - this.next.inTangent) / 2f);
		}
	}

	// Token: 0x06003A81 RID: 14977 RVA: 0x000DB480 File Offset: 0x000D9680
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

	// Token: 0x06003A82 RID: 14978 RVA: 0x000DB5F0 File Offset: 0x000D97F0
	public Vector3 Point3(float t, Vector2 p3)
	{
		Vector2 vector = this.Point(t, p3);
		Vector3 result;
		result.x = vector.x;
		result.y = base.transform.position.y;
		result.z = vector.y;
		return result;
	}

	// Token: 0x04001EEC RID: 7916
	private const int kPoints = 16;

	// Token: 0x04001EED RID: 7917
	public WaterMesher next;

	// Token: 0x04001EEE RID: 7918
	public WaterMesher prev;

	// Token: 0x04001EEF RID: 7919
	public Vector2 inTangent;

	// Token: 0x04001EF0 RID: 7920
	public Vector2 outTangent;

	// Token: 0x04001EF1 RID: 7921
	public bool isRoot;
}
