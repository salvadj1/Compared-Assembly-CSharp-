using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UnityEngine;

namespace JSON
{
	// Token: 0x02000856 RID: 2134
	public class Object : IEnumerable, IEnumerable<KeyValuePair<string, Value>>
	{
		// Token: 0x06004B1D RID: 19229 RVA: 0x001485D4 File Offset: 0x001467D4
		public Object()
		{
		}

		// Token: 0x06004B1E RID: 19230 RVA: 0x001485E8 File Offset: 0x001467E8
		public Object(Object other)
		{
			this.values = new Dictionary<string, Value>();
			if (other != null)
			{
				foreach (KeyValuePair<string, Value> keyValuePair in other.values)
				{
					this.values[keyValuePair.Key] = new Value(keyValuePair.Value);
				}
			}
		}

		// Token: 0x06004B1F RID: 19231 RVA: 0x00148684 File Offset: 0x00146884
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.values.GetEnumerator();
		}

		// Token: 0x06004B20 RID: 19232 RVA: 0x00148694 File Offset: 0x00146894
		public bool ContainsKey(string key)
		{
			return this.values.ContainsKey(key);
		}

		// Token: 0x06004B21 RID: 19233 RVA: 0x001486A4 File Offset: 0x001468A4
		public Value GetValue(string key)
		{
			Value result;
			this.values.TryGetValue(key, out result);
			return result;
		}

		// Token: 0x06004B22 RID: 19234 RVA: 0x001486C4 File Offset: 0x001468C4
		public string GetString(string key, string strDEFAULT = "")
		{
			Value value = this.GetValue(key);
			if (value == null)
			{
				return strDEFAULT;
			}
			string str = value.Str;
			return str.Replace("\\/", "/");
		}

		// Token: 0x06004B23 RID: 19235 RVA: 0x001486FC File Offset: 0x001468FC
		public double GetNumber(string key, double iDefault = 0.0)
		{
			Value value = this.GetValue(key);
			if (value == null)
			{
				return iDefault;
			}
			if (value.Type == ValueType.Number)
			{
				return value.Number;
			}
			if (value.Type == ValueType.String)
			{
				double result = iDefault;
				if (double.TryParse(value.Str, out result))
				{
					return result;
				}
			}
			return iDefault;
		}

		// Token: 0x06004B24 RID: 19236 RVA: 0x00148750 File Offset: 0x00146950
		public int GetInt(string key, int iDefault = 0)
		{
			return (int)this.GetNumber(key, (double)iDefault);
		}

		// Token: 0x06004B25 RID: 19237 RVA: 0x0014875C File Offset: 0x0014695C
		public float GetFloat(string key, float iDefault = 0f)
		{
			return (float)this.GetNumber(key, (double)iDefault);
		}

		// Token: 0x06004B26 RID: 19238 RVA: 0x00148768 File Offset: 0x00146968
		public Object GetObject(string key)
		{
			Value value = this.GetValue(key);
			if (value == null)
			{
				return new Object();
			}
			return value.Obj;
		}

		// Token: 0x06004B27 RID: 19239 RVA: 0x00148790 File Offset: 0x00146990
		public bool GetBoolean(string key, bool bDefault = false)
		{
			Value value = this.GetValue(key);
			if (value == null)
			{
				return bDefault;
			}
			if (value.Type == ValueType.Boolean)
			{
				return value.Boolean;
			}
			if (value.Type == ValueType.Number)
			{
				return value.Number != 0.0;
			}
			return bDefault;
		}

		// Token: 0x06004B28 RID: 19240 RVA: 0x001487E4 File Offset: 0x001469E4
		public Array GetArray(string key)
		{
			Value value = this.GetValue(key);
			if (value == null)
			{
				return new Array();
			}
			return value.Array;
		}

		// Token: 0x17000EAE RID: 3758
		public Value this[string key]
		{
			get
			{
				return this.GetValue(key);
			}
			set
			{
				this.values[key] = value;
			}
		}

		// Token: 0x06004B2B RID: 19243 RVA: 0x00148828 File Offset: 0x00146A28
		public void Add(string key, Value value)
		{
			this.values[key] = value;
		}

		// Token: 0x06004B2C RID: 19244 RVA: 0x00148838 File Offset: 0x00146A38
		public void Add(KeyValuePair<string, Value> pair)
		{
			this.values[pair.Key] = pair.Value;
		}

