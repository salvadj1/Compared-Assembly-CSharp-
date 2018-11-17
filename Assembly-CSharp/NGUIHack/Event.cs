using System;
using UnityEngine;

namespace NGUIHack
{
	// Token: 0x020008FF RID: 2303
	public class Event : IDisposable
	{
		// Token: 0x06004E88 RID: 20104 RVA: 0x001451C0 File Offset: 0x001433C0
		internal Event(Event @event)
		{
			this.@event = @event;
			this.originalType = @event.type;
			this.originalRawType = @event.rawType;
			this.overrideType = 12;
			this.screenPosition = Input.mousePosition;
		}

		// Token: 0x06004E89 RID: 20105 RVA: 0x0014520C File Offset: 0x0014340C
		internal Event(Event @event, EventType overrideType) : this(@event)
		{
			this.overrideType = overrideType;
		}

		// Token: 0x17000F11 RID: 3857
		// (get) Token: 0x06004E8A RID: 20106 RVA: 0x0014521C File Offset: 0x0014341C
		public EventType type
		{
			get
			{
				return (this.overrideType != 12) ? this.overrideType : this.@event.type;
			}
		}

		// Token: 0x17000F12 RID: 3858
		// (get) Token: 0x06004E8B RID: 20107 RVA: 0x00145244 File Offset: 0x00143444
		public EventType rawType
		{
			get
			{
				return (this.overrideType != 12) ? this.overrideType : this.@event.rawType;
			}
		}

		// Token: 0x17000F13 RID: 3859
		// (get) Token: 0x06004E8C RID: 20108 RVA: 0x0014526C File Offset: 0x0014346C
		public int button
		{
			get
			{
				return this.@event.button;
			}
		}

		// Token: 0x17000F14 RID: 3860
		// (get) Token: 0x06004E8D RID: 20109 RVA: 0x0014527C File Offset: 0x0014347C
		public Vector2 mousePosition
		{
			get
			{
				return this.screenPosition;
			}
		}

		// Token: 0x17000F15 RID: 3861
		// (get) Token: 0x06004E8E RID: 20110 RVA: 0x00145284 File Offset: 0x00143484
		public KeyCode keyCode
		{
			get
			{
				return this.@event.keyCode;
			}
		}

		// Token: 0x17000F16 RID: 3862
		// (get) Token: 0x06004E8F RID: 20111 RVA: 0x00145294 File Offset: 0x00143494
		public char character
		{
			get
			{
				return this.@event.character;
			}
		}

		// Token: 0x17000F17 RID: 3863
		// (get) Token: 0x06004E90 RID: 20112 RVA: 0x001452A4 File Offset: 0x001434A4
		public EventModifiers modifiers
		{
			get
			{
				return this.@event.modifiers;
			}
		}

		// Token: 0x17000F18 RID: 3864
		// (get) Token: 0x06004E91 RID: 20113 RVA: 0x001452B4 File Offset: 0x001434B4
		public bool shift
		{
			get
			{
				return this.@event.shift;
			}
		}

		// Token: 0x17000F19 RID: 3865
		// (get) Token: 0x06004E92 RID: 20114 RVA: 0x001452C4 File Offset: 0x001434C4
		public bool alt
		{
			get
			{
				return this.@event.alt;
			}
		}

		// Token: 0x17000F1A RID: 3866
		// (get) Token: 0x06004E93 RID: 20115 RVA: 0x001452D4 File Offset: 0x001434D4
		public bool control
		{
			get
			{
				return this.@event.control;
			}
		}

		// Token: 0x17000F1B RID: 3867
		// (get) Token: 0x06004E94 RID: 20116 RVA: 0x001452E4 File Offset: 0x001434E4
		public bool capsLock
		{
			get
			{
				return this.@event.capsLock;
			}
		}

		// Token: 0x17000F1C RID: 3868
		// (get) Token: 0x06004E95 RID: 20117 RVA: 0x001452F4 File Offset: 0x001434F4
		public Vector2 delta
		{
			get
			{
				return this.@event.delta;
			}
		}

		// Token: 0x06004E96 RID: 20118 RVA: 0x00145304 File Offset: 0x00143504
		public void Dispose()
		{
			if (this.overrideType != 12 && this.@event.type == this.overrideType)
			{
				this.@event.type = this.originalType;
			}
		}

		// Token: 0x06004E97 RID: 20119 RVA: 0x00145348 File Offset: 0x00143548
		public void Use()
		{
			if (this.overrideType == 12)
			{
				this.@event.Use();
			}
		}

		// Token: 0x17000F1D RID: 3869
		// (get) Token: 0x06004E98 RID: 20120 RVA: 0x00145364 File Offset: 0x00143564
		internal Event real
		{
			get
			{
				return this.@event;
			}
		}

		// Token: 0x17000F1E RID: 3870
		// (get) Token: 0x06004E99 RID: 20121 RVA: 0x0014536C File Offset: 0x0014356C
		public EventType unityOriginalRawType
		{
			get
			{
				return this.originalRawType;
			}
		}

		// Token: 0x04002C1E RID: 11294
		public static int pressed;

		// Token: 0x04002C1F RID: 11295
		public static int unpressed;

		// Token: 0x04002C20 RID: 11296
		public static int held;

		// Token: 0x04002C21 RID: 11297
		private readonly Event @event;

		// Token: 0x04002C22 RID: 11298
		private readonly EventType originalType;

		// Token: 0x04002C23 RID: 11299
		private readonly EventType originalRawType;

		// Token: 0x04002C24 RID: 11300
		private readonly EventType overrideType;

		// Token: 0x04002C25 RID: 11301
		private readonly Vector2 screenPosition;
	}
}
