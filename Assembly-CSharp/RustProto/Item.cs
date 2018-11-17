using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Collections;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;
using RustProto.Helpers;
using RustProto.Proto;

namespace RustProto
{
	// Token: 0x02000201 RID: 513
	[DebuggerNonUserCode]
	public sealed class Item : GeneratedMessage<Item, Item.Builder>
	{
		// Token: 0x06000EE8 RID: 3816 RVA: 0x00038D18 File Offset: 0x00036F18
		private Item()
		{
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x00038D40 File Offset: 0x00036F40
		static Item()
		{
			object.ReferenceEquals(Item.Descriptor, null);
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x00038DCC File Offset: 0x00036FCC
		public static Recycler<Item, Item.Builder> Recycler()
		{
			return Recycler<Item, Item.Builder>.Manufacture();
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000EEB RID: 3819 RVA: 0x00038DD4 File Offset: 0x00036FD4
		public static Item DefaultInstance
		{
			get
			{
				return Item.defaultInstance;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000EEC RID: 3820 RVA: 0x00038DDC File Offset: 0x00036FDC
		public override Item DefaultInstanceForType
		{
			get
			{
				return Item.DefaultInstance;
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000EED RID: 3821 RVA: 0x00038DE4 File Offset: 0x00036FE4
		protected override Item ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000EEE RID: 3822 RVA: 0x00038DE8 File Offset: 0x00036FE8
		public static MessageDescriptor Descriptor
		{
			get
			{
				return Item.internal__static_RustProto_Item__Descriptor;
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000EEF RID: 3823 RVA: 0x00038DF0 File Offset: 0x00036FF0
		protected override FieldAccessorTable<Item, Item.Builder> InternalFieldAccessors
		{
			get
			{
				return Item.internal__static_RustProto_Item__FieldAccessorTable;
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000EF0 RID: 3824 RVA: 0x00038DF8 File Offset: 0x00036FF8
		public bool HasId
		{
			get
			{
				return this.hasId;
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000EF1 RID: 3825 RVA: 0x00038E00 File Offset: 0x00037000
		public int Id
		{
			get
			{
				return this.id_;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000EF2 RID: 3826 RVA: 0x00038E08 File Offset: 0x00037008
		public bool HasName
		{
			get
			{
				return this.hasName;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000EF3 RID: 3827 RVA: 0x00038E10 File Offset: 0x00037010
		public string Name
		{
			get
			{
				return this.name_;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000EF4 RID: 3828 RVA: 0x00038E18 File Offset: 0x00037018
		public bool HasSlot
		{
			get
			{
				return this.hasSlot;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000EF5 RID: 3829 RVA: 0x00038E20 File Offset: 0x00037020
		public int Slot
		{
			get
			{
				return this.slot_;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000EF6 RID: 3830 RVA: 0x00038E28 File Offset: 0x00037028
		public bool HasCount
		{
			get
			{
				return this.hasCount;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000EF7 RID: 3831 RVA: 0x00038E30 File Offset: 0x00037030
		public int Count
		{
			get
			{
				return this.count_;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000EF8 RID: 3832 RVA: 0x00038E38 File Offset: 0x00037038
		public bool HasSubslots
		{
			get
			{
				return this.hasSubslots;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000EF9 RID: 3833 RVA: 0x00038E40 File Offset: 0x00037040
		public int Subslots
		{
			get
			{
				return this.subslots_;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000EFA RID: 3834 RVA: 0x00038E48 File Offset: 0x00037048
		public bool HasCondition
		{
			get
			{
				return this.hasCondition;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000EFB RID: 3835 RVA: 0x00038E50 File Offset: 0x00037050
		public float Condition
		{
			get
			{
				return this.condition_;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000EFC RID: 3836 RVA: 0x00038E58 File Offset: 0x00037058
		public bool HasMaxcondition
		{
			get
			{
				return this.hasMaxcondition;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000EFD RID: 3837 RVA: 0x00038E60 File Offset: 0x00037060
		public float Maxcondition
		{
			get
			{
				return this.maxcondition_;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000EFE RID: 3838 RVA: 0x00038E68 File Offset: 0x00037068
		public IList<Item> SubitemList
		{
			get
			{
				return this.subitem_;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000EFF RID: 3839 RVA: 0x00038E70 File Offset: 0x00037070
		public int SubitemCount
		{
			get
			{
				return this.subitem_.Count;
			}
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x00038E80 File Offset: 0x00037080
		public Item GetSubitem(int index)
		{
			return this.subitem_[index];
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000F01 RID: 3841 RVA: 0x00038E90 File Offset: 0x00037090
		public override bool IsInitialized
		{
			get
			{
				if (!this.hasId)
				{
					return false;
				}
				foreach (Item item in this.SubitemList)
				{
					if (!item.IsInitialized)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x00038F10 File Offset: 0x00037110
		public override void WriteTo(ICodedOutputStream output)
		{
			int serializedSize = this.SerializedSize;
			string[] itemFieldNames = Item._itemFieldNames;
			if (this.hasId)
			{
				output.WriteInt32(1, itemFieldNames[2], this.Id);
			}
			if (this.hasName)
			{
				output.WriteString(2, itemFieldNames[4], this.Name);
			}
			if (this.hasSlot)
			{
				output.WriteInt32(3, itemFieldNames[5], this.Slot);
			}
			if (this.hasCount)
			{
				output.WriteInt32(4, itemFieldNames[1], this.Count);
			}
			if (this.subitem_.Count > 0)
			{
				output.WriteMessageArray<Item>(5, itemFieldNames[6], this.subitem_);
			}
			if (this.hasSubslots)
			{
				output.WriteInt32(6, itemFieldNames[7], this.Subslots);
			}
			if (this.hasCondition)
			{
				output.WriteFloat(7, itemFieldNames[0], this.Condition);
			}
			if (this.hasMaxcondition)
			{
				output.WriteFloat(8, itemFieldNames[3], this.Maxcondition);
			}
			this.UnknownFields.WriteTo(output);
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000F03 RID: 3843 RVA: 0x00039014 File Offset: 0x00037214
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
				if (this.hasId)
				{
					num += CodedOutputStream.ComputeInt32Size(1, this.Id);
				}
				if (this.hasName)
				{
					num += CodedOutputStream.ComputeStringSize(2, this.Name);
				}
				if (this.hasSlot)
				{
					num += CodedOutputStream.ComputeInt32Size(3, this.Slot);
				}
				if (this.hasCount)
				{
					num += CodedOutputStream.ComputeInt32Size(4, this.Count);
				}
				if (this.hasSubslots)
				{
					num += CodedOutputStream.ComputeInt32Size(6, this.Subslots);
				}
				if (this.hasCondition)
				{
					num += CodedOutputStream.ComputeFloatSize(7, this.Condition);
				}
				if (this.hasMaxcondition)
				{
					num += CodedOutputStream.ComputeFloatSize(8, this.Maxcondition);
				}
				foreach (Item item in this.SubitemList)
				{
					num += CodedOutputStream.ComputeMessageSize(5, item);
				}
				num += this.UnknownFields.SerializedSize;
				this.memoizedSerializedSize = num;
				return num;
			}
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x00039158 File Offset: 0x00037358
		public static Item ParseFrom(ByteString data)
		{
			return Item.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x0003916C File Offset: 0x0003736C
		public static Item ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return Item.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x00039180 File Offset: 0x00037380
		public static Item ParseFrom(byte[] data)
		{
			return Item.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x00039194 File Offset: 0x00037394
		public static Item ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return Item.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x000391A8 File Offset: 0x000373A8
		public static Item ParseFrom(Stream input)
		{
			return Item.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x000391BC File Offset: 0x000373BC
		public static Item ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return Item.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x000391D0 File Offset: 0x000373D0
		public static Item ParseDelimitedFrom(Stream input)
		{
			return Item.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x000391E4 File Offset: 0x000373E4
		public static Item ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return Item.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x000391F8 File Offset: 0x000373F8
		public static Item ParseFrom(ICodedInputStream input)
		{
			return Item.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x0003920C File Offset: 0x0003740C
		public static Item ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return Item.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x00039220 File Offset: 0x00037420
		private Item MakeReadOnly()
		{
			this.subitem_.MakeReadOnly();
			return this;
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x00039230 File Offset: 0x00037430
		public static Item.Builder CreateBuilder()
		{
			return new Item.Builder();
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x00039238 File Offset: 0x00037438
		public override Item.Builder ToBuilder()
		{
			return Item.CreateBuilder(this);
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x00039240 File Offset: 0x00037440
		public override Item.Builder CreateBuilderForType()
		{
			return new Item.Builder();
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x00039248 File Offset: 0x00037448
		public static Item.Builder CreateBuilder(Item prototype)
		{
			return new Item.Builder(prototype);
		}

		// Token: 0x040008C9 RID: 2249
		public const int IdFieldNumber = 1;

		// Token: 0x040008CA RID: 2250
		public const int NameFieldNumber = 2;

		// Token: 0x040008CB RID: 2251
		public const int SlotFieldNumber = 3;

		// Token: 0x040008CC RID: 2252
		public const int CountFieldNumber = 4;

		// Token: 0x040008CD RID: 2253
		public const int SubslotsFieldNumber = 6;

		// Token: 0x040008CE RID: 2254
		public const int ConditionFieldNumber = 7;

		// Token: 0x040008CF RID: 2255
		public const int MaxconditionFieldNumber = 8;

		// Token: 0x040008D0 RID: 2256
		public const int SubitemFieldNumber = 5;

		// Token: 0x040008D1 RID: 2257
		private static readonly Item defaultInstance = new Item().MakeReadOnly();

		// Token: 0x040008D2 RID: 2258
		private static readonly string[] _itemFieldNames = new string[]
		{
			"condition",
			"count",
			"id",
			"maxcondition",
			"name",
			"slot",
			"subitem",
			"subslots"
		};

		// Token: 0x040008D3 RID: 2259
		private static readonly uint[] _itemFieldTags = new uint[]
		{
			61u,
			32u,
			8u,
			69u,
			18u,
			24u,
			42u,
			48u
		};

		// Token: 0x040008D4 RID: 2260
		private bool hasId;

		// Token: 0x040008D5 RID: 2261
		private int id_;

		// Token: 0x040008D6 RID: 2262
		private bool hasName;

		// Token: 0x040008D7 RID: 2263
		private string name_ = string.Empty;

		// Token: 0x040008D8 RID: 2264
		private bool hasSlot;

		// Token: 0x040008D9 RID: 2265
		private int slot_;

		// Token: 0x040008DA RID: 2266
		private bool hasCount;

		// Token: 0x040008DB RID: 2267
		private int count_;

		// Token: 0x040008DC RID: 2268
		private bool hasSubslots;

		// Token: 0x040008DD RID: 2269
		private int subslots_;

		// Token: 0x040008DE RID: 2270
		private bool hasCondition;

		// Token: 0x040008DF RID: 2271
		private float condition_;

		// Token: 0x040008E0 RID: 2272
		private bool hasMaxcondition;

		// Token: 0x040008E1 RID: 2273
		private float maxcondition_;

		// Token: 0x040008E2 RID: 2274
		private PopsicleList<Item> subitem_ = new PopsicleList<Item>();

		// Token: 0x040008E3 RID: 2275
		private int memoizedSerializedSize = -1;

		// Token: 0x02000202 RID: 514
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<Item, Item.Builder>
		{
			// Token: 0x06000F13 RID: 3859 RVA: 0x00039250 File Offset: 0x00037450
			public Builder()
			{
				this.result = Item.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x06000F14 RID: 3860 RVA: 0x0003926C File Offset: 0x0003746C
			internal Builder(Item cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x170003D9 RID: 985
			// (get) Token: 0x06000F15 RID: 3861 RVA: 0x00039284 File Offset: 0x00037484
			protected override Item.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x06000F16 RID: 3862 RVA: 0x00039288 File Offset: 0x00037488
			private Item PrepareBuilder()
			{
				if (this.resultIsReadOnly)
				{
					Item other = this.result;
					this.result = new Item();
					this.resultIsReadOnly = false;
					this.MergeFrom(other);
				}
				return this.result;
			}

			// Token: 0x170003DA RID: 986
			// (get) Token: 0x06000F17 RID: 3863 RVA: 0x000392C8 File Offset: 0x000374C8
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x170003DB RID: 987
			// (get) Token: 0x06000F18 RID: 3864 RVA: 0x000392D8 File Offset: 0x000374D8
			protected override Item MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x06000F19 RID: 3865 RVA: 0x000392E0 File Offset: 0x000374E0
			public override Item.Builder Clear()
			{
				this.result = Item.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x06000F1A RID: 3866 RVA: 0x000392F8 File Offset: 0x000374F8
			public override Item.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new Item.Builder(this.result);
				}
				return new Item.Builder().MergeFrom(this.result);
			}

			// Token: 0x170003DC RID: 988
			// (get) Token: 0x06000F1B RID: 3867 RVA: 0x00039324 File Offset: 0x00037524
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return Item.Descriptor;
				}
			}

			// Token: 0x170003DD RID: 989
			// (get) Token: 0x06000F1C RID: 3868 RVA: 0x0003932C File Offset: 0x0003752C
			public override Item DefaultInstanceForType
			{
				get
				{
					return Item.DefaultInstance;
				}
			}

			// Token: 0x06000F1D RID: 3869 RVA: 0x00039334 File Offset: 0x00037534
			public override Item BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x06000F1E RID: 3870 RVA: 0x00039368 File Offset: 0x00037568
			public override Item.Builder MergeFrom(IMessage other)
			{
				if (other is Item)
				{
					return this.MergeFrom((Item)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x06000F1F RID: 3871 RVA: 0x0003938C File Offset: 0x0003758C
			public override Item.Builder MergeFrom(Item other)
			{
				if (other == Item.DefaultInstance)
				{
					return this;
				}
				this.PrepareBuilder();
				if (other.HasId)
				{
					this.Id = other.Id;
				}
				if (other.HasName)
				{
					this.Name = other.Name;
				}
				if (other.HasSlot)
				{
					this.Slot = other.Slot;
				}
				if (other.HasCount)
				{
					this.Count = other.Count;
				}
				if (other.HasSubslots)
				{
					this.Subslots = other.Subslots;
				}
				if (other.HasCondition)
				{
					this.Condition = other.Condition;
				}
				if (other.HasMaxcondition)
				{
					this.Maxcondition = other.Maxcondition;
				}
				if (other.subitem_.Count != 0)
				{
					this.result.subitem_.Add(other.subitem_);
				}
				this.MergeUnknownFields(other.UnknownFields);
				return this;
			}

			// Token: 0x06000F20 RID: 3872 RVA: 0x00039484 File Offset: 0x00037684
			public override Item.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x06000F21 RID: 3873 RVA: 0x00039494 File Offset: 0x00037694
			public override Item.Builder MergeFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
			{
				this.PrepareBuilder();
				UnknownFieldSet.Builder builder = null;
				uint num;
				string text;
				while (input.ReadTag(ref num, ref text))
				{
					if (num == 0u && text != null)
					{
						int num2 = Array.BinarySearch<string>(Item._itemFieldNames, text, StringComparer.Ordinal);
						if (num2 < 0)
						{
							if (builder == null)
							{
								builder = UnknownFieldSet.CreateBuilder(this.UnknownFields);
							}
							this.ParseUnknownField(input, builder, extensionRegistry, num, text);
							continue;
						}
						num = Item._itemFieldTags[num2];
					}
					uint num3 = num;
					if (num3 == 0u)
					{
						throw InvalidProtocolBufferException.InvalidTag();
					}
					if (num3 != 8u)
					{
						if (num3 != 18u)
						{
							if (num3 != 24u)
							{
								if (num3 != 32u)
								{
									if (num3 != 42u)
									{
										if (num3 != 48u)
										{
											if (num3 != 61u)
											{
												if (num3 != 69u)
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
													this.result.hasMaxcondition = input.ReadFloat(ref this.result.maxcondition_);
												}
											}
											else
											{
												this.result.hasCondition = input.ReadFloat(ref this.result.condition_);
											}
										}
										else
										{
											this.result.hasSubslots = input.ReadInt32(ref this.result.subslots_);
										}
									}
									else
									{
										input.ReadMessageArray<Item>(num, text, this.result.subitem_, Item.DefaultInstance, extensionRegistry);
									}
								}
								else
								{
									this.result.hasCount = input.ReadInt32(ref this.result.count_);
								}
							}
							else
							{
								this.result.hasSlot = input.ReadInt32(ref this.result.slot_);
							}
						}
						else
						{
							this.result.hasName = input.ReadString(ref this.result.name_);
						}
					}
					else
					{
						this.result.hasId = input.ReadInt32(ref this.result.id_);
					}
				}
				if (builder != null)
				{
					this.UnknownFields = builder.Build();
				}
				return this;
			}

			// Token: 0x170003DE RID: 990
			// (get) Token: 0x06000F22 RID: 3874 RVA: 0x000396CC File Offset: 0x000378CC
			public bool HasId
			{
				get
				{
					return this.result.hasId;
				}
			}

			// Token: 0x170003DF RID: 991
			// (get) Token: 0x06000F23 RID: 3875 RVA: 0x000396DC File Offset: 0x000378DC
			// (set) Token: 0x06000F24 RID: 3876 RVA: 0x000396EC File Offset: 0x000378EC
			public int Id
			{
				get
				{
					return this.result.Id;
				}
				set
				{
					this.SetId(value);
				}
			}

			// Token: 0x06000F25 RID: 3877 RVA: 0x000396F8 File Offset: 0x000378F8
			public Item.Builder SetId(int value)
			{
				this.PrepareBuilder();
				this.result.hasId = true;
				this.result.id_ = value;
				return this;
			}

			// Token: 0x06000F26 RID: 3878 RVA: 0x00039728 File Offset: 0x00037928
			public Item.Builder ClearId()
			{
				this.PrepareBuilder();
				this.result.hasId = false;
				this.result.id_ = 0;
				return this;
			}

			// Token: 0x170003E0 RID: 992
			// (get) Token: 0x06000F27 RID: 3879 RVA: 0x00039758 File Offset: 0x00037958
			public bool HasName
			{
				get
				{
					return this.result.hasName;
				}
			}

			// Token: 0x170003E1 RID: 993
			// (get) Token: 0x06000F28 RID: 3880 RVA: 0x00039768 File Offset: 0x00037968
			// (set) Token: 0x06000F29 RID: 3881 RVA: 0x00039778 File Offset: 0x00037978
			public string Name
			{
				get
				{
					return this.result.Name;
				}
				set
				{
					this.SetName(value);
				}
			}

			// Token: 0x06000F2A RID: 3882 RVA: 0x00039784 File Offset: 0x00037984
			public Item.Builder SetName(string value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasName = true;
				this.result.name_ = value;
				return this;
			}

			// Token: 0x06000F2B RID: 3883 RVA: 0x000397B4 File Offset: 0x000379B4
			public Item.Builder ClearName()
			{
				this.PrepareBuilder();
				this.result.hasName = false;
				this.result.name_ = string.Empty;
				return this;
			}

			// Token: 0x170003E2 RID: 994
			// (get) Token: 0x06000F2C RID: 3884 RVA: 0x000397E8 File Offset: 0x000379E8
			public bool HasSlot
			{
				get
				{
					return this.result.hasSlot;
				}
			}

			// Token: 0x170003E3 RID: 995
			// (get) Token: 0x06000F2D RID: 3885 RVA: 0x000397F8 File Offset: 0x000379F8
			// (set) Token: 0x06000F2E RID: 3886 RVA: 0x00039808 File Offset: 0x00037A08
			public int Slot
			{
				get
				{
					return this.result.Slot;
				}
				set
				{
					this.SetSlot(value);
				}
			}

			// Token: 0x06000F2F RID: 3887 RVA: 0x00039814 File Offset: 0x00037A14
			public Item.Builder SetSlot(int value)
			{
				this.PrepareBuilder();
				this.result.hasSlot = true;
				this.result.slot_ = value;
				return this;
			}

			// Token: 0x06000F30 RID: 3888 RVA: 0x00039844 File Offset: 0x00037A44
			public Item.Builder ClearSlot()
			{
				this.PrepareBuilder();
				this.result.hasSlot = false;
				this.result.slot_ = 0;
				return this;
			}

			// Token: 0x170003E4 RID: 996
			// (get) Token: 0x06000F31 RID: 3889 RVA: 0x00039874 File Offset: 0x00037A74
			public bool HasCount
			{
				get
				{
					return this.result.hasCount;
				}
			}

			// Token: 0x170003E5 RID: 997
			// (get) Token: 0x06000F32 RID: 3890 RVA: 0x00039884 File Offset: 0x00037A84
			// (set) Token: 0x06000F33 RID: 3891 RVA: 0x00039894 File Offset: 0x00037A94
			public int Count
			{
				get
				{
					return this.result.Count;
				}
				set
				{
					this.SetCount(value);
				}
			}

			// Token: 0x06000F34 RID: 3892 RVA: 0x000398A0 File Offset: 0x00037AA0
			public Item.Builder SetCount(int value)
			{
				this.PrepareBuilder();
				this.result.hasCount = true;
				this.result.count_ = value;
				return this;
			}

			// Token: 0x06000F35 RID: 3893 RVA: 0x000398D0 File Offset: 0x00037AD0
			public Item.Builder ClearCount()
			{
				this.PrepareBuilder();
				this.result.hasCount = false;
				this.result.count_ = 0;
				return this;
			}

			// Token: 0x170003E6 RID: 998
			// (get) Token: 0x06000F36 RID: 3894 RVA: 0x00039900 File Offset: 0x00037B00
			public bool HasSubslots
			{
				get
				{
					return this.result.hasSubslots;
				}
			}

			// Token: 0x170003E7 RID: 999
			// (get) Token: 0x06000F37 RID: 3895 RVA: 0x00039910 File Offset: 0x00037B10
			// (set) Token: 0x06000F38 RID: 3896 RVA: 0x00039920 File Offset: 0x00037B20
			public int Subslots
			{
				get
				{
					return this.result.Subslots;
				}
				set
				{
					this.SetSubslots(value);
				}
			}

			// Token: 0x06000F39 RID: 3897 RVA: 0x0003992C File Offset: 0x00037B2C
			public Item.Builder SetSubslots(int value)
			{
				this.PrepareBuilder();
				this.result.hasSubslots = true;
				this.result.subslots_ = value;
				return this;
			}

			// Token: 0x06000F3A RID: 3898 RVA: 0x0003995C File Offset: 0x00037B5C
			public Item.Builder ClearSubslots()
			{
				this.PrepareBuilder();
				this.result.hasSubslots = false;
				this.result.subslots_ = 0;
				return this;
			}

			// Token: 0x170003E8 RID: 1000
			// (get) Token: 0x06000F3B RID: 3899 RVA: 0x0003998C File Offset: 0x00037B8C
			public bool HasCondition
			{
				get
				{
					return this.result.hasCondition;
				}
			}

			// Token: 0x170003E9 RID: 1001
			// (get) Token: 0x06000F3C RID: 3900 RVA: 0x0003999C File Offset: 0x00037B9C
			// (set) Token: 0x06000F3D RID: 3901 RVA: 0x000399AC File Offset: 0x00037BAC
			public float Condition
			{
				get
				{
					return this.result.Condition;
				}
				set
				{
					this.SetCondition(value);
				}
			}

			// Token: 0x06000F3E RID: 3902 RVA: 0x000399B8 File Offset: 0x00037BB8
			public Item.Builder SetCondition(float value)
			{
				this.PrepareBuilder();
				this.result.hasCondition = true;
				this.result.condition_ = value;
				return this;
			}

			// Token: 0x06000F3F RID: 3903 RVA: 0x000399E8 File Offset: 0x00037BE8
			public Item.Builder ClearCondition()
			{
				this.PrepareBuilder();
				this.result.hasCondition = false;
				this.result.condition_ = 0f;
				return this;
			}

			// Token: 0x170003EA RID: 1002
			// (get) Token: 0x06000F40 RID: 3904 RVA: 0x00039A1C File Offset: 0x00037C1C
			public bool HasMaxcondition
			{
				get
				{
					return this.result.hasMaxcondition;
				}
			}

			// Token: 0x170003EB RID: 1003
			// (get) Token: 0x06000F41 RID: 3905 RVA: 0x00039A2C File Offset: 0x00037C2C
			// (set) Token: 0x06000F42 RID: 3906 RVA: 0x00039A3C File Offset: 0x00037C3C
			public float Maxcondition
			{
				get
				{
					return this.result.Maxcondition;
				}
				set
				{
					this.SetMaxcondition(value);
				}
			}

			// Token: 0x06000F43 RID: 3907 RVA: 0x00039A48 File Offset: 0x00037C48
			public Item.Builder SetMaxcondition(float value)
			{
				this.PrepareBuilder();
				this.result.hasMaxcondition = true;
				this.result.maxcondition_ = value;
				return this;
			}

			// Token: 0x06000F44 RID: 3908 RVA: 0x00039A78 File Offset: 0x00037C78
			public Item.Builder ClearMaxcondition()
			{
				this.PrepareBuilder();
				this.result.hasMaxcondition = false;
				this.result.maxcondition_ = 0f;
				return this;
			}

			// Token: 0x170003EC RID: 1004
			// (get) Token: 0x06000F45 RID: 3909 RVA: 0x00039AAC File Offset: 0x00037CAC
			public IPopsicleList<Item> SubitemList
			{
				get
				{
					return this.PrepareBuilder().subitem_;
				}
			}

			// Token: 0x170003ED RID: 1005
			// (get) Token: 0x06000F46 RID: 3910 RVA: 0x00039ABC File Offset: 0x00037CBC
			public int SubitemCount
			{
				get
				{
					return this.result.SubitemCount;
				}
			}

			// Token: 0x06000F47 RID: 3911 RVA: 0x00039ACC File Offset: 0x00037CCC
			public Item GetSubitem(int index)
			{
				return this.result.GetSubitem(index);
			}

			// Token: 0x06000F48 RID: 3912 RVA: 0x00039ADC File Offset: 0x00037CDC
			public Item.Builder SetSubitem(int index, Item value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.subitem_[index] = value;
				return this;
			}

			// Token: 0x06000F49 RID: 3913 RVA: 0x00039B04 File Offset: 0x00037D04
			public Item.Builder SetSubitem(int index, Item.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.subitem_[index] = builderForValue.Build();
				return this;
			}

			// Token: 0x06000F4A RID: 3914 RVA: 0x00039B3C File Offset: 0x00037D3C
			public Item.Builder AddSubitem(Item value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.subitem_.Add(value);
				return this;
			}

			// Token: 0x06000F4B RID: 3915 RVA: 0x00039B70 File Offset: 0x00037D70
			public Item.Builder AddSubitem(Item.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.subitem_.Add(builderForValue.Build());
				return this;
			}

			// Token: 0x06000F4C RID: 3916 RVA: 0x00039B9C File Offset: 0x00037D9C
			public Item.Builder AddRangeSubitem(IEnumerable<Item> values)
			{
				this.PrepareBuilder();
				this.result.subitem_.Add(values);
				return this;
			}

			// Token: 0x06000F4D RID: 3917 RVA: 0x00039BB8 File Offset: 0x00037DB8
			public Item.Builder ClearSubitem()
			{
				this.PrepareBuilder();
				this.result.subitem_.Clear();
				return this;
			}

			// Token: 0x040008E4 RID: 2276
			private bool resultIsReadOnly;

			// Token: 0x040008E5 RID: 2277
			private Item result;
		}
	}
}
