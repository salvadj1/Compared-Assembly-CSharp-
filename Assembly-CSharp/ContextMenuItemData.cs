using System;
using System.Text;

// Token: 0x0200047B RID: 1147
internal struct ContextMenuItemData
{
	// Token: 0x060028EF RID: 10479 RVA: 0x000A0878 File Offset: 0x0009EA78
	public ContextMenuItemData(int name, int utf8_length, byte[] utf8_text)
	{
		this.name = name;
		this.utf8_length = utf8_length;
		this.utf8_text = utf8_text;
	}

	// Token: 0x060028F0 RID: 10480 RVA: 0x000A0890 File Offset: 0x0009EA90
	public ContextMenuItemData(ContextActionPrototype prototype)
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

	// Token: 0x04001505 RID: 5381
	public readonly int name;

	// Token: 0x04001506 RID: 5382
	public readonly int utf8_length;

	// Token: 0x04001507 RID: 5383
	public readonly byte[] utf8_text;
}
