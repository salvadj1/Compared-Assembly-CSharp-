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
	// Token: 0x020001FB RID: 507
	[DebuggerNonUserCode]
	public sealed class WorldSave : GeneratedMessage<WorldSave, WorldSave.Builder>
	{
		// Token: 0x06000DBB RID: 3515 RVA: 0x00035CF8 File Offset: 0x00033EF8
		private WorldSave()
		{
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x00035D20 File Offset: 0x00033F20
		static WorldSave()
		{
			object.ReferenceEquals(Worldsave.Descriptor, null);
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x00035D78 File Offset: 0x00033F78
		public static Recycler<WorldSave, WorldSave.Builder> Recycler()
		{
			return Recycler<WorldSave, WorldSave.Builder>.Manufacture();
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000DBE RID: 3518 RVA: 0x00035D80 File Offset: 0x00033F80
		public static WorldSave DefaultInstance
		{
			get
			{
				return WorldSave.defaultInstance;
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000DBF RID: 3519 RVA: 0x00035D88 File Offset: 0x00033F88
		public override WorldSave DefaultInstanceForType
		{
			get
			{
				return WorldSave.DefaultInstance;
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000DC0 RID: 3520 RVA: 0x00035D90 File Offset: 0x00033F90
		protected override WorldSave ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000DC1 RID: 3521 RVA: 0x00035D94 File Offset: 0x00033F94
		public static MessageDescriptor Descriptor
		{
			get
			{
				return Worldsave.internal__static_RustProto_WorldSave__Descriptor;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000DC2 RID: 3522 RVA: 0x00035D9C File Offset: 0x00033F9C
		protected override FieldAccessorTable<WorldSave, WorldSave.Builder> InternalFieldAccessors
		{
			get
			{
				return Worldsave.internal__static_RustProto_WorldSave__FieldAccessorTable;
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000DC3 RID: 3523 RVA: 0x00035DA4 File Offset: 0x00033FA4
		public IList<SavedObject> SceneObjectList
		{
			get
			{
				return this.sceneObject_;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000DC4 RID: 3524 RVA: 0x00035DAC File Offset: 0x00033FAC
		public int SceneObjectCount
		{
			get
			{
				return this.sceneObject_.Count;
			}
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x00035DBC File Offset: 0x00033FBC
		public SavedObject GetSceneObject(int index)
		{
			return this.sceneObject_[index];
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000DC6 RID: 3526 RVA: 0x00035DCC File Offset: 0x00033FCC
		public IList<SavedObject> InstanceObjectList
		{
			get
			{
				return this.instanceObject_;
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000DC7 RID: 3527 RVA: 0x00035DD4 File Offset: 0x00033FD4
		public int InstanceObjectCount
		{
			get
			{
				return this.instanceObject_.Count;
			}
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x00035DE4 File Offset: 0x00033FE4
		public SavedObject GetInstanceObject(int index)
		{
			return this.instanceObject_[index];
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000DC9 RID: 3529 RVA: 0x00035DF4 File Offset: 0x00033FF4
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

		// Token: 0x06000DCA RID: 3530 RVA: 0x00035EC8 File Offset: 0x000340C8
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

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000DCB RID: 3531 RVA: 0x00035F30 File Offset: 0x00034130
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

		// Token: 0x06000DCC RID: 3532 RVA: 0x00036018 File Offset: 0x00034218
		public static WorldSave ParseFrom(ByteString data)
		{
			return WorldSave.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x0003602C File Offset: 0x0003422C
		public static WorldSave ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return WorldSave.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x00036040 File Offset: 0x00034240
		public static WorldSave ParseFrom(byte[] data)
		{
			return WorldSave.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x00036054 File Offset: 0x00034254
		public static WorldSave ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return WorldSave.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x00036068 File Offset: 0x00034268
		public static WorldSave ParseFrom(Stream input)
		{
			return WorldSave.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x0003607C File Offset: 0x0003427C
		public static WorldSave ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return WorldSave.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x00036090 File Offset: 0x00034290
		public static WorldSave ParseDelimitedFrom(Stream input)
		{
			return WorldSave.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x000360A4 File Offset: 0x000342A4
		public static WorldSave ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return WorldSave.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x000360B8 File Offset: 0x000342B8
		public static WorldSave ParseFrom(ICodedInputStream input)
		{
			return WorldSave.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x000360CC File Offset: 0x000342CC
		public static WorldSave ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return WorldSave.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x000360E0 File Offset: 0x000342E0
		private WorldSave MakeReadOnly()
		{
			this.sceneObject_.MakeReadOnly();
			this.instanceObject_.MakeReadOnly();
			return this;
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x000360FC File Offset: 0x000342FC
		public static WorldSave.Builder CreateBuilder()
		{
			return new WorldSave.Builder();
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x00036104 File Offset: 0x00034304
		public override WorldSave.Builder ToBuilder()
		{
			return WorldSave.CreateBuilder(this);
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x0003610C File Offset: 0x0003430C
		public override WorldSave.Builder CreateBuilderForType()
		{
			return new WorldSave.Builder();
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x00036114 File Offset: 0x00034314
		public static WorldSave.Builder CreateBuilder(WorldSave prototype)
		{
			return new WorldSave.Builder(prototype);
		}

		// Token: 0x04000881 RID: 2177
		public const int SceneObjectFieldNumber = 1;

		// Token: 0x04000882 RID: 2178
		public const int InstanceObjectFieldNumber = 2;

		// Token: 0x04000883 RID: 2179
		private static readonly WorldSave defaultInstance = new WorldSave().MakeReadOnly();

		// Token: 0x04000884 RID: 2180
		private static readonly string[] _worldSaveFieldNames = new string[]
		{
			"instanceObject",
			"sceneObject"
		};

		// Token: 0x04000885 RID: 2181
		private static readonly uint[] _worldSaveFieldTags = new uint[]
		{
			18u,
			10u
		};

		// Token: 0x04000886 RID: 2182
		private PopsicleList<SavedObject> sceneObject_ = new PopsicleList<SavedObject>();

		// Token: 0x04000887 RID: 2183
		private PopsicleList<SavedObject> instanceObject_ = new PopsicleList<SavedObject>();

		// Token: 0x04000888 RID: 2184
		private int memoizedSerializedSize = -1;

		// Token: 0x020001FC RID: 508
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<WorldSave, WorldSave.Builder>
		{
			// Token: 0x06000DDB RID: 3547 RVA: 0x0003611C File Offset: 0x0003431C
			public Builder()
			{
				this.result = WorldSave.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x06000DDC RID: 3548 RVA: 0x00036138 File Offset: 0x00034338
			internal Builder(WorldSave cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x17000359 RID: 857
			// (get) Token: 0x06000DDD RID: 3549 RVA: 0x00036150 File Offset: 0x00034350
			protected override WorldSave.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x06000DDE RID: 3550 RVA: 0x00036154 File Offset: 0x00034354
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

			// Token: 0x1700035A RID: 858
			// (get) Token: 0x06000DDF RID: 3551 RVA: 0x00036194 File Offset: 0x00034394
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x1700035B RID: 859
			// (get) Token: 0x06000DE0 RID: 3552 RVA: 0x000361A4 File Offset: 0x000343A4
			protected override WorldSave MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x06000DE1 RID: 3553 RVA: 0x000361AC File Offset: 0x000343AC
			public override WorldSave.Builder Clear()
			{
				this.result = WorldSave.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x06000DE2 RID: 3554 RVA: 0x000361C4 File Offset: 0x000343C4
			public override WorldSave.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new WorldSave.Builder(this.result);
				}
				return new WorldSave.Builder().MergeFrom(this.result);
			}

			// Token: 0x1700035C RID: 860
			// (get) Token: 0x06000DE3 RID: 3555 RVA: 0x000361F0 File Offset: 0x000343F0
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return WorldSave.Descriptor;
				}
			}

			// Token: 0x1700035D RID: 861
			// (get) Token: 0x06000DE4 RID: 3556 RVA: 0x000361F8 File Offset: 0x000343F8
			public override WorldSave DefaultInstanceForType
			{
				get
				{
					return WorldSave.DefaultInstance;
				}
			}

			// Token: 0x06000DE5 RID: 3557 RVA: 0x00036200 File Offset: 0x00034400
			public override WorldSave BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x06000DE6 RID: 3558 RVA: 0x00036234 File Offset: 0x00034434
			public override WorldSave.Builder MergeFrom(IMessage other)
			{
				if (other is WorldSave)
				{
					return this.MergeFrom((WorldSave)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x06000DE7 RID: 3559 RVA: 0x00036258 File Offset: 0x00034458
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

			// Token: 0x06000DE8 RID: 3560 RVA: 0x000362D4 File Offset: 0x000344D4
			public override WorldSave.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x06000DE9 RID: 3561 RVA: 0x000362E4 File Offset: 0x000344E4
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

			// Token: 0x1700035E RID: 862
			// (get) Token: 0x06000DEA RID: 3562 RVA: 0x0003641C File Offset: 0x0003461C
			public IPopsicleList<SavedObject> SceneObjectList
			{
				get
				{
					return this.PrepareBuilder().sceneObject_;
				}
			}

			// Token: 0x1700035F RID: 863
			// (get) Token: 0x06000DEB RID: 3563 RVA: 0x0003642C File Offset: 0x0003462C
			public int SceneObjectCount
			{
				get
				{
					return this.result.SceneObjectCount;
				}
			}

			// Token: 0x06000DEC RID: 3564 RVA: 0x0003643C File Offset: 0x0003463C
			public SavedObject GetSceneObject(int index)
			{
				return this.result.GetSceneObject(index);
			}

			// Token: 0x06000DED RID: 3565 RVA: 0x0003644C File Offset: 0x0003464C
			public WorldSave.Builder SetSceneObject(int index, SavedObject value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.sceneObject_[index] = value;
				return this;
			}

			// Token: 0x06000DEE RID: 3566 RVA: 0x00036474 File Offset: 0x00034674
			public WorldSave.Builder SetSceneObject(int index, SavedObject.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.sceneObject_[index] = builderForValue.Build();
				return this;
			}

			// Token: 0x06000DEF RID: 3567 RVA: 0x000364AC File Offset: 0x000346AC
			public WorldSave.Builder AddSceneObject(SavedObject value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.sceneObject_.Add(value);
				return this;
			}

			// Token: 0x06000DF0 RID: 3568 RVA: 0x000364E0 File Offset: 0x000346E0
			public WorldSave.Builder AddSceneObject(SavedObject.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.sceneObject_.Add(builderForValue.Build());
				return this;
			}

			// Token: 0x06000DF1 RID: 3569 RVA: 0x0003650C File Offset: 0x0003470C
			public WorldSave.Builder AddRangeSceneObject(IEnumerable<SavedObject> values)
			{
				this.PrepareBuilder();
				this.result.sceneObject_.Add(values);
				return this;
			}

			// Token: 0x06000DF2 RID: 3570 RVA: 0x00036528 File Offset: 0x00034728
			public WorldSave.Builder ClearSceneObject()
			{
				this.PrepareBuilder();
				this.result.sceneObject_.Clear();
				return this;
			}

			// Token: 0x17000360 RID: 864
			// (get) Token: 0x06000DF3 RID: 3571 RVA: 0x00036544 File Offset: 0x00034744
			public IPopsicleList<SavedObject> InstanceObjectList
			{
				get
				{
					return this.PrepareBuilder().instanceObject_;
				}
			}

			// Token: 0x17000361 RID: 865
			// (get) Token: 0x06000DF4 RID: 3572 RVA: 0x00036554 File Offset: 0x00034754
			public int InstanceObjectCount
			{
				get
				{
					return this.result.InstanceObjectCount;
				}
			}

			// Token: 0x06000DF5 RID: 3573 RVA: 0x00036564 File Offset: 0x00034764
			public SavedObject GetInstanceObject(int index)
			{
				return this.result.GetInstanceObject(index);
			}

			// Token: 0x06000DF6 RID: 3574 RVA: 0x00036574 File Offset: 0x00034774
			public WorldSave.Builder SetInstanceObject(int index, SavedObject value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.instanceObject_[index] = value;
				return this;
			}

			// Token: 0x06000DF7 RID: 3575 RVA: 0x0003659C File Offset: 0x0003479C
			public WorldSave.Builder SetInstanceObject(int index, SavedObject.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.instanceObject_[index] = builderForValue.Build();
				return this;
			}

			// Token: 0x06000DF8 RID: 3576 RVA: 0x000365D4 File Offset: 0x000347D4
			public WorldSave.Builder AddInstanceObject(SavedObject value)
			{
				ThrowHelper.ThrowIfNull(value, "value");
				this.PrepareBuilder();
				this.result.instanceObject_.Add(value);
				return this;
			}

			// Token: 0x06000DF9 RID: 3577 RVA: 0x00036608 File Offset: 0x00034808
			public WorldSave.Builder AddInstanceObject(SavedObject.Builder builderForValue)
			{
				ThrowHelper.ThrowIfNull(builderForValue, "builderForValue");
				this.PrepareBuilder();
				this.result.instanceObject_.Add(builderForValue.Build());
				return this;
			}

			// Token: 0x06000DFA RID: 3578 RVA: 0x00036634 File Offset: 0x00034834
			public WorldSave.Builder AddRangeInstanceObject(IEnumerable<SavedObject> values)
			{
				this.PrepareBuilder();
				this.result.instanceObject_.Add(values);
				return this;
			}

			// Token: 0x06000DFB RID: 3579 RVA: 0x00036650 File Offset: 0x00034850
			public WorldSave.Builder ClearInstanceObject()
			{
				this.PrepareBuilder();
				this.result.instanceObject_.Clear();
				return this;
			}

			// Token: 0x04000889 RID: 2185
			private bool resultIsReadOnly;

			// Token: 0x0400088A RID: 2186
			private WorldSave result;
		}
	}
}
