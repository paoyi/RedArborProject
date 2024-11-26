namespace RedArbor.Domain.Employees.Entities
{
    public class Employee
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string Email { get; set; }

        public string? Fax { get; set; }

        public string Name { get; set; }

        public DateTime? LastLogin { get; set; }

        public string Password { get; set; }

        public int PortalId { get; set; }

        public int RoleId { get; set; }

        public int StatusId { get; set; }

        public string Status
        {
            get { return StatusId.ToString(); }
        }

        public string? Telephone { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string Username { get; set; }

        public Employee()
        {
        }

        public Employee(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Employee(
            int companyId,
            int portalId,
            int roleId,
            string email,
            string fax,
            string name,
            string password,
            string phone,
            string userName,
            DateTime? createdOn,
            DateTime? lastLogin,
            EmployeeStatus statusId
            )
        {
            CompanyId = companyId;
            CreatedOn = createdOn ?? DateTime.Now;
            Email = email;
            Fax = fax;
            LastLogin = lastLogin;
            Name = name;
            Password = password;
            PortalId = portalId;
            RoleId = roleId;
            StatusId = (int)statusId;
            Telephone = phone;
            Username = userName;
        }

        public void SetUpdatedEmployee(
            int companyId,
            int portalId,
            int roleId,
            string email,
            string fax,
            string name,
            string password,
            string phone,
            string userName,
            DateTime? updatedOn,
            DateTime? lastLogin,
            EmployeeStatus statusId)
        {
            UpdatedOn = updatedOn ?? DateTime.Now;
            CompanyId = companyId;
            Email = email;
            Fax = fax;
            LastLogin = lastLogin;
            Name = name;
            Password = password;
            PortalId = portalId;
            RoleId = roleId;
            StatusId = (int)statusId;
            Telephone = phone;
            Username = userName;
        }

        public void SetStatus(EmployeeStatus status)
        {
            StatusId = (int)status;
        }
    }
}