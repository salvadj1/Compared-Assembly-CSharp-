using System;
using UnityEngine;

// Token: 0x0200081B RID: 2075
[AddComponentMenu("Daikon Forge/Tweens/Color32")]
public class dfTweenColor32 : global::dfTweenComponent<Color32>
{
	// Token: 0x060047E8 RID: 18408 RVA: 0x0010FDE8 File Offset: 0x0010DFE8
	public override Color32 offset(Color32 lhs, Color32 rhs)
	{
		return lhs + rhs;
	}

	// Token: 0x060047E9 RID: 18409 RVA: 0x0010FE00 File Offset: 0x0010E000
	public override Color32 evaluate(Color32 startValue, Color32 endValue, float time)
	{
		Vector4 vector = startValue;
		Vector4 vector2 = endValue;
		Vector4 vector3;
		vector3..ctor(global::dfTweenComponent<Color32>.Lerp(vector.x, vector2.x, time), global::dfTweenComponent<Color32>.Lerp(vector.y, vector2.y, time), global::dfTweenComponent<Color32>.Lerp(vector.z, vector2.z, time), global::dfTweenComponent<Color32>.Lerp(vector.w, vector2.w, time));
		return vector3;
	}
}
