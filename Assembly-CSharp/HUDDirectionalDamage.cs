using System;
using UnityEngine;

// Token: 0x02000575 RID: 1397
public class HUDDirectionalDamage : global::HUDIndicator
{
	// Token: 0x06002DF0 RID: 11760 RVA: 0x000AD5E4 File Offset: 0x000AB7E4
	private void Awake()
	{
		this.lastBoundMin = this.skeletonMaterial.GetVector("_MinChannels");
		this.lastBoundMax = this.skeletonMaterial.GetVector("_MaxChannels");
	}

	// Token: 0x06002DF1 RID: 11761 RVA: 0x000AD620 File Offset: 0x000AB820
	protected new void OnDestroy()
	{
		base.OnDestroy();
	}

	// Token: 0x06002DF2 RID: 11762 RVA: 0x000AD628 File Offset: 0x000AB828
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

	// Token: 0x06002DF3 RID: 11763 RVA: 0x000AD874 File Offset: 0x000ABA74
	protected sealed override bool Continue()
	{
		if (this.duration <= 0.0)
		{
			return false;
		}
		double num = global::HUDIndicator.stepTime - this.damageTime;
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
		double num6 = global::TransitionFunctions.Spline(num3, 1.0, 0.0);
		double num7 = (num4 >= 0.10000000149011612) ? global::TransitionFunctions.Spline((num4 - 0.1) / 0.9, 1.0, 0.0) : global::TransitionFunctions.Spline(num4 / 0.1, 0.0, 1.0);
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
		Vector3 vector3 = global::HUDIndicator.worldToCameraLocalMatrix.MultiplyVector(this.worldDirection);
		vector3.Normalize();
		if (vector3.y * vector3.y <= 0.99f)
		{
			base.transform.localEulerAngles = new Vector3(0f, 0f, Mathf.Atan2(vector3.z, vector3.x) * 57.29578f);
		}
		this.panel.propertyBlock = this.propBlock;
		return true;
	}

	// Token: 0x06002DF4 RID: 11764 RVA: 0x000ADC08 File Offset: 0x000ABE08
	public static void CreateIndicator(Vector3 worldDamageDirection, double damageAmount, double timestamp, double duration, global::HUDDirectionalDamage prefab)
	{
		global::HUDDirectionalDamage huddirectionalDamage = (global::HUDDirectionalDamage)global::HUDIndicator.InstantiateIndicator(global::HUDIndicator.ScratchTarget.CenteredFixed3000Tall, prefab, global::HUDIndicator.PlacementSpace.DoNotModify, Vector3.zero);
		huddirectionalDamage.damageTime = timestamp;
		huddirectionalDamage.duration = duration;
		huddirectionalDamage.damageAmount = damageAmount;
		huddirectionalDamage.worldDirection = -worldDamageDirection;
		huddirectionalDamage.transform.localPosition = Vector3.zero;
		huddirectionalDamage.transform.localRotation = Quaternion.identity;
		huddirectionalDamage.transform.localScale = Vector3.one;
		huddirectionalDamage.texture.ForceReloadMaterial();
	}

	// Token: 0x04001801 RID: 6145
	private const string materialProp_MIN = "_MinChannels";

	// Token: 0x04001802 RID: 6146
	private const string materialProp_MAX = "_MaxChannels";

	// Token: 0x04001803 RID: 6147
	[SerializeField]
	private Material skeletonMaterial;

	// Token: 0x04001804 RID: 6148
	private Vector4 lastBoundMin;

	// Token: 0x04001805 RID: 6149
	private Vector4 lastBoundMax;

	// Token: 0x04001806 RID: 6150
	[NonSerialized]
	public Vector3 worldDirection = Vector3.left;

	// Token: 0x04001807 RID: 6151
	[NonSerialized]
	public double damageTime;

	// Token: 0x04001808 RID: 6152
	[NonSerialized]
	public double duration;

	// Token: 0x04001809 RID: 6153
	[NonSerialized]
	public double damageAmount;

	// Token: 0x0400180A RID: 6154
	private Vector4 randMin;

	// Token: 0x0400180B RID: 6155
	private Vector4 randMax;

	// Token: 0x0400180C RID: 6156
	private double speedModX;

	// Token: 0x0400180D RID: 6157
	private double speedModY;

	// Token: 0x0400180E RID: 6158
	private double speedModZ;

	// Token: 0x0400180F RID: 6159
	private double speedModW;

	// Token: 0x04001810 RID: 6160
	private bool swapX;

	// Token: 0x04001811 RID: 6161
	private bool swapY;

	// Token: 0x04001812 RID: 6162
	private bool swapZ;

	// Token: 0x04001813 RID: 6163
	private bool inverseX;

	// Token: 0x04001814 RID: 6164
	private bool inverseY;

	// Token: 0x04001815 RID: 6165
	private bool inverseZ;

	// Token: 0x04001816 RID: 6166
	[SerializeField]
	private global::UIPanel panel;

	// Token: 0x04001817 RID: 6167
	[SerializeField]
	private global::UITexture texture;

	// Token: 0x04001818 RID: 6168
	private global::UIPanelMaterialPropertyBlock propBlock = new global::UIPanelMaterialPropertyBlock();
}
