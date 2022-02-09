namespace PermissionsMiddleware.Models;

    public class PermissionTokenInformation
    {
        public Guid UserId { get; set; }
        public string Area { get; set; } = null!;
        public string Action { get; set; } = null!;
    }