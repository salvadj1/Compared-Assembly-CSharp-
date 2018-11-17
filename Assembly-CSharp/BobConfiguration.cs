using System;
using UnityEngine;

// Token: 0x02000252 RID: 594
public class BobConfiguration : ScriptableObject
{
	// Token: 0x04000B04 RID: 2820
	public Vector3 springConstant = Vector3.one * 5f;

	// Token: 0x04000B05 RID: 2821
	public Vector3 springDampen = Vector3.one * 0.1f;

	// Token: 0x04000B06 RID: 2822
	public float weightMass = 5f;

	// Token: 0x04000B07 RID: 2823
	public float timeScale = 1f;

	// Token: 0x04000B08 RID: 2824
	public Vector3 forceSpeedMultiplier = Vector3.one;

	// Token: 0x04000B09 RID: 2825
	public Vector3 inputForceMultiplier = Vector3.one;

	// Token: 0x04000B0A RID: 2826
	public Vector3 elipsoidRadii = Vector3.one;

	// Token: 0x04000B0B RID: 2827
	public Vector3 maxVelocity = Vector3.one * 20f;

	// Token: 0x04000B0C RID: 2828
	public Vector3 positionDeadzone = new Vector3(0.0001f, 0.0001f, 0.0001f);

	// Token: 0x04000B0D RID: 2829
	public Vector3 rotationDeadzone = new Vector3(0.0001f, 0.0001f, 0.0001f);

	// Token: 0x04000B0E RID: 2830
	public Vector3 angularSpringConstant = Vector3.one * 5f;

	// Token: 0x04000B0F RID: 2831
	public Vector3 angularSpringDampen = Vector3.one * 0.1f;

	// Token: 0x04000B10 RID: 2832
	public float angularWeightMass = 5f;

	// Token: 0x04000B11 RID: 2833
	[SerializeField]
	public BobForceCurve[] additionalCurves;

	// Token: 0x04000B12 RID: 2834
	public AnimationCurve allowCurve;

	// Token: 0x04000B13 RID: 2835
	public AnimationCurve forbidCurve;

	// Token: 0x04000B14 RID: 2836
	public float solveRate = 100f;

	// Token: 0x04000B15 RID: 2837
	public Vector3 impulseForceScale = Vector3.one;

	// Token: 0x04000B16 RID: 2838
	public float impulseForceSmooth = 0.02f;

	// Token: 0x04000B17 RID: 2839
	public float impulseForceMaxChangeAcceleration = float.PositiveInfinity;

	// Token: 0x04000B18 RID: 2840
	public Vector3 angularImpulseForceScale = Vector3.one;

	// Token: 0x04000B19 RID: 2841
	public float angleImpulseForceSmooth = 0.02f;

	// Token: 0x04000B1A RID: 2842
	public float angleImpulseForceMaxChangeAcceleration = float.PositiveInfinity;

	// Token: 0x04000B1B RID: 2843
	public float intermitRate = 20f;

	// Token: 0x04000B1C RID: 2844
	public BobAntiOutput[] antiOutputs;
}
