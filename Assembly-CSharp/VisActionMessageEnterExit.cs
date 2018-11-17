using System;
using UnityEngine;

// Token: 0x02000441 RID: 1089
public class VisActionMessageEnterExit : global::VisActionMessageEnter
{
	// Token: 0x06002629 RID: 9769 RVA: 0x0008AE78 File Offset: 0x00089078
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

	// Token: 0x040011DF RID: 4575
	[SerializeField]
	protected string exitSelfMessage = string.Empty;

	// Token: 0x040011E0 RID: 4576
	[SerializeField]
	protected string exitInstigatorMessage = string.Empty;

	// Token: 0x040011E1 RID: 4577
	[SerializeField]
	protected bool exitWithOtherAsArg = true;

	// Token: 0x040011E2 RID: 4578
	[SerializeField]
	protected bool exitSwapMessageOrder;

	// Token: 0x040011E3 RID: 4579
	[SerializeField]
	protected bool exitSelfNonNull;

	// Token: 0x040011E4 RID: 4580
	[SerializeField]
	protected bool exitInstigatorNonNull;
}
