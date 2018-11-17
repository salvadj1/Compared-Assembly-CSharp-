using System;
using UnityEngine;

// Token: 0x02000822 RID: 2082
[AddComponentMenu("Daikon Forge/Tweens/Float")]
public class dfTweenFloat : global::dfTweenComponent<float>
{
	// Token: 0x06004843 RID: 18499 RVA: 0x00110F20 File Offset: 0x0010F120
	public override float offset(float lhs, float rhs)
	{
		return lhs + rhs;
	}

	// Token: 0x06004844 RID: 18500 RVA: 0x00110F28 File Offset: 0x0010F128
	public override float evaluate(float startValue, float endValue, float time)
	{
		return startValue + (endValue - startValue) * time;
	}
}
