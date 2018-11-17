using System;
using UnityEngine;

// Token: 0x02000285 RID: 645
public class BobConfiguration : ScriptableObject
{
	// Token: 0x04000C27 RID: 3111
	public Vector3 springConstant = Vector3.one * 5f;

	// Token: 0x04000C28 RID: 3112
	public Vector3 springDampen = Vector3.one * 0.1f;

	// Token: 0x04000C29 RID: 3113
	public float weightMass = 5f;

	// Token: 0x04000C2A RID: 3114
	public float timeScale = 1f;

	// Token: 0x04000C2B RID: 3115
	public Vector3 forceSpeedMultiplier = Vector3.one;

	// Token: 0x04000C2C RID: 3116
	public Vector3 inputForceMultiplier = Vector3.one;

	// Token: 0x04000C2D RID: 3117
	public Vector3 elipsoidRadii = Vector3.one;

	// Token: 0x04000C2E RID: 3118
	public Vector3 maxVelocity = Vector3.one * 20f;

	// Token: 0x04000C2F RID: 3119
	public Vector3 positionDeadzone = new Vector3(0.0001f, 0.0001f, 0.0001f);

	// Token: 0x04000C30 RID: 3120
	public Vector3 rotationDeadzone = new Vector3(0.0001f, 0.0001f, 0.0001f);

	// Token: 0x04000C31 RID: 3121
	public Vector3 angularSpringConstant = Vector3.one * 5f;

	// Token: 0x04000C32 RID: 3122
	public Vector3 angularSpringDampen = Vector3.one * 0.1f;

	// Token: 0x04000C33 RID: 3123
	public float angularWeightMass = 5f;

	// Token: 0x04000C34 RID: 3124
	[SerializeField]
	public global::BobForceCurve[] additionalCurves;

	// Token: 0x04000C35 RID: 3125
	public AnimationCurve allowCurve;

	// Token: 0x04000C36 RID: 3126
	public AnimationCurve forbidCurve;

	// Token: 0x04000C37 RID: 3127
	public float solveRate = 100f;

	// Token: 0x04000C38 RID: 3128
	public Vector3 impulseForceScale = Vector3.one;

	// Token: 0x04000C39 RID: 3129
	public float impulseForceSmooth = 0.02f;

	// Token: 0x04000C3A RID: 3130
	public float impulseForceMaxChangeAcceleration = float.PositiveInfinity;

	// Token: 0x04000C3B RID: 3131
	public Vector3 angularImpulseForceScale = Vector3.one;

	// Token: 0x04000C3C RID: 3132
	public float angleImpulseForceSmooth = 0.02f;

	// Token: 0x04000C3D RID: 3133
	public float angleImpulseForceMaxChangeAcceleration = float.PositiveInfinity;

	// Token: 0x04000C3E RID: 3134
	public float intermitRate = 20f;

	// Token: 0x04000C3F RID: 3135
	public global::BobAntiOutput[] antiOutputs;
}
