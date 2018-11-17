using System;
using UnityEngine;

// Token: 0x02000749 RID: 1865
[AddComponentMenu("Daikon Forge/Tweens/Vector4")]
public class dfTweenVector4 : dfTweenComponent<Vector4>
{
	// Token: 0x0600441F RID: 17439 RVA: 0x00107E90 File Offset: 0x00106090
	public override Vector4 offset(Vector4 lhs, Vector4 rhs)
	{
		return lhs + rhs;
	}

	// Token: 0x06004420 RID: 17440 RVA: 0x00107E9C File Offset: 0x0010609C
	public override Vector4 evaluate(Vector4 startValue, Vector4 endValue, float time)
	{
		return new Vector4(dfTweenComponent<Vector4>.Lerp(startValue.x, endValue.x, time), dfTweenComponent<Vector4>.Lerp(startValue.y, endValue.y, time), dfTweenComponent<Vector4>.Lerp(startValue.z, endValue.z, time), dfTweenComponent<Vector4>.Lerp(startValue.w, endValue.w, time));
	}
}
