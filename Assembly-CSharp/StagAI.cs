using System;

// Token: 0x020004EF RID: 1263
public class StagAI : global::BasicWildLifeAI
{
	// Token: 0x06002B71 RID: 11121 RVA: 0x000A1A28 File Offset: 0x0009FC28
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

	// Token: 0x0400154B RID: 5451
	protected string lastMoveAnim;
}
