using System;
using System.Diagnostics;
using System.IO;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;
using RustProto.Helpers;

namespace RustProto
{
	// Token: 0x0200020B RID: 523
	[DebuggerNonUserCode]
	public sealed class objectNetInstance : GeneratedMessage<objectNetInstance, objectNetInstance.Builder>
	{
		// Token: 0x060010B1 RID: 4273 RVA: 0x0003D600 File Offset: 0x0003B800
		private objectNetInstance()
		{
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x0003D610 File Offset: 0x0003B810
		static objectNetInstance()
		{
			object.ReferenceEquals(Worldsave.Descriptor, null);
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x0003D67C File Offset: 0x0003B87C
		public static Recycler<objectNetInstance, objectNetInstance.Builder> Recycler()
		{
			return Recycler<objectNetInstance, objectNetInstance.Builder>.Manufacture();
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x060010B4 RID: 4276 RVA: 0x0003D684 File Offset: 0x0003B884
		public static objectNetInstance DefaultInstance
		{
			get
			{
				return objectNetInstance.defaultInstance;
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x060010B5 RID: 4277 RVA: 0x0003D68C File Offset: 0x0003B88C
		public override objectNetInstance DefaultInstanceForType
		{
			get
			{
				return objectNetInstance.DefaultInstance;
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x060010B6 RID: 4278 RVA: 0x0003D694 File Offset: 0x0003B894
		protected override objectNetInstance ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x060010B7 RID: 4279 RVA: 0x0003D698 File Offset: 0x0003B898
		public static MessageDescriptor Descriptor
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectNetInstance__Descriptor;
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x060010B8 RID: 4280 RVA: 0x0003D6A0 File Offset: 0x0003B8A0
		protected override FieldAccessorTable<objectNetInstance, objectNetInstance.Builder> InternalFieldAccessors
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectNetInstance__FieldAccessorTable;
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x060010B9 RID: 4281 RVA: 0x0003D6A8 File Offset: 0x0003B8A8
		public bool HasServerPrefab
		{
			get
			{
				return this.hasServerPrefab;
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x060010BA RID: 4282 RVA: 0x0003D6B0 File Offset: 0x0003B8B0
		public int ServerPrefab
		{
			get
			{
				return this.serverPrefab_;
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x060010BB RID: 4283 RVA: 0x0003D6B8 File Offset: 0x0003B8B8
		public bool HasOwnerPrefab
		{
			get
			{
				return this.hasOwnerPrefab;
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x060010BC RID: 4284 RVA: 0x0003D6C0 File Offset: 0x0003B8C0
		public int OwnerPrefab
		{
			get
			{
				return this.ownerPrefab_;
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x060010BD RID: 4285 RVA: 0x0003D6C8 File Offset: 0x0003B8C8
		public bool HasProxyPrefab
		{
			get
			{
				return this.hasProxyPrefab;
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x060010BE RID: 4286 RVA: 0x0003D6D0 File Offset: 0x0003B8D0
		public int ProxyPrefab
		{
			get
			{
				return this.proxyPrefab_;
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x060010BF RID: 4287 RVA: 0x0003D6D8 File Offset: 0x0003B8D8
		public bool HasGroupID
		{
			get
			{
				return this.hasGroupID;
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x060010C0 RID: 4288 RVA: 0x0003D6E0 File Offset: 0x0003B8E0
		public int GroupID
		{
			get
			{
				return this.groupID_;
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x060010C1 RID: 4289 RVA: 0x0003D6E8 File Offset: 0x0003B8E8
		public override bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x0003D6EC File Offset: 0x0003B8EC
		public override void WriteTo(ICodedOutputStream output)
		{
			int serializedSize = this.SerializedSize;
			string[] objectNetInstanceFieldNames = objectNetInstance._objectNetInstanceFieldNames;
			if (this.hasServerPrefab)
			{
				output.WriteInt32(1, objectNetInstanceFieldNames[3], this.ServerPrefab);
			}
			if (this.hasOwnerPrefab)
			{
				output.WriteInt32(2, objectNetInstanceFieldNames[1], this.OwnerPrefab);
			}
			if (this.hasProxyPrefab)
			{
				output.WriteInt32(3, objectNetInstanceFieldNames[2], this.ProxyPrefab);
			}
			if (this.hasGroupID)
			{
				output.WriteInt32(4, objectNetInstanceFieldNames[0], this.GroupID);
			}
			this.UnknownFields.WriteTo(output);
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x060010C3 RID: 4291 RVA: 0x0003D780 File Offset: 0x0003B980
		public override int SerializedSize
		{
			get
			{
				int num = this.memoizedSerializedSize;
				if (num != -1)
				{
					return num;
				}
				num = 0;
				if (this.hasServerPrefab)
				{
					num += CodedOutputStream.ComputeInt32Size(1, this.ServerPrefab);
				}
				if (this.hasOwnerPrefab)
				{
					num += CodedOutputStream.ComputeInt32Size(2, this.OwnerPrefab);
				}
				if (this.hasProxyPrefab)
				{
					num += CodedOutputStream.ComputeInt32Size(3, this.ProxyPrefab);
				}
				if (this.hasGroupID)
				{
					num += CodedOutputStream.ComputeInt32Size(4, this.GroupID);
				}
				num += this.UnknownFields.SerializedSize;
				this.memoizedSerializedSize = num;
				return num;
			}
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x0003D820 File Offset: 0x0003BA20
		public static objectNetInstance ParseFrom(ByteString data)
		{
			return objectNetInstance.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x0003D834 File Offset: 0x0003BA34
		public static objectNetInstance ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return objectNetInstance.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x0003D848 File Offset: 0x0003BA48
		public static objectNetInstance ParseFrom(byte[] data)
		{
			return objectNetInstance.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x0003D85C File Offset: 0x0003BA5C
		public static objectNetInstance ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return objectNetInstance.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x0003D870 File Offset: 0x0003BA70
		public static objectNetInstance ParseFrom(Stream input)
		{
			return objectNetInstance.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x0003D884 File Offset: 0x0003BA84
		public static objectNetInstance ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectNetInstance.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x0003D898 File Offset: 0x0003BA98
		public static objectNetInstance ParseDelimitedFrom(Stream input)
		{
			return objectNetInstance.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x0003D8AC File Offset: 0x0003BAAC
		public static objectNetInstance ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectNetInstance.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x0003D8C0 File Offset: 0x0003BAC0
		public static objectNetInstance ParseFrom(ICodedInputStream input)
		{
			return objectNetInstance.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x0003D8D4 File Offset: 0x0003BAD4
		public static objectNetInstance ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return objectNetInstance.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x0003D8E8 File Offset: 0x0003BAE8
		private objectNetInstance MakeReadOnly()
		{
			return this;
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x0003D8EC File Offset: 0x0003BAEC
		public static objectNetInstance.Builder CreateBuilder()
		{
			return new objectNetInstance.Builder();
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x0003D8F4 File Offset: 0x0003BAF4
		public override objectNetInstance.Builder ToBuilder()
		{
			return objectNetInstance.CreateBuilder(this);
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x0003D8FC File Offset: 0x0003BAFC
		public override objectNetInstance.Builder CreateBuilderForType()
		{
			return new objectNetInstance.Builder();
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x0003D904 File Offset: 0x0003BB04
		public static objectNetInstance.Builder CreateBuilder(objectNetInstance prototype)
		{
			return new objectNetInstance.Builder(prototype);
		}

		// Token: 0x0400093F RID: 2367
		public const int ServerPrefabFieldNumber = 1;

		// Token: 0x04000940 RID: 2368
		public const int OwnerPrefabFieldNumber = 2;

		// Token: 0x04000941 RID: 2369
		public const int ProxyPrefabFieldNumber = 3;

		// Token: 0x04000942 RID: 2370
		public const int GroupIDFieldNumber = 4;

		// Token: 0x04000943 RID: 2371
		private static readonly objectNetInstance defaultInstance = new objectNetInstance().MakeReadOnly();

		// Token: 0x04000944 RID: 2372
		private static readonly string[] _objectNetInstanceFieldNames = new string[]
		{
			"groupID",
			"ownerPrefab",
			"proxyPrefab",
			"serverPrefab"
		};

		// Token: 0x04000945 RID: 2373
		private static readonly uint[] _objectNetInstanceFieldTags = new uint[]
		{
			32u,
			16u,
			24u,
			8u
		};

		// Token: 0x04000946 RID: 2374
		private bool hasServerPrefab;

		// Token: 0x04000947 RID: 2375
		private int serverPrefab_;

		// Token: 0x04000948 RID: 2376
		private bool hasOwnerPrefab;

		// Token: 0x04000949 RID: 2377
		private int ownerPrefab_;

		// Token: 0x0400094A RID: 2378
		private bool hasProxyPrefab;

		// Token: 0x0400094B RID: 2379
		private int proxyPrefab_;

		// Token: 0x0400094C RID: 2380
		private bool hasGroupID;

		// Token: 0x0400094D RID: 2381
		private int groupID_;

		// Token: 0x0400094E RID: 2382
		private int memoizedSerializedSize = -1;

		// Token: 0x0200020C RID: 524
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<objectNetInstance, objectNetInstance.Builder>
		{
			// Token: 0x060010D3 RID: 4307 RVA: 0x0003D90C File Offset: 0x0003BB0C
			public Builder()
			{
				this.result = objectNetInstance.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x060010D4 RID: 4308 RVA: 0x0003D928 File Offset: 0x0003BB28
			internal Builder(objectNetInstance cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x17000485 RID: 1157
			// (get) Token: 0x060010D5 RID: 4309 RVA: 0x0003D940 File Offset: 0x0003BB40
			protected override objectNetInstance.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x060010D6 RID: 4310 RVA: 0x0003D944 File Offset: 0x0003BB44
			private objectNetInstance PrepareBuilder()
			{
				if (this.resultIsReadOnly)
				{
					objectNetInstance other = this.result;
					this.result = new objectNetInstance();
					this.resultIsReadOnly = false;
					this.MergeFrom(other);
				}
				return this.result;
			}

			// Token: 0x17000486 RID: 1158
			// (get) Token: 0x060010D7 RID: 4311 RVA: 0x0003D984 File Offset: 0x0003BB84
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x17000487 RID: 1159
			// (get) Token: 0x060010D8 RID: 4312 RVA: 0x0003D994 File Offset: 0x0003BB94
			protected override objectNetInstance MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x060010D9 RID: 4313 RVA: 0x0003D99C File Offset: 0x0003BB9C
			public override objectNetInstance.Builder Clear()
			{
				this.result = objectNetInstance.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x060010DA RID: 4314 RVA: 0x0003D9B4 File Offset: 0x0003BBB4
			public override objectNetInstance.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new objectNetInstance.Builder(this.result);
				}
				return new objectNetInstance.Builder().MergeFrom(this.result);
			}

			// Token: 0x17000488 RID: 1160
			// (get) Token: 0x060010DB RID: 4315 RVA: 0x0003D9E0 File Offset: 0x0003BBE0
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return objectNetInstance.Descriptor;
				}
			}

			// Token: 0x17000489 RID: 1161
			// (get) Token: 0x060010DC RID: 4316 RVA: 0x0003D9E8 File Offset: 0x0003BBE8
			public override objectNetInstance DefaultInstanceForType
			{
				get
				{
					return objectNetInstance.DefaultInstance;
				}
			}

			// Token: 0x060010DD RID: 4317 RVA: 0x0003D9F0 File Offset: 0x0003BBF0
			public override objectNetInstance BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x060010DE RID: 4318 RVA: 0x0003DA24 File Offset: 0x0003BC24
			public override objectNetInstance.Builder MergeFrom(IMessage other)
			{
				if (other is objectNetInstance)
				{
					return this.MergeFrom((objectNetInstance)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x060010DF RID: 4319 RVA: 0x0003DA48 File Offset: 0x0003BC48
			public override objectNetInstance.Builder MergeFrom(objectNetInstance other)
			{
				if (other == objectNetInstance.DefaultInstance)
				{
					return this;
				}
				this.PrepareBuilder();
				if (other.HasServerPrefab)
				{
					this.ServerPrefab = other.ServerPrefab;
				}
				if (other.HasOwnerPrefab)
				{
					this.OwnerPrefab = other.OwnerPrefab;
				}
				if (other.HasProxyPrefab)
				{
					this.ProxyPrefab = other.ProxyPrefab;
				}
				if (other.HasGroupID)
				{
					this.GroupID = other.GroupID;
				}
				this.MergeUnknownFields(other.UnknownFields);
				return this;
			}

			// Token: 0x060010E0 RID: 4320 RVA: 0x0003DAD4 File Offset: 0x0003BCD4
			public override objectNetInstance.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x060010E1 RID: 4321 RVA: 0x0003DAE4 File Offset: 0x0003BCE4
			public override objectNetInstance.Builder MergeFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
			{
				this.PrepareBuilder();
				UnknownFieldSet.Builder builder = null;
				uint num;
				string text;
				while (input.ReadTag(ref num, ref text))
				{
					if (num == 0u && text != null)
					{
						int num2 = Array.BinarySearch<string>(objectNetInstance._objectNetInstanceFieldNames, text, StringComparer.Ordinal);
						if (num2 < 0)
						{
							if (builder == null)
							{
								builder = UnknownFieldSet.CreateBuilder(this.UnknownFields);
							}
							this.ParseUnknownField(input, builder, extensionRegistry, num, text);
							continue;
						}
						num = objectNetInstance._objectNetInstanceFieldTags[num2];
					}
					uint num3 = num;
					if (num3 == 0u)
					{
						throw InvalidProtocolBufferException.InvalidTag();
					}
					if (num3 != 8u)
					{
						if (num3 != 16u)
						{
							if (num3 != 24u)
							{
								if (num3 != 32u)
								{
									if (WireFormat.IsEndGroupTag(num))
									{
										if (builder != null)
										{
											this.UnknownFields = builder.Build();
										}
										return this;
									}
									if (builder == null)
									{
										builder = UnknownFieldSet.CreateBuilder(this.UnknownFields);
									}
									this.ParseUnknownField(input, builder, extensionRegistry, num, text);
								}
								else
								{
									this.result.hasGroupID = input.ReadInt32(ref this.result.groupID_);
								}
							}
							else
							{
								this.result.hasProxyPrefab = input.ReadInt32(ref this.result.proxyPrefab_);
							}
						}
						else
						{
							this.result.hasOwnerPrefab = input.ReadInt32(ref this.result.ownerPrefab_);
						}
					}
					else
					{
						this.result.hasServerPrefab = input.ReadInt32(ref this.result.serverPrefab_);
					}
				}
				if (builder != null)
				{
					this.UnknownFields = builder.Build();
				}
				return this;
			}

			// Token: 0x1700048A RID: 1162
			// (get) Token: 0x060010E2 RID: 4322 RVA: 0x0003DC74 File Offset: 0x0003BE74
			public bool HasServerPrefab
			{
				get
				{
					return this.result.hasServerPrefab;
				}
			}

			// Token: 0x1700048B RID: 1163
			// (get) Token: 0x060010E3 RID: 4323 RVA: 0x0003DC84 File Offset: 0x0003BE84
			// (set) Token: 0x060010E4 RID: 4324 RVA: 0x0003DC94 File Offset: 0x0003BE94
			public int ServerPrefab
			{
				get
				{
					return this.result.ServerPrefab;
				}
				set
				{
					this.SetServerPrefab(value);
				}
			}

			// Token: 0x060010E5 RID: 4325 RVA: 0x0003DCA0 File Offset: 0x0003BEA0
			public objectNetInstance.Builder SetServerPrefab(int value)
			{
				this.PrepareBuilder();
				this.result.hasServerPrefab = true;
				this.result.serverPrefab_ = value;
				return this;
			}

			// Token: 0x060010E6 RID: 4326 RVA: 0x0003DCD0 File Offset: 0x0003BED0
			public objectNetInstance.Builder ClearServerPrefab()
			{
				this.PrepareBuilder();
				this.result.hasServerPrefab = false;
				this.result.serverPrefab_ = 0;
				return this;
			}

			// Token: 0x1700048C RID: 1164
			// (get) Token: 0x060010E7 RID: 4327 RVA: 0x0003DD00 File Offset: 0x0003BF00
			public bool HasOwnerPrefab
			{
				get
				{
					return this.result.hasOwnerPrefab;
				}
			}

			// Token: 0x1700048D RID: 1165
			// (get) Token: 0x060010E8 RID: 4328 RVA: 0x0003DD10 File Offset: 0x0003BF10
			// (set) Token: 0x060010E9 RID: 4329 RVA: 0x0003DD20 File Offset: 0x0003BF20
			public int OwnerPrefab
			{
				get
				{
					return this.result.OwnerPrefab;
				}
				set
				{
					this.SetOwnerPrefab(value);
				}
			}

			// Token: 0x060010EA RID: 4330 RVA: 0x0003DD2C File Offset: 0x0003BF2C
			public objectNetInstance.Builder SetOwnerPrefab(int value)
			{
				this.PrepareBuilder();
				this.result.hasOwnerPrefab = true;
				this.result.ownerPrefab_ = value;
				return this;
			}

			// Token: 0x060010EB RID: 4331 RVA: 0x0003DD5C File Offset: 0x0003BF5C
			public objectNetInstance.Builder ClearOwnerPrefab()
			{
				this.PrepareBuilder();
				this.result.hasOwnerPrefab = false;
				this.result.ownerPrefab_ = 0;
				return this;
			}

			// Token: 0x1700048E RID: 1166
			// (get) Token: 0x060010EC RID: 4332 RVA: 0x0003DD8C File Offset: 0x0003BF8C
			public bool HasProxyPrefab
			{
				get
				{
					return this.result.hasProxyPrefab;
				}
			}

			// Token: 0x1700048F RID: 1167
			// (get) Token: 0x060010ED RID: 4333 RVA: 0x0003DD9C File Offset: 0x0003BF9C
			// (set) Token: 0x060010EE RID: 4334 RVA: 0x0003DDAC File Offset: 0x0003BFAC
			public int ProxyPrefab
			{
				get
				{
					return this.result.ProxyPrefab;
				}
				set
				{
					this.SetProxyPrefab(value);
				}
			}

			// Token: 0x060010EF RID: 4335 RVA: 0x0003DDB8 File Offset: 0x0003BFB8
			public objectNetInstance.Builder SetProxyPrefab(int value)
			{
				this.PrepareBuilder();
				this.result.hasProxyPrefab = true;
				this.result.proxyPrefab_ = value;
				return this;
			}

			// Token: 0x060010F0 RID: 4336 RVA: 0x0003DDE8 File Offset: 0x0003BFE8
			public objectNetInstance.Builder ClearProxyPrefab()
			{
				this.PrepareBuilder();
				this.result.hasProxyPrefab = false;
				this.result.proxyPrefab_ = 0;
				return this;
			}

			// Token: 0x17000490 RID: 1168
			// (get) Token: 0x060010F1 RID: 4337 RVA: 0x0003DE18 File Offset: 0x0003C018
			public bool HasGroupID
			{
				get
				{
					return this.result.hasGroupID;
				}
			}

			// Token: 0x17000491 RID: 1169
			// (get) Token: 0x060010F2 RID: 4338 RVA: 0x0003DE28 File Offset: 0x0003C028
			// (set) Token: 0x060010F3 RID: 4339 RVA: 0x0003DE38 File Offset: 0x0003C038
			public int GroupID
			{
				get
				{
					return this.result.GroupID;
				}
				set
				{
					this.SetGroupID(value);
				}
			}

			// Token: 0x060010F4 RID: 4340 RVA: 0x0003DE44 File Offset: 0x0003C044
			public objectNetInstance.Builder SetGroupID(int value)
			{
				this.PrepareBuilder();
				this.result.hasGroupID = true;
				this.result.groupID_ = value;
				return this;
			}

			// Token: 0x060010F5 RID: 4341 RVA: 0x0003DE74 File Offset: 0x0003C074
			public objectNetInstance.Builder ClearGroupID()
			{
				this.PrepareBuilder();
				this.result.hasGroupID = false;
				this.result.groupID_ = 0;
				return this;
			}

			// Token: 0x0400094F RID: 2383
			private bool resultIsReadOnly;

			// Token: 0x04000950 RID: 2384
			private objectNetInstance result;
		}
	}
}
