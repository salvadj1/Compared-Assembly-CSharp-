using System;
using UnityEngine;

// Token: 0x02000576 RID: 1398
public class HUDHitIndicator : global::HUDIndicator
{
	// Token: 0x06002DF6 RID: 11766 RVA: 0x000ADC90 File Offset: 0x000ABE90
	private void Awake()
	{
		this.startTime = global::NetCull.time;
		this.material = this.texture.material.Clone();
		this.texture.material = this.material;
	}

	// Token: 0x06002DF7 RID: 11767 RVA: 0x000ADCD0 File Offset: 0x000ABED0
	protected new void OnDestroy()
	{
		base.OnDestroy();
		if (this.material)
		{
			Object.Destroy(this.material);
			this.material = null;
		}
	}

	// Token: 0x06002DF8 RID: 11768 RVA: 0x000ADD08 File Offset: 0x000ABF08
	protected override bool Continue()
	{
		float num = (float)(global::HUDIndicator.stepTime - this.startTime);
		if (num > this.curve[this.curve.length - 1].time)
		{
			return false;
		}
		this.material.Set("_AlphaValue", Mathf.Clamp(this.curve.Evaluate(num), 0.003921569f, 0.996078432f));
		if (this.followPoint)
		{
			Vector3 position = base.transform.position;
			Vector3 vector = base.GetPoint(global::HUDIndicator.PlacementSpace.World, this.worldPosition);
			if (position.z != vector.z)
			{
				Plane plane;
				plane..ctor(-base.transform.forward, position);
				Ray ray;
				ray..ctor(vector, Vector3.forward);
				float num2;
				if (plane.Raycast(ray, ref num2))
				{
					vector = ray.GetPoint(num2);
				}
				else
				{
					ray.direction = -ray.direction;
					if (plane.Raycast(ray, ref num2))
					{
						vector = ray.GetPoint(num2);
					}
					else
					{
						vector = position;
					}
				}
			}
			if (vector != position)
			{
				base.transform.position = vector;
			}
		}
		return true;
	}

	// Token: 0x06002DF9 RID: 11769 RVA: 0x000ADE40 File Offset: 0x000AC040
	public static void CreateIndicator(Vector3 worldPoint, bool followPoint, global::HUDHitIndicator prefab)
	{
		global::HUDHitIndicator hudhitIndicator = (global::HUDHitIndicator)global::HUDIndicator.InstantiateIndicator(global::HUDIndicator.ScratchTarget.CenteredAuto, prefab, global::HUDIndicator.PlacementSpace.World, worldPoint);
		hudhitIndicator.worldPosition = worldPoint;
		hudhitIndicator.followPoint = followPoint;
	}

	// Token: 0x04001819 RID: 6169
	private const float kMIN = 0.003921569f;

	// Token: 0x0400181A RID: 6170
	private const float kMAX = 0.996078432f;

	// Token: 0x0400181B RID: 6171
	public global::UITexture texture;

	// Token: 0x0400181C RID: 6172
	public AnimationCurve curve;

	// Token: 0x0400181D RID: 6173
	private global::UIMaterial material;

	// Token: 0x0400181E RID: 6174
	private double startTime;

	// Token: 0x0400181F RID: 6175
	private Vector3 worldPosition;

	// Token: 0x04001820 RID: 6176
	private bool followPoint;
}
