using System;
using UnityEngine;

// Token: 0x02000742 RID: 1858
[AddComponentMenu("Daikon Forge/Tweens/Float")]
public class dfTweenFloat : dfTweenComponent<float>
{
	// Token: 0x060043EF RID: 17391 RVA: 0x00107840 File Offset: 0x00105A40
	public override float offset(float lhs, float rhs)
	{
		return lhs + rhs;
	}

	// Token: 0x060043F0 RID: 17392 RVA: 0x00107848 File Offset: 0x00105A48
	public override float evaluate(float startValue, float endValue, float time)
	{
		return startValue + (endValue - startValue) * time;
	}
}
