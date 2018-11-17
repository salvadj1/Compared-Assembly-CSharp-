using System;

// Token: 0x0200040E RID: 1038
public class RPOSArmorWindow : RPOSWindow
{
	// Token: 0x06002670 RID: 9840 RVA: 0x000955B0 File Offset: 0x000937B0
	protected override void WindowAwake()
	{
		base.WindowAwake();
		this.cellMan = base.GetComponentInChildren<RPOSInvCellManager>();
	}

	// Token: 0x06002671 RID: 9841 RVA: 0x000955C4 File Offset: 0x000937C4
	public void ForceUpdate()
	{
		HumanBodyTakeDamage humanBodyTakeDamage;
		DamageTypeList damageTypeList;
		if (RPOS.GetObservedPlayerComponent<HumanBodyTakeDamage>(out humanBodyTakeDamage))
		{
			damageTypeList = humanBodyTakeDamage.GetArmorValues();
		}
		else
		{
			damageTypeList = new DamageTypeList();
		}
		this.leftText.text = string.Empty;
		this.rightText.text = string.Empty;
		for (int i = 0; i < 6; i++)
		{
			if (damageTypeList[i] != 0f)
			{
				UILabel uilabel = this.leftText;
				uilabel.text = uilabel.text + TakeDamage.DamageIndexToString((DamageTypeIndex)i) + "\n";
				UILabel uilabel2 = this.rightText;
				string text = uilabel2.text;
				uilabel2.text = string.Concat(new object[]
				{
					text,
					"+",
					(int)damageTypeList[i],
					"\n"
				});
			}
		}
	}

	// Token: 0x040012BE RID: 4798
	public UILabel leftText;

	// Token: 0x040012BF RID: 4799
	public UILabel rightText;

	// Token: 0x040012C0 RID: 4800
	public RPOSInvCellManager cellMan;
}
