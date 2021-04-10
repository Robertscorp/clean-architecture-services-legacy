namespace CleanArchitecture.Example.Domain.Entities
{

    public class Employee
    {

        #region - - - - - - Properties - - - - - -

        public Person EmployeeDetails { get; set; }

        public string Title { get; set; }

        public User User { get; set; }

        #endregion Properties

    }

}
