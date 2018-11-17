using System;
using UnityEngine;

// Token: 0x0200081A RID: 2074
[AddComponentMenu("Daikon Forge/Tweens/Color")]
public class dfTweenColor : global::dfTweenComponent<Color>
{
	// Token: 0x060047E5 RID: 18405 RVA: 0x0010FD5C File Offset: 0x0010DF5C
	public override Color offset(Color lhs, Color rhs)
	{
		return lhs + rhs;
	}

	// Token: 0x060047E6 RID: 18406 RVA: 0x0010FD68 File Offset: 0x0010DF68
	public override Color evaluate(Color startValue, Color endValue, float time)
	{
		Vector4 vector = startValue;
		Vector4 vector2 = endValue;
		Vector4 vector3;
		vector3..ctor(global::dfTweenComponent<Color>.Lerp(vector.x, vector2.x, time), global::dfTweenComponent<Color>.Lerp(vector.y, vector2.y, time), global::dfTweenComponent<Color>.Lerp(vector.z, vector2.z, time), global::dfTweenComponent<Color>.Lerp(vector.w, vector2.w, time));
		return vector3;
	}
}
