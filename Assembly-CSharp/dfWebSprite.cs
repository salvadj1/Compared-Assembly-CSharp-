using System;
using System.Collections;
using UnityEngine;

// Token: 0x020007D4 RID: 2004
[AddComponentMenu("Daikon Forge/User Interface/Sprite/Web")]
[ExecuteInEditMode]
[Serializable]
public class dfWebSprite : global::dfTextureSprite
{
	// Token: 0x17000D68 RID: 3432
	// (get) Token: 0x0600458A RID: 17802 RVA: 0x00105704 File Offset: 0x00103904
	// (set) Token: 0x0600458B RID: 17803 RVA: 0x0010570C File Offset: 0x0010390C
	public string URL
	{
		get
		{
			return this.url;
		}
		set
		{
			if (value != this.url)
			{
				this.url = value;
				if (Application.isPlaying)
				{
					base.StopAllCoroutines();
					base.StartCoroutine(this.downloadTexture());
				}
			}
		}
	}

	// Token: 0x17000D69 RID: 3433
	// (get) Token: 0x0600458C RID: 17804 RVA: 0x00105750 File Offset: 0x00103950
	// (set) Token: 0x0600458D RID: 17805 RVA: 0x00105758 File Offset: 0x00103958
	public Texture2D LoadingImage
	{
		get
		{
			return this.loadingImage;
		}
		set
		{
			this.loadingImage = value;
		}
	}

	// Token: 0x17000D6A RID: 3434
	// (get) Token: 0x0600458E RID: 17806 RVA: 0x00105764 File Offset: 0x00103964
	// (set) Token: 0x0600458F RID: 17807 RVA: 0x0010576C File Offset: 0x0010396C
	public Texture2D ErrorImage
	{
		get
		{
			return this.errorImage;
		}
		set
		{
			this.errorImage = value;
		}
	}

	// Token: 0x06004590 RID: 17808 RVA: 0x00105778 File Offset: 0x00103978
	public override void Start()
	{
		base.Start();
		if (base.Texture == null)
		{
			base.Texture = this.LoadingImage;
		}
		if (Application.isPlaying)
		{
			base.StartCoroutine(this.downloadTexture());
		}
	}

	// Token: 0x06004591 RID: 17809 RVA: 0x001057C0 File Offset: 0x001039C0
	private IEnumerator downloadTexture()
	{
		base.Texture = this.loadingImage;
		using (WWW request = new WWW(this.url))
		{
			yield return request;
			if (!string.IsNullOrEmpty(request.error))
			{
				Debug.Log("Error downloading image: " + request.error);
				base.Texture = (this.errorImage ?? this.loadingImage);
			}
			else
			{
				base.Texture = request.texture;
			}
		}
		yield break;
	}

	// Token: 0x04002495 RID: 9365
	[SerializeField]
	protected string url = string.Empty;

	// Token: 0x04002496 RID: 9366
	[SerializeField]
	protected Texture2D loadingImage;

	// Token: 0x04002497 RID: 9367
	[SerializeField]
	protected Texture2D errorImage;
}
