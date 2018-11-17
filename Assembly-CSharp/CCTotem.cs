using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;

// Token: 0x020002C2 RID: 706
public abstract class CCTotem : MonoBehaviour
{
	// Token: 0x060018EB RID: 6379 RVA: 0x0005F8B4 File Offset: 0x0005DAB4
	internal CCTotem()
	{
	}

	// Token: 0x060018EC RID: 6380 RVA: 0x0005F8BC File Offset: 0x0005DABC
	private static string VS(Vector3 v)
	{
		return string.Format("[{0},{1},{2}]", v.x, v.y, v.z);
	}

	// Token: 0x1700071E RID: 1822
	// (get) Token: 0x060018ED RID: 6381 RVA: 0x0005F8F8 File Offset: 0x0005DAF8
	protected internal global::CCTotem.TotemicObject totemicObject
	{
		get
		{
			return this._Object;
		}
	}

	// Token: 0x1700071F RID: 1823
	// (get) Token: 0x060018EE RID: 6382
	internal abstract global::CCTotem.TotemicObject _Object { get; }

	// Token: 0x060018EF RID: 6383 RVA: 0x0005F900 File Offset: 0x0005DB00
	private static void DestroyCCDesc(global::CCTotemPole ScriptOwner, ref global::CCDesc CCDesc)
	{
		if (ScriptOwner)
		{
			ScriptOwner.DestroyCCDesc(ref CCDesc);
		}
		else
		{
			global::CCDesc ccdesc = CCDesc;
			CCDesc = null;
			if (ccdesc)
			{
				Object.Destroy(ccdesc.gameObject);
			}
		}
	}

	// Token: 0x04000D78 RID: 3448
	protected internal const int kMaxTotemicFiguresPerTotemPole = 8;

	// Token: 0x04000D79 RID: 3449
	protected internal const CollisionFlags kCF_Sides = 1;

	// Token: 0x04000D7A RID: 3450
	protected internal const CollisionFlags kCF_Above = 2;

	// Token: 0x04000D7B RID: 3451
	protected internal const CollisionFlags kCF_Below = 4;

	// Token: 0x04000D7C RID: 3452
	protected internal const CollisionFlags kCF_None = 0;

	// Token: 0x020002C3 RID: 707
	protected internal struct Configuration
	{
		// Token: 0x060018F0 RID: 6384 RVA: 0x0005F940 File Offset: 0x0005DB40
		public Configuration(ref global::CCTotem.Initialization totem)
		{
			if (!totem.figurePrefab)
			{
				throw new ArgumentException("figurePrefab was missing", "totem");
			}
			this.totem = totem;
			this.totemMinHeight = totem.minHeight;
			this.totemMaxHeight = totem.maxHeight;
			this.totemBottomBufferUnits = totem.bottomBufferUnits;
			if (this.totemMinHeight >= this.totemMaxHeight)
			{
				throw new ArgumentException("maxHeight is less than or equal to minHeight", "totem");
			}
			if (Mathf.Approximately(this.totemBottomBufferUnits, 0f))
			{
				this.totemBottomBufferUnits = 0f;
			}
			else if (this.totemBottomBufferUnits < 0f)
			{
				throw new ArgumentException("bottomBufferPercent must not be less than zero", "totem");
			}
			global::CCDesc figurePrefab = totem.figurePrefab;
			this.figureSkinWidth = figurePrefab.skinWidth;
			this.figure2SkinWidth = this.figureSkinWidth + this.figureSkinWidth;
			this.figureRadius = figurePrefab.radius;
			this.figureSkinnedRadius = this.figureRadius + this.figureSkinWidth;
			this.figureDiameter = this.figureRadius + this.figureRadius;
			this.figureSkinnedDiameter = this.figureSkinnedRadius + this.figureSkinnedRadius;
			this.figureHeight = figurePrefab.height;
			if (this.figureHeight <= this.figureDiameter)
			{
				throw new ArgumentException("The CCDesc(CharacterController) Prefab is a sphere, not a capsule. Thus cannot be expanded on the totem pole", "totem");
			}
			this.figureSkinnedHeight = this.figureHeight + this.figure2SkinWidth;
			if (this.figureSkinnedHeight > this.totemMinHeight && !Mathf.Approximately(this.totemMinHeight, this.figureSkinnedHeight))
			{
				throw new ArgumentException("minHeight is too small. It must be at least the size of the CCDesc(CharacterController) prefab's [height+(skinWidth*2)]", "totem");
			}
			this.figureSlideHeight = this.figureSkinnedHeight - this.figureSkinnedDiameter;
			if (this.figureSlideHeight <= 0f)
			{
				throw new ArgumentException("The CCDesc(CharacterController) Prefab has limited height availability. Thus cannot be expanded on the totem pole", "totem");
			}
			this.figureFixedHeight = this.figureSkinnedHeight - this.figureSlideHeight;
			this.poleTopBufferAmount = this.figureSkinnedRadius;
			this.poleBottomBufferUnitSize = this.figureSlideHeight * 0.5f;
			this.poleBottomBufferAmount = this.poleBottomBufferUnitSize * this.totemBottomBufferUnits;
			if (this.poleBottomBufferAmount > this.figureSlideHeight)
			{
				if (!Mathf.Approximately(this.poleBottomBufferAmount, this.figureSlideHeight))
				{
					throw new ArgumentException("The bottomBuffer was too large and landed outside of sliding height area of the capsule", "totem");
				}
				this.poleBottomBufferAmount = this.figureSlideHeight;
				this.totemBottomBufferUnits = this.figureSlideHeight / this.poleBottomBufferUnitSize;
			}
			this.poleBottomBufferHeight = this.figureSkinnedRadius + this.poleBottomBufferAmount;
			this.poleMostContractedHeightPossible = this.figureSkinnedHeight + this.poleBottomBufferAmount;
			if (this.poleMostContractedHeightPossible > this.totemMinHeight)
			{
				if (!Mathf.Approximately(this.poleMostContractedHeightPossible, this.totemMinHeight))
				{
					throw new ArgumentException("bottomBufferPercent value is too high with the current setup, results in contracted height greater than totem.minHeight.", "totem");
				}
				this.totemMinHeight = this.poleMostContractedHeightPossible;
			}
			this.poleContractedHeight = Mathf.Max(this.poleMostContractedHeightPossible, this.totemMinHeight);
			this.poleContractedHeightFromMostContractedHeightPossible = this.poleContractedHeight - this.poleMostContractedHeightPossible;
			this.poleExpandedHeight = Mathf.Max(this.poleContractedHeight, this.totemMaxHeight);
			this.poleExpandedHeightFromMostContractedHeightPossible = this.poleExpandedHeight - this.poleMostContractedHeightPossible;
			if (Mathf.Approximately(this.poleContractedHeightFromMostContractedHeightPossible, this.poleExpandedHeightFromMostContractedHeightPossible))
			{
				throw new ArgumentException("minHeight and maxHeight were too close to eachother to provide reliable contraction/expansion calculations.", "totem");
			}
			if (this.poleContractedHeightFromMostContractedHeightPossible < 0f || this.poleExpandedHeightFromMostContractedHeightPossible < this.poleContractedHeightFromMostContractedHeightPossible)
			{
				throw new ArgumentException("Calculation error with current configuration.", "totem");
			}
			this.poleFixedLength = this.poleBottomBufferHeight + this.poleTopBufferAmount;
			this.poleExpansionLength = this.poleExpandedHeight - this.poleFixedLength;
			this.numSlidingTotemicFigures = Mathf.CeilToInt(this.poleExpansionLength / this.figureSlideHeight);
			if (this.numSlidingTotemicFigures < 1)
			{
				throw new ArgumentException("The current configuration of the CCTotem resulted in no need for more than one CCDesc(CharacterController), thus rejecting usage..", "totem");
			}
			this.poleMostExpandedHeightPossible = this.poleFixedLength + (float)this.numSlidingTotemicFigures * this.figureSlideHeight;
			this.numRequiredTotemicFigures = 1 + this.numSlidingTotemicFigures;
			if (this.numRequiredTotemicFigures > 8)
			{
				throw new ArgumentOutOfRangeException("totem", this.numRequiredTotemicFigures, "The current configuration of the CCTotem resulted in more than the max number of TotemicFigure's allowed :" + 8);
			}
			Vector3 center = figurePrefab.center;
			this.figureOriginOffsetCenter = new Vector3(0f - center.x, 0f - center.y, 0f - center.z);
			this.figureOriginOffsetBottom = new Vector3(this.figureOriginOffsetCenter.x, 0f - (center.y - this.figureSkinnedHeight / 2f), this.figureOriginOffsetCenter.z);
			this.figureOriginOffsetTop = new Vector3(this.figureOriginOffsetCenter.x, 0f - (center.y + this.figureSkinnedHeight / 2f), this.figureOriginOffsetCenter.z);
		}

