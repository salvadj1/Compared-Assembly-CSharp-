using System;
using UnityEngine;

// Token: 0x02000440 RID: 1088
public class VisActionMessageEnter : global::VisAction
{
	// Token: 0x06002626 RID: 9766 RVA: 0x0008AD18 File Offset: 0x00088F18
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

	// Token: 0x06002627 RID: 9767 RVA: 0x0008AE4C File Offset: 0x0008904C
	public override void UnAcomplish(IDMain self, IDMain instigator)
	{
	}

	// Token: 0x040011D9 RID: 4569
	[SerializeField]
	protected string selfMessage = string.Empty;

	// Token: 0x040011DA RID: 4570
	[SerializeField]
	protected string instigatorMessage = string.Empty;

	// Token: 0x040011DB RID: 4571
	[SerializeField]
	protected bool withOtherAsArg = true;

	// Token: 0x040011DC RID: 4572
	[SerializeField]
	protected bool swapMessageOrder;

	// Token: 0x040011DD RID: 4573
	[SerializeField]
	protected bool selfNonNull;

	// Token: 0x040011DE RID: 4574
	[SerializeField]
	protected bool instigatorNonNull;
}
