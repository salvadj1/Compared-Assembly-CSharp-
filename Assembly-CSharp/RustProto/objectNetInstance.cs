using System;
using System.Diagnostics;
using System.IO;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;
using RustProto.Helpers;

namespace RustProto
{
	// Token: 0x0200023E RID: 574
	[DebuggerNonUserCode]
	public sealed class objectNetInstance : GeneratedMessage<objectNetInstance, objectNetInstance.Builder>
	{
		// Token: 0x06001205 RID: 4613 RVA: 0x000419A8 File Offset: 0x0003FBA8
		private objectNetInstance()
		{
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x000419B8 File Offset: 0x0003FBB8
		static objectNetInstance()
		{
			object.ReferenceEquals(Worldsave.Descriptor, null);
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x00041A24 File Offset: 0x0003FC24
		public static RustProto.Helpers.Recycler<objectNetInstance, objectNetInstance.Builder> Recycler()
		{
			return RustProto.Helpers.Recycler<objectNetInstance, objectNetInstance.Builder>.Manufacture();
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06001208 RID: 4616 RVA: 0x00041A2C File Offset: 0x0003FC2C
		public static objectNetInstance DefaultInstance
		{
			get
			{
				return objectNetInstance.defaultInstance;
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06001209 RID: 4617 RVA: 0x00041A34 File Offset: 0x0003FC34
		public override objectNetInstance DefaultInstanceForType
		{
			get
			{
				return objectNetInstance.DefaultInstance;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x0600120A RID: 4618 RVA: 0x00041A3C File Offset: 0x0003FC3C
		protected override objectNetInstance ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x0600120B RID: 4619 RVA: 0x00041A40 File Offset: 0x0003FC40
		public static MessageDescriptor Descriptor
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectNetInstance__Descriptor;
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x0600120C RID: 4620 RVA: 0x00041A48 File Offset: 0x0003FC48
		protected override FieldAccessorTable<objectNetInstance, objectNetInstance.Builder> InternalFieldAccessors
		{
			get
			{
				return Worldsave.internal__static_RustProto_objectNetInstance__FieldAccessorTable;
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x0600120D RID: 4621 RVA: 0x00041A50 File Offset: 0x0003FC50
		public bool HasServerPrefab
		{
			get
			{
				return this.hasServerPrefab;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x0600120E RID: 4622 RVA: 0x00041A58 File Offset: 0x0003FC58
		public int ServerPrefab
		{
			get
			{
				return this.serverPrefab_;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x0600120F RID: 4623 RVA: 0x00041A60 File Offset: 0x0003FC60
		public bool HasOwnerPrefab
		{
			get
			{
				return this.hasOwnerPrefab;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001210 RID: 4624 RVA: 0x00041A68 File Offset: 0x0003FC68
		public int OwnerPrefab
		{
			get
			{
				return this.ownerPrefab_;
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001211 RID: 4625 RVA: 0x00041A70 File Offset: 0x0003FC70
		public bool HasProxyPrefab
		{
			get
			{
				return this.hasProxyPrefab;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001212 RID: 4626 RVA: 0x00041A78 File Offset: 0x0003FC78
		public int ProxyPrefab
		{
			get
			{
				return this.proxyPrefab_;
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001213 RID: 4627 RVA: 0x00041A80 File Offset: 0x0003FC80
		public bool HasGroupID
		{
			get
			{
				return this.hasGroupID;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06001214 RID: 4628 RVA: 0x00041A88 File Offset: 0x0003FC88
		public int GroupID
		{
			get
			{
				return this.groupID_;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001215 RID: 4629 RVA: 0x00041A90 File Offset: 0x0003FC90
		public override bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x00041A94 File Offset: 0x0003FC94
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

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001217 RID: 4631 RVA: 0x00041B28 File Offset: 0x0003FD28
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

		// Token: 0x06001218 RID: 4632 RVA: 0x00041BC8 File Offset: 0x0003FDC8
		public static objectNetInstance ParseFrom(ByteString data)
		{
			return objectNetInstance.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x00041BDC File Offset: 0x0003FDDC
		public static objectNetInstance ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return objectNetInstance.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x00041BF0 File Offset: 0x0003FDF0
		public static objectNetInstance ParseFrom(byte[] data)
		{
			return objectNetInstance.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x00041C04 File Offset: 0x0003FE04
		public static objectNetInstance ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return objectNetInstance.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x00041C18 File Offset: 0x0003FE18
		public static objectNetInstance ParseFrom(Stream input)
		{
			return objectNetInstance.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x00041C2C File Offset: 0x0003FE2C
		public static objectNetInstance ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectNetInstance.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x00041C40 File Offset: 0x0003FE40
		public static objectNetInstance ParseDelimitedFrom(Stream input)
		{
			return objectNetInstance.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x00041C54 File Offset: 0x0003FE54
		public static objectNetInstance ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return objectNetInstance.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x00041C68 File Offset: 0x0003FE68
		public static objectNetInstance ParseFrom(ICodedInputStream input)
		{
			return objectNetInstance.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x00041C7C File Offset: 0x0003FE7C
		public static objectNetInstance ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return objectNetInstance.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x00041C90 File Offset: 0x0003FE90
		private objectNetInstance MakeReadOnly()
		{
			return this;
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x00041C94 File Offset: 0x0003FE94
		public static objectNetInstance.Builder CreateBuilder()
		{
			return new objectNetInstance.Builder();
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x00041C9C File Offset: 0x0003FE9C
		public override objectNetInstance.Builder ToBuilder()
		{
			return objectNetInstance.CreateBuilder(this);
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x00041CA4 File Offset: 0x0003FEA4
		public override objectNetInstance.Builder CreateBuilderForType()
		{
			return new objectNetInstance.Builder();
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x00041CAC File Offset: 0x0003FEAC
		public static objectNetInstance.Builder CreateBuilder(objectNetInstance prototype)
		{
			return new objectNetInstance.Builder(prototype);
		}

		// Token: 0x04000A62 RID: 2658
		public const int ServerPrefabFieldNumber = 1;

		// Token: 0x04000A63 RID: 2659
		public const int OwnerPrefabFieldNumber = 2;

		// Token: 0x04000A64 RID: 2660
		public const int ProxyPrefabFieldNumber = 3;

		// Token: 0x04000A65 RID: 2661
		public const int GroupIDFieldNumber = 4;

		// Token: 0x04000A66 RID: 2662
		private static readonly objectNetInstance defaultInstance = new objectNetInstance().MakeReadOnly();

		// Token: 0x04000A67 RID: 2663
		private static readonly string[] _objectNetInstanceFieldNames = new string[]
		{
			"groupID",
			"ownerPrefab",
			"proxyPrefab",
			"serverPrefab"
		};

		// Token: 0x04000A68 RID: 2664
		private static readonly uint[] _objectNetInstanceFieldTags = new uint[]
		{
			32u,
			16u,
			24u,
			8u
		};

		// Token: 0x04000A69 RID: 2665
		private bool hasServerPrefab;

		// Token: 0x04000A6A RID: 2666
		private int serverPrefab_;

		// Token: 0x04000A6B RID: 2667
		private bool hasOwnerPrefab;

		// Token: 0x04000A6C RID: 2668
		private int ownerPrefab_;

		// Token: 0x04000A6D RID: 2669
		private bool hasProxyPrefab;

		// Token: 0x04000A6E RID: 2670
		private int proxyPrefab_;

		// Token: 0x04000A6F RID: 2671
		private bool hasGroupID;

		// Token: 0x04000A70 RID: 2672
		private int groupID_;

		// Token: 0x04000A71 RID: 2673
		private int memoizedSerializedSize = -1;

		// Token: 0x0200023F RID: 575
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<objectNetInstance, objectNetInstance.Builder>
		{
			// Token: 0x06001227 RID: 4647 RVA: 0x00041CB4 File Offset: 0x0003FEB4
			public Builder()
			{
				this.result = objectNetInstance.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x06001228 RID: 4648 RVA: 0x00041CD0 File Offset: 0x0003FED0
			internal Builder(objectNetInstance cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x170004CD RID: 1229
			// (get) Token: 0x06001229 RID: 4649 RVA: 0x00041CE8 File Offset: 0x0003FEE8
			protected override objectNetInstance.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x0600122A RID: 4650 RVA: 0x00041CEC File Offset: 0x0003FEEC
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

			// Token: 0x170004CE RID: 1230
			// (get) Token: 0x0600122B RID: 4651 RVA: 0x00041D2C File Offset: 0x0003FF2C
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x170004CF RID: 1231
			// (get) Token: 0x0600122C RID: 4652 RVA: 0x00041D3C File Offset: 0x0003FF3C
			protected override objectNetInstance MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x0600122D RID: 4653 RVA: 0x00041D44 File Offset: 0x0003FF44
			public override objectNetInstance.Builder Clear()
			{
				this.result = objectNetInstance.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x0600122E RID: 4654 RVA: 0x00041D5C File Offset: 0x0003FF5C
			public override objectNetInstance.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new objectNetInstance.Builder(this.result);
				}
				return new objectNetInstance.Builder().MergeFrom(this.result);
			}

			// Token: 0x170004D0 RID: 1232
			// (get) Token: 0x0600122F RID: 4655 RVA: 0x00041D88 File Offset: 0x0003FF88
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return objectNetInstance.Descriptor;
				}
			}

			// Token: 0x170004D1 RID: 1233
			// (get) Token: 0x06001230 RID: 4656 RVA: 0x00041D90 File Offset: 0x0003FF90
			public override objectNetInstance DefaultInstanceForType
			{
				get
				{
					return objectNetInstance.DefaultInstance;
				}
			}

			// Token: 0x06001231 RID: 4657 RVA: 0x00041D98 File Offset: 0x0003FF98
			public override objectNetInstance BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x06001232 RID: 4658 RVA: 0x00041DCC File Offset: 0x0003FFCC
			public override objectNetInstance.Builder MergeFrom(IMessage other)
			{
				if (other is objectNetInstance)
				{
					return this.MergeFrom((objectNetInstance)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x06001233 RID: 4659 RVA: 0x00041DF0 File Offset: 0x0003FFF0
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

			// Token: 0x06001234 RID: 4660 RVA: 0x00041E7C File Offset: 0x0004007C
			public override objectNetInstance.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x06001235 RID: 4661 RVA: 0x00041E8C File Offset: 0x0004008C
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

			// Token: 0x170004D2 RID: 1234
			// (get) Token: 0x06001236 RID: 4662 RVA: 0x0004201C File Offset: 0x0004021C
			public bool HasServerPrefab
			{
				get
				{
					return this.result.hasServerPrefab;
				}
			}

			// Token: 0x170004D3 RID: 1235
			// (get) Token: 0x06001237 RID: 4663 RVA: 0x0004202C File Offset: 0x0004022C
			// (set) Token: 0x06001238 RID: 4664 RVA: 0x0004203C File Offset: 0x0004023C
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

			// Token: 0x06001239 RID: 4665 RVA: 0x00042048 File Offset: 0x00040248
			public objectNetInstance.Builder SetServerPrefab(int value)
			{
				this.PrepareBuilder();
				this.result.hasServerPrefab = true;
				this.result.serverPrefab_ = value;
				return this;
			}

			// Token: 0x0600123A RID: 4666 RVA: 0x00042078 File Offset: 0x00040278
			public objectNetInstance.Builder ClearServerPrefab()
			{
				this.PrepareBuilder();
				this.result.hasServerPrefab = false;
				this.result.serverPrefab_ = 0;
				return this;
			}

			// Token: 0x170004D4 RID: 1236
			// (get) Token: 0x0600123B RID: 4667 RVA: 0x000420A8 File Offset: 0x000402A8
			public bool HasOwnerPrefab
			{
				get
				{
					return this.result.hasOwnerPrefab;
				}
			}

			// Token: 0x170004D5 RID: 1237
			// (get) Token: 0x0600123C RID: 4668 RVA: 0x000420B8 File Offset: 0x000402B8
			// (set) Token: 0x0600123D RID: 4669 RVA: 0x000420C8 File Offset: 0x000402C8
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

			// Token: 0x0600123E RID: 4670 RVA: 0x000420D4 File Offset: 0x000402D4
			public objectNetInstance.Builder SetOwnerPrefab(int value)
			{
				this.PrepareBuilder();
				this.result.hasOwnerPrefab = true;
				this.result.ownerPrefab_ = value;
				return this;
			}

			// Token: 0x0600123F RID: 4671 RVA: 0x00042104 File Offset: 0x00040304
			public objectNetInstance.Builder ClearOwnerPrefab()
			{
				this.PrepareBuilder();
				this.result.hasOwnerPrefab = false;
				this.result.ownerPrefab_ = 0;
				return this;
			}

			// Token: 0x170004D6 RID: 1238
			// (get) Token: 0x06001240 RID: 4672 RVA: 0x00042134 File Offset: 0x00040334
			public bool HasProxyPrefab
			{
				get
				{
					return this.result.hasProxyPrefab;
				}
			}

			// Token: 0x170004D7 RID: 1239
			// (get) Token: 0x06001241 RID: 4673 RVA: 0x00042144 File Offset: 0x00040344
			// (set) Token: 0x06001242 RID: 4674 RVA: 0x00042154 File Offset: 0x00040354
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

			// Token: 0x06001243 RID: 4675 RVA: 0x00042160 File Offset: 0x00040360
			public objectNetInstance.Builder SetProxyPrefab(int value)
			{
				this.PrepareBuilder();
				this.result.hasProxyPrefab = true;
				this.result.proxyPrefab_ = value;
				return this;
			}

			// Token: 0x06001244 RID: 4676 RVA: 0x00042190 File Offset: 0x00040390
			public objectNetInstance.Builder ClearProxyPrefab()
			{
				this.PrepareBuilder();
				this.result.hasProxyPrefab = false;
				this.result.proxyPrefab_ = 0;
				return this;
			}

			// Token: 0x170004D8 RID: 1240
			// (get) Token: 0x06001245 RID: 4677 RVA: 0x000421C0 File Offset: 0x000403C0
			public bool HasGroupID
			{
				get
				{
					return this.result.hasGroupID;
				}
			}

			// Token: 0x170004D9 RID: 1241
			// (get) Token: 0x06001246 RID: 4678 RVA: 0x000421D0 File Offset: 0x000403D0
			// (set) Token: 0x06001247 RID: 4679 RVA: 0x000421E0 File Offset: 0x000403E0
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

			// Token: 0x06001248 RID: 4680 RVA: 0x000421EC File Offset: 0x000403EC
			public objectNetInstance.Builder SetGroupID(int value)
			{
				this.PrepareBuilder();
				this.result.hasGroupID = true;
				this.result.groupID_ = value;
				return this;
			}

			// Token: 0x06001249 RID: 4681 RVA: 0x0004221C File Offset: 0x0004041C
			public objectNetInstance.Builder ClearGroupID()
			{
				this.PrepareBuilder();
				this.result.hasGroupID = false;
				this.result.groupID_ = 0;
				return this;
			}

			// Token: 0x04000A72 RID: 2674
			private bool resultIsReadOnly;

			// Token: 0x04000A73 RID: 2675
			private objectNetInstance result;
		}
	}
}
