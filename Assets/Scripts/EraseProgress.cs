using UnityEngine;
using UnityEngine.Rendering;

public class EraseProgress : MonoBehaviour
{
	public ScratchCard Card;

	public event ProgressHandler OnProgress;
	public event ProgressHandler OnCompleted;

	public delegate void ProgressHandler(float progress);

	private RenderTexture renderPercent;
//	private Vector3 RightUp = new Vector3(1, 1, 0);
	private float currentProgress;
	private bool isCompleted;

	private Mesh mesh;
	private CommandBuffer commandBuffer;
	private RenderTargetIdentifier rti;

	void Start()
	{
		commandBuffer = new CommandBuffer();
		commandBuffer.name = "EraseProgress";
		CreateRenderTexture();
		rti = new RenderTargetIdentifier(renderPercent);

		mesh = new Mesh();
		mesh.vertices = new Vector3[]
		{
			new Vector3(0, 0, 0),
			new Vector3(0, 1, 0),
			new Vector3(1, 1, 0),
			new Vector3(1, 0, 0),
		};
		mesh.uv = new[]
		{
			new Vector2(0, 0),
			new Vector2(0, 1),
			new Vector2(1, 1),
			new Vector2(1, 0),
		};
		mesh.triangles = new[]
		{
			0, 1, 2,
			2, 3, 0
		};
		mesh.colors = new[]
		{
			Color.white,
			Color.white,
			Color.white,
			Color.white
		};
	}

	private void Update()
	{
		if (Card.IsScratching)
		{
			GL.LoadOrtho();
			commandBuffer.Clear();
			commandBuffer.SetRenderTarget(rti);
			commandBuffer.DrawMesh(mesh, Matrix4x4.identity, Card.Progress);
			Graphics.ExecuteCommandBuffer(commandBuffer);

			CalcProgress();
		}
	}

	private void CreateRenderTexture()
	{
		renderPercent = new RenderTexture(1, 1, 0, RenderTextureFormat.ARGB32);
		renderPercent.Create();
	}

	private void CalcProgress()
	{
		if (!isCompleted)
		{
			var prevRenderTextureT = RenderTexture.active;
			RenderTexture.active = renderPercent;
			var myTexture2D = new Texture2D(renderPercent.width, renderPercent.height, TextureFormat.ARGB32, false, true);
			myTexture2D.ReadPixels(new Rect(0, 0, renderPercent.width, renderPercent.height), 0, 0);
			myTexture2D.Apply();
			RenderTexture.active = prevRenderTextureT;

			var red = myTexture2D.GetPixel(0, 0).r;
			currentProgress = red;
			if (OnProgress != null)
			{
				OnProgress(red);
				if (red == 1f)
				{
					if (OnCompleted != null)
					{
						OnCompleted(red);
					}
					isCompleted = true;
				}
			}
		}
	}

	public float GetProgress()
	{
		return currentProgress;
	}
}