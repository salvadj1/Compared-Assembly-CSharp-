using System;
using System.Diagnostics;
using System.IO;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;
using RustProto.Helpers;
using UnityEngine;

namespace RustProto
{
	// Token: 0x02000221 RID: 545
	[DebuggerNonUserCode]
	public sealed class Vector : GeneratedMessage<Vector, Vector.Builder>
	{
		// Token: 0x06001344 RID: 4932 RVA: 0x00042600 File Offset: 0x00040800
		private Vector()
		{
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x00042610 File Offset: 0x00040810
		static Vector()
		{
			object.ReferenceEquals(Common.Descriptor, null);
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x00042678 File Offset: 0x00040878
		public static Recycler<Vector, Vector.Builder> Recycler()
		{
			return Recycler<Vector, Vector.Builder>.Manufacture();
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001347 RID: 4935 RVA: 0x00042680 File Offset: 0x00040880
		public static Vector DefaultInstance
		{
			get
			{
				return Vector.defaultInstance;
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06001348 RID: 4936 RVA: 0x00042688 File Offset: 0x00040888
		public override Vector DefaultInstanceForType
		{
			get
			{
				return Vector.DefaultInstance;
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06001349 RID: 4937 RVA: 0x00042690 File Offset: 0x00040890
		protected override Vector ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x0600134A RID: 4938 RVA: 0x00042694 File Offset: 0x00040894
		public static MessageDescriptor Descriptor
		{
			get
			{
				return Common.internal__static_RustProto_Vector__Descriptor;
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x0600134B RID: 4939 RVA: 0x0004269C File Offset: 0x0004089C
		protected override FieldAccessorTable<Vector, Vector.Builder> InternalFieldAccessors
		{
			get
			{
				return Common.internal__static_RustProto_Vector__FieldAccessorTable;
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x0600134C RID: 4940 RVA: 0x000426A4 File Offset: 0x000408A4
		public bool HasX
		{
			get
			{
				return this.hasX;
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x0600134D RID: 4941 RVA: 0x000426AC File Offset: 0x000408AC
		public float X
		{
			get
			{
				return this.x_;
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x0600134E RID: 4942 RVA: 0x000426B4 File Offset: 0x000408B4
		public bool HasY
		{
			get
			{
				return this.hasY;
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x0600134F RID: 4943 RVA: 0x000426BC File Offset: 0x000408BC
		public float Y
		{
			get
			{
				return this.y_;
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06001350 RID: 4944 RVA: 0x000426C4 File Offset: 0x000408C4
		public bool HasZ
		{
			get
			{
				return this.hasZ;
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001351 RID: 4945 RVA: 0x000426CC File Offset: 0x000408CC
		public float Z
		{
			get
			{
				return this.z_;
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06001352 RID: 4946 RVA: 0x000426D4 File Offset: 0x000408D4
		public override bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x000426D8 File Offset: 0x000408D8
		public override void WriteTo(ICodedOutputStream output)
		{
			int serializedSize = this.SerializedSize;
			string[] vectorFieldNames = Vector._vectorFieldNames;
			if (this.hasX)
			{
				output.WriteFloat(1, vectorFieldNames[0], this.X);
			}
			if (this.hasY)
			{
				output.WriteFloat(2, vectorFieldNames[1], this.Y);
			}
			if (this.hasZ)
			{
				output.WriteFloat(3, vectorFieldNames[2], this.Z);
			}
			this.UnknownFields.WriteTo(output);
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06001354 RID: 4948 RVA: 0x00042750 File Offset: 0x00040950
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
				if (this.hasX)
				{
					num += CodedOutputStream.ComputeFloatSize(1, this.X);
				}
				if (this.hasY)
				{
					num += CodedOutputStream.ComputeFloatSize(2, this.Y);
				}
				if (this.hasZ)
				{
					num += CodedOutputStream.ComputeFloatSize(3, this.Z);
				}
				num += this.UnknownFields.SerializedSize;
				this.memoizedSerializedSize = num;
				return num;
			}
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x000427D4 File Offset: 0x000409D4
		public static Vector ParseFrom(ByteString data)
		{
			return Vector.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x000427E8 File Offset: 0x000409E8
		public static Vector ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return Vector.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x000427FC File Offset: 0x000409FC
		public static Vector ParseFrom(byte[] data)
		{
			return Vector.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x00042810 File Offset: 0x00040A10
		public static Vector ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return Vector.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x00042824 File Offset: 0x00040A24
		public static Vector ParseFrom(Stream input)
		{
			return Vector.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x00042838 File Offset: 0x00040A38
		public static Vector ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return Vector.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x0004284C File Offset: 0x00040A4C
		public static Vector ParseDelimitedFrom(Stream input)
		{
			return Vector.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x00042860 File Offset: 0x00040A60
		public static Vector ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return Vector.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x00042874 File Offset: 0x00040A74
		public static Vector ParseFrom(ICodedInputStream input)
		{
			return Vector.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x00042888 File Offset: 0x00040A88
		public static Vector ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return Vector.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x0004289C File Offset: 0x00040A9C
		private Vector MakeReadOnly()
		{
			return this;
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x000428A0 File Offset: 0x00040AA0
		public static Vector.Builder CreateBuilder()
		{
			return new Vector.Builder();
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x000428A8 File Offset: 0x00040AA8
		public override Vector.Builder ToBuilder()
		{
			return Vector.CreateBuilder(this);
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x000428B0 File Offset: 0x00040AB0
		public override Vector.Builder CreateBuilderForType()
		{
			return new Vector.Builder();
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x000428B8 File Offset: 0x00040AB8
		public static Vector.Builder CreateBuilder(Vector prototype)
		{
			return new Vector.Builder(prototype);
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x000428C0 File Offset: 0x00040AC0
		public static implicit operator Vector(Vector3 v)
		{
			Vector result;
			using (Recycler<Vector, Vector.Builder> recycler = Vector.Recycler())
			{
				Vector.Builder builder = recycler.OpenBuilder();
				builder.SetX(v.x);
				builder.SetY(v.y);
				builder.SetZ(v.z);
				result = builder.Build();
			}
			return result;
		}

		// Token: 0x040009D7 RID: 2519
		public const int XFieldNumber = 1;

		// Token: 0x040009D8 RID: 2520
		public const int YFieldNumber = 2;

		// Token: 0x040009D9 RID: 2521
		public const int ZFieldNumber = 3;

		// Token: 0x040009DA RID: 2522
		private static readonly Vector defaultInstance = new Vector().MakeReadOnly();

		// Token: 0x040009DB RID: 2523
		private static readonly string[] _vectorFieldNames = new string[]
		{
			"x",
			"y",
			"z"
		};

		// Token: 0x040009DC RID: 2524
		private static readonly uint[] _vectorFieldTags = new uint[]
		{
			13u,
			21u,
			29u
		};

		// Token: 0x040009DD RID: 2525
		private bool hasX;

		// Token: 0x040009DE RID: 2526
		private float x_;

		// Token: 0x040009DF RID: 2527
		private bool hasY;

		// Token: 0x040009E0 RID: 2528
		private float y_;

		// Token: 0x040009E1 RID: 2529
		private bool hasZ;

		// Token: 0x040009E2 RID: 2530
		private float z_;

		// Token: 0x040009E3 RID: 2531
		private int memoizedSerializedSize = -1;

		// Token: 0x02000222 RID: 546
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<Vector, Vector.Builder>
		{
			// Token: 0x06001365 RID: 4965 RVA: 0x00042940 File Offset: 0x00040B40
			public Builder()
			{
				this.result = Vector.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x06001366 RID: 4966 RVA: 0x0004295C File Offset: 0x00040B5C
			internal Builder(Vector cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x06001367 RID: 4967 RVA: 0x00042974 File Offset: 0x00040B74
			public void Set(Vector3 value)
			{
				this.SetX(value.x);
				this.SetY(value.y);
				this.SetZ(value.z);
			}

			// Token: 0x1700057B RID: 1403
			// (get) Token: 0x06001368 RID: 4968 RVA: 0x000429AC File Offset: 0x00040BAC
			protected override Vector.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x06001369 RID: 4969 RVA: 0x000429B0 File Offset: 0x00040BB0
			private Vector PrepareBuilder()
			{
				if (this.resultIsReadOnly)
				{
					Vector other = this.result;
					this.result = new Vector();
					this.resultIsReadOnly = false;
					this.MergeFrom(other);
				}
				return this.result;
			}

			// Token: 0x1700057C RID: 1404
			// (get) Token: 0x0600136A RID: 4970 RVA: 0x000429F0 File Offset: 0x00040BF0
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x1700057D RID: 1405
			// (get) Token: 0x0600136B RID: 4971 RVA: 0x00042A00 File Offset: 0x00040C00
			protected override Vector MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x0600136C RID: 4972 RVA: 0x00042A08 File Offset: 0x00040C08
			public override Vector.Builder Clear()
			{
				this.result = Vector.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x0600136D RID: 4973 RVA: 0x00042A20 File Offset: 0x00040C20
			public override Vector.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new Vector.Builder(this.result);
				}
				return new Vector.Builder().MergeFrom(this.result);
			}

			// Token: 0x1700057E RID: 1406
			// (get) Token: 0x0600136E RID: 4974 RVA: 0x00042A4C File Offset: 0x00040C4C
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return Vector.Descriptor;
				}
			}

			// Token: 0x1700057F RID: 1407
			// (get) Token: 0x0600136F RID: 4975 RVA: 0x00042A54 File Offset: 0x00040C54
			public override Vector DefaultInstanceForType
			{
				get
				{
					return Vector.DefaultInstance;
				}
			}

			// Token: 0x06001370 RID: 4976 RVA: 0x00042A5C File Offset: 0x00040C5C
			public override Vector BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x06001371 RID: 4977 RVA: 0x00042A90 File Offset: 0x00040C90
			public override Vector.Builder MergeFrom(IMessage other)
			{
				if (other is Vector)
				{
					return this.MergeFrom((Vector)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x06001372 RID: 4978 RVA: 0x00042AB4 File Offset: 0x00040CB4
			public override Vector.Builder MergeFrom(Vector other)
			{
				if (other == Vector.DefaultInstance)
				{
					return this;
				}
				this.PrepareBuilder();
				if (other.HasX)
				{
					this.X = other.X;
				}
				if (other.HasY)
				{
					this.Y = other.Y;
				}
				if (other.HasZ)
				{
					this.Z = other.Z;
				}
				this.MergeUnknownFields(other.UnknownFields);
				return this;
			}

			// Token: 0x06001373 RID: 4979 RVA: 0x00042B28 File Offset: 0x00040D28
			public override Vector.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x06001374 RID: 4980 RVA: 0x00042B38 File Offset: 0x00040D38
			public override Vector.Builder MergeFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
			{
				this.PrepareBuilder();
				UnknownFieldSet.Builder builder = null;
				uint num;
				string text;
				while (input.ReadTag(ref num, ref text))
				{
					if (num == 0u && text != null)
					{
						int num2 = Array.BinarySearch<string>(Vector._vectorFieldNames, text, StringComparer.Ordinal);
						if (num2 < 0)
						{
							if (builder == null)
							{
								builder = UnknownFieldSet.CreateBuilder(this.UnknownFields);
							}
							this.ParseUnknownField(input, builder, extensionRegistry, num, text);
							continue;
						}
						num = Vector._vectorFieldTags[num2];
					}
					uint num3 = num;
					if (num3 == 0u)
					{
						throw InvalidProtocolBufferException.InvalidTag();
					}
					if (num3 != 13u)
					{
						if (num3 != 21u)
						{
							if (num3 != 29u)
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
								this.result.hasZ = input.ReadFloat(ref this.result.z_);
							}
						}
						else
						{
							this.result.hasY = input.ReadFloat(ref this.result.y_);
						}
					}
					else
					{
						this.result.hasX = input.ReadFloat(ref this.result.x_);
					}
				}
				if (builder != null)
				{
					this.UnknownFields = builder.Build();
				}
				return this;
			}

			// Token: 0x17000580 RID: 1408
			// (get) Token: 0x06001375 RID: 4981 RVA: 0x00042CA0 File Offset: 0x00040EA0
			public bool HasX
			{
				get
				{
					return this.result.hasX;
				}
			}

			// Token: 0x17000581 RID: 1409
			// (get) Token: 0x06001376 RID: 4982 RVA: 0x00042CB0 File Offset: 0x00040EB0
			// (set) Token: 0x06001377 RID: 4983 RVA: 0x00042CC0 File Offset: 0x00040EC0
			public float X
			{
				get
				{
					return this.result.X;
				}
				set
				{
					this.SetX(value);
				}
			}

			// Token: 0x06001378 RID: 4984 RVA: 0x00042CCC File Offset: 0x00040ECC
			public Vector.Builder SetX(float value)
			{
				this.PrepareBuilder();
				this.result.hasX = true;
				this.result.x_ = value;
				return this;
			}

			// Token: 0x06001379 RID: 4985 RVA: 0x00042CFC File Offset: 0x00040EFC
			public Vector.Builder ClearX()
			{
				this.PrepareBuilder();
				this.result.hasX = false;
				this.result.x_ = 0f;
				return this;
			}

			// Token: 0x17000582 RID: 1410
			// (get) Token: 0x0600137A RID: 4986 RVA: 0x00042D30 File Offset: 0x00040F30
			public bool HasY
			{
				get
				{
					return this.result.hasY;
				}
			}

			// Token: 0x17000583 RID: 1411
			// (get) Token: 0x0600137B RID: 4987 RVA: 0x00042D40 File Offset: 0x00040F40
			// (set) Token: 0x0600137C RID: 4988 RVA: 0x00042D50 File Offset: 0x00040F50
			public float Y
			{
				get
				{
					return this.result.Y;
				}
				set
				{
					this.SetY(value);
				}
			}

			// Token: 0x0600137D RID: 4989 RVA: 0x00042D5C File Offset: 0x00040F5C
			public Vector.Builder SetY(float value)
			{
				this.PrepareBuilder();
				this.result.hasY = true;
				this.result.y_ = value;
				return this;
			}

			// Token: 0x0600137E RID: 4990 RVA: 0x00042D8C File Offset: 0x00040F8C
			public Vector.Builder ClearY()
			{
				this.PrepareBuilder();
				this.result.hasY = false;
				this.result.y_ = 0f;
				return this;
			}

			// Token: 0x17000584 RID: 1412
			// (get) Token: 0x0600137F RID: 4991 RVA: 0x00042DC0 File Offset: 0x00040FC0
			public bool HasZ
			{
				get
				{
					return this.result.hasZ;
				}
			}

			// Token: 0x17000585 RID: 1413
			// (get) Token: 0x06001380 RID: 4992 RVA: 0x00042DD0 File Offset: 0x00040FD0
			// (set) Token: 0x06001381 RID: 4993 RVA: 0x00042DE0 File Offset: 0x00040FE0
			public float Z
			{
				get
				{
					return this.result.Z;
				}
				set
				{
					this.SetZ(value);
				}
			}

			// Token: 0x06001382 RID: 4994 RVA: 0x00042DEC File Offset: 0x00040FEC
			public Vector.Builder SetZ(float value)
			{
				this.PrepareBuilder();
				this.result.hasZ = true;
				this.result.z_ = value;
				return this;
			}

			// Token: 0x06001383 RID: 4995 RVA: 0x00042E1C File Offset: 0x0004101C
			public Vector.Builder ClearZ()
			{
				this.PrepareBuilder();
				this.result.hasZ = false;
				this.result.z_ = 0f;
				return this;
			}

			// Token: 0x040009E4 RID: 2532
			private bool resultIsReadOnly;

			// Token: 0x040009E5 RID: 2533
			private Vector result;
		}
	}
}
