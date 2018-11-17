using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;

namespace Facepunch.Load
{
	// Token: 0x02000268 RID: 616
	public sealed class Loader : IDisposable, IDownloadTask
	{
		// Token: 0x06001673 RID: 5747 RVA: 0x00054F30 File Offset: 0x00053130
		private Loader(Group masterGroup, Job[] allJobs, Group[] allGroups, IDownloaderDispatch dispatch)
		{
			this.MasterGroup = masterGroup;
			this.Jobs = allJobs;
			this.Groups = allGroups;
			this.Dispatch = dispatch;
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06001674 RID: 5748 RVA: 0x00054F68 File Offset: 0x00053168
		// (remove) Token: 0x06001675 RID: 5749 RVA: 0x00054F84 File Offset: 0x00053184
		public event AssetBundleLoadedEventHandler OnAssetBundleLoaded;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06001676 RID: 5750 RVA: 0x00054FA0 File Offset: 0x000531A0
		// (remove) Token: 0x06001677 RID: 5751 RVA: 0x00054FBC File Offset: 0x000531BC
		public event MultipleAssetBundlesLoadedEventHandler OnAllAssetBundlesLoaded;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06001678 RID: 5752 RVA: 0x00054FD8 File Offset: 0x000531D8
		// (remove) Token: 0x06001679 RID: 5753 RVA: 0x00054FF4 File Offset: 0x000531F4
		public event MultipleAssetBundlesLoadedEventHandler OnGroupedAssetBundlesLoaded;

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x0600167A RID: 5754 RVA: 0x00055010 File Offset: 0x00053210
		string IDownloadTask.Name
		{
			get
			{
				return "Loading all bundles";
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x0600167B RID: 5755 RVA: 0x00055018 File Offset: 0x00053218
		string IDownloadTask.ContextualDescription
		{
			get
			{
				return this.MasterGroup.ContextualDescription;
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x0600167C RID: 5756 RVA: 0x00055028 File Offset: 0x00053228
		public int ByteLength
		{
			get
			{
				return this.MasterGroup.ByteLength;
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x0600167D RID: 5757 RVA: 0x00055038 File Offset: 0x00053238
		public int ByteLengthDownloaded
		{
			get
			{
				return this.MasterGroup.ByteLengthDownloaded;
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x0600167E RID: 5758 RVA: 0x00055048 File Offset: 0x00053248
		public float PercentDone
		{
			get
			{
				return this.MasterGroup.PercentDone;
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x0600167F RID: 5759 RVA: 0x00055058 File Offset: 0x00053258
		public TaskStatus TaskStatus
		{
			get
			{
				return this.MasterGroup.TaskStatus;
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06001680 RID: 5760 RVA: 0x00055068 File Offset: 0x00053268
		public int Count
		{
			get
			{
				return this.MasterGroup.Count;
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06001681 RID: 5761 RVA: 0x00055078 File Offset: 0x00053278
		public int Done
		{
			get
			{
				return this.MasterGroup.Done;
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06001682 RID: 5762 RVA: 0x00055088 File Offset: 0x00053288
		public TaskStatusCount TaskStatusCount
		{
			get
			{
				return this.MasterGroup.TaskStatusCount;
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06001683 RID: 5763 RVA: 0x00055098 File Offset: 0x00053298
		public Group CurrentGroup
		{
			get
			{
				return (this.currentGroup < 0 || this.currentGroup >= this.Groups.Length) ? null : this.Groups[this.currentGroup];
			}
		}

		// Token: 0x06001684 RID: 5764 RVA: 0x000550D8 File Offset: 0x000532D8
		private static Loader Deserialize(Reader reader, IDownloaderDispatch dispatch)
		{
			List<Item[]> list = new List<Item[]>();
			List<Item> list2 = new List<Item>();
			while (reader.Read())
			{
				switch (reader.Token)
				{
				case Token.RandomLoadOrderAreaBegin:
					list2.Clear();
					break;
				case Token.BundleListing:
					list2.Add(reader.Item);
					break;
				case Token.RandomLoadOrderAreaEnd:
					list.Add(list2.ToArray());
					break;
				case Token.DownloadQueueEnd:
				{
					Operation operation = new Operation();
					int num = 0;
					foreach (Item[] array in list)
					{
						num += array.Length;
					}
					Job[] array2 = new Job[num];
					int num2 = 0;
					foreach (Item[] array3 in list)
					{
						foreach (Item item in array3)
						{
							array2[num2++] = new Job
							{
								_op = operation,
								Item = item
							};
						}
					}
					Group group = new Group();
					group._op = operation;
					group.Jobs = array2;
					group.Initialize();
					Group[] array5 = new Group[list.Count];
					int num3 = 0;
					int num4 = 0;
					foreach (Item[] array6 in list)
					{
						int num5 = array6.Length;
						Job[] array7 = new Job[num5];
						for (int j = 0; j < num5; j++)
						{
							array7[j] = array2[num3++];
						}
						array5[num4] = new Group();
						array5[num4]._op = operation;
						array5[num4].Jobs = array7;
						array5[num4].Initialize();
						for (int k = 0; k < num5; k++)
						{
							array7[k].Group = array5[num4];
						}
						num4++;
					}
					return operation.Loader = new Loader(group, array2, array5, dispatch);
				}
				}
			}
			throw new InvalidProgramException();
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x0005538C File Offset: 0x0005358C
		public static Loader CreateFromText(string downloadListJson, string bundlePath, IDownloaderDispatch dispatch)
		{
			Loader result;
			using (Reader reader = Reader.CreateFromText(downloadListJson, bundlePath))
			{
				result = Loader.Deserialize(reader, dispatch);
			}
			return result;
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x000553E0 File Offset: 0x000535E0
		public static Loader CreateFromFile(string downloadListPath, string bundlePath, IDownloaderDispatch dispatch)
		{
			Loader result;
			using (Reader reader = Reader.CreateFromFile(downloadListPath, bundlePath))
			{
				result = Loader.Deserialize(reader, dispatch);
			}
			return result;
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x00055434 File Offset: 0x00053634
		public static Loader CreateFromFile(string downloadListPath, IDownloaderDispatch dispatch)
		{
			Loader result;
			using (Reader reader = Reader.CreateFromFile(downloadListPath))
			{
				result = Loader.Deserialize(reader, dispatch);
			}
			return result;
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x00055484 File Offset: 0x00053684
		public static Loader CreateFromReader(TextReader textReader, string bundlePath, IDownloaderDispatch dispatch)
		{
			Loader result;
			using (Reader reader = Reader.CreateFromReader(textReader, bundlePath))
			{
				result = Loader.Deserialize(reader, dispatch);
			}
			return result;
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x000554D8 File Offset: 0x000536D8
		public static Loader CreateFromReader(JsonReader jsonReader, string bundlePath, IDownloaderDispatch dispatch)
		{
			Loader result;
			using (Reader reader = Reader.CreateFromReader(jsonReader, bundlePath))
			{
				result = Loader.Deserialize(reader, dispatch);
			}
			return result;
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x0005552C File Offset: 0x0005372C
		public static Loader Create(Reader reader, IDownloaderDispatch dispatch)
		{
			return Loader.Deserialize(reader, dispatch);
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x00055538 File Offset: 0x00053738
		internal void OnJobCompleted(Job job, IDownloader downloader)
		{
			job.AssetBundle = downloader.GetLoadedAssetBundle(job);
			if (this.OnAssetBundleLoaded != null)
			{
				try
				{
					this.OnAssetBundleLoaded(job.AssetBundle, job.Item);
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
				}
			}
			downloader.OnJobCompleted(job);
			this.Dispatch.DeleteDownloader(job, downloader);
			if (++this.jobsCompleted == this.MasterGroup.Count)
			{
				if (this.OnAllAssetBundlesLoaded != null)
				{
					AssetBundle[] assetBundle;
					Item[] item;
					this.MasterGroup.GetArrays(out assetBundle, out item);
					try
					{
						this.OnAllAssetBundlesLoaded(assetBundle, item);
					}
					catch (Exception ex2)
					{
						Debug.LogException(ex2);
					}
				}
				this.DisposeDispatch();
			}
			else if (++this.jobsCompletedInGroup == this.Groups[this.currentGroup].Jobs.Length)
			{
				if (this.OnGroupedAssetBundlesLoaded != null)
				{
					AssetBundle[] assetBundle2;
					Item[] item2;
					this.Groups[this.currentGroup].GetArrays(out assetBundle2, out item2);
					try
					{
						this.OnGroupedAssetBundlesLoaded(assetBundle2, item2);
					}
					catch (Exception ex3)
					{
						Debug.LogException(ex3);
					}
				}
				this.StartNextGroup();
			}
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x000556C0 File Offset: 0x000538C0
		private void DisposeDispatch()
		{
			if (this.Dispatch != null)
			{
				IDownloaderDispatch dispatch = this.Dispatch;
				this.Dispatch = null;
				dispatch.UnbindLoader(this);
			}
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x000556F0 File Offset: 0x000538F0
		public void Dispose()
		{
			if (!this.disposed)
			{
				this.disposed = true;
				this.DisposeDispatch();
			}
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x0005570C File Offset: 0x0005390C
		private void StartJob(Job job)
		{
			IDownloader downloader = this.Dispatch.CreateDownloaderForJob(job);
			downloader.BeginJob(job);
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x00055730 File Offset: 0x00053930
		private void StartNextGroup()
		{
			this.jobsCompletedInGroup = 0;
			foreach (Job job in this.Groups[++this.currentGroup].Jobs)
			{
				this.StartJob(job);
			}
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x00055784 File Offset: 0x00053984
		public void StartLoading()
		{
			if (this.currentGroup == -1)
			{
				this.Dispatch.BindLoader(this);
				if (this.Groups.Length > 0)
				{
					this.StartNextGroup();
				}
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06001691 RID: 5777 RVA: 0x000557C0 File Offset: 0x000539C0
		public IEnumerator WaitEnumerator
		{
			get
			{
				while (this.jobsCompleted < this.MasterGroup.Jobs.Length)
				{
					yield return null;
				}
				yield break;
			}
		}

		// Token: 0x04000B68 RID: 2920
		[NonSerialized]
		private readonly Group MasterGroup;

		// Token: 0x04000B69 RID: 2921
		[NonSerialized]
		public readonly Group[] Groups;

		// Token: 0x04000B6A RID: 2922
		[NonSerialized]
		public readonly Job[] Jobs;

		// Token: 0x04000B6B RID: 2923
		[NonSerialized]
		private int jobsCompleted;

		// Token: 0x04000B6C RID: 2924
		[NonSerialized]
		private int currentGroup = -1;

		// Token: 0x04000B6D RID: 2925
		[NonSerialized]
		private int jobsCompletedInGroup;

		// Token: 0x04000B6E RID: 2926
		[NonSerialized]
		private bool disposed;

		// Token: 0x04000B6F RID: 2927
		[NonSerialized]
		private IDownloaderDispatch Dispatch;
	}
}
