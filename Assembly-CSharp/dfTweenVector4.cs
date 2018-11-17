using System;
using UnityEngine;

// Token: 0x0200082B RID: 2091
[AddComponentMenu("Daikon Forge/Tweens/Vector4")]
public class dfTweenVector4 : global::dfTweenComponent<Vector4>
{
	// Token: 0x06004880 RID: 18560 RVA: 0x00111810 File Offset: 0x0010FA10
	public override Vector4 offset(Vector4 lhs, Vector4 rhs)
	{
		return lhs + rhs;
	}

	// Token: 0x06004881 RID: 18561 RVA: 0x0011181C File Offset: 0x0010FA1C
	public override Vector4 evaluate(Vector4 startValue, Vector4 endValue, float time)
	{
		return new Vector4(global::dfTweenComponent<Vector4>.Lerp(startValue.x, endValue.x, time), global::dfTweenComponent<Vector4>.Lerp(startValue.y, endValue.y, time), global::dfTweenComponent<Vector4>.Lerp(startValue.z, endValue.z, time), global::dfTweenComponent<Vector4>.Lerp(startValue.w, endValue.w, time));
	}
}
