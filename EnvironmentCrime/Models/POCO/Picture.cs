namespace EnvironmentCrime.Models
{
	public class Picture
	{
		public int PictureId { get; set; }
		public required string PictureName { get; set; }
		public int ErrandID { get; set; }
	}
}
