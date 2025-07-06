using System.ComponentModel.DataAnnotations;

namespace QLDT.Dtos.Request
{
	public class LoginReq
	{
		[Required]
		public string Username { get; set; }

		[Required]
		public string Password { get; set; }
	}
}
