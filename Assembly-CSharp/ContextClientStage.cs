using System;
using System.Text;

// Token: 0x02000464 RID: 1124
internal struct ContextClientStage
{
	// Token: 0x060028D9 RID: 10457 RVA: 0x000A0328 File Offset: 0x0009E528
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

	// Token: 0x040014D7 RID: 5335
	[NonSerialized]
	public ContextClientStageMenuItem[] option;

	// Token: 0x040014D8 RID: 5336
	[NonSerialized]
	public int length;
}
