using System;
using UnityEngine;

// Token: 0x020004BA RID: 1210
public class HUDDirectionalDamage : HUDIndicator
{
	// Token: 0x06002A3E RID: 10814 RVA: 0x000A584C File Offset: 0x000A3A4C
	private void Awake()
	{
		this.lastBoundMin = this.skeletonMaterial.GetVector("_MinChannels");
		this.lastBoundMax = this.skeletonMaterial.GetVector("_MaxChannels");
	}

	// Token: 0x06002A3F RID: 10815 RVA: 0x000A5888 File Offset: 0x000A3A88
	protected new void OnDestroy()
	{
		base.OnDestroy();
	}

	// Token: 0x06002A40 RID: 10816 RVA: 0x000A5890 File Offset: 0x000A3A90
	protected new void Start()
	{
		this.randMin.x = Random.Range(0f, 0.06f);
		this.randMin.y = Random.Range(0f, 0.06f);
		this.randMin.z = Random.Range(0f, 0.06f);
		this.randMin.w = Random.Range(0f, 0.06f);
		this.randMax.x = Random.Range(0.94f, 1f);
		this.randMax.y = Random.Range(0.94f, 1f);
		this.randMax.z = Random.Range(0.94f, 1f);
		this.randMax.w = Random.Range(0.94f, 1f);
		int num = Random.Range(0, 64);
		this.swapX = ((num & 1) == 1);
		this.inverseX = ((num & 8) == 8);
		this.swapY = ((num & 2) == 2);
		this.inverseY = ((num & 16) == 16);
		this.swapZ = ((num & 4) == 4);
		this.inverseZ = ((num & 32) == 32);
		this.speedModX = 1.12 - (1.0 - ((double)this.randMax.x - (double)this.randMin.x));
		this.speedModY = 1.12 - (1.0 - ((double)this.randMax.y - (double)this.randMin.y));
		this.speedModZ = 1.12 - (1.0 - ((double)this.randMax.z - (double)this.randMin.z));
		this.speedModW = 1.12 - (1.0 - ((double)this.randMax.w - (double)this.randMin.w));
		this.speedModX /= this.speedModW;
		this.speedModY /= this.speedModW;
		this.speedModZ /= this.speedModW;
		this.speedModW = 1.0;
		base.Start();
	}

	// Token: 0x06002A41 RID: 10817 RVA: 0x000A5ADC File Offset: 0x000A3CDC
	protected sealed override bool Continue()
	{
		if (this.duration <= 0.0)
		{
			return false;
		}
		double num = HUDIndicator.stepTime - this.damageTime;
		if (num > this.duration)
		{
			return false;
		}
		this.propBlock.Clear();
		double num2 = num / this.duration;
		double num3 = num2 * this.speedModX;
		double num4 = num2 * this.speedModY;
		double num5 = num2 * this.speedModZ;
		if (num3 > 1.0)
		{
			num3 = 1.0 - (num3 - 1.0);
		}
		if (num4 > 1.0)
		{
			num4 = 1.0 - (num4 - 1.0);
		}
		if (num5 > 1.0)
		{
			num5 = 1.0 - (num5 - 1.0);
		}
		double num6 = TransitionFunctions.Spline(num3, 1.0, 0.0);
		double num7 = (num4 >= 0.10000000149011612) ? TransitionFunctions.Spline((num4 - 0.1) / 0.9, 1.0, 0.0) : TransitionFunctions.Spline(num4 / 0.1, 0.0, 1.0);
		double num8 = Math.Cos(num5 * 1.5707963267948966);
		Vector4 vector;
		vector.x = (float)num8;
		vector.y = (float)num6;
		vector.z = (float)num7;
		vector.w = (float)num2;
		Vector4 vector2;
		vector2.x = (float)(num8 / this.speedModX);
		vector2.y = (float)(num6 / this.speedModY);
		vector2.z = (float)(num7 / this.speedModZ);
		vector2.w = (float)(1.0 - num2);
		if (this.inverseX)
		{
			vector2.x = 1f - vector2.x;
		}
		if (this.inverseY)
		{
			vector2.y = 1f - vector2.y;
		}
		if (this.inverseZ)
		{
			vector2.z = 1f - vector2.z;
		}
		if (this.swapX)
		{
			float num9 = vector2.x;
			vector2.x = vector.x;
			vector.x = num9;
		}
		if (this.swapY)
		{
			float num9 = vector2.y;
			vector2.y = vector.y;
			vector.y = num9;
		}
		if (this.swapZ)
		{
			float num9 = vector2.z;
			vector2.z = vector.z;
			vector.z = num9;
		}
		if (vector != this.lastBoundMin)
		{
			this.lastBoundMin = vector;
			this.propBlock.Set("_MinChannels", this.lastBoundMin);
		}
		if (vector2 != this.lastBoundMax)
		{
			this.lastBoundMax = vector2;
			this.propBlock.Set("_MaxChannels", this.lastBoundMax);
		}
		Vector3 vector3 = HUDIndicator.worldToCameraLocalMatrix.MultiplyVector(this.worldDirection);
		vector3.Normalize();
		if (vector3.y * vector3.y <= 0.99f)
		{
			base.transform.localEulerAngles = new Vector3(0f, 0f, Mathf.Atan2(vector3.z, vector3.x) * 57.29578f);
		}
		this.panel.propertyBlock = this.propBlock;
		return true;
	}

