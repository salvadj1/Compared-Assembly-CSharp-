using System;
using System.Text;

// Token: 0x0200051A RID: 1306
internal struct ContextClientStage
{
	// Token: 0x06002C69 RID: 11369 RVA: 0x000A62A8 File Offset: 0x000A44A8
	public void Set(ContextMenuData data)
	{
		if (this.length < data.options_length)
		{
			this.option = new ContextClientStageMenuItem[data.options_length];
			this.length = data.options_length;
		}
		else
		{
			while (this.length > data.options_length)
			{
				this.option[--this.length].text = null;
			}
		}
		for (int i = 0; i < data.options_length; i++)
		{
			this.option[i].name = data.options[i].name;
			if (data.options[i].utf8_length == 0)
			{
				this.option[i].text = string.Empty;
			}
			else
			{
				this.option[i].text = Encoding.UTF8.GetString(data.options[i].utf8_text, 0, data.options[i].utf8_length);
			}
		}
	}

	// Token: 0x0400165A RID: 5722
	[NonSerialized]
	public ContextClientStageMenuItem[] option;

	// Token: 0x0400165B RID: 5723
	[NonSerialized]
	public int length;
}
