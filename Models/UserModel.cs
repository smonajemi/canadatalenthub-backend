using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public enum UserType
{
    Freelancer = 1,
    Client = 2
}

public enum SkillLevel
{
    Beginner = 1,
    Intermediate = 2,
    Advanced = 3
}

public class Skill
{
    public string Name { get; set; }
    public SkillLevel Level { get; set; }
}

public class Education
{
    public string Institution { get; set; }
    public string Degree { get; set; }
    public string FieldOfStudy { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

public class Experience
{
    public string Company { get; set; }
    public string Position { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<string> Responsibilities { get; set; } = new List<string>();
}

// Common properties for both
public class User
{
    public Guid Id { get; set; } 

    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string LastName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    public string Title { get; set; }
    public string PhoneNumber { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 10)]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
    ErrorMessage = "Password must be at least 10 characters long, include a letter, a number, and a special character.")]
    public string Password { get; set; }

    public string Bio { get; set; }
    public string Avatar { get; set; }
    public string CoverImage { get; set; }

    [Required]
    public UserType UserType { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

// Freelancer-specific fields
public class FreelancerUser : User
{
    [Required]
    public List<Skill> Skills { get; set; } = new List<Skill>();

    [Required]
    public List<Education> Education { get; set; } = new List<Education>();

    [Required]
    public List<Experience> Experience { get; set; } = new List<Experience>();
}

// Client-specific fields 
public class ClientUser : User
{ }
