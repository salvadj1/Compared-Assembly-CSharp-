using System;

// Token: 0x020004C3 RID: 1219
public class RPOSArmorWindow : global::RPOSWindow
{
	// Token: 0x060029FA RID: 10746 RVA: 0x0009B474 File Offset: 0x00099674
	protected override void WindowAwake()
	{
		base.WindowAwake();
		this.cellMan = base.GetComponentInChildren<global::RPOSInvCellManager>();
	}

	// Token: 0x060029FB RID: 10747 RVA: 0x0009B488 File Offset: 0x00099688
	public void ForceUpdate()
	{
		global::HumanBodyTakeDamage humanBodyTakeDamage;
		global::DamageTypeList damageTypeList;
		if (global::RPOS.GetObservedPlayerComponent<global::HumanBodyTakeDamage>(out humanBodyTakeDamage))
		{
			damageTypeList = humanBodyTakeDamage.GetArmorValues();
		}
		else
		{
			damageTypeList = new global::DamageTypeList();
		}
		this.leftText.text = string.Empty;
		this.rightText.text = string.Empty;
		for (int i = 0; i < 6; i++)
		{
			if (damageTypeList[i] != 0f)
			{
				global::UILabel uilabel = this.leftText;
				uilabel.text = uilabel.text + global::TakeDamage.DamageIndexToString((global::DamageTypeIndex)i) + "\n";
				global::UILabel uilabel2 = this.rightText;
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

	// Token: 0x0400143E RID: 5182
	public global::UILabel leftText;

	// Token: 0x0400143F RID: 5183
	public global::UILabel rightText;

	// Token: 0x04001440 RID: 5184
	public global::RPOSInvCellManager cellMan;
}
