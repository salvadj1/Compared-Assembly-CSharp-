using System;
using uLink;
using UnityEngine;

// Token: 0x020004E9 RID: 1257
public class ChickenAI : global::BasicWildLifeAI
{
	// Token: 0x06002B53 RID: 11091 RVA: 0x000A1218 File Offset: 0x0009F418
	protected new void uLink_OnNetworkInstantiate(uLink.NetworkMessageInfo info)
	{
		int id = info.networkView.viewID.id;
		this.SetGender((id & 14) >> 1 <= 2, (id & 1) == 1);
		base.uLink_OnNetworkInstantiate(info);
	}

	// Token: 0x06002B54 RID: 11092 RVA: 0x000A1258 File Offset: 0x0009F458
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

	// Token: 0x06002B55 RID: 11093 RVA: 0x000A1314 File Offset: 0x0009F514
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

	// Token: 0x04001527 RID: 5415
	protected bool isMale;

	// Token: 0x04001528 RID: 5416
	[SerializeField]
	protected Material roosterMat;

	// Token: 0x04001529 RID: 5417
	[SerializeField]
	protected Material ChickenMatA;

	// Token: 0x0400152A RID: 5418
	[SerializeField]
	protected Material ChickenMatB;

	// Token: 0x0400152B RID: 5419
	[SerializeField]
	protected Renderer chickenRenderer;

	// Token: 0x0400152C RID: 5420
	protected string lastMoveAnim;
}
