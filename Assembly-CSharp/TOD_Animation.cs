using System;
using UnityEngine;

// Token: 0x02000827 RID: 2087
public class TOD_Animation : MonoBehaviour
{
	// Token: 0x17000E8A RID: 3722
	// (get) Token: 0x06004A57 RID: 19031 RVA: 0x00142388 File Offset: 0x00140588
	// (set) Token: 0x06004A58 RID: 19032 RVA: 0x00142390 File Offset: 0x00140590
	internal Vector4 CloudUV { get; set; }

	// Token: 0x17000E8B RID: 3723
	// (get) Token: 0x06004A59 RID: 19033 RVA: 0x0014239C File Offset: 0x0014059C
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

	// Token: 0x06004A5A RID: 19034 RVA: 0x00142420 File Offset: 0x00140620
	protected void Start()
	{
		this.sky = base.GetComponent<TOD_Sky>();
	}

	// Token: 0x06004A5B RID: 19035 RVA: 0x00142430 File Offset: 0x00140630
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

	// Token: 0x04002AEB RID: 10987
	public float WindDegrees;

	// Token: 0x04002AEC RID: 10988
	public float WindSpeed = 3f;

	// Token: 0x04002AED RID: 10989
	private TOD_Sky sky;
}
