using System;
using System.Collections.Generic;
using System.Text;

// Token: 0x020007F3 RID: 2035
public class dfMarkupEntity
{
	// Token: 0x060046D1 RID: 18129 RVA: 0x0010B894 File Offset: 0x00109A94
	public dfMarkupEntity(string entityName, string entityChar)
	{
		this.EntityName = entityName;
		this.EntityChar = entityChar;
	}

	// Token: 0x060046D3 RID: 18131 RVA: 0x0010B98C File Offset: 0x00109B8C
	public static string Replace(string text)
	{
		global::dfMarkupEntity.buffer.EnsureCapacity(text.Length);
		global::dfMarkupEntity.buffer.Length = 0;
		global::dfMarkupEntity.buffer.Append(text);
		for (int i = 0; i < global::dfMarkupEntity.HTML_ENTITIES.Count; i++)
		{
			global::dfMarkupEntity dfMarkupEntity = global::dfMarkupEntity.HTML_ENTITIES[i];
			global::dfMarkupEntity.buffer.Replace(dfMarkupEntity.EntityName, dfMarkupEntity.EntityChar);
		}
		return global::dfMarkupEntity.buffer.ToString();
	}

	// Token: 0x04002534 RID: 9524
	private static List<global::dfMarkupEntity> HTML_ENTITIES = new List<global::dfMarkupEntity>
	{
		new global::dfMarkupEntity("&nbsp;", " "),
		new global::dfMarkupEntity("&quot;", "\""),
		new global::dfMarkupEntity("&amp;", "&"),
		new global::dfMarkupEntity("&lt;", "<"),
		new global::dfMarkupEntity("&gt;", ">"),
		new global::dfMarkupEntity("&#39;", "'"),
		new global::dfMarkupEntity("&trade;", "™"),
		new global::dfMarkupEntity("&copy;", "©"),
		new global::dfMarkupEntity("\u00a0", " ")
	};

	// Token: 0x04002535 RID: 9525
	private static StringBuilder buffer = new StringBuilder();

	// Token: 0x04002536 RID: 9526
	public string EntityName;

	// Token: 0x04002537 RID: 9527
	public string EntityChar;
}
