using System;

// Token: 0x020004EC RID: 1260
public class RabbitAI : global::BasicWildLifeAI
{
	// Token: 0x06002B6A RID: 11114 RVA: 0x000A1860 File Offset: 0x0009FA60
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

	// Token: 0x04001548 RID: 5448
	protected string lastMoveAnim;
}