		// Token: 0x06004B2D RID: 19245 RVA: 0x00148854 File Offset: 0x00146A54
		public static Object Parse(string jsonString)
		{
			if (string.IsNullOrEmpty(jsonString))
			{
				return null;
			}
			Value value = null;
			List<string> list = new List<string>();
			Object.ParsingState parsingState = Object.ParsingState.Object;
			for (int i = 0; i < jsonString.Length; i++)
			{
				i = Object.SkipWhitespace(jsonString, i);
				switch (parsingState)
				{
				case Object.ParsingState.Object:
				{
					if (jsonString[i] != '{')
					{
						return Object.Fail('{', i);
					}
					Value value2 = new Object();
					if (value != null)
					{
						value2.Parent = value;
					}
					value = value2;
					parsingState = Object.ParsingState.Key;
					break;
				}
				case Object.ParsingState.Array:
				{
					if (jsonString[i] != '[')
					{
						return Object.Fail('[', i);
					}
					Value value3 = new Array();
					if (value != null)
					{
						value3.Parent = value;
					}
					value = value3;
					parsingState = Object.ParsingState.Value;
					break;
				}
				case Object.ParsingState.EndObject:
				{
					if (jsonString[i] != '}')
					{
						return Object.Fail('}', i);
					}
					if (value.Parent == null)
					{
						return value.Obj;
					}
					ValueType type = value.Parent.Type;
					if (type != ValueType.Object)
					{
						if (type != ValueType.Array)
						{
							return Object.Fail("valid object", i);
						}
						value.Parent.Array.Add(new Value(value.Obj));
					}
					else
					{
						value.Parent.Obj.values[list.Pop<string>()] = new Value(value.Obj);
					}
					value = value.Parent;
					parsingState = Object.ParsingState.ValueSeparator;
					break;
				}
				case Object.ParsingState.EndArray:
				{
					if (jsonString[i] != ']')
					{
						return Object.Fail(']', i);
					}
					if (value.Parent == null)
					{
						return value.Obj;
					}
					ValueType type = value.Parent.Type;
					if (type != ValueType.Object)
					{
						if (type != ValueType.Array)
						{
							return Object.Fail("valid object", i);
						}
						value.Parent.Array.Add(new Value(value.Array));
					}
					else
					{
						value.Parent.Obj.values[list.Pop<string>()] = new Value(value.Array);
					}
					value = value.Parent;
					parsingState = Object.ParsingState.ValueSeparator;
					break;
				}
				case Object.ParsingState.Key:
					if (jsonString[i] == '}')
					{
						i--;
						parsingState = Object.ParsingState.EndObject;
					}
					else
					{
						string text = Object.ParseString(jsonString, ref i);
						if (text == null)
						{
							return Object.Fail("key string", i);
						}
						list.Add(text);
						parsingState = Object.ParsingState.KeyValueSeparator;
					}
					break;
				case Object.ParsingState.Value:
				{
					char c = jsonString[i];
					if (c == '"')
					{
						parsingState = Object.ParsingState.String;
					}
					else if (char.IsDigit(c) || c == '-')
					{
						parsingState = Object.ParsingState.Number;
					}
					else
					{
						char c2 = c;
						switch (c2)
						{
						case '[':
							parsingState = Object.ParsingState.Array;
							break;
						default:
							if (c2 != 'f')
							{
								if (c2 == 'n')
								{
									parsingState = Object.ParsingState.Null;
									break;
								}
								if (c2 != 't')
								{
									if (c2 != '{')
									{
										return Object.Fail("beginning of value", i);
									}
									parsingState = Object.ParsingState.Object;
									break;
								}
							}
							parsingState = Object.ParsingState.Boolean;
							break;
						case ']':
							if (value.Type != ValueType.Array)
							{
								return Object.Fail("valid array", i);
							}
							parsingState = Object.ParsingState.EndArray;
							break;
						}
					}
					i--;
					break;
				}
				case Object.ParsingState.KeyValueSeparator:
					if (jsonString[i] != ':')
					{
						return Object.Fail(':', i);
					}
					parsingState = Object.ParsingState.Value;
					break;
				case Object.ParsingState.ValueSeparator:
				{
					char c2 = jsonString[i];
					if (c2 != ',')
					{
						if (c2 != ']')
						{
							if (c2 != '}')
							{
								return Object.Fail(", } ]", i);
							}
							parsingState = Object.ParsingState.EndObject;
							i--;
						}
						else
						{
							parsingState = Object.ParsingState.EndArray;
							i--;
						}
					}
					else
					{
						parsingState = ((value.Type != ValueType.Object) ? Object.ParsingState.Value : Object.ParsingState.Key);
					}
					break;
				}
				case Object.ParsingState.String:
				{
					string text2 = Object.ParseString(jsonString, ref i);
					if (text2 == null)
					{
						return Object.Fail("string value", i);
					}
					ValueType type = value.Type;
					if (type != ValueType.Object)
					{
						if (type != ValueType.Array)
						{
							Debug.LogError("Fatal error, current JSON value not valid");
							return null;
						}
						value.Array.Add(text2);
					}
					else
					{
						value.Obj.values[list.Pop<string>()] = new Value(text2);
					}
					parsingState = Object.ParsingState.ValueSeparator;
					break;
				}
				case Object.ParsingState.Number:
				{
					double num = Object.ParseNumber(jsonString, ref i);
					if (double.IsNaN(num))
					{
						return Object.Fail("valid number", i);
					}
					ValueType type = value.Type;
					if (type != ValueType.Object)
					{
						if (type != ValueType.Array)
						{
							Debug.LogError("Fatal error, current JSON value not valid");
							return null;
						}
						value.Array.Add(num);
					}
					else
					{
						value.Obj.values[list.Pop<string>()] = new Value(num);
					}
					parsingState = Object.ParsingState.ValueSeparator;
					break;
				}
				case Object.ParsingState.Boolean:
					if (jsonString[i] == 't')
					{
						if (jsonString.Length < i + 4 || jsonString[i + 1] != 'r' || jsonString[i + 2] != 'u' || jsonString[i + 3] != 'e')
						{
							return Object.Fail("true", i);
						}
						ValueType type = value.Type;
						if (type != ValueType.Object)
						{
							if (type != ValueType.Array)
							{
								Debug.LogError("Fatal error, current JSON value not valid");
								return null;
							}
							value.Array.Add(new Value(true));
						}
						else
						{
							value.Obj.values[list.Pop<string>()] = new Value(true);
						}
						i += 3;
					}
					else
					{
						if (jsonString.Length < i + 5 || jsonString[i + 1] != 'a' || jsonString[i + 2] != 'l' || jsonString[i + 3] != 's' || jsonString[i + 4] != 'e')
						{
							return Object.Fail("false", i);
						}
						ValueType type = value.Type;
						if (type != ValueType.Object)
						{
							if (type != ValueType.Array)
							{
								Debug.LogError("Fatal error, current JSON value not valid");
								return null;
							}
							value.Array.Add(new Value(false));
						}
						else
						{
							value.Obj.values[list.Pop<string>()] = new Value(false);
						}
						i += 4;
					}
					parsingState = Object.ParsingState.ValueSeparator;
					break;
				case Object.ParsingState.Null:
					if (jsonString[i] == 'n')
					{
						if (jsonString.Length < i + 4 || jsonString[i + 1] != 'u' || jsonString[i + 2] != 'l' || jsonString[i + 3] != 'l')
						{
							return Object.Fail("null", i);
						}
						ValueType type = value.Type;
						if (type != ValueType.Object)
						{
							if (type != ValueType.Array)
							{
								Debug.LogError("Fatal error, current JSON value not valid");
								return null;
							}
							value.Array.Add(new Value(ValueType.Null));
						}
						else
						{
							value.Obj.values[list.Pop<string>()] = new Value(ValueType.Null);
						}
						i += 3;
					}
					parsingState = Object.ParsingState.ValueSeparator;
					break;
				}
			}
			Debug.LogError("Unexpected end of string");
			return null;
		}

