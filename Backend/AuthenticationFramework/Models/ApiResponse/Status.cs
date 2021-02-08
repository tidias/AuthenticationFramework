namespace AuthenticationFramework.Models.ApiResponse
{
    public class Status
    {
        public bool Success { get; set; }
        public string Info { get; set; }
        public string Token { get; set; }
        public string[] Errors { get; set; }
    }
}
