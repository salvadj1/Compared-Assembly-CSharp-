using System;
using UnityEngine;

// Token: 0x020004EE RID: 1262
public class WolfAI : global::HostileWildlifeAI
{
	// Token: 0x06002B6D RID: 11117 RVA: 0x000A192C File Offset: 0x0009FB2C
	public override string GetAttackAnim()
	{
		return "bite";
	}

	// Token: 0x06002B6E RID: 11118 RVA: 0x000A1934 File Offset: 0x0009FB34
	public void Start()
	{
		this.wolfRenderer.material = this.mats[Random.Range(0, this.mats.Length)];
	}

	// Token: 0x06002B6F RID: 11119 RVA: 0x000A1964 File Offset: 0x0009FB64
	protected void Update()
	{
		if (this._takeDamage.dead)
		{
			return;
		}
		string text = "idle";
		float speed = 1f;
		float moveSpeedForAnim = base.GetMoveSpeedForAnim();
		if (moveSpeedForAnim <= 0.001f)
		{
			text = "idle";
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

	// Token: 0x04001549 RID: 5449
	public Renderer wolfRenderer;

	// Token: 0x0400154A RID: 5450
	public Material[] mats;
}
