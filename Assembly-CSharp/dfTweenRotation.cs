using System;
using UnityEngine;

// Token: 0x02000746 RID: 1862
[AddComponentMenu("Daikon Forge/Tweens/Rotation")]
public class dfTweenRotation : dfTweenComponent<Quaternion>
{
	// Token: 0x06004414 RID: 17428 RVA: 0x00107D24 File Offset: 0x00105F24
	public override Quaternion offset(Quaternion lhs, Quaternion rhs)
	{
		return lhs * rhs;
	}

	// Token: 0x06004415 RID: 17429 RVA: 0x00107D30 File Offset: 0x00105F30
	public override Quaternion evaluate(Quaternion startValue, Quaternion endValue, float time)
	{
		Vector3 eulerAngles = startValue.eulerAngles;
		Vector3 eulerAngles2 = endValue.eulerAngles;
		return Quaternion.Euler(dfTweenRotation.LerpEuler(eulerAngles, eulerAngles2, time));
	}

	// Token: 0x06004416 RID: 17430 RVA: 0x00107D5C File Offset: 0x00105F5C
	private static Vector3 LerpEuler(Vector3 startValue, Vector3 endValue, float time)
	{
		return new Vector3(dfTweenRotation.LerpAngle(startValue.x, endValue.x, time), dfTweenRotation.LerpAngle(startValue.y, endValue.y, time), dfTweenRotation.LerpAngle(startValue.z, endValue.z, time));
	}

	// Token: 0x06004417 RID: 17431 RVA: 0x00107DAC File Offset: 0x00105FAC
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
