using System;
using System.Collections.Generic;

namespace EmployeeCRUD.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Department { get; set; }

    public decimal? Salary { get; set; }

    public string? Address { get; set; }
}
