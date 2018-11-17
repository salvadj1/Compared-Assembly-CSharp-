using System;
using UnityEngine;

// Token: 0x0200082A RID: 2090
[AddComponentMenu("Daikon Forge/Tweens/Vector3")]
public class dfTweenVector3 : global::dfTweenComponent<Vector3>
{
	// Token: 0x0600487D RID: 18557 RVA: 0x001117AC File Offset: 0x0010F9AC
	public override Vector3 offset(Vector3 lhs, Vector3 rhs)
	{
		return lhs + rhs;
	}

	// Token: 0x0600487E RID: 18558 RVA: 0x001117B8 File Offset: 0x0010F9B8
	public override Vector3 evaluate(Vector3 startValue, Vector3 endValue, float time)
	{
		return new Vector3(global::dfTweenComponent<Vector3>.Lerp(startValue.x, endValue.x, time), global::dfTweenComponent<Vector3>.Lerp(startValue.y, endValue.y, time), global::dfTweenComponent<Vector3>.Lerp(startValue.z, endValue.z, time));
	}
}
