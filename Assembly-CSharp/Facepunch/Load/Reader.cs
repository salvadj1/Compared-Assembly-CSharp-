using System;
using System.Collections.Generic;
using System.IO;
using LitJson;

namespace Facepunch.Load
{
	// Token: 0x0200026A RID: 618
	public sealed class Reader : Stream
	{
		// Token: 0x06001693 RID: 5779 RVA: 0x000557E4 File Offset: 0x000539E4
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

		// Token: 0x06001694 RID: 5780 RVA: 0x0005588C File Offset: 0x00053A8C
		private Reader(JsonReader json, string bundlePath) : this(json, bundlePath, false)
		{
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x00055898 File Offset: 0x00053A98
		private Reader(TextReader reader, string bundlePath) : this(new JsonReader(reader), bundlePath, false)
		{
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x000558A8 File Offset: 0x00053AA8
		private Reader(string text, string bundlePath) : this(new JsonReader(text), bundlePath, true)
		{
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06001697 RID: 5783 RVA: 0x000558B8 File Offset: 0x00053AB8
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

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06001698 RID: 5784 RVA: 0x000558D8 File Offset: 0x00053AD8
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

		// Token: 0x06001699 RID: 5785 RVA: 0x00055910 File Offset: 0x00053B10
		public static Reader CreateFromFile(string openFilePath, string bundlePath)
		{
			return new Reader(new JsonReader(File.OpenText(openFilePath)), bundlePath, true);
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x00055924 File Offset: 0x00053B24
		public static Reader CreateFromFile(string openFilePath)
		{
			return Reader.CreateFromFile(openFilePath, Path.GetDirectoryName(openFilePath));
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x00055934 File Offset: 0x00053B34
		public static Reader CreateFromText(string jsonText, string bundlePath)
		{
			return new Reader(jsonText, bundlePath);
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x00055940 File Offset: 0x00053B40
		public static Reader CreateFromReader(TextReader textReader, string bundlePath)
		{
			return new Reader(textReader, bundlePath);
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x0005594C File Offset: 0x00053B4C
		public static Reader CreateFromReader(JsonReader textReader, string bundlePath)
		{
			return new Reader(textReader, bundlePath);
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x00055958 File Offset: 0x00053B58
		private string PathToBundle(string incomingPathFromJson)
		{
			if (incomingPathFromJson.Contains("//") || incomingPathFromJson.Contains(":/") || incomingPathFromJson.Contains(":\\") || Path.IsPathRooted(incomingPathFromJson))
			{
				return incomingPathFromJson;
			}
			return this.prefix + incomingPathFromJson;
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x000559B0 File Offset: 0x00053BB0
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

		// Token: 0x060016A0 RID: 5792 RVA: 0x0005602C File Offset: 0x0005422C
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

		// Token: 0x060016A1 RID: 5793 RVA: 0x00056168 File Offset: 0x00054368
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

		// Token: 0x060016A2 RID: 5794 RVA: 0x00056220 File Offset: 0x00054420
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

		// Token: 0x04000B74 RID: 2932
		private JsonReader json;

		// Token: 0x04000B75 RID: 2933
		private bool insideOrderList;

		// Token: 0x04000B76 RID: 2934
		private bool insideRandomList;

		// Token: 0x04000B77 RID: 2935
		private Token token;

		// Token: 0x04000B78 RID: 2936
		private bool disposed;

		// Token: 0x04000B79 RID: 2937
		private readonly bool disposesTextReader;

		// Token: 0x04000B7A RID: 2938
		private readonly string prefix;

		// Token: 0x04000B7B RID: 2939
		private Item item;
	}
}
