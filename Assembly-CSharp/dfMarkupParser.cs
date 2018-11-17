using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

// Token: 0x020007F9 RID: 2041
public class dfMarkupParser
{
	// Token: 0x060046F8 RID: 18168 RVA: 0x0010BE6C File Offset: 0x0010A06C
	static dfMarkupParser()
	{
		RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.CultureInvariant;
		global::dfMarkupParser.TAG_PATTERN = new Regex("(\\<\\/?)(?<tag>[a-zA-Z0-9$_]+)(\\s(?<attr>.+?))?([\\/]*\\>)", options);
		global::dfMarkupParser.ATTR_PATTERN = new Regex("(?<key>[a-zA-Z0-9$_]+)=(?<value>(\"((\\\\\")|\\\\\\\\|[^\"\\n])*\")|('((\\\\')|\\\\\\\\|[^'\\n])*')|\\d+|\\w+)", options);
		global::dfMarkupParser.STYLE_PATTERN = new Regex("(?<key>[a-zA-Z0-9\\-]+)(\\s*\\:\\s*)(?<value>[^;]+)", options);
	}

	// Token: 0x060046F9 RID: 18169 RVA: 0x0010BED4 File Offset: 0x0010A0D4
	public static global::dfList<global::dfMarkupElement> Parse(global::dfRichTextLabel owner, string source)
	{
		global::dfList<global::dfMarkupElement> result;
		try
		{
			global::dfMarkupParser.parserInstance.owner = owner;
			global::dfList<global::dfMarkupElement> dfList = global::dfMarkupParser.parserInstance.parseMarkup(source);
			result = dfList;
		}
		finally
		{
		}
		return result;
	}

	// Token: 0x060046FA RID: 18170 RVA: 0x0010BF24 File Offset: 0x0010A124
	private global::dfList<global::dfMarkupElement> parseMarkup(string source)
	{
		Queue<global::dfMarkupElement> queue = new Queue<global::dfMarkupElement>();
		MatchCollection matchCollection = global::dfMarkupParser.TAG_PATTERN.Matches(source);
		int num = 0;
		for (int i = 0; i < matchCollection.Count; i++)
		{
			Match match = matchCollection[i];
			if (match.Index > num)
			{
				string text = source.Substring(num, match.Index - num);
				global::dfMarkupString item = new global::dfMarkupString(text);
				queue.Enqueue(item);
			}
			num = match.Index + match.Length;
			queue.Enqueue(this.parseTag(match));
		}
		if (num < source.Length)
		{
			string text2 = source.Substring(num);
			global::dfMarkupString item2 = new global::dfMarkupString(text2);
			queue.Enqueue(item2);
		}
		return this.processTokens(queue);
	}

	// Token: 0x060046FB RID: 18171 RVA: 0x0010BFE0 File Offset: 0x0010A1E0
	private global::dfList<global::dfMarkupElement> processTokens(Queue<global::dfMarkupElement> tokens)
	{
		global::dfList<global::dfMarkupElement> dfList = global::dfList<global::dfMarkupElement>.Obtain();
		while (tokens.Count > 0)
		{
			dfList.Add(this.parseElement(tokens));
		}
		for (int i = 0; i < dfList.Count; i++)
		{
			if (dfList[i] is global::dfMarkupTag)
			{
				((global::dfMarkupTag)dfList[i]).Owner = this.owner;
			}
		}
		return dfList;
	}

