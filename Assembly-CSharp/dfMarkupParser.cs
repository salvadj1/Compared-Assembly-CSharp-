using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

// Token: 0x0200071D RID: 1821
public class dfMarkupParser
{
	// Token: 0x060042B4 RID: 17076 RVA: 0x00102B5C File Offset: 0x00100D5C
	static dfMarkupParser()
	{
		RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.CultureInvariant;
		dfMarkupParser.TAG_PATTERN = new Regex("(\\<\\/?)(?<tag>[a-zA-Z0-9$_]+)(\\s(?<attr>.+?))?([\\/]*\\>)", options);
		dfMarkupParser.ATTR_PATTERN = new Regex("(?<key>[a-zA-Z0-9$_]+)=(?<value>(\"((\\\\\")|\\\\\\\\|[^\"\\n])*\")|('((\\\\')|\\\\\\\\|[^'\\n])*')|\\d+|\\w+)", options);
		dfMarkupParser.STYLE_PATTERN = new Regex("(?<key>[a-zA-Z0-9\\-]+)(\\s*\\:\\s*)(?<value>[^;]+)", options);
	}

	// Token: 0x060042B5 RID: 17077 RVA: 0x00102BC4 File Offset: 0x00100DC4
	public static dfList<dfMarkupElement> Parse(dfRichTextLabel owner, string source)
	{
		dfList<dfMarkupElement> result;
		try
		{
			dfMarkupParser.parserInstance.owner = owner;
			dfList<dfMarkupElement> dfList = dfMarkupParser.parserInstance.parseMarkup(source);
			result = dfList;
		}
		finally
		{
		}
		return result;
	}

	// Token: 0x060042B6 RID: 17078 RVA: 0x00102C14 File Offset: 0x00100E14
	private dfList<dfMarkupElement> parseMarkup(string source)
	{
		Queue<dfMarkupElement> queue = new Queue<dfMarkupElement>();
		MatchCollection matchCollection = dfMarkupParser.TAG_PATTERN.Matches(source);
		int num = 0;
		for (int i = 0; i < matchCollection.Count; i++)
		{
			Match match = matchCollection[i];
			if (match.Index > num)
			{
				string text = source.Substring(num, match.Index - num);
				dfMarkupString item = new dfMarkupString(text);
				queue.Enqueue(item);
			}
			num = match.Index + match.Length;
			queue.Enqueue(this.parseTag(match));
		}
		if (num < source.Length)
		{
			string text2 = source.Substring(num);
			dfMarkupString item2 = new dfMarkupString(text2);
			queue.Enqueue(item2);
		}
		return this.processTokens(queue);
	}

	// Token: 0x060042B7 RID: 17079 RVA: 0x00102CD0 File Offset: 0x00100ED0
	private dfList<dfMarkupElement> processTokens(Queue<dfMarkupElement> tokens)
	{
		dfList<dfMarkupElement> dfList = dfList<dfMarkupElement>.Obtain();
		while (tokens.Count > 0)
		{
			dfList.Add(this.parseElement(tokens));
		}
		for (int i = 0; i < dfList.Count; i++)
		{
			if (dfList[i] is dfMarkupTag)
			{
				((dfMarkupTag)dfList[i]).Owner = this.owner;
			}
		}
		return dfList;
	}

	// Token: 0x060042B8 RID: 17080 RVA: 0x00102D44 File Offset: 0x00100F44
	private dfMarkupElement parseElement(Queue<dfMarkupElement> tokens)
	{
		dfMarkupElement dfMarkupElement = tokens.Dequeue();
		if (dfMarkupElement is dfMarkupString)
		{
			return ((dfMarkupString)dfMarkupElement).SplitWords();
		}
		dfMarkupTag dfMarkupTag = (dfMarkupTag)dfMarkupElement;
		if (dfMarkupTag.IsClosedTag || dfMarkupTag.IsEndTag)
		{
			return this.refineTag(dfMarkupTag);
		}
		while (tokens.Count > 0)
		{
			dfMarkupElement dfMarkupElement2 = this.parseElement(tokens);
			if (dfMarkupElement2 is dfMarkupTag)
			{
				dfMarkupTag dfMarkupTag2 = (dfMarkupTag)dfMarkupElement2;
				if (dfMarkupTag2.IsEndTag)
				{
					if (dfMarkupTag2.TagName == dfMarkupTag.TagName)
					{
						break;
					}
					return this.refineTag(dfMarkupTag);
				}
			}
			dfMarkupTag.AddChildNode(dfMarkupElement2);
		}
		return this.refineTag(dfMarkupTag);
	}

