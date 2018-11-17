using System;
using UnityEngine;

// Token: 0x02000393 RID: 915
public class VisActionMessageEnter : VisAction
{
	// Token: 0x060022C4 RID: 8900 RVA: 0x0008591C File Offset: 0x00083B1C
	public override void Accomplish(IDMain self, IDMain instigator)
	{
		bool flag = !self;
		bool flag2 = !instigator;
		if (flag)
		{
			if (flag2)
			{
				Debug.LogError("Self and instgator are null", this);
			}
			else
			{
				if (this.selfNonNull)
				{
					return;
				}
				Debug.LogWarning("Self is null!", this);
			}
		}
		else if (flag2)
		{
			if (this.instigatorNonNull)
			{
				return;
			}
			Debug.LogWarning("Instigator is null!", this);
		}
		string text;
		string text2;
		if (this.swapMessageOrder)
		{
			IDMain idmain = self;
			self = instigator;
			instigator = idmain;
			text = this.instigatorMessage;
			text2 = this.selfMessage;
			bool flag3 = flag;
			flag = flag2;
			flag2 = flag3;
		}
		else
		{
			text = this.selfMessage;
			text2 = this.instigatorMessage;
		}
		if (this.withOtherAsArg)
		{
			if (!flag && !string.IsNullOrEmpty(text))
			{
				self.SendMessage(text, instigator, 1);
			}
			if (!flag2 && !string.IsNullOrEmpty(text2))
			{
				instigator.SendMessage(text2, self, 1);
			}
		}
		else
		{
			if (!flag && !string.IsNullOrEmpty(text))
			{
				self.SendMessage(text, 1);
			}
			if (!flag2 && !string.IsNullOrEmpty(text2))
			{
				instigator.SendMessage(text2, 1);
			}
		}
	}

	// Token: 0x060022C5 RID: 8901 RVA: 0x00085A50 File Offset: 0x00083C50
	public override void UnAcomplish(IDMain self, IDMain instigator)
	{
	}

	// Token: 0x04001073 RID: 4211
	[SerializeField]
	protected string selfMessage = string.Empty;

	// Token: 0x04001074 RID: 4212
	[SerializeField]
	protected string instigatorMessage = string.Empty;

	// Token: 0x04001075 RID: 4213
	[SerializeField]
	protected bool withOtherAsArg = true;

	// Token: 0x04001076 RID: 4214
	[SerializeField]
	protected bool swapMessageOrder;

	// Token: 0x04001077 RID: 4215
	[SerializeField]
	protected bool selfNonNull;

	// Token: 0x04001078 RID: 4216
	[SerializeField]
	protected bool instigatorNonNull;
}
