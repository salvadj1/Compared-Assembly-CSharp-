using System;
using UnityEngine;

// Token: 0x020004BB RID: 1211
public class HUDHitIndicator : HUDIndicator
{
	// Token: 0x06002A44 RID: 10820 RVA: 0x000A5EF8 File Offset: 0x000A40F8
	private void Awake()
	{
		this.startTime = NetCull.time;
		this.material = this.texture.material.Clone();
		this.texture.material = this.material;
	}

	// Token: 0x06002A45 RID: 10821 RVA: 0x000A5F38 File Offset: 0x000A4138
	protected new void OnDestroy()
	{
		base.OnDestroy();
		if (this.material)
		{
			Object.Destroy(this.material);
			this.material = null;
		}
	}

	// Token: 0x06002A46 RID: 10822 RVA: 0x000A5F70 File Offset: 0x000A4170
	protected override bool Continue()
	{
		float num = (float)(HUDIndicator.stepTime - this.startTime);
		if (num > this.curve[this.curve.length - 1].time)
		{
			return false;
		}
		this.material.Set("_AlphaValue", Mathf.Clamp(this.curve.Evaluate(num), 0.003921569f, 0.996078432f));
		if (this.followPoint)
		{
			Vector3 position = base.transform.position;
			Vector3 vector = base.GetPoint(HUDIndicator.PlacementSpace.World, this.worldPosition);
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

	// Token: 0x06002A47 RID: 10823 RVA: 0x000A60A8 File Offset: 0x000A42A8
	public static void CreateIndicator(Vector3 worldPoint, bool followPoint, HUDHitIndicator prefab)
	{
		HUDHitIndicator hudhitIndicator = (HUDHitIndicator)HUDIndicator.InstantiateIndicator(HUDIndicator.ScratchTarget.CenteredAuto, prefab, HUDIndicator.PlacementSpace.World, worldPoint);
		hudhitIndicator.worldPosition = worldPoint;
		hudhitIndicator.followPoint = followPoint;
	}

	// Token: 0x0400165C RID: 5724
	private const float kMIN = 0.003921569f;

	// Token: 0x0400165D RID: 5725
	private const float kMAX = 0.996078432f;

	// Token: 0x0400165E RID: 5726
	public UITexture texture;

	// Token: 0x0400165F RID: 5727
	public AnimationCurve curve;

	// Token: 0x04001660 RID: 5728
	private UIMaterial material;

	// Token: 0x04001661 RID: 5729
	private double startTime;

	// Token: 0x04001662 RID: 5730
	private Vector3 worldPosition;

	// Token: 0x04001663 RID: 5731
	private bool followPoint;
}
