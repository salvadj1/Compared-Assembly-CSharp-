using System;
using UnityEngine;

// Token: 0x0200073C RID: 1852
[AddComponentMenu("Daikon Forge/Tweens/Color")]
public class dfTweenColor : dfTweenComponent<Color>
{
	// Token: 0x06004399 RID: 17305 RVA: 0x00106A4C File Offset: 0x00104C4C
	public override Color offset(Color lhs, Color rhs)
	{
		return lhs + rhs;
	}

	// Token: 0x0600439A RID: 17306 RVA: 0x00106A58 File Offset: 0x00104C58
	public override Color evaluate(Color startValue, Color endValue, float time)
	{
		Vector4 vector = startValue;
		Vector4 vector2 = endValue;
		Vector4 vector3;
		vector3..ctor(dfTweenComponent<Color>.Lerp(vector.x, vector2.x, time), dfTweenComponent<Color>.Lerp(vector.y, vector2.y, time), dfTweenComponent<Color>.Lerp(vector.z, vector2.z, time), dfTweenComponent<Color>.Lerp(vector.w, vector2.w, time));
		return vector3;
	}
}
