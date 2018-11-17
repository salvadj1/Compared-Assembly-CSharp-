using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000701 RID: 1793
[AddComponentMenu("Daikon Forge/User Interface/Sprite/Web")]
[ExecuteInEditMode]
[Serializable]
public class dfWebSprite : dfTextureSprite
{
	// Token: 0x17000CE2 RID: 3298
	// (get) Token: 0x06004168 RID: 16744 RVA: 0x000FCA18 File Offset: 0x000FAC18
	// (set) Token: 0x06004169 RID: 16745 RVA: 0x000FCA20 File Offset: 0x000FAC20
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

	// Token: 0x17000CE3 RID: 3299
	// (get) Token: 0x0600416A RID: 16746 RVA: 0x000FCA64 File Offset: 0x000FAC64
	// (set) Token: 0x0600416B RID: 16747 RVA: 0x000FCA6C File Offset: 0x000FAC6C
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

	// Token: 0x17000CE4 RID: 3300
	// (get) Token: 0x0600416C RID: 16748 RVA: 0x000FCA78 File Offset: 0x000FAC78
	// (set) Token: 0x0600416D RID: 16749 RVA: 0x000FCA80 File Offset: 0x000FAC80
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

	// Token: 0x0600416E RID: 16750 RVA: 0x000FCA8C File Offset: 0x000FAC8C
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

	// Token: 0x0600416F RID: 16751 RVA: 0x000FCAD4 File Offset: 0x000FACD4
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

	// Token: 0x04002289 RID: 8841
	[SerializeField]
	protected string url = string.Empty;

	// Token: 0x0400228A RID: 8842
	[SerializeField]
	protected Texture2D loadingImage;

	// Token: 0x0400228B RID: 8843
	[SerializeField]
	protected Texture2D errorImage;
}