		// Token: 0x06004B2E RID: 19246 RVA: 0x00148FB0 File Offset: 0x001471B0
		private static int SkipWhitespace(string str, int pos)
		{
			while (pos < str.Length && char.IsWhiteSpace(str[pos]))
			{
				pos++;
			}
			return pos;
		}

		// Token: 0x06004B2F RID: 19247 RVA: 0x00148FE8 File Offset: 0x001471E8
		private static string ParseString(string str, ref int startPosition)
		{
			if (str[startPosition] != '"' || startPosition + 1 >= str.Length)
			{
				Object.Fail('"', startPosition);
				return null;
			}
			int num = str.IndexOf('"', startPosition + 1);
			if (num <= startPosition)
			{
				Object.Fail('"', startPosition + 1);
				return null;
			}
			while (str[num - 1] == '\\')
			{
				num = str.IndexOf('"', num + 1);
				if (num <= startPosition)
				{
					Object.Fail('"', startPosition + 1);
					return null;
				}
			}
			string result = string.Empty;
			if (num > startPosition + 1)
			{
				result = str.Substring(startPosition + 1, num - startPosition - 1);
			}
			startPosition = num;
			return result;
		}

		// Token: 0x06004B30 RID: 19248 RVA: 0x001490A0 File Offset: 0x001472A0
		private static double ParseNumber(string str, ref int startPosition)
		{
			if (startPosition >= str.Length || (!char.IsDigit(str[startPosition]) && str[startPosition] != '-'))
			{
				return double.NaN;
			}
			int num = startPosition + 1;
			while (num < str.Length && str[num] != ',' && str[num] != ']' && str[num] != '}')
			{
				num++;
			}
			double result;
			if (!double.TryParse(str.Substring(startPosition, num - startPosition), NumberStyles.Float, CultureInfo.InvariantCulture, out result))
			{
				return double.NaN;
			}
			startPosition = num - 1;
			return result;
		}

