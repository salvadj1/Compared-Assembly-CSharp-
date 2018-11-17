using System;
using UnityEngine;

// Token: 0x020004E7 RID: 1255
public class BearAI : global::HostileWildlifeAI
{
	// Token: 0x06002B4D RID: 11085 RVA: 0x000A1054 File Offset: 0x0009F254
	public override string GetDeathAnim()
	{
		return "4LegsDeath";
	}

	// Token: 0x06002B4E RID: 11086 RVA: 0x000A105C File Offset: 0x0009F25C
	public override string GetAttackAnim()
	{
		int num = Random.Range(0, 3);
		if (num == 0)
		{
			return "4LegsClawsAttackL";
		}
		if (num == 1)
		{
			return "4LegsClawsAttackR";
		}
		return "4LegsBiteAttack";
	}

	// Token: 0x06002B4F RID: 11087 RVA: 0x000A1090 File Offset: 0x0009F290
	protected void Update()
	{
		if (this._takeDamage.dead)
		{
			return;
		}
		string text = "idle4legs";
		float speed = 1f;
		float moveSpeedForAnim = base.GetMoveSpeedForAnim();
		if (moveSpeedForAnim <= 0.001f)
		{
			text = "idle4Legs";
		}
		else if (moveSpeedForAnim <= 2f)
		{
			text = "walk";
			speed = moveSpeedForAnim / base.GetWalkAnimScalar();
		}
		else if (moveSpeedForAnim > 2f)
		{
			text = "run";
			speed = moveSpeedForAnim / base.GetRunAnimScalar();
		}
		if (text != this.lastMoveAnim)
		{
			base.animation.CrossFade(text, 0.25f, 0);
		}
		base.animation[text].speed = speed;
		this.lastMoveAnim = text;
	}
}
