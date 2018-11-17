using System;
using UnityEngine;

// Token: 0x0200091C RID: 2332
public class TOD_Animation : MonoBehaviour
{
	// Token: 0x17000F24 RID: 3876
	// (get) Token: 0x06004F12 RID: 20242 RVA: 0x0014C2EC File Offset: 0x0014A4EC
	// (set) Token: 0x06004F13 RID: 20243 RVA: 0x0014C2F4 File Offset: 0x0014A4F4
	internal Vector4 CloudUV { get; set; }

	// Token: 0x17000F25 RID: 3877
	// (get) Token: 0x06004F14 RID: 20244 RVA: 0x0014C300 File Offset: 0x0014A500
	internal Vector4 OffsetUV
	{
		get
		{
			Vector3 position = base.transform.position;
			Vector3 lossyScale = base.transform.lossyScale;
			Vector3 vector;
			vector..ctor(position.x / lossyScale.x, 0f, position.z / lossyScale.z);
			vector = -base.transform.TransformDirection(vector);
			return new Vector4(vector.x, vector.z, vector.x, vector.z);
		}
	}

	// Token: 0x06004F15 RID: 20245 RVA: 0x0014C384 File Offset: 0x0014A584
	protected void Start()
	{
		this.sky = base.GetComponent<global::TOD_Sky>();
	}

	// Token: 0x06004F16 RID: 20246 RVA: 0x0014C394 File Offset: 0x0014A594
	protected void Update()
	{
		Vector2 vector;
		vector..ctor(Mathf.Cos(0.0174532924f * (this.WindDegrees + 15f)), Mathf.Sin(0.0174532924f * (this.WindDegrees + 15f)));
		Vector2 vector2;
		vector2..ctor(Mathf.Cos(0.0174532924f * (this.WindDegrees - 15f)), Mathf.Sin(0.0174532924f * (this.WindDegrees - 15f)));
		Vector4 vector3 = this.WindSpeed / 100f * new Vector4(vector.x, vector.y, vector2.x, vector2.y);
		this.CloudUV += Time.deltaTime * vector3;
		this.CloudUV = new Vector4(this.CloudUV.x % this.sky.Clouds.Scale1.x, this.CloudUV.y % this.sky.Clouds.Scale1.y, this.CloudUV.z % this.sky.Clouds.Scale2.x, this.CloudUV.w % this.sky.Clouds.Scale2.y);
	}

	// Token: 0x04002D39 RID: 11577
	public float WindDegrees;

	// Token: 0x04002D3A RID: 11578
	public float WindSpeed = 3f;

	// Token: 0x04002D3B RID: 11579
	private global::TOD_Sky sky;
}