	// Token: 0x060046FC RID: 18172 RVA: 0x0010C054 File Offset: 0x0010A254
	private global::dfMarkupElement parseElement(Queue<global::dfMarkupElement> tokens)
	{
		global::dfMarkupElement dfMarkupElement = tokens.Dequeue();
		if (dfMarkupElement is global::dfMarkupString)
		{
			return ((global::dfMarkupString)dfMarkupElement).SplitWords();
		}
		global::dfMarkupTag dfMarkupTag = (global::dfMarkupTag)dfMarkupElement;
		if (dfMarkupTag.IsClosedTag || dfMarkupTag.IsEndTag)
		{
			return this.refineTag(dfMarkupTag);
		}
		while (tokens.Count > 0)
		{
			global::dfMarkupElement dfMarkupElement2 = this.parseElement(tokens);
			if (dfMarkupElement2 is global::dfMarkupTag)
			{
				global::dfMarkupTag dfMarkupTag2 = (global::dfMarkupTag)dfMarkupElement2;
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

	// Token: 0x060046FD RID: 18173 RVA: 0x0010C10C File Offset: 0x0010A30C
	private global::dfMarkupTag refineTag(global::dfMarkupTag original)
	{
		if (original.IsEndTag)
		{
			return original;
		}
		if (global::dfMarkupParser.tagTypes == null)
		{
			global::dfMarkupParser.tagTypes = new Dictionary<string, Type>();
			foreach (Type type in Assembly.GetExecutingAssembly().GetExportedTypes())
			{
				if (typeof(global::dfMarkupTag).IsAssignableFrom(type))
				{
					object[] customAttributes = type.GetCustomAttributes(typeof(global::dfMarkupTagInfoAttribute), true);
					if (customAttributes != null && customAttributes.Length != 0)
					{
						for (int j = 0; j < customAttributes.Length; j++)
						{
							string tagName = ((global::dfMarkupTagInfoAttribute)customAttributes[j]).TagName;
							global::dfMarkupParser.tagTypes[tagName] = type;
						}
					}
				}
			}
		}
		if (global::dfMarkupParser.tagTypes.ContainsKey(original.TagName))
		{
			Type type2 = global::dfMarkupParser.tagTypes[original.TagName];
			return (global::dfMarkupTag)Activator.CreateInstance(type2, new object[]
			{
				original
			});
		}
		return original;
	}

	// Token: 0x060046FE RID: 18174 RVA: 0x0010C210 File Offset: 0x0010A410
	private global::dfMarkupElement parseTag(Match tag)
	{
		string text = tag.Groups["tag"].Value.ToLowerInvariant();
		if (tag.Value.StartsWith("</"))
		{
			return new global::dfMarkupTag(text)
			{
				IsEndTag = true
			};
		}
		global::dfMarkupTag dfMarkupTag = new global::dfMarkupTag(text);
		string value = tag.Groups["attr"].Value;
		MatchCollection matchCollection = global::dfMarkupParser.ATTR_PATTERN.Matches(value);
		for (int i = 0; i < matchCollection.Count; i++)
		{
			Match match = matchCollection[i];
			string value2 = match.Groups["key"].Value;
			string text2 = global::dfMarkupEntity.Replace(match.Groups["value"].Value);
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
					dfMarkupTag.Attributes.Add(new global::dfMarkupAttribute(value2, text2));
				}
			}
		}
		if (tag.Value.EndsWith("/>") || text == "br" || text == "img")
		{
			dfMarkupTag.IsClosedTag = true;
		}
		return dfMarkupTag;
	}

	// Token: 0x060046FF RID: 18175 RVA: 0x0010C3B4 File Offset: 0x0010A5B4
	private void parseStyleAttribute(global::dfMarkupTag element, string text)
	{
		MatchCollection matchCollection = global::dfMarkupParser.STYLE_PATTERN.Matches(text);
		for (int i = 0; i < matchCollection.Count; i++)
		{
			Match match = matchCollection[i];
			string name = match.Groups["key"].Value.ToLowerInvariant();
			string value = match.Groups["value"].Value;
			element.Attributes.Add(new global::dfMarkupAttribute(name, value));
		}
	}

	// Token: 0x04002543 RID: 9539
	private static Regex TAG_PATTERN = null;

	// Token: 0x04002544 RID: 9540
	private static Regex ATTR_PATTERN = null;

	// Token: 0x04002545 RID: 9541
	private static Regex STYLE_PATTERN = null;

	// Token: 0x04002546 RID: 9542
	private static Dictionary<string, Type> tagTypes = null;

	// Token: 0x04002547 RID: 9543
	private static global::dfMarkupParser parserInstance = new global::dfMarkupParser();

	// Token: 0x04002548 RID: 9544
	private global::dfRichTextLabel owner;
}
