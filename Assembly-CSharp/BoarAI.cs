using System;

// Token: 0x020004E8 RID: 1256
public class BoarAI : global::BasicWildLifeAI
{
	// Token: 0x06002B51 RID: 11089 RVA: 0x000A1154 File Offset: 0x0009F354
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

	// Token: 0x04001526 RID: 5414
	protected string lastMoveAnim;
}