		// Token: 0x060018F1 RID: 6385 RVA: 0x0005FE2C File Offset: 0x0005E02C
		public override string ToString()
		{
			return global::CCTotem.ToStringHelper<global::CCTotem.Configuration>.GetString(this);
		}

		// Token: 0x04000D7D RID: 3453
		public readonly global::CCTotem.Initialization totem;

		// Token: 0x04000D7E RID: 3454
		public readonly float totemMinHeight;

		// Token: 0x04000D7F RID: 3455
		public readonly float totemMaxHeight;

		// Token: 0x04000D80 RID: 3456
		public readonly float totemBottomBufferUnits;

		// Token: 0x04000D81 RID: 3457
		public readonly float figureSkinWidth;

		// Token: 0x04000D82 RID: 3458
		public readonly float figure2SkinWidth;

		// Token: 0x04000D83 RID: 3459
		public readonly float figureRadius;

		// Token: 0x04000D84 RID: 3460
		public readonly float figureSkinnedRadius;

		// Token: 0x04000D85 RID: 3461
		public readonly float figureDiameter;

		// Token: 0x04000D86 RID: 3462
		public readonly float figureSkinnedDiameter;

		// Token: 0x04000D87 RID: 3463
		public readonly float figureHeight;

		// Token: 0x04000D88 RID: 3464
		public readonly float figureSkinnedHeight;

		// Token: 0x04000D89 RID: 3465
		public readonly float figureSlideHeight;

		// Token: 0x04000D8A RID: 3466
		public readonly float figureFixedHeight;

		// Token: 0x04000D8B RID: 3467
		public readonly float poleTopBufferAmount;

		// Token: 0x04000D8C RID: 3468
		public readonly float poleBottomBufferAmount;

		// Token: 0x04000D8D RID: 3469
		public readonly float poleBottomBufferHeight;

		// Token: 0x04000D8E RID: 3470
		public readonly float poleBottomBufferUnitSize;

		// Token: 0x04000D8F RID: 3471
		public readonly float poleMostContractedHeightPossible;

		// Token: 0x04000D90 RID: 3472
		public readonly float poleMostExpandedHeightPossible;

		// Token: 0x04000D91 RID: 3473
		public readonly float poleContractedHeight;

		// Token: 0x04000D92 RID: 3474
		public readonly float poleContractedHeightFromMostContractedHeightPossible;

		// Token: 0x04000D93 RID: 3475
		public readonly float poleExpandedHeight;

		// Token: 0x04000D94 RID: 3476
		public readonly float poleExpandedHeightFromMostContractedHeightPossible;

		// Token: 0x04000D95 RID: 3477
		public readonly float poleFixedLength;

		// Token: 0x04000D96 RID: 3478
		public readonly float poleExpansionLength;

		// Token: 0x04000D97 RID: 3479
		public readonly int numRequiredTotemicFigures;

		// Token: 0x04000D98 RID: 3480
		public readonly int numSlidingTotemicFigures;

		// Token: 0x04000D99 RID: 3481
		public readonly Vector3 figureOriginOffsetBottom;

		// Token: 0x04000D9A RID: 3482
		public readonly Vector3 figureOriginOffsetTop;

		// Token: 0x04000D9B RID: 3483
		public readonly Vector3 figureOriginOffsetCenter;
	}

