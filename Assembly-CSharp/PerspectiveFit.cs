using System;
using UnityEngine;

// Token: 0x02000708 RID: 1800
[RequireComponent(typeof(Camera))]
public class PerspectiveFit : MonoBehaviour
{
	// Token: 0x06003BF4 RID: 15348 RVA: 0x000D5E98 File Offset: 0x000D4098
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

	// Token: 0x06003BF5 RID: 15349 RVA: 0x000D5F84 File Offset: 0x000D4184
	private void Reset()
	{
		if (!this.camera)
		{
			this.camera = base.camera;
		}
	}

	// Token: 0x04001E16 RID: 7702
	[PrefetchComponent]
	public Camera camera;

	// Token: 0x04001E17 RID: 7703
	public float targetDistance = 2.2f;

	// Token: 0x04001E18 RID: 7704
	public Vector2 targetSize = new Vector2(2.4f, 1.1f);
}
