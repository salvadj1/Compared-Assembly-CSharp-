using System;
using UnityEngine;

// Token: 0x02000828 RID: 2088
[AddComponentMenu("Daikon Forge/Tweens/Rotation")]
public class dfTweenRotation : global::dfTweenComponent<Quaternion>
{
	// Token: 0x06004875 RID: 18549 RVA: 0x001116A4 File Offset: 0x0010F8A4
	public override Quaternion offset(Quaternion lhs, Quaternion rhs)
	{
		return lhs * rhs;
	}

	// Token: 0x06004876 RID: 18550 RVA: 0x001116B0 File Offset: 0x0010F8B0
	public override Quaternion evaluate(Quaternion startValue, Quaternion endValue, float time)
	{
		Vector3 eulerAngles = startValue.eulerAngles;
		Vector3 eulerAngles2 = endValue.eulerAngles;
		return Quaternion.Euler(global::dfTweenRotation.LerpEuler(eulerAngles, eulerAngles2, time));
	}

	// Token: 0x06004877 RID: 18551 RVA: 0x001116DC File Offset: 0x0010F8DC
	private static Vector3 LerpEuler(Vector3 startValue, Vector3 endValue, float time)
	{
		return new Vector3(global::dfTweenRotation.LerpAngle(startValue.x, endValue.x, time), global::dfTweenRotation.LerpAngle(startValue.y, endValue.y, time), global::dfTweenRotation.LerpAngle(startValue.z, endValue.z, time));
	}

	// Token: 0x06004878 RID: 18552 RVA: 0x0011172C File Offset: 0x0010F92C
	private static float LerpAngle(float startValue, float endValue, float time)
	{
		float num = Mathf.Repeat(endValue - startValue, 360f);
		if (num > 180f)
		{
			num -= 360f;
		}
		return startValue + num * time;
	}
}
