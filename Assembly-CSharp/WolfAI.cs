using System;
using UnityEngine;

// Token: 0x02000438 RID: 1080
public class WolfAI : HostileWildlifeAI
{
	// Token: 0x060027DD RID: 10205 RVA: 0x0009B9AC File Offset: 0x00099BAC
	public override string GetAttackAnim()
	{
		return "bite";
	}

	// Token: 0x060027DE RID: 10206 RVA: 0x0009B9B4 File Offset: 0x00099BB4
	public void Start()
	{
		this.wolfRenderer.material = this.mats[Random.Range(0, this.mats.Length)];
	}

	// Token: 0x060027DF RID: 10207 RVA: 0x0009B9E4 File Offset: 0x00099BE4
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

	// Token: 0x040013C6 RID: 5062
	public Renderer wolfRenderer;

	// Token: 0x040013C7 RID: 5063
	public Material[] mats;
}
