using System;
using UnityEngine;

// Token: 0x02000747 RID: 1863
[AddComponentMenu("Daikon Forge/Tweens/Vector2")]
public class dfTweenVector2 : dfTweenComponent<Vector2>
{
	// Token: 0x06004419 RID: 17433 RVA: 0x00107DE8 File Offset: 0x00105FE8
	public override Vector2 offset(Vector2 lhs, Vector2 rhs)
	{
		return lhs + rhs;
	}

	// Token: 0x0600441A RID: 17434 RVA: 0x00107DF4 File Offset: 0x00105FF4
	public override Vector2 evaluate(Vector2 startValue, Vector2 endValue, float time)
	{
		return new Vector2(dfTweenComponent<Vector2>.Lerp(startValue.x, endValue.x, time), dfTweenComponent<Vector2>.Lerp(startValue.y, endValue.y, time));
	}
}
