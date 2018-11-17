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
	// Token: 0x020001FD RID: 509
	[DebuggerNonUserCode]
	public sealed class Avatar : GeneratedMessage<Avatar, Avatar.Builder>
	{
		// Token: 0x06000DFC RID: 3580 RVA: 0x0003666C File Offset: 0x0003486C
		private Avatar()
		{
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x000366A8 File Offset: 0x000348A8
		static Avatar()
		{
			object.ReferenceEquals(Avatar.Descriptor, null);
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x00036734 File Offset: 0x00034934
		public static Recycler<Avatar, Avatar.Builder> Recycler()
		{
			return Recycler<Avatar, Avatar.Builder>.Manufacture();
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000DFF RID: 3583 RVA: 0x0003673C File Offset: 0x0003493C
		public static Avatar DefaultInstance
		{
			get
			{
				return Avatar.defaultInstance;
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000E00 RID: 3584 RVA: 0x00036744 File Offset: 0x00034944
		public override Avatar DefaultInstanceForType
		{
			get
			{
				return Avatar.DefaultInstance;
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000E01 RID: 3585 RVA: 0x0003674C File Offset: 0x0003494C
		protected override Avatar ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000E02 RID: 3586 RVA: 0x00036750 File Offset: 0x00034950
		public static MessageDescriptor Descriptor
		{
			get
			{
				return Avatar.internal__static_RustProto_Avatar__Descriptor;
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000E03 RID: 3587 RVA: 0x00036758 File Offset: 0x00034958
		protected override FieldAccessorTable<Avatar, Avatar.Builder> InternalFieldAccessors
		{
			get
			{
				return Avatar.internal__static_RustProto_Avatar__FieldAccessorTable;
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000E04 RID: 3588 RVA: 0x00036760 File Offset: 0x00034960
		public bool HasPos
		{
			get
			{
				return this.hasPos;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000E05 RID: 3589 RVA: 0x00036768 File Offset: 0x00034968
		public Vector Pos
		{
			get
			{
				return this.pos_ ?? Vector.DefaultInstance;
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000E06 RID: 3590 RVA: 0x0003677C File Offset: 0x0003497C
		public bool HasAng
		{
			get
			{
				return this.hasAng;
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000E07 RID: 3591 RVA: 0x00036784 File Offset: 0x00034984
		public Quaternion Ang
		{
			get
			{
				return this.ang_ ?? Quaternion.DefaultInstance;
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000E08 RID: 3592 RVA: 0x00036798 File Offset: 0x00034998
		public bool HasVitals
		{
			get
			{
				return this.hasVitals;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000E09 RID: 3593 RVA: 0x000367A0 File Offset: 0x000349A0
		public Vitals Vitals
		{
			get
			{
				return this.vitals_ ?? Vitals.DefaultInstance;
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000E0A RID: 3594 RVA: 0x000367B4 File Offset: 0x000349B4
		public IList<Blueprint> BlueprintsList
		{
			get
			{
				return this.blueprints_;
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000E0B RID: 3595 RVA: 0x000367BC File Offset: 0x000349BC
		public int BlueprintsCount
		{
			get
			{
				return this.blueprints_.Count;
			}
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x000367CC File Offset: 0x000349CC
		public Blueprint GetBlueprints(int index)
		{
			return this.blueprints_[index];
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000E0D RID: 3597 RVA: 0x000367DC File Offset: 0x000349DC
		public IList<Item> InventoryList
		{
			get
			{
				return this.inventory_;
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000E0E RID: 3598 RVA: 0x000367E4 File Offset: 0x000349E4
		public int InventoryCount
		{
			get
			{
				return this.inventory_.Count;
			}
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x000367F4 File Offset: 0x000349F4
		public Item GetInventory(int index)
		{
			return this.inventory_[index];
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000E10 RID: 3600 RVA: 0x00036804 File Offset: 0x00034A04
		public IList<Item> WearableList
		{
			get
			{
				return this.wearable_;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000E11 RID: 3601 RVA: 0x0003680C File Offset: 0x00034A0C
		public int WearableCount
		{
			get
			{
				return this.wearable_.Count;
			}
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x0003681C File Offset: 0x00034A1C
		public Item GetWearable(int index)
		{
			return this.wearable_[index];
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000E13 RID: 3603 RVA: 0x0003682C File Offset: 0x00034A2C
		public IList<Item> BeltList
		{
			get
			{
				return this.belt_;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000E14 RID: 3604 RVA: 0x00036834 File Offset: 0x00034A34
		public int BeltCount
		{
			get
			{
				return this.belt_.Count;
			}
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x00036844 File Offset: 0x00034A44
		public Item GetBelt(int index)
		{
			return this.belt_[index];
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000E16 RID: 3606 RVA: 0x00036854 File Offset: 0x00034A54
		public bool HasAwayEvent
		{
			get
			{
				return this.hasAwayEvent;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000E17 RID: 3607 RVA: 0x0003685C File Offset: 0x00034A5C
		public AwayEvent AwayEvent
		{
			get
			{
				return this.awayEvent_ ?? AwayEvent.DefaultInstance;
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000E18 RID: 3608 RVA: 0x00036870 File Offset: 0x00034A70
		public override bool IsInitialized
		{
			get
			{
				foreach (Blueprint blueprint in this.BlueprintsList)
				{
					if (!blueprint.IsInitialized)
					{
						return false;
					}
				}
				foreach (Item item in this.InventoryList)
				{
					if (!item.IsInitialized)
					{
						return false;
					}
				}
				foreach (Item item2 in this.WearableList)
				{
					if (!item2.IsInitialized)
					{
						return false;
					}
				}
				foreach (Item item3 in this.BeltList)
				{
					if (!item3.IsInitialized)
					{
						return false;
					}
				}
				return !this.HasAwayEvent || this.AwayEvent.IsInitialized;
			}
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x00036A28 File Offset: 0x00034C28
		public override void WriteTo(ICodedOutputStream output)
		{
			int serializedSize = this.SerializedSize;
			string[] avatarFieldNames = Avatar._avatarFieldNames;
			if (this.hasPos)
			{
				output.WriteMessage(1, avatarFieldNames[5], this.Pos);
			}
			if (this.hasAng)
			{
				output.WriteMessage(2, avatarFieldNames[0], this.Ang);
			}
			if (this.hasVitals)
			{
				output.WriteMessage(3, avatarFieldNames[6], this.Vitals);
			}
			if (this.blueprints_.Count > 0)
			{
				output.WriteMessageArray<Blueprint>(4, avatarFieldNames[3], this.blueprints_);
			}
			if (this.inventory_.Count > 0)
			{
				output.WriteMessageArray<Item>(5, avatarFieldNames[4], this.inventory_);
			}
			if (this.wearable_.Count > 0)
			{
				output.WriteMessageArray<Item>(6, avatarFieldNames[7], this.wearable_);
			}
			if (this.belt_.Count > 0)
			{
				output.WriteMessageArray<Item>(7, avatarFieldNames[2], this.belt_);
			}
			if (this.hasAwayEvent)
			{
				output.WriteMessage(8, avatarFieldNames[1], this.AwayEvent);
			}
			this.UnknownFields.WriteTo(output);
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000E1A RID: 3610 RVA: 0x00036B40 File Offset: 0x00034D40
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
				if (this.hasPos)
				{
					num += CodedOutputStream.ComputeMessageSize(1, this.Pos);
				}
				if (this.hasAng)
				{
					num += CodedOutputStream.ComputeMessageSize(2, this.Ang);
				}
				if (this.hasVitals)
				{
					num += CodedOutputStream.ComputeMessageSize(3, this.Vitals);
				}
				foreach (Blueprint blueprint in this.BlueprintsList)
				{
					num += CodedOutputStream.ComputeMessageSize(4, blueprint);
				}
				foreach (Item item in this.InventoryList)
				{
					num += CodedOutputStream.ComputeMessageSize(5, item);
				}
				foreach (Item item2 in this.WearableList)
				{
					num += CodedOutputStream.ComputeMessageSize(6, item2);
				}
				foreach (Item item3 in this.BeltList)
				{
					num += CodedOutputStream.ComputeMessageSize(7, item3);
				}
				if (this.hasAwayEvent)
				{
					num += CodedOutputStream.ComputeMessageSize(8, this.AwayEvent);
				}
				num += this.UnknownFields.SerializedSize;
				this.memoizedSerializedSize = num;
				return num;
			}
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x00036D48 File Offset: 0x00034F48
		public static Avatar ParseFrom(ByteString data)
		{
			return Avatar.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x00036D5C File Offset: 0x00034F5C
		public static Avatar ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return Avatar.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x00036D70 File Offset: 0x00034F70
		public static Avatar ParseFrom(byte[] data)
		{
			return Avatar.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x00036D84 File Offset: 0x00034F84
		public static Avatar ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return Avatar.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x00036D98 File Offset: 0x00034F98
		public static Avatar ParseFrom(Stream input)
		{
			return Avatar.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x00036DAC File Offset: 0x00034FAC
		public static Avatar ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return Avatar.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x00036DC0 File Offset: 0x00034FC0
		public static Avatar ParseDelimitedFrom(Stream input)
		{
			return Avatar.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x00036DD4 File Offset: 0x00034FD4
		public static Avatar ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return Avatar.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x00036DE8 File Offset: 0x00034FE8
		public static Avatar ParseFrom(ICodedInputStream input)
		{
			return Avatar.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x00036DFC File Offset: 0x00034FFC
		public static Avatar ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return Avatar.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x00036E10 File Offset: 0x00035010
		private Avatar MakeReadOnly()
		{
			this.blueprints_.MakeReadOnly();
			this.inventory_.MakeReadOnly();
			this.wearable_.MakeReadOnly();
			this.belt_.MakeReadOnly();
			return this;
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x00036E4C File Offset: 0x0003504C
		public static Avatar.Builder CreateBuilder()
		{
			return new Avatar.Builder();
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x00036E54 File Offset: 0x00035054
		public override Avatar.Builder ToBuilder()
		{
			return Avatar.CreateBuilder(this);
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x00036E5C File Offset: 0x0003505C
		public override Avatar.Builder CreateBuilderForType()
		{
			return new Avatar.Builder();
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x00036E64 File Offset: 0x00035064
		public static Avatar.Builder CreateBuilder(Avatar prototype)
		{
			return new Avatar.Builder(prototype);
		}

		// Token: 0x0400088B RID: 2187
		public const int PosFieldNumber = 1;

		// Token: 0x0400088C RID: 2188
		public const int AngFieldNumber = 2;

		// Token: 0x0400088D RID: 2189
		public const int VitalsFieldNumber = 3;

		// Token: 0x0400088E RID: 2190
		public const int BlueprintsFieldNumber = 4;

		// Token: 0x0400088F RID: 2191
		public const int InventoryFieldNumber = 5;

		// Token: 0x04000890 RID: 2192
		public const int WearableFieldNumber = 6;

		// Token: 0x04000891 RID: 2193
		public const int BeltFieldNumber = 7;

		// Token: 0x04000892 RID: 2194
		public const int AwayEventFieldNumber = 8;

		// Token: 0x04000893 RID: 2195
		private static readonly Avatar defaultInstance = new Avatar().MakeReadOnly();

		// Token: 0x04000894 RID: 2196
		private static readonly string[] _avatarFieldNames = new string[]
		{
			"ang",
			"awayEvent",
			"belt",
			"blueprints",
			"inventory",
			"pos",
			"vitals",
			"wearable"
		};

		// Token: 0x04000895 RID: 2197
		private static readonly uint[] _avatarFieldTags = new uint[]
		{
			18u,
			66u,
			58u,
			34u,
			42u,
			10u,
			26u,
			50u
		};

		// Token: 0x04000896 RID: 2198
		private bool hasPos;

		// Token: 0x04000897 RID: 2199
		private Vector pos_;

		// Token: 0x04000898 RID: 2200
		private bool hasAng;

		// Token: 0x04000899 RID: 2201
		private Quaternion ang_;

		// Token: 0x0400089A RID: 2202
		private bool hasVitals;

		// Token: 0x0400089B RID: 2203
		private Vitals vitals_;

		// Token: 0x0400089C RID: 2204
		private PopsicleList<Blueprint> blueprints_ = new PopsicleList<Blueprint>();

		// Token: 0x0400089D RID: 2205
		private PopsicleList<Item> inventory_ = new PopsicleList<Item>();

		// Token: 0x0400089E RID: 2206
		private PopsicleList<Item> wearable_ = new PopsicleList<Item>();

		// Token: 0x0400089F RID: 2207
		private PopsicleList<Item> belt_ = new PopsicleList<Item>();

		// Token: 0x040008A0 RID: 2208
		private bool hasAwayEvent;

		// Token: 0x040008A1 RID: 2209
		private AwayEvent awayEvent_;

		// Token: 0x040008A2 RID: 2210
		private int memoizedSerializedSize = -1;

		// Token: 0x020001FE RID: 510
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<Avatar, Avatar.Builder>
		{
			// Token: 0x06000E2A RID: 3626 RVA: 0x00036E6C File Offset: 0x0003506C
			public Builder()
			{
				this.result = Avatar.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x06000E2B RID: 3627 RVA: 0x00036E88 File Offset: 0x00035088
			internal Builder(Avatar cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x17000379 RID: 889
			// (get) Token: 0x06000E2C RID: 3628 RVA: 0x00036EA0 File Offset: 0x000350A0
			protected override Avatar.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x06000E2D RID: 3629 RVA: 0x00036EA4 File Offset: 0x000350A4
			private Avatar PrepareBuilder()
			{
				if (this.resultIsReadOnly)
				{
					Avatar other = this.result;
					this.result = new Avatar();
					this.resultIsReadOnly = false;
					this.MergeFrom(other);
				}
				return this.result;
			}

			// Token: 0x1700037A RID: 890
			// (get) Token: 0x06000E2E RID: 3630 RVA: 0x00036EE4 File Offset: 0x000350E4
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x1700037B RID: 891
			// (get) Token: 0x06000E2F RID: 3631 RVA: 0x00036EF4 File Offset: 0x000350F4
			protected override Avatar MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x06000E30 RID: 3632 RVA: 0x00036EFC File Offset: 0x000350FC
			public override Avatar.Builder Clear()
			{
				this.result = Avatar.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x06000E31 RID: 3633 RVA: 0x00036F14 File Offset: 0x00035114
			public override Avatar.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new Avatar.Builder(this.result);
				}
				return new Avatar.Builder().MergeFrom(this.result);
			}

			// Token: 0x1700037C RID: 892
			// (get) Token: 0x06000E32 RID: 3634 RVA: 0x00036F40 File Offset: 0x00035140
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return Avatar.Descriptor;
				}
			}

			// Token: 0x1700037D RID: 893
			// (get) Token: 0x06000E33 RID: 3635 RVA: 0x00036F48 File Offset: 0x00035148
			public override Avatar DefaultInstanceForType
			{
				get
				{
					return Avatar.DefaultInstance;
				}
			}

			// Token: 0x06000E34 RID: 3636 RVA: 0x00036F50 File Offset: 0x00035150
			public override Avatar BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x06000E35 RID: 3637 RVA: 0x00036F84 File Offset: 0x00035184
			public override Avatar.Builder MergeFrom(IMessage other)
			{
				if (other is Avatar)
				{
					return this.MergeFrom((Avatar)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x06000E36 RID: 3638 RVA: 0x00036FA8 File Offset: 0x000351A8
			public override Avatar.Builder MergeFrom(Avatar other)
			{
				if (other == Avatar.DefaultInstance)
				{
					return this;
				}
				this.PrepareBuilder();
				if (other.HasPos)
				{
					this.MergePos(other.Pos);
				}
				if (other.HasAng)
				{
					this.MergeAng(other.Ang);
				}
				if (other.HasVitals)
				{
					this.MergeVitals(other.Vitals);
				}
				if (other.blueprints_.Count != 0)
				{
					this.result.blueprints_.Add(other.blueprints_);
				}
				if (other.inventory_.Count != 0)
				{
					this.result.inventory_.Add(other.inventory_);
				}
				if (other.wearable_.Count != 0)
				{
					this.result.wearable_.Add(other.wearable_);
				}
				if (other.belt_.Count != 0)
				{
					this.result.belt_.Add(other.belt_);
				}
				if (other.HasAwayEvent)
				{
					this.MergeAwayEvent(other.AwayEvent);
				}
				this.MergeUnknownFields(other.UnknownFields);
				return this;
			}

			// Token: 0x06000E37 RID: 3639 RVA: 0x000370D0 File Offset: 0x000352D0
			public override Avatar.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x06000E38 RID: 3640 RVA: 0x000370E0 File Offset: 0x000352E0
			public override Avatar.Builder MergeFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
			{
				this.PrepareBuilder();
				UnknownFieldSet.Builder builder = null;
				uint num;
				string text;
				while (input.ReadTag(ref num, ref text))
				{
					if (num == 0u && text != null)
					{
						int num2 = Array.BinarySearch<string>(Avatar._avatarFieldNames, text, StringComparer.Ordinal);
						if (num2 < 0)
						{
							if (builder == null)
							{
								builder = UnknownFieldSet.CreateBuilder(this.UnknownFields);
							}
							this.ParseUnknownField(input, builder, extensionRegistry, num, text);
							continue;
						}
						num = Avatar._avatarFieldTags[num2];
					}
					uint num3 = num;
					if (num3 == 0u)
					{
						throw InvalidProtocolBufferException.InvalidTag();
					}
					if (num3 != 10u)
					{
						if (num3 != 18u)
						{
							if (num3 != 26u)
							{
								if (num3 != 34u)
								{
									if (num3 != 42u)
									{
										if (num3 != 50u)
										{
											if (num3 != 58u)
											{
												if (num3 != 66u)
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
													AwayEvent.Builder builder2 = AwayEvent.CreateBuilder();
													if (this.result.hasAwayEvent)
													{
														builder2.MergeFrom(this.AwayEvent);
													}
													input.ReadMessage(builder2, extensionRegistry);
													this.AwayEvent = builder2.BuildPartial();
												}
											}
											else
											{
												input.ReadMessageArray<Item>(num, text, this.result.belt_, Item.DefaultInstance, extensionRegistry);
											}
										}
										else
										{
											input.ReadMessageArray<Item>(num, text, this.result.wearable_, Item.DefaultInstance, extensionRegistry);
										}
									}
									else
									{
										input.ReadMessageArray<Item>(num, text, this.result.inventory_, Item.DefaultInstance, extensionRegistry);
									}
								}
								else
								{
									input.ReadMessageArray<Blueprint>(num, text, this.result.blueprints_, Blueprint.DefaultInstance, extensionRegistry);
								}
							}
							else
							{
								Vitals.Builder builder3 = Vitals.CreateBuilder();
								if (this.result.hasVitals)
								{
									builder3.MergeFrom(this.Vitals);
								}
								input.ReadMessage(builder3, extensionRegistry);
								this.Vitals = builder3.BuildPartial();
							}
						}
						else
						{
							Quaternion.Builder builder4 = Quaternion.CreateBuilder();
							if (this.result.hasAng)
							{
								builder4.MergeFrom(this.Ang);
							}
							input.ReadMessage(builder4, extensionRegistry);
							this.Ang = builder4.BuildPartial();
						}
					}
					else
					{
						Vector.Builder builder5 = Vector.CreateBuilder();
						if (this.result.hasPos)
						{
							builder5.MergeFrom(this.Pos);
						}
						input.ReadMessage(builder5, extensionRegistry);
						this.Pos = builder5.BuildPartial();
					}
				}
				if (builder != null)
				{
					this.UnknownFields = builder.Build();
				}
				return this;
			}

			// Token: 0x1700037E RID: 894
			// (get) Token: 0x06000E39 RID: 3641 RVA: 0x0003738C File Offset: 0x0003558C
			public bool HasPos
			{
				get
				{
					return this.result.hasPos;
				}
			}

			// Token: 0x1700037F RID: 895
			// (get) Token: 0x06000E3A RID: 3642 RVA: 0x0003739C File Offset: 0x0003559C
			// (set) Token: 0x06000E3B RID: 3643 RVA: 0x000373AC File Offset: 0x000355AC
			public Vector Pos
			{
				get
				{
					return this.result.Pos;
				}
				set
				{
					this.SetPos(value);
				}
			}

			// Token: 0x06000E3C RID: 3644 RVA: 0x000373B8 File Offset: 0x000355B8
			public Avatar.Builder SetPos(Vector value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasPos = true;
				this.result.pos_ = value;
				return this;
			}

			// Token: 0x06000E3D RID: 3645 RVA: 0x000373E8 File Offset: 0x000355E8
			public Avatar.Builder SetPos(Vector.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.hasPos = true;
				this.result.pos_ = builderForValue.Build();
				return this;
			}

			// Token: 0x06000E3E RID: 3646 RVA: 0x00037428 File Offset: 0x00035628
			public Avatar.Builder MergePos(Vector value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				if (this.result.hasPos && this.result.pos_ != Vector.DefaultInstance)
				{
					this.result.pos_ = Vector.CreateBuilder(this.result.pos_).MergeFrom(value).BuildPartial();
				}
				else
				{
					this.result.pos_ = value;
				}
				this.result.hasPos = true;
				return this;
			}

			// Token: 0x06000E3F RID: 3647 RVA: 0x000374B0 File Offset: 0x000356B0
			public Avatar.Builder ClearPos()
			{
				this.PrepareBuilder();
				this.result.hasPos = false;
				this.result.pos_ = null;
				return this;
			}

			// Token: 0x17000380 RID: 896
			// (get) Token: 0x06000E40 RID: 3648 RVA: 0x000374E0 File Offset: 0x000356E0
			public bool HasAng
			{
				get
				{
					return this.result.hasAng;
				}
			}

			// Token: 0x17000381 RID: 897
			// (get) Token: 0x06000E41 RID: 3649 RVA: 0x000374F0 File Offset: 0x000356F0
			// (set) Token: 0x06000E42 RID: 3650 RVA: 0x00037500 File Offset: 0x00035700
			public Quaternion Ang
			{
				get
				{
					return this.result.Ang;
				}
				set
				{
					this.SetAng(value);
				}
			}

			// Token: 0x06000E43 RID: 3651 RVA: 0x0003750C File Offset: 0x0003570C
			public Avatar.Builder SetAng(Quaternion value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasAng = true;
				this.result.ang_ = value;
				return this;
			}

			// Token: 0x06000E44 RID: 3652 RVA: 0x0003753C File Offset: 0x0003573C
			public Avatar.Builder SetAng(Quaternion.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.hasAng = true;
				this.result.ang_ = builderForValue.Build();
				return this;
			}

			// Token: 0x06000E45 RID: 3653 RVA: 0x0003757C File Offset: 0x0003577C
			public Avatar.Builder MergeAng(Quaternion value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				if (this.result.hasAng && this.result.ang_ != Quaternion.DefaultInstance)
				{
					this.result.ang_ = Quaternion.CreateBuilder(this.result.ang_).MergeFrom(value).BuildPartial();
				}
				else
				{
					this.result.ang_ = value;
				}
				this.result.hasAng = true;
				return this;
			}

			// Token: 0x06000E46 RID: 3654 RVA: 0x00037604 File Offset: 0x00035804
			public Avatar.Builder ClearAng()
			{
				this.PrepareBuilder();
				this.result.hasAng = false;
				this.result.ang_ = null;
				return this;
			}

			// Token: 0x17000382 RID: 898
			// (get) Token: 0x06000E47 RID: 3655 RVA: 0x00037634 File Offset: 0x00035834
			public bool HasVitals
			{
				get
				{
					return this.result.hasVitals;
				}
			}

			// Token: 0x17000383 RID: 899
			// (get) Token: 0x06000E48 RID: 3656 RVA: 0x00037644 File Offset: 0x00035844
			// (set) Token: 0x06000E49 RID: 3657 RVA: 0x00037654 File Offset: 0x00035854
			public Vitals Vitals
			{
				get
				{
					return this.result.Vitals;
				}
				set
				{
					this.SetVitals(value);
				}
			}

			// Token: 0x06000E4A RID: 3658 RVA: 0x00037660 File Offset: 0x00035860
			public Avatar.Builder SetVitals(Vitals value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasVitals = true;
				this.result.vitals_ = value;
				return this;
			}

			// Token: 0x06000E4B RID: 3659 RVA: 0x00037690 File Offset: 0x00035890
			public Avatar.Builder SetVitals(Vitals.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.hasVitals = true;
				this.result.vitals_ = builderForValue.Build();
				return this;
			}

			// Token: 0x06000E4C RID: 3660 RVA: 0x000376D0 File Offset: 0x000358D0
			public Avatar.Builder MergeVitals(Vitals value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				if (this.result.hasVitals && this.result.vitals_ != Vitals.DefaultInstance)
				{
					this.result.vitals_ = Vitals.CreateBuilder(this.result.vitals_).MergeFrom(value).BuildPartial();
				}
				else
				{
					this.result.vitals_ = value;
				}
				this.result.hasVitals = true;
				return this;
			}

			// Token: 0x06000E4D RID: 3661 RVA: 0x00037758 File Offset: 0x00035958
			public Avatar.Builder ClearVitals()
			{
				this.PrepareBuilder();
				this.result.hasVitals = false;
				this.result.vitals_ = null;
				return this;
			}

			// Token: 0x17000384 RID: 900
			// (get) Token: 0x06000E4E RID: 3662 RVA: 0x00037788 File Offset: 0x00035988
			public IPopsicleList<Blueprint> BlueprintsList
			{
				get
				{
					return this.PrepareBuilder().blueprints_;
				}
			}

			// Token: 0x17000385 RID: 901
			// (get) Token: 0x06000E4F RID: 3663 RVA: 0x00037798 File Offset: 0x00035998
			public int BlueprintsCount
			{
				get
				{
					return this.result.BlueprintsCount;
				}
			}

			// Token: 0x06000E50 RID: 3664 RVA: 0x000377A8 File Offset: 0x000359A8
			public Blueprint GetBlueprints(int index)
			{
				return this.result.GetBlueprints(index);
			}

			// Token: 0x06000E51 RID: 3665 RVA: 0x000377B8 File Offset: 0x000359B8
			public Avatar.Builder SetBlueprints(int index, Blueprint value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.blueprints_[index] = value;
				return this;
			}

			// Token: 0x06000E52 RID: 3666 RVA: 0x000377E0 File Offset: 0x000359E0
			public Avatar.Builder SetBlueprints(int index, Blueprint.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.blueprints_[index] = builderForValue.Build();
				return this;
			}

			// Token: 0x06000E53 RID: 3667 RVA: 0x00037818 File Offset: 0x00035A18
			public Avatar.Builder AddBlueprints(Blueprint value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.blueprints_.Add(value);
				return this;
			}

			// Token: 0x06000E54 RID: 3668 RVA: 0x0003784C File Offset: 0x00035A4C
			public Avatar.Builder AddBlueprints(Blueprint.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.blueprints_.Add(builderForValue.Build());
				return this;
			}

			// Token: 0x06000E55 RID: 3669 RVA: 0x00037878 File Offset: 0x00035A78
			public Avatar.Builder AddRangeBlueprints(IEnumerable<Blueprint> values)
			{
				this.PrepareBuilder();
				this.result.blueprints_.Add(values);
				return this;
			}

			// Token: 0x06000E56 RID: 3670 RVA: 0x00037894 File Offset: 0x00035A94
			public Avatar.Builder ClearBlueprints()
			{
				this.PrepareBuilder();
				this.result.blueprints_.Clear();
				return this;
			}

			// Token: 0x17000386 RID: 902
			// (get) Token: 0x06000E57 RID: 3671 RVA: 0x000378B0 File Offset: 0x00035AB0
			public IPopsicleList<Item> InventoryList
			{
				get
				{
					return this.PrepareBuilder().inventory_;
				}
			}

			// Token: 0x17000387 RID: 903
			// (get) Token: 0x06000E58 RID: 3672 RVA: 0x000378C0 File Offset: 0x00035AC0
			public int InventoryCount
			{
				get
				{
					return this.result.InventoryCount;
				}
			}

			// Token: 0x06000E59 RID: 3673 RVA: 0x000378D0 File Offset: 0x00035AD0
			public Item GetInventory(int index)
			{
				return this.result.GetInventory(index);
			}

			// Token: 0x06000E5A RID: 3674 RVA: 0x000378E0 File Offset: 0x00035AE0
			public Avatar.Builder SetInventory(int index, Item value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.inventory_[index] = value;
				return this;
			}

			// Token: 0x06000E5B RID: 3675 RVA: 0x00037908 File Offset: 0x00035B08
			public Avatar.Builder SetInventory(int index, Item.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.inventory_[index] = builderForValue.Build();
				return this;
			}

			// Token: 0x06000E5C RID: 3676 RVA: 0x00037940 File Offset: 0x00035B40
			public Avatar.Builder AddInventory(Item value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.inventory_.Add(value);
				return this;
			}

			// Token: 0x06000E5D RID: 3677 RVA: 0x00037974 File Offset: 0x00035B74
			public Avatar.Builder AddInventory(Item.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.inventory_.Add(builderForValue.Build());
				return this;
			}

			// Token: 0x06000E5E RID: 3678 RVA: 0x000379A0 File Offset: 0x00035BA0
			public Avatar.Builder AddRangeInventory(IEnumerable<Item> values)
			{
				this.PrepareBuilder();
				this.result.inventory_.Add(values);
				return this;
			}

			// Token: 0x06000E5F RID: 3679 RVA: 0x000379BC File Offset: 0x00035BBC
			public Avatar.Builder ClearInventory()
			{
				this.PrepareBuilder();
				this.result.inventory_.Clear();
				return this;
			}

			// Token: 0x17000388 RID: 904
			// (get) Token: 0x06000E60 RID: 3680 RVA: 0x000379D8 File Offset: 0x00035BD8
			public IPopsicleList<Item> WearableList
			{
				get
				{
					return this.PrepareBuilder().wearable_;
				}
			}

			// Token: 0x17000389 RID: 905
			// (get) Token: 0x06000E61 RID: 3681 RVA: 0x000379E8 File Offset: 0x00035BE8
			public int WearableCount
			{
				get
				{
					return this.result.WearableCount;
				}
			}

			// Token: 0x06000E62 RID: 3682 RVA: 0x000379F8 File Offset: 0x00035BF8
			public Item GetWearable(int index)
			{
				return this.result.GetWearable(index);
			}

			// Token: 0x06000E63 RID: 3683 RVA: 0x00037A08 File Offset: 0x00035C08
			public Avatar.Builder SetWearable(int index, Item value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.wearable_[index] = value;
				return this;
			}

			// Token: 0x06000E64 RID: 3684 RVA: 0x00037A30 File Offset: 0x00035C30
			public Avatar.Builder SetWearable(int index, Item.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.wearable_[index] = builderForValue.Build();
				return this;
			}

			// Token: 0x06000E65 RID: 3685 RVA: 0x00037A68 File Offset: 0x00035C68
			public Avatar.Builder AddWearable(Item value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.wearable_.Add(value);
				return this;
			}

			// Token: 0x06000E66 RID: 3686 RVA: 0x00037A9C File Offset: 0x00035C9C
			public Avatar.Builder AddWearable(Item.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.wearable_.Add(builderForValue.Build());
				return this;
			}

			// Token: 0x06000E67 RID: 3687 RVA: 0x00037AC8 File Offset: 0x00035CC8
			public Avatar.Builder AddRangeWearable(IEnumerable<Item> values)
			{
				this.PrepareBuilder();
				this.result.wearable_.Add(values);
				return this;
			}

			// Token: 0x06000E68 RID: 3688 RVA: 0x00037AE4 File Offset: 0x00035CE4
			public Avatar.Builder ClearWearable()
			{
				this.PrepareBuilder();
				this.result.wearable_.Clear();
				return this;
			}

			// Token: 0x1700038A RID: 906
			// (get) Token: 0x06000E69 RID: 3689 RVA: 0x00037B00 File Offset: 0x00035D00
			public IPopsicleList<Item> BeltList
			{
				get
				{
					return this.PrepareBuilder().belt_;
				}
			}

			// Token: 0x1700038B RID: 907
			// (get) Token: 0x06000E6A RID: 3690 RVA: 0x00037B10 File Offset: 0x00035D10
			public int BeltCount
			{
				get
				{
					return this.result.BeltCount;
				}
			}

			// Token: 0x06000E6B RID: 3691 RVA: 0x00037B20 File Offset: 0x00035D20
			public Item GetBelt(int index)
			{
				return this.result.GetBelt(index);
			}

			// Token: 0x06000E6C RID: 3692 RVA: 0x00037B30 File Offset: 0x00035D30
			public Avatar.Builder SetBelt(int index, Item value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.belt_[index] = value;
				return this;
			}

			// Token: 0x06000E6D RID: 3693 RVA: 0x00037B58 File Offset: 0x00035D58
			public Avatar.Builder SetBelt(int index, Item.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.belt_[index] = builderForValue.Build();
				return this;
			}

			// Token: 0x06000E6E RID: 3694 RVA: 0x00037B90 File Offset: 0x00035D90
			public Avatar.Builder AddBelt(Item value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.belt_.Add(value);
				return this;
			}

			// Token: 0x06000E6F RID: 3695 RVA: 0x00037BC4 File Offset: 0x00035DC4
			public Avatar.Builder AddBelt(Item.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.belt_.Add(builderForValue.Build());
				return this;
			}

			// Token: 0x06000E70 RID: 3696 RVA: 0x00037BF0 File Offset: 0x00035DF0
			public Avatar.Builder AddRangeBelt(IEnumerable<Item> values)
			{
				this.PrepareBuilder();
				this.result.belt_.Add(values);
				return this;
			}

			// Token: 0x06000E71 RID: 3697 RVA: 0x00037C0C File Offset: 0x00035E0C
			public Avatar.Builder ClearBelt()
			{
				this.PrepareBuilder();
				this.result.belt_.Clear();
				return this;
			}

			// Token: 0x1700038C RID: 908
			// (get) Token: 0x06000E72 RID: 3698 RVA: 0x00037C28 File Offset: 0x00035E28
			public bool HasAwayEvent
			{
				get
				{
					return this.result.hasAwayEvent;
				}
			}

			// Token: 0x1700038D RID: 909
			// (get) Token: 0x06000E73 RID: 3699 RVA: 0x00037C38 File Offset: 0x00035E38
			// (set) Token: 0x06000E74 RID: 3700 RVA: 0x00037C48 File Offset: 0x00035E48
			public AwayEvent AwayEvent
			{
				get
				{
					return this.result.AwayEvent;
				}
				set
				{
					this.SetAwayEvent(value);
				}
			}

			// Token: 0x06000E75 RID: 3701 RVA: 0x00037C54 File Offset: 0x00035E54
			public Avatar.Builder SetAwayEvent(AwayEvent value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.hasAwayEvent = true;
				this.result.awayEvent_ = value;
				return this;
			}

			// Token: 0x06000E76 RID: 3702 RVA: 0x00037C84 File Offset: 0x00035E84
			public Avatar.Builder SetAwayEvent(AwayEvent.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.hasAwayEvent = true;
				this.result.awayEvent_ = builderForValue.Build();
				return this;
			}

			// Token: 0x06000E77 RID: 3703 RVA: 0x00037CC4 File Offset: 0x00035EC4
			public Avatar.Builder MergeAwayEvent(AwayEvent value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				if (this.result.hasAwayEvent && this.result.awayEvent_ != AwayEvent.DefaultInstance)
				{
					this.result.awayEvent_ = AwayEvent.CreateBuilder(this.result.awayEvent_).MergeFrom(value).BuildPartial();
				}
				else
				{
					this.result.awayEvent_ = value;
				}
				this.result.hasAwayEvent = true;
				return this;
			}

			// Token: 0x06000E78 RID: 3704 RVA: 0x00037D4C File Offset: 0x00035F4C
			public Avatar.Builder ClearAwayEvent()
			{
				this.PrepareBuilder();
				this.result.hasAwayEvent = false;
				this.result.awayEvent_ = null;
				return this;
			}

			// Token: 0x040008A3 RID: 2211
			private bool resultIsReadOnly;

			// Token: 0x040008A4 RID: 2212
			private Avatar result;
		}
	}
}
