using JetBrains.Annotations;
using OpenTK.Graphics.OpenGL4;
using USharpLibs.Common.Utils;
using OpenGL4 = OpenTK.Graphics.OpenGL4.GL;

namespace USharpLibs.Engine.Client.GL.Models {
	[PublicAPI]
	public class DynamicModel : Model<DynamicModel> {
		protected bool IsDirty = true;

		protected List<Mesh> Meshes { get; } = new();
		protected float[] VertexCache = Array.Empty<float>();
		protected uint[] IndexCache = Array.Empty<uint>();

		public DynamicModel(BufferUsageHint bufferHint) : base(bufferHint) { }

		public override DynamicModel SetMesh(Mesh mesh, params Mesh[] meshes) {
			ClearModelData();
			return AddMesh(mesh, meshes);
		}

		public override DynamicModel SetMesh(List<Mesh> meshes) {
			ClearModelData();
			return AddMesh(meshes);
		}

		public DynamicModel AddMesh(Mesh mesh, params Mesh[] meshes) {
			Meshes.Add(mesh);
			Meshes.AddRange(meshes);
			IsDirty = true;
			return this;
		}

		public DynamicModel AddMesh(List<Mesh> meshes) {
			Meshes.AddRange(meshes);
			IsDirty = true;
			return this;
		}

		protected override void ISetupGL() {
			if (Meshes.Count == 0) {
				Logger.Warn("Tried to setup an empty model!");
				return;
			} else if (WasSetup) {
				Logger.Warn("This model was already setup!");
				return;
			}

			WasSetup = true;

			VAO = OpenGL4.GenVertexArray();
			VBO = OpenGL4.GenBuffer();
			EBO = OpenGL4.GenBuffer();

			RefreshModelData();
		}

		public void ClearModelData() {
			Meshes.Clear();
			IsDirty = true;
		}

		public void RefreshModelData() {
			if (IsDirty) {
				List<float> vertices = new();
				List<uint> indices = new();
				uint indexOffset = 0;

				foreach (Mesh part in Meshes) {
					vertices.AddRange(part.Vertices);

					uint highestIndex = 0;
					foreach (uint i in part.Indices) {
						if (i > highestIndex) { highestIndex = i; }
						indices.Add(i + indexOffset);
					}

					indexOffset += highestIndex + 1;
				}

				VertexCache = vertices.ToArray();
				IndexCache = indices.ToArray();

				OpenGL4.BindVertexArray(VAO);

				OpenGL4.BindBuffer(BufferTarget.ArrayBuffer, VBO);
				OpenGL4.BufferData(BufferTarget.ArrayBuffer, VertexCache.Length * sizeof(float), VertexCache, BufferHint);

				OpenGL4.EnableVertexAttribArray(Shader.PositionLocation);
				OpenGL4.EnableVertexAttribArray(Shader.TextureLocation);
				OpenGL4.VertexAttribPointer(Shader.PositionLocation, 3, VertexAttribPointerType.Float, false, sizeof(float) * 5, 0);
				OpenGL4.VertexAttribPointer(Shader.TextureLocation, 2, VertexAttribPointerType.Float, false, sizeof(float) * 5, sizeof(float) * 3);

				OpenGL4.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
				OpenGL4.BufferData(BufferTarget.ElementArrayBuffer, IndexCache.Length * sizeof(uint), IndexCache, BufferHint);

				OpenGL4.BindVertexArray(0);

				IsDirty = false;
			}
		}

		protected override void IDraw() => OpenGL4.DrawElements(PrimitiveType.Triangles, IndexCache.Length, DrawElementsType.UnsignedInt, 0);
	}
}