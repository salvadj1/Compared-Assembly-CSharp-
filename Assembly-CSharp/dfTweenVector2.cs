using System;
using UnityEngine;

// Token: 0x02000829 RID: 2089
[AddComponentMenu("Daikon Forge/Tweens/Vector2")]
public class dfTweenVector2 : global::dfTweenComponent<Vector2>
{
	// Token: 0x0600487A RID: 18554 RVA: 0x00111768 File Offset: 0x0010F968
	public override Vector2 offset(Vector2 lhs, Vector2 rhs)
	{
		return lhs + rhs;
	}

	// Token: 0x0600487B RID: 18555 RVA: 0x00111774 File Offset: 0x0010F974
	public override Vector2 evaluate(Vector2 startValue, Vector2 endValue, float time)
	{
		return new Vector2(global::dfTweenComponent<Vector2>.Lerp(startValue.x, endValue.x, time), global::dfTweenComponent<Vector2>.Lerp(startValue.y, endValue.y, time));
	}
}
