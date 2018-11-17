using System;
using UnityEngine;

// Token: 0x02000645 RID: 1605
[RequireComponent(typeof(Camera))]
public class PerspectiveFit : MonoBehaviour
{
	// Token: 0x06003808 RID: 14344 RVA: 0x000CD5E8 File Offset: 0x000CB7E8
	private void OnPreCull()
	{
		if (base.enabled && this.camera && this.camera.enabled)
		{
			float aspect = this.camera.aspect;
			float num = this.targetSize.x / this.targetSize.y;
			float num2 = Vector2.Angle(new Vector2(this.targetSize.x / aspect * 0.5f, this.targetDistance), new Vector2(0f, this.targetDistance)) * 2f;
			float num3 = Vector2.Angle(new Vector2(this.targetSize.y * 0.5f, this.targetDistance), new Vector2(0f, this.targetDistance)) * 2f;
			float fieldOfView;
			if (num < aspect)
			{
				fieldOfView = num3;
			}
			else
			{
				fieldOfView = num2;
			}
			this.camera.fieldOfView = fieldOfView;
		}
	}

	// Token: 0x06003809 RID: 14345 RVA: 0x000CD6D4 File Offset: 0x000CB8D4
	private void Reset()
	{
		if (!this.camera)
		{
			this.camera = base.camera;
		}
	}

	// Token: 0x04001C21 RID: 7201
	[PrefetchComponent]
	public Camera camera;

	// Token: 0x04001C22 RID: 7202
	public float targetDistance = 2.2f;

	// Token: 0x04001C23 RID: 7203
	public Vector2 targetSize = new Vector2(2.4f, 1.1f);
}