	// Token: 0x060042B9 RID: 17081 RVA: 0x00102DFC File Offset: 0x00100FFC
	private dfMarkupTag refineTag(dfMarkupTag original)
	{
		if (original.IsEndTag)
		{
			return original;
		}
		if (dfMarkupParser.tagTypes == null)
		{
			dfMarkupParser.tagTypes = new Dictionary<string, Type>();
			foreach (Type type in Assembly.GetExecutingAssembly().GetExportedTypes())
			{
				if (typeof(dfMarkupTag).IsAssignableFrom(type))
				{
					object[] customAttributes = type.GetCustomAttributes(typeof(dfMarkupTagInfoAttribute), true);
					if (customAttributes != null && customAttributes.Length != 0)
					{
						for (int j = 0; j < customAttributes.Length; j++)
						{
							string tagName = ((dfMarkupTagInfoAttribute)customAttributes[j]).TagName;
							dfMarkupParser.tagTypes[tagName] = type;
						}
					}
				}
			}
		}
		if (dfMarkupParser.tagTypes.ContainsKey(original.TagName))
		{
			Type type2 = dfMarkupParser.tagTypes[original.TagName];
			return (dfMarkupTag)Activator.CreateInstance(type2, new object[]
			{
				original
			});
		}
		return original;
	}

	// Token: 0x060042BA RID: 17082 RVA: 0x00102F00 File Offset: 0x00101100
	private dfMarkupElement parseTag(Match tag)
	{
		string text = tag.Groups["tag"].Value.ToLowerInvariant();
		if (tag.Value.StartsWith("</"))
		{
			return new dfMarkupTag(text)
			{
				IsEndTag = true
			};
		}
		dfMarkupTag dfMarkupTag = new dfMarkupTag(text);
		string value = tag.Groups["attr"].Value;
		MatchCollection matchCollection = dfMarkupParser.ATTR_PATTERN.Matches(value);
		for (int i = 0; i < matchCollection.Count; i++)
		{
			Match match = matchCollection[i];
			string value2 = match.Groups["key"].Value;
			string text2 = dfMarkupEntity.Replace(match.Groups["value"].Value);
			if (text2.StartsWith("\""))
			{
				text2 = text2.Trim(new char[]
				{
					'"'
				});
			}
			else if (text2.StartsWith("'"))
			{
				text2 = text2.Trim(new char[]
				{
					'\''
				});
			}
			if (!string.IsNullOrEmpty(text2))
			{
				if (value2 == "style")
				{
					this.parseStyleAttribute(dfMarkupTag, text2);
				}
				else
				{
					dfMarkupTag.Attributes.Add(new dfMarkupAttribute(value2, text2));
				}
			}
		}
		if (tag.Value.EndsWith("/>") || text == "br" || text == "img")
		{
			dfMarkupTag.IsClosedTag = true;
		}
		return dfMarkupTag;
	}

	// Token: 0x060042BB RID: 17083 RVA: 0x001030A4 File Offset: 0x001012A4
	private void parseStyleAttribute(dfMarkupTag element, string text)
	{
		MatchCollection matchCollection = dfMarkupParser.STYLE_PATTERN.Matches(text);
		for (int i = 0; i < matchCollection.Count; i++)
		{
			Match match = matchCollection[i];
			string name = match.Groups["key"].Value.ToLowerInvariant();
			string value = match.Groups["value"].Value;
			element.Attributes.Add(new dfMarkupAttribute(name, value));
		}
	}

	// Token: 0x04002320 RID: 8992
	private static Regex TAG_PATTERN = null;

	// Token: 0x04002321 RID: 8993
	private static Regex ATTR_PATTERN = null;

	// Token: 0x04002322 RID: 8994
	private static Regex STYLE_PATTERN = null;

	// Token: 0x04002323 RID: 8995
	private static Dictionary<string, Type> tagTypes = null;

	// Token: 0x04002324 RID: 8996
	private static dfMarkupParser parserInstance = new dfMarkupParser();

	// Token: 0x04002325 RID: 8997
	private dfRichTextLabel owner;
}
