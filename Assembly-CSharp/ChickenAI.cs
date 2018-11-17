using System;
using uLink;
using UnityEngine;

// Token: 0x02000433 RID: 1075
public class ChickenAI : BasicWildLifeAI
{
	// Token: 0x060027C3 RID: 10179 RVA: 0x0009B298 File Offset: 0x00099498
	protected new void uLink_OnNetworkInstantiate(NetworkMessageInfo info)
	{
		int id = info.networkView.viewID.id;
		this.SetGender((id & 14) >> 1 <= 2, (id & 1) == 1);
		base.uLink_OnNetworkInstantiate(info);
	}

	// Token: 0x060027C4 RID: 10180 RVA: 0x0009B2D8 File Offset: 0x000994D8
	protected void Update()
	{
		if (this._takeDamage.dead)
		{
			return;
		}
		string text = "idleEat";
		float speed = 1f;
		float moveSpeedForAnim = base.GetMoveSpeedForAnim();
		if (moveSpeedForAnim <= 0.001f)
		{
			text = "idleEat";
		}
		else if (moveSpeedForAnim <= 2f)
		{
			text = "walk";
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

	// Token: 0x060027C5 RID: 10181 RVA: 0x0009B394 File Offset: 0x00099594
	protected void SetGender(bool male, bool alt)
	{
		this.isMale = male;
		if (this.isMale)
		{
			this.chickenRenderer.material = this.roosterMat;
		}
		else if (!alt)
		{
			this.chickenRenderer.material = this.ChickenMatA;
		}
		else
		{
			this.chickenRenderer.material = this.ChickenMatB;
		}
	}

	// Token: 0x040013A4 RID: 5028
	protected bool isMale;

	// Token: 0x040013A5 RID: 5029
	[SerializeField]
	protected Material roosterMat;

	// Token: 0x040013A6 RID: 5030
	[SerializeField]
	protected Material ChickenMatA;

	// Token: 0x040013A7 RID: 5031
	[SerializeField]
	protected Material ChickenMatB;

	// Token: 0x040013A8 RID: 5032
	[SerializeField]
	protected Renderer chickenRenderer;

	// Token: 0x040013A9 RID: 5033
	protected string lastMoveAnim;
}
