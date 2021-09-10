using System.Collections.Generic;

namespace ContractDevTestApp.Models
{
	/// <summary>
	/// Common response without data
	/// </summary>
	public class CommonResponseDto
	{
		public string Message { get; set; }

		public string Status { get; set; }
		public string StackTrace { get; set; }

		public IDictionary<string, string[]> Errors { get; set; }
	}

	/// <summary>
	/// Common response with data.
	/// </summary>
	/// <typeparam name="TResponseData"></typeparam>
	public class CommonResponseDto<TResponseData>: CommonResponseDto
	{
		/// <summary>
		/// Payload for response.
		/// </summary>
		public TResponseData Data { get; set; }

	}
}