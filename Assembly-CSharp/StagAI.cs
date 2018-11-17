using System;

// Token: 0x02000439 RID: 1081
public class StagAI : BasicWildLifeAI
{
	// Token: 0x060027E1 RID: 10209 RVA: 0x0009BAA8 File Offset: 0x00099CA8
	protected void Update()
	{
		if (this._takeDamage.dead)
		{
			return;
		}
		string text = "idle1";
		float speed = 1f;
		float moveSpeedForAnim = base.GetMoveSpeedForAnim();
		if (moveSpeedForAnim <= 0.001f)
		{
			text = "idle1";
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

	// Token: 0x040013C8 RID: 5064
	protected string lastMoveAnim;
}