	// Token: 0x020002C4 RID: 708
	protected internal struct Contraction
	{
		// Token: 0x060018F2 RID: 6386 RVA: 0x0005FE40 File Offset: 0x0005E040
		private Contraction(float Contracted, float Expanded, float Range, float InverseRange)
		{
			this.Contracted = Contracted;
			this.Expanded = Expanded;
			this.Range = Range;
			this.InverseRange = InverseRange;
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x0005FE60 File Offset: 0x0005E060
		public global::CCTotem.Expansion ExpansionForValue(float Value)
		{
			global::CCTotem.Expansion result;
			if (Value <= this.Contracted)
			{
				result = new global::CCTotem.Expansion(this.Contracted, 0f, 0f);
			}
			else if (Value >= this.Expanded)
			{
				result = new global::CCTotem.Expansion(this.Expanded, 1f, this.Range);
			}
			else
			{
				float num = Value - this.Contracted;
				float fractionExpanded = num / this.Range;
				result = new global::CCTotem.Expansion(Value, fractionExpanded, num);
			}
			return result;
		}

		// Token: 0x060018F4 RID: 6388 RVA: 0x0005FEDC File Offset: 0x0005E0DC
		public global::CCTotem.Expansion ExpansionForFraction(float FractionExpanded)
		{
			global::CCTotem.Expansion result;
			if (FractionExpanded <= 0f)
			{
				result = new global::CCTotem.Expansion(this.Contracted, 0f, 0f);
			}
			else if (FractionExpanded >= 1f)
			{
				result = new global::CCTotem.Expansion(this.Expanded, 1f, this.Range);
			}
			else
			{
				float num = FractionExpanded * this.Range;
				float value = this.Contracted + num;
				result = new global::CCTotem.Expansion(value, FractionExpanded, num);
			}
			return result;
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x0005FF58 File Offset: 0x0005E158
		public global::CCTotem.Expansion ExpansionForAmount(float Amount)
		{
			global::CCTotem.Expansion result;
			if (Amount <= 0f)
			{
				result = new global::CCTotem.Expansion(this.Contracted, 0f, 0f);
			}
			else if (Amount >= this.Range)
			{
				result = new global::CCTotem.Expansion(this.Expanded, 1f, this.Range);
			}
			else
			{
				float fractionExpanded = Amount / this.Range;
				float value = this.Contracted + Amount;
				result = new global::CCTotem.Expansion(value, fractionExpanded, Amount);
			}
			return result;
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x0005FFD4 File Offset: 0x0005E1D4
		public override string ToString()
		{
			return string.Format("{{Contracted={0},Expanded={1},Range={2},InverseRange={3}}}", new object[]
			{
				this.Contracted,
				this.Expanded,
				this.Range,
				this.InverseRange
			});
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x0006002C File Offset: 0x0005E22C
		public static global::CCTotem.Contraction Define(float Contracted, float Expanded)
		{
			if (Mathf.Approximately(Contracted, Expanded))
			{
				throw new ArgumentOutOfRangeException("Contracted", "approximately equal to Expanded");
			}
			float num = Expanded - Contracted;
			return new global::CCTotem.Contraction(Contracted, Expanded, num, (float)(1.0 / (double)num));
		}

		// Token: 0x04000D9C RID: 3484
		public readonly float Contracted;

		// Token: 0x04000D9D RID: 3485
		public readonly float Expanded;

		// Token: 0x04000D9E RID: 3486
		public readonly float Range;

		// Token: 0x04000D9F RID: 3487
		public readonly float InverseRange;
	}

	// Token: 0x020002C5 RID: 709
	protected internal struct Direction
	{
		// Token: 0x060018F8 RID: 6392 RVA: 0x00060070 File Offset: 0x0005E270
		public Direction(global::CCTotem.TotemicFigure TotemicFigure)
		{
			if (object.ReferenceEquals(TotemicFigure, null))
			{
				throw new ArgumentNullException("TotemicFigure");
			}
			this.TotemicFigure = TotemicFigure;
			this.Exists = true;
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x060018F9 RID: 6393 RVA: 0x00060098 File Offset: 0x0005E298
		public static global::CCTotem.Direction None
		{
			get
			{
				return default(global::CCTotem.Direction);
			}
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x000600B0 File Offset: 0x0005E2B0
		public override string ToString()
		{
			return (!this.Exists) ? "{Does Not Exist}" : string.Format("{{TotemicFigure={0}}}", this.TotemicFigure);
		}

		// Token: 0x04000DA0 RID: 3488
		public readonly bool Exists;

		// Token: 0x04000DA1 RID: 3489
		public readonly global::CCTotem.TotemicFigure TotemicFigure;
	}

	// Token: 0x020002C6 RID: 710
	protected internal struct Ends<T>
	{
		// Token: 0x060018FB RID: 6395 RVA: 0x000600D8 File Offset: 0x0005E2D8
		public Ends(T Bottom, T Top)
		{
			this.Bottom = Bottom;
			this.Top = Top;
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x000600E8 File Offset: 0x0005E2E8
		public override string ToString()
		{
			return string.Format("{{Bottom={0},Top={1}}}", this.Bottom, this.Top);
		}

		// Token: 0x04000DA2 RID: 3490
		public T Bottom;

		// Token: 0x04000DA3 RID: 3491
		public T Top;
	}

	// Token: 0x020002C7 RID: 711
	protected internal struct Expansion
	{
		// Token: 0x060018FD RID: 6397 RVA: 0x00060118 File Offset: 0x0005E318
		internal Expansion(float Value, float FractionExpanded, float Amount)
		{
			this.Value = Value;
			this.FractionExpanded = FractionExpanded;
			this.Amount = Amount;
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x00060130 File Offset: 0x0005E330
		public override string ToString()
		{
			return string.Format("{{Value={0},FractionExpanded={1},Amount={2}}}", this.Value, this.FractionExpanded, this.Amount);
		}

		// Token: 0x04000DA4 RID: 3492
		public readonly float Value;

		// Token: 0x04000DA5 RID: 3493
		public readonly float FractionExpanded;

		// Token: 0x04000DA6 RID: 3494
		public readonly float Amount;
	}

	// Token: 0x020002C8 RID: 712
	protected internal struct Initialization
	{
		// Token: 0x060018FF RID: 6399 RVA: 0x00060160 File Offset: 0x0005E360
		public Initialization(global::CCTotemPole totemPole, global::CCDesc figurePrefab, float minHeight, float maxHeight, float initialHeight, float bottomBufferUnits)
		{
			this.totemPole = totemPole;
			this.figurePrefab = figurePrefab;
			this.minHeight = minHeight;
			this.maxHeight = maxHeight;
			this.initialHeight = initialHeight;
			this.bottomBufferUnits = bottomBufferUnits;
			this.nonDefault = true;
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x000601A4 File Offset: 0x0005E3A4
		public override string ToString()
		{
			return global::CCTotem.ToStringHelper<global::CCTotem.Initialization>.GetString(this);
		}

		// Token: 0x04000DA7 RID: 3495
		public readonly global::CCTotemPole totemPole;

		// Token: 0x04000DA8 RID: 3496
		public readonly global::CCDesc figurePrefab;

		// Token: 0x04000DA9 RID: 3497
		public readonly float minHeight;

		// Token: 0x04000DAA RID: 3498
		public readonly float maxHeight;

		// Token: 0x04000DAB RID: 3499
		public readonly float initialHeight;

		// Token: 0x04000DAC RID: 3500
		public readonly float bottomBufferUnits;

		// Token: 0x04000DAD RID: 3501
		public readonly bool nonDefault;
	}

	// Token: 0x020002C9 RID: 713
	public struct PositionPlacement
	{
		// Token: 0x06001901 RID: 6401 RVA: 0x000601B8 File Offset: 0x0005E3B8
		public PositionPlacement(Vector3 Bottom, Vector3 Top, Vector3 ColliderPosition, float OriginalHeight)
		{
			this.bottom = Bottom;
			this.top = Top;
			this.colliderCenter = ColliderPosition;
			this.height = Top.y - Bottom.y;
			this.originalHeight = OriginalHeight;
			this.originalTop.x = Bottom.x;
			this.originalTop.y = Bottom.y + OriginalHeight;
			this.originalTop.z = Bottom.z;
		}

		// Token: 0x04000DAE RID: 3502
		public Vector3 bottom;

		// Token: 0x04000DAF RID: 3503
		public Vector3 top;

		// Token: 0x04000DB0 RID: 3504
		public Vector3 colliderCenter;

		// Token: 0x04000DB1 RID: 3505
		public float height;

		// Token: 0x04000DB2 RID: 3506
		public float originalHeight;

		// Token: 0x04000DB3 RID: 3507
		public Vector3 originalTop;
	}

	// Token: 0x020002CA RID: 714
	protected internal struct Route
	{
		// Token: 0x06001902 RID: 6402 RVA: 0x00060230 File Offset: 0x0005E430
		public Route(global::CCTotem.Direction Up, global::CCTotem.Direction At, global::CCTotem.Direction Down)
		{
			this.Up = Up;
			this.At = At;
			this.Down = Down;
		}

		// Token: 0x06001903 RID: 6403 RVA: 0x00060248 File Offset: 0x0005E448
		public unsafe bool GetUp(out global::CCTotem.Route route)
		{
			if (this.Up.Exists)
			{
				route = *this.Up.TotemicFigure;
				return true;
			}
			route = new global::CCTotem.Route(global::CCTotem.Direction.None, global::CCTotem.Direction.None, this.At);
			return false;
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x0006029C File Offset: 0x0005E49C
		public unsafe bool GetDown(out global::CCTotem.Route route)
		{
			if (this.Down.Exists)
			{
				route = *this.Down.TotemicFigure;
				return true;
			}
			route = new global::CCTotem.Route(this.At, global::CCTotem.Direction.None, global::CCTotem.Direction.None);
			return false;
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06001905 RID: 6405 RVA: 0x000602F0 File Offset: 0x0005E4F0
		public IEnumerable<global::CCTotem.TotemicFigure> EnumerateUpInclusive
		{
			get
			{
				global::CCTotem.Route it = ref this;
				while (it.At.Exists)
				{
					yield return it.At.TotemicFigure;
					it.GetUp(out it);
				}
				yield break;
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06001906 RID: 6406 RVA: 0x00060318 File Offset: 0x0005E518
		public IEnumerable<global::CCTotem.TotemicFigure> EnumerateUp
		{
			get
			{
				global::CCTotem.Route it;
				if (this.GetUp(out it))
				{
					while (it.At.Exists)
					{
						yield return it.At.TotemicFigure;
						it.GetUp(out it);
					}
				}
				yield break;
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06001907 RID: 6407 RVA: 0x00060340 File Offset: 0x0005E540
		public IEnumerable<global::CCTotem.TotemicFigure> EnumerateDownInclusive
		{
			get
			{
				global::CCTotem.Route it = ref this;
				while (it.At.Exists)
				{
					yield return it.At.TotemicFigure;
					it.GetDown(out it);
				}
				yield break;
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06001908 RID: 6408 RVA: 0x00060368 File Offset: 0x0005E568
		public IEnumerable<global::CCTotem.TotemicFigure> EnumerateDown
		{
			get
			{
				global::CCTotem.Route it;
				if (this.GetUp(out it))
				{
					while (it.At.Exists)
					{
						yield return it.At.TotemicFigure;
						it.GetDown(out it);
					}
				}
				yield break;
			}
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x00060390 File Offset: 0x0005E590
		public override string ToString()
		{
			return string.Format("{{Up={0},At={1},Down={2}}}", this.Up, this.At, this.Down);
		}

		// Token: 0x04000DB4 RID: 3508
		public readonly global::CCTotem.Direction Up;

		// Token: 0x04000DB5 RID: 3509
		public readonly global::CCTotem.Direction At;

		// Token: 0x04000DB6 RID: 3510
		public readonly global::CCTotem.Direction Down;
	}

	// Token: 0x020002CF RID: 719
	public abstract class TotemicObject
	{
		// Token: 0x0600192A RID: 6442 RVA: 0x000607E0 File Offset: 0x0005E9E0
		internal TotemicObject()
		{
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x0600192B RID: 6443 RVA: 0x000607E8 File Offset: 0x0005E9E8
		protected internal global::CCTotem Script
		{
			get
			{
				return this._Script;
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x0600192C RID: 6444
		internal abstract global::CCTotem _Script { get; }
	}

	// Token: 0x020002D0 RID: 720
	public abstract class TotemicObject<CCTotemScript> : global::CCTotem.TotemicObject where CCTotemScript : global::CCTotem, new()
	{
		// Token: 0x0600192D RID: 6445 RVA: 0x000607F0 File Offset: 0x0005E9F0
		internal TotemicObject()
		{
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x0600192E RID: 6446 RVA: 0x000607F8 File Offset: 0x0005E9F8
		internal sealed override global::CCTotem _Script
		{
			get
			{
				return this.Script;
			}
		}

		// Token: 0x04000DC7 RID: 3527
		protected internal new CCTotemScript Script;
	}

	// Token: 0x020002D1 RID: 721
	public abstract class TotemicObject<CCTotemScript, TTotemicObject> : global::CCTotem.TotemicObject<CCTotemScript> where CCTotemScript : global::CCTotem<TTotemicObject, CCTotemScript>, new() where TTotemicObject : global::CCTotem.TotemicObject<CCTotemScript, TTotemicObject>, new()
	{
		// Token: 0x0600192F RID: 6447 RVA: 0x00060808 File Offset: 0x0005EA08
		internal TotemicObject()
		{
		}

		// Token: 0x06001930 RID: 6448
		internal abstract void OnScriptDestroy(CCTotemScript Script);

		// Token: 0x06001931 RID: 6449
		internal abstract void AssignedToScript(CCTotemScript Script);
	}

	// Token: 0x020002D2 RID: 722
	private static class ToStringHelper<T> where T : struct
	{
		// Token: 0x06001932 RID: 6450 RVA: 0x00060810 File Offset: 0x0005EA10
		static ToStringHelper()
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (StringWriter stringWriter = new StringWriter(stringBuilder))
			{
				stringWriter.Write("{{");
				bool flag = true;
				for (int i = 0; i < global::CCTotem.ToStringHelper<T>.allFields.Length; i++)
				{
					if (flag)
					{
						flag = false;
					}
					else
					{
						stringWriter.Write(", ");
					}
					stringWriter.Write("{0}={{{1}}}", global::CCTotem.ToStringHelper<T>.allFields[i].Name, i);
				}
				stringWriter.Write("}}");
			}
			global::CCTotem.ToStringHelper<T>.formatterString = stringBuilder.ToString();
		}

		// Token: 0x06001933 RID: 6451 RVA: 0x000608F0 File Offset: 0x0005EAF0
		public static string GetString(object boxed)
		{
			string result;
			try
			{
				for (int i = 0; i < global::CCTotem.ToStringHelper<T>.allFields.Length; i++)
				{
					global::CCTotem.ToStringHelper<T>.valueHolders[i] = global::CCTotem.ToStringHelper<T>.allFields[i].GetValue(boxed);
				}
				result = string.Format(global::CCTotem.ToStringHelper<T>.formatterString, global::CCTotem.ToStringHelper<T>.valueHolders);
			}
			finally
			{
				for (int j = 0; j < global::CCTotem.ToStringHelper<T>.allFields.Length; j++)
				{
					global::CCTotem.ToStringHelper<T>.valueHolders[j] = null;
				}
			}
			return result;
		}

		// Token: 0x04000DC8 RID: 3528
		private static readonly FieldInfo[] allFields = typeof(T).GetFields();

		// Token: 0x04000DC9 RID: 3529
		private static readonly object[] valueHolders = new object[global::CCTotem.ToStringHelper<T>.allFields.Length];

		// Token: 0x04000DCA RID: 3530
		private static readonly string formatterString;
	}

	// Token: 0x020002D3 RID: 723
	public struct MoveInfo
	{
		// Token: 0x06001934 RID: 6452 RVA: 0x00060984 File Offset: 0x0005EB84
		public MoveInfo(CollisionFlags CollisionFlags, CollisionFlags WorkingCollisionFlags, float WantedHeight, Vector3 BottomMovement, Vector3 TopMovement, global::CCTotem.PositionPlacement PositionPlacement)
		{
			this.CollisionFlags = CollisionFlags;
			this.WorkingCollisionFlags = WorkingCollisionFlags;
			this.WantedHeight = WantedHeight;
			this.BottomMovement = BottomMovement;
			this.TopMovement = TopMovement;
			this.PositionPlacement = PositionPlacement;
		}

		// Token: 0x04000DCB RID: 3531
		public readonly CollisionFlags CollisionFlags;

		// Token: 0x04000DCC RID: 3532
		public readonly CollisionFlags WorkingCollisionFlags;

		// Token: 0x04000DCD RID: 3533
		public readonly float WantedHeight;

		// Token: 0x04000DCE RID: 3534
		public readonly Vector3 BottomMovement;

		// Token: 0x04000DCF RID: 3535
		public readonly Vector3 TopMovement;

		// Token: 0x04000DD0 RID: 3536
		public readonly global::CCTotem.PositionPlacement PositionPlacement;
	}

	// Token: 0x020002D4 RID: 724
	public sealed class TotemPole : global::CCTotem.TotemicObject<global::CCTotemPole, global::CCTotem.TotemPole>
	{
		// Token: 0x06001935 RID: 6453 RVA: 0x000609B4 File Offset: 0x0005EBB4
		[Obsolete("Infrastructure", true)]
		public TotemPole()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x000609C4 File Offset: 0x0005EBC4
		internal TotemPole(ref global::CCTotem.Configuration TotemConfiguration)
		{
			this.Configuration = TotemConfiguration;
			this.TotemicFigures = new global::CCTotem.TotemicFigure[8];
			this.TotemicFigureEnds = global::CCTotem.TotemicFigure.CreateAllTotemicFigures(this);
			this.Contraction = global::CCTotem.Contraction.Define(this.Configuration.poleContractedHeight, this.Configuration.poleExpandedHeight);
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x00060A1C File Offset: 0x0005EC1C
		internal override void AssignedToScript(global::CCTotemPole Script)
		{
			this.Script = Script;
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x00060A28 File Offset: 0x0005EC28
		internal override void OnScriptDestroy(global::CCTotemPole Script)
		{
			if (object.ReferenceEquals(this.Script, Script))
			{
				this.DeleteAllFiguresAndClearScript();
			}
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x00060A44 File Offset: 0x0005EC44
		private void DeleteAllFiguresAndClearScript()
		{
			global::CCTotemPole script = this.Script;
			this.Script = null;
			for (int i = this.Configuration.numRequiredTotemicFigures - 1; i >= 0; i--)
			{
				global::CCTotem.TotemicFigure totemicFigure = this.TotemicFigures[i];
				if (!object.ReferenceEquals(totemicFigure, null))
				{
					if (totemicFigure.TotemPole == this)
					{
						this.TotemicFigures[i].Delete(script);
					}
					else
					{
						this.TotemicFigures[i] = null;
					}
				}
			}
			global::CCTotem.DestroyCCDesc(script, ref this.CCDesc);
			if (script && object.ReferenceEquals(script.totemicObject, this))
			{
				script.totemicObject = null;
			}
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x00060AEC File Offset: 0x0005ECEC
		private global::CCDesc InstantiateCCDesc(Vector3 worldBottom, string name)
		{
			global::CCDesc ccdesc = (global::CCDesc)Object.Instantiate(this.Configuration.totem.figurePrefab, worldBottom, Quaternion.identity);
			if (!string.IsNullOrEmpty(name))
			{
				ccdesc.name = name;
			}
			ccdesc.gameObject.hideFlags = 8;
			ccdesc.detectCollisions = false;
			return ccdesc;
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x00060B48 File Offset: 0x0005ED48
		private global::CCTotemicFigure InstantiateTotemicFigure(Vector3 worldBottom, global::CCTotem.TotemicFigure target)
		{
			worldBottom.y += target.TotemContractionBottom.ExpansionForFraction(this.Expansion.FractionExpanded).Value;
			target.CCDesc = this.InstantiateCCDesc(worldBottom, string.Format("__TotemicFigure{0}", target.BottomUpIndex));
			global::CCTotemicFigure cctotemicFigure = target.CCDesc.gameObject.AddComponent<global::CCTotemicFigure>();
			cctotemicFigure.AssignTotemicObject(target);
			if (this.Script)
			{
				this.Script.ExecuteBinding(target.CCDesc, true);
			}
			return cctotemicFigure;
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x00060BE4 File Offset: 0x0005EDE4
		public void Create()
		{
			float initialHeight = this.Configuration.totem.initialHeight;
			this.Expansion = this.Contraction.ExpansionForValue(initialHeight);
			Vector3 worldBottom = this.Script.transform.position + this.Configuration.figureOriginOffsetBottom;
			this.CCDesc = this.InstantiateCCDesc(worldBottom, "__TotemPole");
			if (this.Script)
			{
				this.Script.ExecuteBinding(this.CCDesc, true);
			}
			for (int i = 0; i < this.Configuration.numRequiredTotemicFigures; i++)
			{
				this.InstantiateTotemicFigure(worldBottom, this.TotemicFigures[i]);
			}
			this.Point.Bottom = this.TotemicFigures[0].BottomOrigin;
			this.Point.Top = this.TotemicFigureEnds.Top.TopOrigin;
			this.CCDesc.ModifyHeight(this.Point.Top.y - this.Point.Bottom.y, false);
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x0600193D RID: 6461 RVA: 0x00060D10 File Offset: 0x0005EF10
		private global::CCDesc CCDescOrPrefab
		{
			get
			{
				return (!this.CCDesc) ? this.Configuration.totem.figurePrefab : this.CCDesc;
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x0600193E RID: 6462 RVA: 0x00060D50 File Offset: 0x0005EF50
		public bool isGrounded
		{
			get
			{
				return this.grounded;
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x0600193F RID: 6463 RVA: 0x00060D58 File Offset: 0x0005EF58
		public Vector3 velocity
		{
			get
			{
				return (!this.CCDesc) ? Vector3.zero : this.CCDesc.velocity;
			}
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06001940 RID: 6464 RVA: 0x00060D80 File Offset: 0x0005EF80
		public CollisionFlags collisionFlags
		{
			get
			{
				return (!this.CCDesc) ? 0 : this.CCDesc.collisionFlags;
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06001941 RID: 6465 RVA: 0x00060DA4 File Offset: 0x0005EFA4
		public float stepOffset
		{
			get
			{
				return this.CCDescOrPrefab.stepOffset;
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06001942 RID: 6466 RVA: 0x00060DB4 File Offset: 0x0005EFB4
		public float slopeLimit
		{
			get
			{
				return this.CCDescOrPrefab.slopeLimit;
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06001943 RID: 6467 RVA: 0x00060DC4 File Offset: 0x0005EFC4
		public float height
		{
			get
			{
				return this.CCDescOrPrefab.height;
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06001944 RID: 6468 RVA: 0x00060DD4 File Offset: 0x0005EFD4
		public float radius
		{
			get
			{
				return this.CCDescOrPrefab.radius;
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06001945 RID: 6469 RVA: 0x00060DE4 File Offset: 0x0005EFE4
		public Vector3 center
		{
			get
			{
				return this.CCDescOrPrefab.center;
			}
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x00060DF4 File Offset: 0x0005EFF4
		public global::CCTotem.MoveInfo Move(Vector3 motion, float height)
		{
			global::CCTotem.Expansion expansion = this.Contraction.ExpansionForValue(height);
			height = expansion.Value;
			CollisionFlags collisionFlags = this.TotemicFigureEnds.Bottom.MoveSweep(motion) & this.TotemicFigureEnds.Bottom.CollisionFlagsMask;
			this.grounded = this.TotemicFigureEnds.Bottom.CCDesc.isGrounded;
			int num = 0;
			for (int i = this.Configuration.numRequiredTotemicFigures - 1; i >= 1; i--)
			{
				Vector3 sweepMovement = this.TotemicFigures[num].SweepMovement;
				collisionFlags |= (this.TotemicFigures[i].MoveSweep(sweepMovement) & this.TotemicFigures[i].CollisionFlagsMask);
				num = i;
			}
			if (this.TotemicFigures[num].SweepMovement != this.TotemicFigures[0].SweepMovement)
			{
				Vector3 sweepMovement2 = this.TotemicFigures[num].SweepMovement;
				for (int j = 0; j < this.Configuration.numRequiredTotemicFigures; j++)
				{
					Vector3 motion2 = sweepMovement2 - this.TotemicFigures[j].SweepMovement;
					collisionFlags |= (this.TotemicFigures[j].MoveSweep(motion2) & this.TotemicFigures[j].CollisionFlagsMask);
				}
			}
			this.Point.Bottom = this.TotemicFigures[0].BottomOrigin;
			this.Point.Top = this.TotemicFigureEnds.Top.TopOrigin;
			this.Expansion = this.Contraction.ExpansionForValue(this.Point.Top.y - this.Point.Bottom.y);
			if (this.Expansion.Value != expansion.Value)
			{
				Vector3 targetTop = this.Point.Bottom + new Vector3(0f, expansion.Value, 0f);
				collisionFlags |= (this.TotemicFigureEnds.Top.MoveWorldTopTo(targetTop) & this.TotemicFigureEnds.Top.CollisionFlagsMask);
				Vector3 topOrigin = this.TotemicFigureEnds.Top.TopOrigin;
				expansion = this.Contraction.ExpansionForValue(topOrigin.y - this.Point.Bottom.y);
				for (int k = this.Configuration.numRequiredTotemicFigures - 2; k > 0; k--)
				{
					global::CCTotem.TotemicFigure totemicFigure = this.TotemicFigures[k];
					Vector3 bottom = this.Point.Bottom;
					bottom.y += totemicFigure.TotemContractionBottom.ExpansionForFraction(expansion.FractionExpanded).Value;
					collisionFlags |= (totemicFigure.MoveWorldBottomTo(bottom) & totemicFigure.CollisionFlagsMask);
				}
				this.Point.Top = this.TotemicFigureEnds.Top.TopOrigin;
				this.Expansion = expansion;
			}
			float effectiveSkinnedHeight = this.CCDesc.effectiveSkinnedHeight;
			Vector3 worldSkinnedBottom = this.CCDesc.worldSkinnedBottom;
			Vector3 worldSkinnedTop = this.CCDesc.worldSkinnedTop;
			Vector3 vector = this.TotemicFigures[0].BottomOrigin - worldSkinnedBottom;
			global::CCDesc.HeightModification heightModification = this.CCDesc.ModifyHeight(this.Expansion.Value, false);
			CollisionFlags collisionFlags2 = this.CCDesc.Move(vector);
			Vector3 worldSkinnedBottom2 = this.CCDesc.worldSkinnedBottom;
			Vector3 vector2 = worldSkinnedBottom2 - worldSkinnedBottom;
			if (vector != vector2)
			{
				Vector3 motion3 = vector2 - vector;
				for (int l = 0; l < this.Configuration.numRequiredTotemicFigures; l++)
				{
					collisionFlags |= (this.TotemicFigures[l].MoveSweep(motion3) & this.TotemicFigures[l].CollisionFlagsMask);
				}
				this.Point.Bottom = this.TotemicFigures[0].BottomOrigin;
				this.Point.Top = this.TotemicFigureEnds.Top.TopOrigin;
				this.Expansion = this.Contraction.ExpansionForValue(this.Point.Top.y - this.Point.Bottom.y);
				global::CCDesc.HeightModification heightModification2 = this.CCDesc.ModifyHeight(this.Expansion.Value, false);
				worldSkinnedBottom2 = this.CCDesc.worldSkinnedBottom;
				vector2 = worldSkinnedBottom2 - worldSkinnedBottom;
			}
			Vector3 worldSkinnedTop2 = this.CCDesc.worldSkinnedTop;
			Vector3 topMovement = worldSkinnedTop2 - worldSkinnedTop;
			global::CCTotem.PositionPlacement positionPlacement = new global::CCTotem.PositionPlacement(worldSkinnedBottom2, worldSkinnedTop2, this.CCDesc.transform.position, this.Configuration.poleExpandedHeight);
			return new global::CCTotem.MoveInfo(collisionFlags2, collisionFlags, height, vector2, topMovement, positionPlacement);
		}

		// Token: 0x04000DD1 RID: 3537
		private const int kCrouch_NotModified = 0;

		// Token: 0x04000DD2 RID: 3538
		private const int kCrouch_MovingDown = -1;

		// Token: 0x04000DD3 RID: 3539
		private const int kCrouch_MovingUp = 1;

		// Token: 0x04000DD4 RID: 3540
		internal readonly global::CCTotem.Configuration Configuration;

		// Token: 0x04000DD5 RID: 3541
		internal readonly global::CCTotem.TotemicFigure[] TotemicFigures;

		// Token: 0x04000DD6 RID: 3542
		internal readonly global::CCTotem.Ends<global::CCTotem.TotemicFigure> TotemicFigureEnds;

		// Token: 0x04000DD7 RID: 3543
		internal readonly global::CCTotem.Contraction Contraction;

		// Token: 0x04000DD8 RID: 3544
		internal global::CCTotem.Ends<Vector3> Point;

		// Token: 0x04000DD9 RID: 3545
		internal global::CCTotem.Expansion Expansion;

		// Token: 0x04000DDA RID: 3546
		internal global::CCDesc CCDesc;

		// Token: 0x04000DDB RID: 3547
		private bool grounded;
	}

	// Token: 0x020002D5 RID: 725
	public sealed class TotemicFigure : global::CCTotem.TotemicObject<global::CCTotemicFigure, global::CCTotem.TotemicFigure>
	{
		// Token: 0x06001947 RID: 6471 RVA: 0x000612D4 File Offset: 0x0005F4D4
		[Obsolete("Infrastructure", true)]
		public TotemicFigure()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001948 RID: 6472 RVA: 0x000612E4 File Offset: 0x0005F4E4
		private TotemicFigure(global::CCTotem.TotemPole TotemPole, int BottomUpIndex)
		{
			this.TotemPole = TotemPole;
			this.BottomUpIndex = BottomUpIndex;
			this.TopDownIndex = this.TotemPole.Configuration.numRequiredTotemicFigures - (this.BottomUpIndex + 1);
			this.CollisionFlagsMask = 1;
			if (this.BottomUpIndex == 0)
			{
				this.CollisionFlagsMask |= 4;
			}
			if (this.TopDownIndex == 0)
			{
				this.CollisionFlagsMask |= 2;
			}
			this.TotemPole.TotemicFigures[this.BottomUpIndex] = this;
		}

		// Token: 0x06001949 RID: 6473 RVA: 0x00061370 File Offset: 0x0005F570
		private TotemicFigure(global::CCTotem.Direction Down) : this(Down.TotemicFigure.TotemPole, Down.TotemicFigure.BottomUpIndex + 1)
		{
			float num = (float)this.BottomUpIndex / (float)this.TotemPole.Configuration.numSlidingTotemicFigures;
			float num2 = (this.TotemPole.Configuration.numSlidingTotemicFigures != 1) ? ((float)(this.BottomUpIndex - 1) / (float)(this.TotemPole.Configuration.numSlidingTotemicFigures - 1)) : num;
			float num3 = Mathf.Lerp(this.TotemPole.Configuration.poleBottomBufferAmount, this.TotemPole.Configuration.poleContractedHeight - this.TotemPole.Configuration.figureSkinnedHeight, num2);
			float num4 = Mathf.Lerp(this.TotemPole.Configuration.poleBottomBufferAmount, this.TotemPole.Configuration.poleExpandedHeight - this.TotemPole.Configuration.figureSkinnedHeight, num);
			this.TotemContractionBottom = global::CCTotem.Contraction.Define(num3, num4);
			this.TotemContractionTop = global::CCTotem.Contraction.Define(num3 + this.TotemPole.Configuration.figureSkinnedHeight, num4 + this.TotemPole.Configuration.figureSkinnedHeight);
			global::CCTotem.Direction direction = new global::CCTotem.Direction(this);
			global::CCTotem.Direction none;
			if (this.BottomUpIndex < this.TotemPole.Configuration.numRequiredTotemicFigures - 1)
			{
				none = new global::CCTotem.Direction(new global::CCTotem.TotemicFigure(direction));
			}
			else
			{
				none = global::CCTotem.Direction.None;
			}
			this.TotemicRoute = new global::CCTotem.Route(Down, direction, none);
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x000614EC File Offset: 0x0005F6EC
		private TotemicFigure(global::CCTotem.TotemPole TotemPole) : this(TotemPole, 0)
		{
			global::CCTotem.Direction direction = new global::CCTotem.Direction(this);
			this.TotemicRoute = new global::CCTotem.Route(global::CCTotem.Direction.None, direction, new global::CCTotem.Direction(new global::CCTotem.TotemicFigure(direction)));
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x00061528 File Offset: 0x0005F728
		internal static global::CCTotem.Ends<global::CCTotem.TotemicFigure> CreateAllTotemicFigures(global::CCTotem.TotemPole TotemPole)
		{
			if (!object.ReferenceEquals(TotemPole.TotemicFigures[0], null))
			{
				throw new ArgumentException("The totem pole already has totemic figures", "TotemPole");
			}
			global::CCTotem.TotemicFigure bottom = new global::CCTotem.TotemicFigure(TotemPole);
			global::CCTotem.TotemicFigure top = TotemPole.TotemicFigures[TotemPole.Configuration.numRequiredTotemicFigures - 1];
			return new global::CCTotem.Ends<global::CCTotem.TotemicFigure>(bottom, top);
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x00061580 File Offset: 0x0005F780
		internal override void OnScriptDestroy(global::CCTotemicFigure Script)
		{
			if (object.ReferenceEquals(this.Script, Script))
			{
				this.Script = null;
				if (object.ReferenceEquals(Script.totemicObject, this))
				{
					Script.totemicObject = null;
				}
			}
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x000615C0 File Offset: 0x0005F7C0
		internal override void AssignedToScript(global::CCTotemicFigure Script)
		{
			this.Script = Script;
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x000615CC File Offset: 0x0005F7CC
		internal void Delete(global::CCTotemPole OwnerScript)
		{
			global::CCTotemicFigure script = this.Script;
			this.Script = null;
			if (script && object.ReferenceEquals(script.totemicObject, this))
			{
				script.totemicObject = null;
			}
			global::CCTotem.DestroyCCDesc(OwnerScript, ref this.CCDesc);
			if (script)
			{
				Object.Destroy(script.gameObject);
			}
			if (object.ReferenceEquals(this.TotemPole.TotemicFigures[this.BottomUpIndex], this))
			{
				this.TotemPole.TotemicFigures[this.BottomUpIndex] = null;
			}
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x0006165C File Offset: 0x0005F85C
		public override string ToString()
		{
			return string.Format("{{Index={0},ContractionBottom={1},ContractionTop={2},Script={3}}}", new object[]
			{
				this.BottomUpIndex,
				this.TotemContractionBottom,
				this.TotemContractionTop,
				this.Script
			});
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06001950 RID: 6480 RVA: 0x000616AC File Offset: 0x0005F8AC
		public Vector3 BottomOrigin
		{
			get
			{
				return this.CCDesc.worldSkinnedBottom;
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06001951 RID: 6481 RVA: 0x000616BC File Offset: 0x0005F8BC
		public Vector3 CenterOrigin
		{
			get
			{
				return this.CCDesc.worldCenter;
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06001952 RID: 6482 RVA: 0x000616CC File Offset: 0x0005F8CC
		public Vector3 TopOrigin
		{
			get
			{
				return this.CCDesc.worldSkinnedTop;
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06001953 RID: 6483 RVA: 0x000616DC File Offset: 0x0005F8DC
		public Vector3 SlideBottomOrigin
		{
			get
			{
				return this.CCDesc.OffsetToWorld(this.CCDesc.center - new Vector3(0f, this.CCDesc.effectiveSkinnedHeight * 0.5f - this.CCDesc.skinnedRadius, 0f));
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06001954 RID: 6484 RVA: 0x00061730 File Offset: 0x0005F930
		public Vector3 SlideTopOrigin
		{
			get
			{
				return this.CCDesc.OffsetToWorld(this.CCDesc.center + new Vector3(0f, this.CCDesc.effectiveSkinnedHeight * 0.5f - this.CCDesc.skinnedRadius, 0f));
			}
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x00061784 File Offset: 0x0005F984
		public CollisionFlags MoveWorldBottomTo(Vector3 targetBottom)
		{
			return this.MoveWorld(targetBottom - this.BottomOrigin);
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x00061798 File Offset: 0x0005F998
		public CollisionFlags MoveWorldTopTo(Vector3 targetTop)
		{
			return this.MoveWorld(targetTop - this.TopOrigin);
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x000617AC File Offset: 0x0005F9AC
		public CollisionFlags MoveWorld(Vector3 motion)
		{
			return this.CCDesc.Move(motion);
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x000617BC File Offset: 0x0005F9BC
		public CollisionFlags MoveSweep(Vector3 motion)
		{
			this.PreSweepBottom = this.BottomOrigin;
			CollisionFlags result = this.MoveWorld(motion);
			this.PostSweepBottom = this.BottomOrigin;
			this.SweepMovement = this.PostSweepBottom - this.PreSweepBottom;
			return result;
		}

		// Token: 0x04000DDC RID: 3548
		public global::CCDesc CCDesc;

		// Token: 0x04000DDD RID: 3549
		internal readonly global::CCTotem.TotemPole TotemPole;

		// Token: 0x04000DDE RID: 3550
		internal readonly int BottomUpIndex;

		// Token: 0x04000DDF RID: 3551
		internal readonly int TopDownIndex;

		// Token: 0x04000DE0 RID: 3552
		internal readonly CollisionFlags CollisionFlagsMask;

		// Token: 0x04000DE1 RID: 3553
		internal readonly global::CCTotem.Route TotemicRoute;

		// Token: 0x04000DE2 RID: 3554
		internal readonly global::CCTotem.Contraction TotemContractionTop;

		// Token: 0x04000DE3 RID: 3555
		internal readonly global::CCTotem.Contraction TotemContractionBottom;

		// Token: 0x04000DE4 RID: 3556
		public Vector3 PreSweepBottom;

		// Token: 0x04000DE5 RID: 3557
		public Vector3 PostSweepBottom;

		// Token: 0x04000DE6 RID: 3558
		public Vector3 SweepMovement;
	}

	// Token: 0x020002D6 RID: 726
	// (Invoke) Token: 0x0600195A RID: 6490
	public delegate void PositionBinder(ref global::CCTotem.PositionPlacement binding, object Tag);

	// Token: 0x020002D7 RID: 727
	// (Invoke) Token: 0x0600195E RID: 6494
	public delegate void ConfigurationBinder(bool Bind, global::CCDesc CCDesc, object Tag);
}
