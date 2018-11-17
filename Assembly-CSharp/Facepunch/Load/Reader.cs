using System;
using System.Collections.Generic;
using System.IO;
using LitJson;

namespace Facepunch.Load
{
	// Token: 0x0200029E RID: 670
	public sealed class Reader : Stream
	{
		// Token: 0x060017ED RID: 6125 RVA: 0x00059C2C File Offset: 0x00057E2C
		private Reader(JsonReader json, string bundlePath, bool createdForThisInstance)
		{
			if (json == null)
			{
				throw new ArgumentNullException("json");
			}
			this.json = json;
			this.disposesTextReader = createdForThisInstance;
			this.prefix = bundlePath;
			if (string.IsNullOrEmpty(this.prefix))
			{
				this.prefix = string.Empty;
			}
			else
			{
				char c = this.prefix[this.prefix.Length - 1];
				if (c != '/' && c != '\\')
				{
					this.prefix += "/";
				}
			}
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x00059CD4 File Offset: 0x00057ED4
		private Reader(JsonReader json, string bundlePath) : this(json, bundlePath, false)
		{
		}

		// Token: 0x060017EF RID: 6127 RVA: 0x00059CE0 File Offset: 0x00057EE0
		private Reader(TextReader reader, string bundlePath) : this(new JsonReader(reader), bundlePath, false)
		{
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x00059CF0 File Offset: 0x00057EF0
		private Reader(string text, string bundlePath) : this(new JsonReader(text), bundlePath, true)
		{
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x060017F1 RID: 6129 RVA: 0x00059D00 File Offset: 0x00057F00
		public Token Token
		{
			get
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException("Reader");
				}
				return this.token;
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x060017F2 RID: 6130 RVA: 0x00059D20 File Offset: 0x00057F20
		public Item Item
		{
			get
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException("Reader");
				}
				if (this.token != Token.BundleListing)
				{
					throw new InvalidOperationException("You may only retreive Item when Token is Token.BundleListing!");
				}
				return this.item;
			}
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x00059D58 File Offset: 0x00057F58
		public static Reader CreateFromFile(string openFilePath, string bundlePath)
		{
			return new Reader(new JsonReader(File.OpenText(openFilePath)), bundlePath, true);
		}

		// Token: 0x060017F4 RID: 6132 RVA: 0x00059D6C File Offset: 0x00057F6C
		public static Reader CreateFromFile(string openFilePath)
		{
			return Reader.CreateFromFile(openFilePath, Path.GetDirectoryName(openFilePath));
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x00059D7C File Offset: 0x00057F7C
		public static Reader CreateFromText(string jsonText, string bundlePath)
		{
			return new Reader(jsonText, bundlePath);
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x00059D88 File Offset: 0x00057F88
		public static Reader CreateFromReader(TextReader textReader, string bundlePath)
		{
			return new Reader(textReader, bundlePath);
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x00059D94 File Offset: 0x00057F94
		public static Reader CreateFromReader(JsonReader textReader, string bundlePath)
		{
			return new Reader(textReader, bundlePath);
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x00059DA0 File Offset: 0x00057FA0
		private string PathToBundle(string incomingPathFromJson)
		{
			if (incomingPathFromJson.Contains("//") || incomingPathFromJson.Contains(":/") || incomingPathFromJson.Contains(":\\") || Path.IsPathRooted(incomingPathFromJson))
			{
				return incomingPathFromJson;
			}
			return this.prefix + incomingPathFromJson;
		}

		// Token: 0x060017F9 RID: 6137 RVA: 0x00059DF8 File Offset: 0x00057FF8
		private void ReadBundleListing(string nameOfBundle)
		{
			if (!this.json.Read())
			{
				throw new JsonException("End of stream unexpected");
			}
			if (this.json.Token != JsonToken.ObjectStart)
			{
				throw new JsonException("Expected object start for bundle name (property) " + nameOfBundle);
			}
			this.item.Name = nameOfBundle;
			this.item.ByteLength = -1;
			while (this.json.Read())
			{
				if (this.json.Token == JsonToken.ObjectEnd)
				{
					if (string.IsNullOrEmpty(this.item.Path))
					{
						throw new JsonException("Path to bundle not defined for bundle listing " + nameOfBundle);
					}
					if (this.item.ByteLength == -1)
					{
						throw new JsonException("There was no size property for bundle listing " + nameOfBundle);
					}
					ContentType contentType = this.item.ContentType;
					if (contentType != ContentType.Assets)
					{
						if (contentType != ContentType.Scenes)
						{
							throw new JsonException(string.Concat(new object[]
							{
								"The content ",
								this.item.ContentType,
								" was not handled for bundle listing ",
								nameOfBundle
							}));
						}
						if (this.item.TypeOfAssets != null)
						{
							throw new JsonException("There should not have been a type property for scene bundle listing " + nameOfBundle);
						}
					}
					else if (this.item.TypeOfAssets == null)
					{
						throw new JsonException("There was no valid type property for asset bundle listing " + nameOfBundle);
					}
					return;
				}
				else
				{
					if (this.json.Token == JsonToken.PropertyName)
					{
						bool flag = false;
						string asString = this.json.Value.AsString;
						if (asString != null)
						{
							if (Reader.<>f__switch$map4 == null)
							{
								Reader.<>f__switch$map4 = new Dictionary<string, int>(5)
								{
									{
										"type",
										0
									},
									{
										"size",
										1
									},
									{
										"content",
										2
									},
									{
										"filename",
										3
									},
									{
										"url",
										4
									}
								};
							}
							int num;
							if (Reader.<>f__switch$map4.TryGetValue(asString, out num))
							{
								switch (num)
								{
								case 0:
									if (!this.json.Read())
									{
										throw new JsonException("Unexpected end of stream at type");
									}
									switch (this.json.Token)
									{
									case JsonToken.String:
										try
										{
											this.item.TypeOfAssets = Reader.ParseType(this.json.Value.AsString);
										}
										catch (TypeLoadException inner_exception)
										{
											throw new JsonException(this.json.Value.AsString, inner_exception);
										}
										continue;
									case JsonToken.Null:
										this.item.TypeOfAssets = null;
										continue;
									}
									throw new JsonException("the type property expects only null or string. got : " + this.json.Token);
								case 1:
									if (!this.json.Read())
									{
										throw new JsonException("Unexpected end of stream at size");
									}
									switch (this.json.Token)
									{
									case JsonToken.Int:
									case JsonToken.Float:
										this.item.ByteLength = this.json.Value.AsInt;
										continue;
									}
									throw new JsonException("the size property expects a number. got : " + this.json.Token);
								case 2:
									if (!this.json.Read())
									{
										throw new JsonException("Unexpected end of stream at content");
									}
									switch (this.json.Token)
									{
									case JsonToken.Int:
										this.item.ContentType = (ContentType)this.json.Value.AsInt;
										continue;
									case JsonToken.String:
										try
										{
											this.item.ContentType = (ContentType)((byte)Enum.Parse(typeof(ContentType), this.json.Value.AsString, true));
										}
										catch (ArgumentException inner_exception2)
										{
											throw new JsonException(this.json.Value.AsString, inner_exception2);
										}
										catch (OverflowException inner_exception3)
										{
											throw new JsonException(this.json.Value.AsString, inner_exception3);
										}
										continue;
									}
									throw new JsonException("the content property expects a string or int. got : " + this.json.Token);
								case 3:
								{
									if (!this.json.Read())
									{
										throw new JsonException("Unexpected end of stream at filename");
									}
									JsonToken jsonToken = this.json.Token;
									if (jsonToken != JsonToken.String)
									{
										throw new JsonException("the filename property expects a string. got : " + this.json.Token);
									}
									if (!flag)
									{
										try
										{
											this.item.Path = this.PathToBundle(this.json.Value.AsString);
										}
										catch (Exception inner_exception4)
										{
											throw new JsonException(this.json.Value.AsString, inner_exception4);
										}
									}
									break;
								}
								case 4:
								{
									if (!this.json.Read())
									{
										throw new JsonException("Unexpected end of stream at url");
									}
									JsonToken jsonToken = this.json.Token;
									if (jsonToken != JsonToken.String)
									{
										throw new JsonException("the url property expects a string. got : " + this.json.Token);
									}
									try
									{
										this.item.Path = this.json.Value.AsString;
									}
									catch (Exception inner_exception5)
									{
										throw new JsonException(this.json.Value.AsString, inner_exception5);
									}
									break;
								}
								default:
									goto IL_4BE;
								}
								continue;
							}
						}
						IL_4BE:
						throw new JsonException("Unhandled property named " + this.json.Value.AsString);
					}
					throw new JsonException("Unexpected token in json : JsonToken." + this.json.Token);
				}
			}
			throw new JsonException("Unexpected end of stream");
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x0005A474 File Offset: 0x00058674
		public bool Read()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("Reader");
			}
			this.item = default(Item);
			if (!this.json.Read())
			{
				this.token = Token.End;
				return false;
			}
			if (this.insideOrderList)
			{
				if (this.insideRandomList)
				{
					JsonToken jsonToken = this.json.Token;
					if (jsonToken == JsonToken.PropertyName)
					{
						this.token = Token.BundleListing;
						this.ReadBundleListing(this.json.Value.AsString);
						return true;
					}
					if (jsonToken == JsonToken.ObjectEnd)
					{
						this.token = Token.RandomLoadOrderAreaEnd;
						this.insideRandomList = false;
						return true;
					}
				}
				else
				{
					JsonToken jsonToken = this.json.Token;
					if (jsonToken == JsonToken.ObjectStart)
					{
						this.token = Token.RandomLoadOrderAreaBegin;
						this.insideRandomList = true;
						return true;
					}
					if (jsonToken == JsonToken.ArrayEnd)
					{
						this.token = Token.DownloadQueueEnd;
						this.insideOrderList = false;
						return true;
					}
				}
			}
			else
			{
				JsonToken jsonToken = this.json.Token;
				if (jsonToken == JsonToken.None)
				{
					this.token = Token.End;
					return false;
				}
				if (jsonToken == JsonToken.ArrayStart)
				{
					this.token = Token.DownloadQueueBegin;
					this.insideOrderList = true;
					return true;
				}
			}
			throw new JsonException("Bad json state");
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x0005A5B0 File Offset: 0x000587B0
		public override void Dispose()
		{
			if (!this.disposed)
			{
				if (!this.disposesTextReader)
				{
					while (this.token != Token.End && this.token != Token.DownloadQueueEnd)
					{
						try
						{
							this.Read();
						}
						catch (JsonException)
						{
							this.token = Token.End;
						}
					}
				}
				else
				{
					try
					{
						this.json.Dispose();
					}
					catch (ObjectDisposedException)
					{
					}
				}
				this.json = null;
				this.disposed = true;
			}
		}

		// Token: 0x060017FC RID: 6140 RVA: 0x0005A668 File Offset: 0x00058868
		private static Type ParseType(string str)
		{
			Type type = Type.GetType(str, false, true);
			if (type != null)
			{
				return type;
			}
			string typeName = "Facepunch.MeshBatch." + str;
			type = Type.GetType(typeName, false, true);
			if (type != null)
			{
				return type;
			}
			return Type.GetType(str, true, true);
		}

		// Token: 0x04000C9A RID: 3226
		private JsonReader json;

		// Token: 0x04000C9B RID: 3227
		private bool insideOrderList;

		// Token: 0x04000C9C RID: 3228
		private bool insideRandomList;

		// Token: 0x04000C9D RID: 3229
		private Token token;

		// Token: 0x04000C9E RID: 3230
		private bool disposed;

		// Token: 0x04000C9F RID: 3231
		private readonly bool disposesTextReader;

		// Token: 0x04000CA0 RID: 3232
		private readonly string prefix;

		// Token: 0x04000CA1 RID: 3233
		private Item item;
	}
}
