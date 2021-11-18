namespace API.Models
{
	public class SimpleResponse
	{
		public object data { get; set; }
		public string message { get; set; }

		public SimpleResponse(object data, string message)
		{
			this.data = data;
			this.message = message;
		}
	}
}
