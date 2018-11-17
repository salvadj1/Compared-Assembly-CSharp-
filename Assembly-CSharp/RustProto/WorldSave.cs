using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Collections;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;
using RustProto.Helpers;

namespace RustProto
{
	// Token: 0x0200022E RID: 558
	[DebuggerNonUserCode]
	public sealed class WorldSave : GeneratedMessage<WorldSave, WorldSave.Builder>
	{
		// Token: 0x06000F0F RID: 3855 RVA: 0x0003A0A0 File Offset: 0x000382A0
		private WorldSave()
		{
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x0003A0C8 File Offset: 0x000382C8
		static WorldSave()
		{
			object.ReferenceEquals(Worldsave.Descriptor, null);
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x0003A120 File Offset: 0x00038320
		public static RustProto.Helpers.Recycler<WorldSave, WorldSave.Builder> Recycler()
		{
			return RustProto.Helpers.Recycler<WorldSave, WorldSave.Builder>.Manufacture();
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000F12 RID: 3858 RVA: 0x0003A128 File Offset: 0x00038328
		public static WorldSave DefaultInstance
		{
			get
			{
				return WorldSave.defaultInstance;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000F13 RID: 3859 RVA: 0x0003A130 File Offset: 0x00038330
		public override WorldSave DefaultInstanceForType
		{
			get
			{
				return WorldSave.DefaultInstance;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000F14 RID: 3860 RVA: 0x0003A138 File Offset: 0x00038338
		protected override WorldSave ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000F15 RID: 3861 RVA: 0x0003A13C File Offset: 0x0003833C
		public static MessageDescriptor Descriptor
		{
			get
			{
				return Worldsave.internal__static_RustProto_WorldSave__Descriptor;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000F16 RID: 3862 RVA: 0x0003A144 File Offset: 0x00038344
		protected override FieldAccessorTable<WorldSave, WorldSave.Builder> InternalFieldAccessors
		{
			get
			{
				return Worldsave.internal__static_RustProto_WorldSave__FieldAccessorTable;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000F17 RID: 3863 RVA: 0x0003A14C File Offset: 0x0003834C
		public IList<SavedObject> SceneObjectList
		{
			get
			{
				return this.sceneObject_;
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000F18 RID: 3864 RVA: 0x0003A154 File Offset: 0x00038354
		public int SceneObjectCount
		{
			get
			{
				return this.sceneObject_.Count;
			}
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x0003A164 File Offset: 0x00038364
		public SavedObject GetSceneObject(int index)
		{
			return this.sceneObject_[index];
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000F1A RID: 3866 RVA: 0x0003A174 File Offset: 0x00038374
		public IList<SavedObject> InstanceObjectList
		{
			get
			{
				return this.instanceObject_;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000F1B RID: 3867 RVA: 0x0003A17C File Offset: 0x0003837C
		public int InstanceObjectCount
		{
			get
			{
				return this.instanceObject_.Count;
			}
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x0003A18C File Offset: 0x0003838C
		public SavedObject GetInstanceObject(int index)
		{
			return this.instanceObject_[index];
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000F1D RID: 3869 RVA: 0x0003A19C File Offset: 0x0003839C
		public override bool IsInitialized
		{
			get
			{
				foreach (SavedObject savedObject in this.SceneObjectList)
				{
					if (!savedObject.IsInitialized)
					{
						return false;
					}
				}
				foreach (SavedObject savedObject2 in this.InstanceObjectList)
				{
					if (!savedObject2.IsInitialized)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x0003A270 File Offset: 0x00038470
		public override void WriteTo(ICodedOutputStream output)
		{
			int serializedSize = this.SerializedSize;
			string[] worldSaveFieldNames = WorldSave._worldSaveFieldNames;
			if (this.sceneObject_.Count > 0)
			{
				output.WriteMessageArray<SavedObject>(1, worldSaveFieldNames[1], this.sceneObject_);
			}
			if (this.instanceObject_.Count > 0)
			{
				output.WriteMessageArray<SavedObject>(2, worldSaveFieldNames[0], this.instanceObject_);
			}
			this.UnknownFields.WriteTo(output);
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000F1F RID: 3871 RVA: 0x0003A2D8 File Offset: 0x000384D8
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
				foreach (SavedObject savedObject in this.SceneObjectList)
				{
					num += CodedOutputStream.ComputeMessageSize(1, savedObject);
				}
				foreach (SavedObject savedObject2 in this.InstanceObjectList)
				{
					num += CodedOutputStream.ComputeMessageSize(2, savedObject2);
				}
				num += this.UnknownFields.SerializedSize;
				this.memoizedSerializedSize = num;
				return num;
			}
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x0003A3C0 File Offset: 0x000385C0
		public static WorldSave ParseFrom(ByteString data)
		{
			return WorldSave.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x0003A3D4 File Offset: 0x000385D4
		public static WorldSave ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return WorldSave.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x0003A3E8 File Offset: 0x000385E8
		public static WorldSave ParseFrom(byte[] data)
		{
			return WorldSave.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x0003A3FC File Offset: 0x000385FC
		public static WorldSave ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return WorldSave.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x0003A410 File Offset: 0x00038610
		public static WorldSave ParseFrom(Stream input)
		{
			return WorldSave.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x0003A424 File Offset: 0x00038624
		public static WorldSave ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return WorldSave.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x0003A438 File Offset: 0x00038638
		public static WorldSave ParseDelimitedFrom(Stream input)
		{
			return WorldSave.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x0003A44C File Offset: 0x0003864C
		public static WorldSave ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return WorldSave.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x0003A460 File Offset: 0x00038660
		public static WorldSave ParseFrom(ICodedInputStream input)
		{
			return WorldSave.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x0003A474 File Offset: 0x00038674
		public static WorldSave ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return WorldSave.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x0003A488 File Offset: 0x00038688
		private WorldSave MakeReadOnly()
		{
			this.sceneObject_.MakeReadOnly();
			this.instanceObject_.MakeReadOnly();
			return this;
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x0003A4A4 File Offset: 0x000386A4
		public static WorldSave.Builder CreateBuilder()
		{
			return new WorldSave.Builder();
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x0003A4AC File Offset: 0x000386AC
		public override WorldSave.Builder ToBuilder()
		{
			return WorldSave.CreateBuilder(this);
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x0003A4B4 File Offset: 0x000386B4
		public override WorldSave.Builder CreateBuilderForType()
		{
			return new WorldSave.Builder();
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x0003A4BC File Offset: 0x000386BC
		public static WorldSave.Builder CreateBuilder(WorldSave prototype)
		{
			return new WorldSave.Builder(prototype);
		}

		// Token: 0x040009A4 RID: 2468
		public const int SceneObjectFieldNumber = 1;

		// Token: 0x040009A5 RID: 2469
		public const int InstanceObjectFieldNumber = 2;

		// Token: 0x040009A6 RID: 2470
		private static readonly WorldSave defaultInstance = new WorldSave().MakeReadOnly();

		// Token: 0x040009A7 RID: 2471
		private static readonly string[] _worldSaveFieldNames = new string[]
		{
			"instanceObject",
			"sceneObject"
		};

		// Token: 0x040009A8 RID: 2472
		private static readonly uint[] _worldSaveFieldTags = new uint[]
		{
			18u,
			10u
		};

		// Token: 0x040009A9 RID: 2473
		private PopsicleList<SavedObject> sceneObject_ = new PopsicleList<SavedObject>();

		// Token: 0x040009AA RID: 2474
		private PopsicleList<SavedObject> instanceObject_ = new PopsicleList<SavedObject>();

		// Token: 0x040009AB RID: 2475
		private int memoizedSerializedSize = -1;

		// Token: 0x0200022F RID: 559
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<WorldSave, WorldSave.Builder>
		{
			// Token: 0x06000F2F RID: 3887 RVA: 0x0003A4C4 File Offset: 0x000386C4
			public Builder()
			{
				this.result = WorldSave.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x06000F30 RID: 3888 RVA: 0x0003A4E0 File Offset: 0x000386E0
			internal Builder(WorldSave cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x170003A1 RID: 929
			// (get) Token: 0x06000F31 RID: 3889 RVA: 0x0003A4F8 File Offset: 0x000386F8
			protected override WorldSave.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x06000F32 RID: 3890 RVA: 0x0003A4FC File Offset: 0x000386FC
			private WorldSave PrepareBuilder()
			{
				if (this.resultIsReadOnly)
				{
					WorldSave other = this.result;
					this.result = new WorldSave();
					this.resultIsReadOnly = false;
					this.MergeFrom(other);
				}
				return this.result;
			}

			// Token: 0x170003A2 RID: 930
			// (get) Token: 0x06000F33 RID: 3891 RVA: 0x0003A53C File Offset: 0x0003873C
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x170003A3 RID: 931
			// (get) Token: 0x06000F34 RID: 3892 RVA: 0x0003A54C File Offset: 0x0003874C
			protected override WorldSave MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x06000F35 RID: 3893 RVA: 0x0003A554 File Offset: 0x00038754
			public override WorldSave.Builder Clear()
			{
				this.result = WorldSave.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x06000F36 RID: 3894 RVA: 0x0003A56C File Offset: 0x0003876C
			public override WorldSave.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new WorldSave.Builder(this.result);
				}
				return new WorldSave.Builder().MergeFrom(this.result);
			}

			// Token: 0x170003A4 RID: 932
			// (get) Token: 0x06000F37 RID: 3895 RVA: 0x0003A598 File Offset: 0x00038798
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return WorldSave.Descriptor;
				}
			}

			// Token: 0x170003A5 RID: 933
			// (get) Token: 0x06000F38 RID: 3896 RVA: 0x0003A5A0 File Offset: 0x000387A0
			public override WorldSave DefaultInstanceForType
			{
				get
				{
					return WorldSave.DefaultInstance;
				}
			}

			// Token: 0x06000F39 RID: 3897 RVA: 0x0003A5A8 File Offset: 0x000387A8
			public override WorldSave BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x06000F3A RID: 3898 RVA: 0x0003A5DC File Offset: 0x000387DC
			public override WorldSave.Builder MergeFrom(IMessage other)
			{
				if (other is WorldSave)
				{
					return this.MergeFrom((WorldSave)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x06000F3B RID: 3899 RVA: 0x0003A600 File Offset: 0x00038800
			public override WorldSave.Builder MergeFrom(WorldSave other)
			{
				if (other == WorldSave.DefaultInstance)
				{
					return this;
				}
				this.PrepareBuilder();
				if (other.sceneObject_.Count != 0)
				{
					this.result.sceneObject_.Add(other.sceneObject_);
				}
				if (other.instanceObject_.Count != 0)
				{
					this.result.instanceObject_.Add(other.instanceObject_);
				}
				this.MergeUnknownFields(other.UnknownFields);
				return this;
			}

			// Token: 0x06000F3C RID: 3900 RVA: 0x0003A67C File Offset: 0x0003887C
			public override WorldSave.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x06000F3D RID: 3901 RVA: 0x0003A68C File Offset: 0x0003888C
			public override WorldSave.Builder MergeFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
			{
				this.PrepareBuilder();
				UnknownFieldSet.Builder builder = null;
				uint num;
				string text;
				while (input.ReadTag(ref num, ref text))
				{
					if (num == 0u && text != null)
					{
						int num2 = Array.BinarySearch<string>(WorldSave._worldSaveFieldNames, text, StringComparer.Ordinal);
						if (num2 < 0)
						{
							if (builder == null)
							{
								builder = UnknownFieldSet.CreateBuilder(this.UnknownFields);
							}
							this.ParseUnknownField(input, builder, extensionRegistry, num, text);
							continue;
						}
						num = WorldSave._worldSaveFieldTags[num2];
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
							input.ReadMessageArray<SavedObject>(num, text, this.result.instanceObject_, SavedObject.DefaultInstance, extensionRegistry);
						}
					}
					else
					{
						input.ReadMessageArray<SavedObject>(num, text, this.result.sceneObject_, SavedObject.DefaultInstance, extensionRegistry);
					}
				}
				if (builder != null)
				{
					this.UnknownFields = builder.Build();
				}
				return this;
			}

			// Token: 0x170003A6 RID: 934
			// (get) Token: 0x06000F3E RID: 3902 RVA: 0x0003A7C4 File Offset: 0x000389C4
			public IPopsicleList<SavedObject> SceneObjectList
			{
				get
				{
					return this.PrepareBuilder().sceneObject_;
				}
			}

			// Token: 0x170003A7 RID: 935
			// (get) Token: 0x06000F3F RID: 3903 RVA: 0x0003A7D4 File Offset: 0x000389D4
			public int SceneObjectCount
			{
				get
				{
					return this.result.SceneObjectCount;
				}
			}

			// Token: 0x06000F40 RID: 3904 RVA: 0x0003A7E4 File Offset: 0x000389E4
			public SavedObject GetSceneObject(int index)
			{
				return this.result.GetSceneObject(index);
			}

			// Token: 0x06000F41 RID: 3905 RVA: 0x0003A7F4 File Offset: 0x000389F4
			public WorldSave.Builder SetSceneObject(int index, SavedObject value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.sceneObject_[index] = value;
				return this;
			}

			// Token: 0x06000F42 RID: 3906 RVA: 0x0003A81C File Offset: 0x00038A1C
			public WorldSave.Builder SetSceneObject(int index, SavedObject.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.sceneObject_[index] = builderForValue.Build();
				return this;
			}

			// Token: 0x06000F43 RID: 3907 RVA: 0x0003A854 File Offset: 0x00038A54
			public WorldSave.Builder AddSceneObject(SavedObject value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.sceneObject_.Add(value);
				return this;
			}

			// Token: 0x06000F44 RID: 3908 RVA: 0x0003A888 File Offset: 0x00038A88
			public WorldSave.Builder AddSceneObject(SavedObject.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.sceneObject_.Add(builderForValue.Build());
				return this;
			}

			// Token: 0x06000F45 RID: 3909 RVA: 0x0003A8B4 File Offset: 0x00038AB4
			public WorldSave.Builder AddRangeSceneObject(IEnumerable<SavedObject> values)
			{
				this.PrepareBuilder();
				this.result.sceneObject_.Add(values);
				return this;
			}

			// Token: 0x06000F46 RID: 3910 RVA: 0x0003A8D0 File Offset: 0x00038AD0
			public WorldSave.Builder ClearSceneObject()
			{
				this.PrepareBuilder();
				this.result.sceneObject_.Clear();
				return this;
			}

			// Token: 0x170003A8 RID: 936
			// (get) Token: 0x06000F47 RID: 3911 RVA: 0x0003A8EC File Offset: 0x00038AEC
			public IPopsicleList<SavedObject> InstanceObjectList
			{
				get
				{
					return this.PrepareBuilder().instanceObject_;
				}
			}

			// Token: 0x170003A9 RID: 937
			// (get) Token: 0x06000F48 RID: 3912 RVA: 0x0003A8FC File Offset: 0x00038AFC
			public int InstanceObjectCount
			{
				get
				{
					return this.result.InstanceObjectCount;
				}
			}

			// Token: 0x06000F49 RID: 3913 RVA: 0x0003A90C File Offset: 0x00038B0C
			public SavedObject GetInstanceObject(int index)
			{
				return this.result.GetInstanceObject(index);
			}

			// Token: 0x06000F4A RID: 3914 RVA: 0x0003A91C File Offset: 0x00038B1C
			public WorldSave.Builder SetInstanceObject(int index, SavedObject value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.instanceObject_[index] = value;
				return this;
			}

			// Token: 0x06000F4B RID: 3915 RVA: 0x0003A944 File Offset: 0x00038B44
			public WorldSave.Builder SetInstanceObject(int index, SavedObject.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.instanceObject_[index] = builderForValue.Build();
				return this;
			}

			// Token: 0x06000F4C RID: 3916 RVA: 0x0003A97C File Offset: 0x00038B7C
			public WorldSave.Builder AddInstanceObject(SavedObject value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.instanceObject_.Add(value);
				return this;
			}

			// Token: 0x06000F4D RID: 3917 RVA: 0x0003A9B0 File Offset: 0x00038BB0
			public WorldSave.Builder AddInstanceObject(SavedObject.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.instanceObject_.Add(builderForValue.Build());
				return this;
			}

			// Token: 0x06000F4E RID: 3918 RVA: 0x0003A9DC File Offset: 0x00038BDC
			public WorldSave.Builder AddRangeInstanceObject(IEnumerable<SavedObject> values)
			{
				this.PrepareBuilder();
				this.result.instanceObject_.Add(values);
				return this;
			}

			// Token: 0x06000F4F RID: 3919 RVA: 0x0003A9F8 File Offset: 0x00038BF8
			public WorldSave.Builder ClearInstanceObject()
			{
				this.PrepareBuilder();
				this.result.instanceObject_.Clear();
				return this;
			}

			// Token: 0x040009AC RID: 2476
			private bool resultIsReadOnly;

			// Token: 0x040009AD RID: 2477
			private WorldSave result;
		}
	}
}
