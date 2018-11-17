using System;
using System.Collections.Generic;
using System.Text;

// Token: 0x02000717 RID: 1815
public class dfMarkupEntity
{
	// Token: 0x0600428D RID: 17037 RVA: 0x00102584 File Offset: 0x00100784
	public dfMarkupEntity(string entityName, string entityChar)
	{
		this.EntityName = entityName;
		this.EntityChar = entityChar;
	}

	// Token: 0x0600428F RID: 17039 RVA: 0x0010267C File Offset: 0x0010087C
	public static string Replace(string text)
	{
		dfMarkupEntity.buffer.EnsureCapacity(text.Length);
		dfMarkupEntity.buffer.Length = 0;
		dfMarkupEntity.buffer.Append(text);
		for (int i = 0; i < dfMarkupEntity.HTML_ENTITIES.Count; i++)
		{
			dfMarkupEntity dfMarkupEntity = dfMarkupEntity.HTML_ENTITIES[i];
			dfMarkupEntity.buffer.Replace(dfMarkupEntity.EntityName, dfMarkupEntity.EntityChar);
		}
		return dfMarkupEntity.buffer.ToString();
	}

	// Token: 0x04002311 RID: 8977
	private static List<dfMarkupEntity> HTML_ENTITIES = new List<dfMarkupEntity>
	{
		new dfMarkupEntity("&nbsp;", " "),
		new dfMarkupEntity("&quot;", "\""),
		new dfMarkupEntity("&amp;", "&"),
		new dfMarkupEntity("&lt;", "<"),
		new dfMarkupEntity("&gt;", ">"),
		new dfMarkupEntity("&#39;", "'"),
		new dfMarkupEntity("&trade;", "™"),
		new dfMarkupEntity("&copy;", "©"),
		new dfMarkupEntity("\u00a0", " ")
	};

	// Token: 0x04002312 RID: 8978
	private static StringBuilder buffer = new StringBuilder();

	// Token: 0x04002313 RID: 8979
	public string EntityName;

	// Token: 0x04002314 RID: 8980
	public string EntityChar;
}
