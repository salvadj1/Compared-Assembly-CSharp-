using System;

// Token: 0x02000436 RID: 1078
public class RabbitAI : BasicWildLifeAI
{
	// Token: 0x060027DA RID: 10202 RVA: 0x0009B8E0 File Offset: 0x00099AE0
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
			text = "hop";
			speed = moveSpeedForAnim / 0.75f;
		}
		else if (moveSpeedForAnim > 2f)
		{
			text = "run";
			speed = moveSpeedForAnim / 3f;
		}
		if (text != this.lastMoveAnim)
		{
			base.animation.CrossFade(text, 0.25f, 0);
		}
		base.animation[text].speed = speed;
		this.lastMoveAnim = text;
	}

	// Token: 0x040013C5 RID: 5061
	protected string lastMoveAnim;
}