		// Token: 0x06004B31 RID: 19249 RVA: 0x0014915C File Offset: 0x0014735C
		private static Object Fail(char expected, int position)
		{
			return Object.Fail(new string(expected, 1), position);
		}

		// Token: 0x06004B32 RID: 19250 RVA: 0x0014916C File Offset: 0x0014736C
		private static Object Fail(string expected, int position)
		{
			Debug.LogError(string.Concat(new object[]
			{
				"Invalid json string, expecting ",
				expected,
				" at ",
				position
			}));
			return null;
		}

		// Token: 0x06004B33 RID: 19251 RVA: 0x001491A8 File Offset: 0x001473A8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('{');
			foreach (KeyValuePair<string, Value> keyValuePair in this.values)
			{
				stringBuilder.Append("\"" + keyValuePair.Key + "\"");
				stringBuilder.Append(':');
				stringBuilder.Append(keyValuePair.Value.ToString());
				stringBuilder.Append(',');
			}
			if (this.values.Count > 0)
			{
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}

		// Token: 0x06004B34 RID: 19252 RVA: 0x00149288 File Offset: 0x00147488
		public IEnumerator<KeyValuePair<string, Value>> GetEnumerator()
		{
			return this.values.GetEnumerator();
		}

		// Token: 0x06004B35 RID: 19253 RVA: 0x00149298 File Offset: 0x00147498
		public void Clear()
		{
			this.values.Clear();
		}

		// Token: 0x06004B36 RID: 19254 RVA: 0x001492A8 File Offset: 0x001474A8
		public void Remove(string key)
		{
			if (this.values.ContainsKey(key))
			{
				this.values.Remove(key);
			}
		}

		// Token: 0x04002C1A RID: 11290
		private readonly IDictionary<string, Value> values = new Dictionary<string, Value>();

		// Token: 0x02000857 RID: 2135
		private enum ParsingState
		{
			// Token: 0x04002C1C RID: 11292
			Object,
			// Token: 0x04002C1D RID: 11293
			Array,
			// Token: 0x04002C1E RID: 11294
			EndObject,
			// Token: 0x04002C1F RID: 11295
			EndArray,
			// Token: 0x04002C20 RID: 11296
			Key,
			// Token: 0x04002C21 RID: 11297
			Value,
			// Token: 0x04002C22 RID: 11298
			KeyValueSeparator,
			// Token: 0x04002C23 RID: 11299
			ValueSeparator,
			// Token: 0x04002C24 RID: 11300
			String,
			// Token: 0x04002C25 RID: 11301
			Number,
			// Token: 0x04002C26 RID: 11302
			Boolean,
			// Token: 0x04002C27 RID: 11303
			Null
		}
	}
}
