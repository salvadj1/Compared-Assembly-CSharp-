using System;
using UnityEngine;

// Token: 0x02000394 RID: 916
public class VisActionMessageEnterExit : VisActionMessageEnter
{
	// Token: 0x060022C7 RID: 8903 RVA: 0x00085A7C File Offset: 0x00083C7C
	public override void UnAcomplish(IDMain self, IDMain instigator)
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
				if (this.exitSelfNonNull)
				{
					return;
				}
				Debug.LogWarning("Self is null!", this);
			}
		}
		else if (flag2)
		{
			if (this.exitInstigatorNonNull)
			{
				return;
			}
			Debug.LogWarning("Instigator is null!", this);
		}
		string text;
		string text2;
		if (this.exitSwapMessageOrder)
		{
			IDMain idmain = self;
			self = instigator;
			instigator = idmain;
			text = this.exitInstigatorMessage;
			text2 = this.exitSelfMessage;
			bool flag3 = flag;
			flag = flag2;
			flag2 = flag3;
		}
		else
		{
			text = this.exitSelfMessage;
			text2 = this.exitInstigatorMessage;
		}
		if (this.exitWithOtherAsArg)
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

	// Token: 0x04001079 RID: 4217
	[SerializeField]
	protected string exitSelfMessage = string.Empty;

	// Token: 0x0400107A RID: 4218
	[SerializeField]
	protected string exitInstigatorMessage = string.Empty;

	// Token: 0x0400107B RID: 4219
	[SerializeField]
	protected bool exitWithOtherAsArg = true;

	// Token: 0x0400107C RID: 4220
	[SerializeField]
	protected bool exitSwapMessageOrder;

	// Token: 0x0400107D RID: 4221
	[SerializeField]
	protected bool exitSelfNonNull;

	// Token: 0x0400107E RID: 4222
	[SerializeField]
	protected bool exitInstigatorNonNull;
}
