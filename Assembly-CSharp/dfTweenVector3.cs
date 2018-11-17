using System;
using UnityEngine;

// Token: 0x02000748 RID: 1864
[AddComponentMenu("Daikon Forge/Tweens/Vector3")]
public class dfTweenVector3 : dfTweenComponent<Vector3>
{
	// Token: 0x0600441C RID: 17436 RVA: 0x00107E2C File Offset: 0x0010602C
	public override Vector3 offset(Vector3 lhs, Vector3 rhs)
	{
		return lhs + rhs;
	}

	// Token: 0x0600441D RID: 17437 RVA: 0x00107E38 File Offset: 0x00106038
	public override Vector3 evaluate(Vector3 startValue, Vector3 endValue, float time)
	{
		return new Vector3(dfTweenComponent<Vector3>.Lerp(startValue.x, endValue.x, time), dfTweenComponent<Vector3>.Lerp(startValue.y, endValue.y, time), dfTweenComponent<Vector3>.Lerp(startValue.z, endValue.z, time));
	}
}
