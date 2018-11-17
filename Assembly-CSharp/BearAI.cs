using System;
using UnityEngine;

// Token: 0x02000431 RID: 1073
public class BearAI : HostileWildlifeAI
{
	// Token: 0x060027BD RID: 10173 RVA: 0x0009B0D4 File Offset: 0x000992D4
	public override string GetDeathAnim()
	{
		return "4LegsDeath";
	}

	// Token: 0x060027BE RID: 10174 RVA: 0x0009B0DC File Offset: 0x000992DC
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

	// Token: 0x060027BF RID: 10175 RVA: 0x0009B110 File Offset: 0x00099310
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