	// Token: 0x06002A42 RID: 10818 RVA: 0x000A5E70 File Offset: 0x000A4070
	public static void CreateIndicator(Vector3 worldDamageDirection, double damageAmount, double timestamp, double duration, HUDDirectionalDamage prefab)
	{
		HUDDirectionalDamage huddirectionalDamage = (HUDDirectionalDamage)HUDIndicator.InstantiateIndicator(HUDIndicator.ScratchTarget.CenteredFixed3000Tall, prefab, HUDIndicator.PlacementSpace.DoNotModify, Vector3.zero);
		huddirectionalDamage.damageTime = timestamp;
		huddirectionalDamage.duration = duration;
		huddirectionalDamage.damageAmount = damageAmount;
		huddirectionalDamage.worldDirection = -worldDamageDirection;
		huddirectionalDamage.transform.localPosition = Vector3.zero;
		huddirectionalDamage.transform.localRotation = Quaternion.identity;
		huddirectionalDamage.transform.localScale = Vector3.one;
		huddirectionalDamage.texture.ForceReloadMaterial();
	}

	// Token: 0x04001644 RID: 5700
	private const string materialProp_MIN = "_MinChannels";

	// Token: 0x04001645 RID: 5701
	private const string materialProp_MAX = "_MaxChannels";

	// Token: 0x04001646 RID: 5702
	[SerializeField]
	private Material skeletonMaterial;

	// Token: 0x04001647 RID: 5703
	private Vector4 lastBoundMin;

	// Token: 0x04001648 RID: 5704
	private Vector4 lastBoundMax;

	// Token: 0x04001649 RID: 5705
	[NonSerialized]
	public Vector3 worldDirection = Vector3.left;

	// Token: 0x0400164A RID: 5706
	[NonSerialized]
	public double damageTime;

	// Token: 0x0400164B RID: 5707
	[NonSerialized]
	public double duration;

	// Token: 0x0400164C RID: 5708
	[NonSerialized]
	public double damageAmount;

	// Token: 0x0400164D RID: 5709
	private Vector4 randMin;

	// Token: 0x0400164E RID: 5710
	private Vector4 randMax;

	// Token: 0x0400164F RID: 5711
	private double speedModX;

	// Token: 0x04001650 RID: 5712
	private double speedModY;

	// Token: 0x04001651 RID: 5713
	private double speedModZ;

	// Token: 0x04001652 RID: 5714
	private double speedModW;

	// Token: 0x04001653 RID: 5715
	private bool swapX;

	// Token: 0x04001654 RID: 5716
	private bool swapY;

	// Token: 0x04001655 RID: 5717
	private bool swapZ;

	// Token: 0x04001656 RID: 5718
	private bool inverseX;

	// Token: 0x04001657 RID: 5719
	private bool inverseY;

	// Token: 0x04001658 RID: 5720
	private bool inverseZ;

	// Token: 0x04001659 RID: 5721
	[SerializeField]
	private UIPanel panel;

	// Token: 0x0400165A RID: 5722
	[SerializeField]
	private UITexture texture;

	// Token: 0x0400165B RID: 5723
	private UIPanelMaterialPropertyBlock propBlock = new UIPanelMaterialPropertyBlock();
}
