﻿using Newtonsoft.Json;

namespace GLTF
{
	public class GLTFAccessorSparse : GLTFProperty
	{
		/// <summary>
		/// Number of entries stored in the sparse array.
		/// <minimum>1</minimum>
		/// </summary>
		public int Count;

		/// <summary>
		/// Index array of size `count` that points to those accessor attributes that
		/// deviate from their initialization value. Indices must strictly increase.
		/// </summary>
		public GLTFAccessorSparseIndices Indices;

		/// <summary>
		/// "Array of size `count` times number of components, storing the displaced
		/// accessor attributes pointed by `indices`. Substituted values must have
		/// the same `componentType` and number of components as the base accessor.
		/// </summary>
		public GLTFAccessorSparseValues Values;

		public static GLTFAccessorSparse Deserialize(GLTFRoot root, JsonReader reader)
		{
			var accessorSparse = new GLTFAccessorSparse();

			while (reader.Read() && reader.TokenType == JsonToken.PropertyName)
			{
				var curProp = reader.Value.ToString();

				switch (curProp)
				{
					case "count":
						accessorSparse.Count = reader.ReadAsInt32().Value;
						break;
					case "indices":
						accessorSparse.Indices = GLTFAccessorSparseIndices.Deserialize(root, reader);
						break;
					case "values":
						accessorSparse.Values = GLTFAccessorSparseValues.Deserialize(root, reader);
						break;
					default:
						accessorSparse.DefaultPropertyDeserializer(root, reader);
						break;
				}
			}

			return accessorSparse;
		}

		public override void Serialize(JsonWriter writer)
		{
			writer.WriteStartObject();

			writer.WritePropertyName("count");
			writer.WriteValue(Count);

			writer.WritePropertyName("indices");
			Indices.Serialize(writer);

			writer.WritePropertyName("values");
			Values.Serialize(writer);

			base.Serialize(writer);

			writer.WriteEndObject();
		}
	}
}
