using System;
using System.Text;

// Token: 0x02000531 RID: 1329
internal struct ContextMenuItemData
{
	// Token: 0x06002C7F RID: 11391 RVA: 0x000A67F8 File Offset: 0x000A49F8
	public ContextMenuItemData(int name, int utf8_length, byte[] utf8_text)
	{
		this.name = name;
		this.utf8_length = utf8_length;
		this.utf8_text = utf8_text;
	}

	// Token: 0x06002C80 RID: 11392 RVA: 0x000A6810 File Offset: 0x000A4A10
	public ContextMenuItemData(global::ContextActionPrototype prototype)
	{
		this.name = prototype.name;
		string text = prototype.text;
		if (string.IsNullOrEmpty(text))
		{
			this.utf8_length = 0;
			this.utf8_text = null;
		}
		else
		{
			this.utf8_text = Encoding.UTF8.GetBytes(text);
			this.utf8_length = this.utf8_text.Length;
		}
	}

	// Token: 0x04001688 RID: 5768
	public readonly int name;

	// Token: 0x04001689 RID: 5769
	public readonly int utf8_length;

	// Token: 0x0400168A RID: 5770
	public readonly byte[] utf8_text;
}
