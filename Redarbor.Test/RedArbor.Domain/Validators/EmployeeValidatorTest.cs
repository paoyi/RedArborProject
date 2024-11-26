using FluentValidation.TestHelper;
using RedArbor.Domain.Employees.Entities;
using RedArbor.Domain.Employees.Validators;

namespace Redarbor.Test.RedArbor.Domain.Validators
{
    [TestClass]
    public class EmployeeValidatorTest
    {
        private EmployeeValidator Validator;

        [TestInitialize]
        public void Setup()
        {
            Validator = new EmployeeValidator();
        }

        [TestMethod]
        public void Should_have_error_when_Name_is_null()
        {
            Employee model = new() { Name = null };
            var result = Validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(pt => pt.Name);
        }

        [TestMethod]
        public void Should_have_error_when_Password_is_null()
        {
            Employee model = new() { Password = null };
            var result = Validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(pt => pt.Password);
        }

        [TestMethod]
        public void Should_not_have_error_when_email_is_incorrect()
        {
            Employee model = new()
            {
                Name = "employee1",
                Email = "email_incorrect",
                CompanyId = 1,
                RoleId = 1,
                Password = "123",
                PortalId = 1
            };
            var result = Validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(pt => pt.Email);
        }
    }
}